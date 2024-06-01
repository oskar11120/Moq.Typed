using Microsoft.CodeAnalysis;
using System.Data.Common;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;

namespace Moq.Typed;

internal static class TypedMockGenerator
{
    private static void WriteParametersContainerType(MethodWritingContext method)
    {
        var output = method.Output;
        output.AppendLine();
        output.AppendLine($"public {(method.AnyRefs ? $"ref struct" : "class")} {method.ParametersContainingType}");
        output.AppendLine("{");
        method.ForEachParameterWrite(
            static (parameter, @ref) => $"public{@ref} {parameter.Type} {parameter.Name};",
            false,
            1);
        output.AppendLine("}");
    }

    private static void WriteSetupType(MethodWritingContext method)
    {
        var symbol = method.Symbol;
        var output = method.Output;
        var setupMoqInterface = symbol.ReturnsVoid ?
            $"ISetup<{method.ContainingType}>" :
            $"ISetup<{method.ContainingType}, {symbol.ReturnType}>";

        output.AppendLine($$"""

            public class {{method.SetupType}}
            {
            """);

        using var indentation_insideClass = output.Indent();
        output.AppendLine($$"""
            private readonly {{setupMoqInterface}} setup;
            
            public {{method.SetupTypeConstructorName}}({{setupMoqInterface}} setup)
            {
                this.setup = setup;
            }
            """);

        var parameterTypesText = symbol.Parameters.Length is 0 ?
            string.Empty :
            $"<{string.Join(", ", symbol.Parameters.Select(parameter => parameter.Type.ToDisplayString()))}>";
        var paramterTexts = symbol.Parameters.Select(parameter => parameter.RefKind is RefKind.Out ?
            $"{parameter.Type} {parameter.Name}" : $"{parameter.ToDisplayString()}");

        void WriteMethod(MethodWritingContext.Delegates delegates)
        {
            var (name, parameterName) = delegates.Return ? ("Returns", "valueFunction") : ("Callback", "callback");
            output.AppendLine();
            output.AppendLine($"public {method.SetupType} {name}({delegates.PublicType} {parameterName})");
            output.AppendLine($$"""
            {
                setup.{{name}}(new {{delegates.InternalType}}(
                    ({{string.Join(", ", paramterTexts)}}) => 
                    {
                        var __parameters__ = new {{method.ParametersContainingType}}
                        {
            """);

            method.ForEachParameterWrite(
                static (parameter, @ref) => $"{parameter.Name} ={@ref} {parameter.Name}", 
                true, 
                4);

            var @ref = method.AnyRefs ? "ref " : null;
            output.AppendLine($$"""
                        };
                        {{(delegates.Return ? "return " : null)}}{{parameterName}}({{@ref}}__parameters__);
                    }));
                return this;
            }
            """);
        }

        method.ForEachDelegate(WriteMethod);

        // TODO
        //if (!method.Symbol.ReturnsVoid)
        //{
        //    output.AppendLine($$"""

        //    public {{typeNameWithGenericParameters}} Returns({{method.ReturnType}} value)
        //        => Returns(_ => value);
        //    """);
        //}

        indentation_insideClass.Dispose();
        output.AppendLine("}");
    }

    private static void WriteDelegates(MethodWritingContext method)
    {
        var output = method.Output;
        void WriteInternal(MethodWritingContext.Delegates delegates)
        {
            var symbol = delegates.OfMethod;
            output.AppendLine();
            output.AppendLine($"private delegate {delegates.ReturnType} {delegates.InternalType}(");
            method.ForEachParameterWrite(
                (parameter, _) => parameter.RefKind is RefKind.Out ? $"{parameter.Type} {parameter.Name}" : parameter.ToDisplayString(),
                true,
                1);
            output.AppendIgnoringIndentation(");");
        }

        var @ref = method.AnyRefs ? "ref " : null;
        void WritePublic(MethodWritingContext.Delegates delegates)
        {
            output.AppendLine();
            output.AppendLine(
                $"public delegate {delegates.ReturnType} {delegates.PublicType}(" +
                $"{@ref}{method.ParametersContainingType} parameters);");
        }

        method.ForEachDelegate(WriteInternal);
        method.ForEachDelegate(WritePublic);
    }

    private static void WriteGenericTypeConstraints(IMethodSymbol method, int atIndentation, IndentingStringBuilder output)
    {
        using var indentation = output.Indent(atIndentation);
        var atLeastOnce = false;
        void Write(ITypeParameterSymbol type)
        {
            void Write(string contraintSuffix)
            {
                if (!atLeastOnce)
                {
                    output.AppendLine($$"""
                        where {{type}} : {{contraintSuffix}}
                        """);
                    atLeastOnce = true;
                }
                else
                    output.AppendIgnoringIndentation($$"""
                    , {{contraintSuffix}}
                    """);
            }

            if (type.HasConstructorConstraint)
                Write("new()");
            if (type.HasNotNullConstraint)
                Write("notnull");
            if (type.HasReferenceTypeConstraint)
                Write("class");
            if (type.HasUnmanagedTypeConstraint)
                Write("unmanaged");
            if (type.HasValueTypeConstraint)
                Write("struct");
            foreach (var constraintType in type.ConstraintTypes)
                Write(constraintType.ToDisplayString());
        }
        foreach (var typeParameter in method.TypeParameters)
            Write(typeParameter);
    }

    private static void WriteMethod(MethodWritingContext method)
    {
        var symbol = method.Symbol;
        var output = method.Output;
        output.AppendLine($"""

            public {method.SetupType} {method.Name}
            """);

        var methodParameters = symbol.Parameters;
        output.AppendIgnoringIndentation("(");
        static string Predicate(IParameterSymbol parameter) => $"Func<{parameter.Type}, bool>";
        method.ForEachParameterWrite(
            static (parameter, _) => parameter.RefKind switch
            {
                RefKind.None => $"{Predicate(parameter)}? {parameter.Name} = null",
                RefKind.Out => $"{parameter.Type} {parameter.Name} = default({parameter.Type})!",
                _ => string.Empty
            },
            true,
            1);
        output.AppendIgnoringIndentation(")");

        WriteGenericTypeConstraints(symbol, atIndentation: 1, output);
        output.AppendLine("{");
        using var indentation_insideMethod = output.Indent(1);
        method.ForEachParameterWrite(
            static (parameter, method) => parameter.RefKind is RefKind.None ?
                $"""
                {parameter.Name} ??= static _ => true;
                Expression<{Predicate(parameter)}> {parameter.Name}Expression = argument => {parameter.Name}(argument);
                """ :
                string.Empty,
            false);

        output.AppendLine($"var __setup__ = mock.Setup(mock => mock.{method.Name}(");
        using var indentation_insideSetupDelegate = output.Indent(1);
        method.ForEachParameterWrite(
            static (parameter, method) => parameter.RefKind switch
            {
                RefKind.None => $"It.Is({parameter.Name}Expression)",
                RefKind.Out => $"out {parameter.Name}",
                RefKind.Ref or RefKind.RefReadOnlyParameter or RefKind.In => $"ref It.Ref<{parameter.Type}>.IsAny",
                _ => $"It.IsAny<{parameter.Type}>()"
            },
            true);
        output.AppendIgnoringIndentation("));");
        indentation_insideSetupDelegate.Dispose();
        output.AppendLine($"return new {method.SetupType}(__setup__);");

        indentation_insideMethod.Dispose();
        output.AppendLine("}");
    }

    private static void WriteProperty(IPropertySymbol property, string onTypeWithName, string overloadSuffix, IndentingStringBuilder output)
        => output.AppendLine($$"""

            public ISetup<{{onTypeWithName}}, {{property.Type}}> {{property.Name}}{{overloadSuffix}}()
            {
                return mock.Setup(mock => mock.{{property.Name}});
            }
            """);

    private static string OverloadSuffix(int overloadNumber)
        => overloadNumber is 0 ? string.Empty : overloadNumber.ToString(CultureInfo.InvariantCulture);

    public static void Run(SourceProductionContext context, INamedTypeSymbol forType)
    {
        static string TypeFullName(INamedTypeSymbol type)
            => (type.ContainingType is INamedTypeSymbol existing ? TypeFullName(existing) + "_" : null) + type.Name;

        var output = new IndentingStringBuilder();
        var @namespace = forType.ContainingNamespace.ToDisplayString();
        var typeName = forType.ToDisplayString();
        var typeShortName = TypeFullName(forType);
        var mockTypeName = $"Mock<{typeName}>";
        var setupsTypeName = $"TypedMockFor_{typeShortName}";
        var generatedCodeAttribute = "[GeneratedCode(\"Moq.Typed\", null)]";
        var classesSource = $$"""
            using Moq;
            using Moq.Language.Flow;
            using System;
            using System.CodeDom.Compiler;
            using System.Linq.Expressions;

            namespace {{@namespace}}
            {
                {{generatedCodeAttribute}}
                internal static class TypedMockSetupExtensionFor_{{typeShortName}}
                {
                    public static {{setupsTypeName}} Setup(this {{mockTypeName}} mock)
                        => new {{setupsTypeName}}(mock);
                }

                {{generatedCodeAttribute}}
                internal sealed class {{setupsTypeName}}
                {
                    private readonly {{mockTypeName}} mock;

                    public {{setupsTypeName}}({{mockTypeName}} mock)
                    {
                        this.mock = mock;
                    }
            """;
        output.Append(classesSource);

        var mockableMembers = forType
            .GetMembers()
            .Where(member =>
                (member.IsAbstract || member.IsVirtual)
                && member.DeclaredAccessibility is not Accessibility.Private
                && member is not IMethodSymbol { MethodKind: MethodKind.PropertyGet or MethodKind.PropertySet });

        var overloadCounts = new OverloadCounter();
        using var indentation = output.Indent(2);
        foreach (var member in mockableMembers)
        {
            if (member is IMethodSymbol methodSymbol)
            {
                var overloadNumber = overloadCounts.Add(methodSymbol.Name);
                var method = new MethodWritingContext(methodSymbol, OverloadSuffix(overloadNumber), typeName, output);
                WriteParametersContainerType(method);
                WriteDelegates(method);
                WriteSetupType(method);
                WriteMethod(method);
            }
            else if (member is IPropertySymbol property)
            {
                var overloadNumber = overloadCounts.Add(property.Name);
                var overloadSuffix = OverloadSuffix(overloadNumber);
                WriteProperty(property, typeName, overloadSuffix, output);
            }
            else
                throw new NotSupportedException($"Unsupported member {member}.");

        }
        indentation.Dispose();

        output.AppendLine("""
                }
            }

            """);
        context.AddSource($"{@namespace}.{forType.Name}", output.ToString());
    }

    private static bool IsRef(IParameterSymbol parameter) => parameter.RefKind is RefKind.Ref;
    private static string Comma(int i, int length) => i < length - 1 ? ", " : string.Empty;

    private readonly struct MethodWritingContext
    {
        public readonly record struct Delegates(string PublicType, bool Return, IMethodSymbol OfMethod)
        {
            public readonly string InternalType = "Internal" + PublicType;
            public readonly string ReturnType = Return ? OfMethod.ReturnType.ToDisplayString() : "void";
        }

        public readonly IMethodSymbol Symbol;
        public readonly string ContainingType;
        public readonly IndentingStringBuilder Output;

        public MethodWritingContext(IMethodSymbol symbol, string overloadSuffix, string containingTypeName, IndentingStringBuilder output)
        {
            Symbol = symbol;
            ContainingType = containingTypeName;
            Output = output;

            this.overloadSuffix = overloadSuffix;
            genericTypeParameters = GetGenericTypeParameters();

            Name = Symbol.Name + genericTypeParameters;
            ParametersContainingType = Symbol.Name + "Parameters" + this.overloadSuffix + genericTypeParameters;
            SetupTypeConstructorName = Symbol.Name + "Setup" + this.overloadSuffix;
            SetupType = SetupTypeConstructorName + genericTypeParameters;
            AnyRefs = symbol.Parameters.Any(IsRef);
            CallbackDelegates = GetDelegates("Callback", genericTypeParameters, false);
            ValueFunctionDelegates = symbol.ReturnsVoid ? default : GetDelegates("ValueFunction", genericTypeParameters, true);
        }

        private readonly string overloadSuffix;
        private readonly string genericTypeParameters;

        public readonly string Name;
        public readonly string ParametersContainingType;
        public readonly string SetupTypeConstructorName;
        public readonly string SetupType;
        public readonly Delegates CallbackDelegates;
        public readonly Delegates ValueFunctionDelegates;
        public readonly bool AnyRefs;

        public void ForEachDelegate(Action<Delegates> action)
        {
            action(CallbackDelegates);
            if (!Symbol.ReturnsVoid)
                action(ValueFunctionDelegates);
        }

        public delegate string GetText(IParameterSymbol symbol, string? @refPrefix);
        public void ForEachParameterWrite(GetText getText, bool comaDelimit, int atIndentation = 0)
        {
            var parameters = Symbol.Parameters;
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var @ref = IsRef(parameter) ? " ref" : null;
                var text = getText(parameter, @ref);
                Output.AppendLine(text, atIndentation);
                if (comaDelimit)
                    Output.AppendIgnoringIndentation(Comma(i, parameters.Length));
            }
        }

        private Delegates GetDelegates(string kind, string genericTypeParameters, bool @return) =>
            new(Symbol.MetadataName + kind + overloadSuffix + genericTypeParameters, @return, Symbol);

        private string GetGenericTypeParameters()
        {
            var typeParameters = Symbol.TypeParameters;
            if (typeParameters.Length is 0)
                return string.Empty;

            var output = new StringBuilder();
            output.Append("<");
            for (int i = 0; i < typeParameters.Length; i++)
                output.Append($"{typeParameters[i]}{Comma(i, typeParameters.Length)}");
            output.Append(">");
            return output.ToString();
        }
    }

    private readonly struct OverloadCounter
    {
        private readonly Dictionary<string, int> state = new();

        public OverloadCounter()
        {
        }

        public int Add(string methodOrPropertyName)
        {
            var name = methodOrPropertyName;
            var current = state.TryGetValue(name, out var value) ? value : -1;
            var @new = current + 1;
            state[name] = @new;
            return @new;
        }
    }
}

using Microsoft.CodeAnalysis;
using System;
using System.Globalization;
using System.Text;

namespace Moq.Typed;

internal static class TypedMockGenerator
{
    private static void WriteParametersContainerType(Method method, IndentingStringBuilder output)
    {
        output.AppendLine();
        output.AppendLine($"public {(method.AnyInsOrRefs ? $"ref struct" : "class")} {method.ParametersContainerTypeName}");
        output.AppendIgnoringIndentation(method.GenericTypeParameters);
        output.AppendLine("{");

        static string? Ref(IParameterSymbol parameter) => IsRef(parameter) ? " ref" : null;
        foreach (var parameter in method.Symbol.Parameters)
            output.AppendLine($"public{Ref(parameter)} {parameter.Type} {parameter.Name};", 1);
        output.AppendLine("}");
    }

    private static void WriteSetupType(Method method, IndentingStringBuilder output)
    {
        var symbol = method.Symbol;
        var typeNameWithGenericParameters = $"{method.SetupTypeName}{method.GenericTypeParameters}";
        var containerTypeNameWithGenericParameters = $"{method.ParametersContainerTypeName}{method.GenericTypeParameters}";
        var setupMoqInterface = symbol.ReturnsVoid ?
            $"ISetup<{method.ContainingTypeName}>" :
            $"ISetup<{method.ContainingTypeName}, {symbol.ReturnType}>";

        output.AppendLine($$"""

            public class {{typeNameWithGenericParameters}}
            {
            """);

        using var indentation_insideClass = output.Indent();
        output.AppendLine($$"""
            private readonly {{setupMoqInterface}} setup;
            
            public {{method.SetupTypeName}}({{setupMoqInterface}} setup)
            {
                this.setup = setup;
            }
            """);

        var parameterTypesText = symbol.Parameters.Length is 0 ?
            string.Empty :
            $"<{string.Join(", ", symbol.Parameters.Select(parameter => parameter.Type.ToDisplayString()))}>";
        var paramterTexts = symbol.Parameters.Select(parameter => parameter.RefKind is RefKind.Out ?
            $"{parameter.Type} {parameter.Name}" : $"{parameter.ToDisplayString()}");

        void WriteMethod(Method.Delegates delegates)
        {
            var (name, parameterName) = delegates.Return ? ("Returns", "valueFunction") : ("Callback", "callback");
            output.AppendLine();
            output.AppendLine($"public {typeNameWithGenericParameters} {name}({delegates.PublicType}{method.GenericTypeParameters} {parameterName})");
            output.AppendLine($$"""
            {
                setup.{{name}}(new {{delegates.InternalType + method.GenericTypeParameters}}(
                    ({{string.Join(", ", paramterTexts)}}) => 
                    {
                        var __parameters__ = new {{containerTypeNameWithGenericParameters}}
                        {
            """);

            foreach (var parameter in method.Symbol.Parameters)
                output.AppendLine($"{parameter.Name} = {(parameter.RefKind is RefKind.Ref ? "ref " : null)}{parameter.Name}", 4);

            output.AppendLine($$"""
                        };
                        {{(delegates.Return ? "return " : null)}}{{parameterName}}({{(method.AnyRefs ? "ref " : null)}}__parameters__);
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

    private static void WriteDelegates(Method method, IndentingStringBuilder output)
    {
        void WriteInternal(Method.Delegates delegates)
        {
            var symbol = delegates.OfMethod;
            output.AppendLine();
            output.AppendLine($"private delegate {delegates.ReturnType} {delegates.InternalType}{method.GenericTypeParameters}(");
            for (int i = 0; i < symbol.Parameters.Length; i++)
            {
                var parameter = symbol.Parameters[i];
                var paramterText = parameter.RefKind is RefKind.Out ? $"{parameter.Type} {parameter.Name}" : parameter.ToDisplayString();
                output.AppendLine($"{paramterText}", 1);
                output.AppendIgnoringIndentation(Comma(i, symbol.Parameters.Length));
            }
            output.AppendIgnoringIndentation(");");
        }

        var @ref = method.AnyRefs ? "ref " : null;
        void WritePublic(Method.Delegates delegates)
        {
            output.AppendLine();
            output.AppendLine(
                $"public delegate {delegates.ReturnType} {delegates.PublicType}{method.GenericTypeParameters}(" +
                $"{@ref}{method.ParametersContainerTypeName}{method.GenericTypeParameters} parameters);");
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

    private static void WriteMethod(Method method, IndentingStringBuilder output)
    {
        var symbol = method.Symbol;
        var setupTypeNameWithGenericParameters = method.SetupTypeName + method.GenericTypeParameters;
        output.AppendLine($"""

            public {setupTypeNameWithGenericParameters} {symbol.Name}{method.GenericTypeParameters}
            """);

        static string Predicate(IParameterSymbol parameter) => $"Func<{parameter.Type}, bool>";
        var methodParameters = symbol.Parameters;
        output.AppendIgnoringIndentation("(");
        for (int i = 0; i < methodParameters.Length; i++)
        {
            var parameter = methodParameters[i];
            var text = parameter.RefKind switch
            {
                RefKind.None => $"{Predicate(parameter)}? {parameter.Name} = null{Comma(i, methodParameters.Length)}",
                RefKind.Out => $"{parameter.Type} {parameter.Name} = default({parameter.Type})!{Comma(i, methodParameters.Length)}",
                _ => string.Empty
            };
            output.AppendLine(text, 1);
        }
        output.AppendIgnoringIndentation(")");

        WriteGenericTypeConstraints(symbol, atIndentation: 1, output);
        output.AppendLine("{");
        using var indentation_insideMethod = output.Indent(1);
        foreach (var parameter in methodParameters)
            if (parameter.RefKind is RefKind.None)
                output.AppendLine($"""
                    {parameter.Name} ??= static _ => true;
                    Expression<{Predicate(parameter)}> {parameter.Name}Expression = argument => {parameter.Name}(argument);
                    """);

        output.AppendLine($"var __setup__ = mock.Setup(mock => mock.{symbol.Name}{method.GenericTypeParameters}(");
        using var indentation_insideSetupDelegate = output.Indent(1);
        for (int i = 0; i < methodParameters.Length; i++)
        {
            var parameter = methodParameters[i];
            var text = parameter.RefKind switch
            {
                RefKind.None => $"It.Is({parameter.Name}Expression)",
                RefKind.Out => $"out {parameter.Name}",
                RefKind.Ref or RefKind.RefReadOnlyParameter or RefKind.In => $"ref It.Ref<{parameter.Type}>.IsAny",
                _ => $"It.IsAny<{parameter.Type}>()"
            };
            output.AppendLine(text);
            output.AppendIgnoringIndentation(Comma(i, methodParameters.Length));
        }
        output.AppendIgnoringIndentation("));");
        indentation_insideSetupDelegate.Dispose();
        output.AppendLine($"return new {setupTypeNameWithGenericParameters}(__setup__);");

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
                var method = new Method(methodSymbol, OverloadSuffix(overloadNumber), typeName);
                WriteParametersContainerType(method, output);
                WriteDelegates(method, output);
                WriteSetupType(method, output);
                WriteMethod(method, output);
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

    private readonly struct Method
    {
        public readonly record struct Delegates(string PublicType, bool Return, IMethodSymbol OfMethod)
        {
            public readonly string InternalType = "Internal" + PublicType;
            public readonly string ReturnType = Return ? OfMethod.ReturnType.ToDisplayString() : "void";
        }

        public readonly IMethodSymbol Symbol;
        public readonly string ContainingTypeName;

        public Method(IMethodSymbol symbol, string overloadSuffix, string containingTypeName)
        {
            Symbol = symbol;
            OverloadSuffix = overloadSuffix;
            ContainingTypeName = containingTypeName;

            ParametersContainerTypeName = Symbol.Name + "Parameters" + OverloadSuffix;
            SetupTypeName = Symbol.Name + "Setup" + OverloadSuffix;
            AnyRefs = symbol.Parameters.Any(IsRef);
            AnyInsOrRefs = AnyRefs || symbol.Parameters.Any(parameter => parameter.RefKind is RefKind.In);
            GenericTypeParameters = GetGenericTypeParameters();
            CallbackDelegates = GetDelegates("Callback", false);
            ValueFunctionDelegates = symbol.ReturnsVoid ? default : GetDelegates("ValueFunction", true);
        }

        public readonly string OverloadSuffix;
        public readonly string ParametersContainerTypeName;
        public readonly string SetupTypeName;
        public readonly Delegates CallbackDelegates;
        public readonly Delegates ValueFunctionDelegates;
        public readonly bool AnyRefs;
        public readonly bool AnyInsOrRefs;
        public readonly string GenericTypeParameters;

        public void ForEachDelegate(Action<Delegates> action)
        {
            action(CallbackDelegates);
            if (!Symbol.ReturnsVoid)
                action(ValueFunctionDelegates);
        }

        private Delegates GetDelegates(string kind, bool @return) =>
            new(Symbol.MetadataName + kind + OverloadSuffix, @return, Symbol);

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

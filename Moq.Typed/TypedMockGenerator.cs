using Microsoft.CodeAnalysis;
using System.Globalization;

namespace Moq.Typed;

internal static class TypedMockGenerator
{
    private static bool IsRef(IParameterSymbol parameter) => parameter.RefKind is RefKind.Ref;
    private static string Comma(int i, int length) => i < length - 1 ? ", " : string.Empty;
    private static string OverloadSuffix(int number) =>
        number is 0 ? string.Empty : number.ToString(CultureInfo.InvariantCulture);
    private static string ParametersContainerTypeName(IMethodSymbol method, int methodOverloadNumber) =>
        method.Name + "Parameters" + OverloadSuffix(methodOverloadNumber);
    private static string SetupTypeName(IMethodSymbol method, int methodOverloadNumber) =>
        method.Name + "Setup" + OverloadSuffix(methodOverloadNumber);
    private static string ParametersDelegateTypeName(IMethodSymbol method, int methodOverloadNumber, bool returnsAnything)
        => method.MetadataName + (returnsAnything ? "ValueFunction" : "Callback") + OverloadSuffix(methodOverloadNumber);

    private static bool WriteGenericTypeParameters(IMethodSymbol method, IndentingStringBuilder output)
    {
        var typeParameters = method.TypeParameters;
        if (typeParameters.Length is 0)
            return false;

        output.AppendIgnoringIndentation("<");
        for (int i = 0; i < typeParameters.Length; i++)
            output.AppendIgnoringIndentation($"{typeParameters[i]}{Comma(i, typeParameters.Length)}");
        output.AppendIgnoringIndentation(">");
        return true;
    }

    private static string GetGenericTypeParameters(IMethodSymbol method)
    {
        var output = new IndentingStringBuilder();
        WriteGenericTypeParameters(method, output);
        return output.ToString();
    }

    private readonly record struct AnyInsOrRefs(bool Refs, bool InsOrRefs)
    {
        public static AnyInsOrRefs Check(IMethodSymbol method)
        {
            var anyRefs = method.Parameters.Any(IsRef);
            var anyInsOrRefs = anyRefs || method.Parameters.Any(parameter => parameter.RefKind is RefKind.In);
            return new AnyInsOrRefs(anyRefs, anyInsOrRefs);
        }
    }

    private static void WriteParametersContainerType(IMethodSymbol method, int methodOverloadNumber, IndentingStringBuilder output)
    {
        var any = AnyInsOrRefs.Check(method);
        output.AppendLine();
        output.AppendLine($"public {(any.InsOrRefs ? $"ref struct" : "class")} {ParametersContainerTypeName(method, methodOverloadNumber)}");
        WriteGenericTypeParameters(method, output);
        output.AppendLine("{");

        static string? Ref(IParameterSymbol parameter) => IsRef(parameter) ? " ref" : null;
        foreach (var parameter in method.Parameters)
            output.AppendLine($"public{Ref(parameter)} {parameter.Type} {parameter.Name};", 1);
        output.AppendLine("}");
    }

    private static void WriteSetupType(IMethodSymbol method, int methodOverloadNumber, string onTypeWithName, IndentingStringBuilder output)
    {
        var typeName = SetupTypeName(method, methodOverloadNumber);
        var containerTypeName = ParametersContainerTypeName(method, methodOverloadNumber);
        var setupTypeName = method.ReturnsVoid ? $"ISetup<{onTypeWithName}>" : $"ISetup<{onTypeWithName}, {method.ReturnType}>";
        var genericTypeParameters = GetGenericTypeParameters(method);
        var typeNameWithGenericParameters = $"{typeName}{genericTypeParameters}";
        var containerTypeNameWithGenericParameters = $"{containerTypeName}{genericTypeParameters}";

        output.AppendLine($$"""

            public class {{typeNameWithGenericParameters}}
            {
            """);

        using var indentation_insideClass = output.Indent();
        output.AppendLine($$"""
            private readonly {{setupTypeName}} setup;
            
            public {{typeName}}({{setupTypeName}} setup)
            {
                this.setup = setup;
            }
            """);

        var parameterTypesText = method.Parameters.Length is 0 ?
            string.Empty :
            $"<{string.Join(", ", method.Parameters.Select(parameter => parameter.Type.ToDisplayString()))}>";
        var paramterTexts = method.Parameters.Select(parameter => parameter.RefKind is RefKind.Out ? 
            $"{parameter.Type} {parameter.Name}" : $"{parameter.ToDisplayString()}");
        var anyRefs = AnyInsOrRefs.Check(method).Refs;

        void WriteMethod(string name, string parameterDelegateName, bool callbackReturns)
        {
            var delegateTypeName = ParametersDelegateTypeName(method, methodOverloadNumber, callbackReturns);
            output.AppendLine();
            output.AppendLine($"public {typeNameWithGenericParameters} {name}({delegateTypeName}{genericTypeParameters} {parameterDelegateName})");
            output.AppendLine($$"""
            {
                setup.{{name}}(new {{"Internal" + delegateTypeName + genericTypeParameters}}(
                    ({{string.Join(", ", paramterTexts)}}) => 
                    {
                        var __parameters__ = new {{containerTypeNameWithGenericParameters}}
                        {
            """);

            foreach (var parameter in method.Parameters)
                output.AppendLine($"{parameter.Name} = {(parameter.RefKind is RefKind.Ref ? "ref " : null)}{parameter.Name}", 4);

            output.AppendLine($$"""
                        };
                        {{(callbackReturns ? "return " : null)}}{{parameterDelegateName}}({{(anyRefs ? "ref " : null)}}__parameters__);
                    }));
                return this;
            }
            """);
        }

        WriteMethod("Callback", "callback", false);

        if (!method.ReturnsVoid)
        {
            // TODO
            //output.AppendLine($$"""

            //public {{typeNameWithGenericParameters}} Returns({{method.ReturnType}} value)
            //    => Returns(_ => value);
            //""");

            WriteMethod("Returns", "valueFunction", true);
        }

        indentation_insideClass.Dispose();
        output.AppendLine("}");
    }

    private static void WriteParametersDelegates(IMethodSymbol method, int methodOverloadNumber, IndentingStringBuilder output)
    {
        string ReturnType(bool returns) => returns ? method.ReturnType.ToDisplayString() : "void";

        var genericTypeParameters = GetGenericTypeParameters(method);
        void WriteInternal(bool returns)
        {
            var delegateTypeName = "Internal" + ParametersDelegateTypeName(method, methodOverloadNumber, returns);
            output.AppendLine();
            output.AppendLine($"private delegate {ReturnType(returns)} {delegateTypeName}{genericTypeParameters}(");
            for (int i = 0; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                var paramterText = parameter.RefKind is RefKind.Out ? $"{parameter.Type} {parameter.Name}" : parameter.ToDisplayString();
                output.AppendLine($"{paramterText}", 1);
                output.AppendIgnoringIndentation(Comma(i, method.Parameters.Length));
            }
            output.AppendIgnoringIndentation(");");
        }
        WriteInternal(false);
        if (!method.ReturnsVoid)
            WriteInternal(true);

        var containerTypeName = ParametersContainerTypeName(method, methodOverloadNumber);
        var anyRef = AnyInsOrRefs.Check(method).Refs;
        string? Ref() => anyRef ? "ref " : null;
        void WritePublic(bool returns)
        {
            var delegateTypeName = ParametersDelegateTypeName(method, methodOverloadNumber, returns);
            output.AppendLine();
            output.AppendLine(
                $"public delegate {ReturnType(returns)} {delegateTypeName}{genericTypeParameters}(" +
                $"{Ref()}{containerTypeName}{genericTypeParameters} parameters);");
        }
        WritePublic(false);
        if (!method.ReturnsVoid)
            WritePublic(true);
    }

    private static void WriteGenericTypeConstraints(IMethodSymbol method, IndentingStringBuilder output, int atIndentation)
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

    private static void WriteMethod(IMethodSymbol method, int methodOverloadNumber, IndentingStringBuilder output)
    {
        string typeParametersOutput = GetGenericTypeParameters(method);
        var setupTypeNameWithGenericParameters = SetupTypeName(method, methodOverloadNumber) + typeParametersOutput;
        output.AppendLine($"""

            public {setupTypeNameWithGenericParameters} {method.Name}
            """);

        if (typeParametersOutput.Length != 0)
            output.AppendIgnoringIndentation(typeParametersOutput);

        static string Predicate(IParameterSymbol parameter) => $"Func<{parameter.Type}, bool>";
        var methodParameters = method.Parameters;
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

        WriteGenericTypeConstraints(method, output, atIndentation: 1);
        output.AppendLine("{");
        using var indentation_insideMethod = output.Indent(1);
        foreach (var parameter in methodParameters)
            if (parameter.RefKind is RefKind.None)
                output.AppendLine($"""
                    {parameter.Name} ??= static _ => true;
                    Expression<{Predicate(parameter)}> {parameter.Name}Expression = argument => {parameter.Name}(argument);
                    """);

        output.AppendLine($"var __setup__ = mock.Setup(mock => mock.{method.Name}{typeParametersOutput}(");
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

    private static void WriteProperty(IPropertySymbol property, string onTypeWithName, int overloadNumber, IndentingStringBuilder output)
        => output.AppendLine($$"""

            public ISetup<{{onTypeWithName}}, {{property.Type}}> {{property.Name}}{{OverloadSuffix(overloadNumber)}}()
            {
                return mock.Setup(mock => mock.{{property.Name}});
            }
            """);

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
            if (member is IMethodSymbol method)
            {
                var overloadNumber = overloadCounts.Add(method.Name);
                WriteParametersContainerType(method, overloadNumber, output);
                WriteParametersDelegates(method, overloadNumber, output);
                WriteSetupType(method, overloadNumber, typeName, output);
                WriteMethod(method, overloadNumber, output);
            }
            else if (member is IPropertySymbol property)
            {
                var overloadNumber = overloadCounts.Add(property.Name);
                WriteProperty(property, typeName, overloadNumber, output);
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

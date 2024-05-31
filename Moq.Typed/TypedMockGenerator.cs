using Microsoft.CodeAnalysis;
using System.Globalization;

namespace Moq.Typed;

internal static class TypedMockGenerator
{
    private static string Comma(int i, int length) => i < length - 1 ? ", " : string.Empty;
    private static string OverloadSuffix(int number) =>
        number is 0 ? string.Empty : number.ToString(CultureInfo.InvariantCulture);
    private static string ParametersContainerTypeName(IMethodSymbol method, int methodOverloadNumber) =>
        method.Name + "Parameters" + OverloadSuffix(methodOverloadNumber);
    private static string SetupTypeName(IMethodSymbol method, int methodOverloadNumber) =>
        method.Name + "Setup" + OverloadSuffix(methodOverloadNumber);

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

    private static void WriteParametersContainerType(IMethodSymbol method, int methodOverloadNumber, IndentingStringBuilder output)
    {
        output.AppendLine($"public class {ParametersContainerTypeName(method, methodOverloadNumber)}");
        WriteGenericTypeParameters(method, output);
        output.AppendLine("{");
        foreach (var parameter in method.Parameters)
            output.AppendLine($$"""public {{parameter.Type}} {{parameter.Name}} { get; init; }""", 1);
        output.AppendLine("}");
    }

    private static void WriteSetupType(IMethodSymbol forMethod, int withOverloadNumber, string onTypeWithName, IndentingStringBuilder output)
    {
        var typeName = SetupTypeName(forMethod, withOverloadNumber);
        var containerTypeName = ParametersContainerTypeName(forMethod, withOverloadNumber);
        var setupTypeName = forMethod.ReturnsVoid ? $"ISetup<{onTypeWithName}>" : $"ISetup<{onTypeWithName}, {forMethod.ReturnType}>";
        var genericTypeParameters = GetGenericTypeParameters(forMethod);
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

        var parameterTypesText = forMethod.Parameters.Length is 0 ?
            string.Empty :
            $"<{string.Join(", ", forMethod.Parameters.Select(parameter => parameter.Type.ToDisplayString()))}>";
        var parameterNames = forMethod.Parameters.Select(parameter => parameter.Name);

        void WriteMethod(string name, string parameterDelegateType, string parameterDelegateName, bool callbackReturns)
        {
            output.AppendLine();
            output.AppendLine($"public {typeNameWithGenericParameters} {name}({parameterDelegateType} {parameterDelegateName})");
            output.AppendLine($$"""
            {
                setup.{{name}}{{parameterTypesText}}(
                    ({{string.Join(", ", parameterNames)}}) => 
                    {
                        var parameters = new {{containerTypeNameWithGenericParameters}}
                        {
            """);

            foreach (var parameterName in parameterNames)
                output.AppendLine($"{parameterName} = {parameterName}", 5);

            output.AppendLine($$"""
                        };
                        {{(callbackReturns ? "return " : null)}}{{parameterDelegateName}}(parameters);
                    });
                return this;
            }
            """);
        }

        WriteMethod("Callback", $"Action<{containerTypeNameWithGenericParameters}>", "callback", false);

        if (!forMethod.ReturnsVoid)
        {
            output.AppendLine($$"""

            public {{typeNameWithGenericParameters}} Returns({{forMethod.ReturnType}} value)
                => Returns(_ => value);
            """);

            WriteMethod("Returns", $"Func<{containerTypeNameWithGenericParameters}, {forMethod.ReturnType}>", "valueFunction", true);
        }

        indentation_insideClass.Dispose();
        output.AppendLine("}");
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
                RefKind.Out => $"{parameter.Type} {parameter.Name} = default({parameter.Type})!",
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
        var output = new IndentingStringBuilder();
        var @namespace = forType.ContainingNamespace.ToDisplayString();
        var typeName = forType.ToDisplayString();
        var typeShortName = forType.Name;
        var mockTypeName = $"Mock<{typeName}>";
        var setupsTypeName = $"TypedMock_For{typeShortName}";
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
                internal static class TypedMockSetupExtension_For{{typeShortName}}
                {
                    public static {{setupsTypeName}} Setup(this {{mockTypeName}} mock)
                        => new {{setupsTypeName}}(mock);
                }

                {{generatedCodeAttribute}}
                internal class {{setupsTypeName}}
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

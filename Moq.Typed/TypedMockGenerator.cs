using Microsoft.CodeAnalysis;
using System.Xml.Linq;

namespace Moq.Typed;

internal static class TypedMockGenerator
{
    private static string Comma(int i, int length) => i < length - 1 ? "," : string.Empty;
    private static string ParametersContainerTypeName(IMethodSymbol method) => method.Name + "Parameters";
    private static string SetupTypeName(IMethodSymbol method) => method.Name + "Setup";

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

    private static void WriteParametersContainerType(IMethodSymbol method, IndentingStringBuilder output)
    {
        output.Append($$"""


            public class {{ParametersContainerTypeName(method)}}
            """);
        WriteGenericTypeParameters(method, output);
        output.Append("""
  
            {
            """);

        foreach (var parameter in method.Parameters)
            output.Append($$"""

                    public {{parameter.Type}} {{parameter.Name}} { get; init; }
                """);

        output.Append("""

            }
            """);
    }

    private static void WriteSetupType(IMethodSymbol forMethod, string onTypeWithName, IndentingStringBuilder output)
    {
        var typeName = SetupTypeName(forMethod);
        var containerTypeName = ParametersContainerTypeName(forMethod);
        var setupTypeName = forMethod.ReturnsVoid ? $"ISetup<{onTypeWithName}>" : $"ISetup<{onTypeWithName}, {forMethod.ReturnType}>";
        var genericTypeParameters = GetGenericTypeParameters(forMethod);
        var typeNameWithGenericParameters = $"{typeName}{genericTypeParameters}";
        var containerTypeNameWithGenericParameters = $"{containerTypeName}{genericTypeParameters}";

        output.Append($$"""


            public class {{typeNameWithGenericParameters}}
            """);

        output.Append($$"""

            {
                private readonly {{setupTypeName}} setup;
            
                public {{typeName}} ({{setupTypeName}} setup)
                {
                    this.setup = setup;
                }
            """);

        output.Append($$"""
  

                public {{typeNameWithGenericParameters}} Callback(Action<{{containerTypeNameWithGenericParameters}}> callback)
            """);
        var parameterTypesText = string.Join(",", forMethod.Parameters.Select(parameter => parameter.Type.ToDisplayString()));
        var parameterNames = forMethod.Parameters.Select(parameter => parameter.Name);
        output.Append($$"""

                {
                    setup.Callback<
                        {{parameterTypesText}}>(
                        ({{string.Join(", ", parameterNames)}}) => 
                        {
                            var parameters = new {{containerTypeNameWithGenericParameters}}
                            {
            """);
        foreach (var name in parameterNames)
            output.Append($$"""
            
                                    {{name}} = {{name}}
                """);
        output.Append($$"""

                            };
                            callback(parameters);
                        });
                    return this;
                }
            }
            """);
    }

    private static void WriteMethod(IMethodSymbol method, IndentingStringBuilder output)
    {
        string typeParametersOutput = GetGenericTypeParameters(method);
        var setupTypeNameWithGenericParameters = SetupTypeName(method) + typeParametersOutput;
        output.Append($"""


            public {setupTypeNameWithGenericParameters} {method.Name}
            """);

        if (typeParametersOutput.Length != 0)
            output.AppendIgnoringIndentation(typeParametersOutput);

        static string Predicate(IParameterSymbol parameter) => $"Func<{parameter.Type},bool>";
        var methodParameters = method.Parameters;
        output.AppendIgnoringIndentation("(");
        for (int i = 0; i < methodParameters.Length; i++)
        {
            var parameter = methodParameters[i];
            output.Append($$"""

                    {{Predicate(parameter)}}? {{parameter.Name}} = null{{Comma(i, methodParameters.Length)}}
                """);
        }

        output.AppendIgnoringIndentation(")");
        output.Append("""
            
            {
            """);
        foreach (var parameter in methodParameters)
            output.Append($"""
                
                {parameter.Name} ??= static _ => true;
                Expression<{Predicate(parameter)}> {parameter.Name}Expression = argument => {parameter.Name}(argument);
            """);

        output.Append($"""
                
                var __setup__ = mock.Setup(mock => mock.{method.Name}{typeParametersOutput}(
            """);
        for (int i = 0; i < methodParameters.Length; i++)
        {
            var parameter = methodParameters[i];
            output.Append($"""
                
                    It.Is({parameter.Name}Expression){Comma(i, methodParameters.Length)}
            """);
        }
        output.AppendIgnoringIndentation("));");
        output.Append($$"""
            
                return new {{setupTypeNameWithGenericParameters}}(__setup__);
            }
            """);
    }

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

        var mockableMethods = forType
            .GetMembers()
            .Where(member =>
                (member.IsAbstract || member.IsVirtual)
                && member.DeclaredAccessibility is not Accessibility.Private
                && member is not IMethodSymbol { MethodKind: MethodKind.PropertyGet or MethodKind.PropertySet })
            .Cast<IMethodSymbol>();

        try
        {
            using var indentation = output.Indent(2);
            foreach (var method in mockableMethods)
            {
                WriteParametersContainerType(method, output);
                WriteSetupType(method, typeName, output);
                WriteMethod(method, output);
            }
            indentation.Dispose();
        }
        catch (Exception e)
        {
            throw;
        }

        output.Append("""

                }
            }

            """);
        context.AddSource($"{@namespace}.{forType.Name}", output.ToString());
    }
}

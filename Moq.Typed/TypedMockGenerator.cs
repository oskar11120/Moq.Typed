using Microsoft.CodeAnalysis;
using System.Text;

namespace Moq.Typed;

internal static class TypedMockGenerator
{
    private static void WriteMethod(IMethodSymbol method, StringBuilder output)
    {
        static string Comma(int i, int length) => i < length - 1 ? "," : string.Empty;
        output.Append($"public {method.ReturnType} {method.Name}");

        var typeParametersSb = new StringBuilder();
        var typeParameters = method.TypeParameters;
        if (typeParameters.Length is not 0)
        {
            typeParametersSb.Append("<");
            for (int i = 0; i < typeParameters.Length; i++)
                typeParametersSb.Append($"""

                    {typeParameters[i]}{Comma(i, typeParameters.Length)}
                    """);
            typeParametersSb.Append(">");
        }
        var typeParametersOutput = typeParametersSb.ToString();
        output.Append(typeParametersOutput);

        static string Predicate(IParameterSymbol parameter) => $"Func<{parameter.Type},bool>";
        var methodParameters = method.Parameters;
        output.Append("(");
        for (int i = 0; i < methodParameters.Length; i++)
        {
            var parameter = methodParameters[i];
            output.Append($$"""

                {{Predicate(parameter)}}? {{parameter.Name}} = null{{Comma(i, methodParameters.Length)}}
                """);
        }

        output.Append("""
            )
            {
            """);
        foreach (var parameter in methodParameters)
            output.Append($"""
                
                {parameter.Name} ??= static _ => true;
                Expression<{Predicate(parameter)}> {parameter.Name}Expression = argument => {parameter.Name}(argument);
            """);

        output.Append($"""
                
                mock.Setup(mock => mock.{method.Name}{typeParametersOutput}(
            """);
        for (int i = 0; i < methodParameters.Length; i++)
        {
            var parameter = methodParameters[i];
            output.Append($"""
                
                It.Is({parameter.Name}Expression){Comma(i, methodParameters.Length)}
            """);
        }
        output.Append("));");
        output.Append("""
            
            }
            """);
    }

    public static void Run(SourceProductionContext context, INamedTypeSymbol forType)
    {
        var output = new StringBuilder();
        var @namespace = forType.ContainingNamespace.ToDisplayString();
        var typeName = forType.ToDisplayString();
        var typeShortName = forType.Name;
        var mockTypeName = $"Mock<{typeName}>";
        var setupsTypeName = $"{typeShortName}_TypedSetups";
        var classesSource = $$"""
            using Moq;
            using System;
            using System.Linq.Expressions;

            namespace {{@namespace}}
            {
                internal static class {{typeShortName}}_MockSetupExtension
                {
                    public static {{setupsTypeName}} Setup(this {{mockTypeName}} mock)
                        => new {{setupsTypeName}}(mock);
                }

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

        foreach (var method in mockableMethods)
            WriteMethod(method, output);

        output.Append("""
                }
            }

            """);
        context.AddSource($"{@namespace}.{forType.Name}", output.ToString());
    }
}

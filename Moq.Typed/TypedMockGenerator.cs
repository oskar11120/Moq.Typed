using Microsoft.CodeAnalysis;
using System.Text;

namespace Moq.Typed;

internal static class TypedMockGenerator
{
    private static void WriteMethod(IMethodSymbol method, IndentingStringBuilder output)
    {
        static string Comma(int i, int length) => i < length - 1 ? "," : string.Empty;
        output.Append($"public {method.ReturnType} {method.Name}");

        string typeParametersOutput;
        var typeParametersSb = new IndentingStringBuilder(output.IndentationLevel);
        var typeParameters = method.TypeParameters;
        if (typeParameters.Length is not 0)
        {
            typeParametersSb.Append("<");
            for (int i = 0; i < typeParameters.Length; i++)
                typeParametersSb.Append($"""

                        {typeParameters[i]}{Comma(i, typeParameters.Length)}
                    """);
            typeParametersSb.Append(">");
            typeParametersOutput = typeParametersSb.ToString();
            output.Append(typeParametersOutput);
        }
        else
            typeParametersOutput = string.Empty;
        

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
                
                mock.Setup(mock => mock.{method.Name}{typeParametersOutput}(
            """);
        for (int i = 0; i < methodParameters.Length; i++)
        {
            var parameter = methodParameters[i];
            output.Append($"""
                
                    It.Is({parameter.Name}Expression){Comma(i, methodParameters.Length)}
            """);
        }
        output.AppendIgnoringIndentation("));");
        output.Append("""
            
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

        try
        {
            using var indentation = output.Indent(2);
            foreach (var method in mockableMethods)
                WriteMethod(method, output);
            indentation.Dispose();
        }
        catch(Exception e)
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

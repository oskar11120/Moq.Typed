using Microsoft.CodeAnalysis;
using System.Globalization;

namespace Moq.Typed.Generators;
internal static partial class TypedMockGenerator
{
    private static void WriteProperty(PropertyWritingContext property, IndentingStringBuilder output)
    {
        var symbol = property.Symbol;
        var feature = property.Feature;
        var passParameter = feature.AdditionalMethodPropertyMockingParameter;
        var parameter = passParameter.Name is null ? null : $", {passParameter.Name}";
        var returnType = feature.HasSetupVerifyType ? $"I{feature.Name}<{feature.Type.Name}, {symbol.Type}>" : "void";
        var @return = feature.HasSetupVerifyType ? "return " : null;
        output.AppendLine($$"""

            public {{returnType}} {{symbol.Name}}{{property.OverloadSuffix}}({{passParameter.Text}})
            {
                {{@return}}mock.{{feature.Name}}(mock => mock.{{symbol.Name}}{{parameter}});
            }
            """);
    }

    private static void WriteFeature(FeatureWritingContext feature, IndentingStringBuilder output)
    {
        WriteMethodProviderGetExtension(feature, output);
        WriteMethodProvider(feature, output);
        var type = feature.Type;
        var mockableMembers = type
            .Symbol
            .GetMembers()
            .Where(member =>
                (member.IsAbstract || member.IsVirtual)
                && member.DeclaredAccessibility is not Accessibility.Private
                && member is not IMethodSymbol { MethodKind: MethodKind.PropertyGet or MethodKind.PropertySet });
        var overloadCounts = new OverloadCounter();

        foreach (var member in mockableMembers)
            if (member is IMethodSymbol methodSymbol)
            {
                var overloadNumber = overloadCounts.Add(methodSymbol.Name);
                var method = new MethodWritingContext(methodSymbol, OverloadSuffix(overloadNumber), feature, output);
                if (feature.HasSetupVerifyType)
                {
                    WriteParametersContainer(method);
                    WriteMethodDelegates(method);
                    WriteSetupType(method);
                }
                WriteMethod(method, feature);
            }
            else if (member is IPropertySymbol propertySymbol)
            {
                var overloadNumber = overloadCounts.Add(propertySymbol.Name);
                var overloadSuffix = OverloadSuffix(overloadNumber);
                var property = new PropertyWritingContext(propertySymbol, overloadSuffix, feature);
                WriteProperty(property, output);
            }
            else
                throw new NotSupportedException($"Unsupported member {member}.");
    }

    private static string OverloadSuffix(int overloadNumber)
        => overloadNumber is 0 ? string.Empty : overloadNumber.ToString(CultureInfo.InvariantCulture);

    public static void Run(SourceProductionContext context, INamedTypeSymbol forType)
    {
        using var output = new IndentingStringBuilder();
        var @namespace = forType.ContainingNamespace.ToDisplayString();
        output.Append($$"""
            using Moq;
            using Moq.Language.Flow;
            using System;
            using System.CodeDom.Compiler;
            using System.Linq.Expressions;
            using System.Threading.Tasks;

            namespace {{@namespace}}
            {
            """
            );

        using var namespaceIndentation = output.Indent();
        var type = new TypeInfo(forType);

        var setup = new FeatureWritingContext(
            "Setup",
            type);
        WriteFeature(setup, output);

        var verify = new FeatureWritingContext(
            "Verify",
            type,
            needsSetupType: false,
            extensionName: "Verifyy",
            additionalMethodPropertyMockingParameter: new("Times", "times"));
        WriteFeature(verify, output);

        namespaceIndentation.Dispose();
        output.AppendLine("""
            }

            """);
        context.AddSource($"{forType}".Replace('<', '_').Replace('>', '_'), output.ToString());
    }
}

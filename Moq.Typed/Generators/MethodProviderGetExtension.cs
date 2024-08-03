
namespace Moq.Typed.Generators;
internal static partial class TypedMockGenerator
{
    private static void WriteMethodProviderGetExtension(FeatureWritingContext feature, IndentingStringBuilder output)
    {
        var type = feature.Type;
        output.AppendLine($$"""

            {{generatedCodeAttribute}}
            internal static class TypedMock{{feature.Name}}ExtensionFor_{{type.ShortName}}
            {
                public static {{feature.TypeName}} {{feature.ExtensionName}}(this {{type.MockName}} mock)
                    => new {{feature.TypeName}}(mock);
            }
            """);
    }
}

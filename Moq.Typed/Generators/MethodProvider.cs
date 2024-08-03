namespace Moq.Typed.Generators;
internal static partial class TypedMockGenerator
{
    private static void WriteMethodProvider(FeatureWritingContext feature, IndentingStringBuilder output)
    {
        var type = feature.Type;
        output.AppendLine($$"""

            {{generatedCodeAttribute}}
            internal sealed class {{feature.TypeName}}
            {
                internal readonly {{type.MockName}} Mock;
            
                public {{feature.TypeName}}({{type.MockName}} mock)
                {
                    Mock = mock;
                }
            }
            """);
    }
}

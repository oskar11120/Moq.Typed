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
                private readonly {{type.MockName}} mock;
            
                public {{feature.TypeName}}({{type.MockName}} mock)
                {
                    this.mock = mock;
                }
            }
            """);
    }
}

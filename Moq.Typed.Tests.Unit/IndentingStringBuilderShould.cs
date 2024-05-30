using System.Text;

namespace Moq.Typed.Tests.Unit;

internal class IndentingStringBuilderShould
{
    [Test]
    public void Work()
    {
        var sb = new IndentingStringBuilder();
        sb.Append("""
            Text
                Text
            """);
        using var indentation = sb.Indent(2);
        sb.Append("""

            Text
                Text
            """);
        indentation.Dispose();
        sb.Append("""

            Text
                Text
            """);

        var expecation = """
            Text
                Text
                    Text
                        Text
            Text
                Text
            """;
        var result = sb.ToString();
        Assert.That(result == expecation);
    }
}

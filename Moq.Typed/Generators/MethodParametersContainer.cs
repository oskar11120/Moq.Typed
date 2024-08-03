namespace Moq.Typed.Generators;

internal static partial class TypedMockGenerator
{
    private static void WriteParametersContainer(MethodWritingContext method)
    {
        var output = method.Output;
        output.AppendLine();
        output.AppendLine("#nullable disable warnings");
        output.AppendLine($"public {(method.Parameters.AnyRefs ? $"ref struct" : "class")} {method.ParametersContainingType}");
        output.AppendLine("{");
        method.Parameters.ForEachWrite(
            static (parameter, @ref) => $"public{@ref} {parameter.Type} {parameter.Name};",
            false,
            1);
        output.AppendLine("}");
        output.AppendLine("#nullable enable warnings");
    }
}

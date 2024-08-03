using Microsoft.CodeAnalysis;

namespace Moq.Typed.Generators;
internal static partial class TypedMockGenerator
{
    private static void WriteMethodDelegates(MethodWritingContext method)
    {
        var output = method.Output;
        void WriteInternal(MethodWritingContext.Delegates delegates)
        {
            var symbol = delegates.OfMethod;
            output.AppendLine();
            output.AppendLine($"private delegate {delegates.ReturnType} {delegates.InternalType}(");
            method.Parameters.ForEachWrite(
                (parameter, _) => parameter.RefKind is RefKind.Out ? $"{parameter.Type} {parameter.Name}" : parameter.ToDisplayString(),
                true,
                1);
            output.AppendIgnoringIndentation(");");
        }

        var @ref = method.Parameters.AnyRefs ? "ref " : null;
        void WritePublic(MethodWritingContext.Delegates delegates)
        {
            output.AppendLine();
            output.AppendLine(
                $"public delegate {delegates.ReturnType} {delegates.PublicType}(" +
                $"{@ref}{method.ParametersContainingType} parameters);");
        }

        method.ForEachDelegate(WriteInternal);
        method.ForEachDelegate(WritePublic);
    }
}

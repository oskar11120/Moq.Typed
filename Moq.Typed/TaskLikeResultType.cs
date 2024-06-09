using Microsoft.CodeAnalysis;

namespace Moq.Typed;

internal static class TaskLikeResultType
{
    public static ITypeSymbol? Get(ITypeSymbol methodReturnType)
    {
        var methodReturnTypeArguments = methodReturnType is INamedTypeSymbol named ? named.TypeArguments : [];
        var returnsTaskLikeWithResult =
            methodReturnType.OriginalDefinition.Name is "Task" or "ValueTask" &&
            methodReturnType.ContainingNamespace.ToDisplayString() is "<global namespace>" or "System.Threading.Tasks" &&
            methodReturnTypeArguments.Length is 1;
        return returnsTaskLikeWithResult ? methodReturnTypeArguments[0] : null;
    }
}

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq.Typed.Generators;

namespace Moq.Typed;

[Generator]
public sealed class SourceGenerator : IIncrementalGenerator
{
    static bool HasMockText(SimpleNameSyntax name) => name.Identifier.ValueText is "Mock";

    static bool IsMockCtorCall(SyntaxNode node) =>
        node is ObjectCreationExpressionSyntax creationExpression &&
        creationExpression.Type is GenericNameSyntax name &&
        name.Arity is 1 &&
        HasMockText(name);
    static INamedTypeSymbol? GetMockedType(GeneratorSyntaxContext context, ObjectCreationExpressionSyntax ctorCall, CancellationToken token)
    {
        var name = (GenericNameSyntax)ctorCall.Type;
        var mockedTypeSyntax = name.TypeArgumentList.Arguments[0];
        var mockedTypeSymbol = context.SemanticModel.GetSymbolInfo(mockedTypeSyntax, token).Symbol;
        return mockedTypeSymbol as INamedTypeSymbol;
    }

    static bool IsMockGetCall(SyntaxNode node) =>
        node is InvocationExpressionSyntax invocation &&
        invocation.ArgumentList.Arguments.Count is 1 &&
        invocation.Expression is MemberAccessExpressionSyntax memberAccess &&
        memberAccess.Expression is IdentifierNameSyntax identifierName &&
        HasMockText(identifierName) &&
        memberAccess.Name.Identifier.ValueText is "Get";
    static INamedTypeSymbol? GetMockedType(GeneratorSyntaxContext context, InvocationExpressionSyntax mockGetCall)
    {
        var argument = mockGetCall.ArgumentList.Arguments[0].Expression;
        return context.SemanticModel.GetTypeInfo(argument).Type as INamedTypeSymbol;
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var mockedTypes = context
            .SyntaxProvider
            .CreateSyntaxProvider(
                (node, _) => IsMockCtorCall(node) || IsMockGetCall(node),
                (context, token) => context.Node switch
                {
                    ObjectCreationExpressionSyntax ctorCall => GetMockedType(context, ctorCall, token),
                    InvocationExpressionSyntax getCall => GetMockedType(context, getCall),
                    _ => throw new NotSupportedException()
                })
            .Where(type => type is not null);
        var deduplicated = mockedTypes
            .Collect()
            .SelectMany((types, _) => types.Distinct<INamedTypeSymbol>(SymbolEqualityComparer.Default));
        context.RegisterSourceOutput(deduplicated, TypedMockGenerator.Run);
    }
}


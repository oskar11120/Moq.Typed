using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Moq.Typed.Generators;
internal static partial class TypedMockGenerator
{
    private static void WriteMethod(MethodWritingContext method, FeatureWritingContext feature)
    {
        void WriteMethodForPamameterCombination(MethodParametersWritingContext parameters)
        {
            var symbol = method.Symbol;
            var output = method.Output;
            var methodFullName = $"{method.Symbol.Name}{method.OverloadSuffix}{method.GenericTypeParameters}";
            output.AppendLine($"""

            public {method.SetupVerifyType} {methodFullName}(
            """);

            static string Predicate(IParameterSymbol parameter) => $"Func<{parameter.Type}, bool>";
            parameters.ForEachWrite(
                feature,
                static (parameter, _, feature) => parameter.RefKind switch
                {
                    RefKind.None => $"{Predicate(parameter)} {parameter.Name}",
                    RefKind.Out when feature.HasSetupVerifyType => $"{parameter.Type} {parameter.Name}",
                    _ => string.Empty
                },
                true,
                1);

            void TryWriteSignatureForAdditionalParameter(int atIndentation)
            {
                var anyWritten = parameters.Symbols.Any(parameter =>
                    parameter.RefKind is RefKind.None ||
                    (parameter.RefKind is RefKind.Out && feature.HasSetupVerifyType));
                if (feature.AdditionalMethodPropertyMockingParameter.Text is string additionalParamText)
                {
                    if (anyWritten)
                        output.AppendIgnoringIndentation(",");
                    output.AppendLine(additionalParamText, atIndentation);
                }
            }
            TryWriteSignatureForAdditionalParameter(1);
            output.AppendIgnoringIndentation(")");

            WriteGenericTypeConstraints(symbol, atIndentation: 1, output);
            output.AppendLine("{");
            using var indentation_insideMethod = output.Indent(1);
            parameters.ForEachWrite(
                feature,
                static (parameter, method, feature) => parameter.RefKind switch
                {
                    RefKind.None => $"""
                    {parameter.Name} ??= static _ => true;
                    Expression<{Predicate(parameter)}> {parameter.Name}Expression = argument => {parameter.Name}(argument);
                    """,
                    RefKind.Out when feature.HasSetupVerifyType is false => $"{parameter.Type} {parameter.Name};",
                    _ => string.Empty
                },
                false);

            var local = feature.HasSetupVerifyType ? "var __local__ = " : null;
            output.AppendLine($"{local}mock.{feature.Name}(mock => mock.{method.Symbol.Name}{method.GenericTypeParameters}(");
            using var indentation_insideSetupDelegate = output.Indent(1);
            parameters.ForEachWrite(
                static (parameter, method) => parameter.RefKind switch
                {
                    RefKind.None => $"It.Is({parameter.Name}Expression)",
                    RefKind.Out => $"out {parameter.Name}",
                    RefKind.Ref => $"ref It.Ref<{parameter.Type}>.IsAny",
                    RefKind.RefReadOnlyParameter or RefKind.In => $"in It.Ref<{parameter.Type}>.IsAny",
                    _ => $"It.IsAny<{parameter.Type}>()"
                },
                true);
            output.AppendIgnoringIndentation(")");
            void TryWriteAdditionalParameterPassing(int atIndentation = 0)
            {
                if (feature.AdditionalMethodPropertyMockingParameter.Name is string existing)
                {
                    output.AppendIgnoringIndentation(",");
                    output.AppendLine(existing, atIndentation);
                }
            }
            TryWriteAdditionalParameterPassing();
            output.AppendIgnoringIndentation(");");
            indentation_insideSetupDelegate.Dispose();
            if (method.SetupVerifyTypeConstructorName is not null)
                output.AppendLine($"return new {method.SetupVerifyType}(__local__);");

            indentation_insideMethod.Dispose();
            output.AppendLine("}");
        }

        static IEnumerable<ImmutableArray<T>> Combinations<T>(ImmutableArray<T> source)
            => Enumerable
            .Range(0, 1 << (source.Length))
            .Select(index => source
                .Where((v, i) => (index & (1 << i)) != 0)
                .ToImmutableArray());
        var combinations = Combinations(method.Parameters.Symbols);
        foreach (var combination in combinations)
            WriteMethodForPamameterCombination(new MethodParametersWritingContext(combination, method.Output));
    }

    private static void WriteGenericTypeConstraints(IMethodSymbol method, int atIndentation, IndentingStringBuilder output)
    {
        using var indentation = output.Indent(atIndentation);
        var atLeastOnce = false;
        void Write(ITypeParameterSymbol type)
        {
            void Write(string contraintSuffix)
            {
                if (!atLeastOnce)
                {
                    output.AppendLine($$"""
                        where {{type}} : {{contraintSuffix}}
                        """);
                    atLeastOnce = true;
                }
                else
                    output.AppendIgnoringIndentation($$"""
                    , {{contraintSuffix}}
                    """);
            }

            if (type.HasConstructorConstraint)
                Write("new()");
            if (type.HasNotNullConstraint)
                Write("notnull");
            if (type.HasReferenceTypeConstraint)
                Write("class");
            if (type.HasUnmanagedTypeConstraint)
                Write("unmanaged");
            if (type.HasValueTypeConstraint)
                Write("struct");
            foreach (var constraintType in type.ConstraintTypes)
                Write(constraintType.ToDisplayString());
        }
        foreach (var typeParameter in method.TypeParameters)
            Write(typeParameter);
    }
}

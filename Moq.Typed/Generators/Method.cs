using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Moq.Typed.Generators;
internal static partial class TypedMockGenerator
{
    private static void WriteMethod(MethodWritingContext method, FeatureWritingContext feature)
    {
        var symbol = method.Symbol;
        var output = method.Output;
        var methodFullName = $"{method.Symbol.Name}{method.OverloadSuffix}{method.GenericTypeParameters}";
        output.AppendLine($"""

            public {method.SetupVerifyType} {methodFullName}(
            """);

        var methodParameters = symbol.Parameters;
        static string Predicate(IParameterSymbol parameter) => $"Func<{parameter.Type}, bool>";
        method.Parameters.ForEachWrite(
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
            var anyWritten = symbol.Parameters.Any(parameter =>
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
        method.Parameters.ForEachWrite(
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
        method.Parameters.ForEachWrite(
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

        var allNormalAndOutParameters = new MethodParametersWritingContext(
            method
                .Symbol
                .Parameters
                .Where(parameter => parameter.RefKind is RefKind.None or RefKind.Out)
                .ToImmutableArray(),
            output);
        if (allNormalAndOutParameters.Symbols.Length is 0)
            return;

        void WritePredicateValueOverloads(ImmutableArray<IParameterSymbol> normalAndOutParametersArray)
        {
            var normalAndOutParameters = new MethodParametersWritingContext(
                normalAndOutParametersArray,
                output);
            var remainingParameters = new MethodParametersWritingContext(
                allNormalAndOutParameters
                    .Symbols
                    .Except<IParameterSymbol>(normalAndOutParameters.Symbols, SymbolEqualityComparer.Default)
                    .Where(symbol => feature.HasSetupVerifyType || symbol.RefKind is RefKind.None)
                    .ToImmutableArray(),
                output);

            void WritePredicateValueOverload(int overloadNumber)
            {
                static int PositionOfRightmostSetbit(int n)
                {
                    if (n < 1)
                        return -1;

                    var position = 0;
                    var m = 1;

                    while ((n & m) == 0)
                    {
                        m <<= 1;
                        position++;
                    }
                    return position;
                }

                var firstParameterIndex = PositionOfRightmostSetbit(overloadNumber);
                if (firstParameterIndex is not -1 && normalAndOutParameters.Symbols[firstParameterIndex].RefKind is RefKind.Out)
                    return;

                var output = method.Output;
                output.AppendLine($"""

                    public {method.SetupVerifyType} {methodFullName}(
                    """);

                Predicate<int> useValueIsteadOfPredicate = (int parameterIndex)
                    => (1 << parameterIndex & overloadNumber) != 0;
                normalAndOutParameters.ForEachWrite(
                    (overloadNumber, useValueIsteadOfPredicate, feature),
                    static (parameter, _, i, args) => parameter.RefKind switch
                    {
                        RefKind.None => args.useValueIsteadOfPredicate(i) ?
                            $"{parameter.Type} {parameter.Name}" :
                            $"{Predicate(parameter)} {parameter.Name}",
                        RefKind.Out when args.feature.HasSetupVerifyType => $"{parameter.Type} {parameter.Name}",
                        _ => string.Empty
                    },
                    comaDelimit: true,
                    atIndentation: 1);
                TryWriteSignatureForAdditionalParameter(atIndentation: 1);
                output.AppendIgnoringIndentation(")");

                output.AppendLine($"=> {methodFullName}(", 1);
                allNormalAndOutParameters.ForEachWrite(
                    (useValueIsteadOfPredicate, feature, normalAndOutParameters),
                    static (parameter, _, i, args) =>
                    {
                        var matchI = args.normalAndOutParameters.Symbols.IndexOf(parameter, SymbolEqualityComparer.Default);
                        if (matchI is -1)
                            return @parameter.RefKind is RefKind.None ?
                                $"{parameter.Name}: static _ => true" :
                                args.feature.HasSetupVerifyType ?
                                    $"{parameter.Name}: default" :
                                     string.Empty;
                        return args.feature.HasSetupVerifyType || parameter.RefKind is not RefKind.Out ?
                            args.useValueIsteadOfPredicate(matchI) ?
                                $"{parameter.Name}: __local__ => Equals(__local__, {parameter.Name})" :
                                $"{parameter.Name}: {parameter.Name}" :
                            string.Empty;
                    },
                    comaDelimit: true,
                    atIndentation: 2);

                TryWriteAdditionalParameterPassing(atIndentation: 2);
                output.AppendIgnoringIndentation(");");
            }

            if (normalAndOutParametersArray.Length is 0)
                WritePredicateValueOverload(0);
            var overloadCount = Math.Pow(2, normalAndOutParameters.Symbols.Length);
            for (int i = 1; i < overloadCount; i++)
                WritePredicateValueOverload(i);
        }

        static IEnumerable<ImmutableArray<T>> Combinations<T>(ImmutableArray<T> source)
            => Enumerable
            .Range(0, 1 << (source.Length))
            .Select(index => source
                .Where((v, i) => (index & (1 << i)) != 0)
                .ToImmutableArray());
        var combinations = Combinations(allNormalAndOutParameters.Symbols);
        foreach (var combination in combinations)
            WritePredicateValueOverloads(combination);
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

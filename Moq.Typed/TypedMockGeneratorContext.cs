using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text;

namespace Moq.Typed;

internal static partial class TypedMockGenerator
{
    private const string generatedCodeAttribute = "[GeneratedCode(\"Moq.Typed\", null)]";
    private const string comma = ", ";

    private static string Comma(int i, int length) => i < length - 1 ? comma : string.Empty;

    private sealed class TypeInfo
    {
        public TypeInfo(INamedTypeSymbol symbol)
        {
            Symbol = symbol;
            ShortName = TypeNameIncludingContainingTypes(symbol);
            Name = Symbol.ToDisplayString();
            MockName = $"Mock<{Name}>";
        }

        public readonly INamedTypeSymbol Symbol;
        public readonly string ShortName;
        public readonly string Name;
        public readonly string MockName;

        private static string TypeNameIncludingContainingTypes(INamedTypeSymbol type)
            => (type.ContainingType is INamedTypeSymbol existing ? TypeNameIncludingContainingTypes(existing) + "_" : null) + type.Name;
    }

    private readonly record struct MethodParameter(string? Type, string? Name)
    {
        public readonly string? Text = Type is null ? null : $"{Type} {Name} = default({Type})!";
    }

    private sealed class FeatureWritingContext(
        string name,
        TypeInfo type,
        bool needsSetupType = true,
        string? extensionName = default,
        MethodParameter additionalMethodPropertyMockingParameter = default)
    {
        public readonly string Name = name;
        public readonly TypeInfo Type = type;
        public readonly string ExtensionName = extensionName ?? name;
        public readonly MethodParameter AdditionalMethodPropertyMockingParameter = additionalMethodPropertyMockingParameter;
        public readonly bool HasSetupVerifyType = needsSetupType;
        public readonly string TypeName = $"TypedMock{name}For_{type.ShortName}";
    }

    private readonly struct PropertyWritingContext(IPropertySymbol symbol, string overloadSuffix, FeatureWritingContext feature)
    {
        public readonly IPropertySymbol Symbol = symbol;
        public readonly string OverloadSuffix = overloadSuffix;
        public readonly FeatureWritingContext Feature = feature;
    }

    private readonly struct MethodParametersWritingContext
    {
        public MethodParametersWritingContext(ImmutableArray<IParameterSymbol> symbols, IndentingStringBuilder output)
        {
            Symbols = symbols;
            Output = output;

            AnyRefs = Symbols.Any(IsRef);
        }

        public readonly ImmutableArray<IParameterSymbol> Symbols;
        public readonly IndentingStringBuilder Output;

        public readonly bool AnyRefs;

        private static bool IsRef(IParameterSymbol parameter) => parameter.RefKind is RefKind.Ref;

        public delegate string GetText<TArgs>(IParameterSymbol symbol, string? @refPrefix, int index, TArgs args);
        public void ForEachWrite<TArgs>(TArgs arguments, GetText<TArgs> getText, bool comaDelimit, int atIndentation = 0)
        {
            using var indentation = Output.Indent(atIndentation);
            for (int i = 0; i < Symbols.Length; i++)
            {
                var parameter = Symbols[i];
                var @ref = IsRef(parameter) ? " ref" : null;
                var text = getText(parameter, @ref, i, arguments);
                Output.AppendLine(text);
                if (comaDelimit && text.Length is not 0)
                    Output.AppendIgnoringIndentation(Comma(i, Symbols.Length));
            }
        }

        public delegate string GetText1<TArgs>(IParameterSymbol symbol, string? @refPrefix, TArgs args);
        public void ForEachWrite<TArgs>(TArgs arguments, GetText1<TArgs> getText, bool comaDelimit, int atIndentation = 0)
            => ForEachWrite((arguments, getText), static (symbol, @ref, _, arguments) => arguments.getText(symbol, @ref, arguments.arguments), comaDelimit, atIndentation);

        public delegate string GetText(IParameterSymbol symbol, string? @refPrefix);
        public void ForEachWrite(GetText getText, bool comaDelimit, int atIndentation = 0)
            => ForEachWrite(getText, static (symbol, @ref, _, getText) => getText(symbol, @ref), comaDelimit, atIndentation);
    }

    private readonly struct MethodWritingContext
    {
        public readonly record struct Delegates(
            string PublicType, 
            bool Return, 
            string ReturnType, 
            IMethodSymbol OfMethod)
        {
            public readonly string InternalType = "Internal" + PublicType;
        }

        public readonly IMethodSymbol Symbol;
        public readonly string ContainingType;
        public readonly string OverloadSuffix;
        public readonly IndentingStringBuilder Output;
        public readonly FeatureWritingContext Feature;

        public MethodWritingContext(IMethodSymbol symbol, string overloadSuffix, FeatureWritingContext feature, IndentingStringBuilder output)
        {
            Symbol = symbol;
            ContainingType = feature.Type.Name;
            Output = output;
            OverloadSuffix = overloadSuffix;
            Feature = feature;

            Parameters = new(symbol.Parameters, output);
            GenericTypeParameters = GetGenericTypeParameters();
            ParametersContainingType = Symbol.Name + "Parameters" + OverloadSuffix + GenericTypeParameters;

            string ApplyConflctResolutionSuffix(string newTypeParameter, int i = 0)
                => symbol.TypeParameters.Any(existing => existing.Name == newTypeParameter) ?
                ApplyConflctResolutionSuffix(newTypeParameter + i.ToString(), i + 1) : newTypeParameter;
            var exceptionTypeParameter = ApplyConflctResolutionSuffix("TException");
            ExceptionFunctionDelegates = GetDelegates("ExceptionFunction", GetGenericTypeParameters(exceptionTypeParameter), true, exceptionTypeParameter);
            CallbackDelegates = GetDelegates("Callback", GenericTypeParameters, false, "void");
            ValueFunctionDelegates = symbol.ReturnsVoid || !feature.HasSetupVerifyType ?
                default : GetDelegates("ValueFunction", GenericTypeParameters, true, symbol.ReturnType.ToDisplayString());

            SetupVerifyType = "void";
            if (feature.HasSetupVerifyType)
            {
                SetupVerifyTypeConstructorName = Symbol.Name + Feature.Name + OverloadSuffix;
                SetupVerifyType = SetupVerifyTypeConstructorName + GenericTypeParameters;
            }
        }

        public readonly string GenericTypeParameters;
        public readonly string ParametersContainingType;
        public readonly string? SetupVerifyTypeConstructorName;
        public readonly string SetupVerifyType;
        public readonly MethodParametersWritingContext Parameters;
        public readonly Delegates CallbackDelegates;
        public readonly Delegates ExceptionFunctionDelegates;
        public readonly Delegates ValueFunctionDelegates;

        public void ForEachDelegate(Action<Delegates> action)
        {
            action(CallbackDelegates);
            if (ValueFunctionDelegates != default)
                action(ValueFunctionDelegates);
            action(ExceptionFunctionDelegates);
        }

        private Delegates GetDelegates(string kind, string genericTypeParameters, bool @return, string returnType) =>
            new(Symbol.MetadataName + kind + OverloadSuffix + genericTypeParameters, @return, returnType, Symbol);

        private string GetGenericTypeParameters(string? withAdditionalParameter = null)
        {
            var typeParameters = Symbol.TypeParameters;
            if (typeParameters.Length is 0)
                return withAdditionalParameter is null ? string.Empty : $"<{withAdditionalParameter}>";

            var output = new StringBuilder();
            output.Append("<");
            for (int i = 0; i < typeParameters.Length; i++)
            {
                output.Append($"{typeParameters[i]}");
                output.Append(Comma(i, typeParameters.Length));
            }

            if (withAdditionalParameter is not null)
            {
                output.Append(comma);
                output.Append(withAdditionalParameter);
            }

            output.Append(">");
            return output.ToString();
        }
    }

    private readonly struct OverloadCounter
    {
        private readonly Dictionary<string, int> state = [];

        public OverloadCounter()
        {
        }

        public int Add(string methodOrPropertyName)
        {
            var name = methodOrPropertyName;
            var current = state.TryGetValue(name, out var value) ? value : -1;
            var @new = current + 1;
            state[name] = @new;
            return @new;
        }
    }
}

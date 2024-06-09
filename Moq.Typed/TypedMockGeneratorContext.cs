using Microsoft.CodeAnalysis;
using System.Text;

namespace Moq.Typed;

internal static partial class TypedMockGenerator
{
    private const string generatedCodeAttribute = "[GeneratedCode(\"Moq.Typed\", null)]";
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

    private readonly struct MethodWritingContext
    {
        public readonly record struct Delegates(string PublicType, bool Return, IMethodSymbol OfMethod)
        {
            public readonly string InternalType = "Internal" + PublicType;
            public readonly string ReturnType = Return ? OfMethod.ReturnType.ToDisplayString() : "void";
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

            GenericTypeParameters = GetGenericTypeParameters();
            Name = Symbol.Name + GenericTypeParameters;
            ParametersContainingType = Symbol.Name + "Parameters" + OverloadSuffix + GenericTypeParameters;
            AnyRefs = symbol.Parameters.Any(IsRef);
            CallbackDelegates = GetDelegates("Callback", GenericTypeParameters, false);
            ValueFunctionDelegates = symbol.ReturnsVoid || !feature.HasSetupVerifyType ? default : GetDelegates("ValueFunction", GenericTypeParameters, true);

            SetupVerifyType = "void";
            if (feature.HasSetupVerifyType)
            {
                SetupVerifyTypeConstructorName = Symbol.Name + Feature.Name + OverloadSuffix;
                SetupVerifyType = SetupVerifyTypeConstructorName + GenericTypeParameters;
            }
        }

        public readonly string Name;
        public readonly string GenericTypeParameters;
        public readonly string ParametersContainingType;
        public readonly string? SetupVerifyTypeConstructorName;
        public readonly string SetupVerifyType;
        public readonly Delegates CallbackDelegates;
        public readonly Delegates ValueFunctionDelegates;
        public readonly bool AnyRefs;

        public void ForEachDelegate(Action<Delegates> action)
        {
            action(CallbackDelegates);
            if (ValueFunctionDelegates != default)
                action(ValueFunctionDelegates);
        }

        public delegate string GetText<TArgs>(IParameterSymbol symbol, string? @refPrefix, TArgs args);
        public void ForEachParameterWrite<TArgs>(TArgs arguments, GetText<TArgs> getText, bool comaDelimit, int atIndentation = 0)
        {
            var parameters = Symbol.Parameters;
            using var indentation = Output.Indent(atIndentation);
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var @ref = IsRef(parameter) ? " ref" : null;
                var text = getText(parameter, @ref, arguments);
                Output.AppendLine(text);
                if (comaDelimit && text.Length is not 0)
                    Output.AppendIgnoringIndentation(Comma(i, parameters.Length));
            }
        }

        public delegate string GetText(IParameterSymbol symbol, string? @refPrefix);
        public void ForEachParameterWrite(GetText getText, bool comaDelimit, int atIndentation = 0)
            => ForEachParameterWrite(getText, static (symbol, @ref, getText) => getText(symbol, @ref), comaDelimit, atIndentation);

        private Delegates GetDelegates(string kind, string genericTypeParameters, bool @return) =>
            new(Symbol.MetadataName + kind + OverloadSuffix + genericTypeParameters, @return, Symbol);

        private string GetGenericTypeParameters()
        {
            var typeParameters = Symbol.TypeParameters;
            if (typeParameters.Length is 0)
                return string.Empty;

            var output = new StringBuilder();
            output.Append("<");
            for (int i = 0; i < typeParameters.Length; i++)
            {
                output.Append($"{typeParameters[i]}{Comma(i, typeParameters.Length)}");
            }

            output.Append(">");
            return output.ToString();
        }

        private const string comma = ", ";
        private static bool IsRef(IParameterSymbol parameter) => parameter.RefKind is RefKind.Ref;
        private static string Comma(int i, int length) => i < length - 1 ? comma : string.Empty;
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

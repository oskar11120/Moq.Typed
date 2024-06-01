using Microsoft.CodeAnalysis;
using System.Text;

namespace Moq.Typed;

internal static partial class TypedMockGenerator
{
    private readonly struct MethodWritingContext
    {
        public readonly record struct Delegates(string PublicType, bool Return, IMethodSymbol OfMethod)
        {
            public readonly string InternalType = "Internal" + PublicType;
            public readonly string ReturnType = Return ? OfMethod.ReturnType.ToDisplayString() : "void";
        }

        public readonly IMethodSymbol Symbol;
        public readonly string ContainingType;
        public readonly IndentingStringBuilder Output;

        public MethodWritingContext(IMethodSymbol symbol, string overloadSuffix, string containingTypeName, IndentingStringBuilder output)
        {
            Symbol = symbol;
            ContainingType = containingTypeName;
            Output = output;

            this.overloadSuffix = overloadSuffix;
            genericTypeParameters = GetGenericTypeParameters();

            Name = Symbol.Name + genericTypeParameters;
            ParametersContainingType = Symbol.Name + "Parameters" + this.overloadSuffix + genericTypeParameters;
            SetupTypeConstructorName = Symbol.Name + "Setup" + this.overloadSuffix;
            SetupType = SetupTypeConstructorName + genericTypeParameters;
            AnyRefs = symbol.Parameters.Any(IsRef);
            CallbackDelegates = GetDelegates("Callback", genericTypeParameters, false);
            ValueFunctionDelegates = symbol.ReturnsVoid ? default : GetDelegates("ValueFunction", genericTypeParameters, true);
        }

        private readonly string overloadSuffix;
        private readonly string genericTypeParameters;

        public readonly string Name;
        public readonly string ParametersContainingType;
        public readonly string SetupTypeConstructorName;
        public readonly string SetupType;
        public readonly Delegates CallbackDelegates;
        public readonly Delegates ValueFunctionDelegates;
        public readonly bool AnyRefs;

        public void ForEachDelegate(Action<Delegates> action)
        {
            action(CallbackDelegates);
            if (!Symbol.ReturnsVoid)
                action(ValueFunctionDelegates);
        }

        public delegate string GetText(IParameterSymbol symbol, string? @refPrefix);
        public void ForEachParameterWrite(GetText getText, bool comaDelimit, int atIndentation = 0)
        {
            var parameters = Symbol.Parameters;
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var @ref = IsRef(parameter) ? " ref" : null;
                var text = getText(parameter, @ref);
                Output.AppendLine(text, atIndentation);
                if (comaDelimit)
                    Output.AppendIgnoringIndentation(Comma(i, parameters.Length));
            }
        }

        private Delegates GetDelegates(string kind, string genericTypeParameters, bool @return) =>
            new(Symbol.MetadataName + kind + overloadSuffix + genericTypeParameters, @return, Symbol);

        private string GetGenericTypeParameters()
        {
            var typeParameters = Symbol.TypeParameters;
            if (typeParameters.Length is 0)
                return string.Empty;

            var output = new StringBuilder();
            output.Append("<");
            for (int i = 0; i < typeParameters.Length; i++)
                output.Append($"{typeParameters[i]}{Comma(i, typeParameters.Length)}");
            output.Append(">");
            return output.ToString();
        }

        private static bool IsRef(IParameterSymbol parameter) => parameter.RefKind is RefKind.Ref;
        private static string Comma(int i, int length) => i < length - 1 ? ", " : string.Empty;
    }
}

using Microsoft.CodeAnalysis;

namespace Moq.Typed.Generators;
internal static partial class TypedMockGenerator
{
    private static void WriteSetupType(MethodWritingContext method)
    {
        var symbol = method.Symbol;
        var output = method.Output;
        var setupMoqInterface = symbol.ReturnsVoid ?
            $"ISetup<{method.ContainingType}>" :
            $"ISetup<{method.ContainingType}, {symbol.ReturnType}>";

        output.AppendLine($$"""

            public class {{method.SetupVerifyType}}
            {
            """);

        using var indentation_insideClass = output.Indent();
        output.AppendLine($$"""
            private readonly {{setupMoqInterface}} setup;
            
            public {{method.SetupVerifyTypeConstructorName}}({{setupMoqInterface}} setup)
            {
                this.setup = setup;
            }
            """);

        var parameterTypesText = symbol.Parameters.Length is 0 ?
            string.Empty :
            $"<{string.Join(", ", symbol.Parameters.Select(parameter => parameter.Type.ToDisplayString()))}>";
        var paramterTexts = symbol.Parameters.Select(parameter => parameter.RefKind is RefKind.Out ?
            $"{parameter.Type} {parameter.Name}" : $"{parameter.ToDisplayString()}");

        void WriteMethod(
            MethodWritingContext.Delegates delegates,
            string name,
            string parameterName,
            string? typeArgument = null,
            string? genericConstrant = null)
        {
            typeArgument = typeArgument is not null ? $"<{typeArgument}>" : typeArgument;
            output.AppendLine();
            output.AppendLine($"public {method.SetupVerifyType} {name}{typeArgument}({delegates.PublicType} {parameterName}){genericConstrant}");
            output.AppendLine($$"""
            {
                setup.{{name}}(new {{delegates.InternalType}}(
                    ({{string.Join(", ", paramterTexts)}}) => 
                    {
                        var __parameters__ = new {{method.ParametersContainingType}}
                        {
            """);

            method.Parameters.ForEachWrite(
                static (parameter, @ref) => $"{parameter.Name} ={@ref} {parameter.Name}",
                true,
                4);

            var @ref = method.Parameters.AnyRefs ? "ref " : null;
            output.AppendLine($$"""
                        };
                        {{(delegates.Return ? "return " : null)}}{{parameterName}}({{@ref}}__parameters__);
                    }));
                return this;
            }
            """);
        }

        WriteMethod(method.CallbackDelegates, "Callback", "callback");
        if (method.ValueFunctionDelegates != default)
            WriteMethod(method.ValueFunctionDelegates, "Returns", "valueFunction");

        var methodTaskLikeResultType = TaskLikeResultType.Get(method.Symbol.ReturnType);

        void WriteReturnsConvenienceMethods()
        {
            if (!method.Symbol.ReturnsVoid)
                output.AppendLine($$"""

                public {{method.SetupVerifyType}} Returns({{symbol.ReturnType}} value)
                    => Returns(_ => value);
                """);

            if (methodTaskLikeResultType is not null)
            {
                output.AppendLine($$"""

                public {{method.SetupVerifyType}} ReturnsAsync(Func<{{method.ParametersContainingType}}, {{methodTaskLikeResultType}}> valueFunction)
                    => Returns(async parameters => 
                    {
                        await Task.CompletedTask;
                        return valueFunction(parameters);
                    });
                """);

                output.AppendLine($$"""

                public {{method.SetupVerifyType}} ReturnsAsync({{methodTaskLikeResultType}} value)
                    => Returns(async _ => 
                    {
                        await Task.CompletedTask;
                        return value;
                    });
                """);
            }
        }

        if (!method.Parameters.AnyRefs)
            WriteReturnsConvenienceMethods();


        WriteMethod(
            method.ExceptionFunctionDelegates,
            "Throws",
            "exceptionFunction",
            method.ExceptionFunctionDelegates.ReturnType,
            $" where {method.ExceptionFunctionDelegates.ReturnType} : Exception");

        void WriteThrowsConvenienceMethods()
        {
            output.AppendLine($$"""

                public {{method.SetupVerifyType}} Throws(Exception exception)
                {
                    setup.Throws(exception);
                    return this;
                }
                """);
            output.AppendLine($$"""

                public {{method.SetupVerifyType}} Throws<TException>() where TException : Exception, new()
                {
                    setup.Throws<TException>();
                    return this;
                }
                """);
            output.AppendLine($$"""

                public {{method.SetupVerifyType}} Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
                {
                    setup.Throws<TException>(exceptionFunction);
                    return this;
                }
                """);
        }

        WriteThrowsConvenienceMethods();

        indentation_insideClass.Dispose();
        output.AppendLine("}");
    }

}

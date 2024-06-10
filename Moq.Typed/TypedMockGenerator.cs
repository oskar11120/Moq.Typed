using Microsoft.CodeAnalysis;
using System.Globalization;

namespace Moq.Typed;

internal static partial class TypedMockGenerator
{
    private static void WriteParametersContainerType(MethodWritingContext method)
    {
        var output = method.Output;
        output.AppendLine();
        output.AppendLine("#nullable disable warnings");
        output.AppendLine($"public {(method.AnyRefs ? $"ref struct" : "class")} {method.ParametersContainingType}");
        output.AppendLine("{");
        method.ForEachParameterWrite(
            static (parameter, @ref) => $"public{@ref} {parameter.Type} {parameter.Name};",
            false,
            1);
        output.AppendLine("}");
        output.AppendLine("#nullable enable warnings");
    }

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

            method.ForEachParameterWrite(
                static (parameter, @ref) => $"{parameter.Name} ={@ref} {parameter.Name}",
                true,
                4);

            var @ref = method.AnyRefs ? "ref " : null;
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

        if (!method.AnyRefs)
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

    private static void WriteDelegates(MethodWritingContext method)
    {
        var output = method.Output;
        void WriteInternal(MethodWritingContext.Delegates delegates)
        {
            var symbol = delegates.OfMethod;
            output.AppendLine();
            output.AppendLine($"private delegate {delegates.ReturnType} {delegates.InternalType}(");
            method.ForEachParameterWrite(
                (parameter, _) => parameter.RefKind is RefKind.Out ? $"{parameter.Type} {parameter.Name}" : parameter.ToDisplayString(),
                true,
                1);
            output.AppendIgnoringIndentation(");");
        }

        var @ref = method.AnyRefs ? "ref " : null;
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

    private static void WriteMethodOverloads(MethodWritingContext method, FeatureWritingContext feature)
    {
        var normalAndOutParameters = method
            .Symbol
            .Parameters
            .Where(parameter => parameter.RefKind is RefKind.None or RefKind.Out)
            .ToArray();
        for (int i = 0; i < normalAndOutParameters.Length; i++)
        {

        }
    }

    private static void WriteMethod(MethodWritingContext method, FeatureWritingContext feature)
    {
        var symbol = method.Symbol;
        var output = method.Output;
        output.AppendLine($"""

            public {method.SetupVerifyType} {method.Symbol.Name}{method.OverloadSuffix}{method.GenericTypeParameters}
            """);

        var methodParameters = symbol.Parameters;
        output.AppendIgnoringIndentation("(");
        static string Predicate(IParameterSymbol parameter) => $"Func<{parameter.Type}, bool>";
        method.ForEachParameterWrite(
            feature,
            static (parameter, _, feature) => parameter.RefKind switch
            {
                RefKind.None => $"{Predicate(parameter)}? {parameter.Name} = null",
                RefKind.Out when feature.HasSetupVerifyType => $"{parameter.Type} {parameter.Name} = default({parameter.Type})!",
                _ => string.Empty
            },
            true,
            1);
        var anyWritten = symbol.Parameters.Any(parameter =>
            parameter.RefKind is RefKind.None ||
            (parameter.RefKind is RefKind.Out && feature.HasSetupVerifyType));
        if (feature.AdditionalMethodPropertyMockingParameter.Text is string additionalParamText)
        {
            if (anyWritten)
                output.AppendIgnoringIndentation(",");
            output.AppendLine(additionalParamText, 1);
        }
        output.AppendIgnoringIndentation(")");

        WriteGenericTypeConstraints(symbol, atIndentation: 1, output);
        output.AppendLine("{");
        using var indentation_insideMethod = output.Indent(1);
        method.ForEachParameterWrite(
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
        method.ForEachParameterWrite(
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
        if (feature.AdditionalMethodPropertyMockingParameter.Name is string existing)
        {
            output.AppendIgnoringIndentation(",");
            output.AppendLine(existing);
        }
        output.AppendIgnoringIndentation(");");
        indentation_insideSetupDelegate.Dispose();
        if (method.SetupVerifyTypeConstructorName is not null)
            output.AppendLine($"return new {method.SetupVerifyType}(__local__);");

        indentation_insideMethod.Dispose();
        output.AppendLine("}");
    }

    private static void WriteProperty(PropertyWritingContext property, IndentingStringBuilder output)
    {
        var symbol = property.Symbol;
        var feature = property.Feature;
        var passParameter = feature.AdditionalMethodPropertyMockingParameter;
        var parameter = passParameter.Name is null ? null : $", {passParameter.Name}";
        var returnType = feature.HasSetupVerifyType ? $"I{feature.Name}<{feature.Type.Name}, {symbol.Type}>" : "void";
        var @return = feature.HasSetupVerifyType ? "return " : null;
        output.AppendLine($$"""

            public {{returnType}} {{symbol.Name}}{{property.OverloadSuffix}}({{passParameter.Text}})
            {
                {{@return}}mock.{{feature.Name}}(mock => mock.{{symbol.Name}}{{parameter}});
            }
            """);
    }

    private static void WriteExtension(FeatureWritingContext feature, IndentingStringBuilder output)
    {
        var type = feature.Type;
        output.AppendLine($$"""

            {{generatedCodeAttribute}}
            internal static class TypedMock{{feature.Name}}ExtensionFor_{{type.ShortName}}
            {
                public static {{feature.TypeName}} {{feature.ExtensionName}}(this {{type.MockName}} mock)
                    => new {{feature.TypeName}}(mock);
            }
            """);
    }

    private static void WriteFeature(FeatureWritingContext feature, IndentingStringBuilder output)
    {
        WriteExtension(feature, output);
        var type = feature.Type;
        output.AppendLine($$"""

            {{generatedCodeAttribute}}
            internal sealed class {{feature.TypeName}}
            {
                private readonly {{type.MockName}} mock;
            
                public {{feature.TypeName}}({{type.MockName}} mock)
                {
                    this.mock = mock;
                }
            """);

        var mockableMembers = type
            .Symbol
            .GetMembers()
            .Where(member =>
                (member.IsAbstract || member.IsVirtual)
                && member.DeclaredAccessibility is not Accessibility.Private
                && member is not IMethodSymbol { MethodKind: MethodKind.PropertyGet or MethodKind.PropertySet });
        var overloadCounts = new OverloadCounter();

        using var indentation = output.Indent();
        foreach (var member in mockableMembers)
            if (member is IMethodSymbol methodSymbol)
            {
                var overloadNumber = overloadCounts.Add(methodSymbol.Name);
                var method = new MethodWritingContext(methodSymbol, OverloadSuffix(overloadNumber), feature, output);
                if (feature.HasSetupVerifyType)
                {
                    WriteParametersContainerType(method);
                    WriteDelegates(method);
                    WriteSetupType(method);
                }
                WriteMethod(method, feature);
            }
            else if (member is IPropertySymbol propertySymbol)
            {
                var overloadNumber = overloadCounts.Add(propertySymbol.Name);
                var overloadSuffix = OverloadSuffix(overloadNumber);
                var property = new PropertyWritingContext(propertySymbol, overloadSuffix, feature);
                WriteProperty(property, output);
            }
            else
                throw new NotSupportedException($"Unsupported member {member}.");
        indentation.Dispose();
        output.AppendLine("}");
    }

    private static string OverloadSuffix(int overloadNumber)
        => overloadNumber is 0 ? string.Empty : overloadNumber.ToString(CultureInfo.InvariantCulture);

    public static void Run(SourceProductionContext context, INamedTypeSymbol forType)
    {
        using var output = new IndentingStringBuilder();
        var @namespace = forType.ContainingNamespace.ToDisplayString();
        output.Append($$"""
            using Moq;
            using Moq.Language.Flow;
            using System;
            using System.CodeDom.Compiler;
            using System.Linq.Expressions;
            using System.Threading.Tasks;

            namespace {{@namespace}}
            {
            """
            );

        using var namespaceIndentation = output.Indent();
        var type = new TypeInfo(forType);

        var setup = new FeatureWritingContext(
            "Setup",
            type);
        WriteFeature(setup, output);

        var verify = new FeatureWritingContext(
            "Verify",
            type,
            needsSetupType: false,
            extensionName: "Verifyy",
            additionalMethodPropertyMockingParameter: new("Times", "times"));
        WriteFeature(verify, output);

        namespaceIndentation.Dispose();
        output.AppendLine("""
            }

            """);
        context.AddSource($"{forType}".Replace('<', '_').Replace('>', '_'), output.ToString());
    }
}

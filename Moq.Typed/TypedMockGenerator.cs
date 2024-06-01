using Microsoft.CodeAnalysis;
using System.Globalization;

namespace Moq.Typed;

internal static partial class TypedMockGenerator
{
    private static void WriteParametersContainerType(MethodWritingContext method)
    {
        var output = method.Output;
        output.AppendLine();
        output.AppendLine($"public {(method.AnyRefs ? $"ref struct" : "class")} {method.ParametersContainingType}");
        output.AppendLine("{");
        method.ForEachParameterWrite(
            static (parameter, @ref) => $"public{@ref} {parameter.Type} {parameter.Name};",
            false,
            1);
        output.AppendLine("}");
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

        void WriteMethod(MethodWritingContext.Delegates delegates)
        {
            var (name, parameterName) = delegates.Return ? ("Returns", "valueFunction") : ("Callback", "callback");
            output.AppendLine();
            output.AppendLine($"public {method.SetupVerifyType} {name}({delegates.PublicType} {parameterName})");
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

        method.ForEachDelegate(WriteMethod);

        if (!method.Symbol.ReturnsVoid && !method.AnyRefs)
        {
            output.AppendLine($$"""

            public {{method.SetupVerifyType}} Returns({{symbol.ReturnType}} value)
                => Returns(_ => value);
            """);
        }

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

    private static void WriteMethod(MethodWritingContext method, FeatureWritingContext feature)
    {
        var symbol = method.Symbol;
        var output = method.Output;
        output.AppendLine($"""

            public {method.SetupVerifyType} {method.Name}
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
            false);;

        var local = feature.HasSetupVerifyType ? "var __local__ = " : null;
        output.AppendLine($"{local}mock.{feature.Name}(mock => mock.{method.Name}(");
        using var indentation_insideSetupDelegate = output.Indent(1);
        method.ForEachParameterWrite(
            static (parameter, method) => parameter.RefKind switch
            {
                RefKind.None => $"It.Is({parameter.Name}Expression)",
                RefKind.Out=> $"out {parameter.Name}",
                RefKind.Ref or RefKind.RefReadOnlyParameter or RefKind.In => $"ref It.Ref<{parameter.Type}>.IsAny",
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

            {{GeneratedCodeAttribute}}
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

            {{GeneratedCodeAttribute}}
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
                WriteParametersContainerType(method);
                if (feature.HasSetupVerifyType)
                {
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

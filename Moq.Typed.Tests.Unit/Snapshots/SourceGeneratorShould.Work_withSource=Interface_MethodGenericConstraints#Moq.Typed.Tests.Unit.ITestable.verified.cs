//HintName: Moq.Typed.Tests.Unit.ITestable.cs
using Moq;
using Moq.Language.Flow;
using System;
using System.CodeDom.Compiler;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Moq.Typed.Tests.Unit
{

    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockSetupExtensionFor_ITestable
    {
        public static TypedMockSetupFor_ITestable Setup(this Mock<Moq.Typed.Tests.Unit.ITestable> mock)
            => new TypedMockSetupFor_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockSetupFor_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable> mock;

        public TypedMockSetupFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            this.mock = mock;
        }

        #nullable disable warnings
        public class MethodParameters<TInput>
        {
            public TInput Parameter;
        }
        #nullable enable warnings

        private delegate void InternalMethodCallback<TInput>(
            TInput Parameter);

        private delegate int InternalMethodValueFunction<TInput>(
            TInput Parameter);

        private delegate TException InternalMethodExceptionFunction<TInput, TException>(
            TInput Parameter);

        public delegate void MethodCallback<TInput>(MethodParameters<TInput> parameters);

        public delegate int MethodValueFunction<TInput>(MethodParameters<TInput> parameters);

        public delegate TException MethodExceptionFunction<TInput, TException>(MethodParameters<TInput> parameters);

        public class MethodSetup<TInput>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public MethodSetup<TInput> Callback(MethodCallback<TInput> callback)
            {
                setup.Callback(new InternalMethodCallback<TInput>(
                    (TInput Parameter) => 
                    {
                        var __parameters__ = new MethodParameters<TInput>
                        {
                            Parameter = Parameter
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public MethodSetup<TInput> Returns(MethodValueFunction<TInput> valueFunction)
            {
                setup.Returns(new InternalMethodValueFunction<TInput>(
                    (TInput Parameter) => 
                    {
                        var __parameters__ = new MethodParameters<TInput>
                        {
                            Parameter = Parameter
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup<TInput> Returns(int value)
                => Returns(_ => value);

            public MethodSetup<TInput> Throws<TException>(MethodExceptionFunction<TInput, TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalMethodExceptionFunction<TInput, TException>(
                    (TInput Parameter) => 
                    {
                        var __parameters__ = new MethodParameters<TInput>
                        {
                            Parameter = Parameter
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup<TInput> Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public MethodSetup<TInput> Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public MethodSetup<TInput> Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public MethodSetup<TInput> Method<TInput>(
            Func<TInput, bool>? Parameter = null)
            where TInput : struct, System.Enum
        {
            Parameter ??= static _ => true;
            Expression<Func<TInput, bool>> ParameterExpression = argument => Parameter(argument);
            var __local__ = mock.Setup(mock => mock.Method<TInput>(
                It.Is(ParameterExpression)));
            return new MethodSetup<TInput>(__local__);
        }
    }

    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockVerifyExtensionFor_ITestable
    {
        public static TypedMockVerifyFor_ITestable Verifyy(this Mock<Moq.Typed.Tests.Unit.ITestable> mock)
            => new TypedMockVerifyFor_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockVerifyFor_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable> mock;

        public TypedMockVerifyFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            this.mock = mock;
        }

        public void Method<TInput>(
            Func<TInput, bool>? Parameter = null,
            Times times = default(Times)!)
            where TInput : struct, System.Enum
        {
            Parameter ??= static _ => true;
            Expression<Func<TInput, bool>> ParameterExpression = argument => Parameter(argument);
            mock.Verify(mock => mock.Method<TInput>(
                It.Is(ParameterExpression)),
                times);
        }
    }
}

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
        public class MethodParameters
        {
            public int parameter;
        }
        #nullable enable warnings

        private delegate void InternalMethodCallback(
            int parameter);

        private delegate Task InternalMethodValueFunction(
            int parameter);

        private delegate TException InternalMethodExceptionFunction<TException>(
            int parameter);

        public delegate void MethodCallback(MethodParameters parameters);

        public delegate Task MethodValueFunction(MethodParameters parameters);

        public delegate TException MethodExceptionFunction<TException>(MethodParameters parameters);

        public class MethodSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, Task> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, Task> setup)
            {
                this.setup = setup;
            }

            public MethodSetup Callback(MethodCallback callback)
            {
                setup.Callback(new InternalMethodCallback(
                    (int parameter) => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                            parameter = parameter
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public MethodSetup Returns(MethodValueFunction valueFunction)
            {
                setup.Returns(new InternalMethodValueFunction(
                    (int parameter) => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                            parameter = parameter
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup Returns(Task value)
                => Returns(_ => value);

            public MethodSetup Throws<TException>(MethodExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalMethodExceptionFunction<TException>(
                    (int parameter) => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                            parameter = parameter
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public MethodSetup Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public MethodSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public MethodSetup Method(
            Func<int, bool>? parameter = null)
        {
            parameter ??= static _ => true;
            Expression<Func<int, bool>> parameterExpression = argument => parameter(argument);
            var __local__ = mock.Setup(mock => mock.Method(
                It.Is(parameterExpression)));
            return new MethodSetup(__local__);
        }

        #nullable disable warnings
        public class MethodParameters1
        {
        }
        #nullable enable warnings

        private delegate void InternalMethodCallback1();

        private delegate ValueTask<int> InternalMethodValueFunction1();

        private delegate TException InternalMethodExceptionFunction1<TException>();

        public delegate void MethodCallback1(MethodParameters1 parameters);

        public delegate ValueTask<int> MethodValueFunction1(MethodParameters1 parameters);

        public delegate TException MethodExceptionFunction1<TException>(MethodParameters1 parameters);

        public class MethodSetup1
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, ValueTask<int>> setup;

            public MethodSetup1(ISetup<Moq.Typed.Tests.Unit.ITestable, ValueTask<int>> setup)
            {
                this.setup = setup;
            }

            public MethodSetup1 Callback(MethodCallback1 callback)
            {
                setup.Callback(new InternalMethodCallback1(
                    () => 
                    {
                        var __parameters__ = new MethodParameters1
                        {
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public MethodSetup1 Returns(MethodValueFunction1 valueFunction)
            {
                setup.Returns(new InternalMethodValueFunction1(
                    () => 
                    {
                        var __parameters__ = new MethodParameters1
                        {
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup1 Returns(ValueTask<int> value)
                => Returns(_ => value);

            public MethodSetup1 ReturnsAsync(Func<MethodParameters1, int> valueFunction)
                => Returns(async parameters => 
                {
                    await Task.CompletedTask;
                    return valueFunction(parameters);
                });

            public MethodSetup1 ReturnsAsync(int value)
                => Returns(async _ => 
                {
                    await Task.CompletedTask;
                    return value;
                });

            public MethodSetup1 Throws<TException>(MethodExceptionFunction1<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalMethodExceptionFunction1<TException>(
                    () => 
                    {
                        var __parameters__ = new MethodParameters1
                        {
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup1 Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public MethodSetup1 Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public MethodSetup1 Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public MethodSetup1 Method1()
        {
            var __local__ = mock.Setup(mock => mock.Method());
            return new MethodSetup1(__local__);
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

        public void Method(
            Func<int, bool>? parameter = null,
            Times times = default(Times)!)
        {
            parameter ??= static _ => true;
            Expression<Func<int, bool>> parameterExpression = argument => parameter(argument);
            mock.Verify(mock => mock.Method(
                It.Is(parameterExpression)),
                times);
        }

        public void Method1(
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.Method(),
                times);
        }
    }
}

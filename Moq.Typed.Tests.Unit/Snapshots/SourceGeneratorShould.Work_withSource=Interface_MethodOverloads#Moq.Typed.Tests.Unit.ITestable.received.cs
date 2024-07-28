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
        }
        #nullable enable warnings

        private delegate void InternalMethodCallback();

        private delegate int InternalMethodValueFunction();

        private delegate TException InternalMethodExceptionFunction<TException>();

        public delegate void MethodCallback(MethodParameters parameters);

        public delegate int MethodValueFunction(MethodParameters parameters);

        public delegate TException MethodExceptionFunction<TException>(MethodParameters parameters);

        public class MethodSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public MethodSetup Callback(MethodCallback callback)
            {
                setup.Callback(new InternalMethodCallback(
                    () => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public MethodSetup Returns(MethodValueFunction valueFunction)
            {
                setup.Returns(new InternalMethodValueFunction(
                    () => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup Returns(int value)
                => Returns(_ => value);

            public MethodSetup Throws<TException>(MethodExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalMethodExceptionFunction<TException>(
                    () => 
                    {
                        var __parameters__ = new MethodParameters
                        {
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

        public MethodSetup Method()
        {
            var __local__ = mock.Setup(mock => mock.Method());
            return new MethodSetup(__local__);
        }

        #nullable disable warnings
        public class MethodParameters1
        {
            public int Parameter;
        }
        #nullable enable warnings

        private delegate void InternalMethodCallback1(
            int Parameter);

        private delegate int InternalMethodValueFunction1(
            int Parameter);

        private delegate TException InternalMethodExceptionFunction1<TException>(
            int Parameter);

        public delegate void MethodCallback1(MethodParameters1 parameters);

        public delegate int MethodValueFunction1(MethodParameters1 parameters);

        public delegate TException MethodExceptionFunction1<TException>(MethodParameters1 parameters);

        public class MethodSetup1
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public MethodSetup1(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public MethodSetup1 Callback(MethodCallback1 callback)
            {
                setup.Callback(new InternalMethodCallback1(
                    (int Parameter) => 
                    {
                        var __parameters__ = new MethodParameters1
                        {
                            Parameter = Parameter
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public MethodSetup1 Returns(MethodValueFunction1 valueFunction)
            {
                setup.Returns(new InternalMethodValueFunction1(
                    (int Parameter) => 
                    {
                        var __parameters__ = new MethodParameters1
                        {
                            Parameter = Parameter
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup1 Returns(int value)
                => Returns(_ => value);

            public MethodSetup1 Throws<TException>(MethodExceptionFunction1<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalMethodExceptionFunction1<TException>(
                    (int Parameter) => 
                    {
                        var __parameters__ = new MethodParameters1
                        {
                            Parameter = Parameter
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

        public MethodSetup1 Method1(
            Func<int, bool>? Parameter = null)
        {
            Parameter ??= static _ => true;
            Expression<Func<int, bool>> ParameterExpression = argument => Parameter(argument);
            var __local__ = mock.Setup(mock => mock.Method(
                It.Is(ParameterExpression)));
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
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.Method(),
                times);
        }

        public void Method1(
            Func<int, bool>? Parameter = null,
            Times times = default(Times)!)
        {
            Parameter ??= static _ => true;
            Expression<Func<int, bool>> ParameterExpression = argument => Parameter(argument);
            mock.Verify(mock => mock.Method(
                It.Is(ParameterExpression)),
                times);
        }
    }
}

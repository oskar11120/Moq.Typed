//HintName: Moq.Typed.Tests.Unit.ITestable.cs
using Moq;
using Moq.Language.Flow;
using System;
using System.CodeDom.Compiler;
using System.Linq.Expressions;

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

        public class MethodParameters
        {
        }

        private delegate void InternalMethodCallback();

        private delegate int InternalMethodValueFunction();

        public delegate void MethodCallback(MethodParameters parameters);

        public delegate int MethodValueFunction(MethodParameters parameters);

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
        }

        public MethodSetup Method()
        {
            var __local__ = mock.Setup(mock => mock.Method());
            return new MethodSetup(__local__);
        }

        public class MethodParameters1
        {
            public int Parameter;
        }

        private delegate void InternalMethodCallback1(
            int Parameter);

        private delegate int InternalMethodValueFunction1(
            int Parameter);

        public delegate void MethodCallback1(MethodParameters1 parameters);

        public delegate int MethodValueFunction1(MethodParameters1 parameters);

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
        }

        public MethodSetup1 Method(
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

        public class MethodParameters
        {
        }

        public void Method(
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.Method(),
                times);
        }

        public class MethodParameters1
        {
            public int Parameter;
        }

        public void Method(
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

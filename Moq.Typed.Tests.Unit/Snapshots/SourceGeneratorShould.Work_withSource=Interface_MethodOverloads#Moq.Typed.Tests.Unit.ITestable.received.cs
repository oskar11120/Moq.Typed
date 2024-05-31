//HintName: Moq.Typed.Tests.Unit.ITestable.cs
using Moq;
using Moq.Language.Flow;
using System;
using System.CodeDom.Compiler;
using System.Linq.Expressions;

namespace Moq.Typed.Tests.Unit
{
    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockSetupExtension_ForITestable
    {
        public static TypedMock_ForITestable Setup(this Mock<Moq.Typed.Tests.Unit.ITestable> mock)
            => new TypedMock_ForITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal class TypedMock_ForITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable> mock;

        public TypedMock_ForITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            this.mock = mock;
        }
        public class MethodParameters
        {
        }

        public class MethodSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public MethodSetup Callback(Action<MethodParameters> callback)
            {
                setup.Callback(
                    () => 
                    {
                        var parameters = new MethodParameters
                        {
                        };
                        callback(parameters);
                    });
                return this;
            }

            public MethodSetup Returns(int value)
                => Returns(_ => value);

            public MethodSetup Returns(Func<MethodParameters, int> valueFunction)
            {
                setup.Returns(
                    () => 
                    {
                        var parameters = new MethodParameters
                        {
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public MethodSetup Method()
        {
            var __setup__ = mock.Setup(mock => mock.Method());
            return new MethodSetup(__setup__);
        }
        public class MethodParameters1
        {
            public int Parameter { get; init; }
        }

        public class MethodSetup1
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public MethodSetup1(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public MethodSetup1 Callback(Action<MethodParameters1> callback)
            {
                setup.Callback<int>(
                    (Parameter) => 
                    {
                        var parameters = new MethodParameters1
                        {
                                Parameter = Parameter
                        };
                        callback(parameters);
                    });
                return this;
            }

            public MethodSetup1 Returns(int value)
                => Returns(_ => value);

            public MethodSetup1 Returns(Func<MethodParameters1, int> valueFunction)
            {
                setup.Returns<int>(
                    (Parameter) => 
                    {
                        var parameters = new MethodParameters1
                        {
                                Parameter = Parameter
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public MethodSetup1 Method(
            Func<int, bool>? Parameter = null)
        {
            Parameter ??= static _ => true;
            Expression<Func<int, bool>> ParameterExpression = argument => Parameter(argument);
            var __setup__ = mock.Setup(mock => mock.Method(
                It.Is(ParameterExpression)));
            return new MethodSetup1(__setup__);
        }
    }
}

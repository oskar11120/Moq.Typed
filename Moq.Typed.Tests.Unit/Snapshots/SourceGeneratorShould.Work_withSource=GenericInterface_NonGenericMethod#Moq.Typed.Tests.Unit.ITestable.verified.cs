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
        public static TypedMock_ForITestable Setup(this Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock)
            => new TypedMock_ForITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal class TypedMock_ForITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock;

        public TypedMock_ForITestable(Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock)
        {
            this.mock = mock;
        }
        public class FirstParameters
        {
        }

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup)
            {
                this.setup = setup;
            }

            public FirstSetup Callback(Action<FirstParameters> callback)
            {
                setup.Callback(
                    () => 
                    {
                        var parameters = new FirstParameters
                        {
                        };
                        callback(parameters);
                    });
                return this;
            }

            public FirstSetup Returns(int value)
                => Returns(_ => value);

            public FirstSetup Returns(Func<FirstParameters, int> valueFunction)
            {
                setup.Returns(
                    () => 
                    {
                        var parameters = new FirstParameters
                        {
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public FirstSetup First()
        {
            var __setup__ = mock.Setup(mock => mock.First());
            return new FirstSetup(__setup__);
        }
        public class SecondParameters
        {
            public int genericParam { get; init; }
        }

        public class SecondSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup)
            {
                this.setup = setup;
            }

            public SecondSetup Callback(Action<SecondParameters> callback)
            {
                setup.Callback<
                    int>(
                    (genericParam) => 
                    {
                        var parameters = new SecondParameters
                        {
                            genericParam = genericParam
                        };
                        callback(parameters);
                    });
                return this;
            }

            public SecondSetup Returns(int value)
                => Returns(_ => value);

            public SecondSetup Returns(Func<SecondParameters, int> valueFunction)
            {
                setup.Returns<
                    int>(
                    (genericParam) => 
                    {
                        var parameters = new SecondParameters
                        {
                                genericParam = genericParam
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public SecondSetup Second(
            Func<int, bool>? genericParam = null)
        {
            genericParam ??= static _ => true;
            Expression<Func<int, bool>> genericParamExpression = argument => genericParam(argument);
            var __setup__ = mock.Setup(mock => mock.Second(
                It.Is(genericParamExpression)));
            return new SecondSetup(__setup__);
        }
    }
}

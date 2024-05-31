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
        public class FirstParameters<TInput>
        {
            public TInput genericParam { get; init; }
        }

        public class FirstSetup<TInput>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup)
            {
                this.setup = setup;
            }

            public FirstSetup<TInput> Callback(Action<FirstParameters<TInput>> callback)
            {
                setup.Callback<TInput>(
                    (genericParam) => 
                    {
                        var parameters = new FirstParameters<TInput>
                        {
                                genericParam = genericParam
                        };
                        callback(parameters);
                    });
                return this;
            }

            public FirstSetup<TInput> Returns(int value)
                => Returns(_ => value);

            public FirstSetup<TInput> Returns(Func<FirstParameters<TInput>, int> valueFunction)
            {
                setup.Returns<TInput>(
                    (genericParam) => 
                    {
                        var parameters = new FirstParameters<TInput>
                        {
                                genericParam = genericParam
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public FirstSetup<TInput> First<TInput>(
            Func<TInput, bool>? genericParam = null)
        {
            genericParam ??= static _ => true;
            Expression<Func<TInput, bool>> genericParamExpression = argument => genericParam(argument);
            var __setup__ = mock.Setup(mock => mock.First<TInput>(
                It.Is(genericParamExpression)));
            return new FirstSetup<TInput>(__setup__);
        }
        public class SecondParameters<TOutput>
        {
            public int genericParam { get; init; }
        }

        public class SecondSetup<TOutput>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable<int>, TOutput> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable<int>, TOutput> setup)
            {
                this.setup = setup;
            }

            public SecondSetup<TOutput> Callback(Action<SecondParameters<TOutput>> callback)
            {
                setup.Callback<int>(
                    (genericParam) => 
                    {
                        var parameters = new SecondParameters<TOutput>
                        {
                                genericParam = genericParam
                        };
                        callback(parameters);
                    });
                return this;
            }

            public SecondSetup<TOutput> Returns(TOutput value)
                => Returns(_ => value);

            public SecondSetup<TOutput> Returns(Func<SecondParameters<TOutput>, TOutput> valueFunction)
            {
                setup.Returns<int>(
                    (genericParam) => 
                    {
                        var parameters = new SecondParameters<TOutput>
                        {
                                genericParam = genericParam
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public SecondSetup<TOutput> Second<TOutput>(
            Func<int, bool>? genericParam = null)
        {
            genericParam ??= static _ => true;
            Expression<Func<int, bool>> genericParamExpression = argument => genericParam(argument);
            var __setup__ = mock.Setup(mock => mock.Second<TOutput>(
                It.Is(genericParamExpression)));
            return new SecondSetup<TOutput>(__setup__);
        }
    }
}

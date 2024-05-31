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
        public class FirstParameters<T>
        {
            public T someGenericParam { get; init; }
        }

        public class FirstSetup<T>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public FirstSetup<T> Callback(Action<FirstParameters<T>> callback)
            {
                setup.Callback<
                    T>(
                    (someGenericParam) => 
                    {
                        var parameters = new FirstParameters<T>
                        {
                            someGenericParam = someGenericParam
                        };
                        callback(parameters);
                    });
                return this;
            }
        }

        public FirstSetup<T> First<T>(
            Func<T, bool>? someGenericParam = null)
        {
            someGenericParam ??= static _ => true;
            Expression<Func<T, bool>> someGenericParamExpression = argument => someGenericParam(argument);
            var __setup__ = mock.Setup(mock => mock.First<T>(
                It.Is(someGenericParamExpression)));
            return new FirstSetup<T>(__setup__);
        }
        public class SecondParameters<TInput, TOutput>
        {
            public int someInt { get; init; }
            public TInput genericInput { get; init; }
        }

        public class SecondSetup<TInput, TOutput>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, TOutput> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, TOutput> setup)
            {
                this.setup = setup;
            }

            public SecondSetup<TInput, TOutput> Callback(Action<SecondParameters<TInput, TOutput>> callback)
            {
                setup.Callback<
                    int, TInput>(
                    (someInt, genericInput) => 
                    {
                        var parameters = new SecondParameters<TInput, TOutput>
                        {
                            someInt = someInt
                            genericInput = genericInput
                        };
                        callback(parameters);
                    });
                return this;
            }

            public SecondSetup<TInput, TOutput> Returns(TOutput value)
                => Returns(_ => value);

            public SecondSetup<TInput, TOutput> Returns(Func<SecondParameters<TInput, TOutput>, TOutput> valueFunction)
            {
                setup.Returns<
                    int, TInput>(
                    (someInt, genericInput) => 
                    {
                        var parameters = new SecondParameters<TInput, TOutput>
                        {
                                someInt = someInt
                                genericInput = genericInput
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public SecondSetup<TInput, TOutput> Second<TInput, TOutput>(
            Func<int, bool>? someInt = null, 
            Func<TInput, bool>? genericInput = null)
        {
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            genericInput ??= static _ => true;
            Expression<Func<TInput, bool>> genericInputExpression = argument => genericInput(argument);
            var __setup__ = mock.Setup(mock => mock.Second<TInput, TOutput>(
                It.Is(someIntExpression), 
                It.Is(genericInputExpression)));
            return new SecondSetup<TInput, TOutput>(__setup__);
        }
    }
}

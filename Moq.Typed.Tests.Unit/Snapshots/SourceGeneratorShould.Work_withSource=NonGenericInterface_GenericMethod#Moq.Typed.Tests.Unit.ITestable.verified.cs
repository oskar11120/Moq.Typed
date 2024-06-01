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

        public class FirstParameters<T>
        {
            public T someGenericParam;
        }

        private delegate void InternalFirstCallback<T>(
            T someGenericParam);

        public delegate void FirstCallback<T>(FirstParameters<T> parameters);

        public class FirstSetup<T>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public FirstSetup<T> Callback(FirstCallback<T> callback)
            {
                setup.Callback(new InternalFirstCallback<T>(
                    (T someGenericParam) => 
                    {
                        var __parameters__ = new FirstParameters<T>
                        {
                            someGenericParam = someGenericParam
                        };
                        callback(__parameters__);
                    }));
                return this;
            }
        }

        public FirstSetup<T> First<T>(
            Func<T, bool>? someGenericParam = null)
        {
            someGenericParam ??= static _ => true;
            Expression<Func<T, bool>> someGenericParamExpression = argument => someGenericParam(argument);
            var __local__ = mock.Setup(mock => mock.First<T>(
                It.Is(someGenericParamExpression)));
            return new FirstSetup<T>(__local__);
        }

        public class SecondParameters<TInput, TOutput>
        {
            public int someInt;
            public TInput genericInput;
        }

        private delegate void InternalSecondCallback<TInput, TOutput>(
            int someInt, 
            TInput genericInput);

        private delegate TOutput InternalSecondValueFunction<TInput, TOutput>(
            int someInt, 
            TInput genericInput);

        public delegate void SecondCallback<TInput, TOutput>(SecondParameters<TInput, TOutput> parameters);

        public delegate TOutput SecondValueFunction<TInput, TOutput>(SecondParameters<TInput, TOutput> parameters);

        public class SecondSetup<TInput, TOutput>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, TOutput> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, TOutput> setup)
            {
                this.setup = setup;
            }

            public SecondSetup<TInput, TOutput> Callback(SecondCallback<TInput, TOutput> callback)
            {
                setup.Callback(new InternalSecondCallback<TInput, TOutput>(
                    (int someInt, TInput genericInput) => 
                    {
                        var __parameters__ = new SecondParameters<TInput, TOutput>
                        {
                            someInt = someInt, 
                            genericInput = genericInput
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public SecondSetup<TInput, TOutput> Returns(SecondValueFunction<TInput, TOutput> valueFunction)
            {
                setup.Returns(new InternalSecondValueFunction<TInput, TOutput>(
                    (int someInt, TInput genericInput) => 
                    {
                        var __parameters__ = new SecondParameters<TInput, TOutput>
                        {
                            someInt = someInt, 
                            genericInput = genericInput
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public SecondSetup<TInput, TOutput> Returns(TOutput value)
                => Returns(_ => value);
        }

        public SecondSetup<TInput, TOutput> Second<TInput, TOutput>(
            Func<int, bool>? someInt = null, 
            Func<TInput, bool>? genericInput = null)
        {
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            genericInput ??= static _ => true;
            Expression<Func<TInput, bool>> genericInputExpression = argument => genericInput(argument);
            var __local__ = mock.Setup(mock => mock.Second<TInput, TOutput>(
                It.Is(someIntExpression), 
                It.Is(genericInputExpression)));
            return new SecondSetup<TInput, TOutput>(__local__);
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

        public class FirstParameters<T>
        {
            public T someGenericParam;
        }

        public void First<T>(
            Func<T, bool>? someGenericParam = null,
            Times times = default(Times)!)
        {
            someGenericParam ??= static _ => true;
            Expression<Func<T, bool>> someGenericParamExpression = argument => someGenericParam(argument);
            mock.Verify(mock => mock.First<T>(
                It.Is(someGenericParamExpression)),
                times);
        }

        public class SecondParameters<TInput, TOutput>
        {
            public int someInt;
            public TInput genericInput;
        }

        public void Second<TInput, TOutput>(
            Func<int, bool>? someInt = null, 
            Func<TInput, bool>? genericInput = null,
            Times times = default(Times)!)
        {
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            genericInput ??= static _ => true;
            Expression<Func<TInput, bool>> genericInputExpression = argument => genericInput(argument);
            mock.Verify(mock => mock.Second<TInput, TOutput>(
                It.Is(someIntExpression), 
                It.Is(genericInputExpression)),
                times);
        }
    }
}

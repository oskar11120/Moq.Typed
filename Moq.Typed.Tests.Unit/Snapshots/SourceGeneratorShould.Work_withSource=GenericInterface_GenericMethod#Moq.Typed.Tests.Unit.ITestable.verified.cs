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
        public static TypedMockFor_ITestable Setup(this Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock)
            => new TypedMockFor_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockFor_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock;

        public TypedMockFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock)
        {
            this.mock = mock;
        }

        public class FirstParameters<TInput>
        {
            public TInput genericParam;
        }

        private delegate void InternalFirstCallback<TInput>(
            TInput genericParam);

        private delegate int InternalFirstValueFunction<TInput>(
            TInput genericParam);

        public delegate void FirstCallback<TInput>(FirstParameters<TInput> parameters);

        public delegate int FirstValueFunction<TInput>(FirstParameters<TInput> parameters);

        public class FirstSetup<TInput>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup)
            {
                this.setup = setup;
            }

            public FirstSetup<TInput> Callback(FirstCallback<TInput> callback)
            {
                setup.Callback(new InternalFirstCallback<TInput>(
                    (TInput genericParam) => 
                    {
                        var __parameters__ = new FirstParameters<TInput>
                        {
                            genericParam = genericParam
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public FirstSetup<TInput> Returns(FirstValueFunction<TInput> valueFunction)
            {
                setup.Returns(new InternalFirstValueFunction<TInput>(
                    (TInput genericParam) => 
                    {
                        var __parameters__ = new FirstParameters<TInput>
                        {
                            genericParam = genericParam
                        };
                        return valueFunction(__parameters__);
                    }));
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
            public int genericParam;
        }

        private delegate void InternalSecondCallback<TOutput>(
            int genericParam);

        private delegate TOutput InternalSecondValueFunction<TOutput>(
            int genericParam);

        public delegate void SecondCallback<TOutput>(SecondParameters<TOutput> parameters);

        public delegate TOutput SecondValueFunction<TOutput>(SecondParameters<TOutput> parameters);

        public class SecondSetup<TOutput>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable<int>, TOutput> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable<int>, TOutput> setup)
            {
                this.setup = setup;
            }

            public SecondSetup<TOutput> Callback(SecondCallback<TOutput> callback)
            {
                setup.Callback(new InternalSecondCallback<TOutput>(
                    (int genericParam) => 
                    {
                        var __parameters__ = new SecondParameters<TOutput>
                        {
                            genericParam = genericParam
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public SecondSetup<TOutput> Returns(SecondValueFunction<TOutput> valueFunction)
            {
                setup.Returns(new InternalSecondValueFunction<TOutput>(
                    (int genericParam) => 
                    {
                        var __parameters__ = new SecondParameters<TOutput>
                        {
                            genericParam = genericParam
                        };
                        return valueFunction(__parameters__);
                    }));
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

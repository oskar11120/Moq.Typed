//HintName: Moq.Typed.Tests.Unit.ITestable_int_.cs
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
        public static TypedMockSetupFor_ITestable Setup(this Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock)
            => new TypedMockSetupFor_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockSetupFor_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock;

        public TypedMockSetupFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock)
        {
            this.mock = mock;
        }

        #nullable disable warnings
        public class FirstParameters<TInput>
        {
            public TInput genericParam;
        }
        #nullable enable warnings

        private delegate void InternalFirstCallback<TInput>(
            TInput genericParam);

        private delegate int InternalFirstValueFunction<TInput>(
            TInput genericParam);

        private delegate TException InternalFirstExceptionFunction<TInput, TException>(
            TInput genericParam);

        public delegate void FirstCallback<TInput>(FirstParameters<TInput> parameters);

        public delegate int FirstValueFunction<TInput>(FirstParameters<TInput> parameters);

        public delegate TException FirstExceptionFunction<TInput, TException>(FirstParameters<TInput> parameters);

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

            public FirstSetup<TInput> Returns(int value)
                => Returns(_ => value);

            public FirstSetup<TInput> Throws<TException>(FirstExceptionFunction<TInput, TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalFirstExceptionFunction<TInput, TException>(
                    (TInput genericParam) => 
                    {
                        var __parameters__ = new FirstParameters<TInput>
                        {
                            genericParam = genericParam
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public FirstSetup<TInput> Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public FirstSetup<TInput> Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public FirstSetup<TInput> Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public FirstSetup<TInput> First<TInput>(
            Func<TInput, bool>? genericParam = null)
        {
            genericParam ??= static _ => true;
            Expression<Func<TInput, bool>> genericParamExpression = argument => genericParam(argument);
            var __local__ = mock.Setup(mock => mock.First<TInput>(
                It.Is(genericParamExpression)));
            return new FirstSetup<TInput>(__local__);
        }

        #nullable disable warnings
        public class SecondParameters<TOutput>
        {
            public int genericParam;
        }
        #nullable enable warnings

        private delegate void InternalSecondCallback<TOutput>(
            int genericParam);

        private delegate TOutput InternalSecondValueFunction<TOutput>(
            int genericParam);

        private delegate TException InternalSecondExceptionFunction<TOutput, TException>(
            int genericParam);

        public delegate void SecondCallback<TOutput>(SecondParameters<TOutput> parameters);

        public delegate TOutput SecondValueFunction<TOutput>(SecondParameters<TOutput> parameters);

        public delegate TException SecondExceptionFunction<TOutput, TException>(SecondParameters<TOutput> parameters);

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

            public SecondSetup<TOutput> Returns(TOutput value)
                => Returns(_ => value);

            public SecondSetup<TOutput> Throws<TException>(SecondExceptionFunction<TOutput, TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalSecondExceptionFunction<TOutput, TException>(
                    (int genericParam) => 
                    {
                        var __parameters__ = new SecondParameters<TOutput>
                        {
                            genericParam = genericParam
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public SecondSetup<TOutput> Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public SecondSetup<TOutput> Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public SecondSetup<TOutput> Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public SecondSetup<TOutput> Second<TOutput>(
            Func<int, bool>? genericParam = null)
        {
            genericParam ??= static _ => true;
            Expression<Func<int, bool>> genericParamExpression = argument => genericParam(argument);
            var __local__ = mock.Setup(mock => mock.Second<TOutput>(
                It.Is(genericParamExpression)));
            return new SecondSetup<TOutput>(__local__);
        }
    }

    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockVerifyExtensionFor_ITestable
    {
        public static TypedMockVerifyFor_ITestable Verifyy(this Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock)
            => new TypedMockVerifyFor_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockVerifyFor_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock;

        public TypedMockVerifyFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable<int>> mock)
        {
            this.mock = mock;
        }

        public void First<TInput>(
            Func<TInput, bool>? genericParam = null,
            Times times = default(Times)!)
        {
            genericParam ??= static _ => true;
            Expression<Func<TInput, bool>> genericParamExpression = argument => genericParam(argument);
            mock.Verify(mock => mock.First<TInput>(
                It.Is(genericParamExpression)),
                times);
        }

        public void Second<TOutput>(
            Func<int, bool>? genericParam = null,
            Times times = default(Times)!)
        {
            genericParam ??= static _ => true;
            Expression<Func<int, bool>> genericParamExpression = argument => genericParam(argument);
            mock.Verify(mock => mock.Second<TOutput>(
                It.Is(genericParamExpression)),
                times);
        }
    }
}

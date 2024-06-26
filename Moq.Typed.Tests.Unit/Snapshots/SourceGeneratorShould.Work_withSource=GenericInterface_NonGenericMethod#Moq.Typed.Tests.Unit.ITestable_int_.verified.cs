﻿//HintName: Moq.Typed.Tests.Unit.ITestable_int_.cs
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
        public class FirstParameters
        {
        }
        #nullable enable warnings

        private delegate void InternalFirstCallback();

        private delegate int InternalFirstValueFunction();

        private delegate TException InternalFirstExceptionFunction<TException>();

        public delegate void FirstCallback(FirstParameters parameters);

        public delegate int FirstValueFunction(FirstParameters parameters);

        public delegate TException FirstExceptionFunction<TException>(FirstParameters parameters);

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup)
            {
                this.setup = setup;
            }

            public FirstSetup Callback(FirstCallback callback)
            {
                setup.Callback(new InternalFirstCallback(
                    () => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public FirstSetup Returns(FirstValueFunction valueFunction)
            {
                setup.Returns(new InternalFirstValueFunction(
                    () => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public FirstSetup Returns(int value)
                => Returns(_ => value);

            public FirstSetup Throws<TException>(FirstExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalFirstExceptionFunction<TException>(
                    () => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public FirstSetup Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public FirstSetup Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public FirstSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public FirstSetup First()
        {
            var __local__ = mock.Setup(mock => mock.First());
            return new FirstSetup(__local__);
        }

        #nullable disable warnings
        public class SecondParameters
        {
            public int genericParam;
        }
        #nullable enable warnings

        private delegate void InternalSecondCallback(
            int genericParam);

        private delegate int InternalSecondValueFunction(
            int genericParam);

        private delegate TException InternalSecondExceptionFunction<TException>(
            int genericParam);

        public delegate void SecondCallback(SecondParameters parameters);

        public delegate int SecondValueFunction(SecondParameters parameters);

        public delegate TException SecondExceptionFunction<TException>(SecondParameters parameters);

        public class SecondSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable<int>, int> setup)
            {
                this.setup = setup;
            }

            public SecondSetup Callback(SecondCallback callback)
            {
                setup.Callback(new InternalSecondCallback(
                    (int genericParam) => 
                    {
                        var __parameters__ = new SecondParameters
                        {
                            genericParam = genericParam
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public SecondSetup Returns(SecondValueFunction valueFunction)
            {
                setup.Returns(new InternalSecondValueFunction(
                    (int genericParam) => 
                    {
                        var __parameters__ = new SecondParameters
                        {
                            genericParam = genericParam
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public SecondSetup Returns(int value)
                => Returns(_ => value);

            public SecondSetup Throws<TException>(SecondExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalSecondExceptionFunction<TException>(
                    (int genericParam) => 
                    {
                        var __parameters__ = new SecondParameters
                        {
                            genericParam = genericParam
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public SecondSetup Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public SecondSetup Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public SecondSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public SecondSetup Second(
            Func<int, bool>? genericParam = null)
        {
            genericParam ??= static _ => true;
            Expression<Func<int, bool>> genericParamExpression = argument => genericParam(argument);
            var __local__ = mock.Setup(mock => mock.Second(
                It.Is(genericParamExpression)));
            return new SecondSetup(__local__);
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

        public void First(
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.First(),
                times);
        }

        public void Second(
            Func<int, bool>? genericParam = null,
            Times times = default(Times)!)
        {
            genericParam ??= static _ => true;
            Expression<Func<int, bool>> genericParamExpression = argument => genericParam(argument);
            mock.Verify(mock => mock.Second(
                It.Is(genericParamExpression)),
                times);
        }
    }
}

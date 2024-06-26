﻿//HintName: Moq.Typed.Tests.Unit.ITestable.cs
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
        public class FirstParameters
        {
        }
        #nullable enable warnings

        private delegate void InternalFirstCallback();

        private delegate TException InternalFirstExceptionFunction<TException>();

        public delegate void FirstCallback(FirstParameters parameters);

        public delegate TException FirstExceptionFunction<TException>(FirstParameters parameters);

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
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
            public IEnumerable<int> someInts;
        }
        #nullable enable warnings

        private delegate void InternalSecondCallback(
            IEnumerable<int> someInts);

        private delegate int InternalSecondValueFunction(
            IEnumerable<int> someInts);

        private delegate TException InternalSecondExceptionFunction<TException>(
            IEnumerable<int> someInts);

        public delegate void SecondCallback(SecondParameters parameters);

        public delegate int SecondValueFunction(SecondParameters parameters);

        public delegate TException SecondExceptionFunction<TException>(SecondParameters parameters);

        public class SecondSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public SecondSetup Callback(SecondCallback callback)
            {
                setup.Callback(new InternalSecondCallback(
                    (IEnumerable<int> someInts) => 
                    {
                        var __parameters__ = new SecondParameters
                        {
                            someInts = someInts
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public SecondSetup Returns(SecondValueFunction valueFunction)
            {
                setup.Returns(new InternalSecondValueFunction(
                    (IEnumerable<int> someInts) => 
                    {
                        var __parameters__ = new SecondParameters
                        {
                            someInts = someInts
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
                    (IEnumerable<int> someInts) => 
                    {
                        var __parameters__ = new SecondParameters
                        {
                            someInts = someInts
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
            Func<IEnumerable<int>, bool>? someInts = null)
        {
            someInts ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> someIntsExpression = argument => someInts(argument);
            var __local__ = mock.Setup(mock => mock.Second(
                It.Is(someIntsExpression)));
            return new SecondSetup(__local__);
        }

        #nullable disable warnings
        public class ThirdParameters
        {
            public IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters;
            public Moq.Typed.Tests.Unit.Parameter oneMoreParameter;
            public int someInt;
        }
        #nullable enable warnings

        private delegate void InternalThirdCallback(
            IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
            Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
            int someInt);

        private delegate TException InternalThirdExceptionFunction<TException>(
            IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
            Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
            int someInt);

        public delegate void ThirdCallback(ThirdParameters parameters);

        public delegate TException ThirdExceptionFunction<TException>(ThirdParameters parameters);

        public class ThirdSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public ThirdSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public ThirdSetup Callback(ThirdCallback callback)
            {
                setup.Callback(new InternalThirdCallback(
                    (IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, Moq.Typed.Tests.Unit.Parameter oneMoreParameter, int someInt) => 
                    {
                        var __parameters__ = new ThirdParameters
                        {
                            someParameters = someParameters, 
                            oneMoreParameter = oneMoreParameter, 
                            someInt = someInt
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public ThirdSetup Throws<TException>(ThirdExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalThirdExceptionFunction<TException>(
                    (IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, Moq.Typed.Tests.Unit.Parameter oneMoreParameter, int someInt) => 
                    {
                        var __parameters__ = new ThirdParameters
                        {
                            someParameters = someParameters, 
                            oneMoreParameter = oneMoreParameter, 
                            someInt = someInt
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public ThirdSetup Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public ThirdSetup Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public ThirdSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public ThirdSetup Third(
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>? someParameters = null, 
            Func<Moq.Typed.Tests.Unit.Parameter, bool>? oneMoreParameter = null, 
            Func<int, bool>? someInt = null)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            var __local__ = mock.Setup(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(oneMoreParameterExpression), 
                It.Is(someIntExpression)));
            return new ThirdSetup(__local__);
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

        public void First(
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.First(),
                times);
        }

        public void Second(
            Func<IEnumerable<int>, bool>? someInts = null,
            Times times = default(Times)!)
        {
            someInts ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> someIntsExpression = argument => someInts(argument);
            mock.Verify(mock => mock.Second(
                It.Is(someIntsExpression)),
                times);
        }

        public void Third(
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>? someParameters = null, 
            Func<Moq.Typed.Tests.Unit.Parameter, bool>? oneMoreParameter = null, 
            Func<int, bool>? someInt = null,
            Times times = default(Times)!)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            mock.Verify(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(oneMoreParameterExpression), 
                It.Is(someIntExpression)),
                times);
        }
    }
}

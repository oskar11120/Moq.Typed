//HintName: Moq.Typed.Tests.Unit.ITestable.cs
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
        public class PublicParameters
        {
        }
        #nullable enable warnings

        private delegate void InternalPublicCallback();

        private delegate TException InternalPublicExceptionFunction<TException>();

        public delegate void PublicCallback(PublicParameters parameters);

        public delegate TException PublicExceptionFunction<TException>(PublicParameters parameters);

        public class PublicSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public PublicSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public PublicSetup Callback(PublicCallback callback)
            {
                setup.Callback(new InternalPublicCallback(
                    () => 
                    {
                        var __parameters__ = new PublicParameters
                        {
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public PublicSetup Throws<TException>(PublicExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalPublicExceptionFunction<TException>(
                    () => 
                    {
                        var __parameters__ = new PublicParameters
                        {
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public PublicSetup Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public PublicSetup Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public PublicSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public PublicSetup Public()
        {
            var __local__ = mock.Setup(mock => mock.Public());
            return new PublicSetup(__local__);
        }

        #nullable disable warnings
        public class InternalParameters
        {
            public object someObject;
        }
        #nullable enable warnings

        private delegate void InternalInternalCallback(
            object someObject);

        private delegate int InternalInternalValueFunction(
            object someObject);

        private delegate TException InternalInternalExceptionFunction<TException>(
            object someObject);

        public delegate void InternalCallback(InternalParameters parameters);

        public delegate int InternalValueFunction(InternalParameters parameters);

        public delegate TException InternalExceptionFunction<TException>(InternalParameters parameters);

        public class InternalSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public InternalSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public InternalSetup Callback(InternalCallback callback)
            {
                setup.Callback(new InternalInternalCallback(
                    (object someObject) => 
                    {
                        var __parameters__ = new InternalParameters
                        {
                            someObject = someObject
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public InternalSetup Returns(InternalValueFunction valueFunction)
            {
                setup.Returns(new InternalInternalValueFunction(
                    (object someObject) => 
                    {
                        var __parameters__ = new InternalParameters
                        {
                            someObject = someObject
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public InternalSetup Returns(int value)
                => Returns(_ => value);

            public InternalSetup Throws<TException>(InternalExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalInternalExceptionFunction<TException>(
                    (object someObject) => 
                    {
                        var __parameters__ = new InternalParameters
                        {
                            someObject = someObject
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public InternalSetup Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public InternalSetup Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public InternalSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public InternalSetup Internal(
            Func<object, bool>? someObject = null)
        {
            someObject ??= static _ => true;
            Expression<Func<object, bool>> someObjectExpression = argument => someObject(argument);
            var __local__ = mock.Setup(mock => mock.Internal(
                It.Is(someObjectExpression)));
            return new InternalSetup(__local__);
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

        public void Public(
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.Public(),
                times);
        }

        public void Internal(
            Func<object, bool>? someObject = null,
            Times times = default(Times)!)
        {
            someObject ??= static _ => true;
            Expression<Func<object, bool>> someObjectExpression = argument => someObject(argument);
            mock.Verify(mock => mock.Internal(
                It.Is(someObjectExpression)),
                times);
        }
    }
}

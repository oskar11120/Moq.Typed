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
        public class FirstParameters
        {
            public object someObject;
        }
        #nullable enable warnings

        private delegate void InternalFirstCallback(
            object someObject);

        private delegate int InternalFirstValueFunction(
            object someObject);

        private delegate TException InternalFirstExceptionFunction<TException>(
            object someObject);

        public delegate void FirstCallback(FirstParameters parameters);

        public delegate int FirstValueFunction(FirstParameters parameters);

        public delegate TException FirstExceptionFunction<TException>(FirstParameters parameters);

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public FirstSetup Callback(FirstCallback callback)
            {
                setup.Callback(new InternalFirstCallback(
                    (object someObject) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            someObject = someObject
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public FirstSetup Returns(FirstValueFunction valueFunction)
            {
                setup.Returns(new InternalFirstValueFunction(
                    (object someObject) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            someObject = someObject
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
                    (object someObject) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            someObject = someObject
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

        public FirstSetup First(
            Func<object, bool>? someObject = null)
        {
            someObject ??= static _ => true;
            Expression<Func<object, bool>> someObjectExpression = argument => someObject(argument);
            var __local__ = mock.Setup(mock => mock.First(
                It.Is(someObjectExpression)));
            return new FirstSetup(__local__);
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
            Func<object, bool>? someObject = null,
            Times times = default(Times)!)
        {
            someObject ??= static _ => true;
            Expression<Func<object, bool>> someObjectExpression = argument => someObject(argument);
            mock.Verify(mock => mock.First(
                It.Is(someObjectExpression)),
                times);
        }
    }
}

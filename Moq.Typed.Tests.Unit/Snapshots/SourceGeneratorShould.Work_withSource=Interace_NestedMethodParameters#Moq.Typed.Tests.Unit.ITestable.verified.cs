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
            public Moq.Typed.Tests.Unit.Parameters.First parameter;
        }
        #nullable enable warnings

        private delegate void InternalFirstCallback(
            Moq.Typed.Tests.Unit.Parameters.First parameter);

        private delegate int InternalFirstValueFunction(
            Moq.Typed.Tests.Unit.Parameters.First parameter);

        private delegate TException InternalFirstExceptionFunction<TException>(
            Moq.Typed.Tests.Unit.Parameters.First parameter);

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
                    (Moq.Typed.Tests.Unit.Parameters.First parameter) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            parameter = parameter
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public FirstSetup Returns(FirstValueFunction valueFunction)
            {
                setup.Returns(new InternalFirstValueFunction(
                    (Moq.Typed.Tests.Unit.Parameters.First parameter) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            parameter = parameter
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
                    (Moq.Typed.Tests.Unit.Parameters.First parameter) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            parameter = parameter
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
            Func<Moq.Typed.Tests.Unit.Parameters.First, bool>? parameter = null)
        {
            parameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameters.First, bool>> parameterExpression = argument => parameter(argument);
            var __local__ = mock.Setup(mock => mock.First(
                It.Is(parameterExpression)));
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
            Func<Moq.Typed.Tests.Unit.Parameters.First, bool>? parameter = null,
            Times times = default(Times)!)
        {
            parameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameters.First, bool>> parameterExpression = argument => parameter(argument);
            mock.Verify(mock => mock.First(
                It.Is(parameterExpression)),
                times);
        }
    }
}

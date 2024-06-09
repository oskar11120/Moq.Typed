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
        public class MethodParameters
        {
            public int outParameter;
        }
        #nullable enable warnings

        private delegate void InternalMethodCallback(
            int outParameter);

        private delegate TException InternalMethodExceptionFunction<TException>(
            int outParameter);

        public delegate void MethodCallback(MethodParameters parameters);

        public delegate TException MethodExceptionFunction<TException>(MethodParameters parameters);

        public class MethodSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public MethodSetup Callback(MethodCallback callback)
            {
                setup.Callback(new InternalMethodCallback(
                    (int outParameter) => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                            outParameter = outParameter
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public MethodSetup Throws<TException>(MethodExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalMethodExceptionFunction<TException>(
                    (int outParameter) => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                            outParameter = outParameter
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public MethodSetup Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public MethodSetup Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public MethodSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public MethodSetup Method(
            int outParameter = default(int)!)
        {
            var __local__ = mock.Setup(mock => mock.Method(
                out outParameter));
            return new MethodSetup(__local__);
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

        public void Method(
            Times times = default(Times)!)
        {
            int outParameter;
            mock.Verify(mock => mock.Method(
                out outParameter),
                times);
        }
    }
}

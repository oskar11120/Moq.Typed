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
        }
        #nullable enable warnings

        private delegate void InternalMethodCallback();

        private delegate TException InternalMethodExceptionFunction<TException>();

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
                    () => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public MethodSetup Throws<TException>(MethodExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalMethodExceptionFunction<TException>(
                    () => 
                    {
                        var __parameters__ = new MethodParameters
                        {
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

        public MethodSetup Method()
        {
            var __local__ = mock.Setup(mock => mock.Method());
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
            mock.Verify(mock => mock.Method(),
                times);
        }
    }
}

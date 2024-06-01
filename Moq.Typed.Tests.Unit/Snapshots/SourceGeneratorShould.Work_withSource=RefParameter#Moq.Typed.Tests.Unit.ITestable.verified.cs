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

        public ref struct MethodParameters
        {
            public ref int refParameter;
        }

        private delegate void InternalMethodCallback(
            ref int refParameter);

        private delegate int InternalMethodValueFunction(
            ref int refParameter);

        public delegate void MethodCallback(ref MethodParameters parameters);

        public delegate int MethodValueFunction(ref MethodParameters parameters);

        public class MethodSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public MethodSetup Callback(MethodCallback callback)
            {
                setup.Callback(new InternalMethodCallback(
                    (ref int refParameter) => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                            refParameter = ref refParameter
                        };
                        callback(ref __parameters__);
                    }));
                return this;
            }

            public MethodSetup Returns(MethodValueFunction valueFunction)
            {
                setup.Returns(new InternalMethodValueFunction(
                    (ref int refParameter) => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                            refParameter = ref refParameter
                        };
                        return valueFunction(ref __parameters__);
                    }));
                return this;
            }
        }

        public MethodSetup Method()
        {
            var __local__ = mock.Setup(mock => mock.Method(
                ref It.Ref<int>.IsAny));
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

        public ref struct MethodParameters
        {
            public ref int refParameter;
        }

        public void Method(
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.Method(
                ref It.Ref<int>.IsAny),
                times);
        }
    }
}

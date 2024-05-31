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
        public static TypedMockFor_ITestable Setup(this Mock<Moq.Typed.Tests.Unit.ITestable> mock)
            => new TypedMockFor_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockFor_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable> mock;

        public TypedMockFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            this.mock = mock;
        }

        public ref struct MethodParameters
        {
            public int inParameter;
        }

        private delegate void InternalMethodCallback(
            in int inParameter);

        public delegate void MethodCallback(MethodParameters parameters);

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
                    (in int inParameter) => 
                    {
                        var __parameters__ = new MethodParameters
                        {
                            inParameter = inParameter
                        };
                        callback(__parameters__);
                    }));
                return this;
            }
        }

        public MethodSetup Method()
        {
            var __setup__ = mock.Setup(mock => mock.Method(
                ref It.Ref<int>.IsAny));
            return new MethodSetup(__setup__);
        }
    }
}

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

        public class MethodParameters
        {
            public int outParameter;
        }

        private delegate void InternalMethodCallback(
            int outParameter);

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
        }

        public MethodSetup Method(
            int outParameter = default(int)!)
        {
            var __setup__ = mock.Setup(mock => mock.Method(
                out outParameter));
            return new MethodSetup(__setup__);
        }
    }
}

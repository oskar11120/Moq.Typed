//HintName: Moq.Typed.Tests.Unit.ITestable.cs
using Moq;
using Moq.Language.Flow;
using System;
using System.CodeDom.Compiler;
using System.Linq.Expressions;

namespace Moq.Typed.Tests.Unit
{
    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockSetupExtension_ForITestable
    {
        public static TypedMock_ForITestable Setup(this Mock<Moq.Typed.Tests.Unit.ITestable> mock)
            => new TypedMock_ForITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal class TypedMock_ForITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable> mock;

        public TypedMock_ForITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            this.mock = mock;
        }
        public class MethodParameters
        {
            public int inParameter { get; init; }
        }

        public class MethodSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public MethodSetup Callback(Action<MethodParameters> callback)
            {
                setup.Callback<int>(
                    (inParameter) => 
                    {
                        var parameters = new MethodParameters
                        {
                                inParameter = inParameter
                        };
                        callback(parameters);
                    });
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

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

        public ref struct MethodParameters
        {
            public ref int refParameter;
        }

        private delegate void InternalMethodCallback(
            ref int refParameter);

        private delegate int InternalMethodValueFunction(
            ref int refParameter);

        public delegate void MethodCallback(ref  MethodCallback parameters)

        public delegate int MethodValueFunction(ref  MethodValueFunction parameters)

        public class MethodSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public MethodSetup Callback(MethodCallback callback)
            {
                setup.Callback(new MethodCallbackInternal(
                    ((ref int refParameter)) => 
                    {
                        var parameters = new MethodParameters
                        {
                            refParameter = ref refParameter
                        };
                        callback(parameters);
                    }));
                return this;
            }

            public MethodSetup Returns(int value)
                => Returns(_ => value);

            public MethodSetup Returns(MethodValueFunction valueFunction)
            {
                setup.Returns(new MethodValueFunctionInternal(
                    ((ref int refParameter)) => 
                    {
                        var parameters = new MethodParameters
                        {
                            refParameter = ref refParameter
                        };
                        return valueFunction(parameters);
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

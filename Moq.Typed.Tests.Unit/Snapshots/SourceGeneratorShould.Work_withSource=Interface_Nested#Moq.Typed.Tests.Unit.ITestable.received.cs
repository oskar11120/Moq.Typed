//HintName: Moq.Typed.Tests.Unit.ITestable.cs
using Moq;
using Moq.Language.Flow;
using System;
using System.CodeDom.Compiler;
using System.Linq.Expressions;

namespace Moq.Typed.Tests.Unit
{
    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockSetupExtensionFor_Interfaces_ITestable
    {
        public static TypedMockFor_Interfaces_ITestable Setup(this Mock<Moq.Typed.Tests.Unit.Interfaces.ITestable> mock)
            => new TypedMockFor_Interfaces_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockFor_Interfaces_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.Interfaces.ITestable> mock;

        public TypedMockFor_Interfaces_ITestable(Mock<Moq.Typed.Tests.Unit.Interfaces.ITestable> mock)
        {
            this.mock = mock;
        }

        public class FirstParameters
        {
        }

        private delegate void InternalFirstCallback();

        private delegate int InternalFirstValueFunction();

        public delegate void FirstCallback(FirstParameters parameters);

        public delegate int FirstValueFunction(FirstParameters parameters);

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.Interfaces.ITestable, int> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.Interfaces.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public FirstSetup Callback(FirstCallback callback)
            {
                setup.Callback(new InternalFirstCallback(
                    () => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public FirstSetup Returns(FirstValueFunction valueFunction)
            {
                setup.Returns(new InternalFirstValueFunction(
                    () => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }
        }

        public FirstSetup First()
        {
            var __setup__ = mock.Setup(mock => mock.First());
            return new FirstSetup(__setup__);
        }
    }
}

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

        public ISetup<Moq.Typed.Tests.Unit.ITestable, int> Getter()
        {
            return mock.Setup(mock => mock.Getter);
        }

        public ISetup<Moq.Typed.Tests.Unit.ITestable, IEnumerable<int>> Mutable()
        {
            return mock.Setup(mock => mock.Mutable);
        }

        public ISetup<Moq.Typed.Tests.Unit.ITestable, int> InitOnly()
        {
            return mock.Setup(mock => mock.InitOnly);
        }

        public class GetterParameters1
        {
        }

        private delegate void InternalGetterCallback1();

        private delegate int InternalGetterValueFunction1();

        public delegate void GetterCallback1(GetterParameters1 parameters);

        public delegate int GetterValueFunction1(GetterParameters1 parameters);

        public class GetterSetup1
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public GetterSetup1(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public GetterSetup1 Callback(GetterCallback1 callback)
            {
                setup.Callback(new InternalGetterCallback1(
                    () => 
                    {
                        var __parameters__ = new GetterParameters1
                        {
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public GetterSetup1 Returns(GetterValueFunction1 valueFunction)
            {
                setup.Returns(new InternalGetterValueFunction1(
                    () => 
                    {
                        var __parameters__ = new GetterParameters1
                        {
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }
        }

        public GetterSetup1 Getter()
        {
            var __local__ = mock.Setup(mock => mock.Getter());
            return new GetterSetup1(__local__);
        }
    }
}

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

        public class GetterSetup1
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public GetterSetup1(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public GetterSetup1 Callback(Action<GetterParameters1> callback)
            {
                setup.Callback(
                    () => 
                    {
                        var parameters = new GetterParameters1
                        {
                        };
                        callback(parameters);
                    });
                return this;
            }

            public GetterSetup1 Returns(int value)
                => Returns(_ => value);

            public GetterSetup1 Returns(Func<GetterParameters1, int> valueFunction)
            {
                setup.Returns(
                    () => 
                    {
                        var parameters = new GetterParameters1
                        {
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public GetterSetup1 Getter()
        {
            var __setup__ = mock.Setup(mock => mock.Getter());
            return new GetterSetup1(__setup__);
        }
    }
}

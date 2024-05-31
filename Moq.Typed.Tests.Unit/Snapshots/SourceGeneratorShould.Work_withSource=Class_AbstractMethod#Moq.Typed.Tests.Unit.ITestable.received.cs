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
        public class PublicParameters
        {
        }

        public class PublicSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public PublicSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public PublicSetup Callback(Action<PublicParameters> callback)
            {
                setup.Callback(
                    () => 
                    {
                        var parameters = new PublicParameters
                        {
                        };
                        callback(parameters);
                    });
                return this;
            }
        }

        public PublicSetup Public()
        {
            var __setup__ = mock.Setup(mock => mock.Public());
            return new PublicSetup(__setup__);
        }
        public class InternalParameters
        {
            public readonly object someObject
        }

        public class InternalSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public InternalSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public InternalSetup Callback(Action<InternalParameters> callback)
            {
                setup.Callback<object>(
                    (someObject) => 
                    {
                        var parameters = new InternalParameters
                        {
                            someObject = someObject
                        };
                        callback(parameters);
                    });
                return this;
            }

            public InternalSetup Returns(int value)
                => Returns(_ => value);

            public InternalSetup Returns(Func<InternalParameters, int> valueFunction)
            {
                setup.Returns<object>(
                    (someObject) => 
                    {
                        var parameters = new InternalParameters
                        {
                            someObject = someObject
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public InternalSetup Internal(
            Func<object, bool>? someObject = null)
        {
            someObject ??= static _ => true;
            Expression<Func<object, bool>> someObjectExpression = argument => someObject(argument);
            var __setup__ = mock.Setup(mock => mock.Internal(
                It.Is(someObjectExpression)));
            return new InternalSetup(__setup__);
        }
    }
}

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
        public class FirstParameters
        {
            public object someObject { get; init; }
        }

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public FirstSetup Callback(Action<FirstParameters> callback)
            {
                setup.Callback<
                    object>(
                    (someObject) => 
                    {
                        var parameters = new FirstParameters
                        {
                            someObject = someObject
                        };
                        callback(parameters);
                    });
                return this;
            }

            public FirstSetup Returns(int value)
                => Returns(_ => value);

            public FirstSetup Returns(Func<FirstParameters, int> valueFunction)
            {
                setup.Returns<
                    object>(
                    (someObject) => 
                    {
                        var parameters = new FirstParameters
                        {
                                someObject = someObject
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public FirstSetup First(
            Func<object, bool>? someObject = null)
        {
            someObject ??= static _ => true;
            Expression<Func<object, bool>> someObjectExpression = argument => someObject(argument);
            var __setup__ = mock.Setup(mock => mock.First(
                It.Is(someObjectExpression)));
            return new FirstSetup(__setup__);
        }
    }
}

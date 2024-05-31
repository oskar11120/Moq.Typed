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
        public class IncrementParameters
        {
            public int number { get; init; }
        }

        public class IncrementSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public IncrementSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public IncrementSetup Callback(Action<IncrementParameters> callback)
            {
                setup.Callback<int>(
                    (number) => 
                    {
                        var parameters = new IncrementParameters
                        {
                                number = number
                        };
                        callback(parameters);
                    });
                return this;
            }

            public IncrementSetup Returns(int value)
                => Returns(_ => value);

            public IncrementSetup Returns(Func<IncrementParameters, int> valueFunction)
            {
                setup.Returns<int>(
                    (number) => 
                    {
                        var parameters = new IncrementParameters
                        {
                                number = number
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public IncrementSetup Increment(
            Func<int, bool>? number = null)
        {
            number ??= static _ => true;
            Expression<Func<int, bool>> numberExpression = argument => number(argument);
            var __setup__ = mock.Setup(mock => mock.Increment(
                It.Is(numberExpression)));
            return new IncrementSetup(__setup__);
        }
    }
}

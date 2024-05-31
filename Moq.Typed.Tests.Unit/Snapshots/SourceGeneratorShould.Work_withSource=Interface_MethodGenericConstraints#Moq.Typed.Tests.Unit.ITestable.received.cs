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
        public class MethodParameters<TInput>
        {
            public TInput Parameter { get; init; }
        }

        public class MethodSetup<TInput>
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public MethodSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public MethodSetup<TInput> Callback(Action<MethodParameters<TInput>> callback)
            {
                setup.Callback<TInput>(
                    (Parameter) => 
                    {
                        var parameters = new MethodParameters<TInput>
                        {
                                Parameter = Parameter
                        };
                        callback(parameters);
                    });
                return this;
            }

            public MethodSetup<TInput> Returns(int value)
                => Returns(_ => value);

            public MethodSetup<TInput> Returns(Func<MethodParameters<TInput>, int> valueFunction)
            {
                setup.Returns<TInput>(
                    (Parameter) => 
                    {
                        var parameters = new MethodParameters<TInput>
                        {
                                Parameter = Parameter
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public MethodSetup<TInput> Method<TInput>(
            Func<TInput, bool>? Parameter = null)
            where TInput : struct, System.Enum
        {
            Parameter ??= static _ => true;
            Expression<Func<TInput, bool>> ParameterExpression = argument => Parameter(argument);
            var __setup__ = mock.Setup(mock => mock.Method<TInput>(
                It.Is(ParameterExpression)));
            return new MethodSetup<TInput>(__setup__);
        }
    }
}

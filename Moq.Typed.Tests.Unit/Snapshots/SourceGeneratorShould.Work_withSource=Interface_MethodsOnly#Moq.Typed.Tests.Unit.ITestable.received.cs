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
        }

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public FirstSetup Callback(Action<FirstParameters> callback)
            {
                setup.Callback(
                    () => 
                    {
                        var parameters = new FirstParameters
                        {
                        };
                        callback(parameters);
                    });
                return this;
            }
        }

        public FirstSetup First()
        {
            var __setup__ = mock.Setup(mock => mock.First());
            return new FirstSetup(__setup__);
        }
        public class SecondParameters
        {
            public IEnumerable<int> someInts { get; init; }
        }

        public class SecondSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public SecondSetup Callback(Action<SecondParameters> callback)
            {
                setup.Callback<IEnumerable<int>>(
                    (someInts) => 
                    {
                        var parameters = new SecondParameters
                        {
                                someInts = someInts
                        };
                        callback(parameters);
                    });
                return this;
            }

            public SecondSetup Returns(int value)
                => Returns(_ => value);

            public SecondSetup Returns(Func<SecondParameters, int> valueFunction)
            {
                setup.Returns<IEnumerable<int>>(
                    (someInts) => 
                    {
                        var parameters = new SecondParameters
                        {
                                someInts = someInts
                        };
                        return valueFunction(parameters);
                    });
                return this;
            }
        }

        public SecondSetup Second(
            Func<IEnumerable<int>, bool>? someInts = null)
        {
            someInts ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> someIntsExpression = argument => someInts(argument);
            var __setup__ = mock.Setup(mock => mock.Second(
                It.Is(someIntsExpression)));
            return new SecondSetup(__setup__);
        }
        public class ThirdParameters
        {
            public IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters { get; init; }
            public Moq.Typed.Tests.Unit.Parameter oneMoreParameter { get; init; }
            public int someInt { get; init; }
        }

        public class ThirdSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public ThirdSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public ThirdSetup Callback(Action<ThirdParameters> callback)
            {
                setup.Callback<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, Moq.Typed.Tests.Unit.Parameter, int>(
                    (someParameters, oneMoreParameter, someInt) => 
                    {
                        var parameters = new ThirdParameters
                        {
                                someParameters = someParameters
                                oneMoreParameter = oneMoreParameter
                                someInt = someInt
                        };
                        callback(parameters);
                    });
                return this;
            }
        }

        public ThirdSetup Third(
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>? someParameters = null, 
            Func<Moq.Typed.Tests.Unit.Parameter, bool>? oneMoreParameter = null, 
            Func<int, bool>? someInt = null)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            var __setup__ = mock.Setup(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(oneMoreParameterExpression), 
                It.Is(someIntExpression)));
            return new ThirdSetup(__setup__);
        }
    }
}

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
        public static TypedMockFor_ITestable Setup(this Mock<Moq.Typed.Tests.Unit.ITestable> mock)
            => new TypedMockFor_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockFor_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable> mock;

        public TypedMockFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            this.mock = mock;
        }

        public class FirstParameters
        {
        }

        private delegate void InternalFirstCallback();

        public delegate void FirstCallback(FirstParameters parameters);

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
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
        }

        public FirstSetup First()
        {
            var __setup__ = mock.Setup(mock => mock.First());
            return new FirstSetup(__setup__);
        }

        public class SecondParameters
        {
            public IEnumerable<int> someInts;
        }

        private delegate void InternalSecondCallback(
            IEnumerable<int> someInts);

        private delegate int InternalSecondValueFunction(
            IEnumerable<int> someInts);

        public delegate void SecondCallback(SecondParameters parameters);

        public delegate int SecondValueFunction(SecondParameters parameters);

        public class SecondSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public SecondSetup Callback(SecondCallback callback)
            {
                setup.Callback(new InternalSecondCallback(
                    (IEnumerable<int> someInts) => 
                    {
                        var __parameters__ = new SecondParameters
                        {
                            someInts = someInts
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public SecondSetup Returns(SecondValueFunction valueFunction)
            {
                setup.Returns(new InternalSecondValueFunction(
                    (IEnumerable<int> someInts) => 
                    {
                        var __parameters__ = new SecondParameters
                        {
                            someInts = someInts
                        };
                        return valueFunction(__parameters__);
                    }));
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
            public IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters;
            public Moq.Typed.Tests.Unit.Parameter oneMoreParameter;
            public int someInt;
        }

        private delegate void InternalThirdCallback(
            IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
            Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
            int someInt);

        public delegate void ThirdCallback(ThirdParameters parameters);

        public class ThirdSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public ThirdSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public ThirdSetup Callback(ThirdCallback callback)
            {
                setup.Callback(new InternalThirdCallback(
                    (IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, Moq.Typed.Tests.Unit.Parameter oneMoreParameter, int someInt) => 
                    {
                        var __parameters__ = new ThirdParameters
                        {
                            someParameters = someParameters, 
                            oneMoreParameter = oneMoreParameter, 
                            someInt = someInt
                        };
                        callback(__parameters__);
                    }));
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

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

        public class PublicParameters
        {
        }

        private delegate void InternalPublicCallback();

        public delegate void PublicCallback(PublicParameters parameters);

        public class PublicSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

            public PublicSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
            {
                this.setup = setup;
            }

            public PublicSetup Callback(PublicCallback callback)
            {
                setup.Callback(new InternalPublicCallback(
                    () => 
                    {
                        var __parameters__ = new PublicParameters
                        {
                        };
                        callback(__parameters__);
                    }));
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
            public object someObject;
        }

        private delegate void InternalInternalCallback(
            object someObject);

        private delegate int InternalInternalValueFunction(
            object someObject);

        public delegate void InternalCallback(InternalParameters parameters);

        public delegate int InternalValueFunction(InternalParameters parameters);

        public class InternalSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public InternalSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public InternalSetup Callback(InternalCallback callback)
            {
                setup.Callback(new InternalInternalCallback(
                    (object someObject) => 
                    {
                        var __parameters__ = new InternalParameters
                        {
                            someObject = someObject
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public InternalSetup Returns(InternalValueFunction valueFunction)
            {
                setup.Returns(new InternalInternalValueFunction(
                    (object someObject) => 
                    {
                        var __parameters__ = new InternalParameters
                        {
                            someObject = someObject
                        };
                        return valueFunction(__parameters__);
                    }));
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

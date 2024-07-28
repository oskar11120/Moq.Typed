//HintName: Moq.Typed.Tests.Unit.ITestable.cs
using Moq;
using Moq.Language.Flow;
using System;
using System.CodeDom.Compiler;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        #nullable disable warnings
        public class GetterParameters1
        {
        }
        #nullable enable warnings

        private delegate void InternalGetterCallback1();

        private delegate int InternalGetterValueFunction1();

        private delegate TException InternalGetterExceptionFunction1<TException>();

        public delegate void GetterCallback1(GetterParameters1 parameters);

        public delegate int GetterValueFunction1(GetterParameters1 parameters);

        public delegate TException GetterExceptionFunction1<TException>(GetterParameters1 parameters);

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

            public GetterSetup1 Returns(int value)
                => Returns(_ => value);

            public GetterSetup1 Throws<TException>(GetterExceptionFunction1<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalGetterExceptionFunction1<TException>(
                    () => 
                    {
                        var __parameters__ = new GetterParameters1
                        {
                        };
                        return exceptionFunction(__parameters__);
                    }));
                return this;
            }

            public GetterSetup1 Throws(Exception exception)
            {
                setup.Throws(exception);
                return this;
            }

            public GetterSetup1 Throws<TException>() where TException : Exception, new()
            {
                setup.Throws<TException>();
                return this;
            }

            public GetterSetup1 Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
            {
                setup.Throws<TException>(exceptionFunction);
                return this;
            }
        }

        public GetterSetup1 Getter1()
        {
            var __local__ = mock.Setup(mock => mock.Getter());
            return new GetterSetup1(__local__);
        }
    }

    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockVerifyExtensionFor_ITestable
    {
        public static TypedMockVerifyFor_ITestable Verifyy(this Mock<Moq.Typed.Tests.Unit.ITestable> mock)
            => new TypedMockVerifyFor_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockVerifyFor_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.ITestable> mock;

        public TypedMockVerifyFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            this.mock = mock;
        }

        public void Getter(Times times = default(Times)!)
        {
            mock.Verify(mock => mock.Getter, times);
        }

        public void Mutable(Times times = default(Times)!)
        {
            mock.Verify(mock => mock.Mutable, times);
        }

        public void InitOnly(Times times = default(Times)!)
        {
            mock.Verify(mock => mock.InitOnly, times);
        }

        public void Getter1(
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.Getter(),
                times);
        }
    }
}

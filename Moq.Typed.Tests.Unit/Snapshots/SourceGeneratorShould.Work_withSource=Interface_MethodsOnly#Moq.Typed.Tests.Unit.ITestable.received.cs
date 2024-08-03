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
        internal readonly Mock<Moq.Typed.Tests.Unit.ITestable> Mock;

        public TypedMockSetupFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            Mock = mock;
        }
    }

    #nullable disable warnings
    public class TypedMockSetupFor_ITestable_FirstParameters
    {
    }
    #nullable enable warnings

    private delegate void InternalTypedMockSetupFor_ITestable_FirstCallback();

    private delegate TException InternalTypedMockSetupFor_ITestable_FirstExceptionFunction<TException>();

    public delegate void TypedMockSetupFor_ITestable_FirstCallback(TypedMockSetupFor_ITestable_FirstParameters parameters);

    public delegate TException TypedMockSetupFor_ITestable_FirstExceptionFunction<TException>(TypedMockSetupFor_ITestable_FirstParameters parameters);

    public class TypedMockSetupFor_ITestable_FirstSetup
    {
        private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

        public TypedMockSetupFor_ITestable_FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
        {
            this.setup = setup;
        }

        public TypedMockSetupFor_ITestable_FirstSetup Callback(TypedMockSetupFor_ITestable_FirstCallback callback)
        {
            setup.Callback(new InternalTypedMockSetupFor_ITestable_FirstCallback(
                () => 
                {
                    var __parameters__ = new TypedMockSetupFor_ITestable_FirstParameters
                    {
                    };
                    callback(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_ITestable_FirstSetup Throws<TException>(TypedMockSetupFor_ITestable_FirstExceptionFunction<TException> exceptionFunction) where TException : Exception
        {
            setup.Throws(new InternalTypedMockSetupFor_ITestable_FirstExceptionFunction<TException>(
                () => 
                {
                    var __parameters__ = new TypedMockSetupFor_ITestable_FirstParameters
                    {
                    };
                    return exceptionFunction(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_ITestable_FirstSetup Throws(Exception exception)
        {
            setup.Throws(exception);
            return this;
        }

        public TypedMockSetupFor_ITestable_FirstSetup Throws<TException>() where TException : Exception, new()
        {
            setup.Throws<TException>();
            return this;
        }

        public TypedMockSetupFor_ITestable_FirstSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
        {
            setup.Throws<TException>(exceptionFunction);
            return this;
        }
    }

    public static class TypedMockSetupFor_ITestable_FirstExtension
    {
        public TypedMockSetupFor_ITestable_FirstSetup First(
            this TypedMockSetupFor_ITestable __self__,)
        {
            var __local__ = __self__.Mock.Setup(mock => mock.First());
            return new TypedMockSetupFor_ITestable_FirstSetup(__local__);
        }
    }

    #nullable disable warnings
    public class TypedMockSetupFor_ITestable_SecondParameters
    {
        public IEnumerable<int> someInts;
    }
    #nullable enable warnings

    private delegate void InternalTypedMockSetupFor_ITestable_SecondCallback(
        IEnumerable<int> someInts);

    private delegate int InternalTypedMockSetupFor_ITestable_SecondValueFunction(
        IEnumerable<int> someInts);

    private delegate TException InternalTypedMockSetupFor_ITestable_SecondExceptionFunction<TException>(
        IEnumerable<int> someInts);

    public delegate void TypedMockSetupFor_ITestable_SecondCallback(TypedMockSetupFor_ITestable_SecondParameters parameters);

    public delegate int TypedMockSetupFor_ITestable_SecondValueFunction(TypedMockSetupFor_ITestable_SecondParameters parameters);

    public delegate TException TypedMockSetupFor_ITestable_SecondExceptionFunction<TException>(TypedMockSetupFor_ITestable_SecondParameters parameters);

    public class TypedMockSetupFor_ITestable_SecondSetup
    {
        private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

        public TypedMockSetupFor_ITestable_SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
        {
            this.setup = setup;
        }

        public TypedMockSetupFor_ITestable_SecondSetup Callback(TypedMockSetupFor_ITestable_SecondCallback callback)
        {
            setup.Callback(new InternalTypedMockSetupFor_ITestable_SecondCallback(
                (IEnumerable<int> someInts) => 
                {
                    var __parameters__ = new TypedMockSetupFor_ITestable_SecondParameters
                    {
                        someInts = someInts
                    };
                    callback(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_ITestable_SecondSetup Returns(TypedMockSetupFor_ITestable_SecondValueFunction valueFunction)
        {
            setup.Returns(new InternalTypedMockSetupFor_ITestable_SecondValueFunction(
                (IEnumerable<int> someInts) => 
                {
                    var __parameters__ = new TypedMockSetupFor_ITestable_SecondParameters
                    {
                        someInts = someInts
                    };
                    return valueFunction(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_ITestable_SecondSetup Returns(int value)
            => Returns(_ => value);

        public TypedMockSetupFor_ITestable_SecondSetup Throws<TException>(TypedMockSetupFor_ITestable_SecondExceptionFunction<TException> exceptionFunction) where TException : Exception
        {
            setup.Throws(new InternalTypedMockSetupFor_ITestable_SecondExceptionFunction<TException>(
                (IEnumerable<int> someInts) => 
                {
                    var __parameters__ = new TypedMockSetupFor_ITestable_SecondParameters
                    {
                        someInts = someInts
                    };
                    return exceptionFunction(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_ITestable_SecondSetup Throws(Exception exception)
        {
            setup.Throws(exception);
            return this;
        }

        public TypedMockSetupFor_ITestable_SecondSetup Throws<TException>() where TException : Exception, new()
        {
            setup.Throws<TException>();
            return this;
        }

        public TypedMockSetupFor_ITestable_SecondSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
        {
            setup.Throws<TException>(exceptionFunction);
            return this;
        }
    }

    public static class TypedMockSetupFor_ITestable_SecondExtension
    {
        public TypedMockSetupFor_ITestable_SecondSetup Second(
            this TypedMockSetupFor_ITestable __self__,)
        {
            var __local__ = __self__.Mock.Setup(mock => mock.Second());
            return new TypedMockSetupFor_ITestable_SecondSetup(__local__);
        }
    }

    public static class TypedMockSetupFor_ITestable_SecondExtension1
    {
        public TypedMockSetupFor_ITestable_SecondSetup Second(
            this TypedMockSetupFor_ITestable __self__,
            Func<IEnumerable<int>, bool> someInts)
        {
            someInts ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> someIntsExpression = argument => someInts(argument);
            var __local__ = __self__.Mock.Setup(mock => mock.Second(
                It.Is(someIntsExpression)));
            return new TypedMockSetupFor_ITestable_SecondSetup(__local__);
        }
    }

    #nullable disable warnings
    public class TypedMockSetupFor_ITestable_ThirdParameters
    {
        public IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters;
        public Moq.Typed.Tests.Unit.Parameter oneMoreParameter;
        public int someInt;
    }
    #nullable enable warnings

    private delegate void InternalTypedMockSetupFor_ITestable_ThirdCallback(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        int someInt);

    private delegate TException InternalTypedMockSetupFor_ITestable_ThirdExceptionFunction<TException>(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        int someInt);

    public delegate void TypedMockSetupFor_ITestable_ThirdCallback(TypedMockSetupFor_ITestable_ThirdParameters parameters);

    public delegate TException TypedMockSetupFor_ITestable_ThirdExceptionFunction<TException>(TypedMockSetupFor_ITestable_ThirdParameters parameters);

    public class TypedMockSetupFor_ITestable_ThirdSetup
    {
        private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

        public TypedMockSetupFor_ITestable_ThirdSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
        {
            this.setup = setup;
        }

        public TypedMockSetupFor_ITestable_ThirdSetup Callback(TypedMockSetupFor_ITestable_ThirdCallback callback)
        {
            setup.Callback(new InternalTypedMockSetupFor_ITestable_ThirdCallback(
                (IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, Moq.Typed.Tests.Unit.Parameter oneMoreParameter, int someInt) => 
                {
                    var __parameters__ = new TypedMockSetupFor_ITestable_ThirdParameters
                    {
                        someParameters = someParameters, 
                        oneMoreParameter = oneMoreParameter, 
                        someInt = someInt
                    };
                    callback(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_ITestable_ThirdSetup Throws<TException>(TypedMockSetupFor_ITestable_ThirdExceptionFunction<TException> exceptionFunction) where TException : Exception
        {
            setup.Throws(new InternalTypedMockSetupFor_ITestable_ThirdExceptionFunction<TException>(
                (IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, Moq.Typed.Tests.Unit.Parameter oneMoreParameter, int someInt) => 
                {
                    var __parameters__ = new TypedMockSetupFor_ITestable_ThirdParameters
                    {
                        someParameters = someParameters, 
                        oneMoreParameter = oneMoreParameter, 
                        someInt = someInt
                    };
                    return exceptionFunction(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_ITestable_ThirdSetup Throws(Exception exception)
        {
            setup.Throws(exception);
            return this;
        }

        public TypedMockSetupFor_ITestable_ThirdSetup Throws<TException>() where TException : Exception, new()
        {
            setup.Throws<TException>();
            return this;
        }

        public TypedMockSetupFor_ITestable_ThirdSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
        {
            setup.Throws<TException>(exceptionFunction);
            return this;
        }
    }

    public static class TypedMockSetupFor_ITestable_ThirdExtension
    {
        public TypedMockSetupFor_ITestable_ThirdSetup Third(
            this TypedMockSetupFor_ITestable __self__,)
        {
            var __local__ = __self__.Mock.Setup(mock => mock.Third());
            return new TypedMockSetupFor_ITestable_ThirdSetup(__local__);
        }
    }

    public static class TypedMockSetupFor_ITestable_ThirdExtension1
    {
        public TypedMockSetupFor_ITestable_ThirdSetup Third(
            this TypedMockSetupFor_ITestable __self__,
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            var __local__ = __self__.Mock.Setup(mock => mock.Third(
                It.Is(someParametersExpression)));
            return new TypedMockSetupFor_ITestable_ThirdSetup(__local__);
        }
    }

    public static class TypedMockSetupFor_ITestable_ThirdExtension2
    {
        public TypedMockSetupFor_ITestable_ThirdSetup Third(
            this TypedMockSetupFor_ITestable __self__,
            Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter)
        {
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            var __local__ = __self__.Mock.Setup(mock => mock.Third(
                It.Is(oneMoreParameterExpression)));
            return new TypedMockSetupFor_ITestable_ThirdSetup(__local__);
        }
    }

    public static class TypedMockSetupFor_ITestable_ThirdExtension3
    {
        public TypedMockSetupFor_ITestable_ThirdSetup Third(
            this TypedMockSetupFor_ITestable __self__,
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
            Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            var __local__ = __self__.Mock.Setup(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(oneMoreParameterExpression)));
            return new TypedMockSetupFor_ITestable_ThirdSetup(__local__);
        }
    }

    public static class TypedMockSetupFor_ITestable_ThirdExtension4
    {
        public TypedMockSetupFor_ITestable_ThirdSetup Third(
            this TypedMockSetupFor_ITestable __self__,
            Func<int, bool> someInt)
        {
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            var __local__ = __self__.Mock.Setup(mock => mock.Third(
                It.Is(someIntExpression)));
            return new TypedMockSetupFor_ITestable_ThirdSetup(__local__);
        }
    }

    public static class TypedMockSetupFor_ITestable_ThirdExtension5
    {
        public TypedMockSetupFor_ITestable_ThirdSetup Third(
            this TypedMockSetupFor_ITestable __self__,
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
            Func<int, bool> someInt)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            var __local__ = __self__.Mock.Setup(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(someIntExpression)));
            return new TypedMockSetupFor_ITestable_ThirdSetup(__local__);
        }
    }

    public static class TypedMockSetupFor_ITestable_ThirdExtension6
    {
        public TypedMockSetupFor_ITestable_ThirdSetup Third(
            this TypedMockSetupFor_ITestable __self__,
            Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
            Func<int, bool> someInt)
        {
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            var __local__ = __self__.Mock.Setup(mock => mock.Third(
                It.Is(oneMoreParameterExpression), 
                It.Is(someIntExpression)));
            return new TypedMockSetupFor_ITestable_ThirdSetup(__local__);
        }
    }

    public static class TypedMockSetupFor_ITestable_ThirdExtension7
    {
        public TypedMockSetupFor_ITestable_ThirdSetup Third(
            this TypedMockSetupFor_ITestable __self__,
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
            Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
            Func<int, bool> someInt)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            var __local__ = __self__.Mock.Setup(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(oneMoreParameterExpression), 
                It.Is(someIntExpression)));
            return new TypedMockSetupFor_ITestable_ThirdSetup(__local__);
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
        internal readonly Mock<Moq.Typed.Tests.Unit.ITestable> Mock;

        public TypedMockVerifyFor_ITestable(Mock<Moq.Typed.Tests.Unit.ITestable> mock)
        {
            Mock = mock;
        }
    }

    public static class TypedMockVerifyFor_ITestable_FirstExtension
    {
        public void First(
            this TypedMockVerifyFor_ITestable __self__,
            Times times = default(Times)!)
        {
            __self__.Mock.Verify(mock => mock.First(),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_SecondExtension
    {
        public void Second(
            this TypedMockVerifyFor_ITestable __self__,
            Times times = default(Times)!)
        {
            __self__.Mock.Verify(mock => mock.Second(),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_SecondExtension1
    {
        public void Second(
            this TypedMockVerifyFor_ITestable __self__,
            Func<IEnumerable<int>, bool> someInts,
            Times times = default(Times)!)
        {
            someInts ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> someIntsExpression = argument => someInts(argument);
            __self__.Mock.Verify(mock => mock.Second(
                It.Is(someIntsExpression)),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_ThirdExtension
    {
        public void Third(
            this TypedMockVerifyFor_ITestable __self__,
            Times times = default(Times)!)
        {
            __self__.Mock.Verify(mock => mock.Third(),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_ThirdExtension1
    {
        public void Third(
            this TypedMockVerifyFor_ITestable __self__,
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters,
            Times times = default(Times)!)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            __self__.Mock.Verify(mock => mock.Third(
                It.Is(someParametersExpression)),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_ThirdExtension2
    {
        public void Third(
            this TypedMockVerifyFor_ITestable __self__,
            Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter,
            Times times = default(Times)!)
        {
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            __self__.Mock.Verify(mock => mock.Third(
                It.Is(oneMoreParameterExpression)),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_ThirdExtension3
    {
        public void Third(
            this TypedMockVerifyFor_ITestable __self__,
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
            Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter,
            Times times = default(Times)!)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            __self__.Mock.Verify(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(oneMoreParameterExpression)),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_ThirdExtension4
    {
        public void Third(
            this TypedMockVerifyFor_ITestable __self__,
            Func<int, bool> someInt,
            Times times = default(Times)!)
        {
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            __self__.Mock.Verify(mock => mock.Third(
                It.Is(someIntExpression)),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_ThirdExtension5
    {
        public void Third(
            this TypedMockVerifyFor_ITestable __self__,
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
            Func<int, bool> someInt,
            Times times = default(Times)!)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            __self__.Mock.Verify(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(someIntExpression)),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_ThirdExtension6
    {
        public void Third(
            this TypedMockVerifyFor_ITestable __self__,
            Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
            Func<int, bool> someInt,
            Times times = default(Times)!)
        {
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            __self__.Mock.Verify(mock => mock.Third(
                It.Is(oneMoreParameterExpression), 
                It.Is(someIntExpression)),
                times);
        }
    }

    public static class TypedMockVerifyFor_ITestable_ThirdExtension7
    {
        public void Third(
            this TypedMockVerifyFor_ITestable __self__,
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
            Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
            Func<int, bool> someInt,
            Times times = default(Times)!)
        {
            someParameters ??= static _ => true;
            Expression<Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>> someParametersExpression = argument => someParameters(argument);
            oneMoreParameter ??= static _ => true;
            Expression<Func<Moq.Typed.Tests.Unit.Parameter, bool>> oneMoreParameterExpression = argument => oneMoreParameter(argument);
            someInt ??= static _ => true;
            Expression<Func<int, bool>> someIntExpression = argument => someInt(argument);
            __self__.Mock.Verify(mock => mock.Third(
                It.Is(someParametersExpression), 
                It.Is(oneMoreParameterExpression), 
                It.Is(someIntExpression)),
                times);
        }
    }
}

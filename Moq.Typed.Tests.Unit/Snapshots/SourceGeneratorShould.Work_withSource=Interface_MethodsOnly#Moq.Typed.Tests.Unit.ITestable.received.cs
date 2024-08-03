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

    public class FirstSetup
    {
        private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

        public FirstSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
        {
            this.setup = setup;
        }

        public FirstSetup Callback(TypedMockSetupFor_ITestable_FirstCallback callback)
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

        public FirstSetup Throws<TException>(TypedMockSetupFor_ITestable_FirstExceptionFunction<TException> exceptionFunction) where TException : Exception
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

        public FirstSetup Throws(Exception exception)
        {
            setup.Throws(exception);
            return this;
        }

        public FirstSetup Throws<TException>() where TException : Exception, new()
        {
            setup.Throws<TException>();
            return this;
        }

        public FirstSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
        {
            setup.Throws<TException>(exceptionFunction);
            return this;
        }
    }

    public FirstSetup First()
    {
        var __local__ = mock.Setup(mock => mock.First());
        return new FirstSetup(__local__);
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

    public class SecondSetup
    {
        private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

        public SecondSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
        {
            this.setup = setup;
        }

        public SecondSetup Callback(TypedMockSetupFor_ITestable_SecondCallback callback)
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

        public SecondSetup Returns(TypedMockSetupFor_ITestable_SecondValueFunction valueFunction)
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

        public SecondSetup Returns(int value)
            => Returns(_ => value);

        public SecondSetup Throws<TException>(TypedMockSetupFor_ITestable_SecondExceptionFunction<TException> exceptionFunction) where TException : Exception
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

        public SecondSetup Throws(Exception exception)
        {
            setup.Throws(exception);
            return this;
        }

        public SecondSetup Throws<TException>() where TException : Exception, new()
        {
            setup.Throws<TException>();
            return this;
        }

        public SecondSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
        {
            setup.Throws<TException>(exceptionFunction);
            return this;
        }
    }

    public SecondSetup Second(
        Func<IEnumerable<int>, bool> someInts)
    {
        someInts ??= static _ => true;
        Expression<Func<IEnumerable<int>, bool>> someIntsExpression = argument => someInts(argument);
        var __local__ = mock.Setup(mock => mock.Second(
            It.Is(someIntsExpression)));
        return new SecondSetup(__local__);
    }

    public SecondSetup Second()
        => Second(
            someInts: static _ => true);

    public SecondSetup Second(
        IEnumerable<int> someInts)
        => Second(
            someInts: __local__ => Equals(__local__, someInts));

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

    public class ThirdSetup
    {
        private readonly ISetup<Moq.Typed.Tests.Unit.ITestable> setup;

        public ThirdSetup(ISetup<Moq.Typed.Tests.Unit.ITestable> setup)
        {
            this.setup = setup;
        }

        public ThirdSetup Callback(TypedMockSetupFor_ITestable_ThirdCallback callback)
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

        public ThirdSetup Throws<TException>(TypedMockSetupFor_ITestable_ThirdExceptionFunction<TException> exceptionFunction) where TException : Exception
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

        public ThirdSetup Throws(Exception exception)
        {
            setup.Throws(exception);
            return this;
        }

        public ThirdSetup Throws<TException>() where TException : Exception, new()
        {
            setup.Throws<TException>();
            return this;
        }

        public ThirdSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
        {
            setup.Throws<TException>(exceptionFunction);
            return this;
        }
    }

    public ThirdSetup Third(
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
        var __local__ = mock.Setup(mock => mock.Third(
            It.Is(someParametersExpression), 
            It.Is(oneMoreParameterExpression), 
            It.Is(someIntExpression)));
        return new ThirdSetup(__local__);
    }

    public ThirdSetup Third()
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: static _ => true, 
            someInt: static _ => true);

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: static _ => true, 
            someInt: static _ => true);

    public ThirdSetup Third(
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: static _ => true);

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: oneMoreParameter, 
            someInt: static _ => true);

    public ThirdSetup Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: static _ => true);

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: static _ => true);

    public ThirdSetup Third(
        int someInt)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: static _ => true, 
            someInt: __local__ => Equals(__local__, someInt));

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Func<int, bool> someInt)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: static _ => true, 
            someInt: someInt);

    public ThirdSetup Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        int someInt)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: static _ => true, 
            someInt: __local__ => Equals(__local__, someInt));

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        int someInt)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: static _ => true, 
            someInt: __local__ => Equals(__local__, someInt));

    public ThirdSetup Third(
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        Func<int, bool> someInt)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: someInt);

    public ThirdSetup Third(
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
        int someInt)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: oneMoreParameter, 
            someInt: __local__ => Equals(__local__, someInt));

    public ThirdSetup Third(
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        int someInt)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: __local__ => Equals(__local__, someInt));

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
        Func<int, bool> someInt)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: oneMoreParameter, 
            someInt: someInt);

    public ThirdSetup Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        Func<int, bool> someInt)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: someInt);

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        Func<int, bool> someInt)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: someInt);

    public ThirdSetup Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
        int someInt)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: oneMoreParameter, 
            someInt: __local__ => Equals(__local__, someInt));

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
        int someInt)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: oneMoreParameter, 
            someInt: __local__ => Equals(__local__, someInt));

    public ThirdSetup Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        int someInt)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: __local__ => Equals(__local__, someInt));

    public ThirdSetup Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        int someInt)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: __local__ => Equals(__local__, someInt));

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
    }

    public void First(
        Times times = default(Times)!)
    {
        mock.Verify(mock => mock.First(),
            times);
    }

    public void Second(
        Func<IEnumerable<int>, bool> someInts,
        Times times = default(Times)!)
    {
        someInts ??= static _ => true;
        Expression<Func<IEnumerable<int>, bool>> someIntsExpression = argument => someInts(argument);
        mock.Verify(mock => mock.Second(
            It.Is(someIntsExpression)),
            times);
    }

    public void Second(,
        Times times = default(Times)!)
        => Second(
            someInts: static _ => true,
            times);

    public void Second(
        IEnumerable<int> someInts,
        Times times = default(Times)!)
        => Second(
            someInts: __local__ => Equals(__local__, someInts),
            times);

    public void Third(
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
        mock.Verify(mock => mock.Third(
            It.Is(someParametersExpression), 
            It.Is(oneMoreParameterExpression), 
            It.Is(someIntExpression)),
            times);
    }

    public void Third(,
        Times times = default(Times)!)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: static _ => true, 
            someInt: static _ => true,
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: static _ => true, 
            someInt: static _ => true,
            times);

    public void Third(
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter,
        Times times = default(Times)!)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: static _ => true,
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: oneMoreParameter, 
            someInt: static _ => true,
            times);

    public void Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter,
        Times times = default(Times)!)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: static _ => true,
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: static _ => true,
            times);

    public void Third(
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: static _ => true, 
            someInt: __local__ => Equals(__local__, someInt),
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Func<int, bool> someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: static _ => true, 
            someInt: someInt,
            times);

    public void Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: static _ => true, 
            someInt: __local__ => Equals(__local__, someInt),
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: static _ => true, 
            someInt: __local__ => Equals(__local__, someInt),
            times);

    public void Third(
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        Func<int, bool> someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: someInt,
            times);

    public void Third(
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: oneMoreParameter, 
            someInt: __local__ => Equals(__local__, someInt),
            times);

    public void Third(
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: static _ => true, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: __local__ => Equals(__local__, someInt),
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
        Func<int, bool> someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: oneMoreParameter, 
            someInt: someInt,
            times);

    public void Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        Func<int, bool> someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: someInt,
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        Func<int, bool> someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: someInt,
            times);

    public void Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: oneMoreParameter, 
            someInt: __local__ => Equals(__local__, someInt),
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Func<Moq.Typed.Tests.Unit.Parameter, bool> oneMoreParameter, 
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: oneMoreParameter, 
            someInt: __local__ => Equals(__local__, someInt),
            times);

    public void Third(
        Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: someParameters, 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: __local__ => Equals(__local__, someInt),
            times);

    public void Third(
        IEnumerable<Moq.Typed.Tests.Unit.Parameter> someParameters, 
        Moq.Typed.Tests.Unit.Parameter oneMoreParameter, 
        int someInt,
        Times times = default(Times)!)
        => Third(
            someParameters: __local__ => Equals(__local__, someParameters), 
            oneMoreParameter: __local__ => Equals(__local__, oneMoreParameter), 
            someInt: __local__ => Equals(__local__, someInt),
            times);
}

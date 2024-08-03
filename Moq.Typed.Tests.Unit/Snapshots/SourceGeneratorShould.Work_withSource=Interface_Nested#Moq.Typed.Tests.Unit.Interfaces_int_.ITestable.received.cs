//HintName: Moq.Typed.Tests.Unit.Interfaces_int_.ITestable.cs
using Moq;
using Moq.Language.Flow;
using System;
using System.CodeDom.Compiler;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Moq.Typed.Tests.Unit
{

    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockSetupExtensionFor_Interfaces_ITestable
    {
        public static TypedMockSetupFor_Interfaces_ITestable Setup(this Mock<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable> mock)
            => new TypedMockSetupFor_Interfaces_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockSetupFor_Interfaces_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable> mock;

        public TypedMockSetupFor_Interfaces_ITestable(Mock<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable> mock)
        {
            this.mock = mock;
        }
    }

    #nullable disable warnings
    public class TypedMockSetupFor_Interfaces_ITestable_FirstParameters
    {
        public IEnumerable<int> values;
    }
    #nullable enable warnings

    private delegate void InternalTypedMockSetupFor_Interfaces_ITestable_FirstCallback(
        IEnumerable<int> values);

    private delegate int InternalTypedMockSetupFor_Interfaces_ITestable_FirstValueFunction(
        IEnumerable<int> values);

    private delegate TException InternalTypedMockSetupFor_Interfaces_ITestable_FirstExceptionFunction<TException>(
        IEnumerable<int> values);

    public delegate void TypedMockSetupFor_Interfaces_ITestable_FirstCallback(TypedMockSetupFor_Interfaces_ITestable_FirstParameters parameters);

    public delegate int TypedMockSetupFor_Interfaces_ITestable_FirstValueFunction(TypedMockSetupFor_Interfaces_ITestable_FirstParameters parameters);

    public delegate TException TypedMockSetupFor_Interfaces_ITestable_FirstExceptionFunction<TException>(TypedMockSetupFor_Interfaces_ITestable_FirstParameters parameters);

    public class TypedMockSetupFor_Interfaces_ITestable_FirstSetup
    {
        private readonly ISetup<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable, int> setup;

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup(ISetup<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable, int> setup)
        {
            this.setup = setup;
        }

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup Callback(TypedMockSetupFor_Interfaces_ITestable_FirstCallback callback)
        {
            setup.Callback(new InternalTypedMockSetupFor_Interfaces_ITestable_FirstCallback(
                (IEnumerable<int> values) => 
                {
                    var __parameters__ = new TypedMockSetupFor_Interfaces_ITestable_FirstParameters
                    {
                        values = values
                    };
                    callback(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup Returns(TypedMockSetupFor_Interfaces_ITestable_FirstValueFunction valueFunction)
        {
            setup.Returns(new InternalTypedMockSetupFor_Interfaces_ITestable_FirstValueFunction(
                (IEnumerable<int> values) => 
                {
                    var __parameters__ = new TypedMockSetupFor_Interfaces_ITestable_FirstParameters
                    {
                        values = values
                    };
                    return valueFunction(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup Returns(int value)
            => Returns(_ => value);

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup Throws<TException>(TypedMockSetupFor_Interfaces_ITestable_FirstExceptionFunction<TException> exceptionFunction) where TException : Exception
        {
            setup.Throws(new InternalTypedMockSetupFor_Interfaces_ITestable_FirstExceptionFunction<TException>(
                (IEnumerable<int> values) => 
                {
                    var __parameters__ = new TypedMockSetupFor_Interfaces_ITestable_FirstParameters
                    {
                        values = values
                    };
                    return exceptionFunction(__parameters__);
                }));
            return this;
        }

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup Throws(Exception exception)
        {
            setup.Throws(exception);
            return this;
        }

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup Throws<TException>() where TException : Exception, new()
        {
            setup.Throws<TException>();
            return this;
        }

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup Throws<TException>(Func<TException> exceptionFunction) where TException : Exception, new()
        {
            setup.Throws<TException>(exceptionFunction);
            return this;
        }
    }
    public static class TypedMockSetupFor_Interfaces_ITestable_FirstExtension
    {

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup First()
        {
            var __local__ = mock.Setup(mock => mock.First());
            return new TypedMockSetupFor_Interfaces_ITestable_FirstSetup(__local__);
        }
    }
    public static class TypedMockSetupFor_Interfaces_ITestable_FirstExtension
    {

        public TypedMockSetupFor_Interfaces_ITestable_FirstSetup First(
            Func<IEnumerable<int>, bool> values)
        {
            values ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> valuesExpression = argument => values(argument);
            var __local__ = mock.Setup(mock => mock.First(
                It.Is(valuesExpression)));
            return new TypedMockSetupFor_Interfaces_ITestable_FirstSetup(__local__);
        }
    }

    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockVerifyExtensionFor_Interfaces_ITestable
    {
        public static TypedMockVerifyFor_Interfaces_ITestable Verifyy(this Mock<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable> mock)
            => new TypedMockVerifyFor_Interfaces_ITestable(mock);
    }

    [GeneratedCode("Moq.Typed", null)]
    internal sealed class TypedMockVerifyFor_Interfaces_ITestable
    {
        private readonly Mock<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable> mock;

        public TypedMockVerifyFor_Interfaces_ITestable(Mock<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable> mock)
        {
            this.mock = mock;
        }
    }
    public static class TypedMockVerifyFor_Interfaces_ITestable_FirstExtension
    {

        public void First(
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.First(),
                times);
        }
    }
    public static class TypedMockVerifyFor_Interfaces_ITestable_FirstExtension
    {

        public void First(
            Func<IEnumerable<int>, bool> values,
            Times times = default(Times)!)
        {
            values ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> valuesExpression = argument => values(argument);
            mock.Verify(mock => mock.First(
                It.Is(valuesExpression)),
                times);
        }
    }
}

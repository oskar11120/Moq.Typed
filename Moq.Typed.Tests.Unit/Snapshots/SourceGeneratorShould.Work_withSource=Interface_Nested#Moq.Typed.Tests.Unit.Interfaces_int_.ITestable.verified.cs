﻿//HintName: Moq.Typed.Tests.Unit.Interfaces_int_.ITestable.cs
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

        #nullable disable warnings
        public class FirstParameters
        {
            public IEnumerable<int> values;
        }
        #nullable enable warnings

        private delegate void InternalFirstCallback(
            IEnumerable<int> values);

        private delegate int InternalFirstValueFunction(
            IEnumerable<int> values);

        private delegate TException InternalFirstExceptionFunction<TException>(
            IEnumerable<int> values);

        public delegate void FirstCallback(FirstParameters parameters);

        public delegate int FirstValueFunction(FirstParameters parameters);

        public delegate TException FirstExceptionFunction<TException>(FirstParameters parameters);

        public class FirstSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable, int> setup;

            public FirstSetup(ISetup<Moq.Typed.Tests.Unit.Interfaces<int>.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public FirstSetup Callback(FirstCallback callback)
            {
                setup.Callback(new InternalFirstCallback(
                    (IEnumerable<int> values) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            values = values
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public FirstSetup Returns(FirstValueFunction valueFunction)
            {
                setup.Returns(new InternalFirstValueFunction(
                    (IEnumerable<int> values) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            values = values
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public FirstSetup Returns(int value)
                => Returns(_ => value);

            public FirstSetup Throws<TException>(FirstExceptionFunction<TException> exceptionFunction) where TException : Exception
            {
                setup.Throws(new InternalFirstExceptionFunction<TException>(
                    (IEnumerable<int> values) => 
                    {
                        var __parameters__ = new FirstParameters
                        {
                            values = values
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

        public FirstSetup First(
            Func<IEnumerable<int>, bool>? values = null)
        {
            values ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> valuesExpression = argument => values(argument);
            var __local__ = mock.Setup(mock => mock.First(
                It.Is(valuesExpression)));
            return new FirstSetup(__local__);
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

        public void First(
            Func<IEnumerable<int>, bool>? values = null,
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

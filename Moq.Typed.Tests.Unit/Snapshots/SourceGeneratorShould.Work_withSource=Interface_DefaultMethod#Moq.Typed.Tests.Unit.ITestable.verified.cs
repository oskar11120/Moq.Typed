﻿//HintName: Moq.Typed.Tests.Unit.ITestable.cs
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

        public class IncrementParameters
        {
            public int number;
        }

        private delegate void InternalIncrementCallback(
            int number);

        private delegate int InternalIncrementValueFunction(
            int number);

        public delegate void IncrementCallback(IncrementParameters parameters);

        public delegate int IncrementValueFunction(IncrementParameters parameters);

        public class IncrementSetup
        {
            private readonly ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup;

            public IncrementSetup(ISetup<Moq.Typed.Tests.Unit.ITestable, int> setup)
            {
                this.setup = setup;
            }

            public IncrementSetup Callback(IncrementCallback callback)
            {
                setup.Callback(new InternalIncrementCallback(
                    (int number) => 
                    {
                        var __parameters__ = new IncrementParameters
                        {
                            number = number
                        };
                        callback(__parameters__);
                    }));
                return this;
            }

            public IncrementSetup Returns(IncrementValueFunction valueFunction)
            {
                setup.Returns(new InternalIncrementValueFunction(
                    (int number) => 
                    {
                        var __parameters__ = new IncrementParameters
                        {
                            number = number
                        };
                        return valueFunction(__parameters__);
                    }));
                return this;
            }

            public IncrementSetup Returns(int value)
                => Returns(_ => value);
        }

        public IncrementSetup Increment(
            Func<int, bool>? number = null)
        {
            number ??= static _ => true;
            Expression<Func<int, bool>> numberExpression = argument => number(argument);
            var __local__ = mock.Setup(mock => mock.Increment(
                It.Is(numberExpression)));
            return new IncrementSetup(__local__);
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

        public class IncrementParameters
        {
            public int number;
        }

        public void Increment(
            Func<int, bool>? number = null,
            Times times = default(Times)!)
        {
            number ??= static _ => true;
            Expression<Func<int, bool>> numberExpression = argument => number(argument);
            mock.Verify(mock => mock.Increment(
                It.Is(numberExpression)),
                times);
        }
    }
}

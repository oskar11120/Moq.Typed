//HintName: Moq.Typed.Tests.Unit.ITestable.cs
using Moq;
using Moq.Language.Flow;
using System;
using System.CodeDom.Compiler;
using System.Linq.Expressions;

namespace Moq.Typed.Tests.Unit
{
    [GeneratedCode("Moq.Typed", null)]
    internal static class TypedMockVerifyExtensionFor_ITestable
    {
        public static TypedMockVerifyFor_ITestable Verify(this Mock<Moq.Typed.Tests.Unit.ITestable> mock)
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

        public class FirstParameters
        {
        }

        private delegate void InternalFirstCallback();

        public delegate void FirstCallback(FirstParameters parameters);

        public void First(, 
            Times times = default(Times)!)
        {
            mock.Verify(mock => mock.First(),
                times);
        }

        public class SecondParameters
        {
            public IEnumerable<int> someInts;
        }

        private delegate void InternalSecondCallback(
            IEnumerable<int> someInts);

        public delegate void SecondCallback(SecondParameters parameters);

        public void Second(
            Func<IEnumerable<int>, bool>? someInts = null, 
            Times times = default(Times)!)
        {
            someInts ??= static _ => true;
            Expression<Func<IEnumerable<int>, bool>> someIntsExpression = argument => someInts(argument);
            mock.Verify(mock => mock.Second(
                It.Is(someIntsExpression)),
                times);
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

        public void Third(
            Func<IEnumerable<Moq.Typed.Tests.Unit.Parameter>, bool>? someParameters = null, 
            Func<Moq.Typed.Tests.Unit.Parameter, bool>? oneMoreParameter = null, 
            Func<int, bool>? someInt = null, 
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
    }
}

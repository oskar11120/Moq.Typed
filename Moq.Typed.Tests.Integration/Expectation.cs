using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq.Language.Flow;
using static Moq.Typed.Tests.Integration.Tests2;

namespace Moq.Typed.Tests.Integration
{
    internal static class IMockable0_MockSetupExtension2
    {
        public static IMockable0_TypedSetups2 Setup2(this Mock<IMockable0> mock)
            => new(mock);
    }

    internal class IMockable0_TypedSetups2
    {
        private readonly Mock<IMockable0> mock;

        public IMockable0_TypedSetups2(Mock<IMockable0> mock)
        {
            this.mock = mock;
        }

        public ISetup<IMockable0> Method0(Func<int, bool>? birthdayNumber = null)
        {
            birthdayNumber ??= static _ => true;
            Expression<Func<int, bool>>? birthdayNumberExpression = argument => birthdayNumber(argument);
            return mock.Setup(mockable => mockable.Method0(
                It.Is(birthdayNumberExpression)));
        }
    }
}

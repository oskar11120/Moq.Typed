using Moq;

namespace Moq.Typed.Tests.Integration;

public class Tests2
{
    [Test]
    public void Test1()
    {
        var first = new Mock<IMockable0>();
        var second = Mock.Get(Mock.Of<IMockable1>());
        first
            .Setup()
            .Method0();
    }

    public interface IMockable0
    {
        public void Method0(int phoneNumber);
    }

    public interface IMockable1
    {
        public void Method1();
    }
}
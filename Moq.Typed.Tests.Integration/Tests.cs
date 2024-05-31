using Moq;
using System.Collections.Generic;
using System.Linq;

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
        var third = new Mock<IMockable2>();
        third
            .Setup()
            .Count<int>(items => items.Any())
            .Callback(parameters => { })
            .Returns(parameters => 5);
        var fourth = new Mock<IMockable2>();
        var result = third.Object.Count(new int[] { 1 });
    }

    public interface IMockable0
    {
        public int Method0(int phoneNumber);
    }

    public interface IMockable1
    {
        public void Method1();
    }

    public interface IMockable2
    {
        public int Count<T>(IEnumerable<T> items);
    }
}
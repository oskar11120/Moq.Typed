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
            .Callback(parameters => { });
    }

    public interface IMockable0
    {
        public void Method0(int phoneNumber);
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
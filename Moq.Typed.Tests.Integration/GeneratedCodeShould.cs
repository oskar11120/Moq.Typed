using System.Collections.Generic;
using System.Linq;

namespace Moq.Typed.Tests.Integration;

public class GeneratedCodeShould
{
    [Test]
    public void Work()
    {
        var first = new Mock<IMockable0>();
        first
            .Setup()
            .Method0()
            .Callback(parameters => { })
            .Returns(parameters =>
            {
                Assert.That(parameters.phoneNumber, Is.EqualTo(1));
                return 2;
            });
        Assert.That(first.Object.Method0(1), Is.EqualTo(2));

        var second = Mock.Get(Mock.Of<IMockable1>());
        var secondCalled = false;
        second
            .Setup()
            .Count<int>(items => items.Any())
            .Callback(parameters =>
            {
                secondCalled = true;
                Assert.That(parameters.items.Single(), Is.EqualTo(1));
            })
            .Returns(parameters => 5);
        var secondResult = second.Object.Count(new int[] { 1 });
        Assert.That(secondCalled, Is.EqualTo(true));
        Assert.That(secondResult, Is.EqualTo(5));

        var thrid = new Mock<IMockable2>();
        thrid = new Mock<IMockable2>();
        thrid
            .Setup()
            .Property()
            .Returns(1);
        Assert.That(thrid.Object.Property, Is.EqualTo(1));
    }

    public interface IMockable0
    {
        int Method0(in int phoneNumber);
    }

    public interface IMockable1
    {
        int Count<T>(IEnumerable<T> items);
    }

    public interface IMockable2
    {
        int Property { get; set; }
    }
}
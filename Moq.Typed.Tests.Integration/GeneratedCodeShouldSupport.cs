using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moq.Typed.Tests.Integration;

public class GeneratedCodeShouldSupport
{
    public interface IWithRef
    {
        int Execute(ref int number);
    }

    [Test]
    public void RefParameters()
    {
        var mock = new Mock<IWithRef>();
        mock
            .Setup()
            .Execute()
            .Callback((ref TypedMockSetupFor_GeneratedCodeShouldSupport_IWithRef.ExecuteParameters parameters) => parameters.number++)
            .Returns((ref TypedMockSetupFor_GeneratedCodeShouldSupport_IWithRef.ExecuteParameters parameters) =>
            {
                Assert.That(parameters.number, Is.EqualTo(2));
                return 2;
            });
        var number = 1;
        Assert.That(mock.Object.Execute(ref number), Is.EqualTo(2));
        mock
            .Verifyy()
            .Execute();
    }

    public interface IWithIn
    {
        int Execute(in int number);
    }

    [Test]
    public void InParameters()
    {
        var mock = new Mock<IWithIn>();
        var wasCallbackCalled = false;
        mock
            .Setup()
            .Execute()
            .Callback(parameters =>
            {
                Assert.That(parameters.number, Is.EqualTo(1));
                wasCallbackCalled = true;
            })
            .Returns(parameters =>
            {
                Assert.That(parameters.number, Is.EqualTo(1));
                return 2;
            });
        Assert.Multiple(() =>
        {
            Assert.That(mock.Object.Execute(1), Is.EqualTo(2));
            Assert.That(wasCallbackCalled, Is.True);
        });
        mock
            .Verifyy()
            .Execute(Times.Once());
    }

    public interface IWithOut
    {
        bool Execute(out int number);
    }

    [Test]
    public void OutParameters()
    {
        var mock = new Mock<IWithOut>();
        mock
            .Setup()
            .Execute(3)
            .Returns(data => true);
        var number = 0;
        Assert.Multiple(() =>
        {
            Assert.That(mock.Object.Execute(out number), Is.True);
            Assert.That(number, Is.EqualTo(3));
        });
        mock
            .Verifyy()
            .Execute(times: Times.Once());
    }

    public interface IWithProperty
    {
        int Number { get; set; }
    }

    [Test]
    public void Properties()
    {
        var mock = new Mock<IWithProperty>();
        mock
            .Setup()
            .Number()
            .Returns(1);
        Assert.That(mock.Object.Number, Is.EqualTo(1));
        mock
            .Verifyy()
            .Number(Times.Once());
    }

    [Test]
    public void MultipleMockFactoryCalls()
    {
        _ = new Mock<IWithProperty>();
        _ = new Mock<IWithProperty>();
        Assert.Pass();
    }

    public interface IWithGeneric
    {
        int Execute<T>(IEnumerable<T> items);
    }

    [Test]
    public void GenericParameters()
    {
        var mock = Mock.Get(Mock.Of<IWithGeneric>());
        var called = false;
        mock
            .Setup()
            .Execute<int>(items => items.Any())
            .Callback(parameters =>
            {
                called = true;
                Assert.That(parameters.items.Single(), Is.EqualTo(1));
            })
            .Returns(parameters => 5);
        var ints = new int[] { 1 };
        var result = mock.Object.Execute(ints);
        Assert.Multiple(() =>
        {
            Assert.That(called, Is.EqualTo(true));
            Assert.That(result, Is.EqualTo(5));
        });
        mock
            .Verifyy()
            .Execute<int>(paramInts => paramInts == ints);
    }

    public interface IWithTaskLikes
    {
        Task<int> Execute(int number);
        ValueTask<int> Execute();
    }

    [Test]
    public async Task TaskLikes()
    {
        var mock = new Mock<IWithTaskLikes>();       
        mock
            .Setup()
            .Execute()
            .ReturnsAsync(parameters => 1);
        await Assert.ThatAsync(() => mock.Object.Execute().AsTask(), Is.EqualTo(1));
        mock
            .Setup()
            .Execute(number => number == 1)
            .ReturnsAsync(2);
        await Assert.ThatAsync(() => mock.Object.Execute(1), Is.EqualTo(2));
    }
}
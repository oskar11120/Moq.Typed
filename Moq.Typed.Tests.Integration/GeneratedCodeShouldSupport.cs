using System.Collections.Generic;
using System.Linq;

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
            .Callback((ref TypedMockForIWithRef.ExecuteParameters parameters) => parameters.number++)
            .Returns((ref TypedMockForIWithRef.ExecuteParameters parameters) =>
            {
                Assert.That(parameters.number, Is.EqualTo(2));
                return 2;
            });
        var number = 1;
        Assert.That(mock.Object.Execute(ref number), Is.EqualTo(2));
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
        var secondCalled = false;
        mock
            .Setup()
            .Execute<int>(items => items.Any())
            .Callback(parameters =>
            {
                secondCalled = true;
                Assert.That(parameters.items.Single(), Is.EqualTo(1));
            })
            .Returns(parameters => 5);
        var secondResult = mock.Object.Execute(new int[] { 1 });
        Assert.Multiple(() =>
        {
            Assert.That(secondCalled, Is.EqualTo(true));
            Assert.That(secondResult, Is.EqualTo(5));
        });
    }
}
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;

namespace Moq.Typed.Tests.Unit;

public class Tests
{
    [ModuleInitializer]
    public static void Init() =>
        VerifySourceGenerators.Initialize();

    [Test]
    public void Test1()
    {
        var mock = new Mock<object>().GetType().GetGenericTypeDefinition().FullName;

        var source = @"
using Moq;

namespace Moq.Typed.Tests.Integration;

public class Tests2
{
    [Test]
    public void Test1()
    {
        var first = new Mock<IMockable0>();
        var second = Mock.Get(Mock.Of<IMockable1>());
    }

    public interface IMockable0
    {
        public void Method0(int phoneNumber);
        public int Property0 { get; }
        public int Property1 { get; init;}
    }

    public class Mockable1
    {
        public abstract void Method1();
    }
}";

        var references = new[] { MetadataReference.CreateFromFile(typeof(Mock).Assembly.Location) };
        var syntax = CSharpSyntaxTree.ParseText(source);
        var compilation = CSharpCompilation.Create("name", [syntax], references);
        var generator = new SourceGenerator();

        var driver = CSharpGeneratorDriver.Create(generator);
        driver.RunGenerators(compilation);
        Verify(driver);
    }
}
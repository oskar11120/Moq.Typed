using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Moq.Typed.Tests.Unit;

internal class SourceGeneratorShould
{
    private static readonly object[] testCaseSources = Enum.GetValues<TestSourceId>().Cast<object>().ToArray();
    public enum TestSourceId
    {
        Interface_MethodsOnly,
        NonGenericInterface_GenericMethod,
        GenericInterface_NonGenericMethod,
        GenericInterface_GenericMethod,
        Interface_DefaultMethod,
        Interface_Properties,
        Interface_Nested,
        Interace_NestedMethodParameters,
        Class_AbstractMethod,
        Class_VirtualMethod,
        Interface_MethodOverloads,
        Interface_MethodGenericConstraints,
        MultipleMocks_OfTheSameInterface,
        OutParameter,
        RefParameter,
        InParameter
    }

    private static string NewSource(string testTypes, string testContent = "var first = new Mock<ITestable>();") => $$"""
        using Moq;
            
        namespace Moq.Typed.Tests.Unit;
            
        {{testTypes}}

        public class Tests
        {
            [Test]
            public void Test()
            {
                {{testContent}}
            }
        }
        """;

    private static readonly Dictionary<TestSourceId, string> sources = new()
    {
        [TestSourceId.Interface_MethodsOnly] = NewSource(
            """
            public record Parameter(int Value);

            public interface ITestable
            {
                void First();
                internal int Second(IEnumerable<int> someInts);
                public void Third(IEnumerable<Parameter> someParameters, Parameter oneMoreParameter, int someInt);
            }
            """),
        [TestSourceId.NonGenericInterface_GenericMethod] = NewSource(
            """
            public interface ITestable
            {
                void First<T>(T someGenericParam);
                TOutput Second<TInput, TOutput>(int someInt, TInput genericInput);
            }
            """),
        [TestSourceId.GenericInterface_NonGenericMethod] = NewSource(
            """
            public interface ITestable<T>
            {
                T First();
                int Second(T genericParam);
            }
            """,
            "var first = Mock.Get(Mock.Of<ITestable<int>>());"),
        [TestSourceId.GenericInterface_GenericMethod] = NewSource(
            """
            public interface ITestable<T>
            {
                T First<TInput>(TInput genericParam);
                TOutput Second<TOutput>(T genericParam);
            }
            """,
            "var first = Mock.Get(Mock.Of<ITestable<int>>());"),
        [TestSourceId.Interface_DefaultMethod] = NewSource(
            """
            public interface ITestable
            {
                int Increment(int number)
                    => number + 1;
            }
            """),
        [TestSourceId.Interface_Properties] = NewSource(
            """
            public interface ITestable
            {
                int Getter { get; }
                IEnumerable<int> Mutable { get; set; }
                int InitOnly { get; init; }
                int Getter();
            }
            """),
        [TestSourceId.Interface_Nested] = NewSource(
            """
            using static Interfaces;
            public static class Interfaces<T>
            {
                public interface ITestable
                {
                    int First(IEnumerable<T> values);
                }
            }
            """,
            "var mock = new Mock<Interfaces<int>.ITestable>();"),
        [TestSourceId.Interace_NestedMethodParameters] = NewSource(
            """
            public static class Parameters
            {
                public record First(int Value);
            }

            public interface ITestable
            {
                int First(Parameters.First parameter);
            }
            """),
        [TestSourceId.Class_AbstractMethod] = NewSource(
            """
            public class ITestable
            {
                public abstract void Public();
                internal abstract int Internal(object someObject);
            }
            """),
        [TestSourceId.Class_VirtualMethod] = NewSource(
            """
            public class ITestable
            {
                public virtual int First(object someObject)
                    => 1;
            }
            """),
        [TestSourceId.Interface_MethodOverloads] = NewSource(
            """
            public interface ITestable
            {
                int Method();
                int Method(int Parameter);
            }
            """),
        [TestSourceId.Interface_MethodGenericConstraints] = NewSource(
            """
            public interface ITestable
            {
                int Method<TInput>(TInput Parameter) where TInput : struct, System.Enum;
            }
            """),
        [TestSourceId.MultipleMocks_OfTheSameInterface] = NewSource(
            """
            public interface ITestable
            {
                void Method();
            }
            """,
            """
            var first = new Mock<ITestable>();
            var second = new Mock<ITestable>();
            """),
        [TestSourceId.OutParameter] = NewSource(
            """
            public interface ITestable
            {
                void Method(out int outParameter);
            }
            """),
        [TestSourceId.RefParameter] = NewSource(
            """
            public interface ITestable
            {
                int Method(ref int refParameter);
            }
            """),
        [TestSourceId.InParameter] = NewSource(
            """
            public interface ITestable
            {
                void Method(in int inParameter);
            }
            """)
    };

    [TestCaseSource(nameof(testCaseSources))]
    public async Task Work(TestSourceId withSource)
    {
        var source = sources[withSource];
        var references = new[] { MetadataReference.CreateFromFile(typeof(Mock).Assembly.Location) };
        var syntax = CSharpSyntaxTree.ParseText(source);
        var compilation = CSharpCompilation.Create("name", [syntax], references);
        var generator = new SourceGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        await Verify(driver.RunGenerators(compilation));
    }
}
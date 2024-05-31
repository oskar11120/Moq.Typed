using System.Runtime.CompilerServices;

namespace Moq.Typed.Tests.Unit;

public static class Initializer
{
    [ModuleInitializer]
    public static void Run()
    {
        VerifySourceGenerators.Initialize();
        UseProjectRelativeDirectory("Snapshots");
    }
}

using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Running;

namespace JsonBenchmark;

[SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
internal static class Program
{
    private static void Main()
    {
        var summary = BenchmarkRunner.Run<NewtonsoftVsSystem>();
        Console.ReadLine();
    }
}
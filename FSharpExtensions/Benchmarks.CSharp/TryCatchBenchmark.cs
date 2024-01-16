using BenchmarkDotNet.Attributes;

namespace Benchmarks.CSharp;

[MemoryDiagnoser]
[DisassemblyDiagnoser]
public class TryCatchBenchmark
{
    private Action<TryCatchBenchmark>? Action;

    [GlobalSetup]
    public void GlobalSetup()
    {
        Action = null;
    }

    [Benchmark]
    public void RunTryCatch()
    {
        try
        {
            Action?.Invoke(this);
        }
        catch
        {
        }
    }

    [Benchmark]
    public void RunCheckThenTryCatch()
    {
        if (Action is {} action)
        {
            try
            {
                action(this);
            }
            catch
            {
            }
        }
    }
}
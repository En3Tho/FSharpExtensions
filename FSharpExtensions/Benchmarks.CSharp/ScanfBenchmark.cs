using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using En3Tho.FSharp.Extensions;

namespace Benchmarks.CSharp
{
    [SimpleJob(RuntimeMoniker.Net60)]
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class ScanfBenchmark
    {
        [Benchmark]
        public bool PrimitivesOnlyPreallocated()
        {
            return Scanf.scanf(Benchmarks.ScanfBenchmark.Assets.primitivesOnlyFmt, "123456 123456.123456");
        }

        [Benchmark]
        public bool PrimitivesOnlyPreallocatedSpan()
        {
            return Scanf.scanfSpan(Benchmarks.ScanfBenchmark.Assets.primitivesOnlyFmt, "123456 123456.123456".AsSpan());
        }
    }
}
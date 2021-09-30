module Benchmarks.BenchmarkConfig

open BenchmarkDotNet.Configs
open BenchmarkDotNet.Environments
open BenchmarkDotNet.Jobs

type ``Net 5, Net 6, Pgo``() =
    inherit ManualConfig()

    let [<Literal>] TieredPGO = "DOTNET_TieredPGO"
    let [<Literal>] QuickJitForLoops = "DOTNET_TC_QuickJitForLoop"
    let [<Literal>] ReadyToRun = "DOTNET_ReadyToRun"

    do
        base.AddJob([|
            Job.Default.WithRuntime(CoreRuntime.Core50).WithId("Net5")
            Job.Default.WithRuntime(CoreRuntime.Core60).WithId("Net6")
            Job.Default.WithRuntime(CoreRuntime.Core60).WithId("Net6PGO")
                        .WithEnvironmentVariables(EnvironmentVariable(TieredPGO, "1"),
                                                  EnvironmentVariable(QuickJitForLoops, "1"),
                                                  EnvironmentVariable(ReadyToRun, "0"))
        |]) |> ignore

type ``Net 6, Pgo``() =
    inherit ManualConfig()

    let [<Literal>] TieredPGO = "DOTNET_TieredPGO"
    let [<Literal>] QuickJitForLoops = "DOTNET_TC_QuickJitForLoop"
    let [<Literal>] ReadyToRun = "DOTNET_ReadyToRun"

    do
        base.AddJob([|
            Job.Default.WithRuntime(CoreRuntime.Core60).WithId("Net6")
            Job.Default.WithRuntime(CoreRuntime.Core60).WithId("Net6PGO")
                        .WithEnvironmentVariables(EnvironmentVariable(TieredPGO, "1"),
                                                  EnvironmentVariable(QuickJitForLoops, "1"),
                                                  EnvironmentVariable(ReadyToRun, "0"))
        |]) |> ignore

type ``Net 5, Net 6``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Default.WithRuntime(CoreRuntime.Core50).WithId("Net5")
            Job.Default.WithRuntime(CoreRuntime.Core60).WithId("Net6")
        |]) |> ignore
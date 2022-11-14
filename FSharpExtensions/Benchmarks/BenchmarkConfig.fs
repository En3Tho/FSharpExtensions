[<Microsoft.FSharp.Core.AutoOpen>]
module Benchmarks.BDN

open System
open System.IO
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Environments
open BenchmarkDotNet.Jobs
open BenchmarkDotNet.Toolchains.CoreRun

let [<Literal>] TieredPGO = "DOTNET_TieredPGO"
let [<Literal>] QuickJitForLoops = "DOTNET_TC_QuickJitForLoop"
let [<Literal>] ReadyToRun = "DOTNET_ReadyToRun"

type CoreRuntime with
    static member Core80 = CoreRuntime.CreateForNewVersion("net8.0", ".NET 8.0")

type Job with
    static member Net5 = Job.Default.WithRuntime(CoreRuntime.Core50).WithId("Net5")
    static member Net6 = Job.Default.WithRuntime(CoreRuntime.Core60).WithId("Net6")
    static member Net7 = Job.Default.WithRuntime(CoreRuntime.Core70).WithId("Net7")
    static member Main =
        let path = Environment.GetEnvironmentVariable("BDN_DOTNET_PATH")
        let moniker = Environment.GetEnvironmentVariable("BDN_DOTNET_MONIKER")
        Job.Default
            .WithToolchain(CoreRunToolchain(
                coreRun = FileInfo path,
                targetFrameworkMoniker = moniker))
            .WithId("Main")

type ``Net 5, Net 6, Pgo``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Net5
            Job.Net6
            Job.Net6.WithId("Net6PGO")
                    .WithEnvironmentVariables(EnvironmentVariable(TieredPGO, "1"),
                                              EnvironmentVariable(QuickJitForLoops, "1"),
                                              EnvironmentVariable(ReadyToRun, "0"))
        |]) |> ignore

type ``Net 6, Pgo``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Net6
            Job.Net6.WithId("Net6PGO")
                     .WithEnvironmentVariables(EnvironmentVariable(TieredPGO, "1"),
                                                 EnvironmentVariable(QuickJitForLoops, "1"),
                                                 EnvironmentVariable(ReadyToRun, "0"))
        |]) |> ignore

type ``Net 5, Net 6``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Net5
            Job.Net6
        |]) |> ignore
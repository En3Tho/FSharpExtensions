[<AutoOpen>]
module Benchmarks.BenchmarkConfig

open System
open System.IO
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Environments
open BenchmarkDotNet.Jobs
open BenchmarkDotNet.Toolchains.CoreRun
open En3Tho.FSharp.Extensions

type EnvironmentVariable with
    static member Get(name: string) =
        match Environment.GetEnvironmentVariable(name) with
        | String.NullOrWhiteSpace ->
            raise ^ InvalidOperationException($"Unable to get {name} environment variable.")
        | envVar ->
            EnvironmentVariable(name, envVar)

module JitEnv =
    module Env =
        let [<Literal>] TieredPGO = "DOTNET_TieredPGO"
        let [<Literal>] QuickJitForLoops = "DOTNET_TC_QuickJitForLoop"
        let [<Literal>] ReadyToRun = "DOTNET_ReadyToRun"
        let [<Literal>] JitStressModeNames = "DOTNET_JitStressModeNames"
        let [<Literal>] JitEnablePhysicalPromotion = "DOTNET_JitEnablePhysicalPromotion"

    type TieredPGO() =
        static member On = EnvironmentVariable(Env.TieredPGO, "1")
        static member Off = EnvironmentVariable(Env.TieredPGO, "0")

    type QuickJitForLoops() =
        static member On = EnvironmentVariable(Env.QuickJitForLoops, "1")
        static member Off = EnvironmentVariable(Env.QuickJitForLoops, "0")

    type ReadyToRun() =
        static member On = EnvironmentVariable(Env.ReadyToRun, "1")
        static member Off = EnvironmentVariable(Env.ReadyToRun, "0")

    type JitEnablePhysicalPromotion() =
        static member On = EnvironmentVariable(Env.JitEnablePhysicalPromotion, "1")
        static member Off = EnvironmentVariable(Env.JitEnablePhysicalPromotion, "0")

    type JitStressModeNames() =
        static member StressGeneralizedPromotion = EnvironmentVariable(Env.JitStressModeNames, "STRESS_GENERALIZED_PROMOTION")
        static member StressGeneralizedPromotionCost = EnvironmentVariable(Env.JitStressModeNames, "STRESS_GENERALIZED_PROMOTION STRESS_GENERALIZED_PROMOTION_COST")

type Job with
    static member Net5 = Job.Default.WithRuntime(CoreRuntime.Core50).WithId("Net5")
    static member Net6 = Job.Default.WithRuntime(CoreRuntime.Core60).WithId("Net6")
    static member Net7 = Job.Default.WithRuntime(CoreRuntime.Core70).WithId("Net7")
    static member Net8 = Job.Default.WithRuntime(CoreRuntime.Core80).WithId("Net8")
    static member CoreRun =
        Job.Default
            .WithToolchain(CoreRunToolchain(
                coreRun = FileInfo(EnvironmentVariable.Get("DOTNET_CORERUN_PATH").Value),
                targetFrameworkMoniker = EnvironmentVariable.Get("DOTNET_CORERUN_TFM").Value))
            .WithId("CoreRun")

type ``Net7, Net8, CoreRun``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Net7
            Job.Net8
            Job.CoreRun
        |]) |> ignore

type ``Net7, Net8``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Net7
            Job.Net8
            Job.Net8.WithEnvironmentVariables(
                JitEnv.JitEnablePhysicalPromotion.On,
                JitEnv.JitStressModeNames.StressGeneralizedPromotionCost
            )
            // Job.Main.WithEnvironmentVariables(
            //     JitEnv.JitStressModeNames.StressGeneralizedPromotion,
            //     EnvironmentVariable.Get("CORE_LIBRARIES")
            // )
        |]) |> ignore

type ``Net 5, Net 6, Pgo``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Net5
            Job.Net6
            Job.Net6
                .WithId("Net6PGO")
                .WithEnvironmentVariables(
                    JitEnv.TieredPGO.On,
                    JitEnv.QuickJitForLoops.On,
                    JitEnv.ReadyToRun.Off
                )
        |]) |> ignore

type ``Net 6, Pgo``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Net6
            Job.Net6
                .WithId("Net6PGO")
                .WithEnvironmentVariables(
                    JitEnv.TieredPGO.On,
                    JitEnv.QuickJitForLoops.On,
                    JitEnv.ReadyToRun.Off
                )
        |]) |> ignore

type ``Net 5, Net 6``() =
    inherit ManualConfig()

    do
        base.AddJob([|
            Job.Net5
            Job.Net6
        |]) |> ignore
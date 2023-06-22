open System
open System.Diagnostics
open ProjectUtilities

let go() =

    let sw = Stopwatch.StartNew()

    ServiceCollectionCodeGen.generateFiles()
    ServiceProviderAndScopeCodeGen.generateFiles()
    WebApplicationsExtensionsCodeGen.generateFiles()

    Console.WriteLine($"Elapsed: {sw.Elapsed}")

go()
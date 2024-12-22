open System
open System.Diagnostics

open ProjectUtilities

let go() =
    try
        let sw = Stopwatch.StartNew()

        // AddFunc.generateFiles()
        // ServiceProviderAndScopeCodeGen.generateFiles()
        // WebApplicationsExtensionsCodeGen.generateFiles()
        // ILoggerExtensionsCodeGen.generateFiles()
        IntrinsicsCodeGen.generateFiles "D:\Downloads\intel-intrinsics-3-6-9.xml"

        Console.WriteLine($"Elapsed: {sw.Elapsed}")
    with e ->
        Console.WriteLine(e.ToString())
go()
module CentralPackageManagementMigrationTool.DotnetInfo

open System
open System.Collections.Generic
open System.Diagnostics
open System.IO

let [<Literal>] DOTNET_CLI_UI_LANGUAGE = "DOTNET_CLI_UI_LANGUAGE"

let readProcessOutput (startInfo: ProcessStartInfo) =
    use process' = Process.Start(startInfo)
    let lines = List<string>();
    process'.OutputDataReceived.AddHandler (fun _ e ->
        if String.IsNullOrWhiteSpace(e.Data) |> not then
            lines.Add(e.Data);
    )
    process'.BeginOutputReadLine();
    process'.WaitForExit()
    lines

let parseCoreBasePath (lines: List<string>) =
    let rec findBasePath i =
        let line = lines[i]
        match line.Split(":", 2, StringSplitOptions.TrimEntries) with
        | [| "Base Path"; sdkPath |] ->
            sdkPath
        | _ ->
            findBasePath(i + 1)

    findBasePath 0

let getCoreBasePath (projectPath: string) =
    let originalCliLanguage = Environment.GetEnvironmentVariable(DOTNET_CLI_UI_LANGUAGE);
    Environment.SetEnvironmentVariable(DOTNET_CLI_UI_LANGUAGE, "en-US")

    try
        let startInfo = ProcessStartInfo("dotnet", "--info",
            WorkingDirectory = Path.GetDirectoryName(projectPath),
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        )
        let lines = readProcessOutput startInfo
        parseCoreBasePath lines
    finally
        Environment.SetEnvironmentVariable(DOTNET_CLI_UI_LANGUAGE, originalCliLanguage);
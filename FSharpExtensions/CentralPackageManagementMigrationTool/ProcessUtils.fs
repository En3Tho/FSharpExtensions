module CentralPackageManagementMigrationTool.ProcessUtils

open System
open System.Collections.Generic
open System.Diagnostics

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
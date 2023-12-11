module ProjectUtilities.ILoggerExtensionsCodeGen

open System
open System.Reflection
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder
open Microsoft.Extensions.Logging

let renameParamNameIfException(paramName: string) =
    if paramName.Equals("exception") then "``exception``" else paramName

let parametersToArgsString (parameters: ParameterInfo[]) =
    parameters
    |> Array.map ^ fun param -> $"{renameParamNameIfException param.Name}: {param.ParameterType.Name}"
    |> String.concat ", "

let parametersToParamString (parameters: ParameterInfo[]) =
    parameters
    |> Array.map ^ fun param -> $"{renameParamNameIfException param.Name}"
    |> String.concat ", "

let generateCodeForLogLevel (logLevel: LogLevel) (methods: MethodInfo[]) = codeBlock {
    for method in methods do
        let nonLoggerAndNonArgsParameters =
            method.GetParameters()
            |> Array.skip 1
            |> Array.where ^ fun param -> param.ParameterType.Equals(typeof<obj[]>) |> not

        let methodArgs =
            nonLoggerAndNonArgsParameters
            |> parametersToArgsString

        let methodParams =
            nonLoggerAndNonArgsParameters
            |> parametersToParamString

        for i = 1 to 16 do
            let genericArgs = Array.init i (fun idx -> $"arg{idx}: 'a{idx}") |> String.concat ", "
            let genericObjArray = Array.init i (fun idx -> $"arg{idx}") |> String.concat "; "
            $"member inline this.{method.Name}({methodArgs}, {genericArgs}) ="
            indent {
                $"if this.IsEnabled({nameof(LogLevel)}.{logLevel}) then"
                indent {
                    $"let args: obj[] = [| {genericObjArray} |]"
                    $"this.{method.Name}({methodParams}, args)"
                }
            }
            ""
        trimEnd()
}

let generateILoggerExtensionsCode() =
    let typeToReadFrom = typeof<LoggerExtensions>
    let methods = typeToReadFrom.GetMethods()
    code {
        "// auto-generated"
        "[<Microsoft.FSharp.Core.AutoOpen>]"
        "module Microsoft.Extensions.Logging.ILoggerExtensions"
        ""
        "open System"
        ""
        "type ILogger with"
        indent {
            for logLevel in Enum.GetValues<LogLevel>() do
                let logLevelMethods = methods |> Array.where ^ fun method -> method.Name.Equals($"Log{logLevel}")
                generateCodeForLogLevel logLevel logLevelMethods
                ""
        }
        trimEnd()
    }

let generateFiles() =
    generateILoggerExtensionsCode()
    |> Code.writeToFile "ILoggerExtensions.fs"
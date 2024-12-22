module ProjectUtilities.Code

open System
open System.IO
open En3Tho.FSharp.ComputationExpressions.CodeBuilder.CodeBuilderImpl
open En3Tho.FSharp.Extensions

let writeToFile filePath (code: CodeBuilder) =
    let dirName = ".artifacts"
    if not ^ Directory.Exists(dirName) then Directory.CreateDirectory(dirName) |> ignore

    let text = code |> toString
    File.WriteAllText(Path.Combine(dirName, filePath), text)

let writeToConsole (code: CodeBuilder) =
    code.Flush(Console.Out)
    Console.WriteLine()
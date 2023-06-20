module ProjectUtilities.WebApplicationsExtensionsCodeGen

open System.IO
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

let generateCodeForHttpMethod httpMethod genericArgsCount = code {
    let args =
        [
            for argIndex = 1 to genericArgsCount do
                $"'T{argIndex}"
        ] |> String.concat ", "

    $"member inline this.Map{httpMethod}(pattern, func: Func<{args}, 'TResult>) = this.MapGet(pattern, handler = func)"
}

let generateWebApplicationExtensions() = code {
    "[<AutoOpen>]"
    "module En3Tho.FSharp.Extensions.AspNetCore.WebAppExtensions"
    ""
    "open System"
    "open Microsoft.AspNetCore.Builder"
    ""
    "type WebApplication with"
    indent {
        for httpMethod in ["Get"; "Post"; "Put"; "Delete"; "Patch"] do
            for genericArgsCount in 1 .. 16 do
                generateCodeForHttpMethod httpMethod genericArgsCount
            ""
    }
}

let generateFileForCodeBlock fileName (codeBlock: CodeBuilderImpl.CodeBuilder) =
        codeBlock
        |> toString
        |> fun text -> File.WriteAllText(fileName, text)

let generateFiles() =
    let dirName = ".artifacts"
    if not ^ Directory.Exists dirName then Directory.CreateDirectory(dirName) |> ignore

    generateWebApplicationExtensions()
    |> generateFileForCodeBlock $"{dirName}/WebApplicationExtensions.fs"
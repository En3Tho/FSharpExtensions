module ProjectUtilities.WebApplicationsExtensionsCodeGen

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
    "// auto-generated"
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
        trimEnd()
    }
}

let generateFiles() =
    generateWebApplicationExtensions()
    |> Code.writeToFile "WebApplicationExtensions.fs"
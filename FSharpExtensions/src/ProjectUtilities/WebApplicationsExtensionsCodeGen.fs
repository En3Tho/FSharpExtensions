module ProjectUtilities.WebApplicationsExtensionsCodeGen

open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

let generateCodeForHttpMethod httpMethod genericArgsCount = codeBlock {
    let args =
        [
            for argIndex = 1 to genericArgsCount do
                $"'T{argIndex}"
        ] |> String.concat ", "

    $"member inline this.Map{httpMethod}(pattern, func: Func<{args}, 'TResult>) = this.Map{httpMethod}(pattern, handler = func)"
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
            $"member inline this.Map{httpMethod}(pattern, func: Action) = this.Map{httpMethod}(pattern, handler = func)"
            $"member inline this.Map{httpMethod}(pattern, func: Func<'TResult>) = this.Map{httpMethod}(pattern, handler = func)"
            for genericArgsCount in 2 .. 16 do
                generateCodeForHttpMethod httpMethod genericArgsCount
            ""
        trimEnd()
    }
}

let generateFactoryCodeForHttpMethod httpMethod genericArgsCount = codeBlock {
    let args =
        [
            for argIndex = 1 to genericArgsCount do
                $"'T{argIndex}"
        ] |> String.concat ", "

    $"static member {httpMethod}(route: string, handler: Func<{args}, 'TResult>) ="
    indent {
        $"{httpMethod}(route, handler :> Delegate)"
    }
}

let generateWebApplicationFactoryMembers() = code {
    "// auto-generated"
    "namespace En3Tho.FSharp.Extensions.AspNetCore"
    ""
    "open System"
    ""
    "[<AbstractClass; Sealed; AutoOpen>]"
    "type EndpointInfoFactory() ="
    ""
    indent {
        for httpMethod in ["Get"; "Post"; "Put"; "Delete"; "Patch"] do

            $"static member {httpMethod}(route: string, handler) ="
            indent {
                $"EndpointInfo(EndpointType.{httpMethod}, route, handler)"
            }
            $"static member {httpMethod}(route: string, handler: Action) ="
            indent {
                $"{httpMethod}(route, handler :> Delegate)"
            }
            $"static member {httpMethod}(route: string, handler: Func<'TResult>) ="
            indent {
                $"{httpMethod}(route, handler :> Delegate)"
            }
            for genericArgsCount in 2 .. 16 do
                generateFactoryCodeForHttpMethod httpMethod genericArgsCount
            ""
        trimEnd()
    }
}

let generateFiles() =
    generateWebApplicationExtensions()
    |> Code.writeToFile "WebApplicationExtensions.fs"

    generateWebApplicationFactoryMembers()
    |> Code.writeToFile "EndpointInfoFactory.fs"
module ProjectUtilities.ServiceCollectionCodeGen

open System
open System.IO
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

let getMethodCodeForVerb dependenciesCount (verb: string) =

    let genericParameters =
        let dependencies = [| for index = 1 to dependenciesCount do  $"TDependency{index}" |]
        String.Join(", ", dependencies)

    code {
        $"public static IServiceCollection {verb}Func<TService, {genericParameters}>(this IServiceCollection collection,"

        indent {
            "Func<TDependency1, TService> factory)"
            "where TService : class"
            for i = 1 to dependenciesCount do
                $"where TDependency{i} : notnull"
        }
        braceBlock {
            $"collection.{verb}("
            indent {
                "serviceProvider => factory("
                indent {
                    for index = 1 to dependenciesCount - 1 do
                        $"serviceProvider.GetRequiredService<TDependency{index}>(),"
                    $"serviceProvider.GetRequiredService<TDependency{dependenciesCount}>()"
                }
                "))";
            }
            "return collection;"
        }
    }

let generateFileForVerb dependenciesCount verb =
    code {
        "using System;"
        "using Microsoft.Extensions.DependencyInjection;"
        "namespace En3Tho.Extensions.DependencyInjection;"
        ""
        "public static partial class IServiceCollectionExtensions"
        braceBlock {
            for i = 1 to dependenciesCount do
                getMethodCodeForVerb i verb
                ""
        }
    }

let generateAllLifetimesAndVerbs() = seq {
    let lifetimes = [| "Singleton"; "Scoped"; "Transient" |]

    for lifetime in lifetimes do
        let verbs = [| $"Add{lifetime}"; $"TryAdd{lifetime}"; $"TryAdd{lifetime}OrFail" |]
        for verb in verbs do
            let count = 15
            let filePath = $"{verb}Func.cs"
            filePath, generateFileForVerb count verb
}

let makeFiles() =
    generateAllLifetimesAndVerbs()
    |> Seq.iter ^ fun (filePath, code) ->
        let text = code |> toString
        File.WriteAllText(filePath, text)

let dumpToConsole() =
    generateAllLifetimesAndVerbs()
    |> Seq.iter ^ fun (_, code) ->
        let text = code |> toString
        Console.WriteLine(text)
        Console.WriteLine()
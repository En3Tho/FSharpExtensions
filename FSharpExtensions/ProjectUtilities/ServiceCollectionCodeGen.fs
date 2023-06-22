module ProjectUtilities.ServiceCollectionCodeGen

open System
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

let getMethodCodeForVerb dependenciesCount (verb: string) =

    let genericParameters =
        let dependencies = [| for index = 1 to dependenciesCount do  $"TDependency{index}" |]
        String.Join(", ", dependencies)

    code {
        $"public static IServiceCollection {verb}Func<TService, {genericParameters}>(this IServiceCollection collection,"

        indent {
            $"Func<{genericParameters}, TService> factory)"
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
            }
            "));"
            "return collection;"
        }
    }

let generateFileForVerb dependenciesCount verb =
    code {
        "// auto-generated"
        "using Microsoft.Extensions.DependencyInjection;"
        "using Microsoft.Extensions.DependencyInjection.Extensions;"
        ""
        "namespace En3Tho.Extensions.DependencyInjection;"
        ""
        "public static partial class IServiceCollectionExtensions"
        braceBlock {
            for i = 1 to dependenciesCount do
                getMethodCodeForVerb i verb
                ""
            trimEnd()
        }
    }

let generateAllLifetimesAndVerbs() = seq {
    let lifetimes = [| "Singleton"; "Scoped"; "Transient" |]

    for lifetime in lifetimes do
        let verbs = [| $"Add{lifetime}"; $"TryAdd{lifetime}"; $"TryAdd{lifetime}OrFail" |]
        for verb in verbs do
            let funcArgsCount = 16
            let filePath = $"{verb}Func.cs"
            filePath, generateFileForVerb funcArgsCount verb
}

let generateFiles() =
    generateAllLifetimesAndVerbs()
    |> Seq.iter ^ fun (filePath, code) ->
        Code.writeToFile filePath code
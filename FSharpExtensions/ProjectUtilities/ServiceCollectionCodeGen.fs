module ProjectUtilities.ServiceCollectionCodeGen

open System
open System.IO
open En3Tho.FSharp.Extensions
open ProjectUtilities.CodeBuilder.CodeBuilder

let test1() =

    code {
        "public static IServiceCollection TryAddScopedFunc<TService, TDependency1>(this IServiceCollection collection,"

        indent {
            "Func<TDependency1, TService> factory)"
            "where TService : class"
            "where TDependency1 : notnull"
        }
        "{"
        indent {
            "collection.TryAddScoped("
            indent {
                "serviceProvider => factory("
                indent {
                    "serviceProvider.GetRequiredService<TDependency1>()"
                }
                "))";
            }
            "return collection;"
        }
        "}"
    }
    |> toString

let runTest1() =
    let result = test1()
    Console.WriteLine result


let generateAddFunc dependenciesCount (verb: string) =

    let genericParameters =
        let deps = Array.init dependenciesCount ^ fun index -> $"TDependency{index + 1}"
        String.Join(", ", deps)

    code {
        $"public static IServiceCollection {verb}Func<TService, {genericParameters}>(this IServiceCollection collection,"

        indent {
            "Func<TDependency1, TService> factory)"
            "where TService : class"
            for i = 1 to dependenciesCount do
                $"where TDependency{i} : notnull"
        }
        "{"
        indent {
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
        "}"
    }

let generateFileForVerb dependenciesCount verb =
    code {
        "using System;"
        "using Microsoft.Extensions.DependencyInjection;"
        "namespace En3Tho.Extensions.DependencyInjection;"

        "public static partial class IServiceCollectionExtensions"
        "{"
        indent {
            for i = 1 to dependenciesCount do
                generateAddFunc i verb
                ""
        }
        "}"
    }

let testGenerateAddFunc() =
    generateAddFunc 1 "TryAddScoped"
    |> Console.WriteLine

let testFileGen() =
    let text = generateFileForVerb 3 "TryAddScoped"
    text |> Console.WriteLine

let generateAllLifetimeAndVerbs() =
    let lifetimes = [| "Singleton"; "Scoped"; "Transient" |]

    for lifetime in lifetimes do
        let verbs = [| $"Add{lifetime}"; $"TryAdd{lifetime}"; $"TryAdd{lifetime}OrFail" |]
        for verb in verbs do
            let count = 15
            let filePath = $"{verb}Func.cs"
            let fileText = generateFileForVerb count verb |> toString
            File.WriteAllText(filePath, fileText)
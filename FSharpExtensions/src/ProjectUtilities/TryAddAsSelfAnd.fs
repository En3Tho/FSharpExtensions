module ProjectUtilities.TryAddAsSelfAnd

open System
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

let genTryAddAsSelfAnd dependenciesCount (verb: string) =
    let genericParameters =
        let dependencies = [| for index = 1 to dependenciesCount do  $"TService{index}" |]
        String.Join(", ", dependencies)

    let verbToCall = verb.Replace("AsSelfAnd", "")

    codeBlock {
        $"public static IServiceCollection {verb}<{genericParameters}, TImplementation>(this IServiceCollection collection)"
        indent {
            for i = 1 to dependenciesCount do
                $"where TService{i} : class"
            $"where TImplementation : class, {genericParameters}"
        }
        braceBlock {
            $"collection.{verbToCall}<TImplementation>();"
            for i = 1 to dependenciesCount do
                $"collection.{verbToCall}<TService{i}>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());"
            "return collection;"
        }
    }

let genTryAddAsSelfAndWithFactory dependenciesCount (verb: string) =
    let genericParameters =
        let dependencies = [| for index = 1 to dependenciesCount do  $"TService{index}" |]
        String.Join(", ", dependencies)

    let verbToCall = verb.Replace("AsSelfAnd", "")

    codeBlock {
        $"public static IServiceCollection {verb}<{genericParameters}, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)"
        indent {
            for i = 1 to dependenciesCount do
                $"where TService{i} : class"
            $"where TImplementation : class, {genericParameters}"
        }
        braceBlock {
            $"collection.{verbToCall}(implementationFactory);"
            for i = 1 to dependenciesCount do
                $"collection.{verbToCall}<TService{i}>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());"
            "return collection;"
        }
    }

let genTryAddAsSelfAndWithInstance dependenciesCount (verb: string) =
    let genericParameters =
        let dependencies = [| for index = 1 to dependenciesCount do  $"TService{index}" |]
        String.Join(", ", dependencies)

    let verbToCall = verb.Replace("AsSelfAnd", "")

    codeBlock {
        $"public static IServiceCollection {verb}<{genericParameters}, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)"
        indent {
            for i = 1 to dependenciesCount do
                $"where TService{i} : class"
            $"where TImplementation : class, {genericParameters}"
        }
        braceBlock {
            $"collection.{verbToCall}(implementationInstance);"
            for i = 1 to dependenciesCount do
                $"collection.{verbToCall}<TService{i}>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());"
            "return collection;"
        }
    }

let generateFileForVerb dependenciesCount verb (generators: seq<int -> string -> CodeBuilderImpl.CodeBuilderCode>) =
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
                for generator in generators do
                    generator i verb
                    ""
            trimEnd()
        }
    }

let generateAllLifetimesAndVerbs() = seq {
    let gen lifetime generators = seq {
        let verbs = [| $"Add{lifetime}AsSelfAnd"; $"TryAdd{lifetime}AsSelfAnd"; $"TryAdd{lifetime}AsSelfAndOrFail" |]
        for verb in verbs do
            let funcArgsCount = 16
            let filePath = $"{verb}.cs"
            filePath, generateFileForVerb funcArgsCount verb generators
    }

    yield! gen "Scoped" [| genTryAddAsSelfAnd |]
    yield! gen "Singleton" [| genTryAddAsSelfAnd; genTryAddAsSelfAndWithInstance; genTryAddAsSelfAndWithFactory |]
}

let generateFiles() =
    generateAllLifetimesAndVerbs()
    |> Seq.iter ^ fun (filePath, code) ->
        Code.writeToFile filePath code
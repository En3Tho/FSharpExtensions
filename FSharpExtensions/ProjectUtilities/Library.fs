namespace ProjectUtilities

open System
open System.IO
open System.Text
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.SStringBuilderBuilder

// TODO: indent aware utility type based on string builder

module ServiceCollectionCodeGen =

    [<return: Struct>]
    let (|Not|_|) x =
        match x with
        | ValueSome() -> ValueNone
        | _ -> ValueSome()

    let generateAddFunc dependenciesCount (verb: string) =

        let makeGenericDependencies() =
            let deps = Array.init dependenciesCount ^ fun index -> $"TDependency{index + 1}"
            String.Join(", ", deps)

        let makeGetDependencies() =
            let deps = Array.init dependenciesCount ^ fun index -> $"serviceProvider.GetRequiredService<TDependency{index + 1}>()"
            String.Join("," + Environment.NewLine, deps)

        StringBuilder() {
            $"public static IServiceCollection {verb}Func<TService, {makeGenericDependencies()}>(this IServiceCollection collection,"; Environment.NewLine

            $"""Func<{makeGenericDependencies()}, TService> factory)
                where TService : class""" ; Environment.NewLine

            for i = 1 to dependenciesCount do
                $"where TDependency{i}: notnull"; Environment.NewLine
            "{"; Environment.NewLine

            match verb with
            | String.StartsWith("TryAdd") & String.EndsWithNot("OrFail") ->
                // this is a MS tryAdd implementations, they are all voids
                $"""collection.{verb}(
                        serviceProvider => factory("""; Environment.NewLine
                makeGetDependencies()
                "));"
                "return collection;"
            | _ ->
                $"""return collection.{verb}(
                        serviceProvider => factory("""; Environment.NewLine
                makeGetDependencies()
                "));"
            "}"
        } |> toString

    let generateFileForVerb filePath dependenciesCount verb =
        StringBuilder() {
            """
                using System;
                using Microsoft.Extensions.DependencyInjection;

                namespace En3Tho.Extensions.DependencyInjection;

                public static partial class IServiceCollectionExtensions
                {
            """

            for i = 1 to dependenciesCount do
                generateAddFunc i verb; Environment.NewLine; Environment.NewLine

            "}"
        } |> toString
        |> fun text -> File.WriteAllText(filePath, text)

    let generateAllLifetimeAndVerbs() =
        let lifetimes = [| "Singleton"; "Scoped"; "Transient" |]

        for lifetime in lifetimes do
            let verbs = [| $"Add{lifetime}"; $"TryAdd{lifetime}"; $"TryAdd{lifetime}OrFail" |]
            for verb in verbs do
                let count = 15
                let filePath = $"{verb}Func.cs"
                generateFileForVerb filePath count verb
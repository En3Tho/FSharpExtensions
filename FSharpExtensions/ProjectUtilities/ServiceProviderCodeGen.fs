namespace ProjectUtilities

open System
open System.IO
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

module ServiceProviderCodeGen =

    let makeActionOfTCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        code {
            $"public static void Run<{typeArgs}>(this IServiceProvider provider, Action<{typeArgs}> action)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
            }
            braceBlock {
                "action("
                indent {
                    for i = 1 to genericArgsCount - 1 do
                        $"provider.GetRequiredService<T{i}>(),"
                    $"provider.GetRequiredService<T{genericArgsCount}>()"
                }
                ");"
            }
        }

    let makeFuncTaskCode isValueTask genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = if isValueTask then "ValueTask" else "Task"

        code {
            $"public static {returnType} RunAsync<{typeArgs}>(this IServiceProvider provider, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
            }
            braceBlock {
                "return func("
                indent {
                    for i = 1 to genericArgsCount - 1 do
                        $"provider.GetRequiredService<T{i}>(),"
                    $"provider.GetRequiredService<T{genericArgsCount}>()"
                }
                ");"
            }
        }

    let makeFuncGenericTaskCode isValueTask genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = if isValueTask then "ValueTask" else "Task"

        code {
            $"public static {returnType}<TOut> RunAsync<{typeArgs}, TOut>(this IServiceProvider provider, Func<{typeArgs}, {returnType}<TOut>> func)"
            indent {
               for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
            }
            braceBlock {
                "return func("
                indent {
                    for i = 1 to genericArgsCount - 1 do
                        $"provider.GetRequiredService<T{i}>(),"
                    $"provider.GetRequiredService<T{genericArgsCount}>()"
                }
                ");"
            }
        }

module ServiceScopeCodeGen =

    let makeActionOfTCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        code {
            $"public static void Run<{typeArgs}>(this IServiceScope scope, Action<{typeArgs}> action)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
            }
            braceBlock {
                "scope.ServiceProvider.Run(action);"
            }
        }

    let makeFuncTaskCode isValueTask genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = if isValueTask then "ValueTask" else "Task"

        code {
            $"public static {returnType} Run<{typeArgs}>(this IServiceScope scope, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
            }
            braceBlock {
                "return scope.ServiceProvider.RunAsync(func);"
            }
        }

    let makeFuncGenericTaskCode isValueTask genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = if isValueTask then "ValueTask" else "Task"

        code {
            $"public static {returnType}<TOut> Run<{typeArgs}, TOut>(this IServiceScope scope, Func<{typeArgs}, {returnType}<TOut>> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
            }
            braceBlock {
                "return scope.ServiceProvider.RunAsync(func);"
            }
        }

module CodeGen =

    let serviceProviderCode =

        let generators = [|
            ServiceProviderCodeGen.makeActionOfTCode
            ServiceProviderCodeGen.makeFuncTaskCode false
            ServiceProviderCodeGen.makeFuncGenericTaskCode false
            ServiceProviderCodeGen.makeFuncTaskCode true
            ServiceProviderCodeGen.makeFuncGenericTaskCode true
        |]

        code {
            "public static partial class IServiceProviderExtensions"
            braceBlock {
                for generator in generators do
                    for i = 1 to 15 do
                        generator i
                        ""
            }
        }

    let serviceScopeCode =

        let generators = [|
            ServiceScopeCodeGen.makeActionOfTCode
            ServiceScopeCodeGen.makeFuncTaskCode false
            ServiceScopeCodeGen.makeFuncGenericTaskCode false
            ServiceScopeCodeGen.makeFuncTaskCode true
            ServiceScopeCodeGen.makeFuncGenericTaskCode true
        |]

        code {
            "public static partial class IServiceScopeExtensions"
            braceBlock {
                for generator in generators do
                    for i = 1 to 15 do
                        generator i
                        ""
            }
        }

    let generateFileForCodeBlock fileName (codeBlock: CodeBuilderImpl.CodeBuilder) =
        code {
            "using System;"
            "using System.Threading.Tasks;"
            "using Microsoft.Extensions.DependencyInjection;"
            ""
            "namespace En3Tho.Extensions.DependencyInjection;"
            ""
            codeBlock
        }
        |> toString
        |> fun text -> File.WriteAllText(fileName, text)

    let generateFiles() =
        generateFileForCodeBlock "IServiceProviderExtensions.cs" serviceProviderCode
        generateFileForCodeBlock "IServiceScopeExtensions.cs" serviceScopeCode
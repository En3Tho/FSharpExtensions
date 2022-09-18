namespace ProjectUtilities

open System.IO
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

module ServiceProviderCodeGen =

    let makeActionCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        code {
            $"public static void Run<{typeArgs}>(this IServiceProvider provider, Action<{typeArgs}> action)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : class"
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

    let makeFuncCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = "TOut"

        code {
            $"public static {returnType} Run<{typeArgs}, {returnType}>(this IServiceProvider provider, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : class"
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

    let makeFuncTaskCode isValueTask genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = if isValueTask then "ValueTask" else "Task"

        code {
            $"public static {returnType} RunAsync<{typeArgs}>(this IServiceProvider provider, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : class"
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

        let returnType = if isValueTask then "ValueTask<TOut>" else "Task<TOut>"

        code {
            $"public static {returnType} RunAsync<{typeArgs}, TOut>(this IServiceProvider provider, Func<{typeArgs}, {returnType}> func)"
            indent {
               for i = 1 to genericArgsCount do
                    $"where T{i} : class"
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

    let makeActionCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        code {
            $"public static void Run<{typeArgs}>(this IServiceScope scope, Action<{typeArgs}> action)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : class"
            }
            braceBlock {
                "scope.ServiceProvider.Run(action);"
            }
        }

    let makeFuncCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = "TOut"

        code {
            $"public static {returnType} Run<{typeArgs}, {returnType}>(this IServiceScope scope, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : class"
            }
            braceBlock {
                "return scope.ServiceProvider.Run(func);"
            }
        }

    let makeFuncTaskCode isValueTask genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = if isValueTask then "ValueTask" else "Task"

        code {
            $"public static {returnType} RunAsync<{typeArgs}>(this IServiceScope scope, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : class"
            }
            braceBlock {
                "return scope.ServiceProvider.RunAsync(func);"
            }
        }

    let makeFuncGenericTaskCode isValueTask genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = if isValueTask then "ValueTask<TOut>" else "Task<TOut>"

        code {
            $"public static {returnType} RunAsync<{typeArgs}, TOut>(this IServiceScope scope, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : class"
            }
            braceBlock {
                "return scope.ServiceProvider.RunAsync(func);"
            }
        }

module ServiceProviderAndScopeCodeGen =

    let serviceProviderCode =

        let generators = [|
            ServiceProviderCodeGen.makeActionCode
            ServiceProviderCodeGen.makeFuncCode

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
            ServiceScopeCodeGen.makeActionCode
            ServiceScopeCodeGen.makeFuncCode

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
        let dirName = ".artifacts"
        if not ^ Directory.Exists dirName then Directory.CreateDirectory(dirName) |> ignore
        generateFileForCodeBlock $"{dirName}/IServiceProviderExtensions.cs" serviceProviderCode
        generateFileForCodeBlock $"{dirName}/IServiceScopeExtensions.cs" serviceScopeCode
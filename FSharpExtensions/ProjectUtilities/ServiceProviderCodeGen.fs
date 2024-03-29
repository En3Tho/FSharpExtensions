namespace ProjectUtilities

open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

module ServiceProviderCodeGen =

    let makeActionCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        codeBlock {
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

    let makeFuncCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = "TOut"

        codeBlock {
            $"public static {returnType} Run<{typeArgs}, {returnType}>(this IServiceProvider provider, Func<{typeArgs}, {returnType}> func)"
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

    let makeFuncTaskCode isValueTask genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        let returnType = if isValueTask then "ValueTask" else "Task"

        codeBlock {
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

        let returnType = if isValueTask then "ValueTask<TOut>" else "Task<TOut>"

        codeBlock {
            $"public static {returnType} RunAsync<{typeArgs}, TOut>(this IServiceProvider provider, Func<{typeArgs}, {returnType}> func)"
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

    let makeActionCode genericArgsCount =
        let typeArgs =
            Array.init genericArgsCount ^ fun index -> $"T{index + 1}"
            |> String.concat ", "

        codeBlock {
            $"public static void Run<{typeArgs}>(this IServiceScope scope, Action<{typeArgs}> action)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
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

        codeBlock {
            $"public static {returnType} Run<{typeArgs}, {returnType}>(this IServiceScope scope, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
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

        codeBlock {
            $"public static {returnType} RunAsync<{typeArgs}>(this IServiceScope scope, Func<{typeArgs}, {returnType}> func)"
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

        let returnType = if isValueTask then "ValueTask<TOut>" else "Task<TOut>"

        codeBlock {
            $"public static {returnType} RunAsync<{typeArgs}, TOut>(this IServiceScope scope, Func<{typeArgs}, {returnType}> func)"
            indent {
                for i = 1 to genericArgsCount do
                    $"where T{i} : notnull"
            }
            braceBlock {
                "return scope.ServiceProvider.RunAsync(func);"
            }
        }

module ServiceProviderAndScopeCodeGen =

    let generateServiceProviderCode() =

        let generators = [|
            ServiceProviderCodeGen.makeActionCode
            ServiceProviderCodeGen.makeFuncCode

            ServiceProviderCodeGen.makeFuncTaskCode false
            ServiceProviderCodeGen.makeFuncGenericTaskCode false

            ServiceProviderCodeGen.makeFuncTaskCode true
            ServiceProviderCodeGen.makeFuncGenericTaskCode true
        |]

        codeBlock {
            "public static class IServiceProviderExtensions"
            braceBlock {
                for generator in generators do
                    for i = 1 to 16 do
                        generator i
                        ""
                trimEnd()
            }
        }

    let generateServiceScopeCode() =

        let generators = [|
            ServiceScopeCodeGen.makeActionCode
            ServiceScopeCodeGen.makeFuncCode

            ServiceScopeCodeGen.makeFuncTaskCode false
            ServiceScopeCodeGen.makeFuncGenericTaskCode false

            ServiceScopeCodeGen.makeFuncTaskCode true
            ServiceScopeCodeGen.makeFuncGenericTaskCode true
        |]

        codeBlock {
            "public static class IServiceScopeExtensions"
            braceBlock {
                for generator in generators do
                    for i = 1 to 16 do
                        generator i
                        ""
                trimEnd()
            }
        }

    let generateFileCode (codeBlock: CodeBuilderImpl.CodeBuilderCode) =
        code {
            "// auto-generated"
            "using Microsoft.Extensions.DependencyInjection;"
            ""
            "namespace En3Tho.Extensions.DependencyInjection;"
            ""
            codeBlock
        }

    let generateFiles() =
        generateServiceProviderCode()
        |> generateFileCode
        |> Code.writeToFile "IServiceProviderExtensions.cs"

        generateServiceScopeCode()
        |> generateFileCode
        |> Code.writeToFile "IServiceScopeExtensions.cs"
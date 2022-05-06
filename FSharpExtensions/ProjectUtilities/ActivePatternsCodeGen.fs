module ProjectUtilities.ActivePatternsCodeGen

open System
open System.Reflection
open ProjectUtilities.CodeBuilder.CodeBuilder

let generatePropertyActivePatterns(type': Type) =
    match type'.Name with
    | name when name.Contains("<") || name.Contains(">") ->
        None
    | _ ->
        match type'.GetProperties(BindingFlags.Public ||| BindingFlags.Instance) with
        | [||] ->
            None
        | properties ->
            code {
                $"module {type'.Name} ="
                indent {
                    for p in properties do
                        $"let inline {p.Name} a = (^a: (member {p.Name}: {p.PropertyType}) a)"

                    for p in properties do
                        $"let inline (|{p.Name}|) a = (^a: (member {p.Name}: {p.PropertyType}) a)"
                }
            } |> Some
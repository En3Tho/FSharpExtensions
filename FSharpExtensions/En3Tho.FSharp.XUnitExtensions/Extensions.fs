[<AutoOpen>]
module En3Tho.FSharp.Xunit.Extensions

open Xunit

type Assert with
    static member IsValueSome<'a>(value: 'a voption) =
        if value.IsNone then Assert.Fail("Expected ValueSome, got ValueNone")
    static member IsValueSome<'a>(expected: 'a, value: 'a voption) =
        match value with ValueSome result -> Assert.Equal(expected, result) | _ -> ()
    static member IsValueNone<'a>(value: 'a voption) =
        if value.IsSome then Assert.Fail($"Expected ValueNone, got ValueSome: {value.Value}")

    static member IsSome<'a>(value: 'a option) =
        if value.IsNone then Assert.Fail("Expected Some, got ValueNone")
    static member IsSome<'a>(expected: 'a, value: 'a option) =
        match value with Some result -> Assert.Equal(expected, result) | _ -> ()
    static member IsNone<'a>(value: 'a option) =
        if value.IsSome then Assert.Fail($"Expected None, got Some: {value.Value}")

    static member IsOk<'a, 'b>(value: Result<'a, 'b>) =
        match value with Error result  -> Assert.Fail($"Expected Ok, got Error: {result}") | _ -> ()
    static member IsOk<'a, 'b>(expected: 'a, value: Result<'a, 'b>) =
        match value with | Ok result -> Assert.Equal(result, expected) | Error result -> Assert.Fail($"Expected Ok, got Error: {result}")

    static member IsError<'a, 'b>(value: Result<'a, 'b>) =
        match value with | Ok result -> Assert.Fail($"Expected Error, got Ok: {result}") | _ -> ()
    static member IsError<'a, 'b>(expected: 'b, value: Result<'a, 'b>) =
        match value with | Error result -> Assert.Equal(result, expected) | Ok result -> Assert.Fail($"Expected Error, got Ok: {result}")

    static member IsErrorOfType<'a, 'b when 'a :> exn>(value: Result<'b, exn>) =
        match value with
        | Error (:? 'a as _) -> ()
        | Error result -> Assert.Fail($"Expected Error of {typeof<'b>.Name} but got Error: {result}")
        | Ok result -> Assert.Fail($"Expected Error, got Ok: {result}")
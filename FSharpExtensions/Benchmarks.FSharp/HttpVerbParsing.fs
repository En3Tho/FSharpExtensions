module Benchmarks.HttpVerbParsing

open System
open System.Runtime.CompilerServices
open System.Text
open BenchmarkDotNet.Attributes
open Benchmarks.Lib

module HttpChecker =

    let [<Literal>] C = 67uy

    let [<Literal>] P = 80uy
    let [<Literal>] U = 85uy
    let [<Literal>] A = 65uy

    let [<Literal>] G = 71uy
    let [<Literal>] H = 72uy

    let [<Literal>] D = 68uy
    let [<Literal>] O = 79uy
    let [<Literal>] T = 84uy

    let [<Literal>] NewLine = 10uy

    let connect = Encoding.UTF8.GetBytes("CONNECT")

    let post = Encoding.UTF8.GetBytes("POST")
    let put = Encoding.UTF8.GetBytes("PUT")
    let patch = Encoding.UTF8.GetBytes("PATCH")

    let get = Encoding.UTF8.GetBytes("GET")
    let head = Encoding.UTF8.GetBytes("HEAD")

    let delete = Encoding.UTF8.GetBytes("DELETE")
    let options = Encoding.UTF8.GetBytes("OPTIONS")
    let trace = Encoding.UTF8.GetBytes("TRACE")

    let getHttpVerbLength (span: ReadOnlySpan<byte>) =
        let mutable bytes = null
        match span[0] with
        | C -> bytes <- connect
        | G -> bytes <- get
        | P ->
            match span[1] with
            | O -> bytes <- post
            | U -> bytes <- put
            | A -> bytes <- patch
            | _ -> ()
        | D -> bytes <- delete
        | H -> bytes <- head
        | O -> bytes <- options
        | T -> bytes <- trace
        | _ -> ()
        if not (Object.ReferenceEquals(bytes, null)) && span.StartsWith(bytes) then
            bytes.Length
        else
            0

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
[<SimpleJob>]
type Benchmark() =

    member val Bytes = null with get, set

    [<GlobalSetup>]
    member this.GlobalSetup() =
        this.Bytes <- Encoding.UTF8.GetBytes("TRACE blabla")

    [<Benchmark>]
    member this.GetHttpVerbLengthLoop() =
        let span = this.Bytes.AsSpan()
        let value = Unsafe.As(&Unsafe.AsRef(&span[0]))
        HttpVerbsWithLength.GetHttpVerbLengthWithLoop(value)

    [<Benchmark>]
    member this.GetHttpVerbLengthIfElse() =
        let span = this.Bytes.AsSpan()
        let value = Unsafe.As(&Unsafe.AsRef(&span[0]))
        HttpVerbsAvx2.GetHttpVerbLengthIfElse(value)

    [<Benchmark>]
    member this.GetHttpVerbLengthAvx2() =
        let span = this.Bytes.AsSpan()
        let value = Unsafe.As(&Unsafe.AsRef(&span[0]))
        HttpVerbsAvx2.GetHttpVerbLengthAvx2(value)

    [<Benchmark>]
    member this.GetHttpVerbLengthAvx2_2() =
        let span = this.Bytes.AsSpan()
        let value = Unsafe.As(&Unsafe.AsRef(&span[0]))
        HttpVerbsAvx2.GetHttpVerbLengthAvx2_2(value)

    [<Benchmark>]
    member this.GetHttpVerbLengthArrays() =
        HttpChecker.getHttpVerbLength this.Bytes
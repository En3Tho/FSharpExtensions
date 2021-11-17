module En3Tho.FSharp.Extensions.Tests.AsyncTests

open System.IO
open Xunit
open En3Tho.FSharp.ComputationExpressions.Tasks
open En3Tho.FSharp.Extensions

[<Fact>]
let ``Test that streams are disposed asynchronously``() = vtask {
    do! File.OpenRead("") |> useAsync ^ fun stream -> vtask {
        do! new MemoryStream() |> useAsync ^ fun ms -> vtask {
            do! stream.CopyToAsync(ms)
        }
    }
}
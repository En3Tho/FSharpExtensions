module En3Tho.FSharp.ComputationExpressions.Tests.GenericTaskBuilder2Tests

open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.Tasks

open GenericTaskBuilderExtensionsLowPriority
open GenericTaskBuilder2ExtensionsMediumPriority
open GenericTaskBuilder2ExtensionsHighPriority

open Xunit

let vtask2 = ValueTaskBuilder2()
let unitvtask2 = UnitValueTaskBuilder2()
let taskSeq = TaskSeqBuilder()

[<Fact>]
let ``test that simple task seq works``() = task {

    let ts = taskSeq {
        1
        do! Task.Delay(1)
        2
        do! unitvtask2 { do! Task.Delay(1) }
        3
    }

    let! res = vtask2 {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)

    let! res = vtask2 {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)

    let! res = vtask2 {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)
}
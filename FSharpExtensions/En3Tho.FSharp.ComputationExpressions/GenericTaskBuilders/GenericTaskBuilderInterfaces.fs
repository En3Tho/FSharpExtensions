namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type StateIntrinsic = struct end

[<AutoOpen>]
module Intrinsics =
    let inline getState() = StateIntrinsic()

type BasicBindExtensions = struct end

type YieldExtensions = struct end
type ReturnExtensions = struct end
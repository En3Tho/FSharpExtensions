namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type StateIntrinsic = struct end

[<AutoOpen>]
module Intrinsics =
    let inline getState() = StateIntrinsic()

type IMoveNextStateCheck<'TState> =
    static abstract CheckState: state: 'TState -> unit

[<Struct>]
type IgnoreStateCheck<'State> =
    interface IMoveNextStateCheck<'State> with
        static member CheckState(_) = ()

type BasicBindExtensions = struct end
namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core

type GenericTaskBuilderWithStateReturnBase<'TState>(state: 'TState) =
    inherit GenericTaskBuilderReturnCore<'TState>(state)
    interface IBindExtensions
    interface IReturnExtensions

type GenericTaskBuilderBase() =
    inherit GenericTaskBuilderWithStateReturnBase<unit>()

type GenericTaskSeqBuilderWithStateBase<'TState>(state: 'TState) =
    inherit GenericTaskBuilderYieldCore<'TState>(state)
    interface IBindExtensions
    interface IYieldExtensions

type GenericTaskBuilderYieldBase() =
    inherit GenericTaskSeqBuilderWithStateBase<unit>()
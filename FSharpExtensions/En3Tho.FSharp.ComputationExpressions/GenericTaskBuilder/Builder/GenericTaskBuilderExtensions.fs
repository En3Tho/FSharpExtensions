namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions

open System
open System.Collections.Generic
open System.ComponentModel
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

module Low =

    type GenericTaskBuilderCore with
        member inline this.Using<'TData, 'TResult, 'TResource
            when 'TData :> IStateMachineDataWithCheck<'TData>
            and 'TResource :> IDisposable>
            (resource: 'TResource, [<InlineIfLambda>] body: 'TResource -> ResumableCode<'TData, 'TResult>) =
            ResumableCodeHelpers.Using(resource, body)

        member inline this.For(sequence: IAsyncEnumerable<'T>, [<InlineIfLambda>] body: 'T -> ResumableCode<'TData, unit>) : ResumableCode<'TData, unit> =
            this.Using(
                sequence.GetAsyncEnumerator(),
                (fun e ->
                    ResumableCodeHelpers.WhileAsync(
                        (fun () ->
                            __debugPoint "ForLoop.InOrToKeyword"
                            e.MoveNextAsync()),
                        ResumableCode<'TData, unit>(fun sm -> (body e.Current).Invoke(&sm))
                    ))
            )

    [<AbstractClass; Sealed; Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    type LowPriorityImpl() =

        [<NoEagerConstraintApplication>]
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Bind(_: #IBindExtensions, task: ^TaskLike, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>)
            : ResumableCode<'TData, 'TResult2> =
            ResumableCodeHelpers.Bind(task, continuation)

        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Return<'TData, 'TResult, 'TExtensions
            when 'TData :> IStateMachineData<'TData, 'TResult>
            and 'TExtensions :> IReturnExtensions>(_: 'TExtensions, value: 'TResult) =
            ResumableCode<'TData, 'TResult>(fun sm ->
                sm.Data.SetResult(value)
                true)

        [<NoEagerConstraintApplication; Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline ReturnFrom(this: #IReturnExtensions, task: ^TaskLike) =
            this.Bind(task, (fun v -> this.Return v))

        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Yield<'TData, 'TResult, 'TExtensions
            when 'TData :> IStateMachineDataYield<'TData, 'TResult>
            and 'TExtensions :> IYieldExtensions>(_: 'TExtensions, value: 'TResult) =
                ResumableCode<'TData, unit>(fun sm ->
                    if sm.Data.CheckCanContinueOrThrow() then
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        if not __stack_yield_fin then
                            sm.Data.SetResult(value)
                        __stack_yield_fin
                    else
                        true)

        [<NoEagerConstraintApplication; Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom (this: #IYieldExtensions, task: ^TaskLike) =
            this.Bind(task, (fun v -> this.Yield v))

        [<NoEagerConstraintApplication; Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom<'TBuilder, 'TResult, 'TData
            when 'TBuilder :> GenericTaskBuilderCore
            and 'TBuilder :> IYieldExtensions
            and 'TData :> IAsyncMethodBuilderBase
            and 'TData :> IStateMachineDataWithCheck<'TData>
            and 'TData :> IStateMachineDataYield<'TData,'TResult>>(this: 'TBuilder, asyncSeq: IAsyncEnumerable<'TResult>) =
            this.For<'TResult, 'TData>(asyncSeq, (fun v -> this.Yield v))

        [<NoEagerConstraintApplication; Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom<'TBuilder, 'TResult, 'TData
            when 'TBuilder :> GenericTaskBuilderCore
            and 'TBuilder :> IYieldExtensions
            and 'TData :> IAsyncMethodBuilderBase
            and 'TData :> IStateMachineDataWithCheck<'TData>
            and 'TData :> IStateMachineDataYield<'TData,'TResult>>(this: 'TBuilder, seq: IEnumerable<'TResult>) =
            this.For<'TData, 'TResult>(seq, (fun v -> this.Yield v))

module High =
    open Low

    [<AbstractClass; Sealed; Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    type HighPriorityImpl() =

        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Bind<'TData, 'TResult1, 'TResult2, 'TExtensions, 'TDataResult
            when 'TData :> IAsyncMethodBuilderBase
            and 'TData :> IStateMachineData<'TData, 'TDataResult>
            and 'TExtensions :> IBindExtensions>
            (this: 'TExtensions, task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
                this.Bind(TaskBindWrapper(task), continuation)

        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline ReturnFrom(this: #IReturnExtensions, task: Task<'TResult>) =
            this.Bind(task, (fun v -> this.Return v))

        [<NoEagerConstraintApplication; Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom (this: #IYieldExtensions, task: Task<'TResult>) =
            this.Bind(task, (fun v -> this.Yield v))

        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Bind(this: #IBindExtensions, computation: Async<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind (Async.StartAsTask computation, continuation)

        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline ReturnFrom(this: #IReturnExtensions, computation: Async<'TResult>) =
            this.ReturnFrom(Async.StartAsTask computation)

        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom (this: #IYieldExtensions, computation: Async<'TResult>) =
            this.YieldFrom (Async.StartAsTask computation)
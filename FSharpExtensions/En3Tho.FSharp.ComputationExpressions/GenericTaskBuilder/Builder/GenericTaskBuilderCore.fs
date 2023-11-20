namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type GenericTaskBuilderCore() =

    [<DefaultValue>]
    member inline _.Zero() : ResumableCode<'TData, unit> =
        ResumableCode.Zero()

    member inline _.Combine(
        [<InlineIfLambda>] task1: ResumableCode<'TData, unit>,
        [<InlineIfLambda>] task2: ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCodeHelpers.Combine(task1, task2)

    member inline _.While(
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
            ResumableCodeHelpers.While(condition, body)

    member inline _.For<'TData, 'T when 'TData :> IStateMachineDataWithCheck<'TData>>(
        sequence: seq<'T>,
        [<InlineIfLambda>] body: 'T -> ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
            ResumableCodeHelpers.Using(
                sequence.GetEnumerator(),
                (fun e ->
                    ResumableCodeHelpers.While(
                        (fun () ->
                            __debugPoint "ForLoop.InOrToKeyword"
                            e.MoveNext()),
                        ResumableCode<'TData, unit>(fun sm -> (body e.Current).Invoke(&sm))
                    ))
            )

    member inline _.TryWith<'TData, 'TResult when 'TData :> IStateMachineDataWithCheck<'TData>>(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] catch: exn -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
            ResumableCodeHelpers.TryWith(body, catch)

    member inline _.TryFinally<'TData, 'TResult when 'TData :> IStateMachineDataWithCheck<'TData>>(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : ResumableCode<'TData, 'TResult> =
           ResumableCodeHelpers.TryFinally(body, ResumableCode(fun _sm -> compensation(); true))

    member inline this.Using<'TData, 'TResource, 'TResult
        when 'TData :> IAsyncMethodBuilderBase
        and 'TData :> IStateMachineDataWithCheck<'TData>
        and 'TResource :> IAsyncDisposable>(
        resource: 'TResource,
        [<InlineIfLambda>] body: 'TResource -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCodeHelpers.TryFinallyAsyncUsing<'TData, 'TResult>(
            ResumableCode<'TData, 'TResult>(fun sm -> (body resource).Invoke(&sm)),
            (fun () ->
                if not (isNull (box resource)) then
                    resource.DisposeAsync()
                else
                    ValueTask()))
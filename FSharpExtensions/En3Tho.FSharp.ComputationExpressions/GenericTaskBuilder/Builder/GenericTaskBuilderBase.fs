namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type GenericTaskBuilderCore() =

    member inline _.Delay([<InlineIfLambda>] generator: unit -> ResumableCode<'TData, 'TResult>) =
        ResumableCode(fun sm -> (generator()).Invoke(&sm))

    [<DefaultValue>]
    member inline _.Zero() : ResumableCode<'TData, unit> =
        ResumableCode.Zero()

    member inline _.Combine(
        [<InlineIfLambda>] task1: ResumableCode<'TData, unit>,
        [<InlineIfLambda>] task2: ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCodeHelpers.Combine(task1, task2)

    member inline _.While<'TData when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
            ResumableCodeHelpers.While(condition, body)

    member inline _.For<'TData, 'T when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(
        sequence: seq<'T>,
        [<InlineIfLambda>] body: 'T -> ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
            ResumableCode.Using(
                sequence.GetEnumerator(),
                (fun e ->
                    ResumableCodeHelpers.While(
                        (fun () ->
                            __debugPoint "ForLoop.InOrToKeyword"
                            e.MoveNext()),
                        ResumableCode<'TData, unit>(fun sm -> (body e.Current).Invoke(&sm))
                    ))
            )

    member inline _.TryWith<'TData, 'TResult when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] catch: exn -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
            ResumableCode(fun sm ->
                if sm.Data.CheckCanContinueOrThrow() then
                    ResumableCode.TryWith(body, catch).Invoke(&sm)
                else
                    true)

    member inline _.TryFinally<'TData, 'TResult when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : ResumableCode<'TData, 'TResult> =
           ResumableCodeHelpers.TryFinally(body, ResumableCode(fun _sm -> compensation(); true))

    member inline this.Using<'TData, 'TResource, 'TResult
        when 'TData :> IAsyncMethodBuilderBase
        and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>
        and 'TResource :> IAsyncDisposable>(
        resource: 'TResource,
        [<InlineIfLambda>] body: 'TResource -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCodeHelpers.TryFinallyAsync<'TData, 'TResult>(
            ResumableCode<'TData, 'TResult>(fun sm -> (body resource).Invoke(&sm)),
            (fun () ->
                if not (isNull (box resource)) then
                    resource.DisposeAsync()
                else
                    ValueTask()))

type GenericTaskBuilderCore<'TExtensionsMarker>() =
    inherit GenericTaskBuilderCore()
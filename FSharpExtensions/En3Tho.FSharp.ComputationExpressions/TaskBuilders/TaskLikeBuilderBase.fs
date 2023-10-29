namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators
open Microsoft.FSharp.Collections

type TaskLikeBuilderBase() =
    member inline _.Delay([<InlineIfLambda>] generator: unit -> ResumableCode<'TData, 'TResult>) =
        ResumableCode(fun sm -> (generator()).Invoke(&sm))
    
    [<DefaultValue>]
    member inline _.Zero() : ResumableCode<'TData, unit> =
        ResumableCode.Zero()
    
    member inline _.Combine(
        [<InlineIfLambda>] task1: ResumableCode<'TData, unit>,
        [<InlineIfLambda>] task2: ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCode.Combine(task1, task2)
    
    member inline _.While(
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
        ResumableCode.While(condition, body)
    
    member inline _.TryWith(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] catch: exn -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCode.TryWith(body, catch)
    
    member inline _.TryFinally(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : ResumableCode<'TData, 'TResult> =
        ResumableCode.TryFinally(body, ResumableCode(fun _sm -> compensation(); true))
    
    member inline _.For(
        sequence: seq<'T>,
        [<InlineIfLambda>] body: 'T -> ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
        ResumableCode.For(sequence, body)
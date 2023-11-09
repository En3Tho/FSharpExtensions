namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open Microsoft.FSharp.Core.CompilerServices

[<Struct; NoComparison; NoEquality>]
type GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>> =

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

and GenericTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> =
        ResumableStateMachine<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>

and GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> =
        ResumptionFunc<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>

and GenericTaskResumptionDynamicInfo<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> =
        ResumptionDynamicInfo<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>

and GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> =
        ResumableCode<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>, 'TResult>
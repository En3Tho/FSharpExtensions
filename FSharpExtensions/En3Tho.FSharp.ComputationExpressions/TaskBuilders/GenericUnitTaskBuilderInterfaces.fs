namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open Microsoft.FSharp.Core.CompilerServices

[<Struct; NoComparison; NoEquality>]
type GenericUnitTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

and GenericUnitTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>> =
        ResumableStateMachine<GenericUnitTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask>>

and GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>> =
        ResumptionFunc<GenericUnitTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask>>

and GenericUnitTaskResumptionDynamicInfo<'TMethodBuilder, 'TAwaiter, 'TTask
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>> =
        ResumptionDynamicInfo<GenericUnitTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask>>

and GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>> =
        ResumableCode<GenericUnitTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask>, 'TResult>
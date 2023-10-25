namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices

module GenericTaskBuilderExtensionsLowPriority =

    type GenericTaskBuilderBase with
        [<NoEagerConstraintApplication>]
        member inline _.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
            and 'TAwaiter :> ITaskAwaiter<'TOverall>
            and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>
            and 'Resource :> IDisposable>(resource: 'Resource,
                [<InlineIfLambda>] body: 'Resource -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>) =
            ResumableCode.Using(resource, body)
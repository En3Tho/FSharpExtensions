namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices

module GenericUnitTaskBuilderExtensionsLowPriority =

    type GenericUnitTaskBuilderBase with
        [<NoEagerConstraintApplication>]
        member inline _.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
            and 'TAwaiter :> ITaskAwaiter
            and 'TTask :> ITaskLike<'TAwaiter>
            and 'Resource :> IDisposable>(
                resource: 'Resource,
                [<InlineIfLambda>] body: 'Resource -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>) =
            ResumableCode.Using(resource, body)
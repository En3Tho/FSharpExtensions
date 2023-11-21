namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions

// open System.Runtime.CompilerServices
// open En3Tho.FSharp.ComputationExpressions.Tasks
// open Microsoft.FSharp.Core
// open Microsoft.FSharp.Core.CompilerServices
// open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

// this is quite painful with tasks actually, I'm not sure I want to expose this
// module MultiBind =
//
//     [<AbstractClass; Sealed; Extension>]
//     type LowPriorityImpl() =
//
//         [<NoEagerConstraintApplication>]
//         [<Extension>]
//         static member inline Bind2(_: #IBindExtensions, task: ^TaskLike, task2: ^TaskLike2, [<InlineIfLambda>] continuation: 'TResult1 * 'TResult2 -> ResumableCode<'TData, 'TResult3>)
//             : ResumableCode<'TData, 'TResult3> =
//                 ResumableCode<'TData, 'TResult3>(fun sm ->
//                     let mutable ma = MultiAwaiterData()
//
//                     let awaiter1 = ResumableCodeHelpers.getAwaiter task
//                     if not (ResumableCodeHelpers.isCompleted awaiter1) then
//                         ma.AddAwaiter(awaiter1)
//
//                     let awaiter2 = ResumableCodeHelpers.getAwaiter task2
//                     if not (ResumableCodeHelpers.isCompleted awaiter2) then
//                         ma.AddAwaiter(awaiter2)
//
//                     ResumableCodeHelpers.Bind(ma, fun () ->
//                         let res1 = ResumableCodeHelpers.getResult awaiter1
//                         let res2 = ResumableCodeHelpers.getResult awaiter2
//                         continuation (res1, res2)
//                         ).Invoke(&sm))
namespace En3Tho.FSharp.Extensions.Experimental

module PipeAndCompositionOperatorEx =

    [<AutoOpen>]
    module Pipe1 =

        type T = T with
            static member inline ($) (_, invokable) = fun value -> (^a: (member Invoke: ^b -> ^c) invokable, value)
            static member inline ($) (_, [<InlineIfLambda>] invokable: 'a -> 'b) = fun value -> invokable value

        let inline (|>) value invokable = (T $ invokable) value
        let inline (>>) f1 f2 = fun value -> (T $ f2) ((T $ f1) value)
        let inline (<<) f1 f2 = fun value -> (T $ f1) ((T $ f2) value)

    [<AutoOpen>]
    module Pipe2 =

        type T = T with
            static member inline ($) (T, invokable: ^a) = fun (value1, value2) -> (^a: (member Invoke: 'b * 'c -> 'd) invokable, value1, value2)
            static member inline ($) (T, [<InlineIfLambda>] invokable: 'a -> 'b -> 'c) = fun (value1, value2) -> invokable value1 value2

        let inline (||>) (value1: 'b, value2: 'c) invokable =
            (T $ invokable) (value1, value2)

    [<AutoOpen>]
    module Pipe3 =

        type T = T with
            static member inline ($) (T, invokable: ^a) = fun (value1, value2, value3) -> (^a: (member Invoke: 'b * 'c * 'd -> 'e) invokable, value1, value2, value3)
            static member inline ($) (T, [<InlineIfLambda>] invokable: 'a -> 'b -> 'c -> 'd) = fun (value1, value2, value3) -> invokable value1 value2 value3

        let inline (|||>) (value1: 'b, value2: 'c, value3: 'd) invokable =
            (T $ invokable) (value1, value2, value3)
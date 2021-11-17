namespace En3Tho.FSharp.Extensions.Experimental

module PipeAndCompositionOperatorEx =

    type T = T with
        static member inline ($) (T, invokable: ^a) = fun value -> (^a: (member Invoke: 'b -> 'c) invokable, value)
        static member inline ($) (T, invokable: 'a -> 'b) = fun value -> invokable value

    let inline (|>) (value: 'b) invokable  =
            (T $ invokable) value

    let inline (>>) f1 f2 =
        fun x ->
            let y = (T $ f1) x
            (T $ f2) y
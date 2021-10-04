namespace En3Tho.FSharp.Validation

open En3Tho.FSharp.Extensions

module Validated =
    let mapTry map (entity: Validated10<'a,'b>) =
        entity.Value |> map |> Validated10<'a,'b>.Try
    let mapMake map (entity: Validated10<'a,'b>) =
        entity.Value |> map |> Validated10<'a,'b>.Try |> EResult.unwrap

[<AutoOpen>]
module ValidatedExtensions =
    type Validated10<'value, 'validator when 'validator: struct
                                         and 'validator: (new: unit -> 'validator)
                                         and 'validator :> IValidator<'value>> with
        static member Try value =
            match value with
            | ValueNone -> ValueNone |> Ok
            | ValueSome value ->
                match Validated10.Try value with
                | Ok value -> value |> ValueSome |> Ok
                | Error err -> Error err

        member this.MapTry map =
            Validated.mapTry map this

        static member Make value =
            value |> Validated10.Try |> EResult.unwrap

        member this.MapMake map =
            Validated.mapMake map this

    type Validated01<'value, 'validator when 'validator :> IValidator<'value>> with
        static member Try (value, validator) =
            match value with
            | ValueNone -> ValueNone |> Ok
            | ValueSome value ->
                match Validated01.Try(value, validator) with
                | Ok value -> value |> ValueSome |> Ok
                | Error err -> Error err

        member this.MapTry (map, validator) =
            Validated01.Try (map this.Value, this.Validator)

        static member Make (value, validator) =
            Validated01.Try(value, validator) |> EResult.unwrap

        member this.MapMake map =
            Validated01.Make (map this.Value, this.Validator)

    type Validated20<'value, 'validator, 'validator2 when 'validator: struct
                                                      and 'validator: (new: unit -> 'validator)
                                                      and 'validator :> IValidator<'value>
                                                      and 'validator2: struct
                                                      and 'validator2: (new: unit -> 'validator2)
                                                      and 'validator2 :> IValidator<'value>> with
        static member Try value =
            match value with
            | ValueNone -> ValueNone |> Ok
            | ValueSome value ->
                match Validated20<'value, 'validator, 'validator2>.Try value with
                | Ok value -> value |> ValueSome |> Ok
                | Error err -> Error err

        member this.MapTry map =
            this.Value |> map |> Validated20<'value, 'validator, 'validator2>.Try

        static member Make value =
            value |> Validated20<'value, 'validator, 'validator2>.Try |> EResult.unwrap

        member this.MapMake map =
            this.Value |> map |> Validated20.Make

    type Validated02<'value, 'validator, 'validator2 when 'validator :> IValidator<'value>
                                                      and 'validator2 :> IValidator<'value>> with
        static member Try(value, validator, validator2) =
            match value with
            | ValueNone -> ValueNone |> Ok
            | ValueSome value ->
                match Validated02<'value, 'validator, 'validator2>.Try(value, validator, validator2) with
                | Ok value -> value |> ValueSome |> Ok
                | Error err -> Error err

        member this.MapTry map =
            Validated02<'value, 'validator, 'validator2>.Try (map this.Value, this.Validator, this.Validator2)

        static member Make(value, validator, validator2) =
            Validated02<'value, 'validator, 'validator2>.Try(value, validator, validator2) |> EResult.unwrap

        member this.MapMake map =
            Validated02<'value, 'validator, 'validator2>.Make (map this.Value, this.Validator, this.Validator2)

    type Validated11<'value, 'validator, 'validator2 when 'validator: struct
                                                      and 'validator: (new: unit -> 'validator)
                                                      and 'validator :> IValidator<'value>
                                                      and 'validator2 :> IValidator<'value>> with
        static member Try(value, validator: 'validator2) =
            match value with
            | ValueNone -> ValueNone |> Ok
            | ValueSome value ->
                match Validated11<'value, 'validator, 'validator2>.Try(value, validator) with
                | Ok value -> value |> ValueSome |> Ok
                | Error err -> Error err

        member this.MapTry map =
            Validated11<'value, 'validator, 'validator2>.Try(this.Value |> map, this.Validator)

        static member Make(value, validator: 'validator2) =
            Validated11<'value, 'validator, 'validator2>.Try(value, validator) |> EResult.unwrap

        member this.MapMake map =
            Validated11<'value, 'validator, 'validator2>.Make(this.Value |> map, this.Validator)
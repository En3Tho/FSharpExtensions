namespace En3Tho.FSharp.Validation

type ValidatedI =
    static member inline mapTry map (entity: InstanceValidatorValidated<'a,'b>) = entity.MapTry(map)
    static member inline mapMake map (entity: InstanceValidatorValidated<'a,'b>) = entity.MapMake(map)
    static member inline mapTryAggregate map (entity: InstanceValidatorValidated<'a,'b>) = entity.MapTryAggregate(map)
    static member inline mapMakeAggregate map (entity: InstanceValidatorValidated<'a,'b>) = entity.MapMakeAggregate(map)
    static member inline value (entity: InstanceValidatorValidated<'a,'b>) = entity.Value

type ValidatedN =
    static member inline mapTry map (entity: NewCtorValidatorValidated<'a,'b>) = entity.MapTry(map)
    static member inline mapMake map (entity: NewCtorValidatorValidated<'a,'b>) = entity.MapMake(map)
    static member inline mapTryAggregate map (entity: NewCtorValidatorValidated<'a,'b>) = entity.MapTryAggregate(map)
    static member inline mapMakeAggregate map (entity: NewCtorValidatorValidated<'a,'b>) = entity.MapMakeAggregate(map)
    static member inline value (entity: NewCtorValidatorValidated<'a,'b>) = entity.Value

type AsyncValidatedI =
    static member inline mapTry map (entity: InstanceAsyncValidatorValidated<'a,'b>) = entity.MapTry(map)
    static member inline mapMake map (entity: InstanceAsyncValidatorValidated<'a,'b>) = entity.MapMake(map)
    static member inline mapTryAggregate map (entity: InstanceAsyncValidatorValidated<'a,'b>) = entity.MapTryAggregate(map)
    static member inline mapMakeAggregate map (entity: InstanceAsyncValidatorValidated<'a,'b>) = entity.MapMakeAggregate(map)
    static member inline value (entity: InstanceAsyncValidatorValidated<'a,'b>) = entity.Value

type AsyncValidatedN =
    static member inline mapTry map (entity: NewCtorAsyncValidatorValidated<'a,'b>) = entity.MapTry(map)
    static member inline mapMake map (entity: NewCtorAsyncValidatorValidated<'a,'b>) = entity.MapMake(map)
    static member inline mapTryAggregate map (entity: NewCtorAsyncValidatorValidated<'a,'b>) = entity.MapTryAggregate(map)
    static member inline mapMakeAggregate map (entity: NewCtorAsyncValidatorValidated<'a,'b>) = entity.MapMakeAggregate(map)
    static member inline value (entity: NewCtorAsyncValidatorValidated<'a,'b>) = entity.Value
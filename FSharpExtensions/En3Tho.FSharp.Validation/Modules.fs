namespace En3Tho.FSharp.Validation

module Validated =
    let inline mapTry map (entity: Validated<'a,'b>) = entity.MapTry(map)
    let inline mapTryAggregate map (entity: Validated<'a,'b>) = entity.MapTryAggregate(map)
    let inline mapMakeAggregate map (entity: Validated<'a,'b>) = entity.MapMakeAggregate(map)
    let inline value (entity: Validated<'a,'b>) = entity.Value
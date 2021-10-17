[<AutoOpen>]
module En3Tho.Extensions.DependencyInjection.AspNetCore.Extensions

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.DependencyInjection

type IServiceCollection with
    member this.AddController<'controller when 'controller :> ControllerBase>() =
        this.AddMvcCore()
            .AddApplicationPart(typeof<'controller>.Assembly)
        |> ignore
        this

    member this.AddAllRegisteredRoutesController() =
        this.AddController<AllRegisteredRoutesController>()
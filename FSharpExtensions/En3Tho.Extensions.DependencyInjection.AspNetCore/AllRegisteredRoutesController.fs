namespace En3Tho.Extensions.DependencyInjection.AspNetCore

open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Mvc.Infrastructure
open Microsoft.AspNetCore.Mvc.Internal
open En3Tho.FSharp.Extensions

[<Route("all-registered-routes")>]
type AllRegisteredRoutesController(actionDescriptorCollectionProvider: IActionDescriptorCollectionProvider) =
    inherit ControllerBase()

    [<HttpGet("")>]
    member _.GetAllRoutes() =
        actionDescriptorCollectionProvider.ActionDescriptors.Items
        |> Seq.map ^ fun item ->
            let action = item.RouteValues |> Dictionary.tryGetValue "Action" |> Option.defaultValue ""
            let controller = item.RouteValues |> Dictionary.tryGetValue "Controller" |> Option.defaultValue ""
            let name, template =
                match item.AttributeRouteInfo with
                | null -> "", ""
                | info -> info.Name, info.Template
            let httpMethods =
                match item.ActionConstraints with
                | null -> [||]
                | constraints ->
                    constraints
                    |> Seq.map ^ fun x ->
                        match x with
                        | :? HttpMethodActionConstraint as actionConstraint ->
                            actionConstraint.HttpMethods |> Seq.toArray
                        | _ -> [||]
                    |> Seq.toArray
            {|
                Action = action
                Controller = controller
                Name = name
                HttpMethods = httpMethods
                Template = template
            |}
        |> Seq.toArray
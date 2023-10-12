[<AutoOpen>]
module En3Tho.FSharp.Extensions.AspNetCore.WebApplicationExtensionsLP

open System
open Microsoft.AspNetCore.Builder

type WebApplication with
    member inline this.MapGet(pattern, func: Func<'T1, 'TResult>) = this.MapGet(pattern, handler = func)

    member inline this.MapPost(pattern, func: Func<'T1, 'TResult>) = this.MapPost(pattern, handler = func)

    member inline this.MapPut(pattern, func: Func<'T1, 'TResult>) = this.MapPut(pattern, handler = func)

    member inline this.MapDelete(pattern, func: Func<'T1, 'TResult>) = this.MapDelete(pattern, handler = func)

    member inline this.MapPatch(pattern, func: Func<'T1, 'TResult>) = this.MapPatch(pattern, handler = func)
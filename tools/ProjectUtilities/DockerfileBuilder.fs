namespace ProjectUtilities.DockerfileBuilder
open System
open System.Collections.Generic
open System.ComponentModel
open System.Runtime.InteropServices
open System.Text
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.SCollectionBuilder

// Okay so basic structure would be yield-bind based
// main dockerfile with "from"/"comment" yield-bind extensions
// and "from" with other extensions
// also, each element could have it's own extensions?

// to module?

[<AbstractClass>]
type DockerfileDirective() = // make this like a unit builder ?
    abstract member Print: builder: StringBuilder -> unit

type Comment(text: string) =
    inherit DockerfileDirective()
    member _.Text = text

    override this.Print(builder) = TODO

type From(image: string, [<Optional>] tag: string | null, [<Optional>] alias: string | null, [<Optional>] platform: string | null) =
    inherit DockerfileDirective()

    let directives = List<DockerfileDirective>()

    member _.Image = image
    member _.Tag = tag
    member _.Platform = platform
    member val Alias: string = alias ??= Guid.NewGuid().ToString()

    override this.Print(builder) = TODO

    // lots of these
    member _.Add(directive: Comment) = directives.Add(directive)

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Run(code: CollectionCode) = code(); this

    // and lots of these?
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Bind(directive: Comment, code: Comment -> CollectionCode) =
        this.Add(directive)
        code(directive)

type Dockerfile() = // these have "Add" members so maybe SCollection builders are fine?
    inherit DockerfileDirective()

    let directives = List<DockerfileDirective>()
    override this.Print(builder) = TODO

    member _.Add(directive: From) = directives.Add(directive)
    member _.Add(directive: Comment) = directives.Add(directive)

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Run(code: CollectionCode) = code(); this

module FooBar =
    let test1() =
        let d = Dockerfile() {
            From("foo") {
                let! comment = Comment("adsdasd") // use for later
                Comment("Foobar")
            }
        }
        let sb = StringBuilder(256)
        d.Print(sb)
        sb.ToString()
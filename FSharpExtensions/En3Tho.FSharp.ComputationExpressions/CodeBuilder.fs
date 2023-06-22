namespace En3Tho.FSharp.ComputationExpressions.CodeBuilder

open System
open System.Collections.Immutable
open System.IO
open System.Runtime.InteropServices
open System.Text
open En3Tho.FSharp.Extensions

module CodeBuilderImpl =

    type ITextWriter =
        abstract Write: value: string -> unit
        abstract WriteLine: value: string -> unit

    // TODO: async version with vtask
    // type IAsyncTextWriter =
    //     abstract WriteAsync: value: string -> ValueTask
    //     abstract WriteLineAsync: value: string -> ValueTask

    type [<Struct>] StringBuilderTextWriter(sb: StringBuilder) =
        member _.Write(value: string) = sb.Append(value) |> ignore
        member _.WriteLine(value: string) = sb.AppendLine(value) |> ignore

        interface ITextWriter with
            member this.Write(value) = this.Write(value)
            member this.WriteLine(value) = this.WriteLine(value)

    type [<Struct>] TextWriterTextWriter(textWriter: TextWriter) = // lul name
        member _.Write(value: string) = textWriter.Write(value)
        member _.WriteLine(value: string) = textWriter.WriteLine(value)

        interface ITextWriter with
            member this.Write(value) = this.Write(value)
            member this.WriteLine(value) = this.WriteLine(value)

    type [<Struct>] LineOfCode = {
        Indent: int
        Text: string
    }

    type CodeBuilder(lines: ResizeArray<LineOfCode>) =

        static let commonIndentations = ImmutableArray.Create<string> [|
            for i = 0 to 16 do
                String.replicate (i * 4) " "
        |]

        let mutable indentation = 0
        let mutable length = 0

        new() = CodeBuilder(ResizeArray())

        member _.Lines = lines
        member _.Indentation = indentation

        member _.IndentOnce() = indentation <- indentation + 1
        member _.UnIndentOnce() = indentation <- indentation - 1

        member private _.AddLength(text: string, indentation) =
            length <- length + Math.Max(1, text.Length) + indentation * 4

        member this.AddLine(value) =
            let text = value.ToString()
            let indentation = indentation

            this.AddLength(text, indentation)
            lines.Add({ Indent = indentation; Text = text })

        member this.AddLine(value: LineOfCode) =
            let text = value.Text
            let indentation = value.Indent + indentation

            this.AddLength(text, indentation)
            lines.Add({ Indent = indentation; Text = text })

        member private _.GetIndent count =
            if commonIndentations.Length < count then
                commonIndentations[count]
            else
                String.replicate (count * 4) " "

        member this.Flush<'a when 'a :> ITextWriter>(writer: 'a) =
            let linesSpan = CollectionsMarshal.AsSpan(lines)
            for lineOfCode in linesSpan do
                if String.IsNullOrWhiteSpace(lineOfCode.Text) then
                    writer.WriteLine("")
                else
                    writer.Write(this.GetIndent lineOfCode.Indent)
                    writer.WriteLine(lineOfCode.Text)

        member this.Flush(textWriter: TextWriter) =
            let textWriter = TextWriterTextWriter(textWriter)
            this.Flush(textWriter)

        member this.Flush(stringBuilder: StringBuilder) =
            let textWriter = StringBuilderTextWriter(stringBuilder)
            this.Flush(textWriter)

        member this.Reset() =
            length <- 0
            indentation <- 0
            lines.Clear()

        override this.ToString() =
            let sb = StringBuilder(length)
            this.Flush(sb)
            sb.ToString()

    type CodeBuilderCode = UnitBuilderCode<CodeBuilder>

    type CodeBlockBase() =
        inherit UnitBuilderBase<CodeBuilder>()

        member inline _.Yield([<InlineIfLambda>] codeBuilderCode: CodeBuilderCode) : CodeBuilderCode =
            fun builder ->
                codeBuilderCode builder

        member inline _.Yield(value: CodeBuilder) : CodeBuilderCode =
            fun builder ->
                for line in value.Lines do
                    builder.AddLine line

        member inline _.Yield(value: string) : CodeBuilderCode =
            fun builder ->
                builder.AddLine(value)

        member inline _.YieldFrom(values: string seq) : CodeBuilderCode =
            fun builder ->
                for value in values do
                    builder.AddLine(value)

    [<Sealed>]
    type Indent() =
        inherit CodeBlockBase()

        member inline this.Run([<InlineIfLambda>] runExpr: CodeBuilderCode) : CodeBuilderCode =
            fun builder ->
                builder.IndentOnce()
                runExpr builder
                builder.UnIndentOnce()

    [<Sealed>]
    type BraceBlock() =
        inherit CodeBlockBase()

        member inline this.Run([<InlineIfLambda>] runExpr: CodeBuilderCode) : CodeBuilderCode =
            fun builder ->
                builder.AddLine("{")
                builder.IndentOnce()
                runExpr builder
                builder.UnIndentOnce()
                builder.AddLine("}")

    [<Sealed>]
    type CodeBuilderRunner() =

        inherit CodeBlockBase()
        member inline this.Run([<InlineIfLambda>] runExpr: CodeBuilderCode) =
            let builder = CodeBuilder(ResizeArray(128))
            runExpr builder
            builder

[<AutoOpen>]
module CodeBuilder =
    open CodeBuilderImpl

    let code = CodeBuilderRunner()
    let indent = Indent()
    let braceBlock = BraceBlock()
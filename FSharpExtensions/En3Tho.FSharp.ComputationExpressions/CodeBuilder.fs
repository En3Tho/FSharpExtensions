namespace En3Tho.FSharp.ComputationExpressions.CodeBuilder

open System
open System.Collections.Immutable
open System.Text
open En3Tho.FSharp.Extensions

module CodeBuilderImpl =

    type [<Struct>] LineOfCode = {
        Indentation: int
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
            lines.Add({ Indentation = indentation; Text = text })

        member this.AddLine(value: LineOfCode) =
            let text = value.Text
            let indentation = value.Indentation + indentation

            this.AddLength(text, indentation)
            lines.Add({ Indentation = indentation; Text = text })

        member private _.GetIndentation count =
            if commonIndentations.Length < count then
                commonIndentations[count]
            else
                String.replicate (count * 4) " "

        override this.ToString() =
            let sb = StringBuilder(length)
            for lineOfCode in lines do
                if String.IsNullOrWhiteSpace(lineOfCode.Text) then
                    sb.AppendLine() |> ignore
                else
                    sb.Append(this.GetIndentation lineOfCode.Indentation).AppendLine(lineOfCode.Text) |> ignore

            // Remove new line at the end
            if sb.Length > 0 then
                sb.Remove(sb.Length - 1, 1) |> ignore

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
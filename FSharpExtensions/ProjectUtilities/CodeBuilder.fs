namespace ProjectUtilities.CodeBuilder

open System
open System.Text
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.Extensions.Byref.Operators
open En3Tho.FSharp.ComputationExpressions.ArrayPoolBasedBuilders
open En3Tho.FSharp.Extensions.GenericBuilderBase

module CodeBuilderImpl =

    type [<Struct>] LineOfCode = {
        Indentation: int
        Text: string
    }

    type CodeBuilder(builder: ResizeArray<LineOfCode>) =

        static let commonIndentations = block {
            for i = 0 to 16 do
                String.replicate (i * 4) " "
        }

        let mutable indentationCount = 0

        new() = CodeBuilder(ResizeArray())

        member _.Lines = builder
        member _.Indentation = indentationCount

        member _.IndentOnce() = &indentationCount +<- 1
        member _.UnIndentOnce() = &indentationCount -<- 1

        member private _.GetIndentation count =
            if commonIndentations.Length < count then
                commonIndentations[count]
            else
                String.replicate (count * 4) " "

        override this.ToString() =
            let sb = StringBuilder()
            for lineOfCode in builder do
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
                let linesCount = value.Lines.Count
                for lineIndex = 0 to linesCount - 1 do
                    let lineOfCode = value.Lines[lineIndex]
                    builder.Lines.Add { lineOfCode with Indentation = lineOfCode.Indentation + builder.Indentation }

        member inline _.Yield(value: 'a) : CodeBuilderCode =
            fun builder ->
                builder.Lines.Add { Indentation = builder.Indentation; Text = value.ToString() }

        member inline _.YieldFrom(values: 'b seq) : CodeBuilderCode =
            fun builder ->
                for value in values do
                    builder.Lines.Add { Indentation = builder.Indentation; Text = value.ToString() }

    [<Sealed>]
    type Indent() =
        inherit CodeBlockBase()

        member inline this.Run([<InlineIfLambda>] runExpr: CodeBuilderCode) : CodeBuilderCode =
            fun builder ->
                builder.IndentOnce()
                runExpr builder
                builder.UnIndentOnce()

    [<Sealed>]
    type CodeBuilderRunner() =

        inherit CodeBlockBase()
        member inline this.Run([<InlineIfLambda>] runExpr: CodeBuilderCode) =
            let builder = CodeBuilder(ResizeArray())
            runExpr builder
            builder

module CodeBuilder =
    open CodeBuilderImpl

    let code = CodeBuilderRunner()
    let indent = Indent()
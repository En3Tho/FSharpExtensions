module ProjectUtilities.IntrinsicsCodeGen

open System
open System.Linq
open System.IO
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open System.Text.RegularExpressions
open System.Xml
open En3Tho.FSharp.Extensions
open System.Collections.Generic
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.ICollectionBuilder
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

let [<Literal>] IntrinsicNodeName = "intrinsic"
let [<Literal>] IntrinsicNameAttribute = "name"
let [<Literal>] IntrinsicVersionAttribute = "version"
let [<Literal>] IntrinsicDateAttribute = "date"
let [<Literal>] OperationNodeName = "operation"
let [<Literal>] DescriptionNodeName = "description"

let extractOperationAndDescriptionNodes (node: XmlNode) =
    let mutable operationNode = null
    let mutable descriptionNode = null
    for childNode in node.ChildNodes |> Seq.cast<XmlNode> do
        match childNode.Name with
        | OperationNodeName -> operationNode <- childNode
        | DescriptionNodeName -> descriptionNode <- childNode
        | _ -> ()
    operationNode, descriptionNode

let generateCodeForIntrinsic (node: XmlNode) = codeBlock {
    $"let [<Literal>] private {node.Attributes[IntrinsicNameAttribute].Value}: string ="
    let operationNode, descriptionNode = extractOperationAndDescriptionNodes node
    indent {
        raw {
            "\"\"\""
            if descriptionNode &!= null then
                descriptionNode.InnerText.Trim()
        }
    }
    noindent {
        raw {
            if operationNode &!= null then
                operationNode.InnerText.Replace("\t", "    ").Trim()
            "\"\"\""
        }
    }
}

// this kills fscompiler - too many intrinsics
let generateCodeForIntrinsicsTableForRider (xmlDoc: XmlDocument) = code {
    let allIntrinsics = List(1024)
    "// Auto-generated"
    ""
    "open System.Collections.Generic"
    "open JetBrains.UI.RichText"
    ""
    let intrinsicsListNode = xmlDoc.FirstChild
    $"// version: {intrinsicsListNode.Attributes[IntrinsicVersionAttribute].Value} date: {intrinsicsListNode.Attributes[IntrinsicDateAttribute].Value}"
    ""

    for node in intrinsicsListNode.ChildNodes |> Seq.cast<XmlNode> do
        let intrinsicName = node.Attributes[IntrinsicNameAttribute].Value
        allIntrinsics.Add(intrinsicName)
        generateCodeForIntrinsic node
        ""

    "let generateIntrinsicsTable() ="
    indent {
        $"let table = Dictionary<string, RichText>({allIntrinsics.Count})"
        for intrinsic in allIntrinsics do
            $"table[\"{intrinsic}\"] <- {intrinsic}"
    }
    ""

    "let private intrinsicsTable = generateIntrinsicsTable()"
    ""

    "let getRichTextForIntrinsic (name: string) ="
    indent {
        "match intrinsicsTable.TryGetValue(name) with"
        "| true, value -> ValueSome RichText(value)"
        "| _ -> ValueNone"
    }
}

// generates file of the following format:
// mnemonicLength descriptionLength operationLength mnemonic
// operation chars ... newLine
// description chars ... newLine
// e.g.
// 14 202 124 _addcarryx_u32
// Add unsigned 64-bit integers "a" and "b" with unsigned 8-bit carry-in "c_in" (carry or overflow flag), and store the unsigned 64-bit result in "out", and the carry-out in "dst" (carry or overflow flag).
// tmp[0..64] := a[0..63] + b[0..63] + (c_in &gt; 0 ? 1 : 0)
// MEM[out..out+63] := tmp[0..63]
// dst[0] := tmp[64]
// dst[1..7] := 0
let generateIntrinsicsFile (xmlDoc: XmlDocument) = code {
    let intrinsicsListNode = xmlDoc.FirstChild
    for node in intrinsicsListNode.ChildNodes |> Seq.cast<XmlNode> do
        let operationNode, descriptionNode = extractOperationAndDescriptionNodes node
        let description = if descriptionNode &!= null then descriptionNode.InnerText.Trim() else ""
        let operation = if operationNode &!= null then operationNode.InnerText.Replace("\t", "    ").Trim() else ""
        let mnemonic = node.Attributes[IntrinsicNameAttribute].Value
        $"{mnemonic.Length} {description.Length} {operation.Length} {mnemonic}"
        description
        operation
}

type [<Struct>] IntrinsicInfo = {
    Description: ReadOnlyMemory<char>
    Operation: ReadOnlyMemory<char>
}

let parseIntrinsicsFile (filePath: string) =
    let table = Dictionary()
    let text = File.ReadAllText(filePath)
    let newLineLength = Environment.NewLine.Length

    let mutable textIndex = 0
    while textIndex < text.Length do
        let mutable mnemonicLengthIndex = textIndex
        while mnemonicLengthIndex < text.Length && Char.IsDigit(text[mnemonicLengthIndex]) do mnemonicLengthIndex <- mnemonicLengthIndex + 1
        let mnemonicLength = int text[textIndex..mnemonicLengthIndex]

        let mutable descriptionLengthIndex = mnemonicLengthIndex + 1
        while descriptionLengthIndex < text.Length && Char.IsDigit(text[descriptionLengthIndex]) do descriptionLengthIndex <- descriptionLengthIndex + 1
        let descriptionLength = int text[mnemonicLengthIndex + 1..descriptionLengthIndex]

        let mutable operationLengthIndex = descriptionLengthIndex + 1
        while operationLengthIndex < text.Length && Char.IsDigit(text[operationLengthIndex]) do operationLengthIndex <- operationLengthIndex + 1
        let operationLength = int text[descriptionLengthIndex + 1..operationLengthIndex]

        let descriptionStart = operationLengthIndex + 1 + mnemonicLength + newLineLength
        let operationStart = descriptionStart + descriptionLength + newLineLength

        textIndex <- operationStart + operationLength + newLineLength

        let mnemonic = text[operationLengthIndex + 1..operationLengthIndex + 1 + mnemonicLength]
        let description = text.AsMemory(descriptionStart, descriptionLength)
        let operation = text.AsMemory(operationStart, operationLength)

        table[mnemonic] <- { Description = description; Operation = operation }

    table

type Dictionary<'TKey, 'TValue> with
    member inline this.GetValueOrDefaultWith(key: 'TKey, [<InlineIfLambda>] valueFactory: unit -> 'TValue) =
        let mutable exists = false
        let valueRef = &CollectionsMarshal.GetValueRefOrAddDefault(this, key, &exists)
        if !exists then
            valueRef <- valueFactory()
        valueRef

let readAllIntrinsicsOperationNames (xmlDoc: XmlDocument) =
    let regex = Regex(".*_([a-z0-9]+)_.+")

    let descriptions = Dictionary()
    let result = Dictionary()
    let alt = result.GetAlternateLookup<ReadOnlySpan<char>>()
    for rootNode in xmlDoc.ChildNodes do
        for node in rootNode.ChildNodes |> Seq.cast<XmlNode> do
            let intrinsicName = node.Attributes[IntrinsicNameAttribute].Value
            let isAvx512 =  node.ChildNodes |> Seq.cast<XmlNode> |> Seq.exists (fun x -> x.Name == "CPUID" && x.InnerText.Contains("AVX512"))
            if isAvx512 then
                descriptions[intrinsicName] <- node["description"].InnerText
                let trimmedName = regex.Match(intrinsicName).Groups[1].Value
                let nameList = result.GetValueOrDefaultWith(trimmedName, fun () -> List())
                nameList.Add(intrinsicName)

    let keys = result.Keys
    for key in keys do
        if key.StartsWith('k') then // k-masks
            let spanKey = key.AsSpan().Slice(1)
            let mutable list = nullRef
            if alt.TryGetValue(spanKey, &list) then
                let mutable existing = nullRef
                result.Remove(key, &existing) |> ignore
                list.AddRange(existing)
    result, descriptions

let generateFiles (filePath: string) =
    let doc = XmlDocument()
    doc.Load(filePath)

    let opNames, descriptions = readAllIntrinsicsOperationNames doc
    let keys = opNames.Keys |> Seq.sort
    let keysWithDescriptions =
        keys |> Seq.map ^ fun key ->
            let mnemonics = opNames[key]
            let mnemonic = mnemonics[0]
            let description = descriptions[mnemonic]
            let mnemonicsStr = String.Join(", ", mnemonics)
            {| Instruction = key; Mnemonics = mnemonicsStr; Description = description |}
        |> Seq.toArray
    ()
    // generateCodeForIntrinsicsTableForRider doc
    // |> Code.writeToFile "Intrinsics.fs"
    // generateIntrinsicsFile doc
    // |> Code.writeToFile "Intrinsics.txt"
    // let table = parseIntrinsicsFile ".artifacts\Intrinsics.txt"
    // for intrinsic in table do
    //     Console.WriteLine(intrinsic.Key)
    //     Console.WriteLine(intrinsic.Value.Description.ToString())
    //     Console.WriteLine(intrinsic.Value.Operation.ToString())
    //|> Code.writeToConsole
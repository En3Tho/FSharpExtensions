module CentralPackageManagementMigrationTool.XmlNodeBuilder

open System.Collections.Generic
open System.Xml
open En3Tho.FSharp.ComputationExpressions

type XmlNodeBuilderBase(xmlElement: XmlElement) =
    member _.XmlElement = xmlElement

    member _.Add(other: XmlElement) =
        xmlElement.AppendChild(other)

    member _.Add(other: XmlElementBuilder) =
        xmlElement.AppendChild(other.XmlElement)

    member _.Add(other: XmlNodeBuilder) =
        xmlElement.AppendChild(other.XmlElement)

and XmlElementBuilder(xmlElement: XmlElement) =
    inherit XmlNodeBuilderBase(xmlElement)

    member inline this.Zero() : CollectionCode = fun() -> ()
    member inline this.Run([<InlineIfLambda>] code: CollectionCode) =
        code()

and XmlNodeBuilder(doc: XmlDocument, name: string) =
    inherit XmlNodeBuilderBase(doc.CreateElement(name))

    member this.Add(attr: KeyValuePair<string, string>) =
        this.XmlElement.Attributes.Append(this.XmlElement.OwnerDocument.CreateAttribute(attr.Key, Value = attr.Value))

    member this.Add(innerText: string) =
        this.XmlElement.InnerText <- innerText

    member inline this.Zero() : CollectionCode = fun() -> ()
    member inline this.Run([<InlineIfLambda>] code: CollectionCode) =
        code()
        XmlElementBuilder(this.XmlElement)

let xmlNode doc name = XmlNodeBuilder(doc, name)
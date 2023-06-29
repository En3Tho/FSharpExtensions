module CentralPackageManagementMigrationTool.PropertiesAndExtensions

open System
open System.Collections.Generic
open System.Xml
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.IDictionaryBuilder
open Microsoft.Build.Execution
open Microsoft.IO

module GlobalProperties =
    let [<Literal>] MSBuildExtensionsPath = "MSBuildExtensionsPath"
    let [<Literal>] SolutionDir = "SolutionDir"
    let [<Literal>] MSBuildSDKsPath = "MSBuildSDKsPath"
    let [<Literal>] RoslynTargetsPath = "RoslynTargetsPath"
    let [<Literal>] DesignTimeBuild = "DesignTimeBuild"
    let [<Literal>] SkipCompilerExecution = "SkipCompilerExecution"
    let [<Literal>] ProvideCommandLineArgs = "ProvideCommandLineArgs"
    let [<Literal>] BuildProjectReferences = "BuildProjectReferences"
    let [<Literal>] MSBuildEnableWorkloadResolver = "MSBuildEnableWorkloadResolver"
    let [<Literal>] RestorePackagesPath = "RestorePackagesPath"

module EnvironmentVariables =
    let [<Literal>] MSBUILD_NUGET_PATH = "MSBUILD_NUGET_PATH"

module Sdk =
    let [<Literal>] Project = "Project"

module Properties =
    let [<Literal>] PropertyGroup = "PropertyGroup"
    let [<Literal>] ManagePackageVersionsCentrally = "ManagePackageVersionsCentrally"

module Items =
    let [<Literal>] ItemGroup = "ItemGroup"
    let [<Literal>] PackageReference = "PackageReference"
    let [<Literal>] PackageVersion = "PackageVersion"

    module Metadata =
        let [<Literal>] Version = "Version"
        let [<Literal>] Include = "Include"
        let [<Literal>] Update = "Update"

let initGlobalProperties (projectPath: string) (toolsPath: string) =
    Dictionary() {
        GlobalProperties.SolutionDir -- Path.GetDirectoryName(projectPath)
        GlobalProperties.MSBuildExtensionsPath -- toolsPath
        GlobalProperties.MSBuildSDKsPath -- Path.Combine(toolsPath, "Sdks")
        GlobalProperties.RoslynTargetsPath -- Path.Combine(toolsPath, "Roslyn")
        GlobalProperties.DesignTimeBuild -- "true"
        GlobalProperties.SkipCompilerExecution -- "true"
        GlobalProperties.ProvideCommandLineArgs -- "true"
        GlobalProperties.BuildProjectReferences -- "false"
        GlobalProperties.MSBuildEnableWorkloadResolver -- "false" // TODO: should this be enabled? if yes then find a way to run this thing
        GlobalProperties.RestorePackagesPath -- Environment.GetEnvironmentVariable("NUGET_PACKAGES")
    }

type ProjectInstance with
    member this.PackageReferences =
        let tryGetPackageReferenceWithVersion (item: ProjectItemInstance) =
            if item.ItemType.Equals(Items.PackageReference) then
                let version = item.Metadata |> Seq.tryFind ^ fun metadata -> metadata.Name.Equals(Items.Metadata.Version)
                match version with
                | Some version ->
                    match item.EvaluatedInclude, version.EvaluatedValue with
                    | String.NotNullOrEmpty & packageName, String.NotNullOrEmpty & packageVersion ->
                        Some (packageName, packageVersion)
                    | _ ->
                        None
                | _ ->
                    None
            else
                None

        this.Items
        |> Seq.choose tryGetPackageReferenceWithVersion

    member this.IsManagedCentrally =
        this.Properties
        |> Seq.exists ^ fun property ->
            property.Name.Equals(Properties.ManagePackageVersionsCentrally)
            && property.EvaluatedValue.Equals("true")

type XmlDocument with
    member this.WriteToFile(filePath: string) =
        let settings = XmlWriterSettings(Indent = true, OmitXmlDeclaration = true, IndentChars = "    ")
        use xmlWriter = XmlWriter.Create(filePath, settings)
        this.WriteTo(xmlWriter)

type XmlNode with
    member this.GetNodesByName(nodeName) =
        match this.ChildNodes with
        | null ->
            Seq.empty
        | childNodes ->
            childNodes
            |> Seq.cast<XmlNode>
            |> Seq.where ^ fun node -> node.Name.Equals(nodeName)

    member this.GetAttributeByName(attributeName) =
        match this.Attributes with
        | null ->
            None
        | attributes ->
            attributes
            |> Seq.cast<XmlAttribute>
            |> Seq.tryFind ^ fun attribute ->
                attribute.Name.Equals(attributeName)

    member this.PropertyGroups =
        this.GetNodesByName(Properties.PropertyGroup)

    member this.ItemGroups =
        this.GetNodesByName(Items.ItemGroup)

    member this.PackageReferences =
        this.GetNodesByName(Items.PackageReference)

    member this.PackageVersions =
        this.GetNodesByName(Items.PackageVersion)

    member this.VersionAttribute =
        this.Attributes[Items.Metadata.Version] |> Option.ofObj

    member this.IncludeAttribute =
        this.Attributes[Items.Metadata.Include] |> Option.ofObj

    member this.UpdateAttribute =
        this.Attributes[Items.Metadata.Update] |> Option.ofObj

    member this.ManagePackageVersionsCentrallyNode =
        this[Properties.ManagePackageVersionsCentrally] |> Option.ofObj
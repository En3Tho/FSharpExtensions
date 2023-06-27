// For more information see https://aka.ms/fsharp-console-apps

open System
open System.Collections.Generic
open System.IO
open System.Xml
open CentralPackageManagementMigrationTool
open Microsoft.Build.Construction
open Microsoft.Build.Evaluation
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.IDictionaryBuilder
open Microsoft.Build.Execution
open Microsoft.Build.Utilities

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

let initEnvironmentVariables (globalProperties: Dictionary<string, string>) =
    for prop in globalProperties do
        Environment.SetEnvironmentVariable(prop.Key, prop.Value)
    Environment.SetEnvironmentVariable(EnvironmentVariables.MSBUILD_NUGET_PATH, globalProperties[GlobalProperties.RestorePackagesPath])

// TODO: find a way to pass toolsPath as msbuild property or something?
let copyRelevantFiles toolsPath =
    File.Copy(Path.Combine(toolsPath, "Microsoft.Build.NuGetSdkResolver.dll"), "Microsoft.Build.NuGetSdkResolver.dll", true)

let prepareEnvironment toolsPath solutionPath =
    let globalProperties = initGlobalProperties solutionPath toolsPath
    initEnvironmentVariables globalProperties
    copyRelevantFiles toolsPath
    
    let collection = ProjectCollection(globalProperties)
    collection.AddToolset(Toolset(ToolLocationHelper.CurrentToolsVersion, toolsPath, collection, ""))
    collection

let removeVersionAttributesFromPackageReferences (projectPath: string) (projectPackageVersions: IReadOnlyDictionary<string, string>) =
    if projectPackageVersions.Count > 0 then
        let document = XmlDocument()
        document.Load(projectPath)
        let project = document[Sdk.Project]

        for itemGroup in project.ItemGroups do
            for packageReference in itemGroup.PackageReferences do
                match packageReference.IncludeAttribute |> Option.orElse packageReference.UpdateAttribute with
                | Some packageName ->
                    if projectPackageVersions.ContainsKey(packageName.Value) then
                        match packageReference.VersionAttribute with
                        | Some version ->
                            packageReference.Attributes.Remove(version) |> ignore
                        | _ ->
                            let versionNode = packageReference.GetNodesByName(Items.Metadata.Version) |> Seq.tryHead
                            match versionNode with
                            | Some version ->
                                packageReference.RemoveChild(version) |> ignore
                            | _ ->
                                ()
                | _ ->
                    ()

        document.WriteToFile(projectPath)

let getOrCreateDirectoryTargetsProps (filePath: string) =
    let document = XmlDocument()
    if File.Exists(filePath) then
        document.Load(filePath)

    let mutable projectNode = document["Project"]
    if projectNode &== null then
        let newProjectNode = document.CreateElement(Sdk.Project)
        document.AppendChild(newProjectNode) |> ignore
        projectNode <- newProjectNode

    document

let updateDirectoryTargetProps (document: XmlDocument) (allPackageVersions: IReadOnlyDictionary<string, string>) =
    let project = document[Sdk.Project]

    let managePackagesCentrally =
        project.PropertyGroups
        |> Seq.tryPick ^ fun node ->
            node.ManagePackageVersionsCentrallyNode

    match managePackagesCentrally with
    | Some managePackagesCentrally ->
        managePackagesCentrally.InnerText <- "true"
    | _ ->
        let propertyGroup = document.CreateElement(Properties.PropertyGroup)
        let managePackagesCentrally = document.CreateElement(Properties.ManagePackageVersionsCentrally, InnerText = "true")
        propertyGroup.AppendChild(managePackagesCentrally) |> ignore
        project.AppendChild(propertyGroup) |> ignore

    let packageReferencesToAdd = Dictionary(allPackageVersions)
    for itemGroup in project.ItemGroups do
        for packageReference in itemGroup.PackageVersions do
            match packageReference.IncludeAttribute with
            | Some include' ->
                let removed, latestVersion = packageReferencesToAdd.Remove(include'.Value)
                if removed then
                    match packageReference.VersionAttribute with
                    | Some version ->
                        version.Value <- latestVersion
                    | _ ->
                        ()
            | _ ->
                ()

    let packageReferences = document.CreateElement(Items.ItemGroup)
    for packageReference in packageReferencesToAdd do
        let include' = packageReference.Key
        let version = packageReference.Value

        let packageReference = document.CreateElement(Items.PackageVersion)
        let includeAttribute = document.CreateAttribute(Items.Metadata.Include, Value = include')
        let versionAttribute = document.CreateAttribute(Items.Metadata.Version, Value = version)
        packageReference.Attributes.Append(includeAttribute) |> ignore
        packageReference.Attributes.Append(versionAttribute) |> ignore
        packageReferences.AppendChild(packageReference) |> ignore

    project.AppendChild(packageReferences) |> ignore

let analyzeSolution toolsPath solutionPath =
    let solution = SolutionFile.Parse(solutionPath)
    use projectCollection = prepareEnvironment toolsPath solutionPath

    let allPackageVersions = Dictionary()
    let perProjectPackageVersions = Dictionary()

    for project in solution.ProjectsInOrder do
        if project.AbsolutePath.EndsWith("proj") then
            projectCollection.LoadProject(project.AbsolutePath) |> ignore
            perProjectPackageVersions[project.AbsolutePath] <- Dictionary()

    for project in projectCollection.LoadedProjects do
        let instance = project.CreateProjectInstance()
        let projectDependencies = perProjectPackageVersions[project.FullPath]

        for packageName, packageVersion in instance.PackageReferences do
            projectDependencies[packageName] <- packageVersion
            match allPackageVersions.TryGetValue(packageName) with
            | true, LessThan packageVersion
            | false, _ ->
                allPackageVersions[packageName] <- packageVersion
            | _ ->
                ()

    for projectPackageVersions in perProjectPackageVersions do
        let projectPath = projectPackageVersions.Key
        let projectPackageVersions = projectPackageVersions.Value
        removeVersionAttributesFromPackageReferences projectPath projectPackageVersions

    let solutionFolder = Path.GetDirectoryName(solutionPath)
    let filePath = Path.Combine(solutionFolder, "Directory.Packages.props")

    let document = getOrCreateDirectoryTargetsProps filePath
    updateDirectoryTargetProps document allPackageVersions
    document.WriteToFile(filePath)

let run() =
    let solutionPath = @"G:\source\repos\En3Tho\PoshRedisViewer\PoshRedisViewer\PoshRedisViewer.sln"
    let toolsPath = DotnetInfo.getCoreBasePath solutionPath
    analyzeSolution toolsPath solutionPath
    
run()
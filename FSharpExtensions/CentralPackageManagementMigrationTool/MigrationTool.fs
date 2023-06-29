module CentralPackageManagementMigrationTool.MigrationTool

open System

open System.Collections.Generic
open System.Xml
open CentralPackageManagementMigrationTool.PropertiesAndExtensions
open En3Tho.FSharp.Extensions
open Microsoft.Build.Construction
open Microsoft.Build.Evaluation
open Microsoft.Build.Utilities
open Microsoft.IO

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

let run solutionPath =
    let toolsPath = DotnetInfo.getCoreBasePath solutionPath
    analyzeSolution toolsPath solutionPath
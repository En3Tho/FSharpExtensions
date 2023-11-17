open System
open CentralPackageManagementMigrationTool

let args = Environment.GetCommandLineArgs()[1]
MigrationTool.run args
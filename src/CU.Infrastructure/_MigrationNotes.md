# Contoso University Clean 3.1 - Migration Notes

=======================================<br/>
IMPORTANT - Set project "CU.Infrastructure" as startup project before doing migration operations<br/>
		if you want to use the connection string from that project.<br/>
=======================================<br/>

Check out [EF Resources](../../_docs/CC3_EFResources.md)
 -- links to Entity Framework resources<br/>

[Command Line tool - ef6.exe instead of migrate.exe](https://github.com/NuGet/NuGetGallery/pull/7711)

**NOTE**: The migrations in CU.Infrastructure were initially copied from ContosoUniversity.DAL
in [ContosoUniversity_dnc31_MVC on GitHub](https://github.com/bgoodearl/ContosoUniversity_dnc31_MVC)<br/>

## Initial Setup

Package Manager Console:
```powershell
Enable-Migrations -verbose
```

## Testing Package Manager Console

Package Manager Console - get info about commands:
```powershell
get-help about_EntityFramework
```

**IMPORTANT**: update the powershell variables set up below for your path and connection string<br/>
Powershell command line (NOT Package Manager Console):
```powershell
$devsolpath = "D:\_dev\GitHub\bgoodearl\Blazor\ContosoU_dnc31_MVCB_Clean";
$dalproj = "CU.Infrastructure"
$connstring = "data source=.\SQLEXPRESS;initial catalog=ContosoUniversity_c31_dev;Integrated Security=SSPI;MultipleActiveResultSets=True;"

dotnet exec `
--depsfile $devsolpath\src\$dalproj\bin\Debug\netcoreapp3.1\$dalproj.deps.json `
--additionalprobingpath C:\Users\$env:username\.nuget\packages `
--additionalprobingpath "C:\Program Files\dotnet\sdk\NuGetFallbackFolder" `
C:\Users\$env:username\.nuget\packages\entityframework\6.4.4\tools\netcoreapp3.0\any\ef6.dll `
migrations list `
--assembly $devsolpath\src\$dalproj\bin\Debug\netcoreapp3.1\$dalproj.dll `
--connection-string $connstring `
--connection-provider "System.Data.SqlClient" `
--verbose
```

To use this sample, create the database and run the SQL schema command file to create the tables.<br/>
`...\SqlScripts\Schema\Schema1.sql`

Or, look at the project from which this was created:
[ContosoUniversity_dnc31_MVC on GitHub](https://github.com/bgoodearl/ContosoUniversity_dnc31_MVC)<br/>
which has greater details on EF 6 migrations<br/>

## Initial Migration

To re-run the initial migration for this project, you'll need to start with an empty database.

**NOTE**: The following runs in a PowerShell command prompt, NOT in the Package Manager Console

**IMPORTANT**: update setting of **$devsolpath** and **$connstring** for your environment!<br/>
Command to run in powershell:
```powershell
$devsolpath = "D:\_dev\GitHub\bgoodearl\Blazor\ContosoU_dnc31_MVCB_Clean"; $srcschema = "0"; $dstschema = "Schema1";
$connstring = "data source=.\SQLEXPRESS;initial catalog=ContosoUniv_c31_dev;Integrated Security=SSPI;MultipleActiveResultSets=True;"
$dalproj = "CU.Infrastructure"

dotnet exec `
--depsfile $devsolpath\src\$dalproj\bin\Debug\netcoreapp3.1\$dalproj.deps.json `
--additionalprobingpath C:\Users\$env:username\.nuget\packages `
--additionalprobingpath "C:\Program Files\dotnet\sdk\NuGetFallbackFolder" `
--runtimeconfig $devsolpath\src\$dalproj\bin\Debug\netcoreapp3.1\$dalproj.runtimeconfig.json `
C:\Users\$env:username\.nuget\packages\entityframework\6.4.4\tools\netcoreapp3.0\any\ef6.dll `
migrations add $dstschema `
--json --verbose --no-color --prefix-output --assembly $devsolpath\src\$dalproj\bin\Debug\netcoreapp3.1\$dalproj.dll `
--project-dir $devsolpath\src\$dalproj\ --language C# --root-namespace $dalproj `
--connection-string $connstring `
--connection-provider "System.Data.SqlClient"
```

## SQL script


**IMPORTANT**: update setting of **$devsolpath** and **$connstring** for your environment!<br/>
Command to run in powershell:
```powershell
$devsolpath = "D:\_dev\GitHub\bgoodearl\Blazor\ContosoU_dnc31_MVCB_Clean"; $srcschema = "0"; $dstschema = "Schema1";
$connstring = "data source=.\SQLEXPRESS;initial catalog=ContosoUniv_c31_dev;Integrated Security=SSPI;MultipleActiveResultSets=True;"
$dalproj = "CU.Infrastructure"

dotnet exec `
--depsfile $devsolpath\src\$dalproj\bin\Debug\netcoreapp3.1\$dalproj.deps.json `
--additionalprobingpath C:\Users\$env:username\.nuget\packages `
--additionalprobingpath "C:\Program Files\dotnet\sdk\NuGetFallbackFolder" `
--runtimeconfig $devsolpath\src\$dalproj\bin\Debug\netcoreapp3.1\$dalproj.runtimeconfig.json `
C:\Users\$env:username\.nuget\packages\entityframework\6.4.4\tools\netcoreapp3.0\any\ef6.dll `
database update `
--source $srcschema `
--target $dstschema `
--script --verbose --no-color --prefix-output `
--assembly $devsolpath\src\$dalproj\bin\Debug\netcoreapp3.1\$dalproj.dll `
--project-dir $devsolpath\src\$dalproj\ `
--language C# `
--root-namespace $dalproj `
--connection-string $connstring `
--connection-provider "System.Data.SqlClient" `
1> $devsolpath\SqlScripts\Schema\$dstschema.sql
```

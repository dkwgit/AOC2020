#Not yet parameterized correctly and not yet working
param ($projectName)
Write-Host "Creating assets for $projectName"

dotnet new classlib --name $projectName
dotnet add Driver\Driver.csproj reference $projectName\$projectName.csproj
Rename-Item -Path "$projectName\Class1.cs" -NewName Puzzle.cs
dotnet add $projectName\$projectName.csproj reference Utilities\Utilities.csproj

Push-Location Utilities
Add-Type -path "C:\Program Files\PowerShell\7\System.Windows.Forms.dll"
$txtFile = "$projectName" + "_PuzzleInput.txt"
Write-Host "data" > "Resources\$txtFile"
$obj = [System.Resources.ResxResourceWriter]::new("resources.resx")
$xref = [System.Resources.ResxDataNode]::new("$projectName_PuzzleInput.txt", $node)
$xRef.Base
$node = [System.Resources.ResXFileRef]::new("Resources\$txtFile","System.String")
$obj.AddResource($xref.Name,$node)
$obj.AddResource("$projectNamePart1_Answer","data")
$obj.AddResource("$projectNamePart2_Answer","data")
$obj.Close()
Pop-Location

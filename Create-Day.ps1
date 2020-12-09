#Not yet parameterized correctly and not yet working
param ($projectName)
Write-Host "Creating assets for $projectName"

dotnet new classlib --name $projectName
dotnet add Driver\Driver.csproj reference $projectName\$projectName.csproj
Rename-Item -Path "$projectName\Class1.cs" -NewName Puzzle.cs
dotnet add $projectName\$projectName.csproj reference Utilities\Utilities.csproj

Push-Location Utilities
Write-Host "now in " + $(Get-Location)
Add-Type -path "C:\Program Files\PowerShell\7\System.Windows.Forms.dll"
$txtFile = "$projectName" + "_PuzzleInput.txt"

Write-Host "Creating assets for $txtFile in Resources\$txtFile"

"data" > "Resources\$txtFile"

$obj = [System.Resources.ResxResourceWriter]::new($(Resolve-Path -Path "resources.resx").Path)
$xref = [System.Resources.ResXFileRef]::new("Resources\$txtFile", "System.String")
$node = [System.Resources.ResxDataNode]::new("$projectName" + "_PuzzleInput.txt", $xref)
$obj.AddResource($xref.Name,$node)
$obj.AddResource("$projectName" + "Part1_Answer","data")
$obj.AddResource("$projectName" + "Part2_Answer","data")
$obj.Close()
Pop-Location

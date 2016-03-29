# Parameters
#   All parameters are optional. They need to be provided if this is run at developers' local environment.
#   If they are not provided, the environment variables from AppVeyor will be used as default.
#
#   $Config: Name of configuration. eg) Debug, Release
Param(
    [string] [Parameter(Mandatory=$false)] $Config
)

$configuration = "Debug"

if (![string]::IsNullOrWhiteSpace($Config))
{
    $configuration = $Config
}

dnu restore -f https://www.myget.org/F/aspnet-contrib/api/v3/index.json

$exitCode = 0

$projects = Get-ChildItem .\src, .\test | ?{$_.PsIsContainer}
foreach($project in $projects) {
    # Display project name
    Write-Host "`n$project`n" -ForegroundColor Green

    $projectPath = $project.FullName
    $projectName = $project.Name

    # Build project
    dnu build $projectPath --out .\artifacts\bin\$projectName --configuration $configuration

    $exitCode += $LASTEXITCODE
}

if($exitCode -ne 0) {
    $host.SetShouldExit($exitCode)
}
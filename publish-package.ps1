# Parameters
#   All parameters are optional. They need to be provided if this is run at developers' local environment.
#   If they are not provided, the environment variables from AppVeyor will be used as default.
#
#   $Config: Name of configuration. eg) Debug, Release
#   $Project: Name of project.
Param(
    [string] [Parameter(Mandatory=$false)] $Config,
    [string] [Parameter(Mandatory=$false)] $Project
)

$configuration = $env:configuration

if (![string]::IsNullOrWhiteSpace($Config))
{
    $configuration = $Config
}

$projectName = $env:project_name

if (![string]::IsNullOrWhiteSpace($Project)) {
    $projectName = $Project
}

# Display project name
Write-Host "`nPublish the $projectName project to as a NuGet package`n" -ForegroundColor Green

dnu restore -f https://www.myget.org/F/aspnet-contrib/api/v3/index.json --quiet

if ($LASTEXITCODE -ne 0) {
    $host.SetShouldExit($exitCode)
}

dnu pack .\src\$projectName --out .\artifacts\bin\$projectName --configuration $configuration --quiet

# Get-ChildItem *.nupkg -Recurse
# dir ".\artifacts\bin\$env:project_name\$env:configuration\*.*"
# Get-ChildItem *.nupkg -Recurse | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

if ($LASTEXITCODE -ne 0) {
    $host.SetShouldExit($exitCode)
}

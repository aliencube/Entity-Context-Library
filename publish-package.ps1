# Parameters
#   All parameters are optional. They need to be provided if this is run at developers' local environment.
#   If they are not provided, the environment variables from AppVeyor will be used as default.
#
#   $Config: Name of configuration. eg) Debug, Release
Param(
    [string] [Parameter(Mandatory=$false)] $Config
)

$configuration = $env:configuration

if (![string]::IsNullOrWhiteSpace($Config))
{
    $configuration = $Config
}

$projectName = "Aliencube.EntityContextLibrary"

# dnu restore -f https://www.myget.org/F/aspnet-contrib/api/v3/index.json

dnu pack .\src\$projectName --out .\artifacts\bin\$projectName --configuration $Config

if ($LASTEXITCODE -ne 0) {
    $host.SetShouldExit($exitCode)
}

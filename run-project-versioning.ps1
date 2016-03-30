# Parameters
#   All parameters are optional. They need to be provided if this is run at developers' local environment.
#   If they are not provided, the environment variables from AppVeyor will be used as default.
#
#   $Project: Name of project.
Param(
    [string] [Parameter(Mandatory=$false)] $Project
)

$projectName = $env:project_name

if (![string]::IsNullOrWhiteSpace($Project)) {
    $projectName = $Project
}

Write-Host "`nUpdate verion on the $projectName project`n" -ForegroundColor Green

cd ./src/$projectName

npm install

node project-version.js

cd ../../

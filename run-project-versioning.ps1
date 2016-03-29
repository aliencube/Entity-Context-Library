# This PowerShel script appends AppVeyor build number to the package version.

$project = "Aliencube.EntityContextLibrary"

Write-Host "`nUpdate verion on the $project project`n" -ForegroundColor Green

cd ./src/$project

npm install

node project-version.js

cd ../../

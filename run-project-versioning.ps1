# This PowerShel script appends AppVeyor build number to the package version.

$project = "Aliencube.EntityContextLibrary"

Write-Host "`n$project`n" -ForegroundColor Green

cd ./src/$project

npm install

node project-version.js

cd ../../

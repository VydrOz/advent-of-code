param(
    [int]$Year, [int]$Day, [string]$Title
)

if (!$PSBoundParameters.ContainsKey('Year') -or !$PSBoundParameters.ContainsKey('Day') -or !$PSBoundParameters.ContainsKey('Title')) {
    Write-Host "No arguments were passed, need 3 args Year, Day and title" -ForegroundColor Red
    return;
}

$template = Get-Content -Raw .\Template\Solution.template
$projectFolder = Split-Path $PSScriptRoot -Parent;
$newDirectory = [IO.Path]::Combine($projectFolder, $Year, "Day$('{0:d2}' -f $Day)")

if(!(Test-Path $newDirectory)) {
    New-Item $newDirectory -ItemType Directory | Out-Null
    Write-Host "New directory created $newDirectory" -ForegroundColor Yellow

    $solutionFile = [IO.Path]::Combine($newDirectory, "Solution.cs")
    if(!(Test-Path $solutionFile)) {
        New-Item $solutionFile -ItemType File -Value ($template -replace "<YEAR>", $Year -replace "<DAY>", "$('{0:d2}' -f $Day)" -replace "<BOOL>", "true" -replace "<TITLE>", $Title) -Force | Out-Null
    }

    $inputDebugFile = [IO.Path]::Combine($newDirectory, "debug_input.in")
    if(!(Test-Path $inputDebugFile)) {
        New-Item $inputDebugFile -ItemType File | Out-Null
    }

    $inputFile = [IO.Path]::Combine($newDirectory, "input.in")
    if(!(Test-Path $inputFile)) {
        New-Item $inputFile -ItemType File | Out-Null
    }

    $readmeFile = [IO.Path]::Combine($newDirectory, "README.md")
    if(!(Test-Path $readmeFile)) {
        New-Item $readmeFile -ItemType File | Out-Null
    }

    Get-ChildItem -Path $newDirectory | ForEach-Object { Write-Host " -> $($_.Name)" -ForegroundColor Blue }
}
else {
    Write-Host "Directory already exist" -ForegroundColor Yellow
}
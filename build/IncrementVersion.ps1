# CaptainSkillTree Auto Version Increment Script
param(
    [string]$ProjectFile,
    [string]$ProjectDir
)

$ErrorActionPreference = "Stop"
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Clean up ProjectDir parameter (remove trailing quotes and backslash)
$ProjectDir = $ProjectDir.TrimEnd('\').TrimEnd('"')

try {
    Write-Host "[VERSION] Script started..." -ForegroundColor Cyan

    # 1. Read current version from .csproj
    [xml]$csproj = Get-Content $ProjectFile -Encoding UTF8

    # Handle multiple PropertyGroup elements
    $versionNode = $csproj.Project.PropertyGroup | Where-Object { $_.Version } | Select-Object -First 1
    if (-not $versionNode -or -not $versionNode.Version) {
        throw "Version property not found in .csproj"
    }
    $currentVersion = $versionNode.Version
    Write-Host "[VERSION] Current: $currentVersion" -ForegroundColor Yellow

    # 2. Parse Semantic Versioning (0.1.0)
    if ($currentVersion -match '^(\d+)\.(\d+)\.(\d+)$') {
        $major = [int]$matches[1]
        $minor = [int]$matches[2]
        $patch = [int]$matches[3] + 1
        $newVersion = "$major.$minor.$patch"
        $newAssemblyVersion = "$major.$minor.$patch.0"
    } else {
        throw "Version format error: $currentVersion (expected: 0.1.0)"
    }

    Write-Host "[VERSION] New: $newVersion" -ForegroundColor Green

    # 3. Update Plugin.cs
    $pluginPath = Join-Path $ProjectDir "Plugin.cs"
    Write-Host "[DEBUG] Plugin path: $pluginPath" -ForegroundColor Gray
    $pluginContent = Get-Content $pluginPath -Raw -Encoding UTF8
    $pluginPattern = '(\[BepInPlugin\("CaptainSkillTree\.SkillTreeMod", "Captain SkillTree Mod", ")(\d+\.\d+\.\d+)("\)\])'
    $pluginContent = $pluginContent -replace $pluginPattern, "`${1}$newVersion`$3"
    [System.IO.File]::WriteAllText($pluginPath, $pluginContent, [System.Text.Encoding]::UTF8)
    Write-Host "[OK] Plugin.cs updated (Line 283)" -ForegroundColor Green

    # 4. Update AssemblyInfo.cs
    $assemblyInfoPath = Join-Path $ProjectDir "Properties\AssemblyInfo.cs"
    Write-Host "[DEBUG] AssemblyInfo path: $assemblyInfoPath" -ForegroundColor Gray
    $assemblyContent = Get-Content $assemblyInfoPath -Raw -Encoding UTF8
    $assemblyPattern = '\[assembly: Assembly(File)?Version\("(\d+\.\d+\.\d+\.\d+)"\)\]'
    $assemblyContent = $assemblyContent -replace $assemblyPattern, "[assembly: Assembly`$1Version(`"$newAssemblyVersion`")]"
    [System.IO.File]::WriteAllText($assemblyInfoPath, $assemblyContent, [System.Text.Encoding]::UTF8)
    Write-Host "[OK] AssemblyInfo.cs updated (Lines 15-16)" -ForegroundColor Green

    # 5. Update README.md
    $readmePath = Join-Path $ProjectDir "README.md"
    Write-Host "[DEBUG] README path: $readmePath" -ForegroundColor Gray
    if (Test-Path $readmePath) {
        $readmeContent = Get-Content $readmePath -Raw -Encoding UTF8
        $readmeContent = $readmeContent -replace 'v\d+\.\d+\.\d+', "v$newVersion"
        [System.IO.File]::WriteAllText($readmePath, $readmeContent, [System.Text.Encoding]::UTF8)
        Write-Host "[OK] README.md updated" -ForegroundColor Green
    }

    # 6. Create/Update manifest.json (Thunderstore)
    $thunderstorePath = Join-Path $ProjectDir "Thunderstore"
    $manifestPath = Join-Path $thunderstorePath "manifest.json"

    if (-not (Test-Path $thunderstorePath)) {
        New-Item -ItemType Directory -Path $thunderstorePath | Out-Null
    }

    # Generate JSON directly
    $manifest = @"
{
  "name": "CaptainSkillTree",
  "version_number": "$newVersion",
  "website_url": "https://github.com/yourusername/CaptainSkillTree",
  "description": "Valheim Skill Tree Expansion - EpicMMOSystem Expert System",
  "dependencies": [
    "denikson-BepInExPack_Valheim-5.4.2200",
    "blacks7ar-EpicMMOSystem-1.6.9"
  ]
}
"@

    [System.IO.File]::WriteAllText($manifestPath, $manifest, [System.Text.Encoding]::UTF8)
    Write-Host "[OK] manifest.json updated" -ForegroundColor Green

    # 7. Save new version to .csproj
    $versionNode.Version = $newVersion
    $csproj.Save($ProjectFile)
    Write-Host "[OK] .csproj version saved" -ForegroundColor Green

    Write-Host "[SUCCESS] Version updated: $currentVersion -> $newVersion" -ForegroundColor Magenta
    exit 0

} catch {
    Write-Host "[ERROR] Failed: $_" -ForegroundColor Red
    Write-Host "[ROLLBACK] Version changes cancelled" -ForegroundColor Yellow
    exit 1
}

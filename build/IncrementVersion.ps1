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

    # 2. Parse Semantic Versioning (major.minor.patch) - patch increments by 1, no rollover/padding
    if ($currentVersion -match '^(\d+)\.(\d+)\.(\d+)$') {
        $major = [int]$matches[1]
        $minor = [int]$matches[2]
        $patch = [int]$matches[3] + 1

        $newVersion = "$major.$minor.$patch"
        $newAssemblyVersion = "$major.$minor.$patch.0"
    } else {
        throw "Version format error: $currentVersion (expected: 1.2.31)"
    }

    Write-Host "[VERSION] New: $newVersion" -ForegroundColor Green

    # 3. Update Plugin.cs
    $pluginPath = Join-Path $ProjectDir "Plugin.cs"
    Write-Host "[DEBUG] Plugin path: $pluginPath" -ForegroundColor Gray
    $pluginContent = Get-Content $pluginPath -Raw -Encoding UTF8
    $pluginPattern = '(\[BepInPlugin\("CaptainSkillTree\.SkillTreeMod", "Captain SkillTree Mod", ")(\d+\.\d+\.\d+)("\)\])'
    $pluginContent = $pluginContent -replace $pluginPattern, "`${1}$newVersion`$3"
    [System.IO.File]::WriteAllText($pluginPath, $pluginContent, [System.Text.Encoding]::UTF8)
    Write-Host "[OK] Plugin.cs updated" -ForegroundColor Green

    # 4. Update AssemblyInfo.cs
    $assemblyInfoPath = Join-Path $ProjectDir "Properties\AssemblyInfo.cs"
    Write-Host "[DEBUG] AssemblyInfo path: $assemblyInfoPath" -ForegroundColor Gray
    $assemblyContent = Get-Content $assemblyInfoPath -Raw -Encoding UTF8
    $assemblyPattern = '\[assembly: Assembly(File)?Version\("(\d+\.\d+\.\d+\.\d+)"\)\]'
    $assemblyContent = $assemblyContent -replace $assemblyPattern, "[assembly: Assembly`$1Version(`"$newAssemblyVersion`")]"
    [System.IO.File]::WriteAllText($assemblyInfoPath, $assemblyContent, [System.Text.Encoding]::UTF8)
    Write-Host "[OK] AssemblyInfo.cs updated" -ForegroundColor Green

    # 5. Update README.md
    $readmePath = Join-Path $ProjectDir "README.md"
    Write-Host "[DEBUG] README path: $readmePath" -ForegroundColor Gray
    if (Test-Path $readmePath) {
        $readmeContent = Get-Content $readmePath -Raw -Encoding UTF8
        $readmeContent = $readmeContent -replace 'v\d+\.\d+\.\d+', "v$newVersion"
        [System.IO.File]::WriteAllText($readmePath, $readmeContent, [System.Text.Encoding]::UTF8)
        Write-Host "[OK] README.md updated" -ForegroundColor Green
    }

    # 5.5. Sync top CHANGELOG.md entry's version tag (content untouched, cst-changelog owns content)
    # NOTE (2026-08-12): the regex used to match the legacy "# [X.X.X]" header format only.
    # cst-changelog has written the current "## YYYY-MM-DD (vX.X.XX)" format for a while now,
    # so this step silently no-op'd on every build (topMatch.Success was always false) --
    # manifest.json/Plugin.cs kept incrementing while the CHANGELOG.md header stayed frozen at
    # whatever version cst-changelog last wrote (2.1.87 vs manifest's 2.1.90 when discovered).
    # Match the current format's version number specifically; the date portion is left untouched.
    $changelogPath = Join-Path $ProjectDir "Thunderstore\CHANGELOG.md"
    if (Test-Path $changelogPath) {
        $changelogContent = Get-Content $changelogPath -Raw -Encoding UTF8
        $topHeaderPattern = [regex]'(## \d{4}-\d{2}-\d{2} \(v)\d+\.\d+\.\d+(\))'
        $topMatch = $topHeaderPattern.Match($changelogContent)
        if ($topMatch.Success) {
            $changelogContent = $topHeaderPattern.Replace($changelogContent, "`${1}$newVersion`$2", 1)
            $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
            [System.IO.File]::WriteAllText($changelogPath, $changelogContent, $utf8NoBom)
            Write-Host "[OK] CHANGELOG.md top version tag updated" -ForegroundColor Green
        } else {
            Write-Host "[WARN] CHANGELOG.md top header didn't match expected format -- version tag NOT synced" -ForegroundColor Yellow
        }
    }

    # 6. Create/Update manifest.json (Thunderstore)
    $thunderstorePath = Join-Path $ProjectDir "Thunderstore"
    $manifestPath = Join-Path $thunderstorePath "manifest.json"

    if (-not (Test-Path $thunderstorePath)) {
        New-Item -ItemType Directory -Path $thunderstorePath | Out-Null
    }

    $manifest = @"
{
  "name": "CaptainSkillTree",
  "version_number": "$newVersion",
  "website_url": "https://discord.gg/W26PTxYhug",
  "description": "Valheim Skill Tree Expansion - EpicMMOSystem Expert System",
  "dependencies": [
    "denikson-BepInExPack_Valheim-5.4.2200",
    "WackyMole-WackyEpicMMOSystem-1.9.58",
    "ValheimModding-Jotunn-2.27.1"
  ]
}
"@

    [System.IO.File]::WriteAllText($manifestPath, $manifest, [System.Text.Encoding]::UTF8)
    Write-Host "[OK] manifest.json updated" -ForegroundColor Green

    # 6-2. Update Captain_Skilltree/manifest.json
    $captainManifestPath = Join-Path $thunderstorePath "Captain_Skilltree\manifest.json"
    if (Test-Path $captainManifestPath) {
        $captainManifestContent = Get-Content $captainManifestPath -Raw -Encoding UTF8
        $captainManifestContent = $captainManifestContent -replace '"version_number":\s*"\d+\.\d+\.\d+"', "`"version_number`": `"$newVersion`""
        [System.IO.File]::WriteAllText($captainManifestPath, $captainManifestContent, [System.Text.Encoding]::UTF8)
        Write-Host "[OK] Captain_Skilltree/manifest.json updated" -ForegroundColor Green
    }

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

# CaptainSkillTree Thunderstore Package Sync Script
# Copies the just-compiled DLL into the Thunderstore package folder and
# rebuilds the release zip, so the packaged DLL and manifest.json version
# are always guaranteed to match (no drift between them).
param(
    [string]$ProjectDir,
    [string]$DllPath
)

$ErrorActionPreference = "Stop"
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

$ProjectDir = $ProjectDir.TrimEnd('\').TrimEnd('"')
$DllPath = $DllPath.Trim('"')

try {
    Write-Host "[SYNC] Script started..." -ForegroundColor Cyan

    $packageDir = Join-Path $ProjectDir "Thunderstore\Captain_Skilltree"
    $manifestPath = Join-Path $packageDir "manifest.json"

    if (-not (Test-Path $manifestPath)) {
        throw "manifest.json not found: $manifestPath"
    }
    if (-not (Test-Path $DllPath)) {
        throw "Compiled DLL not found: $DllPath"
    }

    # 1. Read current version from the package manifest (source of truth)
    $manifest = Get-Content $manifestPath -Raw -Encoding UTF8 | ConvertFrom-Json
    $version = $manifest.version_number
    Write-Host "[SYNC] Package version: $version" -ForegroundColor Yellow

    # 2. Copy freshly compiled DLL into the package folder
    Copy-Item -Path $DllPath -Destination (Join-Path $packageDir "CaptainSkillTree.dll") -Force
    Write-Host "[OK] CaptainSkillTree.dll synced" -ForegroundColor Green

    # 3. Copy AnimationSpeedManager.dll dependency
    $animDll = Join-Path $ProjectDir "Lib\AnimationSpeedManager.dll"
    if (Test-Path $animDll) {
        Copy-Item -Path $animDll -Destination (Join-Path $packageDir "AnimationSpeedManager.dll") -Force
        Write-Host "[OK] AnimationSpeedManager.dll synced" -ForegroundColor Green
    }

    # 3.5. Generate the shipped package's CHANGELOG.md from the maintained root copy
    # (Thunderstore/CHANGELOG.md), which cst-changelog writes to directly. The package
    # copy is a derived, player-facing version: file-name annotations (e.g. "[Foo.cs]")
    # and "## Files Modified" summary blocks are internal dev info and get stripped here
    # rather than shown to players.
    $rootChangelog = Join-Path $ProjectDir "Thunderstore\CHANGELOG.md"
    if (Test-Path $rootChangelog) {
        $changelogLines = Get-Content -Path $rootChangelog -Encoding UTF8
        $strippedLines = New-Object System.Collections.Generic.List[string]
        $skipNext = $false
        foreach ($changelogLine in $changelogLines) {
            if ($skipNext) { $skipNext = $false; continue }
            if ($changelogLine -match '^## Files Modified') { $skipNext = $true; continue }
            # NOTE: build the checkmark emoji via [char]0x2705 instead of embedding the literal
            # glyph in the regex pattern. This script runs under legacy Windows PowerShell
            # (powershell.exe, not pwsh) during the MSBuild PostBuild step, which reads .ps1 files
            # using the system codepage unless a UTF-8 BOM is present -- an embedded multi-byte
            # emoji glyph gets corrupted into an invalid regex pattern and throws at runtime.
            # [char]0x2705 is pure ASCII in the source file, so it parses identically regardless
            # of file encoding.
            $strippedLines.Add(($changelogLine -replace ('^(\s*[-*]\s*(?:' + [char]0x2705 + '\S+\s*:\s*)?)\[[^\]]*\]\s*'), '$1'))
        }
        # NOTE (2026-08-12): Set-Content -Encoding UTF8 (Windows PowerShell) writes a UTF-8 BOM,
        # which fails Thunderstore's package CHANGELOG.md validation ("starts with a UTF-8 BOM") --
        # same trap already hit and fixed in the PotalMap project's equivalent script. Use BOM-less
        # .NET WriteAllText instead.
        $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
        [System.IO.File]::WriteAllText((Join-Path $packageDir "CHANGELOG.md"), ($strippedLines -join "`r`n") + "`r`n", $utf8NoBom)
        Write-Host "[OK] CHANGELOG.md generated into package (file names stripped)" -ForegroundColor Green
    }

    # 4. Remove stale release zips
    $thunderstoreDir = Join-Path $ProjectDir "Thunderstore"
    Get-ChildItem -Path $thunderstoreDir -Filter "Captain_Skilltree ver *.zip" -File -ErrorAction SilentlyContinue |
        ForEach-Object {
            Remove-Item $_.FullName -Force
            Write-Host "[OK] Removed stale package: $($_.Name)" -ForegroundColor Gray
        }

    # 5. Create fresh release zip from the package folder contents
    $zipPath = Join-Path $thunderstoreDir "Captain_Skilltree ver $version.zip"
    Compress-Archive -Path (Join-Path $packageDir "*") -DestinationPath $zipPath -Force
    Write-Host "[OK] Created package: Captain_Skilltree ver $version.zip" -ForegroundColor Green

    Write-Host "[SUCCESS] Thunderstore package synced at version $version" -ForegroundColor Magenta
    exit 0

} catch {
    Write-Host "[ERROR] Failed: $_" -ForegroundColor Red
    exit 1
}

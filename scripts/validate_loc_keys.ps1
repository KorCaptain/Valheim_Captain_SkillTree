# Localization Key Validation Script
# Checks if all L.Get() keys are defined in DefaultLanguages.cs

$projectRoot = Split-Path -Parent $PSScriptRoot
$locFile = Join-Path $projectRoot "Localization\DefaultLanguages.cs"

Write-Host "=" * 60
Write-Host "Localization Key Validation"
Write-Host "=" * 60

# 1. Extract keys used in code
Write-Host "`n[1/3] Analyzing L.Get() calls in code..."
$usedKeys = @{}
$csFiles = Get-ChildItem -Path $projectRoot -Filter "*.cs" -Recurse | Where-Object { $_.Name -ne "DefaultLanguages.cs" }

foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw -ErrorAction SilentlyContinue
    if ($content) {
        $matches = [regex]::Matches($content, 'L\.Get\(\s*"([^"]+)"')
        foreach ($match in $matches) {
            $key = $match.Groups[1].Value
            if (-not $usedKeys.ContainsKey($key)) {
                $usedKeys[$key] = @()
            }
            $usedKeys[$key] += $file.FullName.Replace($projectRoot, "").TrimStart('\')
        }
    }
}

Write-Host "  Found: $($usedKeys.Count) unique keys" -ForegroundColor Cyan

# 2. Extract keys defined in DefaultLanguages.cs
Write-Host "`n[2/3] Analyzing DefaultLanguages.cs..."
$locContent = Get-Content $locFile -Raw

# Korean block - 더 정확한 패턴으로 전체 메서드 매칭
$koreanMatch = [regex]::Match($locContent, 'public static Dictionary<string, string> GetKorean\(\)[^{]*\{(.*?)^\s*\}', [System.Text.RegularExpressions.RegexOptions]::Singleline -bor [System.Text.RegularExpressions.RegexOptions]::Multiline)
$koreanKeys = @{}
if ($koreanMatch.Success) {
    $koreanBlock = $koreanMatch.Groups[1].Value
    $keyMatches = [regex]::Matches($koreanBlock, '\["([^"]+)"\]')
    foreach ($match in $keyMatches) {
        $koreanKeys[$match.Groups[1].Value] = $true
    }
}

# English block - 더 정확한 패턴으로 전체 메서드 매칭
$englishMatch = [regex]::Match($locContent, 'public static Dictionary<string, string> GetEnglish\(\)[^{]*\{(.*?)^\s*\}', [System.Text.RegularExpressions.RegexOptions]::Singleline -bor [System.Text.RegularExpressions.RegexOptions]::Multiline)
$englishKeys = @{}
if ($englishMatch.Success) {
    $englishBlock = $englishMatch.Groups[1].Value
    $keyMatches = [regex]::Matches($englishBlock, '\["([^"]+)"\]')
    foreach ($match in $keyMatches) {
        $englishKeys[$match.Groups[1].Value] = $true
    }
}

Write-Host "  Korean keys: $($koreanKeys.Count)" -ForegroundColor Cyan
Write-Host "  English keys: $($englishKeys.Count)" -ForegroundColor Cyan

# 3. Validation
Write-Host "`n[3/3] Validating..."

$missingKeys = @()
$koreanOnlyKeys = @()
$englishOnlyKeys = @()

# Check for missing keys
foreach ($key in $usedKeys.Keys) {
    $inKorean = $koreanKeys.ContainsKey($key)
    $inEnglish = $englishKeys.ContainsKey($key)

    if (-not $inKorean -and -not $inEnglish) {
        $missingKeys += $key
    }
}

# Check for language mismatch
foreach ($key in $koreanKeys.Keys) {
    if (-not $englishKeys.ContainsKey($key)) {
        $koreanOnlyKeys += $key
    }
}

foreach ($key in $englishKeys.Keys) {
    if (-not $koreanKeys.ContainsKey($key)) {
        $englishOnlyKeys += $key
    }
}

# Report results
$hasIssues = $false

if ($missingKeys.Count -gt 0) {
    $hasIssues = $true
    Write-Host "`nMISSING KEYS ($($missingKeys.Count)):" -ForegroundColor Red
    foreach ($key in ($missingKeys | Sort-Object)) {
        Write-Host "  - '$key'" -ForegroundColor Red
        $locations = $usedKeys[$key] | Select-Object -First 3
        foreach ($loc in $locations) {
            Write-Host "      Used in: $loc" -ForegroundColor Yellow
        }
    }
}

if ($koreanOnlyKeys.Count -gt 0) {
    $hasIssues = $true
    Write-Host "`nKOREAN ONLY ($($koreanOnlyKeys.Count)):" -ForegroundColor Yellow
    foreach ($key in ($koreanOnlyKeys | Sort-Object | Select-Object -First 10)) {
        Write-Host "  - '$key'" -ForegroundColor Yellow
    }
}

if ($englishOnlyKeys.Count -gt 0) {
    $hasIssues = $true
    Write-Host "`nENGLISH ONLY ($($englishOnlyKeys.Count)):" -ForegroundColor Yellow
    foreach ($key in ($englishOnlyKeys | Sort-Object | Select-Object -First 10)) {
        Write-Host "  - '$key'" -ForegroundColor Yellow
    }
}

if (-not $hasIssues) {
    Write-Host "`nALL CHECKS PASSED!" -ForegroundColor Green
    Write-Host "  - Used keys: $($usedKeys.Count)" -ForegroundColor Cyan
    Write-Host "  - Defined keys: $($koreanKeys.Count)" -ForegroundColor Cyan
    Write-Host "  - Match rate: 100%" -ForegroundColor Green
} else {
    Write-Host "`n$('=' * 60)" -ForegroundColor Red
    Write-Host "VALIDATION FAILED - Fix issues above" -ForegroundColor Red
    Write-Host "$('=' * 60)" -ForegroundColor Red
    exit 1
}

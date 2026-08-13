# Localization Key Validation Script
# Checks if all L.Get() keys are defined in DefaultLanguages.cs

$projectRoot = Split-Path -Parent $PSScriptRoot

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
        # (?!\s*\+) 로 리터럴 뒤에 문자열 연결(+)이 오는 동적 키(예: L.Get("item_" + id))는 제외 —
        # 그런 호출은 실제 키가 아니라 접두사일 뿐이라 "누락 키"로 오탐되는 문제 방지
        $matches = [regex]::Matches($content, 'L\.Get\(\s*"([^"]+)"(?!\s*\+)')
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

# 2. Extract keys from DefaultLanguages*.cs (line-by-line, tracks KO/EN method context)
Write-Host "`n[2/3] Analyzing DefaultLanguages*.cs..."
$locFiles = Get-ChildItem -Path (Join-Path $projectRoot "Localization") -Filter "DefaultLanguages*.cs" |
    Where-Object { $_.Name -ne "DefaultLanguages.cs" }

$koreanKeys = @{}
$englishKeys = @{}

foreach ($file in $locFiles) {
    $lines = Get-Content $file.FullName
    $context = ""   # "ko", "en", or ""
    foreach ($line in $lines) {
        if ($line -match '(private|public) static.*GetKorean_') { $context = "ko"; continue }
        if ($line -match '(private|public) static.*GetEnglish_') { $context = "en"; continue }
        if ($line -match '^\s*\}\s*$' -and $context -ne "") {
            # closing brace — could be dict end or method end; reset on method end heuristic
            # (method end: no more keys follow — we just continue and let context reset naturally)
        }
        if ($context -ne "" -and $line -match '\["([^"]+)"\]\s*=') {
            $key = $matches[1]
            if ($context -eq "ko") { $koreanKeys[$key] = $true }
            else                   { $englishKeys[$key] = $true }
        }
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

# 로컬라이제이션 키 검증 스크립트
# DefaultLanguages.cs에서 한국어/영어 키 일치 검증

param(
    [string]$LocalizationFile = "C:/home/ssunyme/.npm-global/bin/CaptainSkillTree/Localization/DefaultLanguages.cs"
)

if (-not (Test-Path $LocalizationFile)) {
    Write-Error "DefaultLanguages.cs 파일을 찾을 수 없습니다: $LocalizationFile"
    exit 1
}

$content = Get-Content $LocalizationFile -Raw

# 한국어 블록 추출 (GetKorean 메서드)
$koreanPattern = 'public static Dictionary<string, string> GetKorean\(\)[^{]*\{(.*?)return dict;'
$koreanMatch = [regex]::Match($content, $koreanPattern, [System.Text.RegularExpressions.RegexOptions]::Singleline)

if (-not $koreanMatch.Success) {
    Write-Error "한국어 블록을 찾을 수 없습니다"
    exit 1
}

$koreanBlock = $koreanMatch.Groups[1].Value

# 영어 블록 추출 (GetEnglish 메서드)
$englishPattern = 'public static Dictionary<string, string> GetEnglish\(\)[^{]*\{(.*?)return dict;'
$englishMatch = [regex]::Match($content, $englishPattern, [System.Text.RegularExpressions.RegexOptions]::Singleline)

if (-not $englishMatch.Success) {
    Write-Error "영어 블록을 찾을 수 없습니다"
    exit 1
}

$englishBlock = $englishMatch.Groups[1].Value

# 키 추출 함수
function Extract-Keys {
    param([string]$block)

    $keyPattern = '\["([^"]+)"\]'
    $matches = [regex]::Matches($block, $keyPattern)
    return $matches | ForEach-Object { $_.Groups[1].Value }
}

$koreanKeys = Extract-Keys $koreanBlock
$englishKeys = Extract-Keys $englishBlock

Write-Host "검증 결과:" -ForegroundColor Yellow
Write-Host "  한국어 키: $($koreanKeys.Count)개" -ForegroundColor Cyan
Write-Host "  영어 키: $($englishKeys.Count)개" -ForegroundColor Cyan

# 키 개수 비교
if ($koreanKeys.Count -ne $englishKeys.Count) {
    Write-Warning "⚠️  키 개수 불일치! (한국어: $($koreanKeys.Count), 영어: $($englishKeys.Count))"
} else {
    Write-Host "✅ 키 개수 일치" -ForegroundColor Green
}

# 중복 키 확인
$koreanDuplicates = $koreanKeys | Group-Object | Where-Object { $_.Count -gt 1 }
$englishDuplicates = $englishKeys | Group-Object | Where-Object { $_.Count -gt 1 }

if ($koreanDuplicates.Count -gt 0) {
    Write-Warning "⚠️  한국어 블록 중복 키 발견:"
    $koreanDuplicates | ForEach-Object { Write-Host "    - $($_.Name) ($($_.Count)회)" -ForegroundColor Red }
} else {
    Write-Host "✅ 한국어 블록 중복 키 없음" -ForegroundColor Green
}

if ($englishDuplicates.Count -gt 0) {
    Write-Warning "⚠️  영어 블록 중복 키 발견:"
    $englishDuplicates | ForEach-Object { Write-Host "    - $($_.Name) ($($_.Count)회)" -ForegroundColor Red }
} else {
    Write-Host "✅ 영어 블록 중복 키 없음" -ForegroundColor Green
}

# 한국어에만 있는 키
$koreanSet = [System.Collections.Generic.HashSet[string]]::new($koreanKeys)
$englishSet = [System.Collections.Generic.HashSet[string]]::new($englishKeys)

$koreanOnly = $koreanKeys | Where-Object { -not $englishSet.Contains($_) }
$englishOnly = $englishKeys | Where-Object { -not $koreanSet.Contains($_) }

if ($koreanOnly.Count -gt 0) {
    Write-Warning "⚠️  한국어에만 존재하는 키 ($($koreanOnly.Count)개):"
    $koreanOnly | ForEach-Object { Write-Host "    - $_" -ForegroundColor Red }
} else {
    Write-Host "✅ 한국어 전용 키 없음" -ForegroundColor Green
}

if ($englishOnly.Count -gt 0) {
    Write-Warning "⚠️  영어에만 존재하는 키 ($($englishOnly.Count)개):"
    $englishOnly | ForEach-Object { Write-Host "    - $_" -ForegroundColor Red }
} else {
    Write-Host "✅ 영어 전용 키 없음" -ForegroundColor Green
}

# config_ 접두사 키만 필터링하여 통계
$koreanConfigKeys = $koreanKeys | Where-Object { $_ -like "config_*" }
$englishConfigKeys = $englishKeys | Where-Object { $_ -like "config_*" }

Write-Host ""
Write-Host "Config 관련 키 통계:" -ForegroundColor Yellow
Write-Host "  한국어: $($koreanConfigKeys.Count)개" -ForegroundColor Cyan
Write-Host "  영어: $($englishConfigKeys.Count)개" -ForegroundColor Cyan

# 최종 결과
Write-Host ""
if ($koreanKeys.Count -eq $englishKeys.Count -and
    $koreanDuplicates.Count -eq 0 -and
    $englishDuplicates.Count -eq 0 -and
    $koreanOnly.Count -eq 0 -and
    $englishOnly.Count -eq 0) {
    Write-Host "✅✅✅ 모든 검증 통과! ✅✅✅" -ForegroundColor Green
} else {
    Write-Host "❌ 검증 실패 - 위 문제를 수정하세요" -ForegroundColor Red
}

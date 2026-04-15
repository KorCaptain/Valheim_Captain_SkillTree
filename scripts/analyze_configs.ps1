# Config 파일 분석 스크립트
# 14개 Config 파일에서 BindServerSync 패턴 추출

param(
    [string]$OutputCsv = "config_analysis.csv"
)

$projectRoot = "C:/home/ssunyme/.npm-global/bin/CaptainSkillTree"
$configFiles = @(
    "SkillTree/Attack_Config.cs",
    "SkillTree/Speed_Config.cs",
    "SkillTree/Defense_Config.cs",
    "SkillTree/Knife_Config.cs",
    "SkillTree/Mace_Config.cs",
    "SkillTree/Spear_Config.cs",
    "SkillTree/Polearm_Config.cs",
    "SkillTree/Crossbow_Config.cs",
    "SkillTree/Staff_Config.cs",
    "SkillTree/Production_Config.cs",
    "SkillTree/Archer_Config.cs",
    "SkillTree/Mage_Config.cs",
    "SkillTree/Tanker_Config.cs",
    "SkillTree/Rogue_Config.cs",
    "SkillTree/Paladin_Config.cs",
    "SkillTree/Berserker_Config.cs"
)

$results = @()

foreach ($file in $configFiles) {
    $fullPath = Join-Path $projectRoot $file
    if (-not (Test-Path $fullPath)) {
        Write-Warning "File not found: $fullPath"
        continue
    }

    $content = Get-Content $fullPath -Raw
    $fileName = Split-Path $file -Leaf

    # 카테고리 추출
    $category = $fileName -replace '_Config.cs', ''

    # BindServerSync 패턴 찾기
    # 패턴: ConfigKey = SkillTreeConfig.BindServerSync(config, "Category", "Key", defaultValue, "English Description");
    $pattern = '(\w+)\s*=\s*SkillTreeConfig\.BindServerSync\([^,]+,\s*"([^"]+)",\s*"([^"]+)",\s*[^,]+,\s*"([^"]+)"'
    $matches = [regex]::Matches($content, $pattern)

    foreach ($match in $matches) {
        $configVarName = $match.Groups[1].Value
        $categoryName = $match.Groups[2].Value
        $configKeyName = $match.Groups[3].Value
        $englishText = $match.Groups[4].Value

        # 로컬라이제이션 키 생성 (config_키이름_소문자)
        $locKey = "config_" + ($configKeyName -replace '([A-Z])', '_$1').ToLower().TrimStart('_')

        $results += [PSCustomObject]@{
            File = $fileName
            Category = $category
            CategoryName = $categoryName
            ConfigVarName = $configVarName
            ConfigKeyName = $configKeyName
            EnglishText = $englishText
            LocalizationKey = $locKey
        }
    }
}

# CSV 출력
$results | Export-Csv -Path $OutputCsv -NoTypeInformation -Encoding UTF8

Write-Host "Analysis Complete: $($results.Count) keys extracted" -ForegroundColor Green
Write-Host "Output file: $OutputCsv" -ForegroundColor Cyan
Write-Host ""
Write-Host "Stats by category:" -ForegroundColor Yellow
$results | Group-Object Category | ForEach-Object {
    Write-Host "  $($_.Name): $($_.Count) keys"
}

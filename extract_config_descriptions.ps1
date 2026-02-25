# Config 설명 추출 스크립트
# 모든 Config 파일에서 BindServerSync 호출을 추출하여 로컬라이제이션 키 생성

$configFiles = @(
    "SkillTree/Sword_Config.cs",
    "SkillTree/Bow_Config.cs",
    "SkillTree/Knife_Config.cs",
    "SkillTree/Spear_Config.cs",
    "SkillTree/Mace_Config.cs",
    "SkillTree/Speed_Config.cs",
    "SkillTree/Attack_Config.cs",
    "SkillTree/Defense_Config.cs",
    "SkillTree/Production_Config.cs",
    "SkillTree/Crossbow_Config.cs",
    "SkillTree/Staff_Config.cs",
    "SkillTree/Polearm_Config.cs",
    "SkillTree/Archer_Config.cs",
    "SkillTree/Mage_Config.cs",
    "SkillTree/Tanker_Config.cs",
    "SkillTree/Rogue_Config.cs",
    "SkillTree/Paladin_Config.cs",
    "SkillTree/Berserker_Config.cs"
)

$output = @()

foreach ($file in $configFiles) {
    $content = Get-Content $file -Raw

    # BindServerSync 호출 패턴 매칭
    $pattern = 'BindServerSync\(config,\s*"[^"]+",\s*"([^"]+)",\s*[^,]+,\s*"([^"]+)"'
    $matches = [regex]::Matches($content, $pattern)

    foreach ($match in $matches) {
        $configKey = $match.Groups[1].Value
        $description = $match.Groups[2].Value
        $locKey = "config_$($configKey.ToLower())"

        $output += [PSCustomObject]@{
            File = $file
            ConfigKey = $configKey
            LocalizationKey = $locKey
            EnglishDescription = $description
        }
    }
}

# CSV로 출력
$output | Export-Csv "config_descriptions.csv" -NoTypeInformation -Encoding UTF8

Write-Host "추출 완료: $($output.Count)개 항목"
Write-Host "출력 파일: config_descriptions.csv"

# Generate localization keys from CSV
# Input: config_analysis.csv
# Output: config_changes.txt, korean_keys.txt, english_keys.txt

param(
    [string]$InputCsv = "config_analysis.csv",
    [string]$OutputConfigChanges = "config_changes.txt",
    [string]$OutputKorean = "korean_keys.txt",
    [string]$OutputEnglish = "english_keys.txt"
)

# Translation mapping table (English to Korean)
$translations = @{
    "Tier" = "티어"
    "Expert" = "전문가"
    "Bonus" = "보너스"
    "Damage" = "데미지"
    "Speed" = "속도"
    "Defense" = "방어"
    "Required Points" = "필요 포인트"
    "Attack" = "공격"
    "Health" = "체력"
    "Stamina" = "스태미나"
    "Eitr" = "에이트르"
    "Crit" = "크리티컬"
    "Critical" = "크리티컬"
    "Dodge" = "회피"
    "Parry" = "패링"
    "Block" = "블럭"
    "Resistance" = "저항"
    "Armor" = "방어구"
    "Weight" = "무게"
    "Carry" = "운반"
    "Run" = "달리기"
    "Jump" = "점프"
    "Swim" = "수영"
    "Axe" = "도끼"
    "Pickaxe" = "곡괭이"
    "Sword" = "검"
    "Knife" = "단검"
    "Mace" = "둔기"
    "Spear" = "창"
    "Polearm" = "폴암"
    "Bow" = "활"
    "Crossbow" = "석궁"
    "Staff" = "지팡이"
    "Archer" = "궁수"
    "Mage" = "마법사"
    "Tanker" = "탱커"
    "Rogue" = "로그"
    "Paladin" = "성기사"
    "Berserker" = "버서커"
    "Production" = "생산"
    "Skill" = "스킬"
    "Point" = "포인트"
    "Level" = "레벨"
    "Cooldown" = "쿨다운"
    "Duration" = "지속시간"
    "Range" = "범위"
    "Server Sync" = "서버 동기화"
    "Chance" = "확률"
    "Physical" = "물리"
    "Elemental" = "속성"
    "Enhancement" = "강화"
    "Finisher" = "마무리"
    "Two Hand" = "양손"
    "Melee" = "근접"
    "Ranged" = "원거리"
    "Stat" = "스탯"
}

function Translate-ToKorean {
    param([string]$text)

    $korean = $text
    foreach ($key in $translations.Keys) {
        $korean = $korean -replace [regex]::Escape($key), $translations[$key]
    }
    return $korean
}

# Check CSV file exists
if (-not (Test-Path $InputCsv)) {
    Write-Error "CSV file not found: $InputCsv"
    exit 1
}

$data = Import-Csv $InputCsv -Encoding UTF8

# Initialize output streams
$configChanges = @()
$koreanKeys = @()
$englishKeys = @()

# Group by category
$grouped = $data | Group-Object Category

foreach ($group in $grouped) {
    $category = $group.Name

    # Category header
    $configChanges += "// ========================================`n"
    $configChanges += "// $category`n"
    $configChanges += "// ========================================`n`n"

    $koreanKeys += "// === $category ===`n"
    $englishKeys += "// === $category ===`n"

    foreach ($item in $group.Group) {
        $configVarName = $item.ConfigVarName
        $categoryName = $item.CategoryName
        $configKeyName = $item.ConfigKeyName
        $englishText = $item.EnglishText
        $locKey = $item.LocalizationKey
        $koreanText = Translate-ToKorean $englishText

        # Config file changes
        $configChanges += "// Before: $configVarName = SkillTreeConfig.BindServerSync(config, `"$categoryName`", `"$configKeyName`", ..., `"$englishText`");`n"
        $configChanges += "$configVarName = SkillTreeConfig.BindServerSync(config, `"$categoryName`", `"$configKeyName`", ..., SkillTreeConfig.GetConfigDescription(`"$locKey`"));`n`n"

        # Korean keys
        $koreanKeys += "[`"$locKey`"] = `"$koreanText`",`n"

        # English keys
        $englishKeys += "[`"$locKey`"] = `"$englishText`",`n"
    }

    $configChanges += "`n"
    $koreanKeys += "`n"
    $englishKeys += "`n"
}

# Save files
$configChanges -join "" | Out-File -FilePath $OutputConfigChanges -Encoding UTF8
$koreanKeys -join "" | Out-File -FilePath $OutputKorean -Encoding UTF8
$englishKeys -join "" | Out-File -FilePath $OutputEnglish -Encoding UTF8

Write-Host "Localization generation complete!" -ForegroundColor Green
Write-Host "  Config changes: $OutputConfigChanges" -ForegroundColor Cyan
Write-Host "  Korean keys: $OutputKorean" -ForegroundColor Cyan
Write-Host "  English keys: $OutputEnglish" -ForegroundColor Cyan
Write-Host ""
Write-Host "Total keys processed: $($data.Count)" -ForegroundColor Yellow

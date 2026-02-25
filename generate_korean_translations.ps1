# 한국어 번역 자동 생성 스크립트

# 번역 매핑
$translations = @{
    # 공통 패턴
    "Required Points" = "필요 포인트"
    "Damage Bonus" = "공격력 보너스"
    "Attack Speed Bonus" = "공격 속도 보너스"
    "Cooldown" = "쿨타임"
    "Stamina Cost" = "스태미나 소모"
    "Duration" = "지속시간"
    "Buff Duration" = "버프 지속시간"
    "Health Bonus" = "체력 보너스"
    "Armor Bonus" = "방어력 보너스"
    "Crit Chance" = "치명타 확률"
    "Crit Damage" = "치명타 데미지"
    "Move Speed" = "이동 속도"
    "Movement Speed" = "이동 속도"
    "Draw Speed" = "활 시위 당기기 속도"
    "Stun Chance" = "기절 확률"
    "Stun Duration" = "기절 지속시간"
    "Push Distance" = "밀쳐내기 거리"
    "AOE Radius" = "AOE 범위"
    "Trigger Chance" = "발동 확률"
    "Arrow Count" = "화살 개수"
    "Reflect Percent" = "반사 데미지 비율"
    "Damage Reduction" = "받는 피해 감소"
    "Incoming Damage Reduction" = "받는 피해 감소"
    "Knockback Chance" = "넉백 확률"
    "Damage Multiplier" = "데미지 배율"
    "Hit 1 Damage Ratio" = "1타 데미지 배율"
    "Hit 2 Damage Ratio" = "2타 데미지 배율"
    "Hit 3 Damage Ratio" = "3타 데미지 배율"
    "Hits 1-4 Damage Multiplier" = "1~4타 데미지 배율"
    "Hit 5 \(Final\) Damage Multiplier" = "5타(최종타) 데미지 배율"
    "Initial Dash Distance" = "초기 돌진 거리"
    "Side Movement Distance" = "측면 이동 거리"

    # 스킬명
    "Sword Expert" = "검 전문가"
    "Fast Slash" = "빠른 베기"
    "Counter Stance" = "반격 자세"
    "Combo Slash" = "연속 베기"
    "Riposte" = "칼날 되치기"
    "All-In-One" = "공방일체"
    "True Duel" = "진검승부"
    "Parry Rush" = "패링 돌격"
    "Rush Slash" = "돌진 연속 베기"

    "Mace Expert" = "둔기 전문가"
    "Stun Boost" = "기절 강화"
    "Guard" = "방어 강화"
    "Heavy Strike" = "무거운 타격"
    "Push" = "밀어내기"
    "Tank" = "탱커"
    "DPS" = "공격력 강화"
    "Grandmaster" = "그랜드마스터"
    "Fury Hammer" = "분노의 망치"
    "Guardian Heart" = "수호자의 진심"

    "Bow Expert" = "활 전문가"
    "Multi Shot" = "멀티샷"
    "Crit Boost" = "크리티컬 부스트"
    "Explosive Arrow" = "폭발 화살"

    "Knife Expert" = "단검 전문가"
    "Backstab" = "백스탭"
    "Assassin Heart" = "암살자의 심장"

    "Spear Expert" = "창 전문가"
    "Penetrate" = "꿰뚫기"
    "Combo" = "연공창"

    "Attack Expert" = "공격 전문가"
    "Speed Expert" = "속도 전문가"
    "Defense Expert" = "방어 전문가"
    "Production Expert" = "생산 전문가"

    "Crossbow Expert" = "석궁 전문가"
    "Staff Expert" = "지팡이 전문가"
    "Polearm Expert" = "폴암 전문가"

    "Archer" = "궁수"
    "Mage" = "마법사"
    "Tanker" = "탱커"
    "Rogue" = "로그"
    "Paladin" = "성기사"
    "Berserker" = "광전사"

    # 속성
    "\(flat\)" = "(고정값)"
    "\(sec\)" = "(초)"
    "\(meters\)" = "(미터)"
    "\(m/s\)" = "(m/s)"
    "\(%\)" = "(%)"
    "Based on Current Attack" = "현재 공격력 기준"
}

# CSV 읽기
$csv = Import-Csv "config_descriptions.csv" -Encoding UTF8

$koreanOutput = @()
$englishOutput = @()

foreach ($row in $csv) {
    $english = $row.EnglishDescription
    $korean = $english

    # 번역 적용
    foreach ($key in $translations.Keys) {
        $korean = $korean -replace $key, $translations[$key]
    }

    $locKey = $row.LocalizationKey

    $koreanOutput += "        [`"$locKey`"] = `"$korean`","
    $englishOutput += "        [`"$locKey`"] = `"$english`","
}

# 파일로 출력
$koreanOutput | Out-File "korean_config_translations.txt" -Encoding UTF8
$englishOutput | Out-File "english_config_translations.txt" -Encoding UTF8

Write-Host "번역 생성 완료!"
Write-Host "한국어: korean_config_translations.txt"
Write-Host "영어: english_config_translations.txt"
Write-Host "총 $($koreanOutput.Count)개 항목"

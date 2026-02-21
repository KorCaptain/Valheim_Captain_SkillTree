# Config 키 영어 변환 스크립트

$replacements = @{
    # Mace Config
    "Tier0_둔기전문가_필요포인트" = "Mace_Expert_RequiredPoints"
    "Tier0_둔기전문가_피해증가" = "Mace_Expert_DamageIncrease"
    "Tier0_둔기전문가_기절확률" = "Mace_Expert_StunChance"
    "Tier0_둔기전문가_기절지속시간" = "Mace_Expert_StunDuration"
    "Tier1_둔기전문가_필요포인트" = "Mace_Step1_RequiredPoints"
    "Tier1_둔기전문가_공격력보너스" = "Mace_Step1_DamageBonus"
    "Tier2_기절강화_필요포인트" = "Mace_StunEnhance_RequiredPoints"
    "Tier2_기절강화_기절확률보너스" = "Mace_StunEnhance_ChanceBonus"
    "Tier2_기절강화_기절지속시간보너스" = "Mace_StunEnhance_DurationBonus"
    "Tier3_방어강화_필요포인트" = "Mace_DefenseBoost_RequiredPoints"
    "Tier3_방어강화_방어력보너스" = "Mace_DefenseBoost_DefenseBonus"
    "Tier4_영혼착취_필요포인트" = "Mace_SoulDrain_RequiredPoints"
    "Tier4_영혼착취_흡혈량" = "Mace_SoulDrain_LifeSteal"
    "Tier5_파쇄_필요포인트" = "Mace_Shatter_RequiredPoints"
    "Tier5_파쇄_피해증폭" = "Mace_Shatter_DamageAmplify"
    "Tier6_분노의망치_필요포인트" = "Mace_FuryHammer_RequiredPoints"
    "Tier6_분노의망치_H키데미지배율" = "Mace_FuryHammer_HKey_DamageMultiplier"
    "Tier6_분노의망치_H키쿨타임" = "Mace_FuryHammer_HKey_Cooldown"
    "Tier6_분노의망치_H키스태미나소모" = "Mace_FuryHammer_HKey_StaminaCost"
    "Tier7_수호자진심_필요포인트" = "Mace_Guardian_RequiredPoints"
    "Tier7_수호자진심_G키쿨타임" = "Mace_Guardian_GKey_Cooldown"
    "Tier7_수호자진심_G키버프지속시간" = "Mace_Guardian_GKey_BuffDuration"
    "Tier7_수호자진심_G키스태미나소모" = "Mace_Guardian_GKey_StaminaCost"
    "Tier7_수호자진심_체력증가" = "Mace_Guardian_HealthIncrease"
    "Tier7_수호자진심_이동속도증가" = "Mace_Guardian_MoveSpeedIncrease"
    "Tier7_수호자진심_공격속도증가" = "Mace_Guardian_AttackSpeedIncrease"
    "Tier7_수호자진심_방어력증가" = "Mace_Guardian_DefenseIncrease"

    # Sword Config
    "Tier0_검전문가_필요포인트" = "Sword_Expert_RequiredPoints"
    "Tier0_검전문가_피해증가" = "Sword_Expert_DamageIncrease"
    "Tier1_빠른베기_필요포인트" = "Sword_FastSlash_RequiredPoints"
    "Tier1_빠른베기_공격속도보너스" = "Sword_FastSlash_AttackSpeedBonus"
    "Tier1_반격자세_필요포인트" = "Sword_Counter_RequiredPoints"
    "Tier1_반격자세_방어보너스" = "Sword_Counter_DefenseBonus"
    "Tier2_연속베기_필요포인트" = "Sword_ComboSlash_RequiredPoints"
    "Tier2_연속베기_보너스" = "Sword_ComboSlash_Bonus"
    "Tier2_연속베기_지속시간" = "Sword_ComboSlash_Duration"
    "Tier3_칼날되치기_필요포인트" = "Sword_BladeReflect_RequiredPoints"
    "Tier3_칼날되치기_공격력보너스" = "Sword_BladeReflect_DamageBonus"
    "Tier3_칼날되치기_스태미나회복" = "Sword_BladeReflect_StaminaRestore"
    "Tier4_치명타집중_필요포인트" = "Sword_CritFocus_RequiredPoints"
    "Tier4_치명타집중_치명타확률" = "Sword_CritFocus_CritChance"
    "Tier4_치명타집중_치명타데미지" = "Sword_CritFocus_CritDamage"
    "Tier5_돌진베기_필요포인트" = "Sword_FinalCut_RequiredPoints"
    "Tier5_돌진베기_G키데미지배율" = "Sword_FinalCut_GKey_DamageMultiplier"
    "Tier5_돌진베기_G키쿨타임" = "Sword_FinalCut_GKey_Cooldown"
    "Tier5_돌진베기_G키스태미나소모" = "Sword_FinalCut_GKey_StaminaCost"
    "Tier5_패링돌격_필요포인트" = "Sword_DefSwitch_RequiredPoints"
    "Tier5_패링돌격_H키쿨타임" = "Sword_DefSwitch_HKey_Cooldown"
    "Tier5_패링돌격_H키스태미나소모" = "Sword_DefSwitch_HKey_StaminaCost"
    "Tier5_패링돌격_H키버프지속시간" = "Sword_DefSwitch_HKey_BuffDuration"
    "Tier5_패링돌격_공격력보너스" = "Sword_DefSwitch_DamageBonus"
    "Tier5_패링돌격_방어력보너스" = "Sword_DefSwitch_DefenseBonus"

    # Knife Config
    "Tier0_단검전문가_필요포인트" = "Knife_Expert_RequiredPoints"
    "Tier0_단검전문가_백스탭데미지보너스" = "Knife_Expert_BackstabDamageBonus"
    "Tier1_회피숙련_필요포인트" = "Knife_Evasion_RequiredPoints"
    "Tier1_회피숙련_회피확률" = "Knife_Evasion_Chance"
    "Tier1_회피숙련_무적시간" = "Knife_Evasion_InvincibilityTime"
    "Tier2_빠른움직임_필요포인트" = "Knife_FastMove_RequiredPoints"
    "Tier2_빠른움직임_이동속도증가" = "Knife_FastMove_MoveSpeedIncrease"
    "Tier3_전투숙련_필요포인트" = "Knife_CombatMastery_RequiredPoints"
    "Tier3_전투숙련_단검데미지증가" = "Knife_CombatMastery_DamageIncrease"
    "Tier3_전투숙련_효과지속시간" = "Knife_CombatMastery_Duration"
    "Tier4_치명타숙련_필요포인트" = "Knife_CritMastery_RequiredPoints"
    "Tier4_치명타숙련_치명타확률" = "Knife_CritMastery_CritChance"
    "Tier5_섬광자세_필요포인트" = "Knife_FlashStance_RequiredPoints"
    "Tier5_섬광자세_공격속도증가" = "Knife_FlashStance_AttackSpeedIncrease"
    "Tier6_독살_필요포인트" = "Knife_Poison_RequiredPoints"
    "Tier6_독살_독데미지" = "Knife_Poison_Damage"
    "Tier6_독살_효과시간" = "Knife_Poison_Duration"
    "Tier7_치명타공격_필요포인트" = "Knife_CritAttack_RequiredPoints"
    "Tier7_치명타공격_치명타확률증가" = "Knife_CritAttack_CritChanceIncrease"
    "Tier7_치명타공격_치명타데미지증가" = "Knife_CritAttack_CritDamageIncrease"
    "Tier8_은신_필요포인트" = "Knife_Stealth_RequiredPoints"
    "Tier8_은신_지속시간" = "Knife_Stealth_Duration"
    "Tier8_은신_쿨타임" = "Knife_Stealth_Cooldown"
    "Tier9_암살자_필요포인트" = "Knife_Assassin_RequiredPoints"
    "Tier9_암살자_G키데미지배율" = "Knife_Assassin_GKey_DamageMultiplier"
    "Tier9_암살자_G키쿨타임" = "Knife_Assassin_GKey_Cooldown"
    "Tier9_암살자_G키스태미나소모" = "Knife_Assassin_GKey_StaminaCost"
    "Tier9_암살자_공격간격" = "Knife_Assassin_AttackInterval"
}

# Mace Config
$maceFile = "C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\SkillTree\Mace_Config.cs"
$maceContent = Get-Content $maceFile -Raw -Encoding UTF8
foreach ($key in $replacements.Keys | Where-Object { $_ -like "Tier*둔기*" -or $_ -like "Tier*기절*" -or $_ -like "Tier*방어강화*" -or $_ -like "Tier*영혼착취*" -or $_ -like "Tier*파쇄*" -or $_ -like "Tier*분노의망치*" -or $_ -like "Tier*수호자진심*" }) {
    $maceContent = $maceContent.Replace($key, $replacements[$key])
}
[System.IO.File]::WriteAllText($maceFile, $maceContent, [System.Text.UTF8Encoding]::new($false))

# Sword Config
$swordFile = "C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\SkillTree\Sword_Config.cs"
$swordContent = Get-Content $swordFile -Raw -Encoding UTF8
foreach ($key in $replacements.Keys | Where-Object { $_ -like "Tier*검*" -or $_ -like "Tier*빠른베기*" -or $_ -like "Tier*반격자세*" -or $_ -like "Tier*연속베기*" -or $_ -like "Tier*칼날되치기*" -or $_ -like "Tier*치명타집중*" -or $_ -like "Tier*돌진베기*" -or $_ -like "Tier*패링돌격*" }) {
    $swordContent = $swordContent.Replace($key, $replacements[$key])
}
[System.IO.File]::WriteAllText($swordFile, $swordContent, [System.Text.UTF8Encoding]::new($false))

# Knife Config
$knifeFile = "C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\SkillTree\Knife_Config.cs"
$knifeContent = Get-Content $knifeFile -Raw -Encoding UTF8
foreach ($key in $replacements.Keys | Where-Object { $_ -like "Tier*단검*" -or $_ -like "Tier*회피*" -or $_ -like "Tier*빠른움직임*" -or $_ -like "Tier*전투숙련*" -or $_ -like "Tier*치명타*" -or $_ -like "Tier*섬광자세*" -or $_ -like "Tier*독살*" -or $_ -like "Tier*은신*" -or $_ -like "Tier*암살자*" }) {
    $knifeContent = $knifeContent.Replace($key, $replacements[$key])
}
[System.IO.File]::WriteAllText($knifeFile, $knifeContent, [System.Text.UTF8Encoding]::new($false))

Write-Host "✅ Config 키 영어 변환 완료!"

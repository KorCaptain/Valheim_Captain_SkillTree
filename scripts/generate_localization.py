#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Generate localization keys from CSV analysis
Input: config_analysis.csv
Output: config_changes.txt, korean_keys.txt, english_keys.txt
"""

import csv
import sys
from pathlib import Path

# Translation mapping (English to Korean)
TRANSLATIONS = {
    "Tier": "티어",
    "Expert": "전문가",
    "Bonus": "보너스",
    "Damage": "데미지",
    "Speed": "속도",
    "Defense": "방어",
    "Required Points": "필요 포인트",
    "Attack": "공격",
    "Health": "체력",
    "Stamina": "스태미나",
    "Eitr": "에이트르",
    "Crit": "크리티컬",
    "Critical": "크리티컬",
    "Dodge": "회피",
    "Parry": "패링",
    "Block": "블럭",
    "Resistance": "저항",
    "Armor": "방어구",
    "Weight": "무게",
    "Carry": "운반",
    "Run": "달리기",
    "Jump": "점프",
    "Swim": "수영",
    "Axe": "도끼",
    "Pickaxe": "곡괭이",
    "Sword": "검",
    "Knife": "단검",
    "Mace": "둔기",
    "Spear": "창",
    "Polearm": "폴암",
    "Bow": "활",
    "Crossbow": "석궁",
    "Staff": "지팡이",
    "Archer": "궁수",
    "Mage": "마법사",
    "Tanker": "탱커",
    "Rogue": "로그",
    "Paladin": "성기사",
    "Berserker": "버서커",
    "Production": "생산",
    "Skill": "스킬",
    "Point": "포인트",
    "Level": "레벨",
    "Cooldown": "쿨다운",
    "Duration": "지속시간",
    "Range": "범위",
    "Server Sync": "서버 동기화",
    "Chance": "확률",
    "Physical": "물리",
    "Elemental": "속성",
    "Enhancement": "강화",
    "Finisher": "마무리",
    "Two Hand": "양손",
    "Melee": "근접",
    "Ranged": "원거리",
    "Stat": "스탯",
    "Base": "기본",
    "Special": "특수",
    "One Hand": "한손",
}

def translate_to_korean(text):
    """Simple translation using keyword replacement"""
    korean = text
    for eng, kor in TRANSLATIONS.items():
        korean = korean.replace(eng, kor)
    return korean

def main():
    csv_file = Path("config_analysis.csv")
    config_changes_file = Path("config_changes.txt")
    korean_keys_file = Path("korean_keys.txt")
    english_keys_file = Path("english_keys.txt")

    if not csv_file.exists():
        print(f"Error: CSV file not found: {csv_file}")
        sys.exit(1)

    # Read CSV
    with open(csv_file, 'r', encoding='utf-8') as f:
        reader = csv.DictReader(f)
        data = list(reader)

    # Group by category
    categories = {}
    for row in data:
        category = row['Category']
        if category not in categories:
            categories[category] = []
        categories[category].append(row)

    # Generate output
    config_changes = []
    korean_keys = []
    english_keys = []

    for category, items in categories.items():
        # Category headers
        config_changes.append("// ========================================\n")
        config_changes.append(f"// {category}\n")
        config_changes.append("// ========================================\n\n")

        korean_keys.append(f"// === {category} ===\n")
        english_keys.append(f"// === {category} ===\n")

        for item in items:
            config_var_name = item['ConfigVarName']
            category_name = item['CategoryName']
            config_key_name = item['ConfigKeyName']
            english_text = item['EnglishText']
            loc_key = item['LocalizationKey']
            korean_text = translate_to_korean(english_text)

            # Config file changes
            config_changes.append(f"// Before: {config_var_name} = SkillTreeConfig.BindServerSync(config, \"{category_name}\", \"{config_key_name}\", ..., \"{english_text}\");\n")
            config_changes.append(f"{config_var_name} = SkillTreeConfig.BindServerSync(config, \"{category_name}\", \"{config_key_name}\", ..., SkillTreeConfig.GetConfigDescription(\"{loc_key}\"));\n\n")

            # Korean keys
            korean_keys.append(f"[\"{loc_key}\"] = \"{korean_text}\",\n")

            # English keys
            english_keys.append(f"[\"{loc_key}\"] = \"{english_text}\",\n")

        config_changes.append("\n")
        korean_keys.append("\n")
        english_keys.append("\n")

    # Save files
    with open(config_changes_file, 'w', encoding='utf-8') as f:
        f.writelines(config_changes)

    with open(korean_keys_file, 'w', encoding='utf-8') as f:
        f.writelines(korean_keys)

    with open(english_keys_file, 'w', encoding='utf-8') as f:
        f.writelines(english_keys)

    print("✅ Localization generation complete!")
    print(f"  Config changes: {config_changes_file}")
    print(f"  Korean keys: {korean_keys_file}")
    print(f"  English keys: {english_keys_file}")
    print(f"\nTotal keys processed: {len(data)}")

if __name__ == "__main__":
    main()

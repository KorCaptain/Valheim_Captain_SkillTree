#!/usr/bin/env python3
"""
모든 Config 파일의 하드코딩 문자열을 GetConfigDescription()으로 변경하는 스크립트
"""

import re
import os

def convert_hardcoded_to_getconfig(filepath, tree_name):
    """
    하드코딩된 문자열을 GetConfigDescription()으로 변경

    Before:
        SkillTreeConfig.BindServerSync(config,
            "Defense Tree", "Tier0_DefenseExpert_RequiredPoints", 2,
            "Tier 0: Defense Expert - Required Points");

    After:
        SkillTreeConfig.BindServerSync(config,
            "Defense Tree", "Tier0_DefenseExpert_RequiredPoints", 2,
            SkillTreeConfig.GetConfigDescription("Tier0_DefenseExpert_RequiredPoints"));
    """

    if not os.path.exists(filepath):
        print(f"❌ {filepath} 파일을 찾을 수 없습니다.")
        return 0

    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    # 패턴: "Tree Name", "KeyName", value, "hardcoded string"
    # 멀티라인 패턴
    pattern = r'(\s*SkillTreeConfig\.BindServerSync\(\s*config,\s*\n?\s*"' + re.escape(tree_name) + r'",\s*\n?\s*"([^"]+)",\s*\n?\s*[^,]+,\s*\n?\s*)"([^"]*)"\s*\n?\s*\)'

    def replacement(match):
        prefix = match.group(1)
        key_name = match.group(2)
        # hardcoded_string = match.group(3)  # 사용하지 않음

        return f'{prefix}SkillTreeConfig.GetConfigDescription("{key_name}"))'

    converted_content = re.sub(pattern, replacement, content, flags=re.MULTILINE)

    # 변경 횟수 확인
    changes = len(re.findall(pattern, content, flags=re.MULTILINE))

    if changes > 0:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(converted_content)
        print(f"✅ {os.path.basename(filepath)}: {changes}개 변경")
    else:
        print(f"⏭️  {os.path.basename(filepath)}: 변경할 항목 없음")

    return changes

def main():
    # CONFIG_RULES.md 순서대로 처리
    config_files = [
        ("SkillTree/Defense_Config.cs", "Defense Tree"),
        ("SkillTree/Production_Config.cs", "Production Tree"),
        ("SkillTree/Crossbow_Config.cs", "Crossbow Tree"),
        ("SkillTree/Staff_Config.cs", "Staff Tree"),
        ("SkillTree/Knife_Config.cs", "Knife Tree"),
        ("SkillTree/Spear_Config.cs", "Spear Tree"),
        ("SkillTree/Polearm_Config.cs", "Polearm Tree"),
        ("SkillTree/Archer_Config.cs", "Archer Job Skills"),
        ("SkillTree/Mage_Config.cs", "Mage Job Skills"),
        ("SkillTree/Tanker_Config.cs", "Tanker Job Skills"),
        ("SkillTree/Rogue_Config.cs", "Rogue Job Skills"),
        ("SkillTree/Paladin_Config.cs", "Paladin Job Skills"),
        ("SkillTree/Berserker_Config.cs", "Berserker Job Skills"),
    ]

    total_changes = 0

    print("=" * 60)
    print("하드코딩 문자열 → GetConfigDescription() 일괄 변환")
    print("=" * 60)

    for filepath, tree_name in config_files:
        changes = convert_hardcoded_to_getconfig(filepath, tree_name)
        total_changes += changes

    print("=" * 60)
    print(f"총 {total_changes}개의 하드코딩 문자열을 변환했습니다.")
    print("=" * 60)

if __name__ == '__main__':
    main()

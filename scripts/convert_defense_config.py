#!/usr/bin/env python3
"""
Defense_Config.cs의 하드코딩된 문자열을 GetConfigDescription()으로 변경하는 스크립트
"""

import re

def convert_config_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    # BindServerSync 호출에서 키 이름과 하드코딩 문자열을 찾아 변경
    # 패턴: "Defense Tree", "Tier0_DefenseExpert_RequiredPoints", 2, "하드코딩 문자열"
    pattern = r'(\s*SkillTreeConfig\.BindServerSync\([^,]+,\s*"Defense Tree",\s*"([^"]+)",\s*[^,]+,\s*)"([^"]*)"(\s*\);)'

    def replacement(match):
        prefix = match.group(1)
        key_name = match.group(2)
        # hardcoded_string = match.group(3)  # 사용하지 않음
        suffix = match.group(4)

        return f'{prefix}SkillTreeConfig.GetConfigDescription("{key_name}"){suffix}'

    converted_content = re.sub(pattern, replacement, content, flags=re.MULTILINE | re.DOTALL)

    # 변경 횟수 확인
    changes = len(re.findall(pattern, content, flags=re.MULTILINE | re.DOTALL))

    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(converted_content)

    return changes

if __name__ == '__main__':
    filepath = 'SkillTree/Defense_Config.cs'
    changes = convert_config_file(filepath)
    print(f'✅ {changes}개의 하드코딩 문자열을 GetConfigDescription()으로 변경했습니다.')

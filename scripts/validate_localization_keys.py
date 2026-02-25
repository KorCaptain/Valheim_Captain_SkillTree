#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
로컬라이제이션 키 검증 스크립트
- 코드에서 사용되는 모든 L.Get() 호출 찾기
- DefaultLanguages.cs에 해당 키가 존재하는지 확인
- 누락된 키 리포트 생성
"""

import re
import os
from pathlib import Path
from collections import defaultdict

def extract_keys_from_code(project_root):
    """코드에서 L.Get() 호출로 사용되는 모든 키 추출"""
    used_keys = set()
    key_usage = defaultdict(list)  # 키별 사용 위치

    cs_files = list(Path(project_root).rglob("*.cs"))

    # L.Get("key") 또는 L.Get("key", ...) 패턴
    pattern = r'L\.Get\(\s*"([^"]+)"'

    for cs_file in cs_files:
        if "DefaultLanguages.cs" in str(cs_file):
            continue  # DefaultLanguages.cs는 제외

        try:
            with open(cs_file, 'r', encoding='utf-8') as f:
                content = f.read()
                matches = re.finditer(pattern, content)
                for match in matches:
                    key = match.group(1)
                    used_keys.add(key)
                    key_usage[key].append(str(cs_file.relative_to(project_root)))
        except Exception as e:
            print(f"Warning: Could not read {cs_file}: {e}")

    return used_keys, key_usage

def extract_keys_from_localization(localization_file):
    """DefaultLanguages.cs에서 정의된 모든 키 추출"""
    korean_keys = set()
    english_keys = set()

    with open(localization_file, 'r', encoding='utf-8') as f:
        content = f.read()

    # 한국어 블록과 영어 블록 분리
    korean_match = re.search(r'public static Dictionary<string, string> GetKorean\(\).*?\{(.*?)return', content, re.DOTALL)
    english_match = re.search(r'public static Dictionary<string, string> GetEnglish\(\).*?\{(.*?)return', content, re.DOTALL)

    key_pattern = r'\["([^"]+)"\]'

    if korean_match:
        korean_block = korean_match.group(1)
        korean_keys = set(re.findall(key_pattern, korean_block))

    if english_match:
        english_block = english_match.group(1)
        english_keys = set(re.findall(key_pattern, english_block))

    return korean_keys, english_keys

def main():
    project_root = Path(__file__).parent.parent
    localization_file = project_root / "Localization" / "DefaultLanguages.cs"

    print("=" * 60)
    print("로컬라이제이션 키 검증")
    print("=" * 60)

    # 1. 코드에서 사용되는 키 추출
    print("\n[1/3] 코드에서 L.Get() 호출 분석 중...")
    used_keys, key_usage = extract_keys_from_code(project_root)
    print(f"  → 발견된 키: {len(used_keys)}개")

    # 2. DefaultLanguages.cs에서 정의된 키 추출
    print("\n[2/3] DefaultLanguages.cs 분석 중...")
    korean_keys, english_keys = extract_keys_from_localization(localization_file)
    print(f"  → 한국어 키: {len(korean_keys)}개")
    print(f"  → 영어 키: {len(english_keys)}개")

    # 3. 검증
    print("\n[3/3] 검증 중...")

    # 3-1. 사용되지만 정의되지 않은 키 (누락된 키)
    missing_in_korean = used_keys - korean_keys
    missing_in_english = used_keys - english_keys
    missing_in_both = missing_in_korean & missing_in_english

    # 3-2. 한국어/영어 블록 불일치
    korean_only = korean_keys - english_keys
    english_only = english_keys - korean_keys

    # 결과 출력
    has_issues = False

    if missing_in_both:
        has_issues = True
        print(f"\n❌ 누락된 키 ({len(missing_in_both)}개):")
        for key in sorted(missing_in_both):
            print(f"  - '{key}'")
            if key in key_usage:
                for usage in key_usage[key][:3]:  # 최대 3개 위치만 표시
                    print(f"      사용처: {usage}")

    if korean_only:
        has_issues = True
        print(f"\n⚠️  한국어에만 존재 ({len(korean_only)}개):")
        for key in sorted(korean_only)[:10]:  # 최대 10개만 표시
            print(f"  - '{key}'")

    if english_only:
        has_issues = True
        print(f"\n⚠️  영어에만 존재 ({len(english_only)}개):")
        for key in sorted(english_only)[:10]:  # 최대 10개만 표시
            print(f"  - '{key}'")

    if not has_issues:
        print("\n✅ 모든 검증 통과!")
        print(f"  - 사용된 키: {len(used_keys)}개")
        print(f"  - 정의된 키: {len(korean_keys & english_keys)}개")
        print(f"  - 일치율: 100%")
    else:
        print("\n" + "=" * 60)
        print("❌ 검증 실패 - 위 문제를 수정하세요")
        print("=" * 60)
        return 1

    return 0

if __name__ == "__main__":
    exit(main())

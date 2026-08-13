using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public class SkillNode
    {
        public string Id { get; set; }

        // Localization 키 (언어 변경 시 자동 업데이트용)
        public string NameKey { get; set; }
        public string DescriptionKey { get; set; }
        public object[] DescriptionArgs { get; set; }  // Description 포맷 인자

        // 실제 표시되는 텍스트 (동적 getter 또는 캐시된 값)
        private string _name;
        private string _description;

        public string Name
        {
            get
            {
                // NameKey가 있으면 동적으로 번역 반환
                if (!string.IsNullOrEmpty(NameKey))
                    return L.Get(NameKey);
                return _name;
            }
            set => _name = value;
        }

        private bool _descriptionOverridden = false;

        public string Description
        {
            get
            {
                // 명시적으로 오버라이드된 값이 있으면 최우선
                if (_descriptionOverridden)
                    return _description;
                // DescriptionKey가 있으면 동적으로 번역 반환
                if (!string.IsNullOrEmpty(DescriptionKey))
                {
                    if (DescriptionArgs != null && DescriptionArgs.Length > 0)
                        return L.Get(DescriptionKey, DescriptionArgs);
                    return L.Get(DescriptionKey);
                }
                return _description;
            }
            set { _description = value; _descriptionOverridden = true; }
        }
        public int RequiredPoints { get; set; }
        public Func<int> RequiredPointsResolver;
        // RequiredPoints는 노드 생성 시 1회 스냅샷되므로, 라이브 Config 변경을 반영하려면
        // RequiredPointsResolver가 설정된 경우 이 값을 우선 사용해야 한다.
        public int EffectiveRequiredPoints => RequiredPointsResolver?.Invoke() ?? RequiredPoints;
        // 레벨별 증가형 포인트 코스트용(선택): targetLevel(1-based) → 해당 레벨 도달에 필요한 포인트.
        // 미설정 시 EffectiveRequiredPoints(정률)로 폴백하므로 기존 스킬은 영향받지 않는다.
        public Func<int, int> RequiredPointsForLevelResolver;
        public int GetRequiredPointsForLevel(int targetLevel) =>
            RequiredPointsForLevelResolver?.Invoke(targetLevel) ?? EffectiveRequiredPoints;
        public List<ItemRequirement> RequiredItems { get; set; } = new List<ItemRequirement>(); // 필요 아이템 목록
        public List<string> Prerequisites { get; set; } = new List<string>();
        public int MaxLevel { get; set; } = 1;
        public Func<int, float> GetEffectValue; // 레벨별 효과값 반환(선택)
        public Action<int> ApplyEffect; // 효과 적용(레벨별)
        public int Tier { get; set; } // 단계(1~6)
        public Vector2 Position { get; set; } // UI상 위치
        public string Category { get; set; } // "근접", "원거리" 등
        public string IconName { get; set; } // 아이콘명(없으면 Id 사용)
        public string IconNameLocked { get; set; }    // 락(잠김) 상태 아이콘
        public string IconNameUnlocked { get; set; }  // 언락(해제) 상태 아이콘
        public List<string> NextNodes { get; set; } = new List<string>(); // 분기/다음 노드
        public List<string> MutuallyExclusive { get; set; } = new List<string>(); // 상호 배타적 스킬 목록 (둘 중 하나만 선택 가능)
        public int RequiredPlayerLevel { get; set; } = 0;
        // 성장형 스킬용: targetLevel(1-based) → 필요 플레이어 레벨(0=없음)
        public Func<int, int> RequiredPlayerLevelResolver;
        // RequiredPlayerLevelResolver가 있으면 우선 사용, 없으면 정적 RequiredPlayerLevel 사용
        public int GetEffectiveRequiredPlayerLevel(int targetLevel) =>
            RequiredPlayerLevelResolver?.Invoke(targetLevel) ?? RequiredPlayerLevel;

        public string GetRootIconName()
        {
            if (Id == "melee_root") return "melee_root";
            if (Id == "attack_root") return "attack_root";
            if (Id != null && Id.EndsWith("_root")) return Id;
            return "all_skill_root";
        }
    }
} 
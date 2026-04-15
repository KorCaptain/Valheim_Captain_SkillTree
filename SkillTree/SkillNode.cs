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

        public string Description
        {
            get
            {
                // DescriptionKey가 있으면 동적으로 번역 반환
                if (!string.IsNullOrEmpty(DescriptionKey))
                {
                    if (DescriptionArgs != null && DescriptionArgs.Length > 0)
                        return L.Get(DescriptionKey, DescriptionArgs);
                    return L.Get(DescriptionKey);
                }
                return _description;
            }
            set => _description = value;
        }
        public int RequiredPoints { get; set; }
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

        public string GetRootIconName()
        {
            if (Id == "melee_root") return "melee_root";
            if (Id == "attack_root") return "attack_root";
            if (Id != null && Id.EndsWith("_root")) return Id;
            return "all_skill_root";
        }
    }
} 
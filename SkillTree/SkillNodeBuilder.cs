using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// SkillNode 생성을 위한 빌더 패턴 클래스
    /// 스킬 데이터 정의 시 중복되는 초기화 코드를 간소화
    /// </summary>
    public class SkillNodeBuilder
    {
        private readonly SkillNode _node;

        private SkillNodeBuilder()
        {
            _node = new SkillNode
            {
                MaxLevel = 1,
                RequiredPoints = 2,
                Prerequisites = new List<string>(),
                NextNodes = new List<string>(),
                MutuallyExclusive = new List<string>(),
                RequiredItems = new List<ItemRequirement>(),
                RequiredPlayerLevel = 0,
                ApplyEffect = _ => { }
            };
        }

        /// <summary>
        /// 빌더 생성
        /// </summary>
        public static SkillNodeBuilder Create() => new SkillNodeBuilder();

        /// <summary>
        /// 근접 스킬용 빌더 생성 (기본 아이콘 설정)
        /// </summary>
        public static SkillNodeBuilder Melee(int tier)
        {
            return Create()
                .Category("근접")
                .Tier(tier)
                .DefaultIcons();
        }

        /// <summary>
        /// 원거리 스킬용 빌더 생성 (기본 아이콘 설정)
        /// </summary>
        public static SkillNodeBuilder Ranged(int tier)
        {
            return Create()
                .Category("원거리")
                .Tier(tier)
                .DefaultIcons();
        }

        /// <summary>
        /// 방어 스킬용 빌더 생성 (기본 아이콘 설정)
        /// </summary>
        public static SkillNodeBuilder Defense(int tier)
        {
            return Create()
                .Category("방어")
                .Tier(tier)
                .DefaultIcons();
        }

        /// <summary>
        /// 지팡이 스킬용 빌더 생성 (기본 아이콘 설정)
        /// </summary>
        public static SkillNodeBuilder Staff(int tier)
        {
            return Create()
                .Category("지팡이")
                .Tier(tier)
                .DefaultIcons();
        }

        /// <summary>
        /// 직업 스킬용 빌더 생성
        /// </summary>
        public static SkillNodeBuilder Job()
        {
            return Create()
                .Category("직업")
                .Tier(7)
                .Points(0);
        }

        // ===== 기본 속성 설정 =====

        public SkillNodeBuilder Id(string id)
        {
            _node.Id = id;
            return this;
        }

        public SkillNodeBuilder Name(string name)
        {
            _node.Name = name;
            return this;
        }

        public SkillNodeBuilder Desc(string description)
        {
            _node.Description = description;
            return this;
        }

        public SkillNodeBuilder Points(int points)
        {
            _node.RequiredPoints = points;
            return this;
        }

        public SkillNodeBuilder MaxLevel(int maxLevel)
        {
            _node.MaxLevel = maxLevel;
            return this;
        }

        public SkillNodeBuilder Tier(int tier)
        {
            _node.Tier = tier;
            return this;
        }

        public SkillNodeBuilder Pos(float x, float y)
        {
            _node.Position = new Vector2(x, y);
            return this;
        }

        public SkillNodeBuilder Position(Vector2 pos)
        {
            _node.Position = pos;
            return this;
        }

        public SkillNodeBuilder Category(string category)
        {
            _node.Category = category;
            return this;
        }

        // ===== 선행/후속 노드 설정 =====

        public SkillNodeBuilder Prereqs(params string[] prereqs)
        {
            _node.Prerequisites = new List<string>(prereqs);
            return this;
        }

        public SkillNodeBuilder Next(params string[] nextNodes)
        {
            _node.NextNodes = new List<string>(nextNodes);
            return this;
        }

        public SkillNodeBuilder Exclusive(params string[] exclusiveNodes)
        {
            _node.MutuallyExclusive = new List<string>(exclusiveNodes);
            return this;
        }

        // ===== 아이콘 설정 =====

        public SkillNodeBuilder Icons(string locked, string unlocked)
        {
            _node.IconNameLocked = locked;
            _node.IconNameUnlocked = unlocked;
            return this;
        }

        public SkillNodeBuilder Icon(string iconName)
        {
            _node.IconName = iconName;
            _node.IconNameLocked = $"{iconName}_lock";
            _node.IconNameUnlocked = $"{iconName}_unlock";
            return this;
        }

        public SkillNodeBuilder DefaultIcons()
        {
            return Icons("all_skill_lock", "all_skill_unlock");
        }

        // ===== 요구사항 설정 =====

        public SkillNodeBuilder RequiredLevel(int level)
        {
            _node.RequiredPlayerLevel = level;
            return this;
        }

        public SkillNodeBuilder RequiredItems(params ItemRequirement[] items)
        {
            _node.RequiredItems = new List<ItemRequirement>(items);
            return this;
        }

        // ===== 효과 설정 =====

        public SkillNodeBuilder Effect(Action<int> applyEffect)
        {
            _node.ApplyEffect = applyEffect;
            return this;
        }

        public SkillNodeBuilder EffectValue(Func<int, float> getEffectValue)
        {
            _node.GetEffectValue = getEffectValue;
            return this;
        }

        public SkillNodeBuilder NoEffect()
        {
            _node.ApplyEffect = _ => { };
            return this;
        }

        /// <summary>
        /// SkillNode 인스턴스 반환
        /// </summary>
        public SkillNode Build()
        {
            return _node;
        }

        /// <summary>
        /// SkillTreeManager에 직접 등록
        /// </summary>
        public void Register()
        {
            var manager = SkillTreeManager.Instance;
            if (manager != null)
            {
                manager.AddSkill(_node);
            }
            else
            {
                Plugin.Log.LogError($"[SkillNodeBuilder] SkillTreeManager가 null입니다. 스킬 '{_node.Id}' 등록 실패");
            }
        }
    }

    /// <summary>
    /// SkillNodeBuilder 확장 - 설명 문자열 빌더
    /// </summary>
    public static class SkillDescriptionBuilder
    {
        /// <summary>
        /// 무기 착용 조건 메시지 추가
        /// </summary>
        public static string WithWeaponCondition(this string desc, string weaponName)
        {
            return $"{desc}\n<color=#DDA0DD><size=16>※ {weaponName} 착용시 효과발동</size></color>";
        }

        /// <summary>
        /// 스킬 유형 추가
        /// </summary>
        public static string WithSkillType(this string desc, string skillType, string key, string weaponType)
        {
            return $"{desc}\n스킬유형: {skillType}, {key}키\n무기타입: {weaponType}";
        }

        /// <summary>
        /// 쿨타임 정보 추가
        /// </summary>
        public static string WithCooldown(this string desc, float cooldown)
        {
            return $"{desc}\n쿨타임: {cooldown}초";
        }

        /// <summary>
        /// 소모량 정보 추가
        /// </summary>
        public static string WithCost(this string desc, string costType, float amount)
        {
            return $"{desc}\n소모: {costType} {amount}";
        }
    }
}

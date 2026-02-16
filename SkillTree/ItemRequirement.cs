using System;
using System.Collections.Generic;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 스킬 언락에 필요한 아이템 요구사항
    /// </summary>
    [Serializable]
    public class ItemRequirement
    {
        public string ItemName { get; set; }        // 아이템 이름 (Valheim 내부 이름)
        public string DisplayNameKey { get; set; }  // 표시될 이름 (로컬라이제이션 키)
        public int Quantity { get; set; }           // 필요 수량
        public bool IsConsumed { get; set; } = true; // 소모 여부 (기본: 소모)

        // 호환성을 위한 DisplayName 프로퍼티 (L.Get()으로 자동 변환)
        public string DisplayName => L.Get(DisplayNameKey);

        public ItemRequirement() { }

        public ItemRequirement(string itemName, string displayNameKey, int quantity, bool isConsumed = true)
        {
            ItemName = itemName;
            DisplayNameKey = displayNameKey;
            Quantity = quantity;
            IsConsumed = isConsumed;
        }

        public override string ToString()
        {
            if (IsConsumed)
                return $"{DisplayName} x{Quantity}{L.Get("item_consumed")}";
            else
                return $"{DisplayName} x{Quantity}";
        }
    }

    /// <summary>
    /// 스킬별 아이템 요구사항 데이터베이스
    /// </summary>
    public static class SkillItemRequirements
    {
        private static readonly Dictionary<string, List<ItemRequirement>> _requirements = new Dictionary<string, List<ItemRequirement>>();

        static SkillItemRequirements()
        {
            InitializeRequirements();
        }

        private static void InitializeRequirements()
        {
            // ===== 생산 전문가 트리만 재료 기반 ===== (dropPrefab.name 형식 사용)
            AddRequirement("production_root", new ItemRequirement("Wood", "item_wood", 100));

            // 초보 일꾼 (1단계)
            AddRequirement("novice_worker", new ItemRequirement("Wood", "item_wood", 100),
                                          new ItemRequirement("Raspberry", "item_raspberry", 20));

            // 벌목 Lv2
            AddRequirement("woodcutting_lv2", new ItemRequirement("Wood", "item_wood", 150));

            // 벌목 Lv3
            AddRequirement("woodcutting_lv3", new ItemRequirement("Wood", "item_wood", 200));

            // 채집 Lv2
            AddRequirement("gathering_lv2", new ItemRequirement("Mushroom", "item_mushroom", 50));

            // 채집 Lv3
            AddRequirement("gathering_lv3",
                new ItemRequirement("Mushroom", "item_mushroom", 30),
                new ItemRequirement("Raspberry", "item_raspberry", 50));

            // 채광 Lv2 - 구리광석으로 변경, 소모형으로 변경
            AddRequirement("mining_lv2", new ItemRequirement("CopperOre", "item_copper_ore", 20, true)); // 소모

            // 채광 Lv3 - 철 광석 30개 소모형으로 변경
            AddRequirement("mining_lv3", new ItemRequirement("IronOre", "item_iron_ore", 30, true)); // 소모

            AddRequirement("crafting_lv2",
                new ItemEquipConsumeRequirement("SwordBronze", "item_bronze_sword"),
                new ItemEquipConsumeRequirement("HelmetBronze", "item_bronze_helmet"));

            AddRequirement("crafting_lv3",
                new ItemEquipConsumeRequirement("SwordIron", "item_iron_sword"),
                new ItemEquipConsumeRequirement("HelmetIron", "item_iron_helmet"));

            AddRequirement("woodcutting_lv4", new ItemQuantityRequirement("Wood", "item_wood", 400));
            AddRequirement("gathering_lv4",
                new ItemRequirement("Mushroom", "item_mushroom", 30),
                new ItemRequirement("Raspberry", "item_raspberry", 30),
                new ItemRequirement("Blueberries", "item_blueberries", 50));

            // 채광 Lv4 - 은 광석 25개 소모형으로 변경
            AddRequirement("mining_lv4", new ItemRequirement("SilverOre", "item_silver_ore", 25, true)); // 소모
            AddRequirement("crafting_lv4",
                new ItemEquipConsumeRequirement("SwordSilver", "item_silver_sword"),
                new ItemEquipConsumeRequirement("HelmetDrake", "item_drake_helmet"));

            // 방어 전문가 트리: 신경강화 - 단검 또는 활 착용 필요 (선택적)
            AddRequirement("defense_Step6_attack",
                new ItemEquipRequirement("KnifeCopper", "item_copper_knife"),
                new ItemEquipRequirement("Bow", "item_crude_bow"));

            // 폴암 전문가 - 창 착용 필요 (확장성: Spear/spear 포함 프리팹 자동 인식)
            // ItemManager.IsSpearWeapon()에서 프리팹명에 "spear" 포함 시 자동 인식
            AddRequirement("polearm_expert", new ItemEquipRequirement("SpearFlint", "item_spear"));

            // 폴암 전문가 단계별 스킬들 - 창 착용 필요 (확장성 지원)
            AddRequirement("polearm_step1_spin", new ItemEquipRequirement("SpearFlint", "item_spear"));
            AddRequirement("polearm_step1_suppress", new ItemEquipRequirement("SpearFlint", "item_spear"));
            AddRequirement("polearm_step2_hero", new ItemEquipRequirement("SpearFlint", "item_spear"));
            AddRequirement("polearm_step3_area", new ItemEquipRequirement("SpearFlint", "item_spear"));
            AddRequirement("polearm_step3_ground", new ItemEquipRequirement("SpearFlint", "item_spear"));
            AddRequirement("polearm_step4_moon", new ItemEquipRequirement("SpearFlint", "item_spear"));
            AddRequirement("polearm_step4_charge", new ItemEquipRequirement("SpearFlint", "item_spear"));
            AddRequirement("polearm_step5_king", new ItemEquipRequirement("SpearFlint", "item_spear"));
        }

        /// <summary>
        /// 단일 아이템 요구사항 추가
        /// </summary>
        private static void AddRequirement(string skillId, ItemRequirement requirement)
        {
            if (!_requirements.ContainsKey(skillId))
                _requirements[skillId] = new List<ItemRequirement>();
            _requirements[skillId].Add(requirement);
        }

        /// <summary>
        /// 복수 아이템 요구사항 추가
        /// </summary>
        private static void AddRequirement(string skillId, params ItemRequirement[] requirements)
        {
            if (!_requirements.ContainsKey(skillId))
                _requirements[skillId] = new List<ItemRequirement>();
            _requirements[skillId].AddRange(requirements);
        }

        /// <summary>
        /// 스킬의 아이템 요구사항 반환
        /// </summary>
        public static List<ItemRequirement> GetRequirements(string skillId)
        {
            return _requirements.ContainsKey(skillId) ? _requirements[skillId] : new List<ItemRequirement>();
        }

        /// <summary>
        /// 스킬에 아이템 요구사항이 있는지 확인
        /// </summary>
        public static bool HasRequirements(string skillId)
        {
            return _requirements.ContainsKey(skillId) && _requirements[skillId].Count > 0;
        }

        /// <summary>
        /// 모든 스킬 ID 목록 반환
        /// </summary>
        public static IEnumerable<string> GetAllSkillIds()
        {
            return _requirements.Keys;
        }

        /// <summary>
        /// 생산 전문가 트리 스킬인지 확인
        /// </summary>
        public static bool IsProductionSkill(string skillId)
        {
            var productionSkills = new HashSet<string>
            {
                "production_root", "novice_worker",
                "woodcutting_lv2", "gathering_lv2", "mining_lv2", "crafting_lv2",
                "woodcutting_lv3", "gathering_lv3", "mining_lv3", "crafting_lv3",
                "woodcutting_lv4", "gathering_lv4", "mining_lv4", "crafting_lv4"
            };
            return productionSkills.Contains(skillId);
        }
    }

    /// <summary>
    /// 수량 조건 기반 아이템 요구사항 (인벤토리에 특정 수량 보유 시)
    /// </summary>
    public class ItemQuantityRequirement : ItemRequirement
    {
        public ItemQuantityRequirement(string itemName, string displayNameKey, int requiredQuantity)
            : base(itemName, displayNameKey, requiredQuantity, false)
        {
            // 수량 조건은 소모하지 않음
        }

        public override string ToString()
        {
            return L.Get("item_quantity_required", DisplayName, Quantity);
        }
    }

    /// <summary>
    /// 장착 조건 기반 아이템 요구사항 (특정 장비를 착용한 상태)
    /// </summary>
    public class ItemEquipRequirement : ItemRequirement
    {
        public ItemEquipRequirement(string itemName, string displayNameKey)
            : base(itemName, displayNameKey, 1, false)
        {
            // 장착 조건은 소모하지 않음
        }

        public override string ToString()
        {
            return L.Get("item_equip_required", DisplayName);
        }
    }

    /// <summary>
    /// 장착 후 소모 조건 기반 아이템 요구사항 (착용한 상태에서 해당 아이템을 소모)
    /// </summary>
    public class ItemEquipConsumeRequirement : ItemRequirement
    {
        public ItemEquipConsumeRequirement(string itemName, string displayNameKey)
            : base(itemName, displayNameKey, 1, true)
        {
            // 장착 후 소모
        }

        public override string ToString()
        {
            return L.Get("item_equip_consume", DisplayName);
        }
    }
}
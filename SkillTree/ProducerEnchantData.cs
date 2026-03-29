using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가 마법부여 데이터 관리
    /// asset/Producer_Enchant.json에서 로드 → 수치/슬롯 풀 데이터 제공
    /// </summary>
    public static class ProducerEnchantData
    {
        // ----------------------------------------------------------------
        // 데이터 구조체
        // ----------------------------------------------------------------
        public struct LevelRange
        {
            public float Min;
            public float Max;
        }

        public class EnchantTypeDef
        {
            public int    Id;
            public string Name;
            public string DisplayKey;
            public string Unit;
            public LevelRange Lv3;
            public LevelRange Lv4;
            public LevelRange Lv5;
        }

        public struct WeightedEnchant
        {
            public int Id;
            public int Weight;
        }

        // ----------------------------------------------------------------
        // 내부 저장소
        // ----------------------------------------------------------------
        private static readonly Dictionary<int, EnchantTypeDef> _types
            = new Dictionary<int, EnchantTypeDef>();

        private static readonly Dictionary<string, List<WeightedEnchant>> _pools
            = new Dictionary<string, List<WeightedEnchant>>();

        private static bool _loaded = false;

        // ----------------------------------------------------------------
        // 공개 API
        // ----------------------------------------------------------------

        /// <summary>
        /// Plugin 초기화 시 1회 호출. EmbeddedResource에서 JSON 파싱.
        /// </summary>
        public static void Load()
        {
            if (_loaded) return;
            try
            {
                string json = ReadEmbeddedJson();
                ParseJson(json);
                _loaded = true;
                Plugin.Log.LogInfo("[ProducerEnchantData] JSON 로드 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[ProducerEnchantData] 로드 실패: {ex.Message}");
                LoadDefaults();
                _loaded = true;
            }
        }

        /// <summary>
        /// 특정 마법부여 타입의 레벨별 수치 범위 반환
        /// </summary>
        public static LevelRange GetRange(int enchantId, int level)
        {
            if (!_loaded) Load();
            if (!_types.TryGetValue(enchantId, out var def))
                return new LevelRange { Min = 0f, Max = 0f };
            return level switch
            {
                3 => def.Lv3,
                4 => def.Lv4,
                _ => def.Lv5
            };
        }

        /// <summary>
        /// 슬롯 키에 해당하는 마법부여 풀 반환
        /// slotKey: "Weapon","Bow","Crossbow","Helmet","Chest","Legs","Shoulder","Accessory"
        /// </summary>
        public static List<WeightedEnchant> GetPool(string slotKey)
        {
            if (!_loaded) Load();
            return _pools.TryGetValue(slotKey, out var pool) ? pool : new List<WeightedEnchant>();
        }

        /// <summary>
        /// 가중치 기반 랜덤 선택 → 마법부여 ID 반환
        /// </summary>
        public static int PickRandom(string slotKey)
        {
            var pool = GetPool(slotKey);
            if (pool.Count == 0) return 0;

            int totalWeight = 0;
            foreach (var e in pool) totalWeight += e.Weight;

            int roll = UnityEngine.Random.Range(0, totalWeight);
            int cumulative = 0;
            foreach (var e in pool)
            {
                cumulative += e.Weight;
                if (roll < cumulative) return e.Id;
            }
            return pool[pool.Count - 1].Id;
        }

        /// <summary>
        /// 표시 키 반환
        /// </summary>
        public static string GetDisplayKey(int enchantId)
        {
            if (!_loaded) Load();
            return _types.TryGetValue(enchantId, out var def) ? def.DisplayKey : "producer_enchant_notify";
        }

        /// <summary>
        /// 단위 반환 (%, ms, 또는 빈 문자열)
        /// </summary>
        public static string GetUnit(int enchantId)
        {
            if (!_loaded) Load();
            return _types.TryGetValue(enchantId, out var def) ? def.Unit : "";
        }

        // ----------------------------------------------------------------
        // 내부 구현
        // ----------------------------------------------------------------

        private static string ReadEmbeddedJson()
        {
            var assembly = Assembly.GetExecutingAssembly();
            // EmbeddedResource LogicalName: CaptainSkillTree.asset.Producer_Enchant.json
            string resourceName = "CaptainSkillTree.asset.Producer_Enchant.json";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"EmbeddedResource not found: {resourceName}");
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 최소 JSON 파서 (외부 라이브러리 없이 SimpleJSON 방식)
        /// </summary>
        private static void ParseJson(string json)
        {
            _types.Clear();
            _pools.Clear();

            // enchant_types 배열 파싱
            int typesStart = json.IndexOf("\"enchant_types\"");
            if (typesStart >= 0)
            {
                int arrStart = json.IndexOf('[', typesStart);
                string typesArr = ExtractArray(json, arrStart);
                foreach (string obj in SplitObjects(typesArr))
                {
                    var def = ParseEnchantType(obj);
                    if (def != null) _types[def.Id] = def;
                }
            }

            // slot_pools 객체 파싱
            int poolsStart = json.IndexOf("\"slot_pools\"");
            if (poolsStart >= 0)
            {
                int objStart = json.IndexOf('{', poolsStart);
                string poolsObj = ExtractObject(json, objStart);
                ParseSlotPools(poolsObj);
            }
        }

        private static EnchantTypeDef ParseEnchantType(string obj)
        {
            try
            {
                var def = new EnchantTypeDef();
                def.Id         = int.Parse(GetJsonValue(obj, "id"));
                def.Name       = GetJsonString(obj, "name");
                def.DisplayKey = GetJsonString(obj, "display_key");
                def.Unit       = GetJsonString(obj, "unit");
                def.Lv3        = ParseRange(obj, "lv3");
                def.Lv4        = ParseRange(obj, "lv4");
                def.Lv5        = ParseRange(obj, "lv5");
                return def;
            }
            catch { return null; }
        }

        private static LevelRange ParseRange(string obj, string key)
        {
            int keyIdx = obj.IndexOf($"\"{key}\"");
            if (keyIdx < 0) return default;
            int braceStart = obj.IndexOf('{', keyIdx);
            string inner = ExtractObject(obj, braceStart);
            return new LevelRange
            {
                Min = float.Parse(GetJsonValue(inner, "min"),
                    System.Globalization.CultureInfo.InvariantCulture),
                Max = float.Parse(GetJsonValue(inner, "max"),
                    System.Globalization.CultureInfo.InvariantCulture)
            };
        }

        private static void ParseSlotPools(string obj)
        {
            // 각 슬롯 키와 배열 파싱
            string[] slotKeys = { "Weapon", "Bow", "Crossbow", "Helmet", "Chest",
                                  "Legs", "Shoulder", "Accessory" };
            foreach (string slot in slotKeys)
            {
                int keyIdx = obj.IndexOf($"\"{slot}\"");
                if (keyIdx < 0) continue;
                int arrStart = obj.IndexOf('[', keyIdx);
                string arr = ExtractArray(obj, arrStart);
                var pool = new List<WeightedEnchant>();
                foreach (string entry in SplitObjects(arr))
                {
                    string idStr = GetJsonValue(entry, "id");
                    string wStr  = GetJsonValue(entry, "weight");
                    if (!string.IsNullOrEmpty(idStr))
                    {
                        pool.Add(new WeightedEnchant
                        {
                            Id     = int.Parse(idStr),
                            Weight = string.IsNullOrEmpty(wStr) ? 1 : int.Parse(wStr)
                        });
                    }
                }
                if (pool.Count > 0) _pools[slot] = pool;
            }
        }

        // ---- 미니 JSON 유틸 ----

        private static string GetJsonValue(string json, string key)
        {
            int idx = json.IndexOf($"\"{key}\"");
            if (idx < 0) return "";
            int colon = json.IndexOf(':', idx);
            if (colon < 0) return "";
            int start = colon + 1;
            while (start < json.Length && (json[start] == ' ' || json[start] == '\n'
                || json[start] == '\r' || json[start] == '\t')) start++;
            if (start >= json.Length) return "";
            if (json[start] == '"')
                return GetJsonString(json, key);
            int end = start;
            while (end < json.Length && json[end] != ',' && json[end] != '}'
                && json[end] != ']' && json[end] != '\n') end++;
            return json.Substring(start, end - start).Trim();
        }

        private static string GetJsonString(string json, string key)
        {
            int idx = json.IndexOf($"\"{key}\"");
            if (idx < 0) return "";
            int colon = json.IndexOf(':', idx);
            int q1 = json.IndexOf('"', colon + 1);
            if (q1 < 0) return "";
            int q2 = json.IndexOf('"', q1 + 1);
            if (q2 < 0) return "";
            return json.Substring(q1 + 1, q2 - q1 - 1);
        }

        private static string ExtractObject(string json, int start)
        {
            int depth = 0;
            for (int i = start; i < json.Length; i++)
            {
                if (json[i] == '{') depth++;
                else if (json[i] == '}') { depth--; if (depth == 0) return json.Substring(start, i - start + 1); }
            }
            return "";
        }

        private static string ExtractArray(string json, int start)
        {
            int depth = 0;
            for (int i = start; i < json.Length; i++)
            {
                if (json[i] == '[') depth++;
                else if (json[i] == ']') { depth--; if (depth == 0) return json.Substring(start, i - start + 1); }
            }
            return "";
        }

        private static IEnumerable<string> SplitObjects(string arr)
        {
            int depth = 0;
            int start = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '{') { if (depth++ == 0) start = i; }
                else if (arr[i] == '}')
                {
                    if (--depth == 0 && start >= 0)
                    {
                        yield return arr.Substring(start, i - start + 1);
                        start = -1;
                    }
                }
            }
        }

        // ----------------------------------------------------------------
        // 폴백: JSON 로드 실패 시 하드코딩 기본값
        // ----------------------------------------------------------------
        private static void LoadDefaults()
        {
            Plugin.Log.LogWarning("[ProducerEnchantData] 기본값으로 폴백");

            void Add(int id, string name, string key, string unit,
                float l3min, float l3max, float l4min, float l4max, float l5min, float l5max)
            {
                _types[id] = new EnchantTypeDef
                {
                    Id = id, Name = name, DisplayKey = key, Unit = unit,
                    Lv3 = new LevelRange { Min = l3min, Max = l3max },
                    Lv4 = new LevelRange { Min = l4min, Max = l4max },
                    Lv5 = new LevelRange { Min = l5min, Max = l5max }
                };
            }

            Add(1, "WeaponDmg",      "producer_enchant_weapon_dmg",      "%",  3,5,  6,8,  9,12);
            Add(2, "Armor",          "producer_enchant_armor",            "%",  3,5,  6,8,  9,12);
            Add(3, "MaxHP",          "producer_enchant_hp",               "%",  3,5,  6,8,  9,12);
            Add(4, "WeaponSpd",      "producer_enchant_weapon_spd",       "%",  3,5,  6,8,  9,12);
            Add(5, "MaxStamina",     "producer_enchant_stamina_pct",      "%",  5,8,  9,12, 13,15);
            Add(6, "BowCrit",        "producer_enchant_bow_crit",         "%",  3,5,  6,8,  9,12);
            Add(7, "CrossbowReload", "producer_enchant_crossbow_reload",  "ms", 20,40, 45,70, 75,100);
            Add(8, "CooldownReduce", "producer_enchant_cooldown_reduce",  "%",  3,5,  6,8,  9,12);
            Add(9, "DodgeRoll",      "producer_enchant_dodge_roll",       "%",  3,5,  6,8,  9,12);
            Add(10,"MoveSpeed",      "producer_enchant_move_speed",       "%",  3,5,  6,8,  9,12);
            Add(11,"Eitr",           "producer_enchant_eitr",             "",   5,8,  9,12, 13,15);
            Add(12,"InvWeight",      "producer_enchant_inv_weight",       "",   80,100, 100,125, 130,150);
            Add(13,"EitrRegen",      "producer_enchant_eitr_regen",       "%",  5,8,  9,12, 13,15);
            Add(14,"JumpForce",      "producer_enchant_jump_force",       "%",  5,8,  9,12, 13,15);

            _pools["Weapon"]    = new List<WeightedEnchant> { new WeightedEnchant{Id=1,Weight=1}, new WeightedEnchant{Id=4,Weight=1} };
            _pools["Bow"]       = new List<WeightedEnchant> { new WeightedEnchant{Id=1,Weight=1}, new WeightedEnchant{Id=6,Weight=1} };
            _pools["Crossbow"]  = new List<WeightedEnchant> { new WeightedEnchant{Id=1,Weight=1}, new WeightedEnchant{Id=7,Weight=1} };
            _pools["Helmet"]    = new List<WeightedEnchant> { new WeightedEnchant{Id=3,Weight=1}, new WeightedEnchant{Id=8,Weight=1} };
            _pools["Chest"]     = new List<WeightedEnchant> { new WeightedEnchant{Id=3,Weight=1}, new WeightedEnchant{Id=2,Weight=1} };
            _pools["Legs"]      = new List<WeightedEnchant> { new WeightedEnchant{Id=9,Weight=1}, new WeightedEnchant{Id=10,Weight=1} };
            _pools["Shoulder"]  = new List<WeightedEnchant> { new WeightedEnchant{Id=5,Weight=1}, new WeightedEnchant{Id=11,Weight=1} };
            _pools["Accessory"] = new List<WeightedEnchant> { new WeightedEnchant{Id=12,Weight=1}, new WeightedEnchant{Id=13,Weight=1}, new WeightedEnchant{Id=14,Weight=1} };
        }
    }
}

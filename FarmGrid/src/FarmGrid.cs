using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가 팜그리드 시스템
    /// - 경작 도구 장착 시 추가 식재 위치를 LineRenderer로 시각화
    /// - 씨앗 심기 시 레벨에 따라 추가 자동 식재 (Plant.Awake 시점)
    /// </summary>
    public static class FarmGrid
    {
        #region Plant Cache (식물 grow radius 캐시)

        private static Dictionary<string, float> plantsConfiguration = new Dictionary<string, float>();
        private static readonly Dictionary<string, float> vanillaPlantsDefaults = new Dictionary<string, float>
        {
            { "BlueberryBush", 0.5f }, { "CloudberryBush", 0.5f }, { "RaspberryBush", 0.5f },
            { "Pickable_Dandelion", 0.5f }, { "Pickable_Fiddlehead", 0.5f }, { "Pickable_Mushroom", 0.5f },
            { "Pickable_Mushroom_blue", 0.5f }, { "Pickable_Mushroom_yellow", 0.5f },
            { "Pickable_SmokePuff", 0.5f }, { "Pickable_Thistle", 0.5f }
        };

        public static void SetupPlantCache()
        {
            if (ZNetScene.instance == null) return;
            plantsConfiguration.Clear();

            foreach (var obj in ZNetScene.instance.m_prefabs)
            {
                var plant = obj.GetComponent<Plant>();
                if (plant == null || plantsConfiguration.ContainsKey(plant.name)) continue;
                plantsConfiguration.Add(plant.name, plant.m_growRadius);
                foreach (var grownPrefab in plant.m_grownPrefabs)
                {
                    if (!plantsConfiguration.ContainsKey(grownPrefab.name))
                        plantsConfiguration.Add(grownPrefab.name, plant.m_growRadius);
                }
            }

            foreach (var vanilla in vanillaPlantsDefaults)
            {
                if (!plantsConfiguration.ContainsKey(vanilla.Key))
                    plantsConfiguration.Add(vanilla.Key, vanilla.Value);
            }
        }

        #endregion

        #region Helpers

        private static readonly HashSet<string> CultivatorNames = new HashSet<string>
        {
            "Cultivator", "$item_cultivator", "cultivator",
            "Hoe", "$item_hoe", "hoe"
        };

        private static bool IsUsingCultivator(Player player)
        {
            // GetCurrentWeapon()은 Unarmed 반환 → m_rightItem으로 직접 접근
            var item = Traverse.Create(player).Field("m_rightItem").GetValue<ItemDrop.ItemData>();
            if (item == null) return false;
            var name = item.m_shared?.m_name;
            return CultivatorNames.Contains(name) || (name?.ToLower().Contains("cultivat") ?? false);
        }

        private static bool IsPlantPiece(Piece piece)
        {
            if (piece == null) return false;
            return piece.GetComponent<Plant>() != null || piece.GetComponent<Pickable>() != null;
        }

        private static float GetSpacingFromObject(GameObject go)
        {
            var plant = go.GetComponent<Plant>();
            if (plant != null) return Mathf.Max(plant.m_growRadius * 2f + 0.01f, 1.0f);

            string prefabName = Utils.GetPrefabName(go);
            if (plantsConfiguration.TryGetValue(prefabName, out float size))
                return size * 2f + 0.01f;
            return 1.0f;
        }

        /// <summary>
        /// rows×cols 2D 격자 위치 반환. 중심(origin)을 기준으로 right·forward 방향으로 배치.
        /// </summary>
        private static Vector3[] GetGridPositions2D(Vector3 origin, Vector3 right, Vector3 forward, int rows, int cols, float spacing)
        {
            var positions = new Vector3[rows * cols];
            float halfRow = (rows - 1) / 2f;
            float halfCol = (cols - 1) / 2f;
            int idx = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    positions[idx++] = origin
                        + right   * ((c - halfCol) * spacing)
                        + forward * ((r - halfRow) * spacing);
            return positions;
        }

        private static bool IsValidPlantPosition(Vector3 pos)
        {
            return Physics.Raycast(pos + Vector3.up * 0.5f, Vector3.down, out _, 2f,
                LayerMask.GetMask("terrain", "Default"));
        }

        #endregion

        #region Grid Visualization (LineRenderer)

        private static GameObject[] gridLines = null;
        private static bool gridVisible = false;
        private static Material lineMaterial = null;
        private const float GRID_Y_OFFSET = 0.1f;
        private static readonly Color GRID_COLOR = new Color(0.3f, 1f, 0.3f, 1f);

        private static void InitGridLines(int count)
        {
            if (!lineMaterial)
            {
                var shader = Shader.Find("Particles/Standard Unlit")
                          ?? Shader.Find("Unlit/Color")
                          ?? Shader.Find("Sprites/Default");
                if (shader != null)
                {
                    lineMaterial = new Material(shader);
                    lineMaterial.color = Color.white;
                }
                if (lineMaterial == null)
                    Plugin.Log.LogWarning("[팜그리드] 셰이더 로드 실패 - 격자 표시 불가");
            }

            DestroyGridLines();
            gridLines = new GameObject[count];
            for (int i = 0; i < count; i++)
            {
                var go = new GameObject("FarmGridMarker");
                var lr = go.AddComponent<LineRenderer>();
                lr.material = lineMaterial;
                lr.useWorldSpace = true;
                lr.startColor = GRID_COLOR;
                lr.endColor = GRID_COLOR;
                lr.widthMultiplier = 0.05f;
                lr.positionCount = 5;
                lr.loop = false;
                lr.enabled = false;
                gridLines[i] = go;
            }
        }

        private static void DrawGridLines(Vector3[] positions, float spacing)
        {
            if (gridLines == null || gridLines.Length != positions.Length)
                InitGridLines(positions.Length);

            float r = spacing * 0.35f;
            for (int i = 0; i < positions.Length; i++)
            {
                var pos = positions[i];
                float groundY = ZoneSystem.instance != null
                    ? ZoneSystem.instance.GetGroundHeight(pos)
                    : pos.y;
                float y = groundY + GRID_Y_OFFSET;

                var lr = gridLines[i].GetComponent<LineRenderer>();
                if (lr == null) continue;
                lr.SetPosition(0, new Vector3(pos.x,     y, pos.z + r));
                lr.SetPosition(1, new Vector3(pos.x + r, y, pos.z    ));
                lr.SetPosition(2, new Vector3(pos.x,     y, pos.z - r));
                lr.SetPosition(3, new Vector3(pos.x - r, y, pos.z    ));
                lr.SetPosition(4, new Vector3(pos.x,     y, pos.z + r));
                lr.enabled = true;
            }
            gridVisible = true;
        }

        private static void HideGridLines()
        {
            if (!gridVisible || gridLines == null) return;
            foreach (var go in gridLines)
            {
                if (go == null) continue;
                var lr = go.GetComponent<LineRenderer>();
                if (lr != null) lr.enabled = false;
            }
            gridVisible = false;
        }

        private static void DestroyGridLines()
        {
            if (gridLines == null) return;
            foreach (var go in gridLines)
            {
                if (go != null) UnityEngine.Object.Destroy(go);
            }
            gridLines = null;
            gridVisible = false;
        }

        private static void UpdateGridDisplay(Player player)
        {
            var ghost = Traverse.Create(player).Field("m_placementGhost").GetValue<GameObject>();
            if (ghost == null) { HideGridLines(); return; }

            int level = ProducerSkills.GetProducerLevel(player);
            var (rows, cols) = Producer_Config.GetFarmGridDimensions(level);
            if (rows == 0 || cols == 0) { HideGridLines(); return; }

            float spacing = GetSpacingFromObject(ghost);
            var positions = GetGridPositions2D(ghost.transform.position, player.transform.right, player.transform.forward, rows, cols, spacing);
            DrawGridLines(positions, spacing);
        }

        #endregion

        #region Batch Planting

        private static bool _isAutoPlanting = false;

        #endregion

        #region Harmony Patches

        [HarmonyPatch(typeof(Player), "SetupPlacementGhost")]
        public static class Patch_Player_SetupPlacementGhost
        {
            static void Postfix()
            {
                HideGridLines();
            }
        }

        [HarmonyPatch(typeof(Player), "UpdatePlacementGhost")]
        public static class Patch_Player_UpdatePlacementGhost
        {
            static void Postfix(Player __instance)
            {
                if (__instance != Player.m_localPlayer) return;
                if (!ProducerSkills.IsProducer(__instance)) { HideGridLines(); return; }
                if (!IsUsingCultivator(__instance)) { HideGridLines(); return; }
                UpdateGridDisplay(__instance);
            }
        }

        /// <summary>
        /// 배치 식재: Plant.Awake 시점 (씨앗 생성 직후) 추가 자동 식재
        /// Player.PlacePiece는 publicized DLL에서 패치 불가 → Plant.Awake 사용
        /// </summary>
        [HarmonyPatch(typeof(Plant), "Awake")]
        public static class Patch_Plant_Awake_BatchPlant
        {
            static void Postfix(Plant __instance)
            {
                try
                {
                    if (_isAutoPlanting) return;

                    var player = Player.m_localPlayer;
                    if (player == null) return;
                    if (!ProducerSkills.IsProducer(player)) return;
                    if (!IsUsingCultivator(player)) return;

                    // 플레이어 4m 이내에서 생성된 식물만 처리
                    if (Vector3.Distance(__instance.transform.position, player.transform.position) > 4f) return;

                    var piece = __instance.GetComponent<Piece>();
                    if (!IsPlantPiece(piece)) return;

                    int level = ProducerSkills.GetProducerLevel(player);
                    var (rows, cols) = Producer_Config.GetFarmGridDimensions(level);
                    if (rows == 0 || cols == 0) return;

                    if (piece.m_resources == null || piece.m_resources.Length == 0) return;
                    var req = piece.m_resources[0];
                    if (req?.m_resItem == null) return;
                    string seedName = req.m_resItem.m_itemData.m_shared.m_name;
                    int seedAmount = Mathf.Max(req.m_amount, 1);

                    var inv = player.GetInventory();
                    if (inv == null) return;

                    string prefabName = Utils.GetPrefabName(__instance.gameObject);
                    var prefab = ZNetScene.instance?.GetPrefab(prefabName);
                    if (prefab == null) { Plugin.Log.LogWarning($"[팜그리드] 프리팹 '{prefabName}' 없음"); return; }

                    Vector3 plantedPos = __instance.transform.position;
                    float spacing = GetSpacingFromObject(__instance.gameObject);
                    var positions = GetGridPositions2D(plantedPos, player.transform.right, player.transform.forward, rows, cols, spacing);

                    int planted = 0;
                    _isAutoPlanting = true;
                    try
                    {
                        foreach (var pos in positions)
                        {
                            if (Vector3.Distance(pos, plantedPos) < 0.1f) continue;
                            if (inv.CountItems(seedName) < seedAmount) break;
                            if (!IsValidPlantPosition(pos)) continue;
                            UnityEngine.Object.Instantiate(prefab, pos, __instance.transform.rotation);
                            inv.RemoveItem(seedName, seedAmount);
                            planted++;
                        }
                    }
                    finally { _isAutoPlanting = false; }

                    if (planted > 0)
                        player.Message(MessageHud.MessageType.TopLeft, L.Get("farmgrid_planted", planted));
                }
                catch (Exception ex)
                {
                    _isAutoPlanting = false;
                    Plugin.Log.LogWarning($"[팜그리드] 배치 식재 오류: {ex.Message}");
                }
            }
        }

        [HarmonyPatch(typeof(Humanoid), "SetupVisEquipment")]
        public static class Patch_Humanoid_SetupVisEquipment
        {
            static void Postfix(Humanoid __instance)
            {
                if (__instance != Player.m_localPlayer) return;
                var rightItem = Traverse.Create(Player.m_localPlayer).Field("m_rightItem").GetValue<ItemDrop.ItemData>();
                bool isCultivator = rightItem?.m_shared?.m_name != null &&
                    CultivatorNames.Contains(rightItem.m_shared.m_name);
                if (!isCultivator)
                {
                    HideGridLines();
                    DestroyGridLines();
                }
            }
        }

        [HarmonyPriority(Priority.Last)]
        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        public static class Patch_ZNetScene_Awake
        {
            static void Postfix() => SetupPlantCache();
        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가(Producer) 농사 그리드 + 대량 식재 시스템
    /// - 경작 도구 장착 시 그리드 안내 메시지 표시
    /// - 씨앗/묘목 식재 시 레벨에 따라 추가 자동 식재
    /// </summary>
    public static class ProducerFarmingGrid
    {
        // 재귀 식재 방지 플래그
        private static bool _isAutoPlanting = false;

        // 경작 도구 이름 목록
        private static readonly HashSet<string> CultivatorNames = new HashSet<string>
        {
            "Cultivator", "$item_cultivator", "cultivator"
        };

        /// <summary>
        /// 현재 무기가 경작 도구인지 확인
        /// </summary>
        private static bool IsUsingCultivator(Player player)
        {
            var item = player?.GetCurrentWeapon();
            if (item == null) return false;
            return CultivatorNames.Contains(item.m_shared.m_name) ||
                   (item.m_shared.m_name?.ToLower().Contains("cultivat") ?? false);
        }

        /// <summary>
        /// 식재 가능한 식물 Piece인지 확인
        /// </summary>
        private static bool IsPlantPiece(Piece piece)
        {
            if (piece == null) return false;
            return piece.GetComponent<Plant>() != null ||
                   piece.GetComponent<Pickable>() != null;
        }

        #region Harmony Patches

        // ============================================================
        // 대량 식재: Player.PlacePiece Postfix
        // ============================================================
        [HarmonyPatch(typeof(Player), "PlacePiece")]
        public static class Producer_Player_PlacePiece_Patch
        {
            public static void Postfix(Player __instance, Piece piece)
            {
                try
                {
                    if (_isAutoPlanting) return; // 재귀 방지
                    if (!ProducerSkills.IsProducer(__instance)) return;
                    if (!IsUsingCultivator(__instance)) return;
                    if (!IsPlantPiece(piece)) return;

                    int level = ProducerSkills.GetProducerLevel(__instance);
                    int gridCount = Producer_Config.GetFarmGridCount(level);
                    if (gridCount <= 0) return;

                    // 씨앗 아이템 인벤토리 확인
                    string seedName = piece.m_name;
                    var inv = __instance.GetInventory();
                    if (inv == null) return;

                    // 그리드 패턴 계산 (선형 X축 식재)
                    var positions = GetGridPositions(__instance, piece, gridCount);
                    int planted = 0;

                    _isAutoPlanting = true;
                    try
                    {
                        foreach (var pos in positions)
                        {
                            // 씨앗 남은 수량 확인
                            if (inv.CountItems(seedName) <= 0) break;

                            // 위치 유효성 확인 (땅이어야 함)
                            if (!IsValidPlantPosition(pos)) continue;

                            // 식재 실행
                            var newObj = UnityEngine.Object.Instantiate(
                                piece.gameObject, pos, piece.transform.rotation);

                            if (newObj != null)
                            {
                                // 씨앗 소모
                                inv.RemoveItem(seedName, 1);
                                planted++;
                            }
                        }
                    }
                    finally
                    {
                        _isAutoPlanting = false;
                    }

                    if (planted > 0)
                    {
                        __instance.Message(MessageHud.MessageType.TopLeft,
                            L.Get("farmgrid_planted", planted));
                        Plugin.Log.LogDebug($"[제작 전문가] 자동 식재 {planted}개 완료 (그리드 {gridCount})");
                    }
                }
                catch (Exception ex)
                {
                    _isAutoPlanting = false;
                    Plugin.Log.LogWarning($"[제작 전문가] 대량 식재 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 식재 그리드 위치 계산 (식재 중심 기준 X축 나열)
        /// </summary>
        private static List<Vector3> GetGridPositions(Player player, Piece piece, int count)
        {
            var positions = new List<Vector3>();
            Vector3 origin = piece.transform.position;
            Vector3 right  = player.transform.right;

            // 간격 = 식물 크기 또는 기본 1m
            float spacing = 1.0f;
            var plantComp = piece.GetComponent<Plant>();
            if (plantComp != null)
                spacing = Mathf.Max(plantComp.m_growRadius * 2f, 1.0f);

            // 중심 좌우로 count/2씩 나열
            int half = count / 2;
            for (int i = 1; i <= count; i++)
            {
                float offset = (i <= half) ? -(i * spacing) : ((i - half) * spacing);
                positions.Add(origin + right * offset);
            }

            return positions;
        }

        private static bool IsValidPlantPosition(Vector3 pos)
        {
            // 땅 위에 위치하는지 간단히 체크
            return Physics.Raycast(pos + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 2f,
                LayerMask.GetMask("terrain", "Default"));
        }

        // ============================================================
        // 경작 도구 장착 감지: 그리드 크기 안내 메시지
        // ============================================================
        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem))]
        public static class Producer_Player_SetupEquipment_Patch
        {
            private static bool _lastCultivatorState = false;

            public static void Postfix(Humanoid __instance, bool __result)
            {
                if (!__result) return;
                if (!(__instance is Player player)) return;
                try
                {
                    if (!ProducerSkills.IsProducer(player)) return;
                    bool isCultivator = IsUsingCultivator(player);

                    if (isCultivator && !_lastCultivatorState)
                    {
                        int level = ProducerSkills.GetProducerLevel(player);
                        int grid  = Producer_Config.GetFarmGridCount(level);
                        player.Message(MessageHud.MessageType.TopLeft,
                            L.Get("farmgrid_planted", grid) + " " + L.Get("producer_passive_lv1", grid));
                    }
                    _lastCultivatorState = isCultivator;
                }
                catch (Exception) { }
            }
        }

        #endregion
    }
}

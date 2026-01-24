using EpicMMOSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.Gui;
using CaptainSkillTree.VFX;
using CaptainSkillTree.SkillTree;
using CaptainSkillTree.SkillTree.CriticalSystem; // 치명타 시스템
using static CaptainSkillTree.SkillTree.SkillEffect;

namespace CaptainSkillTree.SkillTree
{
    public static partial class SkillEffect
    {

        // 스킬별 이펙트 및 효과음 매핑 (mmo 방식 참고)
        private static readonly Dictionary<string, SkillEffectData> skillEffects = new Dictionary<string, SkillEffectData>();

        // 트롤의 재생력: 타이머 추적
        public static Dictionary<Player, float> trollRegenTimers = new Dictionary<Player, float>();

        // === 활 치명타 버프 추적 (Bow Critical Buffs) ===
        // 백스텝 샷: 구르기 후 치명타 확률 증가 (Tier 5)
        public static Dictionary<Player, float> bowBackstepShotEndTime = new Dictionary<Player, float>();

        // 크리티컬 부스트: T키 액티브 치명타 확률 100% (Tier 6)
        public static Dictionary<Player, float> bowCritBoostEndTime = new Dictionary<Player, float>();

        // static constructor - 원거리 스킬 이펙트 등록
        static SkillEffect()
        {
            InitializeSkillEffects();
        }

        private static void InitializeSkillEffects()
        {
            // 기본 스킬 이펙트들 추가
            var baseEffects = new Dictionary<string, SkillEffectData>
        {
            
            
            // 방어/생존 스킬들
            ["defense_evasion"] = new SkillEffectData("fx_dodge", "sfx_dodge", Color.green),
            ["defense_armor"] = new SkillEffectData("fx_guardstone_permitted", "sfx_build_hammer_stone", Color.gray),
            ["defense_heal"] = new SkillEffectData("fx_healing", "sfx_potion_health_medium", Color.green),
        };

            // 기본 이펙트들을 skillEffects에 추가
            foreach (var kvp in baseEffects)
            {
                skillEffects[kvp.Key] = kvp.Value;
            }

            // 원거리 스킬 이펙트들 등록
            RegisterRangedSkillEffects(skillEffects);
            
            // 근접 스킬 이펙트들 등록
            RegisterMeleeSkillEffects(skillEffects);
            
            // 속도 스킬 이펙트들 등록
            RegisterSpeedSkillEffects(skillEffects);
        }

        // 스킬 이펙트 데이터 구조체
        public struct SkillEffectData
        {
            public string EffectName;
            public string SoundName;
            public Color EffectColor;
            
            public SkillEffectData(string effect, string sound, Color color)
            {
                EffectName = effect;
                SoundName = sound;
                EffectColor = color;
            }
        }

        /// <summary>
        /// 액티브 스킬 시스템 - 이벤트 기반으로 처리됨 (주기적 체크 불필요)
        /// </summary>
        public static void OnUpdate()
        {
            // 이벤트 기반으로 변경되어 주기적 체크 불필요
            // 모든 액티브 스킬은 SkillTreeInputListener에서 키 입력 시 즉시 처리
        }
        
        // 액티브 스킬 키 입력 처리는 ActiveSkills.cs에서 담당

        // 방어 버프 적용
        public static void ApplyDefenseBuff(Player player, float bonusPercent, float duration)
        {
            var manager = player.GetComponent<MonoBehaviour>();
            if (defenseBuffCoroutine.ContainsKey(player) && defenseBuffCoroutine[player] != null)
            {
                manager.StopCoroutine(defenseBuffCoroutine[player]);
            }
            
            defenseBonus[player] = bonusPercent;
            defenseBuffCoroutine[player] = manager.StartCoroutine(RemoveDefenseBuff(player, duration));
            
            // 버프 표시 추가
            SkillBuffDisplay.Instance.ShowBuff(
                "sword_defense_buff", 
                string.Format("반격 자세 +{0}%", (bonusPercent * 100).ToString("F0")), 
                duration, 
                new Color(0f, 0.5f, 1f, 1f), // 파란색
                "🛡️"
            );
        }

        private static IEnumerator RemoveDefenseBuff(Player player, float duration)
        {
            yield return new WaitForSeconds(duration);
            if (defenseBonus.ContainsKey(player))
                defenseBonus.Remove(player);
            if (defenseBuffCoroutine.ContainsKey(player))
                defenseBuffCoroutine.Remove(player);
            
            // 화면에서 버프 제거
            SkillBuffDisplay.Instance.RemoveBuff("sword_defense_buff");
        }

        // 다음 공격 부스트 마킹
        public static void MarkNextAttackBonus(Player player, float multiplier, float duration)
        {
            nextAttackBoosted[player] = true;
            nextAttackMultiplier[player] = multiplier;
            nextAttackExpiry[player] = Time.time + duration;
            
            // 버프 표시 추가
            SkillBuffDisplay.Instance.ShowBuff(
                "sword_next_attack_boost", 
                string.Format("칼날 되치기 +{0}%", ((multiplier - 1f) * 100).ToString("F0")), 
                duration, 
                new Color(1f, 0.5f, 0f, 1f), // 주황색
                "⚔️"
            );
        }

        // 다음 공격 부스트 소비
        public static float ConsumeNextAttackBonus(Player player)
        {
            if (!nextAttackBoosted.ContainsKey(player) || !nextAttackBoosted[player]) return 1f;
            if (Time.time > nextAttackExpiry[player])
            {
                // 만료된 버프 제거
                nextAttackBoosted[player] = false;
                SkillBuffDisplay.Instance.RemoveBuff("sword_next_attack_boost");
                return 1f;
            }

            float multiplier = nextAttackMultiplier.ContainsKey(player) ? nextAttackMultiplier[player] : 1f;
            nextAttackBoosted[player] = false; // 한 번만 사용
            SkillBuffDisplay.Instance.RemoveBuff("sword_next_attack_boost"); // 버프 소비 시 제거
            return multiplier;
        }

        // 스킬 이펙트 발동 (mmo 방식 참고)
        public static void PlaySkillEffect(Player player, string skillId, Vector3? position = null)
        {
            if (!skillEffects.ContainsKey(skillId)) return;

            var effectData = skillEffects[skillId];
            var pos = position ?? player.transform.position + Vector3.up * 1.5f;

            // 이펙트 발동
            PlayEffect(effectData.EffectName, pos, player.transform.rotation);
            
            // 효과음 발동
            PlaySound(effectData.SoundName, pos);
            
            // 플로팅 텍스트 표시 (스킬명 + 색상)
            ShowSkillActivation(player, skillId, effectData.EffectColor);
        }

        /// <summary>
        /// 이펙트 발동 (VFX 재생)
        /// </summary>
        private static void PlayEffect(string effectName, Vector3 position, Quaternion rotation)
        {
            try
            {
                // VFX 재생 (사운드는 별도로 재생)
                VFXManager.PlayVFXMultiplayer(effectName, "", position, rotation, 3f);
                Plugin.Log.LogDebug($"[스킬 이펙트] {effectName} VFX 재생");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[스킬 이펙트] {effectName} 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 효과음 발동 (사운드 재생)
        /// </summary>
        private static void PlaySound(string soundName, Vector3 position)
        {
            try
            {
                // 사운드만 재생
                VFXManager.PlayVFXMultiplayer("", soundName, position, Quaternion.identity, 2f);
                Plugin.Log.LogDebug($"[스킬 사운드] {soundName} 사운드 재생");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[스킬 사운드] {soundName} 재생 실패: {ex.Message}");
            }
        }

        // 스킬 활성화 표시 (색상이 있는 플로팅 텍스트)
        private static void ShowSkillActivation(Player player, string skillId, Color color)
        {
            string skillName = GetSkillDisplayName(skillId);
            
            // MessageHud를 통한 간단한 표시
            if (MessageHud.instance != null)
            {
                MessageHud.instance.ShowMessage(MessageHud.MessageType.TopLeft, $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{skillName} 발동!</color>");
            }
        }

        // 스킬 ID를 표시용 이름으로 변환
        private static string GetSkillDisplayName(string skillId)
        {
            var displayNames = new Dictionary<string, string>
            {
                ["knife_master"] = "단검 마스터",
                ["knife_crit1"] = "단검 크리티컬",
                ["knife_crit2"] = "단검 치명타",
                ["knife_stealth"] = "은신 공격",
                ["sword_combo"] = "검 연계",
                ["sword_defense"] = "검 방어",
                ["sword_power"] = "검 강화",
                ["defense_evasion"] = "회피 강화",
                ["defense_armor"] = "방어 강화",
                ["defense_heal"] = "치유 강화",
            };
            
            // 원거리 스킬 이름 추가
            RegisterRangedSkillNames(displayNames);
            
            // 근접 스킬 이름 추가
            RegisterMeleeSkillNames(displayNames);
            
            // 속도 스킬 이름 추가
            RegisterSpeedSkillNames(displayNames);
            
            return displayNames.ContainsKey(skillId) ? displayNames[skillId] : skillId;
        }

        // 스킬 레벨 확인 헬퍼
        public static bool HasSkill(string skillId)
        {
            return SkillTreeManager.Instance.GetSkillLevel(skillId) > 0;
        }

        // 적의 체력 비율 확인
        public static float GetHealthPercent(Character character)
        {
            if (character == null) return 1f;
            return character.GetHealth() / character.GetMaxHealth();
        }

        // ===== 호환성을 위한 기본 보너스 함수들 (락/언락 기반) =====

        // 단검 스킬 보너스들 (Critical 시스템으로 통합)
        public static float GetKnifeCritChance(float mmoValue)
        {
            // 하위 호환을 위한 래퍼 함수 - Critical 시스템 사용
            var player = Player.m_localPlayer;
            if (player == null) return 0f;
            return Critical.GetKnifeCritChance(player);
        }

        public static float GetKnifeCritDamage(float mmoValue)
        {
            // 하위 호환을 위한 래퍼 함수 - CriticalDamage 시스템 사용
            var player = Player.m_localPlayer;
            if (player == null) return 0f;
            return CriticalDamage.GetKnifeCritDamage(player);
        }
        
        public static float GetKnifeStaminaReduction(float mmoValue)
        {
            return HasSkill("knife_stamina") ? 15f : 0f;
        }
        
        public static float GetKnifeAttackSpeed(float mmoValue)
        {
            return HasSkill("knife_speed") ? 15f : 0f;
        }
        
        // GetKnifeStaggerBonus 제거됨 - knife_stagger 스킬이 존재하지 않음
        // 암살술(knife_step8_assassination)이 비틀거림 효과를 처리함
        
        // 기본 스탯 보너스들 (고정값)
        public static float GetStaminaRegen(float mmoValue)
        {
            float bonus = 0f;
            // 방어 전문가 스킬 효과는 별도 구현 예정
            return bonus;
        }
        
        public static float GetStaminaReduction(float mmoValue)
        {
            float bonus = 0f;

            // defense_Step5_focus: 지구력 - 달리기/점프 스태미나 감소
            if (HasSkill("defense_Step5_focus"))
            {
                bonus += Defense_Config.FocusRunStaminaReductionValue;
                Plugin.Log.LogDebug($"[지구력] 달리기/점프 스태미나 감소: -{Defense_Config.FocusRunStaminaReductionValue}%");
            }

            return bonus;
        }
        
        public static float GetPhysicArmor(float mmoValue)
        {
            float bonus = 0f;
            // 방어 전문가 스킬 효과는 별도 구현 예정
            return bonus;
        }
        
        public static float GetMagicArmor(float mmoValue)
        {
            float bonus = 0f;
            // 방어 전문가 스킬 효과는 별도 구현 예정
            return bonus;
        }
        
        // 생산 스킬 보너스들 (고정값)
        public static float GetCurrentWoodcuttingBonus()
        {
            float bonus = 0f;
            if (HasSkill("novice_worker")) bonus += 20f;
            if (HasSkill("woodcutting_lv2")) bonus += 30f;
            if (HasSkill("woodcutting_lv3")) bonus += 40f;
            if (HasSkill("woodcutting_lv4")) bonus += 50f;
            if (HasSkill("labor_specialist")) bonus += 100f;
            return bonus;
        }
        
        public static float GetCurrentMiningBonus()
        {
            float bonus = 0f;
            if (HasSkill("mining_lv2")) bonus += 30f;
            if (HasSkill("mining_lv3")) bonus += 40f;
            if (HasSkill("mining_lv4")) bonus += 50f;
            if (HasSkill("labor_specialist")) bonus += 100f;
            return bonus;
        }
        
        // ===== 생산 스킬 툴팁 시스템 =====
        
        // 생산 도구에 대한 현재 효율 계산
        public static string GetProductionTooltip(ItemDrop.ItemData item)
        {
            if (item == null) return "";
            
            string tooltip = "";
            
            // 벌목 도구 툴팁
            if (item.m_shared.m_skillType == Skills.SkillType.Axes || 
                item.m_shared.m_skillType == Skills.SkillType.WoodCutting)
            {
                float woodcuttingBonus = GetCurrentWoodcuttingBonus();
                if (woodcuttingBonus > 0)
                {
                    tooltip += $"\n<color=#90EE90>🪓 벌목 효율: +{woodcuttingBonus}%</color>";
                }
            }
            
            // 채광 도구 툴팁
            if (item.m_shared.m_skillType == Skills.SkillType.Pickaxes)
            {
                float miningBonus = GetCurrentMiningBonus();
                if (miningBonus > 0)
                {
                    tooltip += $"\n<color=#FFD700>⛏️ 채광 효율: +{miningBonus}%</color>";
                }
            }
            
            return tooltip;
        }

        /// <summary>
        /// 표준화된 스킬 효과 텍스트 표시 (하위 호환성 유지)
        /// 새로운 코드에서는 ShowSkillEffectText 사용 권장
        /// </summary>
        public static void DrawFloatingText(Player player, string text, Color color)
        {
            ShowSkillEffectText(player, text, color, SkillEffectTextType.Standard);
        }

        public static void DrawFloatingText(Player player, string text)
        {
            DrawFloatingText(player, text, Color.white);
        }

        /// <summary>
        /// 표준화된 스킬 효과 텍스트 표시 시스템
        /// 로컬 플레이어만 볼 수 있는 캐릭터 머리 위 DamageText 구현
        /// </summary>
        private static void ShowCustomDamageText(string text, Vector3 position, Color color)
        {
            ShowSkillEffectText(Player.m_localPlayer, text, color, SkillEffectTextType.Standard);
        }

        /// <summary>
        /// 스킬 효과 텍스트 타입 열거형
        /// </summary>
        public enum SkillEffectTextType
        {
            Standard,    // 일반 스킬 효과 (화면 중앙)
            Combat,      // 전투 관련 효과 (더 눈에 띄는 위치)
            Passive,     // 패시브 효과 (조용한 표시)
            Critical,    // 중요한 효과 (강조 표시)
            XLarge       // 매우 큰 크기 (버서커 분노 등 특별한 효과)
        }

        /// <summary>
        /// 표준화된 스킬 효과 텍스트 표시 메서드
        /// 모든 스킬 트리에서 일관된 방식으로 사용
        /// </summary>
        public static void ShowSkillEffectText(Player player, string text, Color color, SkillEffectTextType type = SkillEffectTextType.Standard)
        {
            // ✅ 플레이어 사망 체크 추가
            if (player == null || player != Player.m_localPlayer || player.IsDead())
                return;

            try
            {
                string coloredText = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";
                
                switch (type)
                {
                    case SkillEffectTextType.Standard:
                        ShowStandardText(coloredText);
                        break;
                    case SkillEffectTextType.Combat:
                        ShowCombatText(coloredText);
                        break;
                    case SkillEffectTextType.Passive:
                        ShowPassiveText(coloredText);
                        break;
                    case SkillEffectTextType.Critical:
                        ShowCriticalText(coloredText);
                        break;
                    case SkillEffectTextType.XLarge:
                        ShowXLargeText(coloredText);
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[ShowSkillEffectText] 텍스트 표시 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 일반 스킬 효과 텍스트 표시 (캐릭터 머리 위 3D 월드 텍스트)
        /// </summary>
        private static void ShowStandardText(string coloredText)
        {
            var player = Player.m_localPlayer;
            if (player != null)
            {
                ShowWorldText(player, coloredText, 1.0f);
            }
        }

        /// <summary>
        /// 전투 관련 효과 텍스트 표시 (더 큰 크기로 강조)
        /// </summary>
        private static void ShowCombatText(string coloredText)
        {
            var player = Player.m_localPlayer;
            if (player != null)
            {
                ShowWorldText(player, coloredText, 1.2f);
            }
        }

        /// <summary>
        /// 패시브 효과 텍스트 표시 (작은 크기로 조용하게)
        /// </summary>
        private static void ShowPassiveText(string coloredText)
        {
            var player = Player.m_localPlayer;
            if (player != null)
            {
                ShowWorldText(player, coloredText, 0.8f);
            }
        }

        /// <summary>
        /// 중요한 효과 텍스트 표시 (큰 크기로 강조)
        /// </summary>
        private static void ShowCriticalText(string coloredText)
        {
            var player = Player.m_localPlayer;
            if (player != null)
            {
                ShowWorldText(player, $"<size=20>{coloredText}</size>", 1.5f);
            }
        }

        /// <summary>
        /// 매우 큰 효과 텍스트 표시 (극대 크기로 강조 - 버서커 분노 등)
        /// </summary>
        private static void ShowXLargeText(string coloredText)
        {
            var player = Player.m_localPlayer;
            if (player != null)
            {
                ShowWorldText(player, $"<size=60>{coloredText}</size>", 4.5f);
            }
        }

        /// <summary>
        /// 로컬 플레이어만 보는 지역 메시지 표시 (MessageHud 방식)
        /// </summary>
        private static void ShowWorldText(Player player, string text, float scale = 1.0f)
        {
            try
            {
                if (player == null || player != Player.m_localPlayer)
                    return;

                // 로컬 지역 메시지로 표시 (플레이어만 보임)
                if (MessageHud.instance != null)
                {
                    // 스케일에 따른 크기 조정
                    string sizedText = text;
                    if (scale > 1.2f)
                    {
                        sizedText = $"<size=20>{text}</size>";
                    }
                    else if (scale < 0.9f)
                    {
                        sizedText = $"<size=14>{text}</size>";
                    }
                    
                    MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, sizedText);
                    Plugin.Log.LogDebug($"[지역 메시지] 표시 완료: {sizedText}");
                }
                else if (player != null)
                {
                    // MessageHud가 없으면 Player.Message 사용
                    player.Message(MessageHud.MessageType.TopLeft, text);
                    Plugin.Log.LogDebug($"[플레이어 메시지] 표시 완료: {text}");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지역 메시지] 표시 실패: {ex.Message}");
                
                // 최후 수단: Player.Message
                if (player != null)
                {
                    player.Message(MessageHud.MessageType.TopLeft, text);
                }
            }
        }
        
        /// <summary>
        /// MMO getParameter 방식으로 변경됨 - StatusEffect 상태 관리 불필요
        /// </summary>

        // ===== mmo LevelSystem 확장: 스킬트리 효과를 mmo 시스템에 연동 =====
    

    // === [기본 6개 루트 노드 효과 설명] ===
    // 공격 전문가(attack_root): 모든 데미지 +5% (배율, 노드 해금 시 1회 적용)
    // 근접 전문가(melee_root): 근접무기 타입별 데미지 +2 (베기/관통/블런트, 고정값, 노드 해금 시 1회 적용)
    // 원거리 전문가(ranged_root): 활/석궁 관통 +2, 지팡이/완드 화염 공격 +2 (고정값, 노드 해금 시 1회 적용)
    // 속도 전문가(speed_root): 모든 이동속도 +5% (Speed.cs Player.UpdateModifiers 패치로 구현)
    // 생산 전문가(production_root): 벌목/채집/채광 수량 +1 (드랍 Postfix에서 고정값 추가)
    // 방어 전문가(defense_root): 방어 +2, 체력 +5 (고정값, 노드 해금 시 1회 적용)

    /// <summary>
    /// 속도 전문가를 위한 MMO getParameter 방식 구현
    /// StatusEffect 대신 MMO 시스템과 통합하여 안정적 적용
    /// </summary>

    /// <summary>
    /// 속도 전문가 관련 메서드들은 Speed.cs로 이동됨
    /// Valheim 표준 Player.UpdateModifiers 패치 방식으로 구현
    /// </summary>
    }
}

// mmo와 동일한 패치 포인트에 스킬트리 보너스 직접 추가
namespace CaptainSkillTree.SkillTree
{
    // Character.GetMaxHealth 패치: 체력 증가 (더 일반적인 메서드 패치)
    [HarmonyPatch(typeof(Character), nameof(Character.GetMaxHealth))]
    public static class SkillTree_Character_GetMaxHealth_Patch
    {
        [HarmonyPriority(Priority.Low)]  // mmo 이후에 실행
        public static void Postfix(Character __instance, ref float __result)
        {
            try
            {
                // Player만 처리
                if (!__instance.IsPlayer()) 
                {
                    return;
                }
                
                Player player = __instance as Player;
                if (player == null || !Player.m_localPlayer || player != Player.m_localPlayer) 
                {
                    return;
                }
                
                // === 체력 보너스 ===
                float hpBonus = 0f;
                
                // 방어 전문가 체력 효과는 별도 구현 예정
                
                __result += hpBonus;
                
                // 로그 제거: 불필요한 반복 출력
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리→발하임] GetMaxHealth 패치 오류: {ex.Message}");
            }
        }
    }
    
    // Player.GetMaxStamina 패치: 스태미나 증가 (효율적인 캐시 방식)
    // Player.GetMaxStamina 패치: mmo 방식 따라하기 - 제거 (mmo GetTotalFoodValue에서 처리)
    // 스킬트리 보너스는 mmo getParameter 패치에서 지구력 스탯으로 합산됨
    
    // Player.GetMaxEitr 패치: mmo 방식 따라하기 - 제거 (mmo GetTotalFoodValue에서 처리)
    // 스킬트리 보너스는 mmo getParameter 패치에서 지능 스탯으로 합산됨

    // mmo LevelSystem.getParameter 패치: 스킬트리 보너스를 mmo 스탯에 합산
    [HarmonyPatch]
    public static class SkillTree_MMO_LevelSystem_getParameter_Patch
    {
        /// <summary>
        /// MMO 시스템이 있는지 확인하고 패치 적용 여부 결정
        /// false 반환 시 Harmony가 이 패치를 건너뜀
        /// </summary>
        [HarmonyPrepare]
        static bool Prepare()
        {
            var type = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (type == null)
            {
                Plugin.Log.LogInfo("[MMO 패치] EpicMMOSystem 미설치 - MMO 연동 패치 건너뜀 (정상)");
                return false; // 패치 비활성화
            }

            var method = type.GetMethod("getParameter");
            if (method == null)
            {
                Plugin.Log.LogWarning("[MMO 패치] getParameter 메서드를 찾을 수 없음 - 패치 건너뜀");
                return false;
            }

            Plugin.Log.LogDebug("[MMO 패치] EpicMMOSystem 감지 - getParameter 패치 활성화");
            return true; // 패치 활성화
        }

        [HarmonyTargetMethod]
        static System.Reflection.MethodInfo TargetMethod()
        {
            // Prepare에서 이미 확인했으므로 바로 반환
            var type = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            return type?.GetMethod("getParameter");
        }

        [HarmonyPriority(Priority.High)]  // mmo 기본값 이후에 실행
        public static void Postfix(object parameter, ref int __result)
        {
            try
            {
                if (Player.m_localPlayer == null) return;
                
                string paramName = parameter?.ToString();
                if (paramName == "Intellect")
                {
                    // 스킬트리 보너스를 지능 스탯으로 합산
                    int skillTreeIntellectBonus = 0;

                    // 스킬트리에서 MMO 스탯 직접 조작 금지
                    // 모든 스킬 효과는 GetDamage, GetTotalFoodValue 등의 패치에서 직접 구현

                    __result += skillTreeIntellectBonus;
                    
                    // 로그 제거: 불필요한 반복 출력
                }
                else if (paramName == "Body")
                {
                    // 스킬트리 보너스를 지구력 스탯으로 합산
                    int skillTreeEnduranceBonus = 0;
                    
                    // 방어 전문가 스태미나 효과는 별도 구현 예정
                    
                    // 공격 증가: 지구력 효과 제거 (힘/지능으로 변경)
                    // int atkTwohandDrainLv = SkillTreeManager.Instance.GetSkillLevel("atk_twohand_drain");
                    // if (atkTwohandDrainLv > 0) skillTreeEnduranceBonus += 2 * atkTwohandDrainLv;
                    
                    __result += skillTreeEnduranceBonus;
                    
                    // 로그 제거: 불필요한 반복 출력
                }
                else if (paramName == "Strength")
                {
                    // 공격 증가: 힘 +5 (MMO 독립 구현으로 변경 - SkillEffect.StatTree.cs 참조)
                    // int atkTwohandDrainLv = SkillTreeManager.Instance.GetSkillLevel("atk_twohand_drain");
                    // if (atkTwohandDrainLv > 0) __result += (int)SkillTreeConfig.AttackStatBonusValue * atkTwohandDrainLv;
                }
                else if (paramName == "Intellect")
                {
                    // 공격 증가: 지능 +5 (MMO 독립 구현으로 변경 - SkillEffect.StatTree.cs 참조)
                    // int atkTwohandDrainLv = SkillTreeManager.Instance.GetSkillLevel("atk_twohand_drain");
                    // if (atkTwohandDrainLv > 0) __result += (int)SkillTreeConfig.AttackStatBonusValue * atkTwohandDrainLv;

                    // 스킬트리 보너스를 지능 스탯으로 합산
                    int skillTreeIntellectBonus = 0;

                    // 방어 전문가 에이트르 효과는 별도 구현 예정

                    // 속도 트리 6단계: 지능 +3 (MMO 독립 구현으로 변경 - SkillEffect.StatTree.cs 참조)
                    // int speed3Lv = SkillTreeManager.Instance.GetSkillLevel("speed_3");
                    // if (speed3Lv > 0) skillTreeIntellectBonus += 3 * speed3Lv;
                    
                    // 속도 트리 7단계: 모든 스텟 +2
                    int allMasterLv = SkillTreeManager.Instance.GetSkillLevel("all_master");
                    if (allMasterLv > 0) skillTreeIntellectBonus += 2 * allMasterLv;
                    
                    __result += skillTreeIntellectBonus;
                    
                    // 로그 제거: 불필요한 반복 출력
                }
                else if (paramName == "Vigour")
                {
                    // 방어 전문가: 체력 +5 → 활력 +5 (MMO 독립 구현으로 변경 - SkillEffect.StatTree.cs 참조)
                    // int defenseRootLv = SkillTreeManager.Instance.GetSkillLevel("defense_root");
                    // if (defenseRootLv > 0) __result += 5 * defenseRootLv;
                }
                else if (paramName == "Special")
                {
                    // 특수화 스탯: 특수화 +5
                    int atkSpecialLv = SkillTreeManager.Instance.GetSkillLevel("atk_special");
                    if (atkSpecialLv > 0) __result += (int)SkillTreeConfig.AttackSpecialStatValue * atkSpecialLv;
                    
                    // 모래시계: 쿨타임 시간 -1초 → 특수 +3 (간접 효과)
                    int agilityPeakLv = SkillTreeManager.Instance.GetSkillLevel("agility_peak");
                    if (agilityPeakLv > 0) __result += 3 * agilityPeakLv;
                }
                else if (paramName == "Agility")
                {
                    // 공격 증가: 민첩 효과 제거 (힘/지능으로 변경)
                    // int atkTwohandDrainLv = SkillTreeManager.Instance.GetSkillLevel("atk_twohand_drain");
                    // if (atkTwohandDrainLv > 0) __result += 2 * atkTwohandDrainLv;

                    // 속도 트리 6단계: 민첩 +3 (MMO 독립 구현으로 변경 - SkillEffect.StatTree.cs 참조)
                    // int speed1Lv = SkillTreeManager.Instance.GetSkillLevel("speed_1");
                    // if (speed1Lv > 0) __result += 3 * speed1Lv;
                    
                    // 속도 트리 7단계: 모든 스텟 +2
                    int allMasterLv = SkillTreeManager.Instance.GetSkillLevel("all_master");
                    if (allMasterLv > 0) __result += 2 * allMasterLv;
                    
                    // 속도 트리 3단계: 무기 숙련도 증가
                    int speedEx1Lv = SkillTreeManager.Instance.GetSkillLevel("speed_ex1");
                    int speedEx2Lv = SkillTreeManager.Instance.GetSkillLevel("speed_ex2");
                    if (speedEx1Lv > 0) __result += 3 * speedEx1Lv; // 근접무기 숙련 +3
                    if (speedEx2Lv > 0) __result += 3 * speedEx2Lv; // 지팡이 숙련 +3
                    
                    // 주의: 속도 전문가 이동속도는 Speed.cs의 Player.UpdateModifiers 패치에서 직접 처리됨
                }
                else if (paramName == "Body")
                {
                    // 속도 트리 6단계: 지구력 +3 → 체구 +3 (MMO 독립 구현으로 변경 - SkillEffect.StatTree.cs 참조)
                    // int speed2Lv = SkillTreeManager.Instance.GetSkillLevel("speed_2");
                    // if (speed2Lv > 0) __result += 3 * speed2Lv;

                    // 속도 트리 7단계: 모든 스텟 +2
                    int allMasterLv2 = SkillTreeManager.Instance.GetSkillLevel("all_master");
                    if (allMasterLv2 > 0) __result += 2 * allMasterLv2;

                    // defense_Step6_body: 요툰의 생명력 - 방어력 +10%
                    if (SkillTreeManager.Instance.GetSkillLevel("defense_Step6_body") > 0)
                    {
                        // MMO Body 스탯을 통해 물리/마법 방어력 증가
                        // Body 스탯은 MMO에서 방어력과 체력에 영향을 줌
                        // 10% 방어력 = +5 Body 스탯으로 계산
                        int bodyBonus = (int)(Defense_Config.BodyArmorBonusValue * 0.5f);
                        __result += bodyBonus;
                        Plugin.Log.LogDebug($"[요툰의 생명력] Body 보너스 적용: +{bodyBonus} (방어력 +{Defense_Config.BodyArmorBonusValue}%)");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리→mmo] getParameter 패치 오류: {ex.Message}");
            }
        }
    }

    // SEMan 리젠 패치들: mmo 방식 따라하기 - 제거 (mmo SEMan 패치에서 처리)
    // 스킬트리 보너스는 mmo getParameter 패치에서 스탯으로 합산되어 mmo 리젠 계산에 반영됨
    

    // Character.ApplyDamage 패치: 방어력 적용 (실제 메서드 패치)
    [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
    public static class SkillTree_Character_ApplyDamage_DefenseBonus_Patch
    {
        [HarmonyPriority(Priority.VeryLow)]  // 모든 패치 이후에 실행
        public static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (!__instance.IsPlayer()) return;
                if (hit.GetAttacker() == __instance) return;
                
                // === 물리 방어력 ===
                float physicArmorBonus = 0f;
                
                // defense_root: 방어 +2 (고정값)
                int defenseRootLv = SkillTreeManager.Instance.GetSkillLevel("defense_root");
                if (defenseRootLv > 0) physicArmorBonus += 2f * defenseRootLv;
                
                // Old defense skill effects removed - 새로운 Step-based defense system으로 대체 예정
                
                if (physicArmorBonus > 0)
                {
                    float reduction = 1f - physicArmorBonus / 100f;
                    hit.m_damage.m_blunt *= reduction;
                    hit.m_damage.m_slash *= reduction;
                    hit.m_damage.m_pierce *= reduction;
                    hit.m_damage.m_chop *= reduction;
                    hit.m_damage.m_pickaxe *= reduction;
                    
                    // 로그 제거: 불필요한 반복 출력
                }
                
                // === 마법 방어력 ===
                float magicArmorBonus = 0f;
                
                // mental_defense: 마법방어 +8%
                int mentalDefLv = SkillTreeManager.Instance.GetSkillLevel("mental_defense");
                if (mentalDefLv > 0) magicArmorBonus += 8f * mentalDefLv;
                
                // mind_shield: 마법방어 +5%
                int mindShieldLv = SkillTreeManager.Instance.GetSkillLevel("mind_shield");
                if (mindShieldLv > 0) magicArmorBonus += 5f * mindShieldLv;
                
                if (magicArmorBonus > 0)
                {
                    float magicReduction = 1f - magicArmorBonus / 100f;
                    hit.m_damage.m_fire *= magicReduction;
                    hit.m_damage.m_frost *= magicReduction;
                    hit.m_damage.m_lightning *= magicReduction;
                    hit.m_damage.m_poison *= magicReduction;
                    hit.m_damage.m_spirit *= magicReduction;
                    
                    // 로그 제거: 불필요한 반복 출력
                }
                
                // === 수호자의 진심 반사 효과 ===
                var player = __instance as Player;
                if (player != null)
                {
                    // 버프 상태 확인 (디버깅)
                    bool isBuffActive = IsGuardianHeartActive(player);

                    // 피격 시 항상 버프 상태 로그 출력
                    var attacker = hit.GetAttacker();
                    // 새로운 수호자의 진심 버프가 활성화되어 있는지 확인
                    if (isBuffActive)
                    {
                        // 방패로 막은 공격에 대해서만 반사 적용
                        bool isBlocking = player.IsBlocking();
                        bool hasShield = HasShield(player);

                        if (isBlocking && hasShield)
                        {
                            if (attacker != null && attacker != player)
                            {
                                // Valheim 기본 vfx_blocked 사용 (중복 VFX 방지)

                                // Config에서 반사 데미지 비율 가져오기
                                ApplyGuardianHeartReflectDamage(player, attacker, hit);
                            }
                        }
                    }
                    // 기존 guardianSoulReflectionEndTime 시스템 (하위 호환성)
                    else if (Time.time < guardianSoulReflectionEndTime)
                    {
                        // 방패로 막은 공격에 대해서만 반사 적용
                        if (player.IsBlocking() && HasShield(player))
                        {
                            // attacker 변수는 이미 Line 859에서 선언됨
                            if (attacker != null && attacker != player)
                            {
                                // 받은 피해의 Config% 를 반사 데미지로 계산
                                float reflectPercent = Mace_Config.GuardianHeartReflectPercentValue / 100f;
                                float reflectionDamage = hit.m_damage.GetTotalDamage() * reflectPercent;

                                // 반사 데미지 적용 (Tanker 어그로 코드 참고하여 완전한 HitData 구성)
                                HitData reflectionHit = new HitData();
                                reflectionHit.m_damage.m_blunt = reflectionDamage; // 블런트(둔기) 데미지로 반사
                                reflectionHit.m_attacker = player.GetZDOID();
                                reflectionHit.m_point = attacker.GetCenterPoint(); // 정확한 충돌 지점
                                reflectionHit.m_dir = (attacker.transform.position - player.transform.position).normalized;
                                reflectionHit.m_skill = Skills.SkillType.Clubs; // 둔기 스킬
                                reflectionHit.m_pushForce = 0f; // 밀침 없음
                                reflectionHit.m_blockable = false; // 막을 수 없음
                                reflectionHit.m_dodgeable = false; // 회피 불가
                                reflectionHit.m_ranged = false; // 근접 공격
                                reflectionHit.m_staggerMultiplier = 0f; // 스태거 없음
                                reflectionHit.m_toolTier = 0; // 무기 티어 없음

                                // 몬스터 체력 변화 확인
                                float beforeHP = attacker.GetHealth();
                                attacker.Damage(reflectionHit);
                                float afterHP = attacker.GetHealth();

                                // 반사 효과 표시
                                SkillEffect.DrawFloatingText(player, $"🛡️ 반사 데미지 {reflectionDamage:F0}!", Color.yellow);
                                Plugin.Log.LogInfo($"[구버전 반사] {attacker.name} 체력: {beforeHP:F1} → {afterHP:F1} (피해: {beforeHP - afterHP:F1})");
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리→발하임] ApplyDamage 방어 패치 오류: {ex.Message}");
            }
        }
        
        // === 헬퍼 함수 ===
        private static bool HasShield(Player player)
        {
            var inventory = player.GetInventory();
            if (inventory == null) return false;

            var leftItem = inventory.GetEquippedItems().FirstOrDefault(item =>
                item.m_shared?.m_itemType == ItemDrop.ItemData.ItemType.Shield);
            return leftItem != null;
        }
    }
    
    // Character.GetBodyArmor 패치: 방어구 방어력에 스킬트리 보너스 합산
    [HarmonyPatch(typeof(Character), nameof(Character.GetBodyArmor))]
    public static class SkillTree_Character_GetBodyArmor_Patch
    {
        [HarmonyPriority(Priority.Low)]  // mmo 이후에 실행
        public static void Postfix(Character __instance, ref float __result)
        {
            try
            {
                if (!__instance.IsPlayer()) return;
                
                float armorBonus = 0f;
                
                // 방어 전문가 방어력 효과는 별도 구현 예정
                
                __result += armorBonus;
                
                // 로그 제거: 불필요한 반복 출력
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리→발하임] GetBodyArmor 패치 오류: {ex.Message}");
            }
        }
    }

    // 트롤의 재생력: 2초마다 체력 +5 회복
    // Player.Update에서 타이머 기반으로 체력 회복 처리
    [HarmonyPatch(typeof(Player), "Update")]
    public static class SkillTree_Player_Update_TrollRegen_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Player __instance)
        {
            try
            {
                if (__instance != Player.m_localPlayer) return;

                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                // defense_Step5_heal: 트롤의 재생력
                if (manager.GetSkillLevel("defense_Step5_heal") > 0)
                {
                    // 타이머 초기화
                    if (!trollRegenTimers.ContainsKey(__instance))
                    {
                        trollRegenTimers[__instance] = 0f;
                    }

                    // 타이머 증가
                    trollRegenTimers[__instance] += Time.deltaTime;

                    // 설정된 간격마다 체력 회복
                    float interval = Defense_Config.TrollRegenIntervalValue;
                    if (trollRegenTimers[__instance] >= interval)
                    {
                        float healAmount = Defense_Config.TrollRegenBonusValue;
                        __instance.Heal(healAmount, true);

                        trollRegenTimers[__instance] = 0f;

                        Plugin.Log.LogDebug($"[트롤의 재생력] {interval}초마다 체력 +{healAmount} 회복");
                    }
                }
                else
                {
                    // 스킬이 없으면 타이머 제거
                    if (trollRegenTimers.ContainsKey(__instance))
                    {
                        trollRegenTimers.Remove(__instance);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[트롤의 재생력] 패치 오류: {ex.Message}");
            }
        }
    }

    // ===== 생산 스킬 패치 =====
    
    // 벌목/채광 효율 증가 패치 (mmo 방식 참고)
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    public static class SkillTree_ItemData_GetDamage_Production_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
        {
            if (__instance == null) return;
            
            // 벌목 도구 효율 증가
            if (__instance.m_shared.m_skillType == Skills.SkillType.Axes || 
                __instance.m_shared.m_skillType == Skills.SkillType.WoodCutting)
            {
                float woodcuttingBonus = GetWoodcuttingBonus();
                if (woodcuttingBonus > 0)
                {
                    float multiplier = 1f + (woodcuttingBonus / 100f);
                    __result.m_chop *= multiplier;
                    
                    // 텍스트 표시 비활성화 (GetDamage는 너무 자주 호출됨)
                    // 실제 벌목 시에만 ProductionEffects.cs에서 표시됨
                }
            }
            
            // 채광 도구 효율 증가
            if (__instance.m_shared.m_skillType == Skills.SkillType.Pickaxes)
            {
                float miningBonus = GetMiningBonus();
                if (miningBonus > 0)
                {
                    float multiplier = 1f + (miningBonus / 100f);
                    __result.m_pickaxe *= multiplier;
                    
                    // 텍스트 표시 비활성화 (GetDamage는 너무 자주 호출됨)
                    // 실제 채광 시에만 ProductionEffects.cs에서 표시됨
                }
            }
        }
        
        private static float GetWoodcuttingBonus()
        {
            float bonus = 0f;
            
            if (SkillEffect.HasSkill("novice_worker")) bonus += 20f;
            if (SkillEffect.HasSkill("woodcutting_lv2")) bonus += 30f;
            if (SkillEffect.HasSkill("woodcutting_lv3")) bonus += 40f;
            if (SkillEffect.HasSkill("woodcutting_lv4")) bonus += 50f;
            if (SkillEffect.HasSkill("labor_specialist")) bonus += 100f;
            
            return bonus;
        }
        
        private static float GetMiningBonus()
        {
            float bonus = 0f;
            
            if (SkillEffect.HasSkill("mining_lv2")) bonus += 30f;
            if (SkillEffect.HasSkill("mining_lv3")) bonus += 40f;
            if (SkillEffect.HasSkill("mining_lv4")) bonus += 50f;
            if (SkillEffect.HasSkill("labor_specialist")) bonus += 100f;
            
            return bonus;
        }
    }
    
    // 건물 내구도 증가 패치 (mmo 방식 참고)
    [HarmonyPatch(typeof(WearNTear), nameof(WearNTear.OnPlaced))]
    public static class SkillTree_WearNTear_OnPlaced_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Prefix(ref WearNTear __instance)
        {
            if (Player.m_localPlayer == null) return;
            
            float buildingBonus = GetBuildingBonus();
            if (buildingBonus > 0)
            {
                float multiplier = 1f + (buildingBonus / 100f);
                __instance.m_health *= multiplier;
                
                // 건설 시 텍스트 표시 (MMO 방식 DamageText)
                if (UnityEngine.Random.Range(0f, 1f) < 0.25f) // 빈도 약간 증가
                {
                    SkillEffect.DrawFloatingText(Player.m_localPlayer, 
                        $"🏠 건축 내구도 +{buildingBonus}%", 
                        new Color(0.6f, 0.4f, 0.2f, 1f)); // 나무/돌 비슷한 갈색
                }
            }
        }
        
        private static float GetBuildingBonus()
        {
            float bonus = 0f;
            
            if (SkillEffect.HasSkill("building_lv2")) bonus += 30f;
            if (SkillEffect.HasSkill("building_lv3")) bonus += 40f;
            if (SkillEffect.HasSkill("building_lv4")) bonus += 50f;
            
            return bonus;
        }
    }
    
    /// <summary>
    /// Player 생명주기 이벤트 처리 - MMO getParameter 방식으로 자동 적용
    /// </summary>
    [HarmonyPatch(typeof(Player), "Awake")]
    public static class Player_Awake_SpeedEffect_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Player __instance)
        {
            if (__instance == Player.m_localPlayer)
            {
                // MMO getParameter 패치를 통해 자동 적용되므로 별도 처리 불필요
                Plugin.Log.LogInfo($"[속도 전문가] 플레이어 {__instance.GetPlayerName()} MMO 연동 준비 완료");
            }
        }
    }
    
    /// <summary>
    /// Player 사망/로그아웃 시 메모리 정리 - MMO 방식으로 자동 관리됨
    /// </summary>
    [HarmonyPatch(typeof(Player), "OnDestroy")]
    public static class Player_OnDestroy_SpeedEffect_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Prefix(Player __instance)
        {
            if (__instance == Player.m_localPlayer)
            {
                // MMO getParameter 방식에서는 별도 상태 정리 불필요
                Plugin.Log.LogDebug($"[속도 전문가] 플레이어 {__instance.GetPlayerName()} 로그아웃");
            }
        }
    }

    /// <summary>
    /// 속도 전문가 스킬들의 MMO 연동 - Agility 스탯 증가 (리접속 시에도 지속)
    /// </summary>
    [HarmonyPatch]
    public static class SpeedSkills_MMO_Stats_Patch
    {
        // MMO 시스템 사용 가능 여부 확인
        [HarmonyPrepare]
        static bool Prepare()
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null)
            {
                Plugin.Log.LogInfo("[MMO 패치] EpicMMOSystem 미설치 - 속도 스킬 MMO 연동 패치 건너뜀 (정상)");
                return false;
            }

            var method = levelSystemType.GetMethod("getParameter");
            if (method == null)
            {
                Plugin.Log.LogWarning("[MMO 패치] getParameter 메서드를 찾을 수 없음 - 속도 스킬 패치 건너뜀");
                return false;
            }

            Plugin.Log.LogDebug("[MMO 패치] EpicMMOSystem 감지 - 속도 스킬 MMO 연동 패치 활성화");
            return true;
        }
        
        // EpicMMOSystem.LevelSystem.getParameter 메서드 패치
        [HarmonyTargetMethod]
        static System.Reflection.MethodBase TargetMethod()
        {
            // Prepare에서 이미 확인했으므로 바로 반환
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            return levelSystemType?.GetMethod("getParameter");
        }
        
        [HarmonyPriority(Priority.High)]
        public static void Postfix(object parameter, ref int __result)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                
                string paramType = parameter?.ToString();
                int bonus = 0;
                
                if (paramType == "Agility")
                {
                    // 속도 전문가 루트 - 기본 이동속도 보너스 (MMO Agility 연동)
                    if (SkillEffect.HasSkill("speed_root"))
                    {
                        bonus += (int)(SkillTreeConfig.SpeedRootMoveSpeedValue * 2f); // 5% * 2 = +10 Agility
                        Plugin.Log.LogDebug($"[속도 전문가] speed_root - MMO Agility 증가: +10 (이동속도 +5%)");
                    }
                    
                    // 민첩함의 기초 - 추가 이동속도 보너스
                    if (SkillEffect.HasSkill("speed_base"))
                    {
                        bonus += (int)(SkillTreeConfig.SpeedBaseMoveSpeedValue * 2f); // 3% * 2 = +6 Agility
                        Plugin.Log.LogDebug($"[속도 전문가] speed_base - MMO Agility 증가: +6 (이동속도 +3%)");
                    }
                    
                    // 속도 마스터 - 추가 보너스 (있는 경우)
                    if (SkillEffect.HasSkill("speed_master"))
                    {
                        bonus += 5; // 고정 보너스
                        Plugin.Log.LogDebug($"[속도 전문가] speed_master - MMO Agility 증가: +5");
                    }
                    
                    // 점프 숙련자 - 민첩성 보너스
                    if (SkillEffect.HasSkill("agility_peak"))
                    {
                        bonus += 8; // 점프 능력 향상으로 인한 민첩성 증가
                        Plugin.Log.LogDebug($"[속도 전문가] agility_peak - MMO Agility 증가: +8");
                    }
                    
                    if (bonus > 0)
                    {
                        __result += bonus;
                        Plugin.Log.LogDebug($"[속도 전문가] 총 MMO Agility 증가: +{bonus}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[속도 전문가] MMO 스탯 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 조건부 속도 보너스는 StatusEffect에서 통합 처리 (성능 최적화)
    /// MMO getParameter 방식과 Valheim 네이티브 StatusEffect 병행 사용
    /// </summary>

    /// <summary>
    /// GetDamage 패치용 공통 Helper 함수들
    /// Rule 11 준수: 물리 4종 (pierce, blunt, slash, chop) + 속성 5종 (fire, frost, lightning, poison, spirit)
    /// </summary>
    public static class GetDamageHelper
    {
        /// <summary>
        /// 물리 데미지에 비율 보너스 적용 (전투용 4종: pierce, blunt, slash, chop)
        /// </summary>
        public static void ApplyPhysicalDamageBonus(ref HitData.DamageTypes damage, float bonusPercent)
        {
            if (bonusPercent <= 0) return;
            float multiplier = 1f + (bonusPercent / 100f);

            if (damage.m_pierce > 0) damage.m_pierce *= multiplier;
            if (damage.m_blunt > 0) damage.m_blunt *= multiplier;
            if (damage.m_slash > 0) damage.m_slash *= multiplier;
            if (damage.m_chop > 0) damage.m_chop *= multiplier;
        }

        /// <summary>
        /// 속성 데미지에 비율 보너스 적용 (5종 모두)
        /// </summary>
        public static void ApplyElementalDamageBonus(ref HitData.DamageTypes damage, float bonusPercent)
        {
            if (bonusPercent <= 0) return;
            float multiplier = 1f + (bonusPercent / 100f);

            if (damage.m_fire > 0) damage.m_fire *= multiplier;
            if (damage.m_frost > 0) damage.m_frost *= multiplier;
            if (damage.m_lightning > 0) damage.m_lightning *= multiplier;
            if (damage.m_poison > 0) damage.m_poison *= multiplier;
            if (damage.m_spirit > 0) damage.m_spirit *= multiplier;
        }

        /// <summary>
        /// 특정 데미지 타입에만 고정값 추가
        /// </summary>
        public static void AddFixedDamage(ref HitData.DamageTypes damage, float value, params string[] types)
        {
            if (value <= 0) return;

            foreach (var type in types)
            {
                switch (type.ToLower())
                {
                    case "slash": if (damage.m_slash > 0) damage.m_slash += value; break;
                    case "pierce": if (damage.m_pierce > 0) damage.m_pierce += value; break;
                    case "blunt": if (damage.m_blunt > 0) damage.m_blunt += value; break;
                    case "chop": if (damage.m_chop > 0) damage.m_chop += value; break;
                    case "fire": if (damage.m_fire > 0) damage.m_fire += value; break;
                    case "frost": if (damage.m_frost > 0) damage.m_frost += value; break;
                    case "lightning": if (damage.m_lightning > 0) damage.m_lightning += value; break;
                    case "poison": if (damage.m_poison > 0) damage.m_poison += value; break;
                    case "spirit": if (damage.m_spirit > 0) damage.m_spirit += value; break;
                }
            }
        }

        /// <summary>
        /// 단검 전용 데미지 보너스 (slash + pierce)
        /// </summary>
        public static void ApplyKnifeDamageBonus(ref HitData.DamageTypes damage, float bonusPercent)
        {
            if (bonusPercent <= 0) return;
            float multiplier = 1f + (bonusPercent / 100f);

            if (damage.m_slash > 0) damage.m_slash *= multiplier;
            if (damage.m_pierce > 0) damage.m_pierce *= multiplier;
        }
    }
}
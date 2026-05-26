using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;
using CaptainSkillTree.SkillTree;
using CaptainSkillTree.SkillTree.CriticalSystem;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Audio;
using CaptainSkillTree.MMO_System;

namespace CaptainSkillTree
{
    /// <summary>
    /// Plugin ?대옒?ㅼ쓽 Harmony ?⑥튂 遺遺?
    /// </summary>
    public partial class Plugin
    {
        // 移섎챸? ?쒖뒪???⑥튂 (紐⑤뱺 臾닿린 吏??
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        [HarmonyPriority(Priority.Normal)]
        public static class WeaponCriticalSystemPatch
        {
            public static void Prefix(Character __instance, ref bool showDamageText, ref HitData hit)
            {
                // 怨듦꺽?먭? ?뚮젅?댁뼱?몄? ?뺤씤 (?쇨꺽???꾨떂!)
                // 돌진 연속 베기 무적 (시전 중 + 종료 후 1초)
                if (__instance is Player victimPlayer && SkillTree.Sword_Skill.IsRushSlashInvincible(victimPlayer))
                {
                    hit.m_damage = new HitData.DamageTypes();
                    return;
                }

                var attacker = hit.GetAttacker();
                if (attacker == null || !(attacker is Player)) return;

                var player = attacker as Player;

                // === 李??ъ갹 ?꾩슜 泥섎━ (weapon null ?댁쟾 - ?ъ갹 ??weapon ?놁쓣 ???덉쓬) ===
                // === 투창 스킬 처리 (창 장착 + 2차 공격 플래그 동시 확인) ===
                bool isSpearEquipped = player.GetCurrentWeapon()?.m_shared?.m_skillType == Skills.SkillType.Spears;
                bool isSpearThrow = isSpearEquipped && SkillEffect.IsRecentSpearSecondaryAttack(player);

                // === 연공창 액티브 스킬 (H키) - 투사체 태그 기반으로 콤보 여부 판단 ===
                if (SpearComboThrow_ProjectileHit_Patch.currentHitIsCombo)
                {
                    SkillEffect.ConsumeSpearSecondaryAttack(player);
                    float damageBonus = SkillTreeConfig.SpearStep6ComboDamageValue;
                    float multiplier = 1f + (damageBonus / 100f);

                    // 臾쇰━ ?곕?吏 4醫?(Rule 11 以??
                    hit.m_damage.m_pierce *= multiplier;
                    hit.m_damage.m_blunt *= multiplier;
                    hit.m_damage.m_slash *= multiplier;
                    hit.m_damage.m_chop *= multiplier;

                    Log.LogInfo($"[창 연공] 콤보 강화 투창 데미지 +{damageBonus}% 적용!");

                    // 紐ъ뒪??留욎븯????confetti ?④낵
                    if (__instance != null && !__instance.IsPlayer())
                    {
                        Vector3 monsterPos = __instance.transform.position + Vector3.up * 1f;
                        SimpleVFX.Play("confetti_directional_multicolor", monsterPos, 2f);
                        Log.LogDebug("[창 연공] confetti 이펙트 시작");
                    }

                    // 사용 횟수 차감은 던지는 시점(ProjectileSetup)에서 처리됨
                    return;
                }

                // === 李??ъ갹 ?꾨Ц媛 ?⑥떆釉?- 2李?怨듦꺽(?ъ갹)?먮쭔 ?곸슜 ===
                if (isSpearThrow && SkillEffect.HasSkill("spear_Step1_throw") && SkillEffect.CanUseSpearThrowPassive(player))
                {
                    SkillEffect.ConsumeSpearSecondaryAttack(player);
                    float damageBonus = SkillTreeConfig.SpearStep2ThrowDamageValue;
                    float multiplier = 1f + (damageBonus / 100f);

                    // 臾쇰━ ?곕?吏 4醫?(Rule 11 以??
                    hit.m_damage.m_pierce *= multiplier;
                    hit.m_damage.m_blunt *= multiplier;
                    hit.m_damage.m_slash *= multiplier;
                    hit.m_damage.m_chop *= multiplier;

                    Log.LogInfo($"[투창 패시브] 기본 투창 보너스 +{damageBonus}% 적용!");
                    SkillEffect.ShowSkillEffectText(player, L.Get("spear_throw_passive_activated", $"{damageBonus:F0}"), new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Combat);

                    SkillEffect.SetSpearThrowPassiveCooldown(player);
                    // return ?놁쓬 - ?щ━?곗뺄??諛쒕룞 媛??
                }

                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return;

                var weaponType = weapon.m_shared.m_skillType;

                // === ?ㅻ뱶???щ━?곗뺄 (??+ bow_Step2_focus 蹂댁쑀 + 癒몃━ ?곸쨷 ??100%) ===
                if (weaponType == Skills.SkillType.Bows
                    && SkillEffect.HasSkill("bow_Step2_focus")
                    && Critical.IsHeadshot(__instance, hit.m_point))
                {
                    float critMultiplier = CriticalDamage.CalculateCritDamageMultiplier(player, weaponType);
                    CriticalDamage.ApplyCriticalDamage(player, ref hit, critMultiplier, weaponType);
                    SkillEffect.ShowSkillEffectText(player, L.Get("bow_headshot_text") + "!",
                        new Color(1f, 0.3f, 0.1f), SkillEffect.SkillEffectTextType.Combat);
                    CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("fx_crit", "", hit.m_point);
                    showDamageText = false;
                    Log.LogInfo("[헤드샷] 헤드샷 발생 시 100% 치명타 발동!");
                }
                else
                {
                    // === 湲곗〈 移섎챸? ?쒖뒪??(紐⑤뱢?? ===
                    float critChance = Critical.CalculateCritChance(player, weaponType);

                    if (Critical.RollCritical(critChance))
                    {
                        float critMultiplier = CriticalDamage.CalculateCritDamageMultiplier(player, weaponType);
                        CriticalDamage.ApplyCriticalDamage(player, ref hit, critMultiplier, weaponType);
                        // ?④?/留⑥＜癒?移섎챸? VFX
                        if (weaponType == Skills.SkillType.Knives || weaponType == Skills.SkillType.Unarmed)
                        {
                            CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("fx_crit", "", hit.m_point);
                        }
                        showDamageText = false;
                    }
                }

                // === knife_stagger ?쒓굅??- ?ㅼ젣 ?ㅽ궗 ?몃━??議댁옱?섏? ?딆쓬 ===
                // ?붿궡??knife_step8_assassination)??鍮꾪?嫄곕┝ ?④낵瑜?泥섎━??
            }
        }

        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyAttackStaminaUsage))]
        public static class KnifeSkillTreeStaminaPatch
        {
            public static void Postfix(SEMan __instance, ref float staminaUse)
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                var weapon = player.GetCurrentWeapon();
                if (weapon == null || weapon.m_shared.m_skillType != Skills.SkillType.Knives) return;
                float reduction = SkillEffect.GetKnifeStaminaReduction(0f);
                staminaUse *= (1f - reduction / 100f);
            }
        }

        // ============================================================================
        // ??KnifeAttackSpeedAnimatorPatch - 鍮꾪솢?깊솕??(v0.1.225)
        // ============================================================================
        // 臾몄젣: animator.speed 吏곸젒 議곗옉??AnimationSpeedManager? 異⑸룎?섏뿬
        //       紐⑤뱺 臾닿린??湲곕낯 怨듦꺽 ?띾룄媛 ?먮젮吏??踰꾧렇 諛쒖깮
        //
        // ?닿껐: 紐⑤뱺 怨듦꺽 ?띾룄??AnimationSpeedManager?먯꽌 ?듯빀 泥섎━
        //       - Game.Awake??AttackSpeedHandler_Game_Awake_Patch 李몄“
        //       - SkillEffect.GetTotalAttackSpeedBonus()?먯꽌 ?④? ?ы븿 紐⑤뱺 臾닿린 泥섎━
        //
        // 李멸퀬: md/Attack_Speed_bug.md 臾몄꽌 李몄“
        // ============================================================================
        // [HarmonyPatch(typeof(CharacterAnimEvent), nameof(CharacterAnimEvent.CustomFixedUpdate))]
        // public static class KnifeAttackSpeedAnimatorPatch { ... }
        // ============================================================================

        // ?ㅽ깭誘몃굹 ?ъ깮 ?꾩쟻
        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyStaminaRegen))]
        public static class KnifeSkillTreeStaminaRegenPatch
        {
            public static void Postfix(SEMan __instance, ref float staminaMultiplier)
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                staminaMultiplier += SkillEffect.GetStaminaRegen(0f) / 100f;
            }
        }

        // ?щ━湲??ㅽ깭誘몃굹 媛먯냼 ?꾩쟻
        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyRunStaminaDrain))]
        public static class KnifeSkillTreeRunStaminaPatch
        {
            public static void Postfix(ref float drain)
            {
                float reduction = SkillEffect.GetStaminaReduction(0f);
                drain *= (1f - reduction / 100f);
            }
        }

        // ?먰봽 ?ㅽ깭誘몃굹 媛먯냼 ?꾩쟻
        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyJumpStaminaUsage))]
        public static class KnifeSkillTreeJumpStaminaPatch
        {
            public static void Postfix(ref float staminaUse)
            {
                // 湲곗〈 移??ㅽ궗 ?먰봽 ?ㅽ깭誘몃굹 媛먯냼
                float knifeReduction = SkillEffect.GetStaminaReduction(0f);
                staminaUse *= (1f - knifeReduction / 100f);

                // ?먰봽 ?숇젴???ㅽ궗 ?먰봽 ?ㅽ깭誘몃굹 媛먯냼
                float jumpExpertReduction = SkillEffect.GetJumpStaminaReduction();
                staminaUse *= (1f - jumpExpertReduction / 100f);
            }
        }

        // 臾쇰━/留덈쾿 諛⑹뼱 ?꾩쟻 (ApplyDamage?먯꽌 ?숈떆 ?곸슜)
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        [HarmonyPriority(Priority.High)]
        public static class KnifeSkillTreeArmorPatch
        {
            public static void Prefix(Character __instance, HitData hit)
            {
                if (!__instance.IsPlayer()) return;
                // 臾쇰━ 諛⑹뼱
                float addPhys = SkillEffect.GetPhysicArmor(0f);
                var valuePhys = 1 - addPhys / 100f;
                hit.m_damage.m_blunt *= valuePhys;
                hit.m_damage.m_slash *= valuePhys;
                hit.m_damage.m_pierce *= valuePhys;
                hit.m_damage.m_chop *= valuePhys;
                hit.m_damage.m_pickaxe *= valuePhys;
                // 留덈쾿 諛⑹뼱
                float addMagic = SkillEffect.GetMagicArmor(0f);
                var valueMagic = 1 - addMagic / 100f;
                hit.m_damage.m_fire *= valueMagic;
                hit.m_damage.m_frost *= valueMagic;
                hit.m_damage.m_lightning *= valueMagic;
                hit.m_damage.m_poison *= valueMagic;
                hit.m_damage.m_spirit *= valueMagic;
            }
        }

        [HarmonyPatch(typeof(Humanoid), "BlockAttack")]
        public static class SwordSkillTreeParryPatch
        {
            public static void Postfix(Character __instance, bool __result, HitData hit, Character attacker)
            {
                if (__result && __instance is Player player && player.IsPlayer())
                {
                    // ?⑤쭅 ?뚭꺽? Stagger ?⑥튂?먯꽌 泥섎━ (?꾨옒 ParryRush_Stagger_Patch)

                    var currentWeapon = player.GetCurrentWeapon();
                    if (currentWeapon == null || currentWeapon.m_shared.m_skillType != Skills.SkillType.Swords) return;

                    var manager = SkillTreeManager.Instance;
                    var seman = player.GetSEMan();

                    if (manager.GetSkillLevel("sword_counter") > 0)
                    {
                        var effect = ScriptableObject.CreateInstance<SE_SwordCounter>();
                        effect.m_ttl = 5f;
                        seman.AddStatusEffect(effect, true);
                    }

                    if (manager.GetSkillLevel("sword_riposte") > 0)
                    {
                        var effect = ScriptableObject.CreateInstance<SE_SwordRiposte>();
                        effect.m_ttl = 5f;
                        seman.AddStatusEffect(effect, true);
                    }
                }
            }
        }

        /// <summary>
        /// ?⑤쭅 ?뚭꺽 ?꾩슜 ?⑥튂: 諛쒗뿤?꾩씠 ?⑤쭅 ?깃났 ??attacker.Stagger()瑜??몄텧??
        /// ?대? 媛먯??섏뿬 ?⑤쭅 ?뚭꺽??諛쒕룞
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.Stagger))]
        public static class ParryRush_Stagger_Patch
        {
            public static void Postfix(Character __instance)
            {
                try
                {
                    // __instance = ?ㅽ깭嫄??뱁븯??罹먮┃??(怨듦꺽??紐ъ뒪??
                    if (__instance == null || __instance.IsPlayer()) return;

                    var player = Player.m_localPlayer;
                    if (player == null || player.IsDead()) return;
                    if (!Sword_Skill.IsParryRushActive(player)) return;

                    // ?뚮젅?댁뼱媛 留됯린 以?+ 紐ъ뒪?곌? ?ㅽ깭嫄곕맖 = ?⑤쭅 ?깃났
                    if (player.IsBlocking())
                    {
                        Sword_Skill.OnParryRushTrigger(player, __instance);
                    }
                }
                catch (System.Exception)
                {
                }
            }
        }


        // 회오리베기 시전 중 구르기 상태 무시
        [HarmonyPatch(typeof(Player), nameof(Player.InDodge))]
        public static class WhirlwindInDodge_Patch
        {
            public static bool Prefix(Player __instance, ref bool __result)
            {
                if (Sword_Skill.IsWhirlwindCharging(__instance))
                { __result = false; return false; }
                return true;
            }
        }

        // 회오리베기 시전 중 스태거 상태 무시
        [HarmonyPatch(typeof(Character), nameof(Character.IsStaggering))]
        public static class WhirlwindIsStaggering_Patch
        {
            public static bool Prefix(Character __instance, ref bool __result)
            {
                if (__instance is Player p && Sword_Skill.IsWhirlwindCharging(p))
                { __result = false; return false; }
                return true;
            }
        }

        // 회오리베기 시전 중 스태거 애니메이션 차단 (기존 ParryRush_Stagger_Patch와 공존)
        [HarmonyPatch(typeof(Character), nameof(Character.Stagger))]
        public static class WhirlwindStagger_Patch
        {
            public static bool Prefix(Character __instance)
            {
                if (__instance is Player p && Sword_Skill.IsWhirlwindCharging(p))
                    return false;
                return true;
            }
        }

        // 회오리베기 시전 중 마이너액션(점프착지 애니메이션) 무시
        [HarmonyPatch(typeof(Player), nameof(Player.InMinorAction))]
        public static class WhirlwindInMinorAction_Patch
        {
            public static bool Prefix(Player __instance, ref bool __result)
            {
                if (Sword_Skill.IsWhirlwindCharging(__instance))
                { __result = false; return false; }
                return true;
            }
        }
        public class SE_SwordCounter : StatusEffect
        {
            public SE_SwordCounter()
            {
                m_name = "移쇰궇 ?섏튂湲?";
                m_tooltip = "?ㅼ쓬 怨듦꺽???쇳빐?됱씠 20% 利앷??⑸땲??";
                m_icon = null;
                m_ttl = 5f;
            }

            public override void ModifyAttack(Skills.SkillType skill, ref HitData hitData)
            {
                if (skill == Skills.SkillType.Swords)
                {
                    hitData.m_damage.Modify(1.2f);
                    m_character.GetSEMan().RemoveStatusEffect(this, true);
                }
            }
        }

        public class SE_SwordRiposte : StatusEffect
        {
            public SE_SwordRiposte()
            {
                m_name = "移쇰궇 ?섏튂湲?";
                m_tooltip = "?ㅼ쓬 怨듦꺽???쇳빐?됱씠 20% 利앷??⑸땲??";
                m_icon = null;
            }
        }

        // SE_SwordRiposte???ㅼ젣 ?④낵瑜??곸슜?섍린 ?꾪븳 蹂꾨룄???⑥튂
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        public static class SwordRiposteDamagePatch
        {
            private static readonly int SwordCounterHash = Animator.StringToHash("移쇰궇 ?섏튂湲?");
            private static readonly int SwordRiposteHash = Animator.StringToHash("諛섍꺽 ?먯꽭");

            private static void Prefix(Character __instance, HitData hit)
            {
                try
                {
                    if (hit == null) return;
                    if (hit.GetAttacker() is Player player && player != null)
                    {
                        var seman = player.GetSEMan();
                        if (seman != null && seman.HaveStatusEffect(SwordRiposteHash))
                        {
                            hit.m_damage.m_blunt *= 1.2f;
                            hit.m_damage.m_slash *= 1.2f;
                            hit.m_damage.m_pierce *= 1.2f;
                            seman.RemoveStatusEffect(SwordRiposteHash);
                        }
                    }
                }
                catch (System.Exception)
                {
                }
            }
        }

        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Hide))]
        public static class InventoryHidePatch
        {
            public static void Postfix()
            {
                try
                {
                    // ?ㅽ궗?몃━ UI ?リ린
                    if (skillTreeUI != null && skillTreeUI.panel != null && skillTreeUI.panel.activeSelf)
                    {
                        skillTreeUI.panel.SetActive(false);

                        // ?몃깽?좊━ ?レ쓣 ?뚮룄 BGM ?쇱떆?뺤? 諛?諛쒗뿤???뚯븙 蹂듭썝
                        if (SkillTreeBGMManager.Instance != null)
                        {
                            SkillTreeBGMManager.Instance.PauseSkillTreeBGM();
                        }
                    }

                    // ?몃깽?좊━ ?レ쓣 ???꾩씠肄섎룄 ?④?
                    if (skillTreeIconObj != null)
                    {
                        skillTreeIconObj.SetActive(false);
                    }
                }
                catch (Exception ex)
                {
                    Log.LogError($"[?ㅽ궗?몃━] InventoryHidePatch ?ㅻ쪟: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// ?ㅽ궗?몃━ ?꾩씠肄??쒖떆 諛??꾩튂 議곗젙
        /// ?몃깽?좊━ ???뚮쭔 ?꾩씠肄??쒖떆
        /// EpicMMO媛 ?놁쓣 ??罹먮┃??癒몃━ ?꾩뿉 諛곗튂
        /// </summary>
        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Show))]
        public static class InventoryShowIconPositionPatch
        {
            private static bool _iconPositionAdjusted = false;

            public static void Postfix(InventoryGui __instance)
            {
                try
                {
                    // ?꾩씠肄섏씠 ?놁쑝硫??ㅽ궢
                    if (skillTreeIconObj == null) return;

                    // ?몃깽?좊━ ?????꾩씠肄??쒖떆
                    skillTreeIconObj.SetActive(true);

                    // EpicMMO媛 ?덉쑝硫?湲곕낯 ?꾩튂 ?좎? (EpicMMO 踰꾪듉 ??
                    if (!EpicMMOReflectionHelper.IsInitialized)
                    {
                        EpicMMOReflectionHelper.Initialize();
                    }

                    if (EpicMMOReflectionHelper.IsAvailable)
                    {
                        return;
                    }

                    // ?대? 議곗젙?덉쑝硫??ㅽ궢 (留ㅻ쾲 ?ъ“??諛⑹?)
                    if (_iconPositionAdjusted) return;

                    // EpicMMO ?놁쓣 ???꾩씠肄??꾩튂瑜??붾㈃ 以묒븰 罹먮┃??癒몃━ ?꾨줈 議곗젙
                    var rect = skillTreeIconObj.GetComponent<RectTransform>();
                    if (rect == null) return;

                    // 理쒖긽??Canvas??諛곗튂?섏뿬 ?붾㈃ 以묒븰 湲곗??쇰줈 ?꾩튂 ?ㅼ젙
                    var canvas = skillTreeIconObj.GetComponentInParent<Canvas>();
                    if (canvas != null)
                    {
                        rect.SetParent(canvas.transform, false);

                        // ?붾㈃ 以묒븰 湲곗? (罹먮┃??癒몃━ ??
                        rect.anchorMin = new Vector2(0.5f, 0.5f);
                        rect.anchorMax = new Vector2(0.5f, 0.5f);
                        rect.pivot = new Vector2(0.5f, 0.5f);
                        rect.anchoredPosition = new Vector2(0, 150); // ?붾㈃ 以묒븰?먯꽌 150?쎌? ??(罹먮┃??癒몃━ ??
                        rect.sizeDelta = new Vector2(60, 60);

                        _iconPositionAdjusted = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.LogError($"[?ㅽ궗?몃━] ?꾩씠肄??꾩튂 議곗젙 ?ㅽ뙣: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 寃뚯엫 ?쒖옉 ???ㅽ궗?몃━ ?꾩씠肄?珥덇린 ?④?
        /// ?몃깽?좊━ ???뚮쭔 ?쒖떆?섎룄濡???
        /// </summary>
        [HarmonyPatch(typeof(Hud), "Awake")]
        public static class HudAwakeHideIconPatch
        {
            public static void Postfix()
            {
                // 留덈쾿遺???뚮몢由?湲濡쒖슦 ?ㅽ봽?쇱씠???좏뻾 GPU ?낅줈??(?쒕옒洹??곸옄 ?쒖젏 freeze 諛⑹?)
                SkillTree.ProducerEnchantUI.PreloadSprite();

                // Hud 珥덇린?????쎄컙??吏?곗쓣 ?먭퀬 ?꾩씠肄??④? 泥섎━
                if (Instance != null)
                {
                    Instance.StartCoroutine(DelayedHideIcon());
                }
            }

            private static IEnumerator DelayedHideIcon()
            {
                // ?꾩씠肄??앹꽦 ?꾨즺???뚭퉴吏 ?湲?
                yield return new WaitForSeconds(1f);

                if (skillTreeIconObj != null)
                {
                    skillTreeIconObj.SetActive(false);
                    Log.LogInfo("[?ㅽ궗?몃━] ?꾩씠肄?珥덇린 ?④? ?꾨즺 - ?몃깽?좊━(Tab) ?????쒖떆??");
                }
            }
        }

        // ZNet 珥덇린???꾨즺 ???쒕쾭 ?깊겕 ?쒖뒪??珥덇린??(??대컢 ?댁뒋 ?닿껐)
        [HarmonyPatch(typeof(ZNet), "Awake")]
        public static class ZNet_Awake_Patch
        {
            static void Postfix()
            {
                SkillTreeConfig.DetectServerClientMode();
                InitializeServerSync();
            }
        }

        // 신규 클라이언트 접속 시 서버 Config 개별 전송
        [HarmonyPatch(typeof(ZNet), "RPC_PeerInfo")]
        public static class ZNet_RPC_PeerInfo_Patch
        {
            static void Postfix(ZNet __instance, ZRpc rpc)
            {
                if (!__instance.IsServer()) return;

                ZNetPeer peer = null;
                foreach (var p in __instance.GetPeers())
                {
                    if (p.m_rpc == rpc) { peer = p; break; }
                }
                if (peer == null) return;

                long peerId = peer.m_uid;
                ZNet.instance.StartCoroutine(DelayedConfigSync(peerId));
            }

            private static System.Collections.IEnumerator DelayedConfigSync(long peerId)
            {
                yield return new UnityEngine.WaitForSeconds(2f);

                if (ZNet.instance == null) yield break;
                if (ZNet.instance.GetPeer(peerId) == null) yield break;

                SkillTreeConfig.BroadcastConfigToClients(peerId);
                Log.LogInfo($"[ConfigSync] 신규 접속자 Config 동기화: peerId={peerId}");
            }
        }


        // 肄섏넄 紐낅졊???깅줉
        [HarmonyPatch(typeof(Terminal), "InitTerminal")]
        public static class Terminal_InitTerminal_Patch
        {
            static void Postfix()
            {
                // 怨듦꺽 ?꾨Ц媛 ?ㅼ젙 紐낅졊?대뱾
                new Terminal.ConsoleCommand("skilltree_attack_root", "怨듦꺽 ?꾨Ц媛 猷⑦듃 ?곕?吏 蹂대꼫???ㅼ젙 (?? skilltree_attack_root 7)",
                    args => SetAttackConfig("AttackRootDamageBonus", args));

                new Terminal.ConsoleCommand("skilltree_melee_chance", "洹쇱젒 ?뱁솕 諛쒕룞 ?뺣쪧 ?ㅼ젙 (?? skilltree_melee_chance 25)",
                    args => SetAttackConfig("AttackMeleeBonusChance", args));

                new Terminal.ConsoleCommand("skilltree_melee_damage", "洹쇱젒 ?뱁솕 ?쇳빐???ㅼ젙 (?? skilltree_melee_damage 15)",
                    args => SetAttackConfig("AttackMeleeBonusDamage", args));

                new Terminal.ConsoleCommand("skilltree_bow_chance", "???뱁솕 諛쒕룞 ?뺣쪧 ?ㅼ젙 (?? skilltree_bow_chance 30)",
                    args => SetAttackConfig("AttackBowBonusChance", args));

                new Terminal.ConsoleCommand("skilltree_bow_damage", "???뱁솕 ?쇳빐???ㅼ젙 (?? skilltree_bow_damage 18)",
                    args => SetAttackConfig("AttackBowBonusDamage", args));

                // ?띾룄 ?꾨Ц媛 ?ㅼ젙 紐낅졊?대뱾
                new Terminal.ConsoleCommand("skilltree_speed_root", "?띾룄 ?꾨Ц媛 猷⑦듃 ?대룞?띾룄 ?ㅼ젙 (?? skilltree_speed_root 5)",
                    args => SetSpeedConfig("Speed_Expert_MoveSpeed", args));

                new Terminal.ConsoleCommand("skilltree_speed_dodge", "援щⅤ湲??띾룄 蹂대꼫???ㅼ젙 (?? skilltree_speed_dodge 15)",
                    args => SetSpeedConfig("Speed_Step1_DodgeSpeed", args));

                new Terminal.ConsoleCommand("skilltree_speed_melee_combo", "洹쇱젒 肄ㅻ낫 ?대룞?띾룄 ?ㅼ젙 (?? skilltree_speed_melee_combo 6)",
                    args => SetSpeedConfig("Speed_Step2_MeleeComboBonus", args));

                new Terminal.ConsoleCommand("skilltree_speed_bow_hit", "???곸쨷 ?대룞?띾룄 ?ㅼ젙 (?? skilltree_speed_bow_hit 8)",
                    args => SetSpeedConfig("Speed_Step2_BowHitBonus", args));

                new Terminal.ConsoleCommand("skilltree_speed_attack", "怨듦꺽?띾룄 利앷? ?ㅼ젙 (?? skilltree_speed_attack 10)",
                    args => SetSpeedConfig("Speed_Step8_MeleeAttackSpeed", args));

                new Terminal.ConsoleCommand("skilltree_config_reload", "?ㅼ젙 由щ줈??諛??ъ쟾??",
                    args => SkillTreeConfig.ReloadAndBroadcast());

                new Terminal.ConsoleCommand("skilltree_config_show", "?꾩옱 ?ㅼ젙 ?쒖떆",
                    args => ShowCurrentConfig());

                // ?꾩쿂 硫?곗꺑 ?ㅼ젙 紐낅졊?대뱾
                new Terminal.ConsoleCommand("skilltree_archer_arrows", "?꾩쿂 硫?곗꺑 ?붿궡 ???ㅼ젙 (?? skilltree_archer_arrows 7)",
                    args => SetArcherConfig("Archer_MultiShot_ArrowCount", args));

                new Terminal.ConsoleCommand("skilltree_archer_consume", "?꾩쿂 硫?곗꺑 ?붿궡 ?뚮え???ㅼ젙 (?? skilltree_archer_consume 2)",
                    args => SetArcherConfig("Archer_MultiShot_ArrowConsumption", args));

                new Terminal.ConsoleCommand("skilltree_archer_damage", "?꾩쿂 硫?곗꺑 ?곕?吏 鍮꾩쑉 ?ㅼ젙 (?? skilltree_archer_damage 80)",
                    args => SetArcherConfig("Archer_MultiShot_DamagePercent", args));

                // ???꾨Ц媛 硫?곗꺑 ?ㅼ젙 紐낅졊?대뱾
                new Terminal.ConsoleCommand("skilltree_bow_lv1_chance", "???꾨Ц媛 硫?곗꺑 Lv1 ?뺣쪧 ?ㅼ젙 (?? skilltree_bow_lv1_chance 15)",
                    args => SetBowConfig("Bow_MultiShot_Lv1_Chance", args));

                new Terminal.ConsoleCommand("skilltree_bow_lv2_chance", "???꾨Ц媛 硫?곗꺑 Lv2 ?뺣쪧 ?ㅼ젙 (?? skilltree_bow_lv2_chance 36)",
                    args => SetBowConfig("Bow_MultiShot_Lv2_Chance", args));

                new Terminal.ConsoleCommand("skilltree_bow_arrows", "???꾨Ц媛 硫?곗꺑 ?붿궡 ???ㅼ젙 (?? skilltree_bow_arrows 2)",
                    args => SetBowConfig("Bow_MultiShot_ArrowCount", args));

                new Terminal.ConsoleCommand("skilltree_bow_consume", "???꾨Ц媛 硫?곗꺑 ?붿궡 ?뚮え???ㅼ젙 (?? skilltree_bow_consume 0)",
                    args => SetBowConfig("Bow_MultiShot_ArrowConsumption", args));

                new Terminal.ConsoleCommand("skilltree_bow_damage", "???꾨Ц媛 硫?곗꺑 ?곕?吏 鍮꾩쑉 ?ㅼ젙 (?? skilltree_bow_damage 70)",
                    args => SetBowConfig("Bow_MultiShot_DamagePercent", args));

                // skilladd, skillreset 紐낅졊?대뒗 Jotunn CommandManager濡??대룞??(?먮룞?꾩꽦 吏??
                // RegisterJotunnCommands() 硫붿꽌?쒖뿉???깅줉
            }
        }
    }

    /// <summary>
    /// ?뚮젅?댁뼱 ?щ쭩 ???ㅽ궗/VFX ?뺣━ ??肄붾（??媛뺤젣 ?뺣━
    /// 臾댄븳 濡쒕뵫 踰꾧렇 諛⑹?瑜??꾪븳 ?덉쟾?μ튂 (?뺣━ ?쒖꽌 以묒슂!)
    /// </summary>
    [HarmonyPatch(typeof(Player), "OnDeath")]
    public static class Player_OnDeath_StopPluginCoroutines_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Player __instance)
        {
            if (__instance == Player.m_localPlayer && Plugin.Instance != null)
            {
                // ??1. 癒쇱? ?깆빱 VFX/肄붾（??利됱떆 ?뺣━ (肄붾（??以묒? ?꾩뿉 ?ㅽ뻾!)
                try
                {
                    TankerSkills.CleanupTankerOnDeath(__instance);
                }
                catch (Exception)
                {
                }

                // ??2. 吏곸뾽 ?ㅽ궗 ?뺣━
                try
                {
                    JobSkills.CleanupAllJobSkillsOnDeath(__instance);
                }
                catch (Exception)
                {
                }

                // ??2-1. ?띾룄 ?쒗븳 寃쎄퀬 ?곹깭 珥덇린??
                try
                {
                    ImprovedMoveSpeedPatch.ClearWarningState(__instance);
                    AttackSpeedHandler_Game_Awake_Patch.ClearAttackSpeedWarningState(__instance);
                }
                catch (Exception)
                {
                }

                // ??3. 留덉?留됱쑝濡?肄붾（??以묒? (紐⑤뱺 ?뺣━ ?꾨즺 ??
                Plugin.Instance.StopAllCoroutines();
            }
        }
    }

    /// <summary>
    /// Rule 14-3: Game.Awake Postfix?먯꽌 AnimationSpeedManager ?몃뱾???깅줉
    /// AnimationSpeedManager媛 珥덇린?붾맂 ?댄썑???깅줉?댁빞 ?덉젙?곸쑝濡??묐룞??
    /// Phase 2: 吏꾨떒 濡쒓렇 媛뺥솕
    /// </summary>
    [HarmonyPatch(typeof(Game), "Awake")]
    public static class AttackSpeedHandler_Game_Awake_Patch
    {
        private static bool _attackSpeedHandlerRegistered = false;

        // 寃쎄퀬 ?쒖떆 ?щ? 異붿쟻 (?뚮젅?댁뼱????踰덈쭔 ?쒖떆)
        private static Dictionary<Player, bool> _attackSpeedWarningShown = new Dictionary<Player, bool>();

        [HarmonyPostfix]
        public static void Postfix()
        {
            if (_attackSpeedHandlerRegistered) return;

            try
            {
                AnimationSpeedManager.Add((character, speed) =>
                {
                    // ?뚮젅?댁뼱 怨듦꺽 以묒씪 ?뚮쭔 泥섎━
                    if (character is Player player && player.InAttack())
                    {
                        // ?뚯쭊 踰좉린 ?쒖꽦 ??Config 湲곕컲 怨듦꺽?띾룄 (?ㅻⅨ ?몃━ 蹂대꼫??臾댁떆)
                        if (Sword_Skill.IsSlashActive(player))
                        {
                            return 1.0 + (Sword_Config.RushSlashAttackSpeedBonusValue / 100.0);
                        }


                        // 회오리베기 활성 시 300% 절대속도 (animator.speed 직접 조작 대체)
                        if (Sword_Skill.IsWhirlwindCharging(player))
                            return 3.0;
                        // 怨듦꺽?띾룄 蹂대꼫??怨꾩궛 (200ms 罹먯떆 ?곸슜)
                        float attackSpeedBonus = SkillEffect.GetTotalAttackSpeedBonus(player);

                        if (attackSpeedBonus > 0f)
                        {
                            // 遺꾨끂??留앹튂 1? 踰꾪봽 ?쒖꽦 ??罹??고쉶 (200% 洹몃?濡??곸슜)
                            if (FuryHammerSkill.IsFuryHammer1stHitBuffActive(player))
                            {
                                return speed * (1.0 + (attackSpeedBonus / 100.0));
                            }

                            // 遺꾨끂??留앹튂 5?고? ??援ш컙 罹??고쉶 (?ㅽ궗 ?쒖옉~肄ㅻ낫 醫낅즺)
                            if (FuryHammerSkill.IsFuryHammerCapBypassActive(player))
                            {
                                return speed * (1.0 + (attackSpeedBonus / 100.0));
                            }

                            // 李??꾨Ц媛 proc ?쒖꽦 ??罹??고쉶 (怨듦꺽 ?곸쨷 ?꾧퉴吏 留??꾨젅???좎?)
                            if (SkillEffect.IsSpearExpertProcActive(player))
                            {
                                return speed * (1.0 + (attackSpeedBonus / 100.0));
                            }

                            // ?좎쐢??怨듦꺽 紐⑥뀡 ?쒖꽦 ??罹??고쉶
                            if (SkillEffect.GetWhirlwindAttackSpeedBonus(player) > 0f)
                            {
                                return speed * (1.0 + (attackSpeedBonus / 100.0));
                            }

                            // 理쒕?移??쒗븳 ?곸슜 (v0.1.226+)
                            float maxBonus = SkillTreeConfig.AttackSpeedMaxBonusValue;
                            if (attackSpeedBonus > maxBonus)
                            {
                                // ??踰덈쭔 寃쎄퀬
                                if (!_attackSpeedWarningShown.ContainsKey(player) || !_attackSpeedWarningShown[player])
                                {
                                    player.Message(MessageHud.MessageType.Center,
                                        L.Get("attack_speed_cap_warning", $"{maxBonus:F0}"));
                                    _attackSpeedWarningShown[player] = true;
                                }

                                attackSpeedBonus = maxBonus;
                            }

                            return speed * (1.0 + (attackSpeedBonus / 100.0));
                        }

                        return speed;
                    }
                    return speed;
                });

                _attackSpeedHandlerRegistered = true;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// ?뚮젅?댁뼱 濡쒓렇?꾩썐/?щ쭩 ??寃쎄퀬 ?곹깭 ?뺣━
        /// </summary>
        public static void ClearAttackSpeedWarningState(Player player)
        {
            if (_attackSpeedWarningShown.ContainsKey(player))
                _attackSpeedWarningShown.Remove(player);
        }
    }

}



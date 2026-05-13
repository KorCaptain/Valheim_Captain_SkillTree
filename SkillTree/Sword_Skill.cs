using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.Gui;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 寃 ?ㅽ궗 ?꾩슜 濡쒖쭅 ?쒖뒪??
    /// ?뚯쭊 ?곗냽 踰좉린 (Rush Slash) ?≫떚釉??ㅽ궗 諛?寃 愿??紐⑤뱺 ?ㅽ궗 援ы쁽
    /// </summary>
    public static partial class Sword_Skill
    {
        // === ?뚯쭊 ?곗냽 踰좉린 (Rush Slash) ?≫떚釉??ㅽ궗 愿??蹂??===
        private static Dictionary<Player, float> rushSlashCooldowns = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> rushSlashActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> rushSlashEndTime = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> rushSlashCoroutines = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, int> rushSlashAttackCount = new Dictionary<Player, int>();
        private static Dictionary<Player, float> rushSlashInvincibleUntil = new Dictionary<Player, float>();

        // === 湲곗〈 ?명솚?깆슜 蹂꾩묶 ===
        private static Dictionary<Player, float> swordSlashCooldowns => rushSlashCooldowns;
        private static Dictionary<Player, bool> swordSlashActive => rushSlashActive;
        private static Dictionary<Player, float> swordSlashEndTime => rushSlashEndTime;
        private static Dictionary<Player, Coroutine> swordSlashCoroutines => rushSlashCoroutines;
        private static Dictionary<Player, int> swordSlashAttackCount => rushSlashAttackCount;

        // === Paladin Lv2 異붽? ?ъ슜 李?===
        private static Dictionary<Player, float> _swordSlashPendingWindow = new Dictionary<Player, float>();
        private const float SwordSlashExtraWindow = 30f;

        /// <summary>
        /// ?뚮젅?댁뼱媛 諛⑺뙣瑜?李⑹슜 以묒씤吏 ?뺤씤
        /// </summary>
        public static bool HasShield(Player player)
        {
            if (player == null) return false;

            try
            {
                var inventory = player.GetInventory();
                if (inventory == null) return false;

                foreach (var item in inventory.GetEquippedItems())
                {
                    if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ?λ퉬 紐⑸줉?먯꽌 寃 臾닿린 ?꾩씠??諛섑솚 (?⑤쭅 以?GetCurrentWeapon 臾몄젣 ?뚰뵾)
        /// </summary>
        public static ItemDrop.ItemData GetEquippedSword(Player player)
        {
            if (player == null) return null;
            try
            {
                var inventory = player.GetInventory();
                if (inventory == null) return null;

                foreach (var item in inventory.GetEquippedItems())
                {
                    if (item.m_shared.m_skillType == Skills.SkillType.Swords)
                        return item;
                }
                return null;
            }
            catch { return null; }
        }

        /// <summary>
        /// ?뚮젅?댁뼱媛 寃???ъ슜 以묒씤吏 ?뺤씤 (?뺤옣??怨좊젮 - ?ㅻⅨ 紐⑤뱶 吏??
        /// 1?쒖쐞: Valheim 湲곕낯 Swords ?ㅽ궗 ???
        /// 2?쒖쐞: ?꾨━???대쫫??"Sword", "sword", "Blade", "blade" ?ы븿
        /// 3?쒖쐞: 臾닿린 ?대쫫??"寃", "sword", "blade" ?ы븿
        /// </summary>
        public static bool IsUsingSword(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1?쒖쐞: Valheim 湲곕낯 Swords ?ㅽ궗 ????뺤씤
            if (weapon.m_shared.m_skillType == Skills.SkillType.Swords)
            {
                return true;
            }
            
            // 2?쒖쐞: ?꾨━???대쫫 ?뺤씤 (?ㅻⅨ 紐⑤뱶 吏??
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Sword") || prefabName.Contains("sword") || 
                prefabName.Contains("Blade") || prefabName.Contains("blade"))
            {
                return true;
            }
            
            // 3?쒖쐞: 臾닿린 ?대쫫 ?뺤씤 (吏??솕 諛?而ㅼ뒪? ?대쫫 吏??
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("寃") || weaponName.Contains("sword") || weaponName.Contains("blade"))
            {
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 G???≫떚釉??ㅽ궗 ?쒖꽦??
        /// - ?꾨갑 5m ?뚯쭊 ??紐ъ뒪??二쇰???鍮좊Ⅴ寃??대룞?섎ŉ 3???곗냽 踰좉린
        /// - 1李? 70%, 2李? 80%, 3李? 90% 怨듦꺽??
        /// - ?뚮え: ?ㅽ깭誘몃굹 30 | 荑⑦??? 25珥?
        /// </summary>
        public static void ActivateSwordSlash(Player player) => ActivateRushSlash(player);

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 (Rush Slash) ?≫떚釉??ㅽ궗 諛쒕룞 (G??
        /// </summary>
        public static void ActivateRushSlash(Player player)
        {
            try
            {
                if (player == null || player.IsDead())
                {
                    return;
                }

                // 1. ?ㅽ궗 蹂댁쑀 ?뺤씤
                bool hasSkill = SkillEffect.HasSkill("sword_step5_finalcut") || SkillEffect.HasSkill("sword_slash");
                if (!hasSkill)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("rush_slash_skill_required"), Color.red);
                    return;
                }

                // 2. 寃 李⑹슜 ?뺤씤
                if (!IsUsingSword(player))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("sword_required"), Color.red);
                    return;
                }

                // 3. 荑⑦????뺤씤
                float now = Time.time;
                bool hasPaladinLv2_rush = (SkillTreeManager.Instance?.GetSkillLevel("Paladin") ?? 0) >= 2;
                bool hasBerserkerLv2_rush = (SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 0) >= 2;
                bool hasTankerLv2_rush = (SkillTreeManager.Instance?.GetSkillLevel("Tanker") ?? 0) >= 2;
                bool hasExtraUse_rush = hasPaladinLv2_rush || hasBerserkerLv2_rush || hasTankerLv2_rush;
                bool inRushSlashWindow = hasExtraUse_rush
                    && _swordSlashPendingWindow.TryGetValue(player, out float rushWinExpiry)
                    && now <= rushWinExpiry;

                if (!inRushSlashWindow && rushSlashCooldowns.TryGetValue(player, out float cdEnd) && now < cdEnd)
                {
                    float remaining = cdEnd - now;
                    SkillEffect.DrawFloatingText(player, L.Get("cooldown_remaining", Mathf.CeilToInt(remaining)), Color.yellow);
                    return;
                }

                // 4. ?ㅽ깭誘몃굹 ?뚮え ?뺤씤
                float requiredStamina = Sword_Config.RushSlashStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                // 5. ?대? ?ㅽ궗 ?ㅽ뻾 以묒씤吏 ?뺤씤
                if (rushSlashActive.TryGetValue(player, out bool alreadyActive) && alreadyActive)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("rush_slash_in_progress"), Color.yellow);
                    return;
                }

                // 6. ?ㅽ궗 ?쒖꽦??
                float duration = Sword_Config.CalculateTotalSkillDuration();
                float cooldown = Sword_Config.RushSlashCooldownValue;

                rushSlashActive[player] = true;
                rushSlashEndTime[player] = now + duration;
                rushSlashAttackCount[player] = 0;

                // Paladin Lv2 / Berserker Lv2 遺꾧린
                if (hasExtraUse_rush && !inRushSlashWindow)
                {
                    // 1踰덉㎏ ?ъ슜: 荑⑦???蹂대쪟 + 30珥?李??ㅽ뵂
                    _swordSlashPendingWindow[player] = now + SwordSlashExtraWindow;
                    player.StartCoroutine(ExpireSwordSlashWindow(player));
                }
                else
                {
                    // 2踰덉㎏ ?ъ슜(李??? or 鍮꾪뙏?쇰뵖: 荑⑦???利됱떆 ?쒖옉
                    _swordSlashPendingWindow.Remove(player);
                    rushSlashCooldowns[player] = now + cooldown;
                    ActiveSkillCooldownRegistry.SetCooldownForSkills("G", new[] { "sword_step5_finalcut", "sword_slash" }, cooldown);
                }

                // 7. ?ㅽ깭誘몃굹 ?뚮え
                player.UseStamina(requiredStamina);
                SkillEffect.TankerPrereqLastUsedTime = Time.time;

                // 8. 諛쒕룞 硫붿떆吏
                SkillEffect.DrawFloatingText(player, L.Get("rush_slash_activate"), Color.red);

                // 9. 肄붾（???쒖옉
                if (rushSlashCoroutines.TryGetValue(player, out var existingCoroutine) && existingCoroutine != null)
                {
                    player.StopCoroutine(existingCoroutine);
                }

                var coroutine = ExecuteRushSlashSequence(player);
                rushSlashCoroutines[player] = player.StartCoroutine(coroutine);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Rush Slash] ?ㅽ궗 ?쒖꽦???ㅻ쪟: {ex.Message}");
            }
        }
        
        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 ?쒗???ㅽ뻾 肄붾（??
        /// ?꾨갑 ?뚯쭊 ??紐ъ뒪??二쇰???鍮좊Ⅴ寃??대룞?섎ŉ 3???곗냽 踰좉린
        /// ?ㅽ궗 醫낅즺 ???먮옒 ?꾩튂濡?蹂듦?
        /// </summary>
        private static IEnumerator ExecuteRushSlashSequence(Player player)
        {
            if (player == null || player.IsDead())
            {
                yield break;
            }

            // ?먮옒 ?꾩튂 ???(?ㅽ궗 醫낅즺 ??蹂듦???
            Vector3 originalPosition = player.transform.position;

            var skillData = Sword_Config.GetRushSlashData();
            float moveSpeed = skillData.moveSpeed;
            float initialDist = skillData.initialDistance;
            float sideDist = skillData.sideDistance;

            int rushLevel = SkillTreeManager.Instance?.GetSkillLevel("sword_step5_finalcut") ?? 1;
            float rushLevelBonus = (rushLevel - 1) * Sword_Config.RushSlashDamageLevelBonusValue;
            float adj1st = skillData.damage1stRatio + rushLevelBonus;
            float adj2nd = skillData.damage2ndRatio + rushLevelBonus;
            float adj3rd = skillData.damage3rdRatio + rushLevelBonus;

            // 臾닿린 ?뺤씤
            var weapon = player.GetCurrentWeapon();
            if (weapon == null)
            {
                if (rushSlashActive.ContainsKey(player))
                    rushSlashActive[player] = false;
                yield break;
            }

            // 臾닿린 湲곕낯 ?곕?吏
            var weaponDamage = weapon.GetDamage();
            int totalHits = 0;
            var alreadyHitInPath = new HashSet<int>();

            // === ?寃?紐ъ뒪???먯? (10m ??媛??媛源뚯슫 紐ъ뒪?? ===
            Character target = FindNearestMonster(player, 10f);
            Vector3 targetPos = target?.transform.position ??
                               (player.transform.position + GetCameraForward(player) * initialDist);

            // === 1李? ?꾨갑 ?뚯쭊 + 踰좉린 ===
            Vector3 dashTarget = player.transform.position + GetCameraForward(player) * initialDist;
            yield return MoveToPositionWithPathHit(player, dashTarget, initialDist, moveSpeed,
                weapon, weaponDamage, adj1st, Sword_Config.RushSlashPathWidthValue, alreadyHitInPath);

            if (player == null || player.IsDead() || !rushSlashActive.TryGetValue(player, out bool rsActive1) || !rsActive1)
            {
                CleanupRushSlash(player);
                yield break;
            }

            // 1李?踰좉린 ?ㅽ뻾
            int hits1 = ExecuteSlashAttack(player, weapon, weaponDamage, 1, adj1st, target);
            totalHits += hits1;
            rushSlashAttackCount[player] = 1;
            yield return new WaitForSeconds(0.35f);

            // === 2李? 紐ъ뒪???ㅻⅨ履??대룞 + 踰좉린 ===
            if (player == null || player.IsDead() || !IsUsingSword(player))
            {
                CleanupRushSlash(player);
                yield break;
            }

            // ?寃??꾩튂 媛깆떊 (紐ъ뒪?곌? ?대룞?덉쓣 ???덉쓬)
            if (target != null && !target.IsDead())
            {
                targetPos = target.transform.position;
            }

            Vector3 rightPos = CalculateSidePosition(player, targetPos, sideDist, true);
            yield return MoveToPositionWithPathHit(player, rightPos, sideDist, moveSpeed,
                weapon, weaponDamage, adj2nd, Sword_Config.RushSlashPathWidthValue, alreadyHitInPath);

            if (player == null || player.IsDead() || !rushSlashActive.TryGetValue(player, out bool rsActive2) || !rsActive2)
            {
                CleanupRushSlash(player);
                yield break;
            }

            // 2李?踰좉린 ?ㅽ뻾
            int hits2 = ExecuteSlashAttack(player, weapon, weaponDamage, 2, adj2nd, target);
            totalHits += hits2;
            rushSlashAttackCount[player] = 2;
            yield return new WaitForSeconds(0.35f);

            // === 3李? 紐ъ뒪???쇱そ ?대룞 + 踰좉린 ===
            if (player == null || player.IsDead() || !IsUsingSword(player))
            {
                CleanupRushSlash(player);
                yield break;
            }

            // ?寃??꾩튂 媛깆떊
            if (target != null && !target.IsDead())
            {
                targetPos = target.transform.position;
            }

            Vector3 leftPos = CalculateSidePosition(player, targetPos, sideDist, false);
            yield return MoveToPositionWithPathHit(player, leftPos, sideDist, moveSpeed,
                weapon, weaponDamage, adj3rd, Sword_Config.RushSlashPathWidthValue, alreadyHitInPath);

            if (player == null || player.IsDead() || !rushSlashActive.TryGetValue(player, out bool rsActive3) || !rsActive3)
            {
                CleanupRushSlash(player);
                yield break;
            }

            // 3李?踰좉린 ?ㅽ뻾 (留덈Т由??쇰땲??
            int hits3 = ExecuteSlashAttack(player, weapon, weaponDamage, 3, adj3rd, target);
            totalHits += hits3;
            rushSlashAttackCount[player] = 3;
            yield return new WaitForSeconds(0.35f);

            // === 留덈Т由? ?먮옒 ?꾩튂濡?蹂듦? ===
            if (player != null && !player.IsDead())
            {
                if (originalPosition.y > 4000f)
                {
                    // ?섏쟾 ?대?: 利됱떆 蹂듦? (?대룞 以?異쒓뎄 ?몃━嫄??묒큺 諛⑹?)
                    player.transform.position = originalPosition;
                }
                else
                {
                    float returnDistance = Vector3.Distance(player.transform.position, originalPosition);
                    yield return MoveToPosition(player, originalPosition, returnDistance, moveSpeed);
                }

                SkillEffect.DrawFloatingText(player, L.Get("rush_slash_return"), Color.cyan);
            }

            // ?곹깭 ?뺣━
            rushSlashInvincibleUntil[player] = Time.time + 1f;
            CleanupRushSlash(player);
            SkillEffect.DrawFloatingText(player, L.Get("rush_slash_complete", totalHits), Color.green);

            yield return null;
        }

        /// <summary>
        /// 湲곗〈 ?명솚?깆슜 肄붾（??(deprecated)
        /// </summary>
        private static IEnumerator ExecuteSwordSlashCombo(Player player)
        {
            yield return ExecuteRushSlashSequence(player);
        }

        /// <summary>
        /// 媛??媛源뚯슫 紐ъ뒪???먯?
        /// </summary>
        private static Character FindNearestMonster(Player player, float range)
        {
            if (player == null) return null;

            Character nearest = null;
            float minDist = range;

            foreach (var c in Character.GetAllCharacters())
            {
                if (c == null || c.IsDead() || (!c.IsMonsterFaction(Time.time) && !c.IsBoss())) continue;

                float dist = Vector3.Distance(c.transform.position, player.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = c;
                }
            }

            return nearest;
        }

        /// <summary>
        /// ?뚮젅?댁뼱瑜?紐⑺몴 ?꾩튂濡?鍮좊Ⅴ寃??대룞 (Lerp 蹂닿컙)
        /// </summary>
        private static IEnumerator MoveToPosition(Player player, Vector3 targetPos, float distance, float speed)
        {
            if (player == null) yield break;

            float duration = distance / speed;
            float elapsed = 0f;
            Vector3 startPos = player.transform.position;

            // 吏硫??믪씠 蹂댁젙 (Raycast)
            targetPos = GetGroundPosition(targetPos);

            while (elapsed < duration)
            {
                if (player == null || player.IsDead())
                {
                    yield break;
                }

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                // 遺?쒕윭???대룞 (EaseOut)
                float smoothT = 1f - Mathf.Pow(1f - t, 2f);
                Vector3 newPos = Vector3.Lerp(startPos, targetPos, smoothT);

                // 吏硫??믪씠 蹂댁젙
                newPos = GetGroundPosition(newPos);
                player.transform.position = newPos;

                yield return null;
            }

            // 理쒖쥌 ?꾩튂 ?ㅼ젙
            player.transform.position = GetGroundPosition(targetPos);
        }

        /// <summary>
        /// 吏硫??믪씠 蹂댁젙 (Raycast)
        /// </summary>
        private static Vector3 GetGroundPosition(Vector3 pos)
        {
            if (pos.y > 4000f)
            {
                // ?섏쟾 ??Y>4000): ?섏쟾 諛붾떏? terrain ?덉씠?닿? ?꾨땲誘濡??덉씠???쒗븳 ?놁씠 Raycast
                if (Physics.Raycast(pos + Vector3.up * 3f, Vector3.down, out RaycastHit dungeonHit, 8f))
                {
                    return new Vector3(pos.x, dungeonHit.point.y + 0.1f, pos.z);
                }
                return pos;
            }
            // ?쇰컲 吏??
            if (Physics.Raycast(pos + Vector3.up * 5f, Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("terrain", "Default")))
            {
                return new Vector3(pos.x, hit.point.y + 0.1f, pos.z);
            }
            return pos;
        }

        /// <summary>
        /// 踰좉린 怨듦꺽 ?ㅽ뻾 + VFX + ?곕?吏
        /// </summary>
        private static int ExecuteSlashAttack(Player player, ItemDrop.ItemData weapon, HitData.DamageTypes weaponDamage,
            int attackNumber, float damageRatio, Character target)
        {
            if (player == null) return 0;

            // 1. ?寃?諛⑺뼢?쇰줈 ?뚯쟾 (怨듦꺽 紐⑥뀡???먯뿰?ㅻ읇寃?蹂댁씠?꾨줉)
            if (target != null && !target.IsDead())
            {
                Vector3 lookDir = (target.transform.position - player.transform.position);
                lookDir.y = 0;
                if (lookDir.sqrMagnitude > 0.001f)
                    player.transform.rotation = Quaternion.LookRotation(lookDir.normalized);
            }

            // 2. 寃 ?곗냽 怨듦꺽 紐⑥뀡 ?몃━嫄?(留덉슦???곗냽 ?대┃怨??숈씪??3?곗냽 踰좉린)
            try
            {
                player.StartAttack(null, false);
            }
            catch { }

            // 3. VFX ?좏깮 (怨듦꺽 李⑥닔蹂?
            string vfxName = attackNumber switch
            {
                1 => "fx_lightningstaffprojectile_hit",
                2 => "fx_lightningstaffprojectile_hit",
                3 => "fx_crit", // ?쇰땲???④낵
                _ => "fx_lightningstaffprojectile_hit"
            };

            // 3. VFX ?ъ깮 ?꾩튂 (?뚮젅?댁뼱 ?꾨갑 or ?寃??꾩튂)
            Vector3 vfxPos = target != null && !target.IsDead()
                ? target.GetCenterPoint()
                : player.transform.position + player.transform.forward * 2f + Vector3.up * 1f;

            SimpleVFX.Play(vfxName, vfxPos, 1.5f);

            // 4. 吏곸젒 ?곕?吏 ?곸슜 (踰붿쐞 4m ??紐ъ뒪??
            float ratio = damageRatio / 100f;
            var monsters = Character.GetAllCharacters()
                .Where(c => c != null && !c.IsDead() && (c.IsMonsterFaction(Time.time) || c.IsBoss()) && Vector3.Distance(c.transform.position, player.transform.position) < 4f)
                .Take(5);

            int hitCount = 0;
            foreach (var monster in monsters)
            {
                float bossBonus = monster.IsBoss() ? 1.2f : 1f;
                var hit = new HitData();
                hit.m_damage.m_slash = weaponDamage.m_slash * ratio * bossBonus;
                hit.m_damage.m_blunt = weaponDamage.m_blunt * ratio * bossBonus;
                hit.m_damage.m_pierce = weaponDamage.m_pierce * ratio * bossBonus;

                hit.m_point = monster.GetCenterPoint();
                hit.m_dir = (monster.transform.position - player.transform.position).normalized;
                hit.m_attacker = player.GetZDOID();
                hit.SetAttacker(player);
                hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                monster.Damage(hit);
                hitCount++;

                // 媛쒕퀎 ?寃?VFX
                SimpleVFX.Play("flash_ellow_pink", monster.GetCenterPoint(), 1f);
            }

            // 5. ?뚮줈???띿뒪??
            string attackText = attackNumber switch
            {
                1 => L.Get("rush_slash_1st_attack", (int)damageRatio),
                2 => L.Get("rush_slash_2nd_attack", (int)damageRatio),
                3 => L.Get("rush_slash_finisher", (int)damageRatio),
                _ => L.Get("rush_slash_default", (int)damageRatio)
            };
            SkillEffect.DrawFloatingText(player, attackText, attackNumber == 3 ? Color.yellow : Color.red);

            return hitCount;
        }

        /// <summary>
        /// 移대찓??湲곗? ?꾨갑 諛⑺뼢 (Y異?臾댁떆)
        /// </summary>
        private static Vector3 GetCameraForward(Player player)
        {
            if (Camera.main != null)
            {
                Vector3 forward = Camera.main.transform.forward;
                forward.y = 0;
                return forward.normalized;
            }
            return player.transform.forward;
        }

        /// <summary>
        /// 移대찓??湲곗? 痢〓㈃ ?꾩튂 怨꾩궛 (?ㅻⅨ履??쇱そ)
        /// </summary>
        private static Vector3 CalculateSidePosition(Player player, Vector3 targetPos, float distance, bool right)
        {
            Vector3 sideDir;
            if (Camera.main != null)
            {
                sideDir = right ? Camera.main.transform.right : -Camera.main.transform.right;
                sideDir.y = 0;
                sideDir.Normalize();
            }
            else
            {
                sideDir = right ? player.transform.right : -player.transform.right;
            }

            return targetPos + sideDir * distance;
        }

        /// <summary>
        /// 移대찓??湲곗? ?ㅼそ ?꾩튂 怨꾩궛 (紐ъ뒪????
        /// </summary>
        private static Vector3 CalculateBackPosition(Player player, Vector3 targetPos, float distance)
        {
            Vector3 backDir;
            if (Camera.main != null)
            {
                backDir = Camera.main.transform.forward; // 移대찓???꾨갑 = 紐ъ뒪???ㅼそ
                backDir.y = 0;
                backDir.Normalize();
            }
            else
            {
                backDir = player.transform.forward;
            }

            return targetPos + backDir * distance;
        }

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 ?곹깭 ?뺣━
        /// </summary>
        private static void CleanupRushSlash(Player player)
        {
            if (player != null && rushSlashActive.ContainsKey(player))
            {
                rushSlashActive[player] = false;
            }
        }

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 ?≫떚釉??곹깭 ?뺤씤
        /// </summary>
        public static bool IsSwordSlashActive(Player player)
        {
            return rushSlashActive.TryGetValue(player, out bool active) && active &&
                   rushSlashEndTime.TryGetValue(player, out float endTime) && Time.time < endTime;
        }
        /// <summary>
        /// 돌진 연속 베기 무적 여부 (시전 중 + 종료 후 1초)
        /// </summary>
        public static bool IsRushSlashInvincible(Player player)
        {
            if (player == null) return false;
            if (rushSlashActive.TryGetValue(player, out bool active) && active) return true;
            return rushSlashInvincibleUntil.TryGetValue(player, out float t) && Time.time < t;
        }

        /// <summary>
        /// ?꾩옱 怨듦꺽 ?잛닔 ?뺤씤
        /// </summary>
        public static int GetSwordSlashAttackCount(Player player)
        {
            return rushSlashAttackCount.TryGetValue(player, out int count) ? count : 0;
        }

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 ?ㅽ궗 媛뺤젣 以묐떒
        /// </summary>
        public static void StopSwordSlash(Player player)
        {
            try
            {
                if (rushSlashActive.ContainsKey(player))
                {
                    rushSlashActive[player] = false;
                }

                if (rushSlashCoroutines.TryGetValue(player, out var stopCoroutine1) && stopCoroutine1 != null)
                {
                    player.StopCoroutine(stopCoroutine1);
                    rushSlashCoroutines[player] = null;
                }

                SkillEffect.DrawFloatingText(player, L.Get("rush_slash_canceled"), Color.yellow);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Rush Slash] ?ㅽ궗 以묐떒 ?ㅻ쪟: {ex.Message}");
            }
        }

        /// <summary>
        /// 寃 ?ㅽ궗 荑⑦????뺣낫 議고쉶
        /// </summary>
        public static float GetSwordSlashCooldownRemaining(Player player)
        {
            if (rushSlashCooldowns.TryGetValue(player, out float cooldownEnd))
            {
                return Mathf.Max(0f, cooldownEnd - Time.time);
            }
            return 0f;
        }

        /// <summary>
        /// 紐⑤뱺 寃 ?ㅽ궗 ?곹깭 珥덇린??(?뚮젅?댁뼱 濡쒓렇?꾩썐 ????
        /// </summary>
        public static void ClearSwordSkillStates(Player player)
        {
            try
            {
                StopSwordSlash(player);

                rushSlashCooldowns.Remove(player);
                rushSlashActive.Remove(player);
                rushSlashEndTime.Remove(player);
                rushSlashCoroutines.Remove(player);
                rushSlashAttackCount.Remove(player);
                rushSlashInvincibleUntil.Remove(player);

                Plugin.Log.LogDebug($"[Sword Skill] {player.GetPlayerName()} 紐⑤뱺 寃 ?ㅽ궗 ?곹깭 珥덇린???꾨즺");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Skill] ?곹깭 珥덇린???ㅻ쪟: {ex.Message}");
            }
        }

        /// <summary>
        /// ?щ옒???ㅽ궗 ?쒖꽦???곹깭 ?뺤씤 (?명솚?깆슜)
        /// </summary>
        public static bool IsSlashActive(Player player)
        {
            if (player == null) return false;
            return rushSlashActive.TryGetValue(player, out bool active) && active;
        }

        /// <summary>
        /// 寃 踰좉린 ?≫떚釉??ㅽ궗 ?щ쭩 ???뺣━ ?쒖뒪??
        /// </summary>
        /// <summary>Paladin Lv2 ?뚯쭊踰좉린 異붽? ?ъ슜 李?留뚮즺</summary>
        private static IEnumerator ExpireSwordSlashWindow(Player player)
        {
            yield return new WaitForSeconds(SwordSlashExtraWindow);
            if (_swordSlashPendingWindow.ContainsKey(player))
            {
                _swordSlashPendingWindow.Remove(player);
                float cd = Sword_Config.RushSlashCooldownValue;
                rushSlashCooldowns[player] = Time.time + cd;
                ActiveSkillCooldownRegistry.SetCooldownForSkills("G", new[] { "sword_step5_finalcut", "sword_slash" }, cd);
                Plugin.Log.LogDebug("[?뚯쭊踰좉린] Paladin 異붽? ?ъ슜 李?留뚮즺 - 荑⑦????쒖옉");
            }
        }

        public static void CleanupSwordSkillOnDeath(Player player)
        {
            try
            {
                rushSlashCooldowns.Remove(player);
                rushSlashActive.Remove(player);
                rushSlashEndTime.Remove(player);
                _swordSlashPendingWindow.Remove(player);

                if (rushSlashCoroutines.TryGetValue(player, out var stopCoroutine2) && stopCoroutine2 != null)
                {
                    try
                    {
                        Plugin.Instance?.StopCoroutine(stopCoroutine2);
                    }
                    catch { }
                    rushSlashCoroutines.Remove(player);
                }

                rushSlashAttackCount.Remove(player);
                rushSlashInvincibleUntil.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Sword Skill] ?뺣━ ?ㅽ뙣: {ex.Message}");
            }
        }

        /// <summary>
        /// 寃 ?꾨Ц媛 - 怨듦꺽??蹂대꼫??(鍮꾩쑉)
        /// ?ㅼ젣 ?④낵??ItemData.GetDamage ?⑥튂?먯꽌 ?곸슜??
        /// </summary>
        public static float GetSwordExpertDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_expert") || !Sword_Skill.IsUsingSword(player))
                return 0f;

            try
            {
                float damageBonus = Sword_Config.SwordExpertDamageValue;
                Plugin.Log.LogDebug($"[寃 ?꾨Ц媛] 怨듦꺽??+{damageBonus}%");
                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[寃 ?꾨Ц媛] 蹂대꼫??怨꾩궛 ?ㅽ뙣: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 移쇰궇 ?섏튂湲?- 怨듦꺽??怨좎젙 蹂대꼫??
        /// ?ㅼ젣 ?④낵??ItemData.GetDamage ?⑥튂?먯꽌 ?곸슜??
        /// </summary>
        public static float GetSwordRiposteDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_step3_riposte") || !Sword_Skill.IsUsingSword(player)) return 0f;

            try
            {
                float damageBonus = Sword_Config.SwordRiposteDamageBonusValue;
                Plugin.Log.LogDebug($"[移쇰궇 ?섏튂湲? 怨듦꺽??+{damageBonus}");
                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[移쇰궇 ?섏튂湲? 蹂대꼫??怨꾩궛 ?ㅽ뙣: {ex.Message}");
                return 0f;
            }
        }


        // === 패링 돌격 → 회오리베기로 교체됨 (후방호환 스텁) ===

        /// <summary>H키 핸들러에서 더 이상 호출되지 않음 — 후방호환 스텁</summary>
        public static void ActivateParryRush(Player player) { }

        /// <summary>Plugin.Patches.cs / DefenseTree.cs 에서 참조 — 항상 false 반환</summary>
        public static bool IsParryRushActive(Player player) => false;

        /// <summary>Plugin.Patches.cs / DefenseTree.cs 에서 참조 — 회오리베기에선 무시</summary>
        public static void OnParryRushTrigger(Player player, Character attacker) { }

        /// <summary>JobSkills.cs 사망 처리에서 참조 — 회오리베기 정리로 위임</summary>
        public static void CleanupParryRushOnDeath(Player player) { CleanupWhirlwindOnDeath(player); }

    }

}

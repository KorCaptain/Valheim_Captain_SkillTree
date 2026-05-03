using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// ?뚯쭊 ?곗냽 踰좉린 - ?대룞 寃쎈줈 ?덊듃 濡쒖쭅 (partial)
    /// MoveToPositionWithPathHit: ?대룞 以?寃쎈줈 ??紐ъ뒪?곕? ?ㅼ떆媛??곸쨷
    /// </summary>
    public static partial class Sword_Skill
    {
        /// <summary>
        /// ?대룞?섎ŉ 寃쎈줈 ??紐ъ뒪?곕? ?곸쨷?섎뒗 肄붾（??
        /// MoveToPosition怨??숈씪??Lerp/EaseOut ?대룞,
        /// 留??꾨젅??pathWidth 諛섍꼍 ??紐ъ뒪?곗뿉寃??곕?吏 ?곸슜 (以묐났 諛⑹?)
        /// </summary>
        private static IEnumerator MoveToPositionWithPathHit(
            Player player, Vector3 targetPos, float distance, float speed,
            ItemDrop.ItemData weapon, HitData.DamageTypes weaponDamage,
            float damageRatio, float pathWidth, HashSet<int> alreadyHit)
        {
            if (player == null) yield break;

            float duration = distance / speed;
            float elapsed = 0f;
            Vector3 startPos = player.transform.position;

            // 吏硫??믪씠 蹂댁젙
            targetPos = GetGroundPosition(targetPos);

            while (elapsed < duration)
            {
                if (player == null || player.IsDead()) yield break;

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                // EaseOut 蹂닿컙
                float smoothT = 1f - Mathf.Pow(1f - t, 2f);
                Vector3 newPos = Vector3.Lerp(startPos, targetPos, smoothT);
                newPos = GetGroundPosition(newPos);
                player.transform.position = newPos;

                // 寃쎈줈 ?덊듃: ?꾩옱 ?꾩튂 湲곗? pathWidth 諛섍꼍 ??紐ъ뒪???먯?
                if (alreadyHit != null)
                {
                    float ratio = damageRatio / 100f;
                    foreach (var c in Character.GetAllCharacters())
                    {
                        if (c == null || c.IsDead() || (!c.IsMonsterFaction(Time.time) && !c.IsBoss())) continue;
                        int id = c.GetInstanceID();
                        if (alreadyHit.Contains(id)) continue;
                        if (Vector3.Distance(c.GetCenterPoint(), player.GetCenterPoint()) > pathWidth) continue;

                        float bossBonus = c.IsBoss() ? 1.2f : 1f;
                        var hit = new HitData();
                        hit.m_damage.m_slash = weaponDamage.m_slash * ratio * bossBonus;
                        hit.m_damage.m_blunt = weaponDamage.m_blunt * ratio * bossBonus;
                        hit.m_damage.m_pierce = weaponDamage.m_pierce * ratio * bossBonus;
                        hit.m_point = c.GetCenterPoint();
                        hit.m_dir = (c.transform.position - player.transform.position).normalized;
                        hit.m_attacker = player.GetZDOID();
                        hit.SetAttacker(player);
                        hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                        c.Damage(hit);
                        alreadyHit.Add(id);

                        SimpleVFX.Play("flash_ellow_pink", c.GetCenterPoint(), 1f);
                        SkillEffect.DrawFloatingText(player, L.Get("rush_slash_path_hit"), Color.white);
                    }
                }

                yield return null;
            }

            // 理쒖쥌 ?꾩튂 蹂댁젙
            player.transform.position = GetGroundPosition(targetPos);
        }
    }
}


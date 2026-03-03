using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 돌진 연속 베기 - 이동 경로 히트 로직 (partial)
    /// MoveToPositionWithPathHit: 이동 중 경로 상 몬스터를 실시간 적중
    /// </summary>
    public static partial class Sword_Skill
    {
        /// <summary>
        /// 이동하며 경로 상 몬스터를 적중하는 코루틴
        /// MoveToPosition과 동일한 Lerp/EaseOut 이동,
        /// 매 프레임 pathWidth 반경 내 몬스터에게 데미지 적용 (중복 방지)
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

            // 지면 높이 보정
            targetPos = GetGroundPosition(targetPos);

            while (elapsed < duration)
            {
                if (player == null || player.IsDead()) yield break;

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                // EaseOut 보간
                float smoothT = 1f - Mathf.Pow(1f - t, 2f);
                Vector3 newPos = Vector3.Lerp(startPos, targetPos, smoothT);
                newPos = GetGroundPosition(newPos);
                player.transform.position = newPos;

                // 경로 히트: 현재 위치 기준 pathWidth 반경 내 몬스터 탐지
                if (alreadyHit != null)
                {
                    float ratio = damageRatio / 100f;
                    foreach (var c in Character.GetAllCharacters())
                    {
                        if (c == null || c.IsDead() || !c.IsMonsterFaction(Time.time)) continue;
                        int id = c.GetInstanceID();
                        if (alreadyHit.Contains(id)) continue;
                        if (Vector3.Distance(c.transform.position, player.transform.position) > pathWidth) continue;

                        var hit = new HitData();
                        hit.m_damage.m_slash = weaponDamage.m_slash * ratio;
                        hit.m_damage.m_blunt = weaponDamage.m_blunt * ratio;
                        hit.m_damage.m_pierce = weaponDamage.m_pierce * ratio;
                        hit.m_point = c.GetCenterPoint();
                        hit.m_dir = (c.transform.position - player.transform.position).normalized;
                        hit.m_attacker = player.GetZDOID();
                        hit.SetAttacker(player);
                        hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                        c.Damage(hit);
                        alreadyHit.Add(id);

                        SimpleVFX.Play("fx_sword_hit", c.GetCenterPoint(), 1f);
                        SkillEffect.DrawFloatingText(player, L.Get("rush_slash_path_hit"), Color.white);
                    }
                }

                yield return null;
            }

            // 최종 위치 보정
            player.transform.position = GetGroundPosition(targetPos);
        }
    }
}

using HarmonyLib;

namespace CaptainSkillTree.Mods
{
    // BlacksmithingExpanded 기능 차단 패치 묶음
    // 모두 [HarmonyAfter(GUID)]로 BlacksmithingExpanded 실행 후 비-제작 전문가는 원본 복원
    // 미설치 시 CanUse()가 true 반환 → 즉시 return, 성능 영향 없음

    // ──── 데미지 / 원소 주입 ────────────────────────────────────────────────

    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    [HarmonyAfter(BlacksmithingBridge.GUID)]
    internal static class BSE_DamageGate
    {
        static void Prefix(ItemDrop.ItemData __instance, out HitData.DamageTypes __state)
        {
            __state = __instance.m_shared.m_damages;
        }

        static void Postfix(ref HitData.DamageTypes __result, HitData.DamageTypes __state)
        {
            if (BlacksmithingBridge.CanUse()) return;
            __result = __state;
        }
    }

    // ──── 방어력 보너스 ─────────────────────────────────────────────────────

    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetArmor), new[] { typeof(int), typeof(float) })]
    [HarmonyAfter(BlacksmithingBridge.GUID)]
    internal static class BSE_ArmorGate
    {
        static void Prefix(ItemDrop.ItemData __instance, out float __state)
        {
            __state = __instance.m_shared.m_armor;
        }

        static void Postfix(ref float __result, float __state)
        {
            if (BlacksmithingBridge.CanUse()) return;
            __result = __state;
        }
    }

    // ──── 최대 내구도 ───────────────────────────────────────────────────────

    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetMaxDurability), new[] { typeof(int) })]
    [HarmonyAfter(BlacksmithingBridge.GUID)]
    internal static class BSE_DurabilityGate
    {
        static void Prefix(ItemDrop.ItemData __instance, out float __state)
        {
            __state = __instance.m_shared.m_maxDurability;
        }

        static void Postfix(ref float __result, float __state)
        {
            if (BlacksmithingBridge.CanUse()) return;
            __result = __state;
        }
    }

    // ──── 블록 파워 ─────────────────────────────────────────────────────────

    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetBlockPower),
        new System.Type[] { typeof(float) })]
    [HarmonyAfter(BlacksmithingBridge.GUID)]
    internal static class BSE_BlockGate
    {
        static void Prefix(ItemDrop.ItemData __instance, out float __state)
        {
            __state = __instance.m_shared.m_blockPower;
        }

        static void Postfix(ref float __result, float __state)
        {
            if (BlacksmithingBridge.CanUse()) return;
            __result = __state;
        }
    }

    // ──── 제련소 / 가마 속도 강화 ───────────────────────────────────────────

    [HarmonyPatch(typeof(Smelter), "UpdateSmelter")]
    [HarmonyAfter(BlacksmithingBridge.GUID)]
    internal static class BSE_SmelterGate
    {
        // BlacksmithingExpanded가 쿠킹 속도를 곱한 경우 원본 복원
        // 실제 패치 대상 메서드명은 DLL 분석 후 조정 필요
        static void Prefix(Smelter __instance, out float __state)
        {
            __state = __instance.m_secPerProduct;
        }

        static void Postfix(Smelter __instance, float __state)
        {
            if (BlacksmithingBridge.CanUse()) return;
            __instance.m_secPerProduct = __state;
        }
    }
}

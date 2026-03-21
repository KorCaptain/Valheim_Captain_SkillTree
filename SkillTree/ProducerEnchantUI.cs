using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가(Producer) 마법부여 아이템 슬롯 테두리 표시
    /// cspt_enchant_type > 0 인 슬롯에 주황-붉은색 테두리를 표시한다.
    /// </summary>
    [HarmonyPatch]
    public static class ProducerEnchantUI
    {
        // ───────────────────────────────────────────────────────
        // 상수 / 설정값
        // ───────────────────────────────────────────────────────
        const string BorderName  = "CsptEnchantBorder";
        const int    TexSize     = 64;
        const float  GlowPx     = 8f;  // 64px 기준 글로우 두께 (좁은 가장자리)
        static readonly Color GlowColor = new Color(0.9f, 0.05f, 0.05f); // Crimson red

        static Sprite _glowSprite; // 캐시된 글로우 스프라이트 (한 번만 생성)

        // ───────────────────────────────────────────────────────
        // 패치 1: InventoryGrid.UpdateGui
        // ───────────────────────────────────────────────────────
        [HarmonyPatch(typeof(InventoryGrid), "UpdateGui")]
        [HarmonyPostfix]
        public static void Postfix_InventoryGrid(InventoryGrid __instance)
        {
            // Traverse로 private 필드 접근
            var elements  = Traverse.Create(__instance).Field("m_elements").GetValue() as IList;
            var inventory = Traverse.Create(__instance).Field("m_inventory").GetValue<Inventory>();
            if (elements == null || inventory == null) return;

            // 그리드 너비: Traverse 또는 m_width 필드
            int width = Traverse.Create(__instance).Field("m_width").GetValue<int>();
            if (width <= 0) width = 8; // 기본값 fallback

            var allItems = inventory.GetAllItems();

            for (int i = 0; i < elements.Count; i++)
            {
                var elem = elements[i];
                if (elem == null) continue;

                var go = Traverse.Create(elem).Field("m_go").GetValue<GameObject>();
                if (go == null) continue;

                var gridPos = new Vector2i(i % width, i / width);
                ItemDrop.ItemData item = allItems.Find(it => it.m_gridPos == gridPos);

                SetBorder(go.transform, IsEnchanted(item));
            }
        }

        // ───────────────────────────────────────────────────────
        // 패치 2: HotkeyBar.UpdateIcons
        // ───────────────────────────────────────────────────────
        [HarmonyPatch(typeof(HotkeyBar), "UpdateIcons")]
        [HarmonyPostfix]
        public static void Postfix_HotkeyBar(HotkeyBar __instance, Player player)
        {
            if (player == null) return;

            var elements = Traverse.Create(__instance).Field("m_elements").GetValue() as IList;
            if (elements == null) return;

            var allItems = player.GetInventory().GetAllItems();

            for (int i = 0; i < elements.Count; i++)
            {
                var elem = elements[i];
                if (elem == null) continue;

                var go = Traverse.Create(elem).Field("m_go").GetValue<GameObject>();
                if (go == null)
                {
                    var icon = Traverse.Create(elem).Field("m_icon").GetValue<Image>();
                    if (icon != null && icon.transform.parent != null)
                        go = icon.transform.parent.gameObject;
                }
                if (go == null) continue;

                // 핫키바는 인벤토리 첫 번째 행(y = 0)
                var gridPos = new Vector2i(i, 0);
                ItemDrop.ItemData item = allItems.Find(it => it.m_gridPos == gridPos);

                SetBorder(go.transform, IsEnchanted(item));
            }
        }

        // ───────────────────────────────────────────────────────
        // 헬퍼: 마법부여 여부 판정
        // ───────────────────────────────────────────────────────
        static bool IsEnchanted(ItemDrop.ItemData item)
        {
            if (item?.m_customData == null) return false;
            return item.m_customData.TryGetValue("cspt_enchant_type", out string t)
                   && int.TryParse(t, out int v) && v > 0;
        }

        // ───────────────────────────────────────────────────────
        // 헬퍼: 슬롯에 테두리 생성/표시/숨김
        // ───────────────────────────────────────────────────────
        static void SetBorder(Transform slotTransform, bool show)
        {
            Transform border = slotTransform.Find(BorderName);

            if (!show)
            {
                if (border != null) border.gameObject.SetActive(false);
                return;
            }

            if (border == null)
                border = CreateBorder(slotTransform);
            else
                RefreshBorder(border); // 기존 border sprite 갱신 (구버전 잔존 처리)

            border.gameObject.SetActive(true);
            border.SetAsFirstSibling(); // 아이콘 뒤에 위치
        }

        // ───────────────────────────────────────────────────────
        // 글로우 스프라이트 생성 (캐시)
        // 가장자리: Crimson red 불투명 → 중앙: 완전 투명 그라데이션
        // ───────────────────────────────────────────────────────
        static Sprite GetOrCreateGlowSprite()
        {
            // 씬 재로드 시 Unity가 Texture를 파괴할 수 있으므로 texture까지 체크
            if (_glowSprite != null && _glowSprite.texture != null) return _glowSprite;

            var tex = new Texture2D(TexSize, TexSize, TextureFormat.RGBA32, false);
            tex.hideFlags = HideFlags.HideAndDontSave; // 씬 전환에도 파괴되지 않음
            for (int x = 0; x < TexSize; x++)
            for (int y = 0; y < TexSize; y++)
            {
                float dx       = Mathf.Min(x, TexSize - 1 - x);
                float dy       = Mathf.Min(y, TexSize - 1 - y);
                float edgeDist = Mathf.Min(dx, dy);
                float alpha    = 1f - Mathf.Clamp01(edgeDist / GlowPx);
                tex.SetPixel(x, y, new Color(GlowColor.r, GlowColor.g, GlowColor.b, alpha));
            }
            tex.Apply();
            _glowSprite = Sprite.Create(tex, new Rect(0, 0, TexSize, TexSize),
                                        new Vector2(0.5f, 0.5f));
            _glowSprite.hideFlags = HideFlags.HideAndDontSave;
            return _glowSprite;
        }

        // ───────────────────────────────────────────────────────
        // 기존 border 갱신: 구버전 Inner 제거 + sprite 교체
        // ───────────────────────────────────────────────────────
        static void RefreshBorder(Transform border)
        {
            // 구버전 Inner 자식 제거 (파란 박스 2중 표시 원인)
            var inner = border.Find("Inner");
            if (inner != null) UnityEngine.Object.Destroy(inner.gameObject);

            var img = border.GetComponent<Image>();
            if (img == null) img = border.gameObject.AddComponent<Image>();
            img.sprite        = GetOrCreateGlowSprite();
            img.color         = Color.white;
            img.raycastTarget = false;
        }

        // ───────────────────────────────────────────────────────
        // 테두리 GameObject 생성
        // ───────────────────────────────────────────────────────
        static Transform CreateBorder(Transform parent)
        {
            var go = new GameObject(BorderName);
            go.transform.SetParent(parent, false);

            var img = go.AddComponent<Image>();
            img.sprite        = GetOrCreateGlowSprite();
            img.color         = Color.white; // 스프라이트에 색상 포함
            img.raycastTarget = false;

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            return go.transform;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        // 캐시된 FieldInfo (반복 리플렉션 제거)
        // Element/ElementData는 internal 타입이므로 GetNestedType으로 런타임 획득
        // ───────────────────────────────────────────────────────
        static FieldInfo _fi_elemGo;      // InventoryGrid.Element.m_go
        static FieldInfo _fi_hkbElemGo;   // HotkeyBar.ElementData.m_go
        static FieldInfo _fi_hkbElemIcon; // HotkeyBar.ElementData.m_icon
        // InventoryGrid 인스턴스 필드 캐시 (Traverse.Create 반복 제거)
        static FieldInfo _fi_gridElements; // InventoryGrid.m_elements
        static FieldInfo _fi_gridInventory; // InventoryGrid.m_inventory
        static FieldInfo _fi_gridWidth;    // InventoryGrid.m_width
        static bool _fieldsInitialized;

        static void EnsureFields()
        {
            if (_fieldsInitialized) return;
            _fieldsInitialized = true;

            const BindingFlags bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var elemType = typeof(InventoryGrid).GetNestedType("Element", bf);
            if (elemType != null)
                _fi_elemGo = elemType.GetField("m_go", bf);

            var hkbType = typeof(HotkeyBar).GetNestedType("ElementData", bf);
            if (hkbType != null)
            {
                _fi_hkbElemGo   = hkbType.GetField("m_go",   bf);
                _fi_hkbElemIcon = hkbType.GetField("m_icon", bf);
            }

            // InventoryGrid 필드 캐시
            _fi_gridElements = typeof(InventoryGrid).GetField("m_elements", bf);
            _fi_gridInventory = typeof(InventoryGrid).GetField("m_inventory", bf);
            _fi_gridWidth    = typeof(InventoryGrid).GetField("m_width", bf);

            // 글로우 스프라이트 선행 초기화는 PreloadSprite()로 분리됨 (HudAwake 시점 호출)
            // 드래그/상자 열기 시점에 tex.Apply() GPU 업로드가 발생하지 않도록 여기서 제거
        }

        // InventoryGrid 업데이트 쓰로틀 (인벤토리 열 때 과도한 반복 방지)
        static float _lastGridUpdate = 0f;
        const float GridUpdateInterval = 0.25f; // 4회/초로 제한
        // HotkeyBar 업데이트 쓰로틀 (매 프레임 실행 방지)
        static float _lastHotkeyUpdate = 0f;
        const float HotkeyUpdateInterval = 0.5f;

        // ───────────────────────────────────────────────────────
        // 패치 1: InventoryGrid.UpdateGui
        // ───────────────────────────────────────────────────────
        [HarmonyPatch(typeof(InventoryGrid), "UpdateGui")]
        [HarmonyPostfix]
        public static void Postfix_InventoryGrid(InventoryGrid __instance)
        {
            // 쓰로틀: 0.25초 간격으로 제한 (인벤토리 열기/수리/상자 이동 시 과도한 반복 방지)
            float now = Time.time;
            if (now - _lastGridUpdate < GridUpdateInterval) return;
            _lastGridUpdate = now;

            EnsureFields();
            // 캐시된 FieldInfo 사용 (Traverse.Create 3회 반복 제거)
            var elements  = _fi_gridElements  != null ? _fi_gridElements.GetValue(__instance)  as IList      : Traverse.Create(__instance).Field("m_elements").GetValue() as IList;
            var inventory = _fi_gridInventory != null ? _fi_gridInventory.GetValue(__instance) as Inventory   : Traverse.Create(__instance).Field("m_inventory").GetValue<Inventory>();
            if (elements == null || inventory == null) return;

            int width = _fi_gridWidth != null ? (int)_fi_gridWidth.GetValue(__instance) : Traverse.Create(__instance).Field("m_width").GetValue<int>();
            if (width <= 0) width = 8;

            // O(n) Dictionary 빌드 → 슬롯마다 O(1) 룩업
            var allItems = inventory.GetAllItems();
            var itemByPos = BuildItemDict(allItems);

            for (int i = 0; i < elements.Count; i++)
            {
                var elem = elements[i];
                if (elem == null) continue;

                // 캐시된 FieldInfo 사용 (Traverse.Create 루프 내 반복 제거)
                var go = _fi_elemGo != null
                    ? _fi_elemGo.GetValue(elem) as GameObject
                    : Traverse.Create(elem).Field("m_go").GetValue<GameObject>();
                if (go == null) continue;

                var gridPos = new Vector2i(i % width, i / width);
                itemByPos.TryGetValue(gridPos, out var item);
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
            EnsureFields();

            // 쓰로틀: HotkeyBar.UpdateIcons는 매 프레임 호출되므로 0.5초 간격만 처리
            float now = Time.time;
            if (now - _lastHotkeyUpdate < HotkeyUpdateInterval) return;
            _lastHotkeyUpdate = now;

            var elements = Traverse.Create(__instance).Field("m_elements").GetValue() as IList;
            if (elements == null) return;

            // O(n) Dictionary 빌드 → 슬롯마다 O(1) 룩업
            var allItems = player.GetInventory().GetAllItems();
            var itemByPos = BuildItemDict(allItems);

            for (int i = 0; i < elements.Count; i++)
            {
                var elem = elements[i];
                if (elem == null) continue;

                // 캐시된 FieldInfo 사용 (Traverse.Create 루프 내 반복 제거)
                GameObject go = null;
                if (_fi_hkbElemGo != null)
                    go = _fi_hkbElemGo.GetValue(elem) as GameObject;
                else
                    go = Traverse.Create(elem).Field("m_go").GetValue<GameObject>();

                if (go == null)
                {
                    Image icon = null;
                    if (_fi_hkbElemIcon != null)
                        icon = _fi_hkbElemIcon.GetValue(elem) as Image;
                    else
                        icon = Traverse.Create(elem).Field("m_icon").GetValue<Image>();

                    if (icon != null && icon.transform.parent != null)
                        go = icon.transform.parent.gameObject;
                }
                if (go == null) continue;

                // 핫키바는 인벤토리 첫 번째 행(y = 0)
                var gridPos = new Vector2i(i, 0);
                itemByPos.TryGetValue(gridPos, out var item);
                SetBorder(go.transform, IsEnchanted(item));
            }
        }

        // ───────────────────────────────────────────────────────
        // 헬퍼: 그리드 위치 → 아이템 Dictionary 빌드 (O(n) 1회)
        // static Dictionary 재사용으로 GC 할당 제거 (0.25s마다 new 제거)
        // ───────────────────────────────────────────────────────
        static readonly Dictionary<Vector2i, ItemDrop.ItemData> _itemByPosCache =
            new Dictionary<Vector2i, ItemDrop.ItemData>();

        static Dictionary<Vector2i, ItemDrop.ItemData> BuildItemDict(List<ItemDrop.ItemData> items)
        {
            _itemByPosCache.Clear();
            foreach (var item in items)
                _itemByPosCache[item.m_gridPos] = item;
            return _itemByPosCache;
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
        // Transform 캐시: 슬롯마다 Transform.Find(O(n)) 반복 제거
        // ───────────────────────────────────────────────────────
        static readonly Dictionary<Transform, Transform> _borderCache =
            new Dictionary<Transform, Transform>();

        static void SetBorder(Transform slotTransform, bool show)
        {
            // 캐시 조회 (Unity 오브젝트는 파괴 시 null 반환)
            if (!_borderCache.TryGetValue(slotTransform, out Transform border) || border == null)
            {
                border = slotTransform.Find(BorderName);
                if (border != null) _borderCache[slotTransform] = border;
            }

            if (!show)
            {
                if (border != null) border.gameObject.SetActive(false);
                return;
            }

            if (border == null)
            {
                border = CreateBorder(slotTransform);
                _borderCache[slotTransform] = border;
            }
            else if (!IsBorderUpToDate(border))
                RefreshBorder(border); // 구버전 border에만 1회 갱신 (매 0.25s 반복 실행 방지)

            border.gameObject.SetActive(true);
            border.SetAsFirstSibling(); // 아이콘 뒤에 위치
        }

        // ───────────────────────────────────────────────────────
        // HudAwake 시점에 호출 — tex.Apply() GPU 업로드를 게임 로드 시점에 선행 처리
        // ───────────────────────────────────────────────────────
        public static void PreloadSprite()
        {
            EnsureFields();
            GetOrCreateGlowSprite();
        }

        // ───────────────────────────────────────────────────────
        // 헬퍼: border가 최신 sprite를 갖고 있는지 확인
        // RefreshBorder()를 구버전 border에만 1회 실행하기 위해 사용
        // ───────────────────────────────────────────────────────
        static bool IsBorderUpToDate(Transform border)
        {
            if (_glowSprite == null) return false;
            var img = border.GetComponent<Image>();
            return img != null && img.sprite == _glowSprite;
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

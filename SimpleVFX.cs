using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.SkillTree;
namespace CaptainSkillTree
{
    /// <summary>
    /// SimpleVFX - ??影?れ쉬??? VFX ???β뼯援η뙴????????    /// </summary>
    public static class SimpleVFX
    {
        #region Static Fields

        /// <summary>
        /// ??????????썼キ?κ괌??        /// </summary>
        private static Dictionary<string, GameObject> _cachedPrefabs = new Dictionary<string, GameObject>();

        /// <summary>
        /// ?汝??吏??좉텣??AssetBundle (static ???, ?癲ル슢??遺룔뀋??????
        /// </summary>
        private static AssetBundle _debuffBundle = null;

        /// <summary>
        /// ?潁??용끏???????썹땟?????
        /// </summary>
        private static bool _initialized = false;

        /// <summary>
        /// RPC ??嶺뚮슣?쒎젆?嚥???????熬곣뫖?삥납? ?????關??(???類ㅺ튃????猷매?댚???꿔꺂?볟젆怨곷븶???
        /// </summary>
        private static bool _isReceivingRPC = false;

        /// <summary>
        /// ?汝??吏??놁뗀??꿔꺂????????꾩룆??????뚯????덈춣? RPC 嚥싳쉶瑗??꾧틚?????꾩룆????熬곣뫖?삥납???(vfxName+playerID ?????꾩룆???Time.time)
        /// </summary>
        private static readonly Dictionary<string, float> _recentLocalCreations
            = new Dictionary<string, float>();

        /// <summary>
        /// VFX ???????dim factor ?꿔꺂?????몃??(Initialize??????嚥싲갭큔?댁쉩?? ?꿔꺂??袁ㅻ븶??????꾩룆????嚥▲굧???뚪뜮????????嶺?????쇨덫??
        /// </summary>
        private static readonly Dictionary<string, float> _vfxDimMapping
            = new Dictionary<string, float>();

        public static void RegisterVFXDim(string vfxName, float factor)
            => _vfxDimMapping[vfxName] = factor;

        /// <summary>
        /// ??影?れ쉬??? VFX ?????됱뎽????ㅼ뒩??RPC ?????
        /// </summary>
        internal const string RPC_PLAY_CUSTOM_VFX       = "CaptainVFX_PlayCustom";
        internal const string RPC_PLAY_ON_PLAYER        = "CaptainVFX_PlayOnPlayer";
        internal const string RPC_PLAY_VANILLA_VFX      = "CaptainVFX_PlayVanilla";
        internal const string RPC_PLAY_VANILLA_ONPLAYER = "CaptainVFX_PlayVanillaOnPlayer";

        /// <summary>
        /// ??影?れ쉬??? VFX ?꿔꺂??袁ㅻ븶筌믠뫀萸?(AssetBundle??????汝??吏??좉텣? Destroy ????썹땟??
        /// ?熬곣뫖利든뜏類ｋ쭬??????뚯????VFX??Destroy ?癲ル슢?????????類ㅺ튃???汝??吏??뮤??熬곣뫖利든뜏類ｋ렱??
        /// </summary>
        private static readonly HashSet<string> _customVFXNames = new HashSet<string>
        {
            // ????쇨덧????壤굿??뗫툞
            "area_circles_blue", "area_fire_red", "area_heal_green",
            "area_magic_multicolor", "area_star_ellow",

            // ?筌?????브큿????됰Ŧ??????壤굿??뗫툞
            "buff_01", "buff_02a", "buff_03a", "buff_03a_aura",
            "debuff", "debuff_03", "debuff_03_aura",
            "statusailment_01", "statusailment_01_aura",

            // ??濚밸Ŧ?깍㎗????壤굿??뗫툞
            "confetti_blast_multicolor", "confetti_directional_multicolor",

            // ?亦껋꼨援??/????쇰뮛????壤굿??뗫툞
            "dust_permanently_blue",

            // ????????壤굿??뗫툞 (flash_star_ellow_purple ????)
            "flash_blue_purple", "flash_ellow", "flash_ellow_pink",
            "flash_magic_blue_pink", "flash_magic_ellow_blue", "flash_round_ellow",
            "flash_star_ellow_green", "flash_star_ellow_purple",

            // ?熬곣뫖?삥납??關????⑤㈇???レ뵛壤???壤굿??뗫툞
            "guard_01", "healing",

            // ??????壤굿??뗫툞
            "hit_01", "hit_02", "hit_03", "hit_04",

            // ??????嶺?筌???壤굿??뗫툞
            "plexus",

            // ???繹먮굝????壤굿??뗫툞
            "shine_blue", "shine_ellow", "shine_pink",

            // ?????곌틞???????곌틞????壤굿??뗫툞
            "sparkle_ellow",

            // ????????壤굿??뗫툞
            "taunt",

            // ????ㅼ뒩????????????됱뎽 ??壤굿??뗫툞
            "water_blast_blue", "water_blast_green"
        };

        /// <summary>
        /// ????????ㅿ폎???VFX (debuff ?筌???熬곊녠텣?
        /// </summary>
        public static GameObject PlayerVFX = null;

        /// <summary>
        /// ?꿔꺂?????녿쫯??????獒?VFX (Valheim ????ㅼ굣亦?
        /// </summary>
        public static GameObject MonsterVFX = null;

        #endregion

        #region ?潁??용끏???
        /// <summary>
        /// ZNetScene.Awake??????癲ル슢????
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            try
            {

                // 1. PrefabRegistry??????嚥싲갭큔?댁쉩???VFX ????썼キ?κ괌????醫딆쓧??癲ル슢???몄쒜嚥▲룗??                LoadFromPrefabRegistry();

                // 2. "debuff" ?筌???熬곊녠텣??????汝??吏??좉텣?(fallback)
                LoadDebuffBundle();

                // 3. Valheim ????ㅼ굣亦?VFX ?????(?꿔꺂?????녿쫯??????獒?
                CacheValheimPrefabs();

                // VFX ?熬곣뫖利???앶???醫딆┫???- _customVFXNames ????썹땟??????繹?(?筌?????브큿????????袁?닧?嚥싲갭큔?琉븍닱?taunt ??嶺뚮????
                float vfxDim = SkillTreeConfig.VFXOpacityValue;
                var dimExcluded = new HashSet<string> { "statusailment_01_aura", "taunt" };
                foreach (var vfxName in _customVFXNames)
                {
                    if (!dimExcluded.Contains(vfxName))
                        RegisterVFXDim(vfxName, vfxDim);
                }

                _initialized = true;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] ?潁??용끏????????곌숯: {ex.Message}");
            }
        }

        /// <summary>
        /// PrefabRegistry??????嚥싲갭큔?댁쉩???VFX ????썼キ?κ괌??袁⑸즴筌?쑜琉???????????ㅻ쿋??
        /// </summary>
        private static void LoadFromPrefabRegistry()
        {
            try
            {
                var allPrefabs = CaptainSkillTree.Prefab.PrefabRegistry.GetAllRegisteredPrefabs();
                int loadedCount = 0;

                foreach (var kvp in allPrefabs)
                {
                    string prefabName = kvp.Key;
                    GameObject prefab = kvp.Value;

                    if (prefab == null) continue;

                    // ??影?れ쉬??? VFX ???????嚥▲굧????鶯??????????ㅻ쿋??
                    if (_customVFXNames.Contains(prefabName) && !_cachedPrefabs.ContainsKey(prefabName))
                    {
                        _cachedPrefabs[prefabName] = prefab;
                        loadedCount++;
                    }

                    // ??????????類ㅺ퉻???嚥▲굧????(?? Buff_01 -> buff_01)
                    string lowerName = prefabName.ToLowerInvariant();
                    if (_customVFXNames.Contains(lowerName) && !_cachedPrefabs.ContainsKey(lowerName))
                    {
                        _cachedPrefabs[lowerName] = prefab;
                        loadedCount++;
                    }
                }

            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[SimpleVFX] PrefabRegistry ?汝??吏??좉텣??????곌숯: {ex.Message}");
            }
        }

        /// <summary>
        /// "debuff" ?筌???熬곊녠텣??????汝??吏??좉텣?(?????癲ル슢????
        /// </summary>
        private static void LoadDebuffBundle()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourceName = null;

                foreach (var name in assembly.GetManifestResourceNames())
                {
                    if (name.EndsWith(".debuff") || name.Contains(".VFX.debuff"))
                    {
                        resourceName = name;
                        break;
                    }
                }

                if (resourceName == null)
                {
                    Plugin.Log?.LogWarning("[SimpleVFX] 'debuff' ??잙갭큔?딆뼇???????ㅼ굡??);
                    return;
                }

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null) return;
                    _debuffBundle = AssetBundle.LoadFromStream(stream);
                }

                if (_debuffBundle != null)
                {
                    var assets = _debuffBundle.LoadAllAssets<GameObject>();
                    if (assets.Length > 0)
                    {
                        _cachedPrefabs["debuff"] = assets[0];
                        PlayerVFX = assets[0];  // ????????ㅿ폎???VFX??????ル뒇??
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] debuff ?筌???熬곊녠텣??汝??吏??좉텣??????곌숯: {ex.Message}");
            }
        }

        /// <summary>
        /// Valheim ????ㅼ굣亦?VFX ?????(???????????β뼯爰귨㎘?VFX ????썼キ?κ괌??袁⑸즴筌?쑜琉?
        /// </summary>
        public static void CacheValheimPrefabs()
        {
            try
            {
                // ???????????β뼯爰귨㎘?VFX ??????꿔꺂??袁ㅻ븶筌믠뫀萸?
                string[] vfxNames = new string[]
                {
                    // ?꿔꺂?????녿쫯????????壤굿??뗫툞
                    "fx_seeker_hurt", "fx_backstab", "fx_crit",
                    // ??????筌?????브큿???壤굿??뗫툞
                    "vfx_HealthUpgrade", "shaman_heal_aoe", "fx_greydwarf_shaman_heal",
                    "buff_03a", "buff_03a_aura", "buff_02a", "statusailment_01_aura",
                    // ?????꿔꺂???熬곊삳????壤굿??뗫툞
                    "vfx_GodExplosion", "fx_siegebomb_explosion", "fx_Fader_Roar",
                    "fx_Lightning", "fx_fader_meteor_hit", "fx_bow_hit",
                    // ?????곕짍/????Β??뼐 ??壤굿??뗫툞
                    "vfx_spawn_small", "vfx_spawn_large", "flash_blue_purple",
                    "fx_guardstone_activate", "fx_guardstone_permitted_add",
                    "fx_eikthyr_stomp", "fx_Fader_Spin", "debuff_03",
                    // ?????                    "sfx_morgen_alert", "sfx_dverger_heal_finish", "sfx_oozebomb_explode"
                };

                GameObject[] all = Resources.FindObjectsOfTypeAll<GameObject>();

                foreach (GameObject obj in all)
                {
                    if (obj == null) continue;

                    // ????썼キ?κ괌??袁⑸즴筌?（鍮?????꾣뤃?饔낃퀣??(???癲ル슢???吏????ㅿ폍筌???嶺뚮????
                    if (obj.scene.name != null && obj.scene.rootCount > 0) continue;

                    // ?꿔꺂?????녿쫯??????獒?VFX - fx_seeker_hurt
                    if (obj.name.Contains("fx_seeker_hurt") && MonsterVFX == null)
                    {
                        MonsterVFX = obj;
                        _cachedPrefabs["monster"] = obj;
                    }

                    // ???????????β뼯爰귨㎘?VFX ?????
                    foreach (string vfxName in vfxNames)
                    {
                        if (obj.name == vfxName && !_cachedPrefabs.ContainsKey(vfxName))
                        {
                            _cachedPrefabs[vfxName] = obj;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] Valheim ????썼キ?κ괌????????????곌숯: {ex.Message}");
            }
        }

        #endregion

        #region VFX ??嚥?(WackyEpicMMO PlayerFVX ?熬곣뫖?삥납??

        /// <summary>
        /// ????????ㅿ폎???????산뭐?????紐낅뤁??VFX (?筌????釉띾튉??維◈??熬곣뫖利든뜏類ｋ걝????
        /// </summary>
        public static GameObject PlayOnPlayer(Player player, float duration = 5f)
        {
            if (player == null || PlayerVFX == null)
            {
                Plugin.Log?.LogWarning($"[SimpleVFX] PlayOnPlayer - player: {(player != null)}, PlayerVFX: {(PlayerVFX != null)}");
                return null;
            }

            try
            {
                // ????????ㅿ폎??Transform???꿔꺂????????낇뀘???(??????????산뭐?????紐껎뜚)
                var vfxObj = UnityEngine.Object.Instantiate(PlayerVFX, player.transform);

                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = new Vector3(0f, 1f, 0f);  // ??????嚥싳쉶瑗??꾧틡??????썼짆?⑹퀪?
                    // ???????????? (???????됰슦????????
                    Plugin.Log?.LogInfo($"[SimpleVFX] VFX ???꾩룆????(?????????낇뀘??? - ????? {vfxObj.transform.localScale}");
                    UnityEngine.Object.Destroy(vfxObj, duration);
                    // dim ????쇨덫??("debuff" = PlayerVFX)
                    if (_vfxDimMapping.TryGetValue("debuff", out float dimFactor))
                        ApplyVFXDim(vfxObj, dimFactor);
                }

                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayOnPlayer ?????곌숯: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// ???쒙쭫??????썹땟???VFX (?꿔꺂?????녿쫯????????
        /// </summary>
        public static GameObject PlayAtPosition(Vector3 position, float duration = 2f)
        {
            if (MonsterVFX == null) return null;

            try
            {
                // WackyEpicMMO CriticalVFX ?熬곣뫖?삥납??
                var vfxObj = UnityEngine.Object.Instantiate(MonsterVFX, position, Quaternion.identity);
                if (vfxObj != null)
                {
                    UnityEngine.Object.Destroy(vfxObj, duration);
                }

                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayAtPosition ?????곌숯: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// ????썼キ?κ괌???꿔꺂???????醫딆쓧??癲ル슢???몄쒜嚥▲룗??        /// </summary>
        public static GameObject GetPrefab(string type)
        {
            if (type == "player") return PlayerVFX;
            if (type == "monster") return MonsterVFX;
            _cachedPrefabs.TryGetValue(type, out var prefab);
            return prefab;
        }

        /// <summary>
        /// ?潁??용끏???????븐뻤??
        /// </summary>
        public static bool IsInitialized => _initialized && (PlayerVFX != null || MonsterVFX != null);

        #endregion

        #region ?筌?????VFX ??嚥??꿔꺂???熬곊삳튉??
        /// <summary>
        /// Valheim ????ㅼ굣亦?VFX??????????Β????嚥?(???쒙쭫??????썹땟??
        /// - ??影?れ쉬??? VFX: Destroy ?癲ル슢????O
        public static GameObject Play(string vfxName, Vector3 position, float duration = 3f)
        {
            if (string.IsNullOrEmpty(vfxName)) return null;

            if (!_isReceivingRPC && IsCustomVFX(vfxName) && ZRoutedRpc.instance != null)
            {
                try
                {
                    ZRoutedRpc.instance.InvokeRoutedRPC(
                        ZRoutedRpc.Everybody, RPC_PLAY_CUSTOM_VFX, vfxName, position, duration);
                }
                catch { }
            }

            try
            {
                GameObject prefab = null;
                bool isCustom = IsCustomVFX(vfxName);

                if (_cachedPrefabs.TryGetValue(vfxName, out prefab))
                {
                    if (prefab != null)
                        return InstantiateVFX(prefab, position, duration, vfxName);
                    _cachedPrefabs.Remove(vfxName);
                }

                if (isCustom)
                {
                    prefab = FindPrefabInResources(vfxName);
                    _cachedPrefabs[vfxName] = prefab;
                    if (prefab != null)
                        return InstantiateVFX(prefab, position, duration, vfxName);
                }
                else
                {
                    prefab = GetVanillaPrefab(vfxName);
                    if (prefab != null)
                        return InstantiateVFX(prefab, position, duration, vfxName);
                }

                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] Play({vfxName}) failed: {ex.Message}");
                return null;
            }
        }
            }
        }

        /// <summary>
        /// ????Transform??????산뭐?????紐낅뤁????影?れ쉬??? VFX ??嚥?(PlayOnPlayer ?熬곣뫖?삥납??
        /// parent??Instantiate ???꿔꺂?????녿쫯?????????VFX?????嶺????Β??????산뭐?????紐껎뜚
        /// </summary>
        public static GameObject PlayFollowing(string vfxName, Transform followTarget, Vector3 localOffset, float duration = 5f)
        {
            if (string.IsNullOrEmpty(vfxName) || followTarget == null) return null;

            try
            {
                GameObject prefab = null;

                if (_cachedPrefabs.TryGetValue(vfxName, out prefab))
                {
                    if (prefab == null) return null;
                }
                else
                {
                    prefab = FindPrefabInResources(vfxName);
                    _cachedPrefabs[vfxName] = prefab;
                    if (prefab == null) return null;
                }

                // followTarget?????낇뀘????????嶺????Β??????산뭐?????紐껎뜚
                var vfxObj = UnityEngine.Object.Instantiate(prefab, followTarget);
                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = localOffset;
                    UnityEngine.Object.Destroy(vfxObj, duration);

                    // ???뚭퐫??汝??吏??노??dim ?꿔꺂?????몃?????嶺?????쇨덫??
                    if (_vfxDimMapping.TryGetValue(vfxName, out float dimFactor))
                        ApplyVFXDim(vfxObj, dimFactor);
                }

                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayFollowing({vfxName}) ?????곌숯: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// VFX ?熬곣뫖利든뜏??????癲????醫딆┫???
        /// VFXDimmerBehaviour ?????????ㅻ쿋??????낇뀘????????繹먮굞??LateUpdate?????????쇨덫??
        /// (????ㅻ쿋????潁??용끏???????썹땟????????썹땟?????썹땟???꿔꺂??節뉖き???嶺뚮슣??굢??癲ル슢캉??먯젂??????쇨덫????⑤슢????
        /// </summary>
        public static void ApplyVFXDim(GameObject vfx, float factor)
        {
            if (vfx == null) return;
            var dimmer = vfx.AddComponent<VFXDimmerBehaviour>();
            dimmer.factor = factor;
        }

        /// <summary>
        /// Resources?????????썼キ?κ괌???꿔꺂????녺솒?뗫렓?(ZNetScene ???????궰???)
        /// WackyEpicMMOSystem ?熬곣뫖?삥납?? ????썼キ?κ괌??袁⑸즴筌?（鍮?????꾣뤃?饔낃퀣??(???癲ル슢???吏????ㅿ폍筌???嶺뚮????
        /// </summary>
        private static GameObject FindPrefabInResources(string prefabName)
        {
            try
            {
                foreach (var obj in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (obj != null && obj.name == prefabName)
                    {
                        // ????썼キ?κ괌??袁⑸즴筌?（鍮?????꾣뤃?饔낃퀣??(???癲ル슢???吏????ㅿ폍筌???嶺뚮????
                        // scene.name??null??????춦??rootCount??醫딆쓧? 0?????????썼キ?κ괌??                        if (obj.scene.name == null || obj.scene.rootCount == 0)
                        {
                            return obj;
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// ??影?れ쉬??? VFX?癲? ?癲ル슢캉????(Destroy ????썹땟????? ?嚥▲굧????
        /// ?熬곣뫖利든뜏類ｋ쭬??????뚯????VFX??Destroy ?癲ル슢????????????類ㅺ튃???汝??吏??뮤??熬곣뫖利든뜏類ｋ렱??
        /// </summary>
        private static bool IsCustomVFX(string vfxName)
        {
            return !string.IsNullOrEmpty(vfxName) && _customVFXNames.Contains(vfxName);
        }

        /// <summary>
        /// VFX Instantiate (???????궰????곗뒩泳???꿔꺂??節뉖き??
        /// - ??影?れ쉬??? VFX: Instantiate + Destroy
        /// - ?熬곣뫖利든뜏類ｋ쭬??????뚯????VFX: ??嶺???Instantiate (Destroy ????- ?熬곣뫖利든뜏類ｋ쭬?????썹땟?㈑????嶺??癲ル슢???뚭괌?
        /// </summary>
        private static GameObject InstantiateVFX(GameObject prefab, Vector3 position, float duration, string vfxName = "")
        {
            if (prefab == null) return null;

            var vfxObj = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
            if (vfxObj != null)
            {
                // ??影?れ쉬??? VFX??Destroy ?癲ル슢????
                if (!string.IsNullOrEmpty(vfxName) && IsCustomVFX(vfxName))
                {
                    UnityEngine.Object.Destroy(vfxObj, duration);
                }
                // ?熬곣뫖利든뜏類ｋ쭬??????뚯????VFX??Destroy ?癲ル슢????????(?熬곣뫖利든뜏類ｋ쭬?????썹땟?㈑????嶺??癲ル슢???뚭괌?

                // ???뚭퐫??汝??吏??노??dim ?꿔꺂?????몃?????嶺?????쇨덫??
                if (!string.IsNullOrEmpty(vfxName) &&
                    _vfxDimMapping.TryGetValue(vfxName, out float dimFactor))
                    ApplyVFXDim(vfxObj, dimFactor);
            }
            return vfxObj;
        }

        /// <summary>
        /// Valheim ????ㅼ굣亦?VFX??????????ㅿ폎??????낇뀘???(??????????산뭐?????紐껎뜚)
        /// - ??影?れ쉬??? VFX: Destroy ?癲ル슢????O
        /// - ?熬곣뫖利든뜏類ｋ쭬??????뚯????VFX: Destroy ?癲ル슢????X (???類ㅺ튃???汝??吏??뮤??熬곣뫖?삥납?)
        /// </summary>
        public static GameObject PlayOnPlayer(Player player, string vfxName, float duration = 5f, Vector3? localOffset = null)
        {
            if (player == null || string.IsNullOrEmpty(vfxName)) return null;

            if (!_isReceivingRPC && IsCustomVFX(vfxName) && ZRoutedRpc.instance != null)
            {
                try
                {
                    ZRoutedRpc.instance.InvokeRoutedRPC(
                        ZRoutedRpc.Everybody, RPC_PLAY_ON_PLAYER, vfxName, player.GetPlayerID(), duration);
                }
                catch { }
            }

            try
            {
                GameObject prefab = IsCustomVFX(vfxName) ? GetOrFindPrefab(vfxName) : GetVanillaPrefab(vfxName);
                if (prefab == null) return null;

                var vfxObj = UnityEngine.Object.Instantiate(prefab, player.transform);
                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = localOffset ?? new Vector3(0f, 1f, 0f);

                    if (IsCustomVFX(vfxName))
                        UnityEngine.Object.Destroy(vfxObj, duration);

                    if (_vfxDimMapping.TryGetValue(vfxName, out float dimFactor))
                        ApplyVFXDim(vfxObj, dimFactor);

                    return vfxObj;
                }

                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayOnPlayer({vfxName}) failed: {ex.Message}");
                return null;
            }
        }

        private static GameObject GetOrFindPrefab(string vfxName)
        {
            if (_cachedPrefabs.TryGetValue(vfxName, out var prefab))
                return prefab;

            prefab = FindPrefabInResources(vfxName);
            _cachedPrefabs[vfxName] = prefab;
            return prefab;
        }

        private static GameObject GetVanillaPrefab(string vfxName)
        {
            GameObject prefab = null;

            if (ZNetScene.instance != null)
                prefab = ZNetScene.instance.GetPrefab(vfxName);

            if (prefab == null)
                prefab = FindPrefabInResources(vfxName);

            if (prefab != null && !_cachedPrefabs.ContainsKey(vfxName))
                _cachedPrefabs[vfxName] = prefab;

            return prefab;
        }

        public static bool IsCustomVFXName(string vfxName)
        {
            return IsCustomVFX(vfxName);
        }

        public static GameObject PlayVanillaNetworked(string vfxName, Vector3 position, Quaternion rotation = default)
        {
            if (string.IsNullOrEmpty(vfxName)) return null;

            if (!_isReceivingRPC && ZRoutedRpc.instance != null)
            {
                try
                {
                    _recentLocalCreations[BuildPositionDedupKey(vfxName, position)] = Time.time;
                    ZRoutedRpc.instance.InvokeRoutedRPC(
                        ZRoutedRpc.Everybody,
                        RPC_PLAY_VANILLA_VFX,
                        vfxName,
                        position,
                        rotation.eulerAngles);
                }
                catch { }
            }

            return PlayVanillaLocal(vfxName, position, rotation);
        }

        public static GameObject PlayVanillaOnPlayerNetworked(Player player, string vfxName, Vector3? localOffset = null)
        {
            if (player == null || string.IsNullOrEmpty(vfxName)) return null;

            Vector3 offset = localOffset ?? new Vector3(0f, 1f, 0f);

            if (!_isReceivingRPC && ZRoutedRpc.instance != null)
            {
                try
                {
                    _recentLocalCreations[BuildPlayerDedupKey(vfxName, player.GetPlayerID())] = Time.time;
                    ZRoutedRpc.instance.InvokeRoutedRPC(
                        ZRoutedRpc.Everybody,
                        RPC_PLAY_VANILLA_ONPLAYER,
                        vfxName,
                        player.GetPlayerID(),
                        offset);
                }
                catch { }
            }

            return PlayVanillaOnPlayerLocal(player, vfxName, offset);
        }

        private static GameObject PlayVanillaLocal(string vfxName, Vector3 position, Quaternion rotation = default)
        {
            GameObject prefab = GetVanillaPrefab(vfxName);
            if (prefab == null) return null;

            Quaternion finalRotation = rotation == default ? Quaternion.identity : rotation;
            var vfxObj = UnityEngine.Object.Instantiate(prefab, position, finalRotation);
            if (vfxObj != null && _vfxDimMapping.TryGetValue(vfxName, out float dimFactor))
                ApplyVFXDim(vfxObj, dimFactor);

            return vfxObj;
        }

        private static GameObject PlayVanillaOnPlayerLocal(Player player, string vfxName, Vector3 localOffset)
        {
            GameObject prefab = GetVanillaPrefab(vfxName);
            if (prefab == null) return null;

            var vfxObj = UnityEngine.Object.Instantiate(prefab, player.transform);
            if (vfxObj != null)
            {
                vfxObj.transform.localPosition = localOffset;
                if (_vfxDimMapping.TryGetValue(vfxName, out float dimFactor))
                    ApplyVFXDim(vfxObj, dimFactor);
            }

            return vfxObj;
        }

        private static string BuildPlayerDedupKey(string vfxName, long playerID)
        {
            return $"{vfxName}_player_{playerID}";
        }

        private static string BuildPositionDedupKey(string vfxName, Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x * 10f);
            int y = Mathf.RoundToInt(position.y * 10f);
            int z = Mathf.RoundToInt(position.z * 10f);
            return $"{vfxName}_pos_{x}_{y}_{z}";
        }

        public static GameObject PlayOnTarget(Character target, string vfxName, float duration = 3f, Vector3? localOffset = null)
        {
            if (target == null || string.IsNullOrEmpty(vfxName)) return null;

            try
            {
                GameObject prefab = IsCustomVFX(vfxName) ? GetOrFindPrefab(vfxName) : GetVanillaPrefab(vfxName);
                if (prefab == null) return null;

                var vfxObj = UnityEngine.Object.Instantiate(prefab, target.transform);
                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = localOffset ?? new Vector3(0f, 1.5f, 0f);

                    if (IsCustomVFX(vfxName))
                        UnityEngine.Object.Destroy(vfxObj, duration);

                    if (_vfxDimMapping.TryGetValue(vfxName, out float dimFactor))
                        ApplyVFXDim(vfxObj, dimFactor);

                    return vfxObj;
                }

                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayOnTarget({vfxName}) failed: {ex.Message}");
                return null;
            }
        }

        public static void PlayWithSound(string vfxName, string sfxName, Vector3 position, float duration = 3f)
        {
            if (!string.IsNullOrEmpty(vfxName))
                Play(vfxName, position, duration);

            if (!string.IsNullOrEmpty(sfxName))
                Play(sfxName, position, duration);
        }

        public static GameObject PlayWithRotation(string vfxName, string sfxName, Vector3 position, Quaternion rotation, float duration = 3f)
        {
            GameObject result = null;

            if (!string.IsNullOrEmpty(vfxName))
            {
                try
                {
                    bool isCustom = IsCustomVFX(vfxName);
                    GameObject prefab = isCustom ? GetOrFindPrefab(vfxName) : GetVanillaPrefab(vfxName);

                    if (prefab != null)
                    {
                        result = UnityEngine.Object.Instantiate(prefab, position, rotation);
                        if (result != null)
                        {
                            if (isCustom)
                                UnityEngine.Object.Destroy(result, duration);

                            if (_vfxDimMapping.TryGetValue(vfxName, out float dimFactor))
                                ApplyVFXDim(result, dimFactor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log?.LogWarning($"[SimpleVFX] PlayWithRotation({vfxName}) failed: {ex.Message}");
                }
            }

            if (!string.IsNullOrEmpty(sfxName))
                Play(sfxName, position, duration);

            return result;
        }

        #endregion

        #region Network RPC Handlers

        internal static void OnReceiveCustomVFX(long sender, string vfxName, Vector3 pos, float duration)
        {
            _isReceivingRPC = true;
            try   { Play(vfxName, pos, duration); }
            finally { _isReceivingRPC = false; }
        }

        internal static void OnReceiveVanillaVFX(long sender, string vfxName, Vector3 pos, Vector3 eulerAngles)
        {
            string dedupKey = BuildPositionDedupKey(vfxName, pos);
            if (_recentLocalCreations.TryGetValue(dedupKey, out float t) && Time.time - t < 1f)
            {
                _recentLocalCreations.Remove(dedupKey);
                return;
            }

            _isReceivingRPC = true;
            try   { PlayVanillaLocal(vfxName, pos, Quaternion.Euler(eulerAngles)); }
            finally { _isReceivingRPC = false; }
        }

        internal static void OnReceivePlayOnPlayer(long sender, string vfxName, long playerID, float duration)
        {
            string dedupKey = BuildPlayerDedupKey(vfxName, playerID);
            if (_recentLocalCreations.TryGetValue(dedupKey, out float t) && Time.time - t < 1f)
            {
                _recentLocalCreations.Remove(dedupKey);
                return;
            }

            _isReceivingRPC = true;
            try
            {
                foreach (var p in Player.GetAllPlayers())
                {
                    if (p == null) continue;
                    if (p.GetPlayerID() == playerID)
                    {
                        PlayOnPlayer(p, vfxName, duration);
                        break;
                    }
                }
            }
            finally { _isReceivingRPC = false; }
        }

        internal static void OnReceiveVanillaOnPlayer(long sender, string vfxName, long playerID, Vector3 localOffset)
        {
            string dedupKey = BuildPlayerDedupKey(vfxName, playerID);
            if (_recentLocalCreations.TryGetValue(dedupKey, out float t) && Time.time - t < 1f)
            {
                _recentLocalCreations.Remove(dedupKey);
                return;
            }

            _isReceivingRPC = true;
            try
            {
                foreach (var p in Player.GetAllPlayers())
                {
                    if (p == null) continue;
                    if (p.GetPlayerID() == playerID)
                    {
                        PlayVanillaOnPlayerLocal(p, vfxName, localOffset);
                        break;
                    }
                }
            }
            finally { _isReceivingRPC = false; }
        }

        #endregion
    }
    #region Harmony Patch

    [HarmonyPatch(typeof(ZNetScene), "Awake")]
    public static class SimpleVFX_ZNetScene_Awake_Patch
    {
        static void Postfix(ZNetScene __instance)
        {
            try
            {
                SimpleVFX.Initialize();
                CaptainSkillTree.Prefab.PrefabRegistry.RegisterToZNetScene();

                if (ZRoutedRpc.instance != null)
                {
                    try
                    {
                        ZRoutedRpc.instance.Register(SimpleVFX.RPC_PLAY_CUSTOM_VFX,
                            new Action<long, string, Vector3, float>(SimpleVFX.OnReceiveCustomVFX));
                    }
                    catch { }

                    try
                    {
                        ZRoutedRpc.instance.Register(SimpleVFX.RPC_PLAY_ON_PLAYER,
                            new Action<long, string, long, float>(SimpleVFX.OnReceivePlayOnPlayer));
                    }
                    catch { }

                    try
                    {
                        ZRoutedRpc.instance.Register(SimpleVFX.RPC_PLAY_VANILLA_VFX,
                            new Action<long, string, Vector3, Vector3>(SimpleVFX.OnReceiveVanillaVFX));
                    }
                    catch { }

                    try
                    {
                        ZRoutedRpc.instance.Register(SimpleVFX.RPC_PLAY_VANILLA_ONPLAYER,
                            new Action<long, string, long, Vector3>(SimpleVFX.OnReceiveVanillaOnPlayer));
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] ZNetScene patch failed: {ex.Message}");
            }
        }
    }

    #endregion

    /// <summary>
    /// VFX ?熬곣뫖利???앶얜뀛??Β???癲????醫딆┫????????????ㅻ쿋??
    /// ????ㅻ쿋????潁??용끏???????썹땟?????ш끽維??LateUpdate?????????????쇨덫???????嶺????곌퇈?뗦틦?
    /// - Material: _TintColor / _Color / _BaseColor (RGB + alpha ?꿔꺂??袁ㅻ븶?癲???factor)
    /// - Additive shader: alpha ???類ㅺ퉻????RGB ??醫딆┫????븐슦?? ?????
    /// - ParticleSystem: Stop/Clear ??startColor + emission ??醫딆┫?????Play
    /// - Light: intensity ??醫딆┫???
    /// </summary>
    public class VFXDimmerBehaviour : MonoBehaviour
    {
    public float factor = 0.5f;

    private void LateUpdate()
    {
        try { Apply(); }
        catch (Exception ex)
        {
            Plugin.Log?.LogWarning($"[VFXDimmer] ????쇨덫???????곌숯: {ex.Message}");
        }
        Destroy(this); // 1??????쇨덫?????????????ㅻ쿋?????곌퇈?뗦틦?
    }

    private void Apply()
    {
        Plugin.Log?.LogInfo($"[VFXDimmer] Apply START: {name}, factor={factor}");

        // 1. Material (multi-material ???? ????썹땟??????
        var colorProps = new[] { "_TintColor", "_Color", "_BaseColor" };
        var matList = new List<Material>();
        foreach (var r in GetComponentsInChildren<Renderer>(true))
        {
            if (r == null) continue;
            r.GetMaterials(matList);
            foreach (var mat in matList)
            {
                if (mat == null) continue;
                foreach (var prop in colorProps)
                {
                    if (mat.HasProperty(prop))
                    {
                        Color c = mat.GetColor(prop);
                        c.r *= factor; c.g *= factor; c.b *= factor; c.a *= factor;
                        mat.SetColor(prop, c);
                        break;
                    }
                }
                if (mat.HasProperty("_EmissionColor"))
                    mat.SetColor("_EmissionColor", mat.GetColor("_EmissionColor") * factor);
            }
        }

        // 2. Light
        foreach (var lt in GetComponentsInChildren<Light>(true))
            if (lt != null) lt.intensity *= factor;

        // 3. ParticleSystem
        var allPS = GetComponentsInChildren<ParticleSystem>(true);
        Plugin.Log?.LogInfo($"[VFXDimmer] PS count={allPS.Length}");
        if (allPS.Length == 0) return;

        // 3a. ??醫딆┻???Stop
        foreach (var ps in allPS)
            if (ps != null) ps.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        foreach (var ps in allPS)
        {
            if (ps == null) continue;
            var main = ps.main;

            // startColor RGB?뉗떜竊??푦ha ??醫딆┫???
            Color sc = main.startColor.color;
            sc.r *= factor; sc.g *= factor; sc.b *= factor; sc.a *= factor;
            main.startColor = new ParticleSystem.MinMaxGradient(sc);

            // ????ㅻ쿋??????????ㅻ쿋繹??(?癲ル슢캉??먯젂??????????⑤슢堉???
            main.startSizeMultiplier *= factor;

            // colorOverLifetime ??gradient key colors ??醫딆┫???(startColor?????????살퓢???꿔꺂??袁ㅻ븶???????
            var col = ps.colorOverLifetime;
            if (col.enabled)
            {
                var grad = col.color;
                if (grad.mode == ParticleSystemGradientMode.Gradient ||
                    grad.mode == ParticleSystemGradientMode.TwoGradients)
                {
                    var g = grad.gradient;
                    var keys = g.colorKeys;
                    for (int i = 0; i < keys.Length; i++)
                    {
                        var k = keys[i];
                        k.color = new Color(k.color.r * factor, k.color.g * factor,
                                            k.color.b * factor, k.color.a);
                        keys[i] = k;
                    }
                    g.colorKeys = keys;
                    col.color = new ParticleSystem.MinMaxGradient(g);
                }
            }

            // ??Emission rate ????볥궚?????곌퇈?뗦틦?
            // rateOverTime.mode??醫딆쓧? Curve/TwoConstants????.constant=0 ????????
            // ????ㅻ쿋???????썹땟????????筌??????熬곣뫖利든뜏類ｋ렱??????濚밸Þ?볟퐲?믫닧??????醫딆┫??????뼱??嶺뚮ㅏ諭???熬곣뫖利???앶???됰슦??????롪퍓梨띄댚??
        }

        // 3b. ??醫딆┻???Play
        foreach (var ps in allPS)
            if (ps != null) ps.Play(false);

        Plugin.Log?.LogInfo($"[VFXDimmer] Apply DONE: {name}");
    }
    } // class VFXDimmerBehaviour
} // namespace CaptainSkillTree

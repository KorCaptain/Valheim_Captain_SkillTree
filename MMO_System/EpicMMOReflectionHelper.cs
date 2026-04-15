using System;
using System.Reflection;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// EpicMMOSystem 리플렉션 헬퍼
    /// 직접 참조 없이 런타임에 EpicMMOSystem 존재 여부를 확인하고 호출
    /// EpicMMOSystem이 없어도 빌드/실행 가능
    /// </summary>
    public static class EpicMMOReflectionHelper
    {
        #region === Cached Reflection Info ===

        private static Type _levelSystemType;
        private static PropertyInfo _instanceProperty;
        private static MethodInfo _getLevelMethod;
        private static MethodInfo _addExpMethod;
        private static MethodInfo _getCurrentExpMethod;
        private static MethodInfo _getNeedExpMethod;
        private static MethodInfo _getTotalExpMethod;

        private static Type _apiType;
        private static MethodInfo _apiGetLevelMethod;
        private static MethodInfo _apiAddExpMethod;

        #endregion

        #region === Properties ===

        /// <summary>
        /// EpicMMOSystem 어셈블리가 로드되어 있는지 여부
        /// </summary>
        public static bool IsAvailable { get; private set; } = false;

        /// <summary>
        /// 초기화 완료 여부
        /// </summary>
        public static bool IsInitialized { get; private set; } = false;

        #endregion

        #region === Initialize ===

        /// <summary>
        /// 리플렉션 정보 초기화 - 한 번만 호출
        /// </summary>
        public static void Initialize()
        {
            if (IsInitialized) return;

            try
            {
                // LevelSystem 타입 찾기
                _levelSystemType = Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");

                if (_levelSystemType == null)
                {
                    Plugin.Log?.LogDebug("[EpicMMOReflectionHelper] EpicMMOSystem 어셈블리 없음 - 독립 모드");
                    IsAvailable = false;
                    IsInitialized = true;
                    return;
                }

                // Instance 프로퍼티
                _instanceProperty = _levelSystemType.GetProperty("Instance",
                    BindingFlags.Public | BindingFlags.Static);

                if (_instanceProperty == null)
                {
                    Plugin.Log?.LogWarning("[EpicMMOReflectionHelper] LevelSystem.Instance 프로퍼티 없음");
                    IsAvailable = false;
                    IsInitialized = true;
                    return;
                }

                // 메서드 캐싱
                _getLevelMethod = _levelSystemType.GetMethod("getLevel",
                    BindingFlags.Public | BindingFlags.Instance);
                _addExpMethod = _levelSystemType.GetMethod("AddExp",
                    BindingFlags.Public | BindingFlags.Instance,
                    null, new Type[] { typeof(int) }, null);
                _getCurrentExpMethod = _levelSystemType.GetMethod("getCurrentExp",
                    BindingFlags.Public | BindingFlags.Instance);
                _getNeedExpMethod = _levelSystemType.GetMethod("getNeedExp",
                    BindingFlags.Public | BindingFlags.Instance);
                _getTotalExpMethod = _levelSystemType.GetMethod("getTotalExp",
                    BindingFlags.Public | BindingFlags.Instance);

                // API 타입 (EpicMMOSystem_API)
                _apiType = Type.GetType("EpicMMOSystem.API.EpicMMOSystem_API, EpicMMOSystem");
                if (_apiType != null)
                {
                    _apiGetLevelMethod = _apiType.GetMethod("GetLevel",
                        BindingFlags.Public | BindingFlags.Static);
                    _apiAddExpMethod = _apiType.GetMethod("AddExp",
                        BindingFlags.Public | BindingFlags.Static,
                        null, new Type[] { typeof(int) }, null);
                }

                IsAvailable = _getLevelMethod != null;
                IsInitialized = true;

                if (IsAvailable)
                {
                    Plugin.Log?.LogDebug("[EpicMMOReflectionHelper] EpicMMOSystem 감지됨 - 연동 모드");
                }
                else
                {
                    Plugin.Log?.LogWarning("[EpicMMOReflectionHelper] EpicMMOSystem 메서드 누락 - 독립 모드");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[EpicMMOReflectionHelper] 초기화 실패: {ex.Message}");
                IsAvailable = false;
                IsInitialized = true;
            }
        }

        #endregion

        #region === Instance Access ===

        /// <summary>
        /// LevelSystem.Instance 가져오기 (null 가능)
        /// </summary>
        private static object GetInstance()
        {
            if (!IsAvailable || _instanceProperty == null) return null;

            try
            {
                return _instanceProperty.GetValue(null);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// LevelSystem.Instance가 존재하는지 확인
        /// </summary>
        public static bool HasInstance()
        {
            return GetInstance() != null;
        }

        #endregion

        #region === LevelSystem Methods ===

        /// <summary>
        /// 현재 레벨 가져오기
        /// </summary>
        public static int GetLevel()
        {
            if (!IsAvailable) return 1;

            try
            {
                var instance = GetInstance();
                if (instance == null) return 1;

                var result = _getLevelMethod?.Invoke(instance, null);
                return result != null ? (int)result : 1;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogDebug($"[EpicMMOReflectionHelper] GetLevel 실패: {ex.Message}");
                return 1;
            }
        }

        /// <summary>
        /// 경험치 추가
        /// </summary>
        public static void AddExp(int exp)
        {
            if (!IsAvailable || exp <= 0) return;

            try
            {
                var instance = GetInstance();
                if (instance == null) return;

                _addExpMethod?.Invoke(instance, new object[] { exp });
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogDebug($"[EpicMMOReflectionHelper] AddExp 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 경험치 가져오기
        /// </summary>
        public static long GetCurrentExp()
        {
            if (!IsAvailable) return 0;

            try
            {
                var instance = GetInstance();
                if (instance == null) return 0;

                var result = _getCurrentExpMethod?.Invoke(instance, null);
                return result != null ? (long)result : 0;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogDebug($"[EpicMMOReflectionHelper] GetCurrentExp 실패: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 다음 레벨까지 필요한 경험치
        /// </summary>
        public static long GetNeedExp()
        {
            if (!IsAvailable) return 0;

            try
            {
                var instance = GetInstance();
                if (instance == null) return 0;

                var result = _getNeedExpMethod?.Invoke(instance, null);
                return result != null ? (long)result : 0;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogDebug($"[EpicMMOReflectionHelper] GetNeedExp 실패: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 총 경험치 가져오기
        /// </summary>
        public static long GetTotalExp()
        {
            if (!IsAvailable) return 0;

            try
            {
                var instance = GetInstance();
                if (instance == null) return 0;

                var result = _getTotalExpMethod?.Invoke(instance, null);
                return result != null ? (long)result : 0;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogDebug($"[EpicMMOReflectionHelper] GetTotalExp 실패: {ex.Message}");
                return 0;
            }
        }

        #endregion

        #region === API Methods ===

        /// <summary>
        /// EpicMMOSystem_API.GetLevel() 호출
        /// </summary>
        public static int API_GetLevel()
        {
            if (_apiGetLevelMethod == null) return GetLevel();

            try
            {
                var result = _apiGetLevelMethod.Invoke(null, null);
                return result != null ? (int)result : 1;
            }
            catch
            {
                return GetLevel();
            }
        }

        /// <summary>
        /// EpicMMOSystem_API.AddExp() 호출
        /// </summary>
        public static void API_AddExp(int exp)
        {
            if (_apiAddExpMethod == null)
            {
                AddExp(exp);
                return;
            }

            try
            {
                _apiAddExpMethod.Invoke(null, new object[] { exp });
            }
            catch
            {
                AddExp(exp);
            }
        }

        #endregion

        #region === Type Access (for Harmony patches) ===

        /// <summary>
        /// EpicMMOSystem.LevelSystem 타입 가져오기 (Harmony 패치용)
        /// </summary>
        public static Type GetLevelSystemType()
        {
            if (!IsInitialized) Initialize();
            return _levelSystemType;
        }

        /// <summary>
        /// getParameter 메서드 가져오기 (Harmony 패치용)
        /// </summary>
        public static MethodInfo GetParameterMethod()
        {
            if (!IsAvailable) return null;
            return _levelSystemType?.GetMethod("getParameter",
                BindingFlags.Public | BindingFlags.Instance);
        }

        #endregion
    }
}

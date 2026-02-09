using System;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// Captain Level System
    /// 자체 레벨/경험치 관리 시스템
    /// EpicMMO가 없을 때 자동 활성화
    /// </summary>
    public class CaptainLevelSystem
    {
        #region === Singleton ===

        private static CaptainLevelSystem _instance;
        public static CaptainLevelSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CaptainLevelSystem();
                }
                return _instance;
            }
        }

        #endregion

        #region === Properties ===

        /// <summary>
        /// 현재 레벨
        /// </summary>
        public int Level { get; private set; } = 1;

        /// <summary>
        /// 현재 레벨에서의 경험치
        /// </summary>
        public long CurrentExp { get; private set; } = 0;

        /// <summary>
        /// 총 누적 경험치
        /// </summary>
        public long TotalExp { get; private set; } = 0;

        /// <summary>
        /// 초기화 여부
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        #endregion

        #region === Events ===

        /// <summary>
        /// 레벨업 이벤트
        /// </summary>
        public event Action<int> OnLevelUp;

        /// <summary>
        /// 경험치 획득 이벤트 (획득량, 실제 획득량)
        /// </summary>
        public event Action<int, long> OnExpGain;

        #endregion

        #region === Save Keys ===

        // Player.m_knownTexts 또는 m_customData에 저장하는 키
        private const string KEY_LEVEL = "CaptainSkillTree_Level";
        private const string KEY_CURRENT_EXP = "CaptainSkillTree_CurrentExp";
        private const string KEY_TOTAL_EXP = "CaptainSkillTree_TotalExp";

        #endregion

        #region === Initialize ===

        private CaptainLevelSystem() { }

        /// <summary>
        /// 시스템 초기화
        /// </summary>
        public void Initialize()
        {
            if (IsInitialized)
            {
                Plugin.Log.LogDebug("[CaptainLevelSystem] 이미 초기화됨");
                return;
            }

            // 경험치 테이블 초기화
            CaptainExpTable.FillLevelsExp();

            // 몬스터 경험치 데이터 로드
            CaptainMonsterExp.Initialize();

            IsInitialized = true;
            Plugin.Log.LogDebug("[CaptainLevelSystem] 자체 레벨 시스템 초기화 완료");
        }

        /// <summary>
        /// 시스템 종료
        /// </summary>
        public void Shutdown()
        {
            Save();
            IsInitialized = false;
            Plugin.Log.LogDebug("[CaptainLevelSystem] 자체 레벨 시스템 종료");
        }

        #endregion

        #region === Experience ===

        /// <summary>
        /// 경험치 추가 (EpicMMO AddExp와 동일한 로직)
        /// </summary>
        /// <param name="exp">기본 경험치</param>
        public void AddExp(int exp)
        {
            if (!IsInitialized || !CaptainLevelConfig.EnableCaptainLevel.Value)
            {
                return;
            }

            if (exp <= 0) return;

            // 최대 레벨 체크
            if (Level >= CaptainLevelConfig.MaxLevel.Value)
            {
                Plugin.Log.LogDebug("[CaptainLevelSystem] 최대 레벨 도달 - 경험치 획득 무시");
                return;
            }

            // 경험치 배율 적용
            float rate = CaptainLevelConfig.RateExp.Value;
            long giveExp = (long)(exp * rate);

            CurrentExp += giveExp;
            TotalExp += giveExp;

            // 레벨업 체크
            int addLvl = 0;
            long need = CaptainExpTable.GetExpForLevel(Level + 1);

            while (CurrentExp >= need && Level + addLvl < CaptainLevelConfig.MaxLevel.Value)
            {
                CurrentExp -= need;
                addLvl++;
                need = CaptainExpTable.GetExpForLevel(Level + addLvl + 1);
            }

            if (addLvl > 0)
            {
                Level += addLvl;
                OnLevelUp?.Invoke(Level);
                ShowLevelUpEffect();
                Plugin.Log.LogInfo($"[CaptainLevelSystem] 레벨업! Lv.{Level - addLvl} -> Lv.{Level}");
            }

            OnExpGain?.Invoke(exp, giveExp);

            // 경험치 팝업 표시
            if (CaptainLevelConfig.ShowExpPopup.Value)
            {
                ShowExpPopup(giveExp);
            }

            Save();
        }

        /// <summary>
        /// 레벨 직접 설정 (관리자용)
        /// </summary>
        public void SetLevel(int level)
        {
            level = Mathf.Clamp(level, 1, CaptainLevelConfig.MaxLevel.Value);
            int oldLevel = Level;
            Level = level;
            CurrentExp = 0;
            TotalExp = CaptainExpTable.GetTotalExpForLevel(level);

            if (level > oldLevel)
            {
                OnLevelUp?.Invoke(Level);
            }

            Save();
            Plugin.Log.LogInfo($"[CaptainLevelSystem] 레벨 설정: {oldLevel} -> {level}");
        }

        /// <summary>
        /// 경험치 직접 설정 (관리자용)
        /// </summary>
        public void SetExp(long exp)
        {
            TotalExp = Math.Max(0, exp);
            Level = CaptainExpTable.GetLevelFromTotalExp(TotalExp);
            CurrentExp = TotalExp - CaptainExpTable.GetTotalExpForLevel(Level);

            Save();
            Plugin.Log.LogInfo($"[CaptainLevelSystem] 경험치 설정: {exp} (Lv.{Level})");
        }

        /// <summary>
        /// 현재 레벨에서 다음 레벨까지 필요한 경험치
        /// </summary>
        public long GetExpToNextLevel()
        {
            return CaptainExpTable.GetExpForLevel(Level + 1);
        }

        /// <summary>
        /// 레벨업 진행률 (0.0 ~ 1.0)
        /// </summary>
        public float GetLevelProgress()
        {
            return CaptainExpTable.GetLevelProgress(Level, CurrentExp);
        }

        #endregion

        #region === Skill Points ===

        /// <summary>
        /// 현재 레벨 기반 스킬 포인트 계산
        /// </summary>
        public int GetSkillPointsFromLevel()
        {
            return Level * CaptainLevelConfig.SkillPointsPerLevel.Value;
        }

        #endregion

        #region === 스킬포인트 기반 레벨 계산 ===

        /// <summary>
        /// 스킬포인트 기반 레벨 계산
        /// 사용한 스킬 포인트를 기준으로 레벨을 역산
        /// 공식: 레벨 = 사용포인트 / 레벨당필요포인트
        /// 예: 130포인트 / 2 = 65레벨
        /// </summary>
        /// <returns>계산된 레벨 (최소 1)</returns>
        public int GetLevelFromSkillPoints()
        {
            int usedPoints = GetTotalUsedSkillPoints();
            int pointsPerLevel = CaptainLevelConfig.PointsRequiredPerLevel?.Value ?? 2;

            if (pointsPerLevel <= 0) pointsPerLevel = 2;

            // 스킬 포인트가 0이면 레벨 1 반환
            if (usedPoints <= 0) return 1;

            int calculatedLevel = usedPoints / pointsPerLevel;

            // 최소 레벨 1, 최대 레벨 MaxLevel
            int finalLevel = Mathf.Clamp(calculatedLevel, 1, CaptainLevelConfig.MaxLevel.Value);

            Plugin.Log.LogDebug($"[CaptainLevelSystem] GetLevelFromSkillPoints - 사용포인트: {usedPoints}, 레벨당: {pointsPerLevel}, 계산결과: {calculatedLevel}, 최종: {finalLevel}");

            return finalLevel;
        }

        /// <summary>
        /// 총 사용된 스킬 포인트 반환
        /// SkillTreeManager에서 가져옴
        /// </summary>
        public int GetTotalUsedSkillPoints()
        {
            try
            {
                var manager = SkillTree.SkillTreeManager.Instance;
                if (manager != null)
                {
                    return manager.GetTotalUsedPoints();
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[CaptainLevelSystem] 스킬 포인트 조회 실패: {ex.Message}");
            }
            return 0;
        }

        /// <summary>
        /// 스킬포인트 기반 레벨을 경험치 시스템에 동기화
        /// 레벨 차이가 있을 때만 동기화
        /// </summary>
        public void SyncLevelFromSkillPoints()
        {
            if (!CaptainLevelConfig.UseSkillPointBasedLevel?.Value ?? false)
            {
                return;
            }

            int skillBasedLevel = GetLevelFromSkillPoints();

            if (skillBasedLevel > Level)
            {
                int oldLevel = Level;
                Level = skillBasedLevel;
                TotalExp = CaptainExpTable.GetTotalExpForLevel(Level);
                CurrentExp = 0;

                Save();
                Plugin.Log.LogInfo($"[CaptainLevelSystem] 스킬포인트 기반 레벨 동기화: Lv.{oldLevel} -> Lv.{Level}");
            }
        }

        /// <summary>
        /// 스킬포인트 기반 레벨 정보 반환 (UI 표시용)
        /// </summary>
        public (int level, int usedPoints, int pointsPerLevel) GetSkillPointLevelInfo()
        {
            int usedPoints = GetTotalUsedSkillPoints();
            int pointsPerLevel = CaptainLevelConfig.PointsRequiredPerLevel?.Value ?? 2;
            int level = GetLevelFromSkillPoints();

            return (level, usedPoints, pointsPerLevel);
        }

        #endregion

        #region === Save/Load ===

        /// <summary>
        /// 플레이어 데이터에 저장
        /// </summary>
        public void Save()
        {
            var player = Player.m_localPlayer;
            if (player == null) return;

            try
            {
                player.m_customData[KEY_LEVEL] = Level.ToString();
                player.m_customData[KEY_CURRENT_EXP] = CurrentExp.ToString();
                player.m_customData[KEY_TOTAL_EXP] = TotalExp.ToString();

                Plugin.Log.LogDebug($"[CaptainLevelSystem] 저장 완료 - Lv.{Level}, Exp:{CurrentExp}/{GetExpToNextLevel()}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainLevelSystem] 저장 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어 데이터에서 로드
        /// </summary>
        public void Load()
        {
            var player = Player.m_localPlayer;
            if (player == null) return;

            try
            {
                // 레벨 로드
                if (player.m_customData.TryGetValue(KEY_LEVEL, out var lvlStr) &&
                    int.TryParse(lvlStr, out int lvl))
                {
                    Level = Mathf.Clamp(lvl, 1, CaptainLevelConfig.MaxLevel.Value);
                }
                else
                {
                    Level = 1;
                }

                // 현재 경험치 로드
                if (player.m_customData.TryGetValue(KEY_CURRENT_EXP, out var curStr) &&
                    long.TryParse(curStr, out long cur))
                {
                    CurrentExp = Math.Max(0, cur);
                }
                else
                {
                    CurrentExp = 0;
                }

                // 총 경험치 로드
                if (player.m_customData.TryGetValue(KEY_TOTAL_EXP, out var totalStr) &&
                    long.TryParse(totalStr, out long total))
                {
                    TotalExp = Math.Max(0, total);
                }
                else
                {
                    TotalExp = CaptainExpTable.GetTotalExpForLevel(Level) + CurrentExp;
                }

                Plugin.Log.LogInfo($"[CaptainLevelSystem] 로드 완료 - Lv.{Level}, Exp:{CurrentExp}/{GetExpToNextLevel()}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainLevelSystem] 로드 실패: {ex.Message}");
                Level = 1;
                CurrentExp = 0;
                TotalExp = 0;
            }
        }

        /// <summary>
        /// 데이터 초기화 (리셋)
        /// </summary>
        public void ResetData()
        {
            Level = 1;
            CurrentExp = 0;
            TotalExp = 0;
            Save();
            Plugin.Log.LogInfo("[CaptainLevelSystem] 데이터 초기화 완료");
        }

        #endregion

        #region === Visual Effects ===

        /// <summary>
        /// 레벨업 이펙트 표시
        /// </summary>
        private void ShowLevelUpEffect()
        {
            if (!CaptainLevelConfig.ShowLevelUpEffect.Value) return;

            var player = Player.m_localPlayer;
            if (player == null) return;

            try
            {
                // 레벨업 VFX
                var vfx = ZNetScene.instance?.GetPrefab("vfx_spawn");
                if (vfx != null)
                {
                    GameObject.Instantiate(vfx, player.transform.position, Quaternion.identity);
                }

                // 레벨업 SFX
                var sfx = ZNetScene.instance?.GetPrefab("sfx_ui_success");
                if (sfx != null)
                {
                    GameObject.Instantiate(sfx, player.transform.position, Quaternion.identity);
                }

                // 레벨업 텍스트
                SkillTree.SkillEffect.DrawFloatingText(player, $"LEVEL UP! Lv.{Level}", Color.yellow);

                // 화면 중앙 메시지
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center, $"<color=yellow>LEVEL UP!</color> Lv.{Level}");

                Plugin.Log.LogDebug("[CaptainLevelSystem] 레벨업 이펙트 표시");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainLevelSystem] 레벨업 이펙트 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 경험치 획득 팝업 표시
        /// </summary>
        private void ShowExpPopup(long exp)
        {
            var player = Player.m_localPlayer;
            if (player == null) return;

            try
            {
                SkillTree.SkillEffect.DrawFloatingText(player, $"+{exp:N0} EXP", new Color(0.5f, 1f, 0.5f));
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainLevelSystem] 경험치 팝업 오류: {ex.Message}");
            }
        }

        #endregion

        #region === Debug Commands ===

        /// <summary>
        /// 디버그 정보 출력
        /// </summary>
        public void LogStatus()
        {
            Plugin.Log.LogInfo("=== Captain Level System Status ===");
            Plugin.Log.LogInfo($"Level: {Level}");
            Plugin.Log.LogInfo($"Current Exp: {CurrentExp:N0}");
            Plugin.Log.LogInfo($"Exp to Next: {GetExpToNextLevel():N0}");
            Plugin.Log.LogInfo($"Progress: {GetLevelProgress() * 100:F1}%");
            Plugin.Log.LogInfo($"Total Exp: {TotalExp:N0}");
            Plugin.Log.LogInfo($"Skill Points: {GetSkillPointsFromLevel()}");
            Plugin.Log.LogInfo("===================================");
        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using BepInEx;

namespace CaptainSkillTree.Audio
{
    /// <summary>
    /// 스킬트리 전용 BGM 관리자 - Captain_audio 패턴 적용
    /// Skill_Tree_BGM.ogg 파일을 EmbeddedResource에서 로드하여 재생
    /// </summary>
    public class SkillTreeBGMManager : MonoBehaviour
    {
        #region Singleton
        public static SkillTreeBGMManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Plugin.Log.LogDebug("[BGM 관리자] SkillTreeBGMManager 초기화 완료");
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        #endregion

        #region BGM Settings
        private AudioSource bgmAudioSource;
        private AudioClip skillTreeBGM;
        private bool isBGMEnabled = true;
        private float bgmVolume = 0.3f;
        private bool isInitialized = false;
        private static readonly string BGM_RESOURCE_NAME = "CaptainSkillTree.asset.Resources.Skill_Tree_BGM";

        // 발헤임 음악 제어
        private float originalValheimVolume = 1.0f;

        // 일시정지/재개 기능
        private float pausedTime = 0f; // BGM 일시정지 시 재생 위치 저장
        #endregion

        #region Initialization
        /// <summary>
        /// BGM 시스템 초기화 - Plugin.cs에서 호출
        /// </summary>
        public void Initialize()
        {
            if (isInitialized) return;

            try
            {
                // AudioSource 컴포넌트 추가
                bgmAudioSource = gameObject.AddComponent<AudioSource>();
                bgmAudioSource.playOnAwake = false;
                bgmAudioSource.loop = true;
                bgmAudioSource.volume = bgmVolume;
                
                // BGM 로드 시작
                StartCoroutine(LoadSkillTreeBGM());
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[BGM 관리자] 초기화 실패: {ex.Message}");
            }
        }
        #endregion

        #region BGM Loading (Captain_audio 패턴 적용)
        /// <summary>
        /// EmbeddedResource에서 Skill_Tree_BGM.ogg 로드 (Captain_audio 방식)
        /// </summary>
        private IEnumerator LoadSkillTreeBGM()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            
            // BGM 관련 리소스만 검색 및 디버깅
            var resourceNames = assembly.GetManifestResourceNames();
            var bgmResources = resourceNames.Where(name => 
                name.Contains("Skill_Tree_BGM") || 
                name.Contains(".ogg") || 
                name.EndsWith(".ogg")).ToArray();
                
            if (bgmResources.Length > 0)
            {
                Plugin.Log.LogDebug($"[BGM 관리자] BGM 관련 리소스 발견:");
                foreach (var name in bgmResources)
                {
                    Plugin.Log.LogDebug($"[BGM 관리자] - {name}");
                }
            }
            else
            {
                Plugin.Log.LogWarning("[BGM 관리자] BGM 관련 리소스를 찾을 수 없습니다.");
            }
            
            // BGM 리소스 이름 찾기 (여러 패턴 시도)
            string actualBGMResourceName = null;
            var possibleNames = new string[]
            {
                BGM_RESOURCE_NAME, // CaptainSkillTree.asset.Resources.Skill_Tree_BGM
                "CaptainSkillTree.asset.Resources.Skill_Tree_BGM.ogg",
                "asset.Resources.Skill_Tree_BGM.ogg",
                "Skill_Tree_BGM.ogg"
            };
            
            foreach (var possibleName in possibleNames)
            {
                if (resourceNames.Contains(possibleName))
                {
                    actualBGMResourceName = possibleName;
                    Plugin.Log.LogDebug($"[BGM 관리자] BGM 리소스 발견: {actualBGMResourceName}");
                    break;
                }
            }

            // ✅ 플레이어 사망 체크 추가 (리소스 검색 후)
            if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
            {
                Plugin.Log.LogInfo("[BGM 관리자] 플레이어 사망으로 BGM 로딩 중단 (리소스 검색 후)");
                yield break;
            }

            // BGM 리소스를 찾지 못한 경우
            if (actualBGMResourceName == null)
            {
                // 부분 일치로 다시 검색
                actualBGMResourceName = resourceNames.FirstOrDefault(name => 
                    name.Contains("Skill_Tree_BGM") || name.EndsWith(".ogg"));
                    
                if (actualBGMResourceName == null)
                {
                    Plugin.Log.LogError($"[BGM 관리자] BGM 리소스를 찾을 수 없음");
                    yield break;
                }
            }
            
            // EmbeddedResource에서 스트림 가져오기
            byte[] audioData = null;
            
            using (Stream stream = assembly.GetManifestResourceStream(actualBGMResourceName))
            {
                if (stream == null)
                {
                    Plugin.Log.LogError($"[BGM 관리자] 스트림을 가져올 수 없음: {actualBGMResourceName}");
                    yield break;
                }

                try
                {
                    // 스트림을 바이트 배열로 변환
                    audioData = new byte[stream.Length];
                    stream.Read(audioData, 0, audioData.Length);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[BGM 관리자] 스트림 읽기 중 오류: {ex.Message}");
                    yield break;
                }
            }

            // ✅ 플레이어 사망 체크 추가 (스트림 읽기 후)
            if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
            {
                Plugin.Log.LogInfo("[BGM 관리자] 플레이어 사망으로 BGM 로딩 중단 (스트림 읽기 후)");
                yield break;
            }

            // 바이트에서 AudioClip 로드 (Captain_audio 패턴)
            yield return LoadAudioClipFromBytes(audioData, "Skill_Tree_BGM", (clip) =>
            {
                if (clip != null)
                {
                    skillTreeBGM = clip;
                    isInitialized = true;
                    Plugin.Log.LogInfo($"[BGM 관리자] BGM 로드 성공: {clip.name} ({clip.length:F1}초)");
                }
                else
                {
                    Plugin.Log.LogError("[BGM 관리자] BGM 로드 실패");
                }
            });
        }

        /// <summary>
        /// Captain_audio에서 사용하는 바이트에서 AudioClip 로드 방식
        /// </summary>
        private IEnumerator LoadAudioClipFromBytes(byte[] audioData, string clipName, System.Action<AudioClip> onComplete)
        {
            // 임시 파일 생성하여 UnityWebRequest로 로드 (Captain_audio 패턴)
            string tempPath = Path.Combine(Application.temporaryCachePath, $"temp_skillbgm_{clipName}_{System.Guid.NewGuid()}.ogg");

            try
            {
                // 임시 디렉토리 생성 및 파일 작성
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
                File.WriteAllBytes(tempPath, audioData);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[BGM 관리자] 임시 파일 생성 실패: {ex.Message}");
                onComplete?.Invoke(null);
                yield break;
            }

            // ✅ 플레이어 사망 체크 추가 (임시 파일 생성 후)
            if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
            {
                Plugin.Log.LogInfo("[BGM 관리자] 플레이어 사망으로 AudioClip 로딩 중단 (임시 파일 생성 후)");
                // 임시 파일 정리
                try
                {
                    if (File.Exists(tempPath))
                        File.Delete(tempPath);
                }
                catch { }
                onComplete?.Invoke(null);
                yield break;
            }

            // UnityWebRequest로 오디오 로드
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + tempPath.Replace("\\", "/"), AudioType.OGGVORBIS))
            {
                www.timeout = 10; // 10초 타임아웃
                yield return www.SendWebRequest();

                // ✅ 플레이어 사망 체크 추가 (UnityWebRequest 완료 후)
                if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
                {
                    Plugin.Log.LogInfo("[BGM 관리자] 플레이어 사망으로 AudioClip 로딩 중단 (UnityWebRequest 완료 후)");
                    // 임시 파일 정리
                    try
                    {
                        if (File.Exists(tempPath))
                            File.Delete(tempPath);
                    }
                    catch { }
                    onComplete?.Invoke(null);
                    yield break;
                }

                if (www.result == UnityWebRequest.Result.Success)
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    if (clip != null && clip.length > 0)
                    {
                        clip.name = clipName;
                        onComplete?.Invoke(clip);
                    }
                    else
                    {
                        Plugin.Log.LogError($"[BGM 관리자] AudioClip이 유효하지 않음: {clipName}");
                        onComplete?.Invoke(null);
                    }
                }
                else
                {
                    Plugin.Log.LogError($"[BGM 관리자] AudioClip 로드 실패: {www.error}");
                    onComplete?.Invoke(null);
                }
            }
            
            // 임시 파일 정리
            try
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[BGM 관리자] 임시 파일 정리 실패: {ex.Message}");
            }
        }
        #endregion

        #region BGM Control
        /// <summary>
        /// 스킬트리 BGM 재생 시작 - 아이콘 클릭 시 호출 (중복 재생 방지 + 일시정지 위치에서 재개)
        /// </summary>
        public void PlaySkillTreeBGM()
        {
            if (!isInitialized)
            {
                Plugin.Log.LogWarning("[BGM 관리자] BGM 매니저가 아직 초기화되지 않았습니다. 초기화를 시도합니다.");
                Initialize();
                return; // 초기화 후 다음 클릭 시 재생 시도
            }

            if (skillTreeBGM == null || bgmAudioSource == null)
            {
                Plugin.Log.LogWarning("[BGM 관리자] BGM이 로드되지 않았거나 AudioSource가 없습니다. 로딩을 다시 시도합니다.");
                // 코루틴으로 BGM 로딩 재시도
                StartCoroutine(LoadSkillTreeBGM());
                Plugin.Log.LogDebug("[BGM 관리자] BGM 로딩 재시도 중... 다음 클릭 시 재생됩니다.");
                return;
            }

            if (!isBGMEnabled)
            {
                Plugin.Log.LogDebug("[BGM 관리자] BGM이 비활성화되어 있습니다");
                return;
            }

            // **중복 재생 방지**: 이미 재생 중이면 다시 재생하지 않음
            if (bgmAudioSource.isPlaying)
            {
                Plugin.Log.LogDebug("[BGM 관리자] 스킬트리 BGM이 이미 재생 중입니다. 중복 재생을 방지합니다.");
                return;
            }

            try
            {
                // 발헤임 기본 음악 중지 (스킬트리 BGM 재생 전)
                PauseValheimMusic();

                bgmAudioSource.clip = skillTreeBGM;
                bgmAudioSource.volume = bgmVolume;

                // ✅ 일시정지 위치에서 재개
                if (pausedTime > 0)
                {
                    bgmAudioSource.time = pausedTime;
                    Plugin.Log.LogDebug($"[BGM 관리자] 일시정지 위치에서 재개: {pausedTime:F1}초");
                    pausedTime = 0f; // 초기화
                }

                bgmAudioSource.Play();

                Plugin.Log.LogDebug("[BGM 관리자] 스킬트리 BGM 재생 시작 (발헤임 음악 일시정지됨)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[BGM 관리자] BGM 재생 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 스킬트리 BGM 일시정지 - 스킬트리 UI 닫을 때 호출 (재생 위치 저장)
        /// </summary>
        public void PauseSkillTreeBGM()
        {
            try
            {
                Plugin.Log.LogDebug("[🎵 BGM] PauseSkillTreeBGM 호출됨 - 일시정지 시작");

                // ✅ 일시정지: 현재 재생 위치 저장
                if (bgmAudioSource != null)
                {
                    if (bgmAudioSource.isPlaying)
                    {
                        pausedTime = bgmAudioSource.time; // 현재 위치 저장
                        bgmAudioSource.Pause(); // Stop() 대신 Pause() 사용
                        Plugin.Log.LogDebug($"[BGM 관리자] 스킬트리 BGM 일시정지 (위치: {pausedTime:F1}초)");
                    }
                    else
                    {
                        Plugin.Log.LogDebug("[BGM 관리자] 스킬트리 BGM이 재생 중이 아님");
                    }

                    // ❌ clip = null 제거 (Unity 오디오 seek 에러 방지)
                    // ❌ enabled = false 제거 (재개 가능하도록 유지)
                }
                else
                {
                    Plugin.Log.LogWarning("[BGM 관리자] bgmAudioSource가 null입니다");
                }

                // 발헤임 기본 음악 복원 (스킬트리 BGM 일시정지 후)
                ResumeValheimMusic();
                Plugin.Log.LogDebug("[BGM 관리자] 발헤임 음악 복원 완료");
                Plugin.Log.LogDebug("[🎵 BGM] PauseSkillTreeBGM 완료 ✅");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[BGM 관리자] BGM 일시정지 중 오류: {ex.Message}");

                // 오류가 발생해도 발헤임 음악은 복원 시도
                try
                {
                    ResumeValheimMusic();
                    Plugin.Log.LogDebug("[BGM 관리자] 오류 발생 시에도 발헤임 음악 복원 시도");
                }
                catch (Exception resumeEx)
                {
                    Plugin.Log.LogError($"[BGM 관리자] 발헤임 음악 복원 중 오류: {resumeEx.Message}");
                }
            }
        }

        /// <summary>
        /// 스킬트리 BGM 중지 - 완전 정지가 필요할 때 호출 (OnDestroy에서 사용)
        /// </summary>
        private void StopSkillTreeBGM()
        {
            try
            {
                Plugin.Log.LogDebug("[🎵 BGM] StopSkillTreeBGM 호출됨 - 완전 정지 시작");

                if (bgmAudioSource != null)
                {
                    if (bgmAudioSource.isPlaying)
                    {
                        bgmAudioSource.Stop();
                        Plugin.Log.LogDebug("[BGM 관리자] 스킬트리 BGM 완전 정지");
                    }

                    // 완전 정리: clip 제거 및 비활성화
                    bgmAudioSource.clip = null;
                    bgmAudioSource.enabled = false;
                    Plugin.Log.LogDebug("[🎵 BGM] AudioSource 클립 제거 및 컴포넌트 비활성화");
                }

                pausedTime = 0f; // 일시정지 위치 초기화

                // 발헤임 기본 음악 복원
                ResumeValheimMusic();
                Plugin.Log.LogDebug("[🎵 BGM] StopSkillTreeBGM 완료 ✅");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[BGM 관리자] BGM 완전 정지 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// BGM On/Off 토글 (스킬트리 UI에서 호출)
        /// </summary>
        public void ToggleBGM()
        {
            isBGMEnabled = !isBGMEnabled;

            if (!isBGMEnabled)
            {
                // BGM 비활성화: 스킬트리 BGM 일시정지하고 발헤임 음악 복원
                PauseSkillTreeBGM();
                pausedTime = 0f; // 위치 초기화 (다음 활성화 시 처음부터)
                Plugin.Log.LogDebug("[BGM 관리자] BGM 비활성화 - 발헤임 음악 복원됨");
            }
            else
            {
                Plugin.Log.LogDebug("[BGM 관리자] BGM 활성화됨");
                // BGM 활성화: 스킬트리가 열려있다면 즉시 재생, 닫혀있다면 다음 열기까지 대기
                if (Plugin.IsSkillTreeOpen)
                {
                    Plugin.Log.LogDebug("[BGM 관리자] 스킬트리가 열려있으므로 BGM 즉시 재생");
                    PlaySkillTreeBGM();
                }
                else
                {
                    Plugin.Log.LogDebug("[BGM 관리자] 스킬트리가 닫혀있으므로 다음 열기까지 대기");
                    // 스킬트리가 닫혀있으면 발헤임 음악이 재생되어야 함
                    ResumeValheimMusic();
                }
            }

            Plugin.Log.LogInfo($"[BGM 관리자] BGM 토글: {(isBGMEnabled ? "활성화" : "비활성화")}");
        }

        /// <summary>
        /// BGM 볼륨 설정
        /// </summary>
        public void SetVolume(float volume)
        {
            bgmVolume = Mathf.Clamp01(volume);

            if (bgmAudioSource != null)
            {
                bgmAudioSource.volume = bgmVolume;
            }

            Plugin.Log.LogInfo($"[BGM 관리자] 볼륨 설정: {bgmVolume:F2}");
        }

        /// <summary>
        /// BGM을 처음부터 재생 (일시정지 위치 초기화)
        /// </summary>
        public void ResetBGM()
        {
            pausedTime = 0f;
            Plugin.Log.LogDebug("[BGM 관리자] BGM 재생 위치 초기화 - 다음 재생 시 처음부터 시작됩니다");
        }
        #endregion

        #region Valheim Music Control
        /// <summary>
        /// 발헤임 기본 음악 일시정지 (볼륨 0 + AudioSource.Pause() 이중 방식)
        /// </summary>
        private void PauseValheimMusic()
        {
            try
            {
                // MusicMan이 존재하는지 확인
                if (MusicMan.instance != null)
                {
                    // 1. 원본 볼륨 저장
                    originalValheimVolume = MusicMan.m_masterMusicVolume;

                    // 2. 볼륨을 0으로 설정하여 음소거
                    MusicMan.m_masterMusicVolume = 0f;

                    // 3. AudioSource 직접 Pause (Reflection 사용)
                    var musicSourceField = typeof(MusicMan).GetField("m_musicSource",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    if (musicSourceField != null)
                    {
                        var audioSource = musicSourceField.GetValue(MusicMan.instance) as AudioSource;
                        if (audioSource != null && audioSource.isPlaying)
                        {
                            audioSource.Pause();
                            Plugin.Log.LogDebug("[BGM 관리자] MusicMan AudioSource 일시정지 완료");
                        }
                    }

                    Plugin.Log.LogDebug("[BGM 관리자] 발헤임 음악 일시정지 완료 (볼륨 0 + AudioSource.Pause)");
                }
                else
                {
                    Plugin.Log.LogDebug("[BGM 관리자] MusicMan.instance가 아직 초기화되지 않음 (게임 로딩 중)");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[BGM 관리자] 발헤임 음악 일시정지 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 발헤임 기본 음악 복원 (AudioSource.UnPause() + 볼륨 복원 이중 방식)
        /// </summary>
        private void ResumeValheimMusic()
        {
            try
            {
                // MusicMan이 존재하는지 확인
                if (MusicMan.instance != null)
                {
                    // 1. AudioSource UnPause (Reflection 사용)
                    var musicSourceField = typeof(MusicMan).GetField("m_musicSource",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    if (musicSourceField != null)
                    {
                        var audioSource = musicSourceField.GetValue(MusicMan.instance) as AudioSource;
                        if (audioSource != null)
                        {
                            audioSource.UnPause();
                            Plugin.Log.LogDebug("[BGM 관리자] MusicMan AudioSource 재개 완료");
                        }
                    }

                    // 2. 볼륨 복원
                    MusicMan.m_masterMusicVolume = originalValheimVolume > 0 ? originalValheimVolume : 1.0f;

                    Plugin.Log.LogDebug($"[BGM 관리자] 발헤임 음악 복원 완료 (볼륨: {MusicMan.m_masterMusicVolume})");
                }
                else
                {
                    Plugin.Log.LogWarning("[BGM 관리자] MusicMan.instance가 null입니다 - 게임 로딩 중일 수 있음");
                }

                // 3. 상태 초기화 (항상 수행)
                originalValheimVolume = 1.0f;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[BGM 관리자] 발헤임 음악 복원 실패: {ex.Message}");

                // 예외 발생 시에도 최소한 볼륨은 복원 시도
                try
                {
                    if (MusicMan.instance != null)
                    {
                        MusicMan.m_masterMusicVolume = 1.0f;
                    }
                }
                catch { }

                originalValheimVolume = 1.0f;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// BGM 활성화 상태
        /// </summary>
        public bool IsBGMEnabled
        {
            get => isBGMEnabled;
            set
            {
                if (isBGMEnabled != value)
                {
                    isBGMEnabled = value;
                    if (!value)
                    {
                        StopSkillTreeBGM();
                    }
                }
            }
        }

        /// <summary>
        /// BGM 재생 상태
        /// </summary>
        public bool IsPlaying => bgmAudioSource != null && bgmAudioSource.isPlaying;

        /// <summary>
        /// BGM 로드 완료 상태
        /// </summary>
        public bool IsInitialized => isInitialized && skillTreeBGM != null;

        /// <summary>
        /// 현재 볼륨
        /// </summary>
        public float Volume
        {
            get => bgmVolume;
            set => SetVolume(value);
        }
        #endregion

        #region Cleanup
        void OnDestroy()
        {
            Plugin.Log.LogInfo("[BGM 관리자] OnDestroy - 모든 코루틴 정리");
            StopAllCoroutines(); // ✅ 모든 BGM 로딩 코루틴 강제 정리

            StopSkillTreeBGM(); // 이미 ResumeValheimMusic() 포함됨

            if (skillTreeBGM != null)
            {
                Destroy(skillTreeBGM);
            }

            if (Instance == this)
            {
                Instance = null;
                Plugin.Log.LogInfo("[BGM 관리자] Instance 해제됨");
            }

            Plugin.Log.LogDebug("[BGM 관리자] SkillTreeBGMManager 정리 완료 (발헤임 음악 복원됨)");
        }
        #endregion
    }

    // ⚠️ OnDeath 패치 비활성화 - 무한 로딩 문제 디버깅 중
    // BGM 정리는 Plugin.cs OnDeath에서 StopAllCoroutines로 처리됨
    // [HarmonyLib.HarmonyPatch(typeof(Player), "OnDeath")]
    // public static class Player_OnDeath_StopBGMCoroutines_Patch
    // {
    //     [HarmonyLib.HarmonyPostfix]
    //     public static void Postfix(Player __instance)
    //     {
    //         if (__instance == Player.m_localPlayer && SkillTreeBGMManager.Instance != null)
    //         {
    //             Plugin.Log.LogInfo("[플레이어 사망] SkillTreeBGMManager 모든 BGM 로딩 코루틴 강제 정리");
    //             SkillTreeBGMManager.Instance.StopAllCoroutines();
    //
    //             // BGM이 재생 중이었다면 일시정지 (재생 위치 저장)
    //             if (SkillTreeBGMManager.Instance.IsPlaying)
    //             {
    //                 SkillTreeBGMManager.Instance.PauseSkillTreeBGM();
    //             }
    //         }
    //     }
    // }
}
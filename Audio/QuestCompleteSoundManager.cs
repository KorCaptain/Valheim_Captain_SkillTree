using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace CaptainSkillTree.Audio
{
    /// <summary>
    /// 퀘스트 보상 수령 시 재생하는 전용 완료음(Quest Complete.ogg) 관리자.
    /// SkillTreeBGMManager와 동일한 EmbeddedResource → 임시 파일 → UnityWebRequest 로드 패턴을 쓰되,
    /// 루프 없는 1회성 재생이라 훨씬 단순하다. Plugin.cs(수정 금지)를 거치지 않도록 최초 접근 시
    /// 스스로 GameObject를 만드는 지연 초기화 싱글턴으로 구현한다.
    /// </summary>
    public class QuestCompleteSoundManager : MonoBehaviour
    {
        private static QuestCompleteSoundManager _instance;
        public static QuestCompleteSoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("QuestCompleteSoundManager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<QuestCompleteSoundManager>();
                }
                return _instance;
            }
        }

        private const string ResourceName = "CaptainSkillTree.asset.Resources.QuestComplete";

        private AudioSource oneShotSource;
        private AudioClip clip;
        private bool isLoading;

        /// <summary>완료음을 1회 재생한다. 최초 호출 시 로드 후 재생, 이후에는 캐시된 클립을 바로 재생한다.</summary>
        public void PlayOnce(float volume = 1f)
        {
            if (clip != null)
            {
                PlayClip(volume);
                return;
            }

            if (isLoading) return;
            isLoading = true;
            StartCoroutine(LoadAndPlay(volume));
        }

        private void PlayClip(float volume)
        {
            if (oneShotSource == null) oneShotSource = gameObject.AddComponent<AudioSource>();
            oneShotSource.PlayOneShot(clip, volume);
        }

        private IEnumerator LoadAndPlay(float volume)
        {
            byte[] audioData;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(ResourceName))
            {
                if (stream == null)
                {
                    Plugin.Log.LogWarning($"[QuestCompleteSound] 리소스를 찾을 수 없음: {ResourceName}");
                    isLoading = false;
                    yield break;
                }
                audioData = new byte[stream.Length];
                stream.Read(audioData, 0, audioData.Length);
            }

            string tempPath = Path.Combine(Application.temporaryCachePath, $"temp_questcomplete_{Guid.NewGuid()}.ogg");
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
                File.WriteAllBytes(tempPath, audioData);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestCompleteSound] 임시 파일 생성 실패: {ex.Message}");
                isLoading = false;
                yield break;
            }

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + tempPath.Replace("\\", "/"), AudioType.OGGVORBIS))
            {
                www.timeout = 10;
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    var loaded = DownloadHandlerAudioClip.GetContent(www);
                    if (loaded != null && loaded.length > 0)
                    {
                        loaded.name = "QuestComplete";
                        clip = loaded;
                        PlayClip(volume);
                    }
                    else
                    {
                        Plugin.Log.LogError("[QuestCompleteSound] AudioClip이 유효하지 않음");
                    }
                }
                else
                {
                    Plugin.Log.LogError($"[QuestCompleteSound] AudioClip 로드 실패: {www.error}");
                }
            }

            try { if (File.Exists(tempPath)) File.Delete(tempPath); } catch { }
            isLoading = false;
        }
    }
}

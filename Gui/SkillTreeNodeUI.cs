using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;

namespace CaptainSkillTree.Gui
{
    public class SkillTreeNodeUI : MonoBehaviour
    {
        public Dictionary<string, GameObject> nodeObjects = new Dictionary<string, GameObject>();
        public Dictionary<(string from, string to), Image> connectionLines = new Dictionary<(string, string), Image>();

        // job_icon 번들 우선, skill_node 번들 보조로 아이콘 로딩
        private Sprite TryLoadSprite(string iconName)
        {
            string unlockName = iconName;
            if (!iconName.EndsWith("_unlock"))
                unlockName = iconName.Replace("_lock", "_unlock").Replace("_locked", "_unlock");
            
            // 1. job_icon 번들에서 먼저 시도 (Job 아이콘들: Paladin, Tanker, Berserker, Archer, Mage, Rogue)
            var jobIconBundle = Plugin.GetJobIconBundle();
            if (jobIconBundle != null)
            {
                var sprite = jobIconBundle.LoadAsset<Sprite>(unlockName);
                if (sprite != null)
                {
                    return sprite;
                }
                
            }
            
            // 2. skill_node 번들에서 시도 (일반 스킬 아이콘들)
            var iconBundle = Plugin.GetIconAssetBundle();
            if (iconBundle != null)
            {
                var sprite = iconBundle.LoadAsset<Sprite>(unlockName);
                if (sprite != null)
                {
                    return sprite;
                }

                // 실패 시 all_skill_unlock 사용
                var fallback = iconBundle.LoadAsset<Sprite>("all_skill_unlock");
                if (fallback != null)
                {
                    return fallback;
                }
            }
            
            return null;
        }
        
        private static readonly HashSet<string> JobIconNames = new HashSet<string> { "Berserker", "Tanker", "Rogue", "Archer", "Mage", "mage", "Paladin", "paladin", "Paladin" };
        
        public bool IsJobIconName(string iconName)
        {
            // _unlock, _lock 접미사 제거
            if (iconName.EndsWith("_unlock") || iconName.EndsWith("_lock"))
            {
                iconName = iconName.Substring(0, iconName.LastIndexOf('_'));
            }
            
            return JobIconNames.Contains(iconName);
        }
        
        
        // 폴암 전문가 관련 노드인지 확인 (polearm_expert는 무기 노드로 취급하여 제외)
        private bool IsPolearmNode(string nodeId)
        {
            return nodeId.StartsWith("polearm") && nodeId != "polearm_expert";
        }
        
        // 생산 전문가 3단계 노드인지 확인
        private bool IsProductionLv3Node(string nodeId)
        {
            string[] productionLv3Nodes = { 
                "woodcutting_lv3", "gathering_lv3", "mining_lv3", "crafting_lv3" // farming→gathering, building→crafting
            };
            return Array.Exists(productionLv3Nodes, node => node == nodeId);
        }
        
        // 생산 전문가 1,2,4,5 단계 노드인지 확인
        private bool IsProductionOtherNode(string nodeId)
        {
            string[] productionOtherNodes = { 
                "novice_worker", // 1단계
                "woodcutting_lv2", "gathering_lv2", "mining_lv2", "crafting_lv2", // 2단계 (farming→gathering, building→crafting)
                "woodcutting_lv4", "gathering_lv4", "mining_lv4", "crafting_lv4", // 4단계 (farming→gathering, building→crafting)
                "crafting_specialist", // 5단계 (labor_specialist 삭제됨)
                "grandmaster_artisan" // 6단계
            };
            return Array.Exists(productionOtherNodes, node => node == nodeId);
        }
        

        public void GenerateSkillTreeNodesAndLines(GameObject skillTreePanel, Action<CaptainSkillTree.SkillTree.SkillNode, RectTransform> onNodeClick, Action<CaptainSkillTree.SkillTree.SkillNode, Vector2> onNodeHover, Action onNodeExit)
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            nodeObjects.Clear();
            connectionLines.Clear();
            var nodes = manager.SkillNodes;
            
            // 노드 존재 확인 (로그 정리됨)
            
            // 노드 등록 확인 (로그 정리됨)
            string[] weaponNodeIds = { "dagger", "sword", "mace", "spear", "polearm", "crossbow", "bow", "staff" };
            string[] rootNodeIds = { "attack_root", "melee_root", "ranged_root", "production_root", "speed_root", "defense_root" };
            
            
            foreach (var node in nodes.Values)
            {
                // 방어 전문가 노드 생성 (로그 정리됨)
                
                // 기존 오브젝트가 있으면 삭제하고 새로 생성 (로그 정리됨)
                var existingObj = skillTreePanel.transform.Find(node.Id);
                if (existingObj != null) {
                    UnityEngine.Object.DestroyImmediate(existingObj.gameObject);
                }
                
                var nodeObj = new GameObject(node.Id, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
                nodeObj.transform.SetParent(skillTreePanel.transform, false);
                var rect = nodeObj.GetComponent<RectTransform>();
                var img = nodeObj.GetComponent<Image>();
                var btn = nodeObj.GetComponent<Button>();
                rect.anchoredPosition = node.Position;
                
                // 노드 UI 생성 (로그 정리됨)
                

                // icon.mdc 규칙: 항상 _unlock 버전만 사용
                string iconName = node.IconName ?? node.Id;
                
                // 아이콘 로딩 (로그 정리됨)
                
                Sprite iconSprite = TryLoadSprite(iconName);
                bool isFallbackCommon = false;
                if (iconSprite == null) {
                    iconSprite = TryLoadSprite("all_skill_unlock");
                    isFallbackCommon = true;
                    Debug.LogWarning($"[SkillTreeNodeUI] 아이콘 스프라이트를 찾을 수 없음: {iconName} (노드: {node.Id}), all_skill_unlock로 대체");
                }
                img.sprite = iconSprite;
                
                // 아이콘 로딩 결과 (로그 정리됨)

                // 상태별 크기/투명도 적용 (초기엔 잠김)
                bool isWeapon = weaponNodeIds.Any(w => node.Id.Contains(w)) || node.Id.Contains("knife");
                bool isRoot = rootNodeIds.Contains(node.Id);
                bool isCommon = isFallbackCommon;
                bool isJobIcon = IsJobIconName(iconName);
                // 메이지와 Paladin는 강제로 직업 아이콘으로 처리 (인식 문제 해결)
                bool isJobIconOrForced = isJobIcon || node.Id == "Mage" || node.Id == "Paladin";
                
                // 직업 아이콘인지 확인하여 항상 최상위로 설정 (락/언락 관계없이)
                if (isJobIconOrForced)
                {
                    // 직업 아이콘은 생성 시점부터 최상위로 설정
                    nodeObj.transform.SetAsLastSibling();
                }
                
                // 노드 타입별 크기 설정 (localScale 방식 - CLAUDE.md 규칙 #10)
                // 1. 모든 노드 sizeDelta를 100x100으로 통일
                rect.sizeDelta = new Vector2(100, 100);

                // 2. localScale로 실제 크기 제어 (스프라이트 원본 크기 완전 무시)
                if (isJobIconOrForced)
                {
                    rect.localScale = new Vector3(0.85f, 0.85f, 1f); // 직업: 85x85
                }
                else if (isWeapon)
                {
                    rect.localScale = new Vector3(0.42f, 0.42f, 1f); // 무기 전문가: 42x42
                }
                else // 일반 노드 + 루트 노드
                {
                    rect.localScale = new Vector3(0.40f, 0.40f, 1f); // 일반+루트: 40x40
                }

                // 3. 픽셀 밀도 정규화
                img.pixelsPerUnitMultiplier = 1.0f;
                
                // 표준 투명도 적용 (모든 일반 노드 50% 투명)
                img.color = new Color(1f, 1f, 1f, 0.5f); // 표준 50% 투명도
                
                // Image를 raycastTarget으로 설정하여 이벤트를 받도록 함
                img.raycastTarget = true;
                
                // 백업 폴더와 동일하게 특별 처리 없음
                
                // 모든 노드에 Button 활성화 (통일된 방식)
                btn.interactable = true;
                btn.targetGraphic = img;

                // Button 클릭 이벤트 (모든 노드)
                btn.onClick.AddListener(() => {
                    onNodeClick(node, rect);
                });
                
                // 버튼과 EventTrigger 활성화 (로그 정리됨)
                
                // EventTrigger 추가 (호버용)
                var eventTrigger = nodeObj.AddComponent<UnityEngine.EventSystems.EventTrigger>();
                
                
                // PointerDown 이벤트 추가 (백업용 - Button이 실패할 경우를 위해 실제 클릭 동작도 수행)
                var pointerDown = new UnityEngine.EventSystems.EventTrigger.Entry { eventID = UnityEngine.EventSystems.EventTriggerType.PointerDown };
                pointerDown.callback.AddListener((data) => {
                    // EventTrigger 클릭 - PointerDown 호출됨 (백업용)
                    onNodeClick(node, rect); // 실제 클릭 동작 수행
                });
                eventTrigger.triggers.Add(pointerDown);
                
                var pointerEnter = new UnityEngine.EventSystems.EventTrigger.Entry { eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter };
                pointerEnter.callback.AddListener((data) => { 
                    onNodeHover(node, rect.position); 
                });
                eventTrigger.triggers.Add(pointerEnter);
                
                var pointerExit = new UnityEngine.EventSystems.EventTrigger.Entry { eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit };
                pointerExit.callback.AddListener((data) => { 
                    onNodeExit(); 
                });
                eventTrigger.triggers.Add(pointerExit);
                nodeObjects[node.Id] = nodeObj;
                
            }
            // 1단계: 연결선 생성 (배경 다음, 아이콘 전에)
            foreach (var node in nodes.Values)
            {
                if (node.Prerequisites != null && node.Prerequisites.Count > 0)
                {
                    foreach (var preId in node.Prerequisites)
                    {
                        if (nodes.ContainsKey(preId) && nodeObjects.ContainsKey(node.Id) && nodeObjects.ContainsKey(preId))
                        {
                            var line = CreateLine(skillTreePanel.GetComponent<RectTransform>(), nodeObjects[preId].GetComponent<RectTransform>(), nodeObjects[node.Id].GetComponent<RectTransform>());
                            connectionLines[(preId, node.Id)] = line;
                        }
                    }
                }
            }
            
            // 2단계: 노드 아이콘 렌더링 순서 설정
            foreach (var kvp in nodeObjects)
            {
                var nodeId = kvp.Key;
                var nodeObj = kvp.Value;
                var node = CaptainSkillTree.SkillTree.SkillTreeManager.Instance.SkillNodes[nodeId];
                
            }
            
            // 노드 상태별 UI 갱신
            RefreshNodeStates();
        }

        // 노드 상태별 크기/투명도 갱신
        public void RefreshNodeStates()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            string[] weaponNodeIds = { "dagger", "sword", "mace", "spear", "polearm", "crossbow", "bow", "staff" };
            string[] rootNodeIds = { "attack_root", "melee_root", "ranged_root", "production_root", "speed_root", "defense_root" };
            foreach (var node in manager.SkillNodes.Values)
            {
                if (nodeObjects.TryGetValue(node.Id, out var nodeObj))
                {
                    var img = nodeObj.GetComponent<Image>();
                    var rect = nodeObj.GetComponent<RectTransform>();
                    int level = manager.GetSkillLevel(node.Id);
                    int pendingLevel = manager.pendingInvestments.ContainsKey(node.Id) ? manager.pendingInvestments[node.Id] : 0;
                    bool isUnlocked = level > 0 || pendingLevel > 0;
                    bool isWeapon = weaponNodeIds.Any(w => node.Id.Contains(w)) || node.Id.Contains("knife");
                    bool isRoot = rootNodeIds.Contains(node.Id);
                    bool isCommon = img.sprite != null && img.sprite.name == "all_skill_unlock";
                    bool isJobIcon = IsJobIconName(node.IconName ?? node.Id);
                    // 메이지와 Paladin는 강제로 직업 아이콘으로 처리 (인식 문제 해결)
                    bool isJobIconOrForced = isJobIcon || node.Id == "Mage" || node.Id == "Paladin";
                    
                    // 메이지와 Paladin 특별 디버깅
                    if (isUnlocked)
                    {
                        // 해제 상태 크기 설정 (localScale 방식 - CLAUDE.md 규칙 #10)
                        rect.sizeDelta = new Vector2(100, 100); // 모든 노드 통일

                        if (isJobIconOrForced)
                        {
                            rect.localScale = new Vector3(1.05f, 1.05f, 1f); // 직업: 105x105
                        }
                        else if (isWeapon)
                        {
                            rect.localScale = new Vector3(0.50f, 0.50f, 1f); // 무기 전문가: 50x50
                        }
                        else // 일반 노드 + 루트 노드
                        {
                            rect.localScale = new Vector3(0.52f, 0.52f, 1f); // 일반+루트: 52x52
                        }

                        img.pixelsPerUnitMultiplier = 1.0f;
                        
                        // 표준 해제 투명도 설정 (100% 불투명)
                        img.color = new Color(1f, 1f, 1f, 1f);
                        
                        // 모든 언락된 아이콘은 노드선 위에 렌더링
                        nodeObj.transform.SetAsLastSibling();
                        
                        // 직업 아이콘의 Canvas 제거 (클릭 시 배경 뒤로 사라지는 문제 방지)
                        if (isJobIconOrForced)
                        {
                            var canvas = nodeObj.GetComponent<Canvas>();
                            if (canvas != null)
                            {
                                UnityEngine.Object.Destroy(canvas);
                                var raycaster = nodeObj.GetComponent<UnityEngine.UI.GraphicRaycaster>();
                                if (raycaster != null) UnityEngine.Object.Destroy(raycaster);
                            }
                        }
                        
                    }
                    else
                    {
                        // 잠김 상태 크기 설정 (localScale 방식 - CLAUDE.md 규칙 #10)
                        rect.sizeDelta = new Vector2(100, 100); // 모든 노드 통일

                        if (isJobIconOrForced)
                        {
                            rect.localScale = new Vector3(0.85f, 0.85f, 1f); // 직업: 85x85
                        }
                        else if (isWeapon)
                        {
                            rect.localScale = new Vector3(0.42f, 0.42f, 1f); // 무기 전문가: 42x42
                        }
                        else // 일반 노드 + 루트 노드
                        {
                            rect.localScale = new Vector3(0.40f, 0.40f, 1f); // 일반+루트: 40x40
                        }

                        img.pixelsPerUnitMultiplier = 1.0f;
                        
                        // 표준 잠김 투명도 설정 (50% 투명)
                        img.color = new Color(1f, 1f, 1f, 0.5f);
                        
                        // 잠김 상태의 직업 아이콘도 Canvas 제거 확인
                        if (isJobIconOrForced)
                        {
                            var canvas = nodeObj.GetComponent<Canvas>();
                            if (canvas != null)
                            {
                                UnityEngine.Object.Destroy(canvas);
                                var raycaster = nodeObj.GetComponent<UnityEngine.UI.GraphicRaycaster>();
                                if (raycaster != null) UnityEngine.Object.Destroy(raycaster);
                            }
                        }
                    }
                }
            }
            
            // 모든 상태 갱신 후 직업 아이콘들을 다시 최상위로 설정 (클릭 후 배경 뒤로 사라지는 문제 방지)
            EnsureJobIconsOnTop();
        }
        
        /// <summary>
        /// 모든 직업 아이콘들(락/언락 관계없이)을 모든 일반 노드보다 위에 표시되도록 설정
        /// </summary>
        private void EnsureJobIconsOnTop()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            var jobIcons = new List<GameObject>();
            
            // 모든 직업 아이콘들 수집 (락/언락 관계없이)
            foreach (var node in manager.SkillNodes.Values)
            {
                if (nodeObjects.TryGetValue(node.Id, out var nodeObj))
                {
                    bool isJobIcon = IsJobIconName(node.IconName ?? node.Id);
                    bool isJobIconOrForced = isJobIcon || node.Id == "Mage" || node.Id == "Paladin";
                    
                    if (isJobIconOrForced)
                    {
                        jobIcons.Add(nodeObj);
                    }
                }
            }
            
            // 직업 아이콘들을 최상위로 이동
            foreach (var jobIcon in jobIcons)
            {
                jobIcon.transform.SetAsLastSibling();
            }
            
            // 직업 아이콘 Z-Order 재설정 완료 (로그 제거)
        }
        

        private Image CreateLine(RectTransform parent, RectTransform from, RectTransform to)
        {
            var go = new GameObject("Line", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            go.transform.SetParent(parent, false);
            var img = go.GetComponent<Image>();
            img.color = new Color(0.7f, 0.7f, 0.7f, 0.8f);
            img.raycastTarget = false;
            var rect = go.GetComponent<RectTransform>();
            Vector2 start = from.anchoredPosition;
            Vector2 end = to.anchoredPosition;
            Vector2 dir = (end - start).normalized;
            float dist = Vector2.Distance(start, end);
            rect.sizeDelta = new Vector2(dist, 4);
            rect.anchoredPosition = (start + end) / 2f;
            float angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;
            rect.localRotation = Quaternion.Euler(0, 0, angle);
            return img;
        }

        public System.Collections.IEnumerator NodeInvestAnimation(RectTransform nodeRect)
        {
            Vector3 orig = nodeRect.localScale;
            nodeRect.localScale = orig * 1.2f;
            yield return new WaitForSeconds(0.1f);
            nodeRect.localScale = orig;
        }

        // 연결선 색상 갱신 (투자 전 50% 흐림, 투자된 노드끼리만 흰색)
        public void UpdateConnectionLines()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            // 모든 연결선을 기본 흐린 회색(50% 투명)으로 설정
            foreach (var line in connectionLines.Values)
            {
                line.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
            }
            // 투자된 노드들의 연결선만 색상 변경
            foreach (var kvp in connectionLines)
            {
                var (fromNodeId, toNodeId) = kvp.Key;
                var line = kvp.Value;
                int fromLevel = manager.GetSkillLevel(fromNodeId);
                int fromPendingLevel = manager.pendingInvestments.ContainsKey(fromNodeId) ? manager.pendingInvestments[fromNodeId] : 0;
                bool fromInvested = fromLevel > 0 || fromPendingLevel > 0;
                int toLevel = manager.GetSkillLevel(toNodeId);
                int toPendingLevel = manager.pendingInvestments.ContainsKey(toNodeId) ? manager.pendingInvestments[toNodeId] : 0;
                bool toInvested = toLevel > 0 || toPendingLevel > 0;
                if (fromInvested && toInvested)
                {
                    line.color = Color.white; // 투자된 노드끼리 연결선만 흰색
                }
            }
        }
    }
} 
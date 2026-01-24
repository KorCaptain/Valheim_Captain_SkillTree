using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.Gui
{
    public class SkillTreeTooltipUI : MonoBehaviour
    {
        private GameObject? dynamicTooltipObj = null;
        private GameObject? warningObj;
        private Text? warningText;
        private static Canvas? tooltipCanvas = null; // 툴팁 전용 Canvas
        
        /// <summary>
        /// 이벤트 기반 툴팁 입력 처리 - 키 입력 시에만 실행
        /// </summary>
        private void Update()
        {
            // 툴팁이 열려있을 때만 입력 체크
            if (dynamicTooltipObj != null && dynamicTooltipObj.activeInHierarchy)
            {
                // ESC 키로 툴팁 닫기
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    HideTooltip();
                }
                
                // Tab 키로 툴팁 닫기
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    HideTooltip();
                }
                
                // 우클릭으로 툴팁 닫기
                if (Input.GetMouseButtonDown(1))
                {
                    HideTooltip();
                }
                
                // 휠클릭으로 툴팁 닫기
                if (Input.GetMouseButtonDown(2))
                {
                    HideTooltip();
                }
            }
        }
        
        // LateUpdate 제거 - 이벤트 기반으로 처리하므로 불필요

        public void ShowTooltip(CaptainSkillTree.SkillTree.SkillNode node, Vector2 nodePos)
        {
            if (node == null)
            {
                return;
            }
            
            if (dynamicTooltipObj != null)
                Destroy(dynamicTooltipObj);
                
            // 최상위 Canvas 찾기 (가장 높은 sortingOrder를 가진 Canvas)
            var allCanvases = GameObject.FindObjectsOfType<Canvas>();
            Canvas topCanvas = null;
            int highestOrder = int.MinValue;
            
            foreach (var c in allCanvases)
            {
                if (c.sortingOrder > highestOrder)
                {
                    highestOrder = c.sortingOrder;
                    topCanvas = c;
                }
            }
            
            if (topCanvas == null) 
            {
                return;
            }
            
            // 툴팁 전용 Canvas 생성 또는 기존 것 사용
            if (tooltipCanvas == null)
            {
                CreateTooltipCanvas(highestOrder);
            }
            
            dynamicTooltipObj = new GameObject("Tooltip", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            dynamicTooltipObj.transform.SetParent(tooltipCanvas.transform, false);
            var rect = dynamicTooltipObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(450, 200); // 기본 크기 증가
            rect.pivot = new Vector2(0, 1);
            
            // 먼저 테두리 생성
            var borderObj = new GameObject("TooltipBorder", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            borderObj.transform.SetParent(dynamicTooltipObj.transform, false);
            var borderRect = borderObj.GetComponent<RectTransform>();
            borderRect.anchorMin = Vector2.zero;
            borderRect.anchorMax = Vector2.one;
            borderRect.offsetMin = new Vector2(-3, -3);
            borderRect.offsetMax = new Vector2(3, 3);
            var borderImg = borderObj.GetComponent<Image>();
            borderImg.color = new Color(0.2f, 0.4f, 0.8f, 0.9f); // 파란색 테두리
            borderImg.raycastTarget = false;
            
            // 그 다음 검정색 배경 생성
            var bgObj = new GameObject("TooltipBackground", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            bgObj.transform.SetParent(dynamicTooltipObj.transform, false);
            var bgRect = bgObj.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var bgImg = bgObj.GetComponent<Image>();
            bgImg.color = new Color(0f, 0f, 0f, 1f); // 완전 검정색 배경
            bgImg.raycastTarget = false;
            
            // 메인 툴팁 오브젝트는 투명하게
            var img = dynamicTooltipObj.GetComponent<Image>();
            img.color = new Color(0f, 0f, 0f, 0f); // 완전 투명
            img.raycastTarget = false;
            var txtObj = new GameObject("TooltipText", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            txtObj.transform.SetParent(dynamicTooltipObj.transform, false);
            var txtRect = txtObj.GetComponent<RectTransform>();
            txtRect.anchorMin = new Vector2(0, 0);
            txtRect.anchorMax = new Vector2(1, 1);
            txtRect.offsetMin = new Vector2(15, 15); // 여백 증가
            txtRect.offsetMax = new Vector2(-15, -15);
            var txt = txtObj.GetComponent<Text>();
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.fontSize = 16; // 가독성을 위해 크기 조정
            txt.color = Color.white;
            txt.alignment = TextAnchor.UpperLeft;
            txt.horizontalOverflow = HorizontalWrapMode.Wrap;
            txt.verticalOverflow = VerticalWrapMode.Overflow;
            txt.lineSpacing = 1.2f; // 줄 간격 개선
            string nodeName = node.Name ?? node.Id;
            string tooltipText;
            
            // 단검 스킬들의 경우 개선된 Knife_Tooltip 시스템 사용
            if (node.Id.StartsWith("knife_"))
            {
                tooltipText = Knife_Tooltip.GetKnifeSkillTooltip(node.Id);
            }
            // 검 스킬들의 경우 개선된 Sword_Tooltip 시스템 사용
            else if (node.Id.StartsWith("sword_"))
            {
                tooltipText = Sword_Tooltip.GetSwordSkillTooltip(node.Id);
            }
            // 창 스킬들의 경우 개선된 Spear_Tooltip 시스템 사용
            else if (node.Id.StartsWith("spear_"))
            {
                tooltipText = Spear_Tooltip.GetSpearSkillTooltip(node.Id);
            }
            // 폴암 스킬들의 경우 개선된 Polearm_Tooltip 시스템 사용
            else if (node.Id.StartsWith("polearm_"))
            {
                tooltipText = Polearm_Tooltip.GetPolearmSkillTooltip(node.Id);
            }
            // 둔기 스킬들의 경우 개선된 Mace_Tooltip 시스템 사용
            else if (node.Id.StartsWith("mace_"))
            {
                tooltipText = Mace_Tooltip.GetMaceSkillTooltip(node.Id);
            }
            // 아처, 탱커, 메이지의 경우 새로운 툴팁 시스템 사용
            else if (node.Id == "Archer")
            {
                tooltipText = Archer_Tooltip.GetArcherTooltip();
            }
            else if (node.Id == "Tanker")
            {
                tooltipText = Tanker_Tooltip.GetTankerTooltip();
            }
            else if (node.Id == "Mage")
            {
                tooltipText = Mage_Tooltip.GetMageTooltip();
            }
            else if (node.Id == "Berserker")
            {
                tooltipText = Berserker_Tooltip.GetBerserkerTooltip();
            }
            else if (node.Id == "성기사")
            {
                tooltipText = Paladin_Config.GetPaladinTooltip();
            }
            else if (node.Id == "Rogue")
            {
                tooltipText = Rogue_Tooltip.GetRogueTooltip();
            }
            else if (node.Id == "staff_Step6_heal")
            {
                // 힐 스킬 전용 동적 툴팁 처리
                tooltipText = HealerMode_Tooltip.GetHealerModeTooltip();
            }
            else if (node.Id == "staff_Step6_dual_cast")
            {
                // 이중 시전 스킬 전용 동적 툴팁 처리
                tooltipText = Staff_Tooltip.GetDualCastTooltip();
            }
            else if (node.Id == "spear_Step5_combo")
            {
                // 연공창 스킬 전용 동적 툴팁 처리 (SpearSkillMappings에 없으므로 특별 처리)
                tooltipText = Spear_Tooltip.GetSpearEnhancedThrowTooltip();
            }
            else
            {
                string desc = node.Description ?? "";
                string descNoTag = Regex.Replace(desc, "<.*?>", "");
                // Config 값을 반영한 설명 생성
                string finalDesc = ApplyConfigToDescription(node.Id, descNoTag);
                
                string descMain = finalDesc;
                string condLine = null;
                var condMatch = Regex.Match(finalDesc, @"(※ ?[가-힣 ]+착용시 효과발동|※ ?[가-힣 ]+착용 시 효과 발동|※ ?[가-힣 ]+사용 시 효과 발동)");
                if (condMatch.Success) {
                    condLine = condMatch.Value;
                    descMain = finalDesc.Replace(condLine, "").TrimEnd('\n');
                }
                descMain = descMain.Replace("\n", "").Trim();
                if (condLine != null) {
                    condLine = condLine.Replace("\n", "").Trim();
                }
                
                tooltipText = CreateFormattedTooltip(node, descMain, condLine);
            }
            txt.supportRichText = true;
            txt.text = tooltipText;
            
            // 텍스트 크기에 맞춰 툴팁 크기 자동 조절
            AutoResizeTooltip(rect, txt);
            
            // 마우스 위치 기준으로 툴팁 위치 설정
            Vector2 mousePos = Input.mousePosition;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 tooltipSize = rect.sizeDelta;
            
            // 화면 경계 내에서 툴팁 위치 조정
            Vector2 targetPos = mousePos + new Vector2(20, -20);
            
            // 오른쪽 경계 체크
            if (targetPos.x + tooltipSize.x > screenSize.x)
            {
                targetPos.x = mousePos.x - tooltipSize.x - 20;
            }
            
            // 아래쪽 경계 체크
            if (targetPos.y - tooltipSize.y < 0)
            {
                targetPos.y = mousePos.y + tooltipSize.y + 20;
            }
            
            rect.position = targetPos;
            dynamicTooltipObj.transform.SetAsLastSibling();
        }
        
        /// <summary>
        /// 툴팁 전용 Canvas 생성 (모든 UI보다 위에 렌더링)
        /// </summary>
        private static void CreateTooltipCanvas(int highestOrder)
        {
            var canvasObj = new GameObject("TooltipCanvas", typeof(Canvas), typeof(UnityEngine.UI.GraphicRaycaster));
            tooltipCanvas = canvasObj.GetComponent<Canvas>();
            
            // Screen Space - Overlay 모드로 설정
            tooltipCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            tooltipCanvas.sortingOrder = highestOrder + 100; // 기존 Canvas들보다 훨씬 위에
            
            // 마우스 이벤트 차단하지 않도록 설정
            var raycaster = canvasObj.GetComponent<UnityEngine.UI.GraphicRaycaster>();
            raycaster.blockingObjects = UnityEngine.UI.GraphicRaycaster.BlockingObjects.None;
            
            // Canvas가 파괴되지 않도록 설정
            GameObject.DontDestroyOnLoad(canvasObj);
        }
        public void HideTooltip()
        {
            if (dynamicTooltipObj != null)
            {
                Destroy(dynamicTooltipObj);
                dynamicTooltipObj = null;
            }
        }
        
        /// <summary>
        /// 툴팁이 현재 표시되고 있는지 확인
        /// </summary>
        public bool IsTooltipVisible()
        {
            return dynamicTooltipObj != null && dynamicTooltipObj.activeInHierarchy;
        }
        public void CreateWarning(Transform parent)
        {
            warningObj = new GameObject("Warning", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            warningObj.transform.SetParent(parent, false);
            var rect = warningObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(400, 30);
            rect.anchoredPosition = new Vector2(0, 120);
            warningText = warningObj.GetComponent<Text>();
            warningText.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // 포인트 초기화 버튼과 동일한 폰트
            warningText.fontSize = 20;
            warningText.color = Color.red;
            warningText.alignment = TextAnchor.MiddleCenter;
            warningObj.SetActive(false);
        }
        public void ShowWarning(string msg)
        {
            // Warning 오브젝트가 없으면 자동 생성
            if (warningObj == null || warningText == null)
            {
                
                // 툴팁 Canvas 찾기
                Transform parentTransform = tooltipCanvas?.transform;
                if (parentTransform == null)
                {
                    // 최상위 Canvas 찾기
                    var allCanvases = GameObject.FindObjectsOfType<Canvas>();
                    Canvas topCanvas = null;
                    int highestOrder = int.MinValue;
                    
                    foreach (var c in allCanvases)
                    {
                        if (c.sortingOrder > highestOrder)
                        {
                            highestOrder = c.sortingOrder;
                            topCanvas = c;
                        }
                    }
                    parentTransform = topCanvas?.transform;
                }
                
                if (parentTransform != null)
                {
                    CreateWarning(parentTransform);
                }
                else
                {
                    return;
                }
            }
            
            try
            {
                warningObj.SetActive(true);
                warningText.color = Color.red;
                warningText.text = msg;
                warningObj.transform.SetAsLastSibling();
                CancelInvoke(nameof(HideWarning));
                Invoke(nameof(HideWarning), 1.5f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[ShowWarning] 경고 표시 중 오류: {ex.Message}");
            }
        }
        public void HideWarning()
        {
            if (warningObj != null) warningObj.SetActive(false);
        }
        public System.Collections.IEnumerator ButtonClickEffect(RectTransform buttonRect)
        {
            Vector3 originalScale = buttonRect.localScale;
            buttonRect.localScale = originalScale * 0.9f;
            yield return new WaitForSeconds(0.1f);
            buttonRect.localScale = originalScale;
        }
        
        /// <summary>
        /// 공격 전문가 노드의 설명에서 config 값을 대체
        /// </summary>
        public string ApplyConfigToDescription(string nodeId, string originalDesc)
        {
            // ApplyConfigToDescription 진입

            if (string.IsNullOrEmpty(nodeId))
            {
                return originalDesc;
            }

            // staff_Step6_heal과 staff_Step6_dual_cast는 위쪽 직접 처리 경로에서 처리되므로 여기서는 제외

            if (string.IsNullOrEmpty(originalDesc))
            {
                return originalDesc;
            }
                
            // Plugin.Log.LogInfo(string.Format("[툴팁] ApplyConfigToDescription 호출: nodeId={0}, desc={1}", nodeId, originalDesc));
            string result = originalDesc;
            
            switch (nodeId)
            {
                case "attack_root": // 루트 노드 - "모든 데미지 +5%"
                    result = result.Replace("+5%", string.Format("+{0}%", SkillTreeConfig.AttackRootDamageBonusValue.ToString("F0")));
                    // Plugin.Log.LogInfo(string.Format("[툴팁] attack_root 적용: {0}", result));
                    break;

                case "defense_root": // 방어 전문가 루트 노드 - "체력 +5, 방어 +2"
                    result = $"체력 +{Defense_Config.DefenseRootHealthBonusValue}, 방어 +{Defense_Config.DefenseRootArmorBonusValue}";
                    break;

                // === 둔기 트리 스킬들 ===
                case "mace_Step1_damage": // 둔기 전문가
                    result = $"둔기 피해 +{Mace_Config.MaceExpertDamageBonusValue}%, 공격 시 {Mace_Config.MaceExpertStunChanceValue}% 확률로 {Mace_Config.MaceExpertStunDurationValue}초 기절";
                    break;
                case "mace_Step2_stun_boost": // 기절 강화
                    result = $"기절 확률 +{Mace_Config.MaceStep2StunChanceBonusValue}%, 지속시간 +{Mace_Config.MaceStep2StunDurationBonusValue}초";
                    break;
                case "mace_Step3_branch_guard": // 방어 강화
                    result = $"방어 +{Mace_Config.MaceStep3GuardArmorBonusValue}%";
                    break;
                case "mace_Step3_branch_heavy": // 무거운 타격
                    result = $"무거운 공격 데미지 +{Mace_Config.MaceStep3HeavyDamageBonusValue}%";
                    break;
                case "mace_Step4_push": // 밀어내기
                    result = $"공격 시 {Mace_Config.MaceStep4KnockbackChanceValue}% 확률로 노크백";
                    break;
                case "mace_Step5_tank": // 탱커
                    result = $"체력 +{Mace_Config.MaceStep5TankHealthBonusValue}%, 받는 데미지 -{Mace_Config.MaceStep5TankDamageReductionValue}%";
                    break;
                case "mace_Step5_dps": // 공격력 강화
                    result = $"공격력 +{Mace_Config.MaceStep5DpsDamageBonusValue}%, 공격속도 +{Mace_Config.MaceStep5DpsAttackSpeedBonusValue}%";
                    break;
                case "mace_Step6_grandmaster": // 그랜드마스터
                    result = $"방어 +{Mace_Config.MaceStep6ArmorBonusValue}%";
                    break;
                case "mace_Step7_fury_hammer": // 분노의 망치 (액티브 스킬)
                    result = Mace_Tooltip.GetMaceStep7FuryHammerTooltip();
                    break;
                case "mace_Step7_guardian_heart": // 수호자의 진심 (액티브 스킬)
                    result = Mace_Tooltip.GetMaceStep7GuardianHeartTooltip();
                    break;

                // === 지팡이 트리 스킬들 ===
                case "staff_Step1_damage": // 지팡이 전문가
                    result = $"속성 공격 +{Staff_Config.StaffExpertDamageValue}%";
                    break;
                case "staff_Step2_focus": // 정신 집중
                    result = $"에이트르 소모 -{Staff_Config.StaffFocusEitrReductionValue}%";
                    break;
                case "staff_Step2_stream": // 마법 흐름
                    result = $"최대 에이트르 +{Staff_Config.StaffStreamEitrBonusValue}";
                    break;
                case "staff_Step3_amp": // 마법 증폭
                    result = $"속성 공격 +{Staff_Config.StaffAmpDamageValue}%";
                    break;
                case "staff_Step4_reduction": // 냉기 속성
                    result = "냉기 공격 +3";
                    break;
                case "staff_Step4_range": // 화염 속성
                    result = "화염 공격 +3";
                    break;
                case "staff_Step4_surge": // 번개 속성
                    result = "번개 공격 +3";
                    break;
                case "staff_Step5_archmage": // 행운 마력
                    result = $"{Staff_Config.StaffLuckManaChanceValue}% 확률로 에이트르 소모 없음";
                    break;
                case "staff_Step6_dual_cast": // 이중 시전 (액티브 스킬)
                    result = Staff_Tooltip.GetDualCastTooltip();
                    break;

                // 2단계 노드들
                case "atk_melee_bonus": // 근접 특화 - "근접 무기 사용 시 20% 확률로 +10% 추가 피해"
                    result = result.Replace("20%", SkillTreeConfig.AttackMeleeBonusChanceValue.ToString("F0") + "%");
                    result = result.Replace("+10%", "+" + SkillTreeConfig.AttackMeleeBonusDamageValue.ToString("F0") + "%");
                    break;
                case "atk_bow_bonus": // 활 특화 - "활 사용 시 20% 확률로 +10% 추가 피해"
                    result = result.Replace("20%", SkillTreeConfig.AttackBowBonusChanceValue.ToString("F0") + "%");
                    result = result.Replace("+10%", "+" + SkillTreeConfig.AttackBowBonusDamageValue.ToString("F0") + "%");
                    break;
                case "atk_crossbow_bonus": // 석궁 특화 - "석궁 사용 시 15% 확률로 넉백 발생"
                    result = result.Replace("15%", SkillTreeConfig.AttackCrossbowBonusChanceValue.ToString("F0") + "%");
                    // 석궁은 데미지가 아닌 넉백이므로 데미지 값은 적용하지 않음
                    break;
                case "atk_staff_bonus": // 지팡이 특화 - "지팡이 사용 시 20% 확률로 주변 2마리 적에게 속성 50% 추가 피해"
                    result = result.Replace("20%", SkillTreeConfig.AttackStaffBonusChanceValue.ToString("F0") + "%");
                    result = result.Replace("50%", SkillTreeConfig.AttackStaffBonusDamageValue.ToString("F0") + "%");
                    break;
                    
                // 4단계 노드들
                case "atk_crit_chance": // 정밀 공격 - "치명타 확률 +5%"
                    result = result.Replace("+5%", "+" + SkillTreeConfig.AttackCritChanceValue.ToString("F0") + "%");
                    break;
                case "atk_melee_crit": // 한손 강화 - "한손 근접무기 2연속 공격 시 +10% 추가 피해"
                    result = result.Replace("+10%", "+" + SkillTreeConfig.AttackOneHandedBonusValue.ToString("F0") + "%");
                    break;
                    
                // 6단계 노드들
                case "atk_crit_dmg": // 약점 공격 - "치명타 피해 +7%"
                    result = result.Replace("+7%", "+" + SkillTreeConfig.AttackCritDamageBonusValue.ToString("F0") + "%");
                    break;
                case "atk_twohand_crush": // 양손 분쇄 - "양손 무기 공격력 +10%"
                    result = result.Replace("+10%", "+" + SkillTreeConfig.AttackTwoHandedBonusValue.ToString("F0") + "%");
                    break;
                case "atk_staff_mage": // 속성 공격 - "지팡이 공격 시 속성 공격 +10%"
                    result = result.Replace("+10%", "+" + SkillTreeConfig.AttackStaffElementalValue.ToString("F0") + "%");
                    break;
                case "atk_finisher_melee": // 연속 근접의 대가 - "근접 3연속 공격 시 +10% 추가 피해"
                    result = result.Replace("+10%", "+" + SkillTreeConfig.AttackFinisherMeleeBonusValue.ToString("F0") + "%");
                    break;
                    
                // === 지팡이 액티브 스킬들 ===
                // staff_Step6_dual_cast와 staff_Step6_heal은 위쪽 직접 처리 경로에서 처리됨
                    
                // === 폴암 스킬들 ===
                case "polearm_expert": // 폴암 전문가
                case "polearm_spin": // 회전 공격
                case "polearm_suppress": // 제압 공격
                case "polearm_balance": // 균형 잡기
                case "polearm_area": // 광역 공격
                case "polearm_ground": // 지면 충격
                case "polearm_moon": // 달빛 베기
                case "polearm_charge": // 돌진 공격
                case "polearm_king": // 폴암 킹
                    // 폴암 스킬들은 현재 컨피그 적용 없음 - 원본 설명 사용
                    // Plugin.Log.LogInfo($"[툴팁] 폴암 스킬 {nodeId} 툴팁 처리");
                    break;

                // === 방어 트리 스킬들 ===
                case "defense_Step1_survival": // 피부경화 - "체력 +?, 방어 +?"
                    result = $"체력 +{Defense_Config.SurvivalHealthBonusValue}, 방어 +{Defense_Config.SurvivalArmorBonusValue}";
                    break;
                case "defense_Step2_health": // 체력단련 - "체력 +?, 방어 +?"
                    result = $"체력 +{Defense_Config.HealthBonusValue}, 방어 +{Defense_Config.HealthArmorBonusValue}";
                    break;
                case "defense_Step2_dodge": // 심신단련 - "스태미나 최대치 +?, 에이트르 최대치 +?"
                    result = $"스태미나 최대치 +{Defense_Config.DodgeStaminaBonusValue}, 에이트르 최대치 +{Defense_Config.DodgeEitrBonusValue}";
                    break;
                case "defense_Step3_breath": // 단전호흡 - "에이트르 최대치 +?"
                    result = $"에이트르 최대치 +{Defense_Config.BreathEitrBonusValue}";
                    break;
                case "defense_Step3_agile": // 회피단련 - "회피 +7%, 구르기 무적시간 +20%"
                    result = $"회피 +{Defense_Config.AgileDodgeBonusValue}%, 구르기 무적시간 +{Defense_Config.AgileInvincibilityBonusValue}%";
                    break;
                case "defense_Step3_boost": // 체력증강 - "체력 +?"
                    result = $"체력 +{Defense_Config.BoostHealthBonusValue}";
                    break;
                case "defense_Step3_shield": // 방패훈련 - "방패 방어력 +?"
                    result = $"방패 방어력 +{Defense_Config.ShieldTrainingBlockPowerBonusValue}";
                    break;
                case "defense_Step4_tanker": // 바위피부 - "방어력 +12%" (Config 추가 전 임시 하드코딩)
                    result = "방어력 +12%";
                    break;
                case "defense_Step5_focus": // 지구력 - "달리기 스태미나 -?, 점프 스태미나 -?"
                    result = $"달리기 스태미나 -{Defense_Config.FocusRunStaminaReductionValue}%, 점프 스태미나 -{Defense_Config.FocusJumpStaminaReductionValue}%";
                    break;
                case "defense_Step5_stamina": // 기민함 - "회피 +12%, 구르기 스태미나 -12%"
                    result = $"회피 +{Defense_Config.StaminaDodgeBonusValue}%, 구르기 스태미나 -{Defense_Config.StaminaRollStaminaReductionValue}%";
                    break;
                case "defense_Step5_heal": // 트롤의 재생력 - "?초마다 체력 +?"
                    result = $"{Defense_Config.TrollRegenIntervalValue}초마다 체력 +{Defense_Config.TrollRegenBonusValue}";
                    break;
                case "defense_Step5_parry": // 막기달인 - "패링 +?초, 방패 방어력 +?"
                    result = $"패링 +{Defense_Config.ParryMasterParryDurationBonusValue}초, 방패 방어력 +{Defense_Config.ParryMasterBlockPowerBonusValue}";
                    break;
                case "defense_Step6_attack": // 신경강화 - "회피 +15%"
                    result = $"회피 +{Defense_Config.AttackDodgeBonusValue}%";
                    break;
                case "defense_Step6_body": // 요툰의 생명력 - "체력 최대치 +?%, 물리/마법 방어력 +?%"
                    result = $"체력 최대치 +{Defense_Config.BodyHealthBonusValue}%, 물리/마법 방어력 +{Defense_Config.BodyArmorBonusValue}%";
                    break;
                case "defense_Step6_true": // 요툰의 방패 - "방패 블럭 스태미나 -?%, 일반 방패 이동속도 +?%, 대형 방패 이동속도 +?%"
                    result = $"블럭 스태미나 -{Defense_Config.JotunnShieldBlockStaminaReductionValue}%, 일반 방패 이동속도 +{Defense_Config.JotunnShieldNormalSpeedBonusValue}%, 대형 방패 이동속도 +{Defense_Config.JotunnShieldTowerSpeedBonusValue}%";
                    break;

                // === 직업 스킬들 - 실제 노드 ID 사용 ===
                case "성기사": // 성기사 - 상세 툴팁 시스템 사용 (컨피그 연동)
                    result = Paladin_Config.GetPaladinTooltip();
                    break;
                case "Tanker": // 탱커 - 상세 툴팁 시스템 사용
                    result = Tanker_Tooltip.GetTankerTooltip();
                    break;
                case "Archer": // 궁수 - 상세 툴팁 시스템 사용
                    result = Archer_Tooltip.GetArcherTooltip();
                    break;
                case "Rogue": // 도적
                    // 기본 설명 사용 (컨피그 연동 없음)
                    break;

                case "Mage": // 마법사 - 별도 툴팁 시스템에서 처리됨 (중복 방지)
                    // 메이지는 이제 직접 툴팁 처리 경로에서 처리되므로 여기서는 원본 설명 사용
                    break;
            }

            // ApplyConfigToDescription 완료
            return result;
        }
        
        /// <summary>
        /// 체계적이고 일관된 툴팁 텍스트 생성
        /// </summary>
        private string CreateFormattedTooltip(CaptainSkillTree.SkillTree.SkillNode node, string descMain, string condLine)
        {
            var tooltipText = new System.Text.StringBuilder();
            
            // 스킬 이름 (한국어 표시명)
            string nodeName = node.Name ?? node.Id;
            tooltipText.AppendLine($"<color=#FFD700><size=22>{nodeName}</size></color>");
            tooltipText.AppendLine();
            
            // 스킬 정보 파싱
            var skillInfo = ParseSkillInfo(node.Id, descMain, condLine);
            
            // 설명
            if (!string.IsNullOrEmpty(skillInfo.Description))
            {
                tooltipText.AppendLine($"<color=#90EE90><size=16>설명: </size></color><color=#E0E0E0><size=16>{skillInfo.Description}</size></color>");
            }
            
            // 범위
            if (!string.IsNullOrEmpty(skillInfo.Range))
            {
                tooltipText.AppendLine($"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>{skillInfo.Range}</size></color>");
            }
            
            // 소모
            if (!string.IsNullOrEmpty(skillInfo.Cost))
            {
                tooltipText.AppendLine($"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>{skillInfo.Cost}</size></color>");
            }
            
            // 스킬 유형
            tooltipText.AppendLine($"<color=#DDA0DD><size=16>스킬유형: </size></color><color=#E6E6FA><size=16>{skillInfo.SkillType}</size></color>");
            
            // 쿨타임
            if (!string.IsNullOrEmpty(skillInfo.Cooldown))
            {
                tooltipText.AppendLine($"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{skillInfo.Cooldown}</size></color>");
            }
            
            // 필요조건 처리 (생산 스킬 특별 처리)
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            bool isProductionSkill = CaptainSkillTree.SkillTree.SkillItemRequirements.IsProductionSkill(node.Id);
            bool isUnlocked = manager != null && manager.GetSkillLevel(node.Id) > 0;
            
            if (isProductionSkill && isUnlocked)
            {
                // 언락된 생산 스킬: "습득완료"로 표시하고 재료 목록 제거
                tooltipText.AppendLine($"<color=#98FB98><size=16>필요조건: </size></color><color=#90EE90><size=16>습득완료</size></color>");
            }
            else if (!string.IsNullOrEmpty(skillInfo.Requirements))
            {
                if (isProductionSkill)
                {
                    // 미언락 생산 스킬: 조건 충족 여부에 따라 색상 변경
                    string coloredRequirements = GetColoredProductionRequirements(node.Id, skillInfo.Requirements);
                    tooltipText.AppendLine($"<color=#98FB98><size=16>필요조건: </size></color>{coloredRequirements}");
                }
                else
                {
                    // 일반 스킬: 기본 색상
                    tooltipText.AppendLine($"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>{skillInfo.Requirements}</size></color>");
                }
            }
            
            // 확인사항 (직업 제한, 레벨 제한 등)
            if (!string.IsNullOrEmpty(skillInfo.Notice))
            {
                tooltipText.AppendLine($"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>{skillInfo.Notice}</size></color>");
            }
            
            // 💎 필요 포인트 (직업별 특별 처리)
            tooltipText.AppendLine();
            
            string requiredPointsText;
            
            if (node.Id == "성기사")
            {
                requiredPointsText = "에이크쉬르 트로피";
            }
            else if (node.Id == "Tanker" || node.Id == "Berserker" || node.Id == "Rogue" || 
                     node.Id == "Mage" || node.Id == "Archer")
            {
                requiredPointsText = "에이크쉬르 트로피";
            }
            else
            {
                requiredPointsText = node.RequiredPoints.ToString();
            }
            
            // 생산 스킬 여부 확인
            if (SkillItemRequirements.IsProductionSkill(node.Id))
            {
                // 생산 스킬인 경우 재료 요구사항 표시
                // 아직 언락되지 않은 스킬만 필요조건 표시
                if (!isUnlocked)
                {
                    var requirementText = CaptainSkillTree.SkillTree.ItemManager.GetSkillRequirementsText(node.Id);
                    if (!string.IsNullOrEmpty(requirementText))
                    {
                        string headerText = "📦 필요조건";
                        string requirementColor = "#FFEB3B"; // 노란색
                        
                        tooltipText.AppendLine($"<color=#FFD700><size=18>{headerText}:</size></color>");
                        tooltipText.AppendLine($"<color={requirementColor}><size=16>{requirementText}</size></color>");
                    }
                }
            }
            else
            {
                // 일반 스킬인 경우 필요 포인트 표시
                tooltipText.AppendLine($"<color=#87CEEB><size=16>💎 필요 포인트: </size></color><color=#FF6B6B><size=16>{requiredPointsText}</size></color>");
            }
            
            return tooltipText.ToString().TrimEnd();
        }
        
        /// <summary>
        /// 스킬 정보 데이터 구조체
        /// </summary>
        private struct SkillInfo
        {
            public string Description;
            public string Range;
            public string Cost;
            public string SkillType;
            public string Cooldown;
            public string Requirements;
            public string ExtraInfo;
            public string Notice; // 확인사항 (직업 제한, 레벨 제한 등)
        }
        
        /// <summary>
        /// 스킬 설명에서 구조화된 정보 추출
        /// </summary>
        private SkillInfo ParseSkillInfo(string nodeId, string descMain, string condLine)
        {
            var info = new SkillInfo();
            
            // 기본 스킬 유형 결정
            info.SkillType = DetermineSkillType(nodeId, descMain);
            
            // 설명에서 정보 추출
            if (!string.IsNullOrEmpty(descMain))
            {
                var lines = descMain.Split('\n');
                var description = "";
                
                foreach (var line in lines)
                {
                    var trimmedLine = line.Trim();
                    
                    if (trimmedLine.StartsWith("설명:") || trimmedLine.StartsWith("설명 :"))
                    {
                        // "설명:" 라벨이 있는 경우 - Job_Tooltip 시스템에서 생성된 툴팁
                        var descContent = trimmedLine.StartsWith("설명:") ? 
                            trimmedLine.Substring(3).Trim() : 
                            trimmedLine.Substring(4).Trim();
                        if (!string.IsNullOrEmpty(descContent))
                        {
                            description += (string.IsNullOrEmpty(description) ? "" : " ") + descContent;
                        }
                    }
                    else if (trimmedLine.StartsWith("범위:"))
                    {
                        info.Range = trimmedLine.Substring(3).Trim();
                        // 설명에는 포함하지 않음 (별도 항목으로 표시)
                    }
                    else if (trimmedLine.StartsWith("소모:"))
                    {
                        info.Cost = trimmedLine.Substring(3).Trim();
                        // 설명에는 포함하지 않음 (별도 항목으로 표시)
                    }
                    else if (trimmedLine.StartsWith("쿨타임:"))
                    {
                        info.Cooldown = trimmedLine.Substring(4).Trim();
                        // 설명에는 포함하지 않음 (별도 항목으로 표시)
                    }
                    else if (trimmedLine.StartsWith("스킬유형:"))
                    {
                        info.SkillType = trimmedLine.Substring(5).Trim();
                        // 설명에는 포함하지 않음 (별도 항목으로 표시)
                    }
                    else if (trimmedLine.StartsWith("필요조건:"))
                    {
                        info.Requirements = trimmedLine.Substring(5).Trim();
                        // 설명에는 포함하지 않음 (별도 항목으로 표시)
                    }
                    else if (trimmedLine.Contains("키:"))
                    {
                        // Y키, T키, G키, H키 설명은 스킬 설명에 포함 (단, 키 정보와 쿨타임 정보는 제외)
                        string cleanLine = trimmedLine;
                        
                        // 키 정보 제거 (Y키:, T키:, G키:, H키:, F키: 등)
                        cleanLine = System.Text.RegularExpressions.Regex.Replace(cleanLine, @"[YTGHF]키:\s*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Trim();
                        
                        // 쿨타임 정보 제거
                        cleanLine = System.Text.RegularExpressions.Regex.Replace(cleanLine, @"\(쿨타임\s*\d+초\)", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Trim();
                        cleanLine = System.Text.RegularExpressions.Regex.Replace(cleanLine, @",?\s*쿨타임\s*\d+초", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Trim();
                        
                        if (!string.IsNullOrEmpty(cleanLine))
                        {
                            description += (string.IsNullOrEmpty(description) ? "" : " ") + cleanLine;
                        }
                    }
                    else if (trimmedLine.StartsWith("⚠️확인사항:"))
                    {
                        // 확인사항을 Notice로 설정
                        info.Notice = trimmedLine.Substring(6).Trim();
                    }
                    else if (trimmedLine.StartsWith("⚠️필요 아이템:"))
                    {
                        // 필요 아이템 정보는 별도 처리 (현재는 설명에서 제외)
                        // 나중에 필요하면 별도 필드로 추가할 수 있음
                    }
                    else if (trimmedLine.StartsWith("⚠️") || trimmedLine.StartsWith("※") || 
                             trimmedLine.Contains("직업은 1개만 선택") || trimmedLine.Contains("플레이어 레벨 10") ||
                             trimmedLine.Contains("레벨 10 이상"))
                    {
                        // 기타 경고 문구는 설명에서 완전히 제외
                        // 이 내용들은 Notice 항목에서 별도로 표시됨
                    }
                    else if (!string.IsNullOrEmpty(trimmedLine) && !trimmedLine.StartsWith("•"))
                    {
                        description += (string.IsNullOrEmpty(description) ? "" : " ") + trimmedLine;
                    }
                }
                
                // 설명에서 경고 문구 완전 제거
                description = CleanDescription(description);
                info.Description = description;
            }
            
            // condLine 처리 - 무기타입으로 통일
            if (!string.IsNullOrEmpty(condLine))
            {
                if (condLine.Contains("착용"))
                {
                    string cleanCondLine = condLine.Replace("※", "").Trim();
                    // 기존 "활 착용시 효과발동" -> "활 착용"으로 통일
                    if (cleanCondLine.Contains("활 착용시 효과발동") || cleanCondLine.Contains("활 착용 시 효과 발동"))
                    {
                        info.Requirements = "활 착용";
                    }
                    else if (cleanCondLine.Contains("석궁 착용시 효과발동") || cleanCondLine.Contains("석궁 착용 시 효과 발동"))
                    {
                        info.Requirements = "석궁 착용";
                    }
                    else if (cleanCondLine.Contains("지팡이 착용시 효과발동") || cleanCondLine.Contains("지팡이 착용 시 효과 발동"))
                    {
                        info.Requirements = "지팡이 착용";
                    }
                    else if (cleanCondLine.Contains("검 착용시 효과발동") || cleanCondLine.Contains("검 착용 시 효과 발동"))
                    {
                        info.Requirements = "검 착용";
                    }
                    else if (cleanCondLine.Contains("도끼 착용시 효과발동") || cleanCondLine.Contains("도끼 착용 시 효과 발동"))
                    {
                        info.Requirements = "도끼 착용";
                    }
                    else if (cleanCondLine.Contains("단검 착용시 효과발동") || cleanCondLine.Contains("단검 착용 시 효과 발동"))
                    {
                        info.Requirements = "단검 착용";
                    }
                    else if (cleanCondLine.Contains("창 착용시 효과발동") || cleanCondLine.Contains("창 착용 시 효과 발동"))
                    {
                        info.Requirements = "창 착용";
                    }
                    else if (cleanCondLine.Contains("둔기 착용시 효과발동") || cleanCondLine.Contains("둔기 착용 시 효과 발동"))
                    {
                        info.Requirements = "둔기 착용";
                    }
                    else if (cleanCondLine.Contains("한손 근접무기 착용"))
                    {
                        info.Requirements = "한손 근접무기 착용";
                    }
                    else
                    {
                        info.Requirements = cleanCondLine;
                    }
                }
                else
                {
                    info.ExtraInfo += (string.IsNullOrEmpty(info.ExtraInfo) ? "" : "\n") + condLine;
                }
            }
            
            // 노드별 특수 처리
            ProcessSpecialNodes(nodeId, ref info);
            
            return info;
        }
        
        /// <summary>
        /// 스킬 유형 결정
        /// </summary>
        private string DetermineSkillType(string nodeId, string descMain)
        {
            // 액티브 스킬 키 확인
            if (descMain.Contains("Y키:"))
                return "액티브스킬 (Y키)";
            if (descMain.Contains("T키:"))
                return "액티브스킬 (T키)";
            if (descMain.Contains("G키:"))
                return "액티브스킬 (G키)";
            if (descMain.Contains("H키:"))
                return "액티브스킬 (H키)";
            if (descMain.Contains("F키:"))
                return "액티브스킬 (F키)";
                
            // 직업 스킬
            if (nodeId == "성기사" || nodeId == "Tanker" || nodeId == "Berserker" || 
                nodeId == "Rogue" || nodeId == "Mage" || nodeId == "Archer")
                return "직업 액티브스킬 (Y키)";
                
            // 전문가 노드
            if (nodeId.EndsWith("_root"))
                return "패시브 스킬";
                
            // 기본 패시브
            return "패시브스킬";
        }
        
        /// <summary>
        /// 특정 노드별 특수 처리
        /// </summary>
        private void ProcessSpecialNodes(string nodeId, ref SkillInfo info)
        {
            switch (nodeId)
            {
                case "성기사":
                    if (string.IsNullOrEmpty(info.Range))
                        info.Range = "시전자 중심 5m";
                    if (string.IsNullOrEmpty(info.Cost))
                        info.Cost = "에이트르 10, 스태미나 10";
                    if (string.IsNullOrEmpty(info.Cooldown))
                        info.Cooldown = "30초";
                    if (string.IsNullOrEmpty(info.Requirements))
                        info.Requirements = "한손 근접무기 착용, 성기사 직업";
                    if (string.IsNullOrEmpty(info.Notice))
                        info.Notice = "직업은 1개만 선택가능, 레벨 10 이상";
                    break;
                    
                case "Tanker":
                    // 탱커 상세 툴팁 정보 설정 (Tanker_Config 연동)
                    if (string.IsNullOrEmpty(info.Range))
                        info.Range = $"도발 범위 {Tanker_Config.TankerTauntRangeValue}m";
                    if (string.IsNullOrEmpty(info.Cost))
                        info.Cost = $"스태미나 {Tanker_Config.TankerTauntStaminaCostValue}";
                    if (string.IsNullOrEmpty(info.Cooldown))
                        info.Cooldown = $"{Tanker_Config.TankerTauntCooldownValue}초";
                    if (string.IsNullOrEmpty(info.Requirements))
                        info.Requirements = "방패 착용, 탱커 직업";
                    if (string.IsNullOrEmpty(info.Notice))
                        info.Notice = "직업은 1개만 선택가능, 레벨 10 이상";
                    break;
                    
                case "Berserker":
                    // 버서커 상세 툴팁 정보 설정 (Berserker_Config 연동)
                    if (string.IsNullOrEmpty(info.Cost))
                        info.Cost = $"스태미나 {Berserker_Config.BerserkerRageStaminaCostValue}";
                    if (string.IsNullOrEmpty(info.Cooldown))
                        info.Cooldown = $"{Berserker_Config.BerserkerRageCooldownValue}초";
                    if (string.IsNullOrEmpty(info.Notice))
                        info.Notice = "직업은 1개만 선택가능, 레벨 10 이상";
                    break;
                    
                case "Rogue":
                    if (string.IsNullOrEmpty(info.Cost))
                        info.Cost = "스태미나 15%";
                    if (string.IsNullOrEmpty(info.Cooldown))
                        info.Cooldown = "30초";
                    if (string.IsNullOrEmpty(info.Notice))
                        info.Notice = "직업은 1개만 선택가능, 레벨 10 이상";
                    break;
                    
                case "Mage":
                    if (string.IsNullOrEmpty(info.Range))
                        info.Range = "광역범위 +7m";
                    if (string.IsNullOrEmpty(info.Cost))
                        info.Cost = "에이트르 10%";
                    if (string.IsNullOrEmpty(info.Cooldown))
                        info.Cooldown = "30초";
                    if (string.IsNullOrEmpty(info.Notice))
                        info.Notice = "직업은 1개만 선택가능, 레벨 10 이상";
                    break;
                    
                case "Archer":
                    // 아처 상세 툴팁 정보 설정 (Archer_Config 연동)
                    if (string.IsNullOrEmpty(info.Range))
                        info.Range = $"화살 {Archer_Config.ArcherMultiShotArrowCountValue}개 발사";
                    if (string.IsNullOrEmpty(info.Cost))
                        info.Cost = $"스태미나 {Archer_Config.ArcherMultiShotStaminaCostValue}";
                    if (string.IsNullOrEmpty(info.Cooldown))
                        info.Cooldown = $"{Archer_Config.ArcherMultiShotCooldownValue}초";
                    if (string.IsNullOrEmpty(info.Requirements))
                        info.Requirements = "활 착용, 아처 직업";
                    if (string.IsNullOrEmpty(info.Notice))
                        info.Notice = "직업은 1개만 선택가능, 레벨 10 이상";
                    break;
            }
        }
        
        /// <summary>
        /// 설명에서 경고 문구 제거
        /// </summary>
        private string CleanDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                return description;
                
            description = description.Trim();
            
            // 제거할 패턴들 (경고 문구 + 별도 항목으로 표시되는 정보들)
            var removePatterns = new string[]
            {
                // 경고 문구들
                "⚠️ 직업은 1개만 선택 가능",
                "※ 플레이어 레벨 10 이상 필요",
                "⚠️ 직업은 1개만 선택가능",
                "※ 레벨 10 이상 필요",
                "⚠️직업은 1개만 선택 가능",
                "※플레이어 레벨 10 이상 필요",
                "⚠️직업은 1개만 선택가능",
                "※레벨 10 이상 필요"
            };
            
            // 별도 항목으로 표시되는 정보 패턴들 (정규표현식으로 제거)
            var infoPatterns = new string[]
            {
                @"범위:\s*[^\n]*",
                @"소모:\s*[^\n]*", 
                @"쿨타임:\s*[^\n]*",
                @"필요조건:\s*[^\n]*",
                @"\(쿨타임\s*\d+초\)",      // (쿨타임 30초) 형태
                @",?\s*쿨타임\s*\d+초",     // , 쿨타임 30초 형태
                @"쿨타임\s*\d+초",          // 쿨타임 30초 형태
                @"[YTGHF]키:\s*"           // Y키:, T키:, G키:, H키:, F키: 제거
            };
            
            // 단순 텍스트 패턴 제거
            foreach (var pattern in removePatterns)
            {
                description = description.Replace(pattern, "").Trim();
            }
            
            // 정규표현식 패턴 제거 (범위:, 소모:, 쿨타임:, 필요조건: 등)
            foreach (var pattern in infoPatterns)
            {
                description = System.Text.RegularExpressions.Regex.Replace(description, pattern, "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Trim();
            }
            
            // 연속된 공백이나 줄바꿈 정리
            while (description.Contains("  "))
            {
                description = description.Replace("  ", " ");
            }
            
            while (description.Contains("\n\n\n"))
            {
                description = description.Replace("\n\n\n", "\n\n");
            }
            
            return description.Trim();
        }
        
        /// <summary>
        /// 스킬별 아이콘 이모지 반환
        /// </summary>
        private string GetSkillIcon(string nodeId)
        {
            switch (nodeId)
            {
                case "attack_root": return "⚔️";
                case "atk_melee_bonus": return "🗡️";
                case "atk_bow_bonus": return "🏹";
                case "atk_crossbow_bonus": return "🎯";
                case "atk_staff_bonus": return "🔮";
                case "atk_crit_chance": return "💥";
                case "atk_melee_crit": return "✋";
                case "atk_crit_dmg": return "🎪";
                case "atk_twohand_crush": return "🔨";
                case "atk_staff_mage": return "⚡";
                case "atk_finisher_melee": return "🌟";
                
                // === 직업 스킬 아이콘들 - 실제 노드 ID 사용 ===
                case "성기사": return "⭐";
                case "Tanker": return "🛡️";
                case "Berserker": return "⚔️";
                case "Rogue": return "🗡️";
                case "Mage": return "🔮";
                case "Archer": return "🏹";
                
                // === 지팡이 액티브 스킬 아이콘들 ===
                case "staff_Step6_dual_cast": return "💥";
                case "staff_Step6_heal": return "🌟";
                
                default: return "⭐";
            }
        }
        
        /// <summary>
        /// 필요 조건 텍스트 생성
        /// </summary>
        private string GetPrerequisiteText(CaptainSkillTree.SkillTree.SkillNode node)
        {
            if (node.Prerequisites == null || node.Prerequisites.Count == 0)
                return "";
                
            var manager = SkillTree.SkillTreeManager.Instance;
            if (manager == null) return "";
            
            if (node.Id == "grandmaster_artisan")
            {
                return "<color=#87CEEB><size=14>🔗 필요: <color=#FFA500>노가다 전문가 + 제작 전문가</color></size></color>";
            }
            
            var prereqNames = new System.Collections.Generic.List<string>();
            foreach (var preId in node.Prerequisites)
            {
                if (manager.SkillNodes.ContainsKey(preId))
                {
                    prereqNames.Add(manager.SkillNodes[preId].Name);
                }
            }
            
            if (prereqNames.Count == 0) return "";
            
            string connector = node.Prerequisites.Count > 1 ? " 또는 " : "";
            string joinedNames = string.Join(connector, prereqNames.ToArray());
            return string.Format("<color=#87CEEB><size=16>🔗 필요: <color=#FFA500>{0}</color></size></color>", joinedNames);
        }
        
        /// <summary>
        /// 스킬 상태 정보 (포인트, 현재 레벨 등)
        /// </summary>
        private string GetSkillStatusInfo(CaptainSkillTree.SkillTree.SkillNode node)
        {
            var statusText = new System.Text.StringBuilder();
            
            // 생산 스킬은 재료, 일반 스킬은 포인트 표시
            if (SkillTree.SkillItemRequirements.IsProductionSkill(node.Id))
            {
                // 스킬이 언락되었는지 확인
                var skillManager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
                bool isUnlocked = skillManager != null && skillManager.GetSkillLevel(node.Id) > 0;
                
                // 생산 스킬: 필요 재료 표시
                var requirementsText = CaptainSkillTree.SkillTree.ItemManager.GetSkillRequirementsText(node.Id);
                if (!string.IsNullOrEmpty(requirementsText) && requirementsText != "필요 재료 없음")
                {
                    statusText.Append($"<color=#87CEEB><size=16><b>🔨 필요 재료:</b></size></color>\n");
                    
                    // 언락 상태에 따라 색상 변경
                    string materialColor = isUnlocked ? "#90EE90" : "#FFFFFF"; // 언락 시 연두색, 미언락 시 흰색
                    statusText.Append($"<color={materialColor}><size=14>{requirementsText}</size></color>");
                }
                else
                {
                    statusText.Append("<color=#87CEEB><size=16><b>🔨 필요 재료: 없음</b></size></color>");
                }
            }
            else
            {
                // 일반 스킬: 필요 포인트 표시
                statusText.Append(string.Format("<color=#87CEEB><size=16><b>💎 필요 포인트: <color=#FF6B6B><b>{0}</b></color></b></size></color>", node.RequiredPoints));
            }
            
            // 현재 레벨 정보 (가능하다면)
            var manager = SkillTree.SkillTreeManager.Instance;
            if (manager != null)
            {
                int currentLevel = manager.GetSkillLevel(node.Id);
                if (currentLevel > 0)
                {
                    statusText.Append(string.Format("  <color=#90EE90><size=16>✓ 습득됨 (Lv.{0})</size></color>", currentLevel));
                }
                else
                {
                    statusText.Append("  <color=#FFB347><size=16>🔒 미습득</size></color>");
                }
            }
            
            return statusText.ToString();
        }
        
        
        /// <summary>
        /// 텍스트 내용에 맞춰 툴팁 크기 자동 조절
        /// </summary>
        private void AutoResizeTooltip(RectTransform tooltipRect, Text text)
        {
            // 텍스트 렌더링을 위해 한 프레임 대기 후 크기 계산
            Canvas.ForceUpdateCanvases();
            
            // 최소/최대 크기 설정
            float minWidth = 300f;
            float maxWidth = 600f;
            float minHeight = 120f;
            float maxHeight = 500f;
            
            // 여백 계산
            float padding = 30f; // 좌우 여백
            float verticalPadding = 30f; // 상하 여백
            
            // 텍스트의 preferred 크기 계산
            float preferredWidth = Mathf.Clamp(text.preferredWidth + padding, minWidth, maxWidth);
            float preferredHeight = Mathf.Clamp(text.preferredHeight + verticalPadding, minHeight, maxHeight);
            
            // 툴팁 크기 적용
            tooltipRect.sizeDelta = new Vector2(preferredWidth, preferredHeight);
        }
        
        /// <summary>
        /// 생산 스킬 필요조건을 조건 충족 여부에 따라 색상 처리
        /// </summary>
        private string GetColoredProductionRequirements(string skillId, string requirements)
        {
            var player = Player.m_localPlayer;
            if (player?.GetInventory() == null)
            {
                return $"<color=#FF0000><size=16>{requirements}</size></color>";
            }

            // ItemManager를 통해 아이템 요구사항 확인
            var itemRequirements = CaptainSkillTree.SkillTree.SkillItemRequirements.GetRequirements(skillId);
            if (itemRequirements == null || itemRequirements.Count == 0)
            {
                return $"<color=#90EE90><size=16>{requirements}</size></color>";
            }

            var coloredTexts = new System.Collections.Generic.List<string>();
            
            foreach (var requirement in itemRequirements)
            {
                bool hasEnough = CaptainSkillTree.SkillTree.ItemManager.CanSatisfyRequirements(
                    new System.Collections.Generic.List<CaptainSkillTree.SkillTree.ItemRequirement> { requirement }, 
                    out var missingItems
                );
                
                string color = hasEnough ? "#90EE90" : "#FF0000"; // 연두색 또는 빨간색
                string statusIcon = hasEnough ? "✓" : "✗";
                
                // 요구사항 텍스트 생성
                string reqText;
                if (requirement is CaptainSkillTree.SkillTree.ItemEquipRequirement)
                {
                    reqText = $"{statusIcon} {requirement}";
                }
                else if (requirement.IsConsumed)
                {
                    reqText = $"{statusIcon} {requirement}"; // ItemRequirement.ToString()이 이미 (소모) 포함
                }
                else
                {
                    reqText = $"{statusIcon} {requirement.DisplayName} x{requirement.Quantity}";
                }
                
                coloredTexts.Add($"<color={color}><size=16>{reqText}</size></color>");
            }

            return string.Join("\n", coloredTexts);
        }
    }
} 
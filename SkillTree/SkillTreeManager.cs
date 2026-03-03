using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;
using System.Text;
using CaptainSkillTree.MMO_System;
using CaptainSkillTree.Localization;
// using Newtonsoft.Json; // BepInEx 환경에서 제거
#if !NO_JOTUNN
using Jotunn.Managers;
#endif

namespace CaptainSkillTree.SkillTree
{
    // 액티브 스킬 검증 결과 클래스
    public class ActiveSkillValidationResult
    {
        public bool canLearn { get; set; }
        public bool isBlocking { get; set; }
        public string message { get; set; }
        
        public ActiveSkillValidationResult(bool canLearn = true, bool isBlocking = false, string message = "")
        {
            this.canLearn = canLearn;
            this.isBlocking = isBlocking;
            this.message = message;
        }
    }

    public class SkillTreeManager
    {
        public static SkillTreeManager Instance { get; } = new SkillTreeManager();
        public Dictionary<string, SkillNode> SkillNodes = new();
        public Dictionary<string, int> pendingInvestments = new Dictionary<string, int>(); // 임시 투자 내역
        public int SkillPoints = 0; // 별도 관리: 레벨/경험치와 분리

        // 안전 장치 관련 변수
        private static bool _dataIntegrityChecked = false;
        private static Dictionary<string, SkillNode> _backupSkillNodes = new Dictionary<string, SkillNode>();
        private static readonly object _dataLock = new object();

        // 액티브 스킬 및 직업 스킬 변수들은 해당 전용 파일로 이동됨:
        // - ActiveSkills.cs: T/G/H 키 액티브 스킬
        // - JobSkills.cs: Y키 직업 스킬

        public void AddSkill(SkillNode node)
        {
            lock (_dataLock)
            {
                try
                {
                    // 노드 무결성 검증
                    if (!ValidateSkillNode(node))
                    {
                        Debug.LogError($"[SkillTreeManager] 무효한 스킬 노드: {node?.Id ?? "null"}");
                        return;
                    }

                    // 생산 스킬만 아이템 요구사항 자동 설정
                    if (SkillItemRequirements.IsProductionSkill(node.Id) && SkillItemRequirements.HasRequirements(node.Id))
                    {
                        node.RequiredItems = SkillItemRequirements.GetRequirements(node.Id);
                    }
                    
                    SkillNodes[node.Id] = node;
                    
                    // 백업 생성
                    _backupSkillNodes[node.Id] = CloneSkillNode(node);
                    
                    // Plugin.Log.LogInfo($"[SkillTreeManager] 스킬 노드 등록: {node.Id}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[SkillTreeManager] 스킬 노드 추가 실패 ({node?.Id}): {ex.Message}");
                }
            }
        }

        // 스킬 노드 유효성 검증
        private bool ValidateSkillNode(SkillNode node)
        {
            if (node == null)
            {
                Debug.LogError("[SkillTreeManager] 스킬 노드가 null입니다");
                return false;
            }

            if (string.IsNullOrEmpty(node.Id))
            {
                Debug.LogError("[SkillTreeManager] 스킬 노드 ID가 비어있습니다");
                return false;
            }

            if (string.IsNullOrEmpty(node.Name))
            {
                Debug.LogWarning($"[SkillTreeManager] 스킬 노드 이름이 비어있습니다: {node.Id}");
                node.Name = node.Id; // 기본값 설정
            }

            if (node.RequiredPoints < 0)
            {
                Debug.LogWarning($"[SkillTreeManager] 잘못된 포인트 요구량: {node.Id} - {node.RequiredPoints}");
                node.RequiredPoints = 1; // 기본값 설정
            }

            if (node.MaxLevel < 1)
            {
                Debug.LogWarning($"[SkillTreeManager] 잘못된 최대 레벨: {node.Id} - {node.MaxLevel}");
                node.MaxLevel = 1; // 기본값 설정
            }

            return true;
        }

        // 스킬 노드 복제 (백업용)
        private SkillNode CloneSkillNode(SkillNode original)
        {
            var clone = new SkillNode
            {
                Id = original.Id,
                NameKey = original.NameKey,
                DescriptionKey = original.DescriptionKey,
                DescriptionArgs = original.DescriptionArgs,
                RequiredPoints = original.RequiredPoints,
                Prerequisites = new List<string>(original.Prerequisites ?? new List<string>()),
                MaxLevel = original.MaxLevel,
                Tier = original.Tier,
                Position = original.Position,
                Category = original.Category,
                IconName = original.IconName,
                IconNameLocked = original.IconNameLocked,
                IconNameUnlocked = original.IconNameUnlocked,
                NextNodes = new List<string>(original.NextNodes ?? new List<string>())
            };
            // NameKey/DescriptionKey 없는 경우에만 정적 값 복사 (getter 안전 호출)
            if (string.IsNullOrEmpty(original.NameKey))
                clone.Name = original.Name;
            if (string.IsNullOrEmpty(original.DescriptionKey))
                clone.Description = original.Description;
            return clone;
        }

        // 데이터 무결성 검증 및 복구
        public bool VerifyAndRepairDataIntegrity()
        {
            lock (_dataLock)
            {
                try
                {
                    // 이미 검사를 완료했다면 건너뛰기
                    if (_dataIntegrityChecked)
                    {
                        return true;
                    }
                    
                    Plugin.Log.LogInfo("[SkillTreeManager] 데이터 무결성 검증 시작");
                    
                    bool dataCorrupted = false;
                    var corruptedNodes = new List<string>();

                    // 1. 필수 노드 존재 확인
                    var essentialNodes = new string[] {
                        "melee_root", "attack_root", "ranged_root", 
                        "speed_root", "production_root"
                    };

                    foreach (var nodeId in essentialNodes)
                    {
                        if (!SkillNodes.ContainsKey(nodeId))
                        {
                            Debug.LogError($"[SkillTreeManager] 필수 노드 누락: {nodeId}");
                            dataCorrupted = true;
                            
                            // 백업에서 복구 시도
                            if (_backupSkillNodes.ContainsKey(nodeId))
                            {
                                SkillNodes[nodeId] = CloneSkillNode(_backupSkillNodes[nodeId]);
                                // Plugin.Log.LogInfo($"[SkillTreeManager] 백업에서 노드 복구: {nodeId}");
                            }
                        }
                    }

                    // 2. 노드 데이터 유효성 재검증
                    foreach (var kvp in SkillNodes.ToList())
                    {
                        if (!ValidateSkillNode(kvp.Value))
                        {
                            Debug.LogError($"[SkillTreeManager] 손상된 노드 발견: {kvp.Key}");
                            corruptedNodes.Add(kvp.Key);
                            dataCorrupted = true;
                        }
                    }

                    // 3. 손상된 노드 복구 또는 제거
                    foreach (var nodeId in corruptedNodes)
                    {
                        if (_backupSkillNodes.ContainsKey(nodeId))
                        {
                            SkillNodes[nodeId] = CloneSkillNode(_backupSkillNodes[nodeId]);
                            // Plugin.Log.LogInfo($"[SkillTreeManager] 손상된 노드 복구: {nodeId}");
                        }
                        else
                        {
                            SkillNodes.Remove(nodeId);
                            Debug.LogWarning($"[SkillTreeManager] 복구 불가능한 노드 제거: {nodeId}");
                        }
                    }

                    // 4. Prerequisites 연결 상태 검증
                    VerifyNodeConnections();

                    _dataIntegrityChecked = true;
                    
                    if (dataCorrupted)
                    {
                        Debug.LogWarning("[SkillTreeManager] 데이터 손상이 감지되어 복구되었습니다");
                        return false; // 손상이 있었음을 알림
                    }
                    else
                    {
                        Plugin.Log.LogInfo("[SkillTreeManager] 데이터 무결성 검증 완료 - 정상");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[SkillTreeManager] 데이터 무결성 검증 실패: {ex.Message}");
                    return false;
                }
            }
        }

        // 노드 연결 상태 검증
        private void VerifyNodeConnections()
        {
            foreach (var kvp in SkillNodes.ToList())
            {
                var node = kvp.Value;
                
                // Prerequisites 검증
                if (node.Prerequisites != null)
                {
                    var invalidPrereqs = node.Prerequisites.Where(prereqId => !SkillNodes.ContainsKey(prereqId)).ToList();
                    foreach (var invalidPrereq in invalidPrereqs)
                    {
                        Debug.LogWarning($"[SkillTreeManager] 잘못된 Prerequisites 제거: {node.Id} -> {invalidPrereq}");
                        node.Prerequisites.Remove(invalidPrereq);
                    }
                }

                // NextNodes 검증
                if (node.NextNodes != null)
                {
                    var invalidNextNodes = node.NextNodes.Where(nextId => !SkillNodes.ContainsKey(nextId)).ToList();
                    foreach (var invalidNext in invalidNextNodes)
                    {
                        Debug.LogWarning($"[SkillTreeManager] 잘못된 NextNodes 제거: {node.Id} -> {invalidNext}");
                        node.NextNodes.Remove(invalidNext);
                    }
                }
            }
        }

        // 안전한 스킬 레벨 획득
        public int GetSkillLevel(string skillId)
        {
            try
            {
                if (Player.m_localPlayer == null) 
                {
                    Debug.LogWarning("[SkillTreeManager] Player가 null입니다");
                    return 0;
                }

                if (string.IsNullOrEmpty(skillId))
                {
                    Debug.LogWarning("[SkillTreeManager] skillId가 비어있습니다");
                    return 0;
                }

                string key = $"CaptainSkillTree_{skillId}";
                if (Player.m_localPlayer.m_customData.TryGetValue(key, out var str))
                {
                    if (int.TryParse(str, out int level))
                    {
                        // 레벨 유효성 검증
                        if (SkillNodes.ContainsKey(skillId))
                        {
                            var maxLevel = SkillNodes[skillId].MaxLevel;
                            if (level > maxLevel)
                            {
                                Debug.LogWarning($"[SkillTreeManager] 레벨 초과 감지 ({skillId}: {level} > {maxLevel}) - 수정");
                                level = maxLevel;
                                SetSkillLevel(skillId, level);
                            }
                        }
                        return Math.Max(0, level); // 음수 레벨 방지
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SkillTreeManager] 스킬 레벨 획득 실패 ({skillId}): {ex.Message}");
                return 0;
            }
        }

        // 안전한 스킬 레벨 설정
        public void SetSkillLevel(string skillId, int level)
        {
            try
            {
                if (Player.m_localPlayer == null)
                {
                    Debug.LogWarning("[SkillTreeManager] Player가 null입니다");
                    return;
                }

                if (string.IsNullOrEmpty(skillId))
                {
                    Debug.LogWarning("[SkillTreeManager] skillId가 비어있습니다");
                    return;
                }

                // 레벨 유효성 검증
                level = Math.Max(0, level); // 음수 방지
                if (SkillNodes.ContainsKey(skillId))
                {
                    level = Math.Min(level, SkillNodes[skillId].MaxLevel); // 최대 레벨 초과 방지
                }

                string key = $"CaptainSkillTree_{skillId}";
                Player.m_localPlayer.m_customData[key] = level.ToString();

                // 제작 스킬 캐시 시스템 제거됨 (새로운 제작 버튼 감지 시스템으로 대체)

                // 방어 스킬 변경 시 회피율 재계산 (성능 최적화: 변경 시에만)
                if (IsDefenseSkill(skillId))
                {
                    Plugin.Log.LogInfo($"[SkillTreeManager] 방어 스킬 변경 감지 ({skillId}) - UpdateDefenseDodgeRate 호출");
                    SkillEffect.UpdateDefenseDodgeRate(Player.m_localPlayer);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SkillTreeManager] 스킬 레벨 설정 실패 ({skillId}, {level}): {ex.Message}");
            }
        }

        /// <summary>
        /// 방어 스킬인지 확인 (회피율에 영향을 주는 스킬)
        /// </summary>
        private bool IsDefenseSkill(string skillId)
        {
            return skillId == "defense_root" ||
                   skillId == "defense_Step3_agile" ||
                   skillId == "defense_Step5_stamina" ||
                   skillId == "defense_Step6_attack" ||
                   skillId == "knife_step2_evasion"; // 단검 회피 숙련
        }

        public bool CanLevelUp(string skillId)
        {
            if (!SkillNodes.ContainsKey(skillId)) return false;
            var node = SkillNodes[skillId];
            
            // 1. 최대 레벨 체크
            int currentLevel = GetSkillLevel(skillId);
            if (currentLevel >= node.MaxLevel) return false;
            
            // 2. 기존 ItemRequirement 시스템으로 처리됨 (아래에서 일괄 처리)
            
            // 3. 생산 스킬은 아이템 요구사항, 일반 스킬은 포인트 체크
            if (SkillItemRequirements.IsProductionSkill(skillId))
            {
                // 생산 스킬: 아이템 요구사항 체크
                if (!ItemManager.CanUnlockSkill(skillId, out var missingItems))
                {
                    return false;
                }
            }
            else
            {
                // 일반 스킬: 포인트 체크 (대기 중인 투자 고려)
                if (GetAvailablePoints(true) < node.RequiredPoints) return false;
            }
            
            // 4. 전제조건 체크
            if (node.Prerequisites != null && node.Prerequisites.Count > 0)
            {
                // 특별 케이스: 장인(grandmaster_artisan)은 AND 조건 (모든 전제조건 필요)
                if (skillId == "grandmaster_artisan")
                {
                    foreach (var preId in node.Prerequisites)
                    {
                        if (GetSkillLevel(preId) <= 0)
                            return false;
                    }
                }
                else
                {
                    // 일반 케이스: OR 조건 (하나 이상의 전제조건만 만족하면 됨)
                    bool hasAnyPrerequisite = false;
                    foreach (var preId in node.Prerequisites)
                    {
                        if (GetSkillLevel(preId) > 0)
                        {
                            hasAnyPrerequisite = true;
                            break;
                        }
                    }
                    if (!hasAnyPrerequisite) return false;
                }
            }

            // 4-1. 상호 배타적 스킬 체크
            if (node.MutuallyExclusive != null && node.MutuallyExclusive.Count > 0)
            {
                foreach (var exclusiveSkillId in node.MutuallyExclusive)
                {
                    if (GetSkillLevel(exclusiveSkillId) > 0)
                    {
                        Plugin.Log.LogWarning($"[SkillTreeManager] Mutually exclusive skill restriction: {skillId} cannot be learned with {exclusiveSkillId}.");
                        return false;
                    }
                }
            }

            // 5. 액티브 스킬 제한 체크 (전문가 기반)
            if (!CanUnlockActiveSkill(skillId, out string restrictionMessage))
            {
                Plugin.Log.LogWarning($"[SkillTreeManager] 액티브 스킬 제한: {skillId} - {restrictionMessage}");
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 액티브 스킬 언락 제한 체크 (백업 방식: 상호 배타적 제한만)
        /// </summary>
        /// <param name="skillId">언락하려는 스킬 ID</param>
        /// <param name="restrictionMessage">제한 메시지</param>
        /// <returns>언락 가능하면 true</returns>
        private bool CanUnlockActiveSkill(string skillId, out string restrictionMessage)
        {
            restrictionMessage = "";

            // R키: 원거리 액티브 (1개만 선택 가능)
            var rKeySkills = new[] { "crossbow_Step6_expert", "bow_Step6_critboost", "staff_Step6_dual_cast" };

            // G키: 근접 메인 액티브 (같은 무기 트리만 허용)
            var gKeyMeleeSkills = new[] {
                "sword_step5_finalcut",       // 검: 돌진 연속 베기
                "knife_step9_assassin_heart", // 단검: 암살자의 심장
                "spear_Step5_penetrate",      // 창: 꿰뚫는 창
                "polearm_step5_king",         // 폴암: 장창의 제왕
                "mace_Step7_guardian_heart"   // 둔기: 수호자의 진심
            };

            // H키: 보조 액티브 (같은 무기 트리만 허용 - G키와 연동)
            var hKeySkills = new[] {
                "sword_step5_defswitch",      // 검: 패링 돌격
                "spear_Step5_combo",          // 창: 연공창
                "mace_Step7_fury_hammer",     // 둔기: 분노의 망치
                "staff_Step6_heal"            // 지팡이: 범위 힐
            };

            // Y키: 직업 액티브 (1개만 선택 가능)
            var yKeySkills = new[] { "Berserker", "Tanker", "Archer", "Rogue", "Mage", "Paladin" };

            // 액티브 스킬이 아니면 제한 없음
            if (!rKeySkills.Contains(skillId) && !gKeyMeleeSkills.Contains(skillId) &&
                !hKeySkills.Contains(skillId) && !yKeySkills.Contains(skillId))
            {
                return true;
            }

            // ========== R키 원거리 액티브 스킬 제한 (1개만 선택 가능) ==========
            if (rKeySkills.Contains(skillId))
            {
                var existingRKeySkills = rKeySkills.Where(skill => skill != skillId && GetSkillLevel(skill) > 0).ToList();
                if (existingRKeySkills.Count > 0)
                {
                    restrictionMessage = $"Only 1 ranged active skill allowed (current: {string.Join(", ", existingRKeySkills)})";
                    return false;
                }
                return true;
            }

            // ========== 무기별 G키/H키 스킬 그룹 정의 ==========
            var swordGSkills = new[] { "sword_step5_finalcut" };
            var swordHSkills = new[] { "sword_step5_defswitch" };
            var knifeGSkills = new[] { "knife_step9_assassin_heart" };
            var spearGSkills = new[] { "spear_Step5_penetrate" };
            var spearHSkills = new[] { "spear_Step5_combo" };
            var polearmGSkills = new[] { "polearm_step5_king" };
            var maceGSkills = new[] { "mace_Step7_guardian_heart" };
            var maceHSkills = new[] { "mace_Step7_fury_hammer" };
            var staffHSkills = new[] { "staff_Step6_heal" };

            // 무기 타입 판별 함수
            string GetWeaponType(string id)
            {
                if (swordGSkills.Contains(id) || swordHSkills.Contains(id)) return "검";
                if (knifeGSkills.Contains(id)) return "단검";
                if (spearGSkills.Contains(id) || spearHSkills.Contains(id)) return "창";
                if (polearmGSkills.Contains(id)) return "폴암";
                if (maceGSkills.Contains(id) || maceHSkills.Contains(id)) return "둔기";
                if (staffHSkills.Contains(id)) return "지팡이";
                return "";
            }

            // ========== G키 근접 메인 액티브 제한 (같은 무기 트리만) ==========
            if (gKeyMeleeSkills.Contains(skillId))
            {
                string currentWeaponType = GetWeaponType(skillId);

                // 다른 무기의 G키 스킬 체크
                var otherGSkills = gKeyMeleeSkills
                    .Where(s => s != skillId && GetSkillLevel(s) > 0 && GetWeaponType(s) != currentWeaponType)
                    .ToList();

                // 다른 무기의 H키 스킬 체크 (G키와 H키는 같은 무기 타입 공유)
                var otherHSkills = hKeySkills
                    .Where(s => GetSkillLevel(s) > 0 && GetWeaponType(s) != currentWeaponType)
                    .ToList();

                var conflictSkills = otherGSkills.Concat(otherHSkills).ToList();

                if (conflictSkills.Count > 0)
                {
                    restrictionMessage = $"Melee active skill from another weapon already learned (current: {string.Join(", ", conflictSkills)})";
                    return false;
                }

                return true;
            }

            // ========== H키 보조 액티브 제한 (같은 무기 트리만 - G키와 연동) ==========
            if (hKeySkills.Contains(skillId))
            {
                string currentWeaponType = GetWeaponType(skillId);

                // 다른 무기의 H키 스킬 체크
                var otherHSkills = hKeySkills
                    .Where(s => s != skillId && GetSkillLevel(s) > 0 && GetWeaponType(s) != currentWeaponType)
                    .ToList();

                // 다른 무기의 G키 스킬 체크 (H키와 G키는 같은 무기 타입 공유)
                var otherGSkills = gKeyMeleeSkills
                    .Where(s => GetSkillLevel(s) > 0 && GetWeaponType(s) != currentWeaponType)
                    .ToList();

                var conflictSkills = otherHSkills.Concat(otherGSkills).ToList();

                if (conflictSkills.Count > 0)
                {
                    restrictionMessage = $"Sub active skill from another weapon already learned (current: {string.Join(", ", conflictSkills)})";
                    return false;
                }

                return true;
            }

            // ========== Y키 직업 액티브 스킬 제한 (1개만 선택 가능) ==========
            if (yKeySkills.Contains(skillId))
            {
                var existingJobSkills = yKeySkills.Where(skill => skill != skillId && GetSkillLevel(skill) > 0).ToList();
                if (existingJobSkills.Count > 0)
                {
                    restrictionMessage = $"Only 1 job skill allowed (current: {string.Join(", ", existingJobSkills)})";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 임시 투자 시 액티브 스킬 언락 제한 체크 (pending 포함)
        /// </summary>
        private bool CanPendingUnlockActiveSkill(string skillId, out string restrictionMessage)
        {
            restrictionMessage = "";

            // 스킬 레벨 또는 pending 포함 체크 헬퍼
            bool HasSkillOrPending(string id)
            {
                int level = GetSkillLevel(id);
                int pending = pendingInvestments.ContainsKey(id) ? pendingInvestments[id] : 0;
                return level > 0 || pending > 0;
            }

            // R키: 원거리 액티브 (1개만 선택 가능)
            var rKeySkills = new[] { "crossbow_Step6_expert", "bow_Step6_critboost", "staff_Step6_dual_cast" };

            // G키: 근접 메인 액티브 (같은 무기 트리만 허용)
            var gKeyMeleeSkills = new[] {
                "sword_step5_finalcut",       // 검: 돌진 연속 베기
                "knife_step9_assassin_heart", // 단검: 암살자의 심장
                "spear_Step5_penetrate",      // 창: 꿰뚫는 창
                "polearm_step5_king",         // 폴암: 장창의 제왕
                "mace_Step7_guardian_heart"   // 둔기: 수호자의 진심
            };

            // H키: 보조 액티브 (같은 무기 트리만 허용 - G키와 연동)
            var hKeySkills = new[] {
                "sword_step5_defswitch",      // 검: 패링 돌격
                "spear_Step5_combo",          // 창: 연공창
                "mace_Step7_fury_hammer",     // 둔기: 분노의 망치
                "staff_Step6_heal"            // 지팡이: 범위 힐
            };

            // Y키: 직업 액티브 (1개만 선택 가능)
            var yKeySkills = new[] { "Berserker", "Tanker", "Archer", "Rogue", "Mage", "Paladin" };

            // 액티브 스킬이 아니면 제한 없음
            if (!rKeySkills.Contains(skillId) && !gKeyMeleeSkills.Contains(skillId) &&
                !hKeySkills.Contains(skillId) && !yKeySkills.Contains(skillId))
            {
                return true;
            }

            // ========== R키 원거리 액티브 스킬 제한 (1개만 선택 가능) ==========
            if (rKeySkills.Contains(skillId))
            {
                var existingRKeySkills = rKeySkills.Where(skill => skill != skillId && HasSkillOrPending(skill)).ToList();
                if (existingRKeySkills.Count > 0)
                {
                    restrictionMessage = $"Only 1 ranged active skill allowed";
                    return false;
                }
                return true;
            }

            // ========== 무기별 G키/H키 스킬 그룹 정의 ==========
            var swordGSkills = new[] { "sword_step5_finalcut" };
            var swordHSkills = new[] { "sword_step5_defswitch" };
            var knifeGSkills = new[] { "knife_step9_assassin_heart" };
            var spearGSkills = new[] { "spear_Step5_penetrate" };
            var spearHSkills = new[] { "spear_Step5_combo" };
            var polearmGSkills = new[] { "polearm_step5_king" };
            var maceGSkills = new[] { "mace_Step7_guardian_heart" };
            var maceHSkills = new[] { "mace_Step7_fury_hammer" };
            var staffHSkills = new[] { "staff_Step6_heal" };

            // 무기 타입 판별 함수
            string GetWeaponType(string id)
            {
                if (swordGSkills.Contains(id) || swordHSkills.Contains(id)) return "검";
                if (knifeGSkills.Contains(id)) return "단검";
                if (spearGSkills.Contains(id) || spearHSkills.Contains(id)) return "창";
                if (polearmGSkills.Contains(id)) return "폴암";
                if (maceGSkills.Contains(id) || maceHSkills.Contains(id)) return "둔기";
                if (staffHSkills.Contains(id)) return "지팡이";
                return "";
            }

            // ========== G키 근접 메인 액티브 제한 (같은 무기 트리만) ==========
            if (gKeyMeleeSkills.Contains(skillId))
            {
                string currentWeaponType = GetWeaponType(skillId);

                // 다른 무기의 G키 스킬 체크
                var otherGSkills = gKeyMeleeSkills
                    .Where(s => s != skillId && HasSkillOrPending(s) && GetWeaponType(s) != currentWeaponType)
                    .ToList();

                // 다른 무기의 H키 스킬 체크 (G키와 H키는 같은 무기 타입 공유)
                var otherHSkills = hKeySkills
                    .Where(s => HasSkillOrPending(s) && GetWeaponType(s) != currentWeaponType)
                    .ToList();

                var conflictSkills = otherGSkills.Concat(otherHSkills).ToList();

                if (conflictSkills.Count > 0)
                {
                    restrictionMessage = $"Active skill from another weapon already selected";
                    return false;
                }

                return true;
            }

            // ========== H키 보조 액티브 제한 (같은 무기 트리만 - G키와 연동) ==========
            if (hKeySkills.Contains(skillId))
            {
                string currentWeaponType = GetWeaponType(skillId);

                // 다른 무기의 H키 스킬 체크
                var otherHSkills = hKeySkills
                    .Where(s => s != skillId && HasSkillOrPending(s) && GetWeaponType(s) != currentWeaponType)
                    .ToList();

                // 다른 무기의 G키 스킬 체크 (H키와 G키는 같은 무기 타입 공유)
                var otherGSkills = gKeyMeleeSkills
                    .Where(s => HasSkillOrPending(s) && GetWeaponType(s) != currentWeaponType)
                    .ToList();

                var conflictSkills = otherHSkills.Concat(otherGSkills).ToList();

                if (conflictSkills.Count > 0)
                {
                    restrictionMessage = $"Active skill from another weapon already selected";
                    return false;
                }

                return true;
            }

            // ========== Y키 직업 액티브 스킬 제한 (1개만 선택 가능) ==========
            if (yKeySkills.Contains(skillId))
            {
                var existingJobSkills = yKeySkills.Where(skill => skill != skillId && HasSkillOrPending(skill)).ToList();
                if (existingJobSkills.Count > 0)
                {
                    restrictionMessage = $"Only 1 job class allowed";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 아이템을 소모하여 스킬을 언락
        /// </summary>
        /// <param name="skillId">언락할 스킬 ID</param>
        /// <returns>성공 시 true</returns>
        public bool UnlockSkillWithItems(string skillId)
        {
            if (!SkillNodes.ContainsKey(skillId))
            {
                Plugin.Log.LogWarning($"[SkillTreeManager] 존재하지 않는 스킬: {skillId}");
                return false;
            }

            var node = SkillNodes[skillId];
            
            // 1. 언락 가능한지 확인
            if (!CanLevelUp(skillId))
            {
                Plugin.Log.LogWarning($"[SkillTreeManager] 스킬 언락 조건 미충족: {skillId}");
                return false;
            }

            // 2. 기존 ItemRequirement 시스템으로 처리됨 (아래에서 일괄 처리)

            // 3. 생산 전문가 트리는 자원 소모, 일반 스킬은 포인트 소모
            if (SkillItemRequirements.IsProductionSkill(skillId))
            {
                // 생산 전문가 트리: 자원과 아이템 소모
                var player = Player.m_localPlayer;
                if (player == null || !ResourceConsumption.ConsumeResourcesForSkill(player, skillId))
                {
                    Plugin.Log.LogWarning($"[SkillTreeManager] 자원 소모 실패: {skillId}");
                    return false;
                }
            }
            else
            {
                // 일반 스킬: 포인트 소모 (기존 시스템)
                // 포인트는 스킬 레벨 증가 시 자동으로 차감됨
            }

            // 4. 스킬 레벨 증가
            int currentLevel = GetSkillLevel(skillId);
            SetSkillLevel(skillId, currentLevel + 1);

            // 5. 스킬 효과 적용
            try
            {
                node.ApplyEffect?.Invoke(currentLevel + 1);
                Plugin.Log.LogInfo($"[SkillTreeManager] 스킬 언락 성공: {skillId} (레벨 {currentLevel + 1})");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeManager] 스킬 효과 적용 실패 ({skillId}): {ex.Message}");
            }

            return true;
        }

        /// <summary>
        /// 생산 스킬 언락 시도
        /// </summary>
        public bool TryUnlockProductionSkill(string skillId)
        {
            try
            {
                Plugin.Log.LogInfo($"[TryUnlockProductionSkill] {skillId} 언락 시도");
                
                if (CanLearnProductionSkill(skillId))
                {
                    Plugin.Log.LogInfo($"[TryUnlockProductionSkill] {skillId} 조건 충족, 아이템 소모 시작");
                    
                    // 아이템 소모
                    var player = Player.m_localPlayer;
                    if (player != null)
                    {
                        ConsumeSkillRequirements(player, skillId);
                    }
                    
                    // 스킬 레벨 증가
                    int currentLevel = GetSkillLevel(skillId);
                    SetSkillLevel(skillId, currentLevel + 1);
                    
                    // 스킬 효과 적용
                    var node = SkillNodes[skillId];
                    node.ApplyEffect?.Invoke(currentLevel + 1);
                    
                    Plugin.Log.LogInfo($"[TryUnlockProductionSkill] {skillId} 언락 성공");
                    return true;
                }
                else
                {
                    Plugin.Log.LogInfo($"[TryUnlockProductionSkill] {skillId} 조건 미충족");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeManager] 생산 스킬 언락 실패 ({skillId}): {ex.Message}");
            }
            
            return false;
        }
        
        /// <summary>
        /// 생산 스킬 학습 가능 여부 확인 (ItemRequirement 시스템 기반)
        /// </summary>
        public bool CanLearnProductionSkill(string skillId)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return false;

                // 1. ItemRequirement 시스템을 통한 요구사항 확인
                if (SkillItemRequirements.IsProductionSkill(skillId))
                {
                    var requirements = SkillItemRequirements.GetRequirements(skillId);

                    if (requirements.Count > 0)
                    {
                        var inventory = player.GetInventory();
                        if (inventory == null) return false;

                        // 각 요구사항 확인
                        foreach (var req in requirements)
                        {
                            if (req is ItemEquipRequirement equipReq)
                            {
                                // 장착 조건 확인
                                if (!IsItemEquipped(player, equipReq.ItemName))
                                {
                                    return false;
                                }
                            }
                            else if (req is ItemEquipConsumeRequirement equipConsumeReq)
                            {
                                // 장착 후 소모 조건 확인 - 착용 여부만 체크 (소모는 스킬 학습 시)
                                if (!IsItemEquipped(player, equipConsumeReq.ItemName))
                                {
                                    return false;
                                }
                            }
                            else if (req is ItemQuantityRequirement qtyReq)
                            {
                                // 수량 조건 확인 - 스택된 아이템 올바르게 집계
                                int ownedCount = CountUnequippedItems(inventory, qtyReq.ItemName);
                                if (ownedCount < qtyReq.Quantity)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                // 일반 아이템 조건 확인 - ItemManager의 메서드 사용하여 스택된 아이템 올바르게 집계
                                int ownedCount = CountUnequippedItems(inventory, req.ItemName);

                                if (ownedCount < req.Quantity)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

                if (!SkillNodes.ContainsKey(skillId)) return false;

                var node = SkillNodes[skillId];

                // 2. 최대 레벨 체크
                int currentLevel = GetSkillLevel(skillId);
                if (currentLevel >= node.MaxLevel) return false;

                // 3. 전제조건 체크
                if (node.Prerequisites != null && node.Prerequisites.Count > 0)
                {
                    bool hasAnyPrerequisite = false;
                    foreach (var preId in node.Prerequisites)
                    {
                        int preLevel = GetSkillLevel(preId);
                        if (preLevel > 0)
                        {
                            hasAnyPrerequisite = true;
                            break;
                        }
                    }
                    if (!hasAnyPrerequisite) return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[생산 스킬 검증] 오류: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
        }
        
        /// <summary>
        /// 생산 스킬 검증 결과 메시지 가져오기
        /// </summary>
        public string GetProductionSkillValidationMessage(string skillId)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return "플레이어 정보 없음";
                
                // ItemRequirement 시스템을 통한 검증 메시지
                var requirements = SkillItemRequirements.GetRequirements(skillId);
                if (requirements.Count == 0)
                {
                    return "요구사항 없음";
                }
                
                var inventory = player.GetInventory();
                if (inventory == null) return "인벤토리 없음";
                
                var missingItems = new List<string>();
                foreach (var req in requirements)
                {
                    if (req is ItemEquipRequirement equipReq)
                    {
                        // 장착 조건 확인
                        bool isEquipped = IsItemEquipped(player, equipReq.ItemName);
                        if (!isEquipped)
                        {
                            missingItems.Add($"{equipReq.DisplayName} 착용 필요");
                        }
                    }
                    else if (req is ItemQuantityRequirement qtyReq)
                    {
                        // 수량 조건 확인
                        int ownedCount = inventory.CountItems(qtyReq.ItemName);
                        if (ownedCount < qtyReq.Quantity)
                        {
                            missingItems.Add($"{qtyReq.DisplayName} {ownedCount}/{qtyReq.Quantity}");
                        }
                    }
                    else
                    {
                        // 일반 아이템 조건 확인
                        int ownedCount = inventory.CountItems(req.ItemName);
                        if (ownedCount < req.Quantity)
                        {
                            missingItems.Add($"{req.DisplayName} {ownedCount}/{req.Quantity}");
                        }
                    }
                }
                
                return missingItems.Count > 0 ? string.Join(", ", missingItems) : "조건 충족";
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[생산 스킬 메시지] 오류: {ex.Message}");
                return "검증 중 오류 발생";
            }
        }

        public void LevelUp(string skillId)
        {
            if (!CanLevelUp(skillId)) return;

            int currentLevel = GetSkillLevel(skillId);
            var node = SkillNodes[skillId];
            
            // 생산 스킬의 경우 아이템 요구사항 처리
            if (SkillItemRequirements.IsProductionSkill(skillId))
            {
                var player = Player.m_localPlayer;
                if (player != null)
                {
                    ConsumeSkillRequirements(player, skillId);
                }
            }
            if (currentLevel >= node.MaxLevel) return; // 최대 레벨 초과 방지

            int newLevel = currentLevel + 1;
            SetSkillLevel(skillId, newLevel);
            node.ApplyEffect?.Invoke(newLevel);
        }

        // 포인트 임시 투자
        public void AddPendingInvestment(string skillId)
        {
            if (!SkillNodes.ContainsKey(skillId)) return;
            var node = SkillNodes[skillId];
            int currentLevel = GetSkillLevel(skillId) + (pendingInvestments.ContainsKey(skillId) ? pendingInvestments[skillId] : 0);
            if (currentLevel >= node.MaxLevel) return;

            if (node.RequiredPoints > 0 && GetAvailablePoints(true) < node.RequiredPoints) return;

            // === 상호 배타적 스킬 체크 ===
            if (node.MutuallyExclusive != null && node.MutuallyExclusive.Count > 0)
            {
                foreach (var exclusiveSkillId in node.MutuallyExclusive)
                {
                    int exclusiveLevel = GetSkillLevel(exclusiveSkillId);
                    int exclusivePending = pendingInvestments.ContainsKey(exclusiveSkillId) ? pendingInvestments[exclusiveSkillId] : 0;

                    if (exclusiveLevel > 0 || exclusivePending > 0)
                    {
                        // 이미 배타적 스킬에 투자된 상태
                        if ((System.Object)Player.m_localPlayer != null)
                        {
                            string exclusiveSkillName = SkillNodes.ContainsKey(exclusiveSkillId) ? SkillNodes[exclusiveSkillId].Name : exclusiveSkillId;
                            SkillEffect.DrawFloatingText(Player.m_localPlayer, "⚠️ " + L.Get("cannot_learn_with", exclusiveSkillName), Color.red);
                        }
                        Plugin.Log.LogDebug($"[상호 배타적 스킬] {skillId} 투자 차단: 이미 {exclusiveSkillId}에 투자됨");
                        return;
                    }
                }
            }

            // === 액티브 스킬 제한 체크 (R키/G키/H키/Y키) ===
            if (!CanPendingUnlockActiveSkill(skillId, out string restrictionMessage))
            {
                if ((System.Object)Player.m_localPlayer != null)
                {
                    SkillEffect.DrawFloatingText(Player.m_localPlayer, $"⚠️ {restrictionMessage}", Color.red);
                }
                Plugin.Log.LogDebug($"[액티브 스킬 제한] {skillId} 투자 차단: {restrictionMessage}");
                return;
            }

            if (pendingInvestments.ContainsKey(skillId))
            {
                pendingInvestments[skillId]++;
            }
            else
            {
                pendingInvestments[skillId] = 1;
            }
        }
        
        // 투자 확정
        public void ConfirmInvestments()
        {
            if (Player.m_localPlayer == null) return;
            
            // 직업 스킬 아이템 소모 처리 (확정 시에만)
            foreach (var pending in pendingInvestments)
            {
                int currentLevel = GetSkillLevel(pending.Key);
                
                // 직업 스킬이고 레벨 0에서 1로 올라가는 경우(처음 전직)에만 아이템 소모
                if (IsJobSkill(pending.Key) && currentLevel == 0)
                {
                    ConsumeJobSkillRequirements(pending.Key);
                }
                
                int newLevel = currentLevel + pending.Value;
                SetSkillLevel(pending.Key, newLevel);
                
                // 스킬 효과 적용 (누락된 기능 추가)
                if (SkillNodes.ContainsKey(pending.Key))
                {
                    var node = SkillNodes[pending.Key];
                    node.ApplyEffect?.Invoke(newLevel);
                }
            }
            pendingInvestments.Clear();
        }

        // 투자 취소
        public void CancelInvestments()
        {
            pendingInvestments.Clear();
        }
        
        /// <summary>
        /// 직업 스킬인지 확인
        /// </summary>
        private bool IsJobSkill(string skillId)
        {
            return skillId == "Paladin" || skillId == "Tanker" || skillId == "Berserker" || 
                   skillId == "Rogue" || skillId == "Mage" || skillId == "Archer";
        }
        
        /// <summary>
        /// 직업 스킬의 아이템 요구사항을 소모합니다
        /// </summary>
        private void ConsumeJobSkillRequirements(string skillId)
        {
            var player = Player.m_localPlayer;
            if (player == null) return;
            
            var inventory = player.GetInventory();
            if (inventory == null) return;
            
            if (skillId == "Paladin")
            {
                // Paladin: 특별 퀘스트완료 아이템 + 에이크쉬르 트로피 소모
                inventory.RemoveItem("$item_trophy_eikthyr", 1);
                
                // 특별 퀘스트완료 아이템 소모 (실제 아이템명으로 변경 필요)
                if (inventory.HaveItem("$item_questcomplete_paladin"))
                {
                    inventory.RemoveItem("$item_questcomplete_paladin", 1);
                }
                else if (inventory.HaveItem("$item_special_paladin"))
                {
                    inventory.RemoveItem("$item_special_paladin", 1);
                }
                else if (inventory.HaveItem("$item_coins"))
                {
                    // 임시로 코인 소모 (실제 구현 시 제거)
                    inventory.RemoveItem("$item_coins", 100);
                }
            }
            else if (IsJobSkill(skillId))
            {
                // 다른 직업들: 에이크쉬르 트로피만 소모
                inventory.RemoveItem("$item_trophy_eikthyr", 1);
            }
        }

        // 생산 전문가 스킬 ID 집합
        private static readonly HashSet<string> ProductionSkillIds = new HashSet<string>
        {
            "production_root", "novice_worker",
            "woodcutting_lv2", "woodcutting_lv3", "woodcutting_lv4",
            "gathering_lv2", "gathering_lv3", "gathering_lv4",
            "mining_lv2", "mining_lv3", "mining_lv4",
            "crafting_lv2", "crafting_lv3", "crafting_lv4"
        };

        // 스킬 포인트 초기화 로직
        public void ResetAllSkillLevels()
        {
            if (Player.m_localPlayer == null) return;
            foreach (var node in SkillNodes.Values)
            {
                string key = $"CaptainSkillTree_{node.Id}";
                Player.m_localPlayer.m_customData[key] = "0";
            }
            pendingInvestments.Clear();

            // 방어 스킬 초기화 시 회피율 리셋 (중요!)
            SkillEffect.UpdateDefenseDodgeRate(Player.m_localPlayer);
        }

        // 생산 전문가를 제외한 스킬 초기화 (UI 초기화 버튼용)
        public void ResetAllSkillLevelsExceptProduction()
        {
            if (Player.m_localPlayer == null) return;
            foreach (var node in SkillNodes.Values)
            {
                if (ProductionSkillIds.Contains(node.Id)) continue;
                string key = $"CaptainSkillTree_{node.Id}";
                Player.m_localPlayer.m_customData[key] = "0";
            }
            pendingInvestments.Clear();
            SkillEffect.UpdateDefenseDodgeRate(Player.m_localPlayer);
        }

        // 생산 전문가 스킬만 초기화
        public void ResetProductionSkillLevels()
        {
            if (Player.m_localPlayer == null) return;
            foreach (var nodeId in ProductionSkillIds)
            {
                string key = $"CaptainSkillTree_{nodeId}";
                Player.m_localPlayer.m_customData[key] = "0";
            }
            pendingInvestments.Clear();
        }

        // 전체 투자 포인트 합계 반환
        public int GetTotalUsedPoints()
        {
            if (Player.m_localPlayer == null) return 0;

            int totalPoints = 0;
            foreach (var node in SkillNodes.Values)
            {
                // 생산 전문가 스킬은 자원 소모 방식이므로 포인트 계산에서 제외
                if (SkillItemRequirements.IsProductionSkill(node.Id)) continue;

                int level = GetSkillLevel(node.Id);
                if (level > 0)
                {
                    totalPoints += level * node.RequiredPoints;
                }
            }
            return totalPoints;
        }
        // 전체 최대 포인트: (레벨) * 레벨당 포인트 + 보너스 포인트
        // EpicMMO 사용 시: EpicMMO 레벨 기준 (+ 백업 저장)
        // EpicMMO 없을 때: 백업 레벨 기준 (순환 참조 방지 - 계산하지 않음)
        // 핵심 공식: 최대 스킬 포인트 = (저장된 레벨 × 레벨당 포인트) + 보너스 포인트
        // 보너스 포인트는 레벨 계산에 영향 없음!
        public int GetTotalMaxPoints()
        {
            int pointsPerLevel = CaptainLevelConfig.SkillPointsPerLevel?.Value ?? 2;

            // 보너스 포인트 가져오기 (skilladd 명령어로 추가된 포인트)
            int bonusPoints = 0;
            if (Player.m_localPlayer != null)
            {
                bonusPoints = CaptainSkillTree.SkillTree.SkillAddCommand.GetBonusSkillPoints(Player.m_localPlayer);
            }

            // CaptainMMOBridge.GetLevel()을 사용하여 통합 처리
            // - EpicMMO 있으면: EpicMMO 레벨 반환 + 백업 저장
            // - EpicMMO 없으면: 백업 레벨 반환 (계산하지 않음, 순환 참조 방지)
            int level = CaptainMMOBridge.GetLevel();
            int basePoints = Mathf.Max(0, level * pointsPerLevel);

            int totalMaxPoints = basePoints + bonusPoints;
            Plugin.Log.LogDebug($"[SkillTreeManager] GetTotalMaxPoints: 레벨={level}, 기본포인트={basePoints}, 보너스={bonusPoints}, 총={totalMaxPoints}");

            return totalMaxPoints;
        }

        // MMO 시스템에서 현재 캐릭터 레벨을 가져옴
        // CaptainMMOBridge를 통해 EpicMMO 또는 자체 시스템에서 자동 선택
        public int GetCurrentLevel() {
            try
            {
                // CaptainMMOBridge 통합 API 사용 (EpicMMO 또는 자체 시스템 자동 선택)
                return CaptainMMOBridge.GetLevel();
            }
            catch (System.Exception e)
            {
                Plugin.Log.LogWarning($"[SkillTree] CaptainMMOBridge.GetLevel() 실패: {e.Message}");

                try
                {
                    // Fallback 1: EpicMMO 리플렉션 헬퍼 사용
                    if (EpicMMOReflectionHelper.IsAvailable)
                        return EpicMMOReflectionHelper.GetLevel();
                }
                catch { }

                // Fallback 3: 자체 시스템 직접 접근
                if (CaptainLevelSystem.Instance != null && CaptainLevelSystem.Instance.IsInitialized)
                {
                    return CaptainLevelSystem.Instance.Level;
                }

                // 최종 기본값
                return 1;
            }
        }
        // 스킬포인트 = MMO 레벨 * 2 (기본) + 추가 포인트 + 보너스 포인트
        public int GetSkillPoints()
        {
            int basePoints = GetCurrentLevel() * 2; // MMO 레벨 기반 기본 포인트
            int additionalPoints = Plugin.SkillTreePoint; // 추가 획득 포인트
            int bonusPoints = GetBonusSkillPoints(); // skilladd로 추가된 보너스 포인트
            
            return Mathf.Max(2, basePoints + additionalPoints + bonusPoints); // 최소 2포인트 보장
        }
        
        /// <summary>
        /// 현재 플레이어의 보너스 스킬 포인트 가져오기 (skilladd 명령어로 추가된 포인트)
        /// </summary>
        /// <returns>보너스 스킬 포인트</returns>
        public int GetBonusSkillPoints()
        {
            try
            {
                if (Player.m_localPlayer == null || Player.m_localPlayer.m_customData == null)
                {
                    return 0;
                }
                
                string key = "CaptainSkillTree_BonusPoints";
                if (Player.m_localPlayer.m_customData.TryGetValue(key, out var str) && 
                    int.TryParse(str, out int bonusPoints))
                {
                    return Math.Max(0, bonusPoints); // 음수 방지
                }
                
                return 0;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeManager] 보너스 포인트 가져오기 실패: {ex.Message}");
                return 0;
            }
        }
        // 사용가능 스킬포인트 = 전체 스킬포인트 - 이미 투자된 포인트 합계
        public int GetAvailablePoints(bool considerPending = false)
        {
            int pendingPoints = 0;
            if (considerPending)
            {
                foreach (var pending in pendingInvestments)
                {
                    var node = SkillNodes[pending.Key];
                    pendingPoints += pending.Value * node.RequiredPoints;
                }
            }
            return GetSkillPoints() - GetTotalUsedPoints() - pendingPoints;
        }

        // 스킬포인트 획득(예: 레벨업 시 3점 증가)
        public void AddSkillPoints(int amount) {
            // SkillPoints += amount; // 이 방식은 더 이상 사용하지 않음
        }

        // 모든 노드의 투자 레벨을 m_customData에서 불러와 SkillTreeManager에 반영
        public void LoadAllSkillLevelsFromCustomData()
        {
            if (Player.m_localPlayer == null) return;
            foreach (var node in SkillNodes.Values)
            {
                string key = $"CaptainSkillTree_{node.Id}";
                if (!Player.m_localPlayer.m_customData.ContainsKey(key))
                    Player.m_localPlayer.m_customData[key] = "0";
            }
        }

        private string GetPlayerId()
        {
            return (System.Object)Player.m_localPlayer != null ? Player.m_localPlayer.GetPlayerName() : "Unknown";
        }

        private string GetSavePath()
        {
            string dir = Path.Combine(Application.dataPath, "../CaptainSkillTree/Data/PlayerSkillTree");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            return Path.Combine(dir, GetPlayerId() + ".json");
        }

        private bool IsSinglePlayer()
        {
            return ZNet.instance == null || ZNet.instance.IsServer();
        }

        private void SaveSkillTreeToFile()
        {
            try
            {
                var data = new Dictionary<string, int>();
                foreach (var node in SkillNodes.Values)
                {
                    int level = GetSkillLevel(node.Id);
                    data[node.Id] = level;
                }
                // 간단한 문자열 직렬화 (BepInEx 환경용)
                var sb = new StringBuilder();
                foreach (var kvp in data)
                {
                    sb.AppendLine($"{kvp.Key}={kvp.Value}");
                }
                File.WriteAllText(GetSavePath(), sb.ToString(), Encoding.UTF8);
                // Plugin.Log.LogInfo($"[SkillTree] 스킬 데이터 저장 완료");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[SkillTree] 저장 실패: {e.Message}");
            }
        }

        // 간단한 문자열 파싱 메서드 (BepInEx 환경용)
        private Dictionary<string, int> ParseSkillData(string content)
        {
            var result = new Dictionary<string, int>();
            if (string.IsNullOrEmpty(content)) return result;

            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && int.TryParse(parts[1], out int value))
                {
                    result[parts[0]] = value;
                }
            }
            return result;
        }

        private void LoadSkillTreeFromFile()
        {
            try
            {
                string path = GetSavePath();
                if (!File.Exists(path)) return;
                string content = File.ReadAllText(path, Encoding.UTF8);
                var data = ParseSkillData(content);
                if (data == null || data.Count == 0) return;
                foreach (var kv in data)
                {
                    SetSkillLevel(kv.Key, kv.Value);
                }
                // Plugin.Log.LogInfo($"[SkillTree] 스킬 데이터 로드 완료");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[SkillTree] 로드 실패: {e.Message}");
            }
        }

        // === 힐러 이펙트/사운드 코루틴 ===
        public System.Collections.IEnumerator PlayHealCastAndEffect(Player caster, List<Player> targets)
        {
#if !NO_JOTUNN
            // 1. 시전 애니메이션(있으면)
            var animator = caster.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("Heal"); // 트리거명은 실제 애니메이터에 맞게

            // 2. 시전 시작 사운드
            var sfxStartPrefab = PrefabManager.Instance.GetPrefab("sfx_greydwarf_shaman_heal_start");
            if (sfxStartPrefab != null)
                GameObject.Instantiate(sfxStartPrefab, caster.transform.position, Quaternion.identity);

            // 3. 시전 이펙트(캐스팅 중)
            var vfx = PrefabManager.Instance.GetPrefab("vfx_greydwarf_shaman_heal");
            GameObject vfxObj = null;
            if (vfx != null)
                vfxObj = GameObject.Instantiate(vfx, caster.transform.position, Quaternion.identity);

            // 4. 루프 사운드(시전 중)
            var sfxLoopPrefab = PrefabManager.Instance.GetPrefab("sfx_greydwarf_shaman_heal_loop");
            GameObject loopObj = null;
            if (sfxLoopPrefab != null)
                loopObj = GameObject.Instantiate(sfxLoopPrefab, caster.transform.position, Quaternion.identity);

            // 5. 1.5초간 시전(캐스팅)
            yield return new WaitForSeconds(1.5f);

            // 6. 시전 종료 사운드
            var sfxEndPrefab = PrefabManager.Instance.GetPrefab("sfx_greydwarf_shaman_heal_end");
            if (sfxEndPrefab != null)
                GameObject.Instantiate(sfxEndPrefab, caster.transform.position, Quaternion.identity);

            // 7. 이펙트/루프 사운드 제거(필요시)
            if (vfxObj != null)
                GameObject.Destroy(vfxObj, 2f);
            if (loopObj != null)
                GameObject.Destroy(loopObj, 0.5f);

            // 8. 힐 효과 적용(즉시+도트힐)
            foreach (var player in targets)
            {
                player.Heal(player.GetMaxHealth() * 0.2f);
                // 도트힐 타이머/상태 갱신(앞서 설계한 방식)
            }
#else
            yield break;
#endif
        }

        // === 힐러 모드 진입 ===
        // 힐러 모드 관련 함수들은 ActiveSkills.cs로 이동됨
        // === 마법 공격력 -50% 버프 적용 ===
        private void ApplyMagicAttackPenalty(bool on)
        {
            // 실제 마법 공격력 계산에 패널티 적용(구현 필요)
            // 예시: SkillEffect.SetMagicAttackPenalty(on ? HealMagicAtkPenalty : 1f);
        }
        // === staff_heal 스킬 보유 여부 ===
        private bool HasStaffHealSkill()
        {
            return GetSkillLevel("staff_Step6_heal") > 0;
        }
        // === 최적화된 입력 처리 시스템 ===
        private float lastStatusUpdateCheck = 0f;
        private float lastInputCheck = 0f;
        private const float STATUS_UPDATE_INTERVAL = 0.1f; // 100ms - 상태 체크
        private const float INPUT_CHECK_INTERVAL = 0.02f; // 20ms - 키 입력 체크
        
        public void OnUpdate()
        {
            // 키 입력은 높은 빈도로 체크 (반응성 개선)
            if (Time.time - lastInputCheck >= INPUT_CHECK_INTERVAL)
            {
                lastInputCheck = Time.time;
                HandleKeyInputs();
            }
            
            // 상태 업데이트는 낮은 빈도로 체크 (성능 최적화)
            if (Time.time - lastStatusUpdateCheck >= STATUS_UPDATE_INTERVAL)
            {
                lastStatusUpdateCheck = Time.time;
                UpdateActiveSkillStates();
                // UpdateHealerMode는 ActiveSkills.cs로 이동됨
            }
        }
        
        /// <summary>
        /// 키 입력 처리 (높은 반응성)
        /// </summary>
        private void HandleKeyInputs()
        {
            // Y키 처리는 SkillTreeInputListener에서 HandleActiveSkillKeyInput()으로 위임됨 - 중복 제거됨
            
            // 액티브 스킬 키 입력 처리는 SkillTreeInputListener에서 직접 처리됨
            // HandleActiveSkillKeyInput();
            
            // F3/F4 키 처리는 ActiveSkills.cs로 이동됨
            /*
            if (Input.GetKeyDown(KeyCode.F3))
            {
                // 힐러 모드 토글 - ActiveSkills.cs에서 처리
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                // 파티 힐 - ActiveSkills.cs에서 처리
            }
            */
            if (bowMultiShotActive && Time.time > bowMultiShotEndTime)
            {
                bowMultiShotActive = false;
                Debug.Log("[SkillTreeManager] 멀티샷 종료");
                SkillEffect.DrawFloatingText(Player.m_localPlayer, L.Get("multishot_end"));
            }
        }

        public bool IsBowMultiShotActive() => bowMultiShotActive && Time.time < bowMultiShotEndTime;
        
        // === 직업별 Y키 액티브 스킬 시스템 ===
        
        /// <summary>
        /// 액티브 스킬 상태 업데이트 (성능 최적화됨)
        /// </summary>
        private void UpdateActiveSkillStates()
        {
            var player = Player.m_localPlayer;
            
            // 액티브 스킬 상태 체크는 ActiveSkills.cs로 이동됨
            
            // JobSkills 상태 업데이트 호출
            if (player != null)
            {
                JobSkills.UpdateJobSkillStates(player);
            }
        }
        
        
        
        // === 직업 스킬 함수들은 JobSkills.cs로 이동됨 ===
        
        // 필수 변수들 (실제 사용되는 것들만 유지)
        private bool healerModeActive = false;
        private float healerModeEndTime = 0f;
        private const float HealerModeDuration = 600f;
        private const float HealCooldown = 5f;
        private const float HealEitrCost = 25f;
        private const float HealInstantPercent = 0.2f;
        private const float HealRange = 5f;
        
        private bool bowMultiShotActive = false;
        private float bowMultiShotEndTime = 0f;
        
        private void TryPaladinHeal()
        {
            // 함수 내용은 JobSkills.cs로 이동됨
        }
        
        private void UpdateHealerMode()
        {
            // 함수 내용은 ActiveSkills.cs로 이동됨
        }
        
        public bool IsHealerModeActive()
        {
            return healerModeActive && Time.time < healerModeEndTime;
        }
        
        // 원래 함수 내용들은 JobSkills.cs와 ActiveSkills.cs로 이동됨
        private void TryTankTaunt() { }
        private void TryBerserkerRage() { }  
        private void TryRogueShadow() { }
        private void TryMageBurst() { }
        private void TryArcherSpeed() { }
        
        // 누락된 함수들 추가 (빈 구현)
        public void HandleActiveSkillKeyInput()
        {
            // Y키 직업 액티브 스킬 처리 - JobSkills로 위임
            var player = Player.m_localPlayer;
            if (player != null && !player.IsDead())
            {
                JobSkills.ExecuteJobSkill(player);
            }
        }
        
        private void TryActivateHealerMode()
        {
            // 힐러 모드 활성화 - ActiveSkills로 이동됨
        }
        
        private void TryHealParty()
        {
            // 파티 힐 - ActiveSkills로 이동됨
        }
        
        public ActiveSkillValidationResult ValidateActiveSkillLearning(string skillId)
        {
            // 액티브 스킬 학습 제한 검증
            
            // 원거리 전문가 액티브 스킬 제한 (1개만 선택 가능)
            var rangedActiveSkills = new List<string> { "bow_Step6_critboost", "crossbow_Step6_expert" };
            if (rangedActiveSkills.Contains(skillId))
            {
                int learnedCount = 0;
                foreach (var rangedSkill in rangedActiveSkills)
                {
                    if (GetSkillLevel(rangedSkill) > 0 || pendingInvestments.ContainsKey(rangedSkill))
                    {
                        learnedCount++;
                    }
                }
                
                if (learnedCount >= 1)
                {
                    return new ActiveSkillValidationResult(false, true, "원거리 전문가 액티브 스킬은 1개만 선택할 수 있습니다.");
                }
            }
            
            // 근접 전문가 액티브 스킬 - 같은 무기 전문가 내에서만 여러 개 습득 가능
            var swordSkills = new List<string> { "sword_step5_finalcut", "sword_step5_defswitch" };
            var knifeSkills = new List<string> { "knife_step9_assassin_heart" };
            var spearSkills = new List<string> { "spear_Step5_combo", "spear_Step5_penetrate" };
            var polearmSkills = new List<string> { "polearm_step5_king" };
            var maceSkills = new List<string> { "mace_Step7_fury_hammer", "mace_Step7_guardian_heart" };

            var allMeleeActiveSkills = new List<string>();
            allMeleeActiveSkills.AddRange(swordSkills);
            allMeleeActiveSkills.AddRange(knifeSkills);
            allMeleeActiveSkills.AddRange(spearSkills);
            allMeleeActiveSkills.AddRange(polearmSkills);
            allMeleeActiveSkills.AddRange(maceSkills);

            if (allMeleeActiveSkills.Contains(skillId))
            {
                // 현재 스킬의 무기 타입 확인
                List<string> currentWeaponSkills = null;

                if (swordSkills.Contains(skillId)) currentWeaponSkills = swordSkills;
                else if (knifeSkills.Contains(skillId)) currentWeaponSkills = knifeSkills;
                else if (spearSkills.Contains(skillId)) currentWeaponSkills = spearSkills;
                else if (polearmSkills.Contains(skillId)) currentWeaponSkills = polearmSkills;
                else if (maceSkills.Contains(skillId)) currentWeaponSkills = maceSkills;

                // 다른 무기 타입의 스킬이 습득되어 있는지 확인
                foreach (var skill in allMeleeActiveSkills)
                {
                    if (currentWeaponSkills != null && !currentWeaponSkills.Contains(skill))
                    {
                        if (GetSkillLevel(skill) > 0 || pendingInvestments.ContainsKey(skill))
                        {
                            return new ActiveSkillValidationResult(false, true, $"다른 무기의 근접 액티브 스킬이 이미 습득됨");
                        }
                    }
                }
            }

            // 지팡이 전문가는 2개 선택 가능 (예외 처리)
            // staff_Step6_dual_cast + staff_Step6_heal 모두 가능
            // mace_Step7_fury_hammer + defense_Step7_guardian_heart 모두 가능
            
            // 액티브 스킬 학습 허용
            return new ActiveSkillValidationResult(true, false, "");
        }
        
        public void OnGUIShowMessage()
        {
            // GUI 메시지 표시 - 필요시 구현
        }
        private List<Player> GetPlayersInRange(Player center, float range) { return new List<Player>(); }
        
        /// <summary>
        /// 아이템이 장착되어 있는지 확인 (ItemManager와 동일한 로직 사용)
        /// </summary>
        private bool IsItemEquipped(Player player, string itemName)
        {
            try
            {
                var inventory = player.GetInventory();
                if (inventory == null) return false;
                
                Plugin.Log.LogInfo($"[SkillTreeManager 장착 확인] {itemName} 착용 여부 확인 시작");
                
                // 인벤토리의 모든 아이템을 확인하여 장착된 것만 찾기
                var allItems = inventory.GetAllItems();
                foreach (var item in allItems)
                {
                    if (item != null && item.m_equipped)
                    {
                        string dropPrefabName = item.m_dropPrefab?.name;
                        string sharedName = item.m_shared?.m_name;
                        
                        Plugin.Log.LogInfo($"[SkillTreeManager 장착 확인] 착용 중인 아이템: 프리팹='{dropPrefabName}', 표시명='{sharedName}'");
                        
                        // 프리팹명 우선 비교 (정확한 매칭)
                        if (dropPrefabName == itemName)
                        {
                            Plugin.Log.LogInfo($"[SkillTreeManager 장착 확인] 착용 중인 아이템 발견 (프리팹): {itemName} -> {dropPrefabName}");
                            return true;
                        }
                        
                        // 백업: 표시명으로 비교
                        if (sharedName == itemName)
                        {
                            Plugin.Log.LogInfo($"[SkillTreeManager 장착 확인] 착용 중인 아이템 발견 (표시명): {itemName} -> {sharedName}");
                            return true;
                        }
                    }
                }
                
                Plugin.Log.LogInfo($"[SkillTreeManager 장착 확인] 착용 중이지 않음: {itemName}");
                return false;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeManager 장착 확인] 오류: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 착용하지 않은 아이템 개수 계산 (스택된 아이템 올바르게 집계)
        /// </summary>
        private int CountUnequippedItems(Inventory inventory, string itemName)
        {
            if (inventory == null) return 0;

            int count = 0;
            foreach (var item in inventory.GetAllItems())
            {
                if (item != null && !item.m_equipped)
                {
                    // Valheim 내부 이름 형식으로 매칭 (shared.name 우선, dropPrefab도 확인)
                    if (item.m_shared?.m_name == itemName || item.m_dropPrefab?.name == itemName)
                    {
                        count += item.m_stack;
                    }
                }
            }

            Plugin.Log.LogDebug($"[아이템 계산] {itemName}: 착용하지 않은 개수 {count}개");
            return count;
        }

        /// <summary>
        /// 스킬 요구사항 아이템 소모
        /// </summary>
        private void ConsumeSkillRequirements(Player player, string skillId)
        {
            try
            {
                Plugin.Log.LogInfo($"[ConsumeSkillRequirements] {skillId} 아이템 소모 시작");
                
                var requirements = SkillItemRequirements.GetRequirements(skillId);
                if (requirements.Count == 0) 
                {
                    Plugin.Log.LogInfo($"[ConsumeSkillRequirements] {skillId} 요구사항 없음");
                    return;
                }
                
                var inventory = player.GetInventory();
                if (inventory == null) 
                {
                    Plugin.Log.LogWarning($"[ConsumeSkillRequirements] 인벤토리 없음");
                    return;
                }
                
                Plugin.Log.LogInfo($"[ConsumeSkillRequirements] {skillId} 요구사항 개수: {requirements.Count}");
                
                foreach (var req in requirements)
                {
                    Plugin.Log.LogInfo($"[ConsumeSkillRequirements] 요구사항 처리: {req.ItemName} x{req.Quantity}, IsConsumed={req.IsConsumed}, Type={req.GetType().Name}");
                    
                    if (req is ItemEquipConsumeRequirement equipConsumeReq)
                    {
                        // 착용된 아이템 소모 처리
                        Plugin.Log.LogInfo($"[ConsumeSkillRequirements] 착용된 아이템 소모: {equipConsumeReq.ItemName}");
                        ConsumeEquippedItem(player, inventory, equipConsumeReq);
                    }
                    else if (req.IsConsumed && !(req is ItemEquipRequirement) && !(req is ItemQuantityRequirement))
                    {
                        Plugin.Log.LogInfo($"[ConsumeSkillRequirements] {req.ItemName} x{req.Quantity} 소모 시작");
                        
                        // 소모 전 개수 확인
                        int beforeCount = CountUnequippedItems(inventory, req.ItemName);
                        Plugin.Log.LogInfo($"[ConsumeSkillRequirements] 소모 전 {req.ItemName} 개수: {beforeCount}");
                        
                        try
                        {
                            // 방법 1: 기본 RemoveItem 시도
                            Plugin.Log.LogInfo($"[ConsumeSkillRequirements] 방법 1: RemoveItem(\"{req.ItemName}\", {req.Quantity}) 시도");
                            inventory.RemoveItem(req.ItemName, req.Quantity);
                            
                            // 소모 후 개수 확인
                            int afterCount = CountUnequippedItems(inventory, req.ItemName);
                            Plugin.Log.LogInfo($"[ConsumeSkillRequirements] 방법 1 후 {req.ItemName} 개수: {afterCount}");
                            
                            // 실제로 개수가 줄었는지 확인
                            if (afterCount < beforeCount)
                            {
                                Plugin.Log.LogInfo($"[스킬 요구사항] {req.DisplayName} x{req.Quantity} 소모 성공 (전: {beforeCount}, 후: {afterCount})");
                                
                                // 플레이어에게 메시지 표시
                                SkillEffect.DrawFloatingText(player, 
                                    $"📦 {req.DisplayName} x{req.Quantity} 소모", 
                                    Color.red);
                            }
                            else
                            {
                                Plugin.Log.LogWarning($"[ConsumeSkillRequirements] 방법 1 실패, 방법 2 시도: shared.name으로 제거");
                                
                                // 방법 2: shared.name으로 시도
                                var woodItems = inventory.GetAllItems().Where(item => 
                                    item != null && 
                                    !item.m_equipped &&
                                    item.m_dropPrefab?.name == req.ItemName).ToList();
                                
                                Plugin.Log.LogInfo($"[ConsumeSkillRequirements] 방법 2: 발견된 {req.ItemName} 아이템 {woodItems.Count}개");
                                
                                int remainingToRemove = req.Quantity;
                                foreach (var item in woodItems)
                                {
                                    if (remainingToRemove <= 0) break;
                                    
                                    int removeFromThisStack = Mathf.Min(item.m_stack, remainingToRemove);
                                    Plugin.Log.LogInfo($"[ConsumeSkillRequirements] 스택에서 {removeFromThisStack}개 제거 시도");
                                    
                                    inventory.RemoveItem(item, removeFromThisStack);
                                    remainingToRemove -= removeFromThisStack;
                                }
                                
                                // 최종 확인
                                int finalCount = CountUnequippedItems(inventory, req.ItemName);
                                Plugin.Log.LogInfo($"[ConsumeSkillRequirements] 방법 2 후 최종 개수: {finalCount}");
                                
                                if (finalCount < beforeCount)
                                {
                                    Plugin.Log.LogInfo($"[스킬 요구사항] {req.DisplayName} x{req.Quantity} 소모 성공 (방법 2)");
                                    
                                    // 플레이어에게 메시지 표시
                                    SkillEffect.DrawFloatingText(player, 
                                        $"📦 {req.DisplayName} x{req.Quantity} 소모", 
                                        Color.red);
                                }
                                else
                                {
                                    Plugin.Log.LogError($"[스킬 요구사항] {req.DisplayName} x{req.Quantity} 소모 완전 실패");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Plugin.Log.LogWarning($"[스킬 요구사항] {req.DisplayName} x{req.Quantity} 소모 실패: {ex.Message}");
                        }
                    }
                    else
                    {
                        Plugin.Log.LogInfo($"[ConsumeSkillRequirements] {req.ItemName} 소모 안함 (IsConsumed={req.IsConsumed}, Type={req.GetType().Name})");
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬 요구사항 소모] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 착용된 아이템을 소모하는 메서드
        /// </summary>
        private void ConsumeEquippedItem(Player player, Inventory inventory, ItemEquipConsumeRequirement req)
        {
            try
            {
                Plugin.Log.LogInfo($"[ConsumeEquippedItem] {req.ItemName} 착용된 아이템 소모 시작");
                
                // 인벤토리의 모든 아이템을 확인하여 착용된 것 찾기
                var allItems = inventory.GetAllItems();
                ItemDrop.ItemData equippedItem = null;
                
                foreach (var item in allItems)
                {
                    if (item != null && item.m_equipped)
                    {
                        string dropPrefabName = item.m_dropPrefab?.name;
                        string sharedName = item.m_shared?.m_name;
                        
                        Plugin.Log.LogInfo($"[ConsumeEquippedItem] 착용 중인 아이템 확인: 프리팹='{dropPrefabName}', 표시명='{sharedName}'");
                        
                        // 프리팹명 우선 비교
                        if (dropPrefabName == req.ItemName || sharedName == req.ItemName)
                        {
                            equippedItem = item;
                            Plugin.Log.LogInfo($"[ConsumeEquippedItem] 소모할 착용 아이템 발견: {req.ItemName}");
                            break;
                        }
                    }
                }
                
                if (equippedItem != null)
                {
                    // 착용 해제
                    Plugin.Log.LogInfo($"[ConsumeEquippedItem] 아이템 착용 해제 중: {equippedItem.m_shared?.m_name}");
                    player.UnequipItem(equippedItem, false);
                    
                    // 아이템 제거
                    Plugin.Log.LogInfo($"[ConsumeEquippedItem] 아이템 인벤토리에서 제거 중");
                    inventory.RemoveItem(equippedItem);
                    
                    Plugin.Log.LogInfo($"[ConsumeEquippedItem] {req.DisplayName} 착용 아이템 소모 성공");
                    
                    // 플레이어에게 메시지 표시
                    SkillEffect.DrawFloatingText(player, 
                        $"🗡️ {req.DisplayName} 소모", 
                        Color.red);
                }
                else
                {
                    Plugin.Log.LogWarning($"[ConsumeEquippedItem] 착용된 {req.ItemName} 아이템을 찾을 수 없음");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[ConsumeEquippedItem] 오류: {ex.Message}");
            }
        }
        
    } // SkillTreeManager 클래스 끝
}

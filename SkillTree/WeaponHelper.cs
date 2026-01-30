namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 무기 타입 확인용 공통 헬퍼 클래스
    /// 여러 스킬 파일에서 중복되는 무기 체크 로직을 공통화
    /// </summary>
    public static class WeaponHelper
    {
        /// <summary>
        /// 플레이어가 특정 스킬 타입의 무기를 사용 중인지 확인
        /// </summary>
        /// <param name="player">플레이어 인스턴스</param>
        /// <param name="skillType">확인할 무기 스킬 타입</param>
        /// <returns>해당 무기 사용 중이면 true</returns>
        public static bool IsUsingWeapon(Player player, Skills.SkillType skillType)
        {
            if (player == null) return false;

            var weapon = player.GetCurrentWeapon();
            if (weapon?.m_shared == null) return false;

            return weapon.m_shared.m_skillType == skillType;
        }

        /// <summary>
        /// 검 사용 여부 확인
        /// </summary>
        public static bool IsUsingSword(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Swords);

        /// <summary>
        /// 둔기 사용 여부 확인
        /// </summary>
        public static bool IsUsingMace(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Clubs);

        /// <summary>
        /// 창 사용 여부 확인
        /// </summary>
        public static bool IsUsingSpear(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Spears);

        /// <summary>
        /// 폴암 사용 여부 확인
        /// </summary>
        public static bool IsUsingPolearm(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Polearms);

        /// <summary>
        /// 단검 사용 여부 확인
        /// </summary>
        public static bool IsUsingKnife(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Knives);

        /// <summary>
        /// 활 사용 여부 확인
        /// </summary>
        public static bool IsUsingBow(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Bows);

        /// <summary>
        /// 석궁 사용 여부 확인
        /// </summary>
        public static bool IsUsingCrossbow(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Crossbows);

        /// <summary>
        /// 도끼 사용 여부 확인
        /// </summary>
        public static bool IsUsingAxe(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Axes);

        /// <summary>
        /// 곡괭이 사용 여부 확인
        /// </summary>
        public static bool IsUsingPickaxe(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Pickaxes);

        /// <summary>
        /// 맨손 사용 여부 확인 (클로 포함)
        /// </summary>
        public static bool IsUsingUnarmed(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.Unarmed);

        /// <summary>
        /// 원소 마법 (지팡이) 사용 여부 확인
        /// </summary>
        public static bool IsUsingElementalMagic(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.ElementalMagic);

        /// <summary>
        /// 피 마법 (지팡이) 사용 여부 확인
        /// </summary>
        public static bool IsUsingBloodMagic(Player player) =>
            IsUsingWeapon(player, Skills.SkillType.BloodMagic);

        /// <summary>
        /// 지팡이 또는 완드 사용 여부 확인 (ElementalMagic 또는 BloodMagic)
        /// </summary>
        public static bool IsUsingStaffOrWand(Player player)
        {
            if (player == null) return false;

            var weapon = player.GetCurrentWeapon();
            if (weapon?.m_shared == null) return false;

            var skillType = weapon.m_shared.m_skillType;
            return skillType == Skills.SkillType.ElementalMagic ||
                   skillType == Skills.SkillType.BloodMagic;
        }

        /// <summary>
        /// 원거리 무기 사용 여부 확인 (활, 석궁)
        /// </summary>
        public static bool IsUsingRangedWeapon(Player player)
        {
            if (player == null) return false;

            var weapon = player.GetCurrentWeapon();
            if (weapon?.m_shared == null) return false;

            var skillType = weapon.m_shared.m_skillType;
            return skillType == Skills.SkillType.Bows ||
                   skillType == Skills.SkillType.Crossbows;
        }

        /// <summary>
        /// 근접 무기 사용 여부 확인 (검, 둔기, 창, 폴암, 단검, 도끼)
        /// </summary>
        public static bool IsUsingMeleeWeapon(Player player)
        {
            if (player == null) return false;

            var weapon = player.GetCurrentWeapon();
            if (weapon?.m_shared == null) return false;

            var skillType = weapon.m_shared.m_skillType;
            return skillType == Skills.SkillType.Swords ||
                   skillType == Skills.SkillType.Clubs ||
                   skillType == Skills.SkillType.Spears ||
                   skillType == Skills.SkillType.Polearms ||
                   skillType == Skills.SkillType.Knives ||
                   skillType == Skills.SkillType.Axes;
        }
    }
}

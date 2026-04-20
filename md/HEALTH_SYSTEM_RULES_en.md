# HEALTH_SYSTEM_RULES.md - Valheim Health System & Healing Rules

Rules for preventing healing flicker and ensuring correct behavior when implementing health bonuses in the CaptainSkillTree mod.

---

## 📚 Rule List

- **Rule 9**: Valheim Health System & Healing Mechanism

---

## Rule 9: Valheim Health System & Healing Mechanism

### 📋 Purpose
When increasing health via skill effects — **prevent healing flicker** and **ensure correct behavior**

### 🎯 Core Principle
**All health bonuses must be included in `m_baseHP` for healing to work correctly.**

---

## Valheim Health System Structure

### Three Components of the Health System

Valheim's health system consists of three components:

#### 1. m_baseHP (Internal Health)
- **Role**: Base health gained from food
- **Characteristic**: **The range in which healing operates**
- **Importance**: Bonuses not included in this value will not be healed

#### 2. GetMaxHealth() (Maximum Health)
- **Role**: Total max health displayed in the UI
- **Calculation**: `m_baseHP + additional bonuses`
- **Usage**: Display only

#### 3. Player.Heal() (Healing System)
- **Role**: Health recovery mechanism
- **Limitation**: **Only operates within the m_baseHP range**
- **Problem**: Health above m_baseHP cannot be healed

---

## Valheim Healing Mechanism

### How Healing Works

```
Player.Heal() cannot recover health that exceeds m_baseHP
```

**Problem scenario**:
- If a skill bonus exists only in GetMaxHealth but not in m_baseHP
- → Healing flicker occurs in the bonus range

### Example: Healing Flicker Problem

**Scenario**: Base health 100, skill bonus +30 → Max 130

#### ❌ Wrong Implementation (Healing Flicker)
```csharp
// Bonus only added to GetMaxHealth
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static void Postfix(Player __instance, ref float __result)
{
    __result += 30f;  // ❌ Not included in m_baseHP
}
```

**Result**:
- m_baseHP: 100 (food only)
- GetMaxHealth: 130 (100 + 30)
- **Healing range**: 0~100 (m_baseHP range only)
- **Problem**: Healing fails in 100~130 range → flicker occurs

#### ✅ Correct Implementation (Healing Works)
```csharp
// Included in m_baseHP via GetTotalFoodValue
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static void Postfix(Player __instance, ref float hp, ref float stamina)
{
    hp += 30f;  // ✅ Included in m_baseHP
}
```

**Result**:
- m_baseHP: 130 (food + bonus)
- GetMaxHealth: 130 (same as m_baseHP)
- **Healing range**: 0~130 (full range)
- **Effect**: Healing works correctly across the entire 0~130 range

---

## Health Bonus Implementation Rules

### Rule 1: Fixed Health Bonus → Patch GetTotalFoodValue

**Pattern**: Skills that add a fixed value (+20 HP)

```csharp
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static class SkillTree_Player_GetTotalFoodValue_HealthBonus_Patch
{
    public static void Postfix(Player __instance, ref float hp, ref float stamina)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        // Fixed health bonus added to hpBonus (included in m_baseHP)
        float hpBonus = 0f;

        // Example 1: Toughened Skin skill grants +20 HP
        if (manager.GetSkillLevel("defense_Step1_survival") > 0)
        {
            float survivalBonus = Defense_Config.SurvivalHealthBonusValue;
            hpBonus += survivalBonus;
            Plugin.Log.LogDebug($"[Toughened Skin] Fixed health bonus: +{survivalBonus}");
        }

        // Example 2: Defense Expert root grants +20 HP
        if (manager.GetSkillLevel("defense_root") > 0)
        {
            float rootBonus = Defense_Config.DefenseRootHealthBonusValue;
            hpBonus += rootBonus;
            Plugin.Log.LogDebug($"[Defense Expert] Fixed health bonus: +{rootBonus}");
        }

        // Add to base health → included in m_baseHP
        hp += hpBonus;

        if (hpBonus > 0f)
        {
            Plugin.Log.LogInfo($"[Health System] Total fixed bonus: +{hpBonus}, Final m_baseHP: {hp:F0}");
        }
    }
}
```

**Advantages**:
- Directly included in m_baseHP — healing works correctly
- Simple and intuitive implementation
- Handled the same way as food buffs

---

### Rule 2: Percentage Health Bonus → Convert to Fixed Value in GetTotalFoodValue

**Pattern**: Skills that increase health by a percentage (+30%)

**Key**: Convert the percentage bonus to a **fixed value** and include it in m_baseHP

```csharp
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static class SkillTree_Player_GetTotalFoodValue_HealthPercent_Patch
{
    public static void Postfix(Player __instance, ref float hp, ref float stamina)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float hpBonus = 0f;

        // === Apply fixed bonuses first ===
        if (manager.GetSkillLevel("defense_Step1_survival") > 0)
        {
            float survivalBonus = Defense_Config.SurvivalHealthBonusValue;
            hpBonus += survivalBonus;
        }

        // === Convert percentage bonus to a fixed value ===
        if (manager.GetSkillLevel("defense_Step6_body") > 0)
        {
            // Total base health so far (food + fixed bonuses)
            float baseHealthBeforeBonus = hp + hpBonus;

            // Percentage bonus calculation (30% = 0.3)
            float bonusPercent = Defense_Config.BodyHealthBonusValue / 100f;

            // Convert to fixed health value
            float bonusHealth = baseHealthBeforeBonus * bonusPercent;

            // Include in m_baseHP
            hpBonus += bonusHealth;

            Plugin.Log.LogDebug($"[Jotunn's Vitality] Base health +{bonusPercent * 100f}%: {baseHealthBeforeBonus:F0} * {bonusPercent:F2} = +{bonusHealth:F0}");
        }

        // Add total bonus to base health
        hp += hpBonus;

        if (hpBonus > 0f)
        {
            Plugin.Log.LogInfo($"[Health System] Total bonus: +{hpBonus:F0}, Final m_baseHP: {hp:F0}");
        }
    }
}
```

**Conversion Logic**:
1. **Calculate base health**: Food + fixed bonuses = `baseHealthBeforeBonus`
2. **Apply percentage**: `baseHealthBeforeBonus × 0.3` = fixed value
3. **Include in m_baseHP**: `hp += fixed value`

**Example**:
- Food health: 100
- Fixed bonus: +20 (Toughened Skin)
- Percentage bonus: +30% (Jotunn's Vitality)
- Calculation: (100 + 20) × 0.3 = **+36**
- Final m_baseHP: 100 + 20 + 36 = **156**

---

### Rule 3: GetMaxHealth Patch is Display Only

**Principle**: GetMaxHealth is used **for UI display only**

```csharp
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static class SkillTree_Player_GetMaxHealth_Display_Patch
{
    public static void Postfix(Player __instance, ref float __result)
    {
        // ⚠️ GetMaxHealth is for UI display
        // Bonuses that affect healing MUST be handled in GetTotalFoodValue
        // Only add display-only bonuses here that are NOT already in m_baseHP

        // Example: Bonuses already handled in GetTotalFoodValue should NOT be added here
        // === Jotunn's Vitality: already handled in GetTotalFoodValue ===
        // ✅ Healing flicker prevention: percentage bonus converted to fixed value and included in m_baseHP

        // ❌ Adding here will cause healing flicker
        // __result += someBonus;  // Absolutely forbidden!

        Plugin.Log.LogDebug($"[Health System] Final GetMaxHealth: {__result:F0}");
    }
}
```

**Usage**:
- Only adjust the max health shown in the UI
- Only add display-only bonuses not included in m_baseHP
- **In most cases, using GetTotalFoodValue alone is sufficient**

---

## Health Bonus Classification Guide

| Bonus Type | Implementation Location | Reason | Healing Works |
|------------|------------------------|--------|---------------|
| **Fixed Health Bonus** (+20 HP) | GetTotalFoodValue | Included in m_baseHP | ✅ Yes |
| **Percentage Health Bonus** (+30%) | GetTotalFoodValue (converted to fixed) | Converted to fixed value and included in m_baseHP | ✅ Yes |
| **Display-Only Bonus** | GetMaxHealth | UI display only, no effect on healing | ⚠️ No healing |

---

## Prohibited Patterns

### ❌ Do NOT Add Percentage Bonus to GetMaxHealth Only

```csharp
// ❌ Wrong example: causes healing flicker
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static void Postfix(Player __instance, ref float __result)
{
    float bonusPercent = 0.3f;  // 30%
    __result *= (1f + bonusPercent);  // Not included in m_baseHP!
}
```

**Problem**:
- Only GetMaxHealth increases → m_baseHP unchanged
- Healing does not work in the bonus range
- Flicker occurs when using healing items

### ❌ Do NOT Add Fixed Bonus to GetMaxHealth Only

```csharp
// ❌ Wrong example: healing does not work
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static void Postfix(Player __instance, ref float __result)
{
    __result += 20f;  // Not included in m_baseHP!
}
```

**Problem**:
- Fixed bonus not in m_baseHP
- Healing fails in the bonus range

### ❌ Do NOT Apply Bonuses Twice

```csharp
// ❌ Wrong example: duplicate application
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static void Postfix1(Player __instance, ref float hp, ref float stamina)
{
    hp += 20f;  // First application
}

[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static void Postfix2(Player __instance, ref float __result)
{
    __result += 20f;  // Duplicate! (total +40)
}
```

**Problem**:
- Same bonus applied in both GetTotalFoodValue and GetMaxHealth
- Unintended excessive health increase

---

## Debugging Patterns

### Health System Debug Logs

```csharp
// Pattern 1: Fixed bonus
Plugin.Log.LogDebug($"[Toughened Skin] Fixed health bonus: +{survivalBonus}");

// Pattern 2: Percentage bonus conversion
Plugin.Log.LogDebug($"[Jotunn's Vitality→Food] Base health +{bonusPercent * 100f}%: {baseHealthBeforeBonus:F0} * {bonusPercent:F2} = +{bonusHealth:F0}");

// Pattern 3: Final m_baseHP
Plugin.Log.LogDebug($"[Health System] Final m_baseHP: {hp:F0}, Max health: {__result:F0}");

// Pattern 4: Total bonus summary
Plugin.Log.LogInfo($"[Health System] Total bonus: +{hpBonus:F0}, Final m_baseHP: {hp:F0}");
```

### Healing Test Pattern

```csharp
// Test scenario
// 1. Check health after activating skill
Plugin.Log.LogInfo($"[Test] Skill activated: m_baseHP={player.GetHealth():F0}, Max={player.GetMaxHealth():F0}");

// 2. Take damage
player.Damage(new HitData { m_damage = new HitData.DamageTypes { m_blunt = 50f } });
Plugin.Log.LogInfo($"[Test] After damage: m_baseHP={player.GetHealth():F0}");

// 3. Use healing item
player.Heal(50f);
Plugin.Log.LogInfo($"[Test] After healing: m_baseHP={player.GetHealth():F0}");

// 4. Verify healing in bonus range
// ✅ Normal: health increases
// ❌ Flicker: health does not increase
```

---

## Reference Implementation Examples

### Success Case 1: Jotunn's Vitality (Percentage Bonus)

**Location**: `SkillEffect.cs` Lines 706-715

```csharp
// Successful percentage bonus → fixed value conversion
if (manager.GetSkillLevel("defense_Step6_body") > 0)
{
    float baseHealthBeforeBonus = hp + hpBonus;
    float bonusPercent = Defense_Config.BodyHealthBonusValue / 100f;
    float bonusHealth = baseHealthBeforeBonus * bonusPercent;
    hpBonus += bonusHealth;

    Plugin.Log.LogDebug($"[Jotunn's Vitality→Food] Base health +{bonusPercent * 100f}%: {baseHealthBeforeBonus:F0} * {bonusPercent:F2} = +{bonusHealth:F0}");
}

hp += hpBonus;  // Included in m_baseHP
```

**Result**: Healing works correctly, no flicker

### Success Case 2: Toughened Skin (Fixed Bonus)

**Location**: `SkillEffect.cs`

```csharp
// Fixed health bonus implementation
if (manager.GetSkillLevel("defense_Step1_survival") > 0)
{
    float survivalBonus = Defense_Config.SurvivalHealthBonusValue;
    hpBonus += survivalBonus;
    Plugin.Log.LogDebug($"[Toughened Skin] Fixed health bonus: +{survivalBonus}");
}

hp += hpBonus;  // Included in m_baseHP
```

**Result**: Healing works correctly

---

## Checklist

Verify the following when implementing health bonuses:

### Implementation Phase
- [ ] **GetTotalFoodValue patch**: Health bonuses must be implemented here
- [ ] **Fixed bonus**: Add directly as `hp += bonusValue`
- [ ] **Percentage bonus**: Convert to fixed value first (`baseHealth × percent`)
- [ ] **m_baseHP inclusion**: Always execute `hp += hpBonus`

### Debugging Phase
- [ ] **Add logs**: Log the bonus calculation process for each skill
- [ ] **Verify final m_baseHP**: Confirm final m_baseHP value via logs
- [ ] **Test healing**: Confirm healing works correctly in-game
- [ ] **Test bonus range**: Confirm healing works correctly in the bonus range

### Prohibited Pattern Check
- [ ] **No GetMaxHealth**: Do not add healing-affecting bonuses to GetMaxHealth
- [ ] **No duplicate application**: Do not apply the same bonus in both GetTotalFoodValue and GetMaxHealth
- [ ] **No direct percentage application**: Percentages must be converted to fixed values before applying

---

## Troubleshooting Guide

### Symptom: Healing Flicker (health does not recover)

**Cause**:
- Bonus applied only to GetMaxHealth
- Not included in m_baseHP

**Fix**:
1. Remove the GetMaxHealth patch
2. Add `hp += bonus` in GetTotalFoodValue
3. Restart the game and test

### Symptom: Health increases excessively

**Cause**:
- Duplicate application in both GetTotalFoodValue and GetMaxHealth

**Fix**:
1. Remove one of the duplicate patches
2. Recommended: use GetTotalFoodValue only

### Symptom: Percentage bonus does not work

**Cause**:
- Percentage not converted to a fixed value

**Fix**:
1. Calculate `baseHealth × percent`
2. Add the result via `hp += bonusHealth`

---

## 🔗 Related Documents

- [CLAUDE.md](../CLAUDE.md) - Full development rules index
- [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - Health system quick reference
- [CONFIG_MANAGEMENT_RULES.md](CONFIG_MANAGEMENT_RULES.md) - Config integration (Rule 7)
- [DEVELOPMENT_PATTERNS.md](DEVELOPMENT_PATTERNS.md) - Health system success/failure cases

---

**Created**: 2025-01-29
**Version**: 1.0
**Scope**: Rule 9

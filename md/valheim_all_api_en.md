# Valheim Complete API List (assembly_valheim.dll)
# Major APIs from 541 classes extracted via dnSpy
# APIs useful for modding, organized by system
# Valheim code analysis directory
- C:\home\ssunyme\.npm-global\bin\valheim_dll_api

## 🎮 Core Game Systems

### Player.cs - Player System
- Player.GetLocalPlayer() - Get local player
- Player.GetSkillFactor(Skills.SkillType) - Get skill level factor
- Player.RaiseSkill(Skills.SkillType, float) - Increase skill experience
- Player.AddNoise(float) - Add noise
- Player.GetInventory() - Get inventory
- Player.Damage(HitData) - Player damage
- Player.Heal(float) - Heal health
- Player.AddStamina(float) - Add stamina
- Player.PlacePiece(Piece) - Place building piece
- Player.GetClosestPlayer(Vector3, float) - Get nearest player

### Character.cs - Character System
- Character.Damage(HitData) - Character damage
- Character.Heal(float) - Heal health
- Character.SetHealth(float) - Set health
- Character.GetHealth() - Get current health
- Character.IsDead() - Check if dead
- Character.AddSEMan(StatusEffect) - Add status effect
- Character.GetSEMan() - Get status effect manager

### Game.cs - Game Core System
- Game.instance - Game instance
- Game.IncrementPlayerStat(PlayerStatType, float) - Increment player stat
- Game.ScaleDrops(GameObject, int) - Scale drop items
- Game.CheckDropConversion(HitData, ItemDrop, GameObject, ref int) - Check drop conversion

## 🎒 Inventory & Item System

### Inventory.cs - Inventory System
- Inventory.AddItem(GameObject, int) - Add item
- Inventory.AddItem(ItemDrop.ItemData) - Add item data
- Inventory.RemoveItem(string, int) - Remove item
- Inventory.CountItems(string) - Count items
- Inventory.GetAllItems() - Get all items
- Inventory.FindFreeStackSpace(string) - Find free stack space
- Inventory.HaveItem(string) - Check if item is owned

### ItemDrop.cs - Item Drop System
- ItemDrop.ItemData - Item data class
- ItemDrop.OnCreateNew(GameObject) - Called when new item is created
- ItemDrop.SetStack(int) - Set stack count
- ItemDrop.ItemData.GetMaxDurability() - Get max durability
- ItemDrop.ItemData.m_shared - Shared item data
- ItemDrop.ItemData.m_quality - Item quality
- ItemDrop.ItemData.m_durability - Current durability

### Container.cs - Container System
- Container.GetInventory() - Get container inventory
- Container.CheckAccess(long) - Check access permission
- Container.RPC_RequestOpen(long, bool) - Request to open container
- Container.RPC_OpenResponse(long, bool) - Open response

## 🏗️ Building & Crafting System

### Piece.cs - Building System
- Piece.IsPlacedByPlayer() - Check if placed by player
- Piece.CanBeRemoved() - Check if removable
- Piece.m_craftingStation - Crafting station requirement

### CraftingStation.cs - Crafting Station
- CraftingStation.GetLevel() - Get crafting station level
- CraftingStation.CheckUsable(Player, bool) - Check if usable
- CraftingStation.GetExtensionList() - Get extension list

### Recipe.cs - Crafting Recipe
- Recipe.m_item - Crafting result
- Recipe.m_resources - Required materials
- Recipe.m_craftingStation - Required crafting station

## ⚔️ Combat & Damage System

### HitData.cs - Damage Data
- HitData.GetTotalDamage() - Calculate total damage
- HitData.GetAttacker() - Get attacker
- HitData.CheckToolTier(int, bool) - Check tool tier
- HitData.ApplyResistance(HitData.DamageModifiers, out HitData.DamageModifier) - Apply resistance
- HitData.m_damage - Damage struct
- HitData.m_point - Hit point
- HitData.m_dir - Hit direction

### Attack.cs - Attack System
- Attack.GetAttackStamina() - Get attack stamina cost
- Attack.GetAttackDamage() - Get attack damage
- Attack.OnAttackTrigger() - Attack trigger

### BaseAI.cs - AI System
- BaseAI.SetTarget(Character) - Set target
- BaseAI.GetTarget() - Get current target
- BaseAI.AggravateAllInArea(Vector3, float, BaseAI.AggravatedReason) - Aggravate nearby enemies

## 🌲 Resource Gathering System

### Pickable.cs - Pickable Object
- Pickable.RPC_Pick(long, int) - Pick RPC
- Pickable.Interact(Humanoid, bool, bool) - Interact
- Pickable.SetPicked(bool) - Set picked state
- Pickable.CanBePicked() - Check if pickable
- Pickable.m_itemPrefab - Picked item prefab

### TreeBase.cs - Tree System
- TreeBase.RPC_Damage(long, HitData) - Tree damage RPC
- TreeBase.Damage(HitData) - Tree damage
- TreeBase.SpawnLog(Vector3) - Spawn log
- TreeBase.m_health - Tree health
- TreeBase.m_dropWhenDestroyed - Drop table when destroyed

### TreeLog.cs - Log System
- TreeLog.RPC_Damage(long, HitData) - Log damage RPC
- TreeLog.Destroy(HitData) - Destroy log
- TreeLog.m_dropWhenDestroyed - Drop table when destroyed

### MineRock.cs - Mining System
- MineRock.RPC_Hit(long, HitData, int) - Mining hit RPC
- MineRock.Damage(HitData) - Mining damage
- MineRock.GetHealth() - Get rock health
- MineRock.m_dropItems - Drop items

### MineRock5.cs - Advanced Mining System
- MineRock5.RPC_Damage(long, HitData, int) - Advanced mining damage RPC
- MineRock5.DamageArea(int, HitData) - Area damage
- MineRock5.UpdateMesh() - Update mesh

### Destructible.cs - Destructible Object
- Destructible.RPC_Damage(long, HitData) - Destruction damage RPC
- Destructible.Damage(HitData) - Destruction damage
- Destructible.GetDestructibleType() - Get destructible type

## 🐟 Fishing & Animal System

### Fish.cs - Fish System
- Fish.OnHooked() - Hooked on fishing line
- Fish.Interact(Humanoid, bool, bool) - Fish interaction

### Tameable.cs - Taming System
- Tameable.Tame() - Tame animal
- Tameable.GetTameness() - Get tameness level
- Tameable.SetTamed(bool) - Set tamed state

### AnimalAI.cs - Animal AI
- AnimalAI.SetFollowTarget(GameObject) - Set follow target
- AnimalAI.GetFollowTarget() - Get current follow target

## 🏠 Building & Station System

### Bed.cs - Bed System
- Bed.CheckAccess(long) - Check bed access
- Bed.IsCurrent() - Check if current spawn point

### Fireplace.cs - Fireplace System
- Fireplace.IsBurning() - Check if burning
- Fireplace.GetFuel() - Get fuel amount
- Fireplace.AddFuel() - Add fuel

### Smelter.cs - Smelter System
- Smelter.GetQueueSize() - Get queue size
- Smelter.AddOre(GameObject) - Add ore
- Smelter.AddFuel(GameObject) - Add fuel

### CookingStation.cs - Cooking Station
- CookingStation.GetFreeSlot() - Find free slot
- CookingStation.AddItem(string, int) - Add cooking ingredient

## 🌍 World & Environment System

### ZNetScene.cs - Network Scene
- ZNetScene.instance - Global instance
- ZNetScene.GetPrefab(string) - Get prefab
- ZNetScene.Destroy(GameObject) - Destroy object
- ZNetScene.InLoadingScreen - Loading screen flag

### World.cs - World System
- World.GetWorldSaveData() - Get world save data
- World.GetName() - Get world name

### EnvMan.cs - Environment Manager
- EnvMan.instance - Global instance
- EnvMan.GetCurrentEnvironment() - Get current environment
- EnvMan.SetForceEnvironment(string) - Force set environment

## 📊 UI & GUI System

### Hud.cs - HUD System
- Hud.instance - Global instance
- Hud.FlashHealthBar() - Flash health bar
- Hud.DamageFlash() - Damage flash

### InventoryGui.cs - Inventory GUI
- InventoryGui.instance - Global instance
- InventoryGui.Show(Container) - Show with container
- InventoryGui.Hide() - Hide

### MessageHud.cs - Message HUD
- MessageHud.instance - Global instance
- MessageHud.ShowMessage(MessageType, string) - Show message
- MessageHud.QueueUnlockMsg(string) - Queue unlock message

### DamageText.cs - Damage Text
- DamageText.instance - Global instance
- DamageText.ShowText(DamageText.TextType, Vector3, float, bool) - Show text

## 🎵 Audio & Effects System

### AudioMan.cs - Audio Manager
- AudioMan.instance - Global instance
- AudioMan.PlaySoundAt(AudioClip, Vector3) - Play sound at position

### EffectList.cs - Effect System
- EffectList.Create(Vector3, Quaternion, Transform, float, int) - Create effect
- EffectList.HasEffects() - Check if has effects

## 🔧 Utility & Network

### ZNetView.cs - Network View
- ZNetView.IsOwner() - Check if owner
- ZNetView.IsValid() - Check if valid
- ZNetView.GetZDO() - Get ZDO
- ZNetView.Register<T>(string, Action<long, T>) - Register RPC
- ZNetView.InvokeRPC(string, object[]) - Invoke RPC
- ZNetView.Destroy() - Destroy network object

### ZDO.cs - Network Data Object
- ZDO.GetFloat(int, float) - Get float value
- ZDO.Set(int, float) - Set float value
- ZDO.GetBool(int, bool) - Get bool value
- ZDO.Set(int, bool) - Set bool value
- ZDO.GetString(int, string) - Get string value

### Utils & Extensions
- UnityEngine.Object.Instantiate<T>() - Instantiate object
- UnityEngine.Random - Random utility
- Mathf - Math functions

## 📝 Skill System

### Skills.cs - Skill System
- Skills.RaiseSkill(SkillType, float) - Level up skill
- Skills.GetSkill(SkillType) - Get specific skill
- Skills.GetSkillFactor(SkillType) - Get skill factor

### StatusEffect.cs - Status Effect
- StatusEffect.Setup(Character) - Set up status effect
- StatusEffect.UpdateStatusEffect(float) - Update status effect
- StatusEffect.Stop() - Stop status effect

## 🚢 Transport & Movement System

### Ship.cs - Ship System
- Ship.GetSpeed() - Get current speed
- Ship.HasPlayerOnboard() - Check if player is onboard

### Vagon.cs - Cart System
- Vagon.CanAttach(Ship) - Check if attachable
- Vagon.AttachTo(Ship) - Attach to ship

## 🏺 Save & Data System

### SaveSystem.cs - Save System
- SaveSystem.GetSaveDataAsync() - Get save data asynchronously
- SaveSystem.SaveAsync() - Save asynchronously

### ObjectDB.cs - Object Database
- ObjectDB.instance - Global instance
- ObjectDB.GetItemPrefab(string) - Get item prefab

## 📊 Total Statistics
- **Total Classes**: 541
- **Major Systems**: 15 categories
- **Core APIs**: 200+ methods
- **RPC Methods**: 50+ network functions

## 🎯 Top 20 Most Useful APIs for Modding
1. Player.GetLocalPlayer()
2. ZNetScene.instance.GetPrefab()
3. Player.GetInventory().AddItem()
4. HitData.GetTotalDamage()
5. Character.Damage()
6. ZNetView.InvokeRPC()
7. Game.IncrementPlayerStat()
8. DamageText.instance.ShowText()
9. EffectList.Create()
10. Pickable.RPC_Pick()
11. MineRock.RPC_Hit()
12. TreeBase.RPC_Damage()
13. ItemDrop.OnCreateNew()
14. Player.RaiseSkill()
15. Inventory.CountItems()
16. ZNetView.IsOwner()
17. MessageHud.instance.ShowMessage()
18. Skills.GetSkillFactor()
19. Character.AddSEMan()
20. BaseAI.AggravateAllInArea()

---
**Created**: 2025-01-30
**Analysis Tool**: dnSpy
**Source**: assembly_valheim.dll (Valheim Official)

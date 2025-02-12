﻿using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System.IO;
using BepInEx.Configuration;
using System.Collections.Generic;

namespace ValheimHTDArmory
{
    /*
        Crafting Station forge --- forge (CraftingStation)
        Crafting Station piece_workbench --- piece_workbench (CraftingStation)
        Crafting Station piece_cauldron --- piece_cauldron (CraftingStation)
        Crafting Station piece_stonecutter --- piece_stonecutter (CraftingStation)
        Crafting Station piece_artisanstation --- piece_artisanstation (CraftingStation)
     */
    public static class ItemManager
    {
        private static AssetBundle assetBundle;
        public static string ResourcePath = "Assets/CustomItems/";

        private static RecipeHelper currentRecipeHelper;
        private static GameObject currentItem;
        private static CustomItem currentCustom;

        public static void BuildLists()
        {
            //weapon resources
            assetBundle = GetAssetBundleFromResources("twohandedweapons");

            StatusEffect se = assetBundle.LoadAsset<StatusEffect>("SEUnholyRegenHTD");
            MyReferences.TryAddToStatusEffectList(se);

            se = assetBundle.LoadAsset<StatusEffect>("SEBoarBerserkerSetHTD");
            Plugin.cl.TryLocalizeStatusEffect(se.name,ref se);
            MyReferences.TryAddToStatusEffectList(se);

            Color bronzeColor = new(0.783f, 0.4329f, 0.1588f);
            Color ironColor = new(0.4f, 0.4f, 0.4f);

            //ArmorBoar
            currentItem = ExtractGameObjectFromBundle(assetBundle, "ArmorHelmetBoarHTD");
            var id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "piece_workbench", 1, 1);
                currentRecipeHelper.AddResource("TrophyBoar", 1, 0).AddResource("LeatherScraps", 6, 3).AddResource("BoneFragments", 12, 6);
            }
            Plugin.cc.AddArmorDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("model", "TrophyBoar", "model").CopyTargetMaterial().CopyTextures().StartNewNode();
            currentCustom.prefabNodeManager.SetNode("model", "TrophyBoar", "Fangs 006").SetMyMateiralIndex(1).CopyTargetMaterial().StartNewNode();
            currentCustom.prefabNodeManager.SetNode("modelcloth", "TrophyBoar", "model").CopyTargetMaterial().CopyTextures();
            Plugin.customItems.Add(currentCustom);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            //
            currentItem = ExtractGameObjectFromBundle(assetBundle, "ArmorChestBoarHTD");
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "piece_workbench", 1, 1);
                currentRecipeHelper.AddResource("TrophyBoar", 1, 0).AddResource("LeatherScraps", 10, 4).AddResource("BoneFragments", 12, 6);
            }
            Plugin.cc.AddArmorDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);


            currentCustom = new CustomItem(currentItem);
            currentCustom.prefabNodeManager.SetNode("BoarChestMesh", "TrophyBoar", "Fangs 006").SetMyMateiralIndex(1).CopyTargetMaterial().StartNewNode();
            Plugin.customItems.Add(currentCustom);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            //
            currentItem = ExtractGameObjectFromBundle(assetBundle, "ArmorLegsBoarHTD");
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "piece_workbench", 1, 1);
                currentRecipeHelper.AddResource("TrophyBoar", 1, 0).AddResource("LeatherScraps", 10, 4);
            }
            Plugin.cc.AddArmorDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);


            currentCustom = new CustomItem(currentItem);
            Plugin.customItems.Add(currentCustom);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            //
            currentItem = ExtractGameObjectFromBundle(assetBundle, "ArmorShoulderBoarHTD");
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "piece_workbench", 1, 1);
                currentRecipeHelper.AddResource("TrophyBoar", 1, 0).AddResource("LeatherScraps", 8, 3).AddResource("BoneFragments", 6, 3);
            }
            Plugin.cc.AddArmorDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            currentCustom = new CustomItem(currentItem);
            currentCustom.prefabNodeManager.SetNode("model", "TrophyBoar", "Fangs 006").SetMyMateiralIndex(1).CopyTargetMaterial().StartNewNode();
            Plugin.customItems.Add(currentCustom);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);



            /*
                       currentItem = ExtractGameObjectFromBundle(assetBundle, "GSSwordBleedingEyes");
                       var tid = currentItem.GetComponent<ItemDrop>();

                       currentRecipeHelper = ApplyConfigChanges(ref currentItem);

                       if (currentRecipeHelper == null)
                       {
                           currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                           currentRecipeHelper.AddResource("Wood", 4, 0);
                       }
                       Plugin.cc.AddItemDataAsConfigRecord(currentItem);
                       Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
                       //Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

                       MyReferences.myRecipeHelperList.Add(currentRecipeHelper);

                       currentCustom = new CustomItem(currentItem);

                       currentCustom.effectHandler.AddEffect("vfx_HitSparks", WeaponEffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
                       currentCustom.effectHandler.AddEffect("sfx_metal_blocked", WeaponEffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
                       currentCustom.effectHandler.AddEffect("fx_swing_camshake", WeaponEffectsManager.EffectList.TRIGGER);
                       currentCustom.effectHandler.AddEffect("sfx_sword_swing", WeaponEffectsManager.EffectList.TRAIL);

                       MyReferences.customItems.Add(currentCustom);

                       */



            //FistBronze
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BronzeFistsHTD");//FistBronze
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("Bronze", 10, 5).AddResource("LeatherScraps", 5, 3);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Horizontal;
            Attack3.m_attackAnimation = "knife_secondary";
            Attack3.m_attackStamina = 40f;
            Attack3.m_speedFactor = 0f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 2f;
            Attack3.m_attackHitNoise = 5f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 1.5f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 30f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            //MyReferences.listHashOfSMRWeapons.Add(currentItem.name.GetStableHashCode());
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("FistMetalKnucklesMesh", "SwordBlackmetal", "default").CopyTargetMaterial(true).ReplaceMetalColor(bronzeColor).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("model", "SwordBlackmetal", "default").CopyTargetMaterial(true).ReplaceMetalColor(bronzeColor).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("sfx_unarmed_swing", EffectsManager.EffectList.START);
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_unarmed_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //FistIron
            currentItem = ExtractGameObjectFromBundle(assetBundle, "IronFistsHTD");//FistIron
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("Iron", 10, 5).AddResource("LeatherScraps", 5, 3);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Horizontal;
            Attack3.m_attackAnimation = "knife_secondary";
            Attack3.m_attackStamina = 40f;
            Attack3.m_speedFactor = 0f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 2f;
            Attack3.m_attackHitNoise = 5f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 1.5f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 30f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            //MyReferences.listHashOfSMRWeapons.Add(currentItem.name.GetStableHashCode());
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("FistMetalKnucklesMesh", "SwordBlackmetal", "default").CopyTargetMaterial(true).ReplaceMetalColor(ironColor).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("model", "SwordBlackmetal", "default").CopyTargetMaterial(true).ReplaceMetalColor(ironColor).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("sfx_unarmed_swing", EffectsManager.EffectList.START);
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_unarmed_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //FistBuckAndDoe
            currentItem = ExtractGameObjectFromBundle(assetBundle, "DeerFistsHTD");//FistBuckAndDoe
            id = currentItem.GetComponent<ItemDrop>();
            //MyReferences.prefabsThatUpgradeAtLevelOneAlways.Add(id.name.GetStableHashCode());

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "piece_workbench", 1, 1);
                currentRecipeHelper.AddResource("HardAntler", 2, 6).AddResource("TrophyDeer", 1, 2).AddResource("LeatherScraps", 8, 8);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Horizontal;
            Attack3.m_attackAnimation = "knife_secondary";
            Attack3.m_attackStamina = 40f;
            Attack3.m_speedFactor = 0f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 2f;
            Attack3.m_attackHitNoise = 5f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 1.5f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 30f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            //MyReferences.listHashOfSMRWeapons.Add(currentItem.name.GetStableHashCode());
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("FistBuckAndDoeMesh", "HardAntler", "model").CopyTargetMaterial().SetMyMateiralIndex(1).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("model", "HardAntler", "model").CopyTargetMaterial().SetMyMateiralIndex(1).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("sfx_unarmed_swing", EffectsManager.EffectList.START);
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_unarmed_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Silver Fist
            currentItem = ExtractGameObjectFromBundle(assetBundle, "SilverFistsHTD");//SilverFistsHTD
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("WolfPelt", 8, 4).AddResource("Silver", 25, 10).AddResource("LeatherScraps", 9, 3).AddResource("Iron", 10, 5);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Horizontal;
            Attack3.m_attackAnimation = "knife_secondary";
            Attack3.m_attackStamina = 40f;
            Attack3.m_speedFactor = 0f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 2f;
            Attack3.m_attackHitNoise = 5f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 1.5f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 30f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            //MyReferences.listHashOfSMRWeapons.Add(currentItem.name.GetStableHashCode());
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("SilverGlovesSMR", "ArmorWolfChest", "SilverWolfArmor").CopyTargetMaterial().SetMyMateiralIndex(3).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("model", "ArmorWolfChest", "SilverWolfArmor").CopyTargetMaterial().SetMyMateiralIndex(3).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("sfx_unarmed_swing", EffectsManager.EffectList.START);
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_unarmed_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Bronze Crowbill   
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BronzeCrowbillHTD");
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("Wood", 4, 0).AddResource("Bronze", 8, 3).AddResource("LeatherScraps", 2, 2);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            //Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            //Attack3.m_attackType = Attack.AttackType.Horizontal;
            //Attack3.m_attackAnimation = "battleaxe_secondary";
            //Attack3.m_attackStamina = 15f;
            //Attack3.m_speedFactor = 0.1f;
            //Attack3.m_speedFactorRotation = 0.5f;
            //Attack3.m_attackStartNoise = 10f;
            //Attack3.m_attackHitNoise = 30f;
            //Attack3.m_damageMultiplier = 0.5f;
            //Attack3.m_forceMultiplier = 3f;
            //Attack3.m_staggerMultiplier = 4f;
            //Attack3.m_attackRange = 3f;
            //Attack3.m_attackHeight = 1f;
            //Attack3.m_attackAngle = 30f;

            //MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_sword_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Silver Great Sword    
            currentItem = ExtractGameObjectFromBundle(assetBundle, "SilverGreatSwordHTD");//SwordSilverGreat
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("FineWood", 8, 0).AddResource("Silver", 60, 30).AddResource("LeatherScraps", 9, 3).AddResource("Iron", 10, 5);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_pickaxe";
            Attack3.m_attackStamina = 25f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.3f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 40f;
            Attack3.m_damageMultiplier = 1.75f;
            Attack3.m_forceMultiplier = 0.75f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 120f;

            if (!Plugin.holdSilverGreatswordByBlade)
            {
                id.m_itemData.m_shared.m_animationState = ItemDrop.ItemData.AnimationState.TwoHandedClub;
            }

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_sword_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Bronze Great Sword    
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BronzeGreatSwordHTD");//SwordSilverGreat
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("Wood", 5, 0).AddResource("Bronze", 45, 20).AddResource("LeatherScraps", 4, 2);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_pickaxe";
            Attack3.m_attackStamina = 25f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.3f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 40f;
            Attack3.m_damageMultiplier = 1.75f;
            Attack3.m_forceMultiplier = 0.75f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 120f;

            //id.m_itemData.m_shared.m_animationState = ItemDrop.ItemData.AnimationState.TwoHandedAxe;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_sword_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Iron
            currentItem = ExtractGameObjectFromBundle(assetBundle, "IronGreatSwordHTD");//SwordIronGreat
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("FineWood", 8, 0).AddResource("Iron", 40, 20).AddResource("LeatherScraps", 9, 3);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_pickaxe";
            Attack3.m_attackStamina = 25f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.3f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 40f;
            Attack3.m_damageMultiplier = 1.75f;
            Attack3.m_forceMultiplier = 0.75f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 120f;

            if (!Plugin.holdIronGreatswordByBlade)
            {
                id.m_itemData.m_shared.m_animationState = ItemDrop.ItemData.AnimationState.TwoHandedClub;
            }

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_sword_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Bronze Battle axe
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BronzeBattleaxeHTD");//AxeSilverBattle

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("Wood", 10, 0).AddResource("Bronze", 35, 15).AddResource("LeatherScraps", 5, 3);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_pickaxe";
            Attack3.m_attackStamina = 25f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.3f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 40f;
            Attack3.m_damageMultiplier = 1.75f;
            Attack3.m_forceMultiplier = 0.75f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 2.5f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 120f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_battleaxe_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_wood_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);

            //Silver Battle axe
            currentItem = ExtractGameObjectFromBundle(assetBundle, "SilverBattleaxeHTD");//AxeSilverBattle

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("ElderBark", 20, 0).AddResource("Silver", 60, 30).AddResource("LeatherScraps", 9, 3).AddResource("Iron", 10, 5);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_pickaxe";
            Attack3.m_attackStamina = 25f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.3f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 40f;
            Attack3.m_damageMultiplier = 1.75f;
            Attack3.m_forceMultiplier = 0.75f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 2.5f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 120f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_battleaxe_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_wood_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            if (Plugin.disableSilverBattleaxeLights)
            {
                List<Transform> particleTransforms = new();
                RecursiveSearchFunctions.ChildNodesFinderDepthFirst(id.transform, "vfx_sparks", 9, ref particleTransforms);
                if (particleTransforms.Count > 0)
                {
                    foreach (var pt in particleTransforms)
                    {
                        ParticleSystem ptps = pt.GetComponent<ParticleSystem>();
                        if (ptps != null)
                        {
                            var light = ptps.lights;
                            light.enabled = false;
                        }
                    }
                }
            }

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);


            //Bronze Hammer
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BronzeHammerHTD");//AxeSilverBattle

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("Wood", 10, 0).AddResource("Bronze", 35, 15).AddResource("LeatherScraps", 5, 3);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Horizontal;
            Attack3.m_attackAnimation = "battleaxe_secondary";
            Attack3.m_hitTerrain = false;
            Attack3.m_attackStamina = 15f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.5f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 30f;
            Attack3.m_damageMultiplier = 0.5f;
            Attack3.m_forceMultiplier = 3f;
            Attack3.m_staggerMultiplier = 4f;
            Attack3.m_attackRange = 2f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 30f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_battleaxe_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_wood_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);

            //Mace Silver Great
            currentItem = ExtractGameObjectFromBundle(assetBundle, "SilverGreatMaceHTD");//MaceSilverGreat

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("ElderBark", 20, 0).AddResource("Silver", 60, 30).AddResource("YmirRemains", 10, 0).AddResource("FreezeGland", 10, 0);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;
            Attack3.m_hitTerrain = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);
            //MaceCore
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreMaceHTD");

            var rotatorTransform = RecursiveSearchFunctions.ChildNodeFinderDepthFirst(currentItem.transform, "rotator");

            Rotator rotatorGOComponent = rotatorTransform.gameObject.AddComponent<Rotator>();
            rotatorGOComponent.rotateX = 0f;
            rotatorGOComponent.rotateY = 45f;
            rotatorGOComponent.rotateZ = 0f;

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 15, 5).AddResource("Iron", 10, 5).AddResource("LeatherScraps", 6, 4);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core").CopyTargetMesh().ChangeMeshScale(0.0018f).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_club_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //MaceCoreGreen
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreMaceGreenHTD");

            rotatorTransform = RecursiveSearchFunctions.ChildNodeFinderDepthFirst(currentItem.transform, "rotator");

            rotatorGOComponent = rotatorTransform.gameObject.AddComponent<Rotator>();
            rotatorGOComponent.rotateX = 0f;
            rotatorGOComponent.rotateY = 45f;
            rotatorGOComponent.rotateZ = 0f;

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 15, 5).AddResource("Iron", 10, 5).AddResource("LeatherScraps", 6, 4);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core").CopyTargetMesh().ChangeMeshScale(0.0018f).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_club_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //MaceCoreBlue
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreMaceBlueHTD");

            rotatorTransform = RecursiveSearchFunctions.ChildNodeFinderDepthFirst(currentItem.transform, "rotator");

            rotatorGOComponent = rotatorTransform.gameObject.AddComponent<Rotator>();
            rotatorGOComponent.rotateX = 0f;
            rotatorGOComponent.rotateY = 45f;
            rotatorGOComponent.rotateZ = 0f;

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 15, 5).AddResource("Iron", 10, 5).AddResource("LeatherScraps", 6, 4);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core").CopyTargetMesh().ChangeMeshScale(0.0018f).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_club_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //MaceCoreGreat
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreGreatMaceHTD");//MaceCoreGreat

            List<Transform> rotators = new();
            RecursiveSearchFunctions.ChildNodesFinderDepthFirst(currentItem.transform, "rotator", 3, ref rotators);
            foreach (Transform rotator in rotators)
            {
                Rotator rotatorComponent = rotator.gameObject.AddComponent<Rotator>();
                rotatorComponent.rotateX = 0f;
                rotatorComponent.rotateY = 45f;
                rotatorComponent.rotateZ = 0f;
            }

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 40, 15).AddResource("Iron", 35, 10).AddResource("LeatherScraps", 12, 8);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;
            Attack3.m_hitTerrain = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core", 3).CopyTargetMesh().ChangeMeshScale(0.0034f).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);
            //Green
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreGreatMaceGreenHTD");//MaceCoreGreatGreen

            rotators = new List<Transform>();
            RecursiveSearchFunctions.ChildNodesFinderDepthFirst(currentItem.transform, "rotator", 3, ref rotators);
            foreach (Transform rotator in rotators)
            {
                Rotator rotatorComponent = rotator.gameObject.AddComponent<Rotator>();
                rotatorComponent.rotateX = 0f;
                rotatorComponent.rotateY = 45f;
                rotatorComponent.rotateZ = 0f;
            }

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 40, 15).AddResource("Iron", 35, 10).AddResource("LeatherScraps", 12, 8);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;
            Attack3.m_hitTerrain = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core", 3).CopyTargetMesh().ChangeMeshScale(0.0034f).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Blue
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreGreatMaceBlueHTD");

            rotators = new List<Transform>();
            RecursiveSearchFunctions.ChildNodesFinderDepthFirst(currentItem.transform, "rotator", 3, ref rotators);
            foreach (Transform rotator in rotators)
            {
                Rotator rotatorComponent = rotator.gameObject.AddComponent<Rotator>();
                rotatorComponent.rotateX = 0f;
                rotatorComponent.rotateY = 45f;
                rotatorComponent.rotateZ = 0f;
            }

            id = currentItem.GetComponent<ItemDrop>();
            //id.m_itemData.m_shared.m_name += " Green";

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 40, 15).AddResource("Iron", 35, 10).AddResource("LeatherScraps", 12, 8);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;
            Attack3.m_hitTerrain = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core", 3).CopyTargetMesh().ChangeMeshScale(0.0034f).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //MaceCore
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreAxeHTD");

            rotatorTransform = RecursiveSearchFunctions.ChildNodeFinderDepthFirst(currentItem.transform, "rotator");

            rotatorGOComponent = rotatorTransform.gameObject.AddComponent<Rotator>();
            rotatorGOComponent.rotateX = 0f;
            rotatorGOComponent.rotateY = 45f;
            rotatorGOComponent.rotateZ = 0f;

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 15, 5).AddResource("Iron", 10, 5).AddResource("LeatherScraps", 6, 4);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core").CopyTargetMesh().ChangeMeshScale(0.0018f).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_club_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //MaceCoreGreen
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreAxeGreenHTD");

            rotatorTransform = RecursiveSearchFunctions.ChildNodeFinderDepthFirst(currentItem.transform, "rotator");

            rotatorGOComponent = rotatorTransform.gameObject.AddComponent<Rotator>();
            rotatorGOComponent.rotateX = 0f;
            rotatorGOComponent.rotateY = 45f;
            rotatorGOComponent.rotateZ = 0f;

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 15, 5).AddResource("Iron", 10, 5).AddResource("LeatherScraps", 6, 4);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core").CopyTargetMesh().ChangeMeshScale(0.0018f).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_club_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //MaceCoreBlue
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreAxeBlueHTD");

            rotatorTransform = RecursiveSearchFunctions.ChildNodeFinderDepthFirst(currentItem.transform, "rotator");

            rotatorGOComponent = rotatorTransform.gameObject.AddComponent<Rotator>();
            rotatorGOComponent.rotateX = 0f;
            rotatorGOComponent.rotateY = 45f;
            rotatorGOComponent.rotateZ = 0f;

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 15, 5).AddResource("Iron", 10, 5).AddResource("LeatherScraps", 6, 4);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core").CopyTargetMesh().ChangeMeshScale(0.0018f).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_club_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //MaceCoreGreat
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreGreatAxeHTD");//MaceCoreGreat

            rotators = new List<Transform>();
            RecursiveSearchFunctions.ChildNodesFinderDepthFirst(currentItem.transform, "rotator", 3, ref rotators);
            foreach (Transform rotator in rotators)
            {
                Rotator rotatorComponent = rotator.gameObject.AddComponent<Rotator>();
                rotatorComponent.rotateX = 0f;
                rotatorComponent.rotateY = 45f;
                rotatorComponent.rotateZ = 0f;
            }

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 40, 15).AddResource("Iron", 35, 10).AddResource("LeatherScraps", 12, 8);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;
            Attack3.m_hitTerrain = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core", 3).CopyTargetMesh().ChangeMeshScale(0.0034f).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);
            //Green
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreGreatAxeGreenHTD");//MaceCoreGreatGreen

            rotators = new List<Transform>();
            RecursiveSearchFunctions.ChildNodesFinderDepthFirst(currentItem.transform, "rotator", 3, ref rotators);
            foreach (Transform rotator in rotators)
            {
                Rotator rotatorComponent = rotator.gameObject.AddComponent<Rotator>();
                rotatorComponent.rotateX = 0f;
                rotatorComponent.rotateY = 45f;
                rotatorComponent.rotateZ = 0f;
            }

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 40, 15).AddResource("Iron", 35, 10).AddResource("LeatherScraps", 12, 8);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;
            Attack3.m_hitTerrain = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core", 3).CopyTargetMesh().ChangeMeshScale(0.0034f).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Blue
            currentItem = ExtractGameObjectFromBundle(assetBundle, "CoreGreatAxeBlueHTD");

            rotators = new List<Transform>();
            RecursiveSearchFunctions.ChildNodesFinderDepthFirst(currentItem.transform, "rotator", 3, ref rotators);
            foreach (Transform rotator in rotators)
            {
                Rotator rotatorComponent = rotator.gameObject.AddComponent<Rotator>();
                rotatorComponent.rotateX = 0f;
                rotatorComponent.rotateY = 45f;
                rotatorComponent.rotateZ = 0f;
            }

            id = currentItem.GetComponent<ItemDrop>();
            //id.m_itemData.m_shared.m_name += " Green";

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophySurtling", 1, 0).AddResource("SurtlingCore", 40, 15).AddResource("Iron", 35, 10).AddResource("LeatherScraps", 12, 8);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;
            Attack3.m_hitTerrain = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("core", "SurtlingCore", "core", 3).CopyTargetMesh().ChangeMeshScale(0.0034f).StartNewNode();
            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //Grasp
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BoneGreatMaceHTD");//MaceGraspUndying

            id = currentItem.GetComponent<ItemDrop>();

            //var shared = id.m_itemData.m_shared;
            ////shared.m_attackStatusEffect = 

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophyBonemass", 1, 0).AddResource("WitheredBone", 20, 10).AddResource("BoneFragments", 50, 25).AddResource("TrollHide", 30, 20);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;
            Attack3.m_hitTerrain = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddStatusEffect("Tared", EffectsManager.StatusEffectTarget.ODB, EffectsManager.StatusEffectTarget.ATTACK);
            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_sledge_hit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sledge_iron_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            //SwordBlackmetalGreat
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BlackMetalGreatSwordHTD");//SwordBlackMetalGreat

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("BlackMetal", 60, 30).AddResource("LinenThread", 9, 3);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Horizontal;
            Attack3.m_attackAnimation = "battleaxe_secondary";
            Attack3.m_attackStamina = 15f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.5f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 30f;
            Attack3.m_damageMultiplier = 0.5f;
            Attack3.m_forceMultiplier = 3f;
            Attack3.m_staggerMultiplier = 4f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 30f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("model", "SwordBlackmetal", "default", 3).CopyTargetMaterial(true).StartNewNode();

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_sword_swing", EffectsManager.EffectList.TRAIL);

            //currentCustom.materialManager.SetCloneTargetSwapTextures("SwordBlackmetal");

            Plugin.customItems.Add(currentCustom);

            //Iron but as Black Metal
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BlackMetalGreatSwordAltHTD");//SwordIronGreatBlack
            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("BlackMetal", 60, 30).AddResource("LinenThread", 9, 3);
            }

            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_pickaxe";
            Attack3.m_attackStamina = 25f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.3f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 40f;
            Attack3.m_damageMultiplier = 1.75f;
            Attack3.m_forceMultiplier = 0.75f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 120f;

            if (!Plugin.holdBlackMetalAltGreatswordByBlade)
            {
                id.m_itemData.m_shared.m_animationState = ItemDrop.ItemData.AnimationState.TwoHandedClub;
            }

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("model", "SwordBlackmetal", "default", 4).CopyTargetMaterial(true).StartNewNode();

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_sword_swing", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);


            //SwordObsidianGreat
            currentItem = ExtractGameObjectFromBundle(assetBundle, "ObsidianGreatSwordHTD");//SwordObsidianGreat

            rotators = new List<Transform>();
            RecursiveSearchFunctions.ChildNodesFinderDepthFirst(currentItem.transform, "rotator", 3, ref rotators);
            foreach (Transform rotator in rotators)
            {
                Rotator rotatorComponent = rotator.gameObject.AddComponent<Rotator>();
                rotatorComponent.rotateX = 13f;
                rotatorComponent.rotateY = -15.75f;
                rotatorComponent.rotateZ = 9.25f;
            }

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("DragonTear", 1, 0).AddResource("Obsidian", 25, 35).AddResource("FreezeGland", 30, 30).AddResource("Crystal", 20, 10);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "sword_secondary";
            Attack3.m_attackStamina = 30f;
            Attack3.m_speedFactor = 0.2f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 30f;
            Attack3.m_damageMultiplier = 3f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 1f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 45f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            currentCustom.prefabNodeManager.SetNode("wrap", "LinenThread", "model", 3).CopyTargetMaterial().ReplaceMainColor(new Color(0.3647f, .2431f, .145f)).StartNewNode();

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);

            //SwordObsidianGreat Red
            currentItem = ExtractGameObjectFromBundle(assetBundle, "ObsidianGreatSwordRedHTD");//SwordObsidianGreat

            rotators = new List<Transform>();
            RecursiveSearchFunctions.ChildNodesFinderDepthFirst(currentItem.transform, "rotator", 3, ref rotators);
            foreach (Transform rotator in rotators)
            {
                Rotator rotatorComponent = rotator.gameObject.AddComponent<Rotator>();
                rotatorComponent.rotateX = 13f;
                rotatorComponent.rotateY = -15.75f;
                rotatorComponent.rotateZ = 9.25f;
            }

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("DragonTear", 1, 0).AddResource("Obsidian", 25, 35).AddResource("FreezeGland", 30, 30).AddResource("Crystal", 20, 10);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "sword_secondary";
            Attack3.m_attackStamina = 30f;
            Attack3.m_speedFactor = 0.2f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 30f;
            Attack3.m_damageMultiplier = 3f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 1f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 45f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            currentCustom.prefabNodeManager.SetNode("wrap", "LinenThread", "model", 3).CopyTargetMaterial().ReplaceMainColor(new Color(0.3647f, .2431f, .145f)).StartNewNode();

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);


            //SwordFlametalGreat
            currentItem = ExtractGameObjectFromBundle(assetBundle, "FlametalGreatSwordHTD");//SwordFlametalGreat

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("Flametal", 35, 20).AddResource("Iron", 25, 15).AddResource("LeatherScraps", 10, 10).AddResource("SurtlingCore", 20, 10);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "sword_secondary";
            Attack3.m_attackStamina = 30f;
            Attack3.m_speedFactor = 0.2f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 30f;
            Attack3.m_damageMultiplier = 3f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 1f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 45f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            if (Plugin.disableFlametalFlames)
            {
                currentItem.transform.Find("attach/content/particles/vfx_flames").gameObject.SetActive(false);
                currentItem.transform.Find("attach/content/particles/vfx_smoke").gameObject.SetActive(false);
                currentItem.transform.Find("attach/content/particles/vfx_fire").gameObject.SetActive(false);
                currentItem.transform.Find("attach/content/particles/vfx_embers").gameObject.SetActive(false);

                currentItem.transform.Find("attach_stand/v/content/particles/vfx_flames").gameObject.SetActive(false);
                currentItem.transform.Find("attach_stand/v/content/particles/vfx_smoke").gameObject.SetActive(false);
                currentItem.transform.Find("attach_stand/v/content/particles/vfx_fire").gameObject.SetActive(false);
                currentItem.transform.Find("attach_stand/v/content/particles/vfx_embers").gameObject.SetActive(false);

                currentItem.transform.Find("attach_stand/h/content/particles/vfx_flames").gameObject.SetActive(false);
                currentItem.transform.Find("attach_stand/h/content/particles/vfx_smoke").gameObject.SetActive(false);
                currentItem.transform.Find("attach_stand/h/content/particles/vfx_fire").gameObject.SetActive(false);
                currentItem.transform.Find("attach_stand/h/content/particles/vfx_embers").gameObject.SetActive(false);
            }

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);

            //Variant
            currentItem = ExtractGameObjectFromBundle(assetBundle, "IronHeavyGreatSwordHTD");//SwordFlametalGreatIron

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophyDraugr", 1, 0).AddResource("Iron", 60, 25).AddResource("ElderBark", 10, 5).AddResource("LeatherScraps", 20, 10);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "sword_secondary";
            Attack3.m_attackStamina = 30f;
            Attack3.m_speedFactor = 0.2f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 30f;
            Attack3.m_damageMultiplier = 3f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 1f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 45f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);

            //Axes


            //Black Metal
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BlackMetalBattleaxeHTD");//AxeBlackMetalBattle

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("BlackMetal", 60, 30).AddResource("LinenThread", 9, 3);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_pickaxe";
            Attack3.m_attackStamina = 25f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.3f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 40f;
            Attack3.m_damageMultiplier = 1.75f;
            Attack3.m_forceMultiplier = 0.75f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 2.5f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 120f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("model", "SwordBlackmetal", "default", 3).CopyTargetMaterial(true).StartNewNode();

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_battleaxe_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_wood_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);

            //DragonSlayer
            currentItem = ExtractGameObjectFromBundle(assetBundle, "DragonSlayerSwordHTD");//SwordDragonSlayer

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophyDragonQueen", 1, 0).AddResource("TrophyDraugrElite", 1, 0).AddResource("Iron", 90, 45).AddResource("LinenThread", 20, 10);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "sword_secondary";
            Attack3.m_attackStamina = 30f;
            Attack3.m_speedFactor = 0.2f;
            Attack3.m_speedFactorRotation = 0f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 30f;
            Attack3.m_damageMultiplier = 3f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 1f;
            Attack3.m_attackRange = 3.5f;
            Attack3.m_attackHeight = 1f;
            Attack3.m_attackAngle = 45f;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_HitSparks", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_sword_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            Plugin.customItems.Add(currentCustom);

            //Bone Sword
            currentItem = ExtractGameObjectFromBundle(assetBundle, "BoneGreatSwordHTD");//SwordBoneGreat

            id = currentItem.GetComponent<ItemDrop>();

            currentRecipeHelper = ApplyConfigChangesAndRecipes(ref currentItem);

            if (currentRecipeHelper == null)
            {
                currentRecipeHelper = new RecipeHelper(currentItem, "forge", 1, 1);
                currentRecipeHelper.AddResource("TrophyBonemass", 1, 0).AddResource("TrophyBlob", 3, 1).AddResource("BoneFragments", 35, 20).AddResource("WitheredBone", 10, 5);
            }
            Plugin.cc.AddItemDataAsConfigRecord(currentItem);
            Plugin.cc.AddRecipeAsConfigRecord(currentRecipeHelper);
            Plugin.cl.TryLocaliazeItem(currentItem.name, ref id);

            Attack3 = id.m_itemData.m_shared.m_secondaryAttack.Clone();
            Attack3.m_attackType = Attack.AttackType.Vertical;
            Attack3.m_attackAnimation = "swing_sledge";
            Attack3.m_attackStamina = 35f;
            Attack3.m_speedFactor = 0.1f;
            Attack3.m_speedFactorRotation = 0.4f;
            Attack3.m_attackStartNoise = 10f;
            Attack3.m_attackHitNoise = 60f;
            Attack3.m_damageMultiplier = 1.5f;
            Attack3.m_forceMultiplier = 1f;
            Attack3.m_staggerMultiplier = 2f;
            Attack3.m_attackRange = 3f;
            Attack3.m_attackHeight = 0f;
            Attack3.m_attackAngle = 90f;
            Attack3.m_attackRayWidth = 4f;
            Attack3.m_maxYAngle = 0f;
            Attack3.m_hitThroughWalls = true;

            MyReferences.TryAddToAttackList(currentItem, Attack3);
            Plugin.myRecipeHelperList.Add(currentRecipeHelper);

            currentCustom = new CustomItem(currentItem);

            currentCustom.prefabNodeManager.SetNode("standmesh", "wood_wall_roof_top", "top", 4).CopyTargetMesh().CopyTargetMaterial().StartNewNode();

            currentCustom.effectHandler.AddStatusEffect("Spirit", EffectsManager.StatusEffectTarget.ODB, EffectsManager.StatusEffectTarget.ATTACK);

            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("vfx_clubhit", EffectsManager.EffectList.HIT_TERRAIN).AddEffect("sfx_club_hit").AddEffect("fx_hit_camshake");
            currentCustom.effectHandler.AddEffect("sfx_metal_blocked", EffectsManager.EffectList.BLOCK).AddEffect("vfx_blocked").AddEffect("fx_block_camshake");
            currentCustom.effectHandler.AddEffect("fx_swing_camshake", EffectsManager.EffectList.TRIGGER);
            currentCustom.effectHandler.AddEffect("sfx_battleaxe_swing_wosh", EffectsManager.EffectList.TRAIL);

            Plugin.customItems.Add(currentCustom);

            assetBundle.Unload(false);
        }

        public static RecipeHelper ApplyConfigChangesAndRecipes(ref GameObject item)
        {
            //ApplyConfigChanges(ref item);


            return Plugin.cc.ApplyRecipeHelperFromConfigRecord(item);
        }

        //public static void ApplyConfigChanges(ref GameObject item)
        //{
        //    var id = item.GetComponent<ItemDrop>();
        //    switch (id.m_itemData.m_shared.m_itemType)
        //    {
        //        case ItemDrop.ItemData.ItemType.Helmet:
        //        case ItemDrop.ItemData.ItemType.Chest:
        //        case ItemDrop.ItemData.ItemType.Legs:
        //        case ItemDrop.ItemData.ItemType.Shoulder:
        //            Plugin.cc.ApplyArmorDataFromConfigRecord(ref item);
        //            break;
        //        //case ItemDrop.ItemData.ItemType.OneHandedWeapon:
        //        //case ItemDrop.ItemData.ItemType.Bow:
        //        //case ItemDrop.ItemData.ItemType.TwoHandedWeapon:
        //        //case ItemDrop.ItemData.ItemType.Torch:
        //        default:
        //            Plugin.cc.ApplyItemDataFromConfigRecord(ref item);
        //            break;
        //    }
        //}

        public static void ApplySyncedItemConfigData()
        {
            if (Plugin.myItemList.Count > 0)
            {
                for (int i = 0; i < Plugin.myItemList.Count; i++)
                {
                    GameObject itemReference = Plugin.myItemList[i];

                    var id = itemReference.GetComponent<ItemDrop>();
                    switch (id.m_itemData.m_shared.m_itemType)
                    {
                        case ItemDrop.ItemData.ItemType.Helmet:
                        case ItemDrop.ItemData.ItemType.Chest:
                        case ItemDrop.ItemData.ItemType.Legs:
                        case ItemDrop.ItemData.ItemType.Shoulder:
                            Plugin.cc.ApplyArmorDataFromConfigRecord(ref itemReference);
                            break;
                        //case ItemDrop.ItemData.ItemType.OneHandedWeapon:
                        //case ItemDrop.ItemData.ItemType.Bow:
                        //case ItemDrop.ItemData.ItemType.TwoHandedWeapon:
                        //case ItemDrop.ItemData.ItemType.Torch:
                        default:
                            Plugin.cc.ApplyItemDataFromConfigRecord(ref itemReference);
                            break;
                    }

                    //Plugin.cc.ApplyItemDataFromConfigRecord(ref itemReference);
                    Plugin.myItemList[i] = itemReference;
                }
            }
        }

        private static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            var execAssembly = Assembly.GetExecutingAssembly();

            var resourceName = execAssembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));

            using (var stream = execAssembly.GetManifestResourceStream(resourceName))
            {
                return AssetBundle.LoadFromStream(stream);
            }
        }

        private static GameObject ExtractGameObjectFromBundle(AssetBundle bundle, string prefabName)
        {
            try
            {
                return bundle.LoadAsset<GameObject>(ResourcePath + prefabName + ".prefab");
            }
            catch (Exception e)
            {
                Plugin.Log.LogError($"Error Trying to load asset '{prefabName}.prefab' from assetbundle '{bundle.name}'");
                Plugin.Log.LogError($"Catch Exception details: {e.Message} --- {e.StackTrace}");
            }
            return null;
        }

    }
}

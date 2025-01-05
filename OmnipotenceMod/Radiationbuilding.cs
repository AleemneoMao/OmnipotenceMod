using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using TUNING;
using HarmonyLib;
using UnityEngine;
using static STRINGS.BUILDING.STATUSITEMS;

namespace OmnipotenceMod
{
    public class RadiationBudling_o
    {
        [HarmonyPatch(typeof(HEPBatteryConfig), "CreateBuildingDef")]//修改辐射粒子蓄存器
        public class Patch_GeneratorConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(HEPBatteryConfig), "ConfigureBuildingTemplate")]//修改辐射粒子蓄存器
        public class Patch_HEPBatteryBuildingTemplate
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<HighEnergyParticleStorage>().capacity = 500000f;
                go.AddOrGetDef<HEPBattery.Def>().minLaunchInterval = 1f;
                go.AddOrGetDef<HEPBattery.Def>().minSlider = 0f;
                go.AddOrGetDef<HEPBattery.Def>().maxSlider = 100f;
                go.AddOrGetDef<HEPBattery.Def>().particleDecayRate = 0f;
            }
        }

        [HarmonyPatch(typeof(ManualHighEnergyParticleSpawnerConfig), "ConfigureBuildingTemplate")]//修改人力辐射发生器
        public class Patch_ManualHighEnergyParticleSpawnerConfig
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<LogicOperationalController>();
                go.AddOrGet<DropAllWorkable>();
                go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
                Prioritizable.AddRef(go);
                go.AddOrGet<HighEnergyParticleStorage>();
                go.AddOrGet<LoopingSounds>();
                ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
                complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
                complexFabricator.duplicantOperated = true;
                go.AddOrGet<FabricatorIngredientStatusManager>();
                go.AddOrGet<CopyBuildingSettings>();
                ComplexFabricatorWorkable complexFabricatorWorkable = go.AddOrGet<ComplexFabricatorWorkable>();
                BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
                complexFabricatorWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_manual_radbolt_generator_kanim") };
                complexFabricatorWorkable.workLayer = Grid.SceneLayer.BuildingUse;
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.UraniumOre.CreateTag(), 1f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.UraniumOre.CreateTag(), 2f),
                    new ComplexRecipe.RecipeElement(ManualHighEnergyParticleSpawnerConfig.WASTE_MATERIAL, 20f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("ManualHighEnergyParticleSpawner", array, array2), array, array2, 0, 100);
                complexRecipe.time = 20f;
                complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.MANUALHIGHENERGYPARTICLESPAWNER.RECIPE_DESCRIPTION, SimHashes.UraniumOre.CreateTag().ProperName(), ManualHighEnergyParticleSpawnerConfig.WASTE_MATERIAL.ProperName());
                complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.HEP;
                complexRecipe.fabricators = new List<Tag> { TagManager.Create("ManualHighEnergyParticleSpawner") };
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.EnrichedUranium.CreateTag(), 1f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.EnrichedUranium.CreateTag(), 2f),
                    new ComplexRecipe.RecipeElement(ManualHighEnergyParticleSpawnerConfig.WASTE_MATERIAL, 32f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe2 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("ManualHighEnergyParticleSpawner", array3, array4), array3, array4, 0, 500);
                complexRecipe2.time = 20f;
                complexRecipe2.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.MANUALHIGHENERGYPARTICLESPAWNER.RECIPE_DESCRIPTION, SimHashes.EnrichedUranium.CreateTag().ProperName(), ManualHighEnergyParticleSpawnerConfig.WASTE_MATERIAL.ProperName());
                complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.HEP;
                complexRecipe2.fabricators = new List<Tag> { TagManager.Create("ManualHighEnergyParticleSpawner") };
                go.AddOrGet<ManualHighEnergyParticleSpawner>();
                RadiationEmitter radiationEmitter = go.AddComponent<RadiationEmitter>();
                radiationEmitter.emissionOffset = new Vector3(0f, 2f);
                radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
                radiationEmitter.emitRadiusX = 3;
                radiationEmitter.emitRadiusY = 3;
                radiationEmitter.emitRads = 20f;
                return false;// 返回false以阻止原方法执行
            }
        }
    }
}

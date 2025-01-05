using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using HarmonyLib;
using UnityEngine;

namespace OmnipotenceMod
{
    public class Station_o
    {
        [HarmonyPatch(typeof(ResearchCenterConfig), "CreateBuildingDef")]// 修改基础研究站
        public class Patch_ResearchCenterConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(ResearchCenterConfig), "ConfigureBuildingTemplate")]// 修改基础研究站
        public class Patch_ResearchCenter
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<Storage>().capacityKg = 2000f;
                go.AddOrGet<ResearchCenter>().mass_per_point = 10f;
                go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(ResearchCenterConfig.INPUT_MATERIAL, 0.5f, true)
                };
            }
        }

        [HarmonyPatch(typeof(AdvancedResearchCenterConfig), "CreateBuildingDef")]// 修改超级计算机
        public class Patch_AdvancedResearchCenterConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(AdvancedResearchCenterConfig), "ConfigureBuildingTemplate")]// 修改超级计算机
        public class Patch_AdvancedResearchCenter
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<Storage>().capacityKg = 2000f;
                go.AddOrGet<ResearchCenter>().mass_per_point = 10f;
                go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(AdvancedResearchCenterConfig.INPUT_MATERIAL, 0.4f, true)
                };
            }
        }

        [HarmonyPatch(typeof(NuclearResearchCenterConfig), "CreateBuildingDef")]// 修改材料研究终端
        public class Patch_NuclearResearchCenterConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(NuclearResearchCenterConfig), "ConfigureBuildingTemplate")]// 修改材料研究终端
        public class Patch_NuclearResearchCenter
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<HighEnergyParticleStorage>().capacity = 200f;
                go.AddOrGet<NuclearResearchCenter>().materialPerPoint = 5f;
                go.AddOrGet<NuclearResearchCenter>().timePerPoint = 50f;
            }
        }

        [HarmonyPatch(typeof(CosmicResearchCenterConfig), "CreateBuildingDef")]// 修改虚拟天象仪
        public class Patch_CosmicResearchCenterConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(CosmicResearchCenterConfig), "ConfigureBuildingTemplate")]// 修改虚拟天象仪
        public class Patch_CosmicResearchCenter
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<Storage>().capacityKg = 2000f;
                go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(CosmicResearchCenterConfig.INPUT_MATERIAL, 0.01f, true)
                };
            }
        }

        [HarmonyPatch(typeof(DLC1CosmicResearchCenterConfig), "CreateBuildingDef")]// 修改DLC虚拟天象仪
        public class Patch_DLC1CosmicResearchCenterConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(DLC1CosmicResearchCenterConfig), "ConfigureBuildingTemplate")]// 修改DLC虚拟天象仪
        public class Patch_DLC1CosmicResearchCenter
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<Storage>().capacityKg = 2000f;
                go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(CosmicResearchCenterConfig.INPUT_MATERIAL, 0.01f, true)
                };
            }
        }

        [HarmonyPatch(typeof(DataMinerConfig), "CreateBuildingDef")]// 修改数据挖掘仪
        public class Patch_DataMinerConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(DataMinerConfig), "ConfigureBuildingTemplate")]// 修改数据挖掘仪
        public class Patch_DataMiner
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
                go.AddOrGet<DropAllWorkable>();
                go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
                go.AddOrGet<LogicOperationalController>();
                go.AddOrGet<CopyBuildingSettings>();
                DataMiner dataMiner = go.AddOrGet<DataMiner>();
                dataMiner.duplicantOperated = false;
                dataMiner.showProgressBar = true;
                dataMiner.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
                BuildingTemplates.CreateComplexFabricatorStorage(go, dataMiner);
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Polypropylene.CreateTag(), 1f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(DatabankHelper.TAG, 5f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                string text = ComplexRecipeManager.MakeObsoleteRecipeID("DataMiner", DatabankHelper.TAG);
                string text2 = ComplexRecipeManager.MakeRecipeID("DataMiner", array, array2);
                ComplexRecipe complexRecipe = new ComplexRecipe(text2, array, array2);
                complexRecipe.time = 30f;
                complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.Polypropylene).name, DatabankHelper.NAME);
                complexRecipe.fabricators = new List<Tag> { TagManager.Create("DataMiner") };
                complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe.sortOrder = 300;
                ComplexRecipeManager.Get().AddObsoleteIDMapping(text, text2);
                Prioritizable.AddRef(go);
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(SuitFabricatorConfig), "ConfigureRecipes")]// 太空服锻造台
        public static class Patch_SuitFabricatorConfig
        {
            public static bool Prefix(SuitFabricatorConfig __instance)
            {
                // 完全替换原有方法的逻辑
                CustomConfigureRecipes(__instance);
                return false; // 阻止原始方法执行
            }
            public static void CustomConfigureRecipes(SuitFabricatorConfig __instance)
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Copper.CreateTag(), 300f, true),
                    new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 1f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Atmo_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array, array2), array, array2)
                {
                    time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                    description = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.RECIPE_DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                    fabricators = new List<Tag> { "SuitFabricator" },
                    requiredTech = Db.Get().TechItems.atmoSuit.parentTechId,
                    sortOrder = 1
                };
                AtmoSuitConfig.recipe.RequiresAllIngredientsDiscovered = true;
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Aluminum.CreateTag(), 300f, true),
                    new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 1f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Atmo_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array3, array4), array3, array4)
                {
                    time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                    description = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.RECIPE_DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                    fabricators = new List<Tag> { "SuitFabricator" },
                    requiredTech = Db.Get().TechItems.atmoSuit.parentTechId,
                    sortOrder = 1
                };
                AtmoSuitConfig.recipe.RequiresAllIngredientsDiscovered = true;
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Iron.CreateTag(), 300f, true),
                    new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 1f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Atmo_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array5, array6), array5, array6)
                {
                    time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                    description = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.RECIPE_DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                    fabricators = new List<Tag> { "SuitFabricator" },
                    requiredTech = Db.Get().TechItems.atmoSuit.parentTechId,
                    sortOrder = 1
                };
                AtmoSuitConfig.recipe.RequiresAllIngredientsDiscovered = true;
                if (ElementLoader.FindElementByHash(SimHashes.Cobalt) != null)
                {
                    ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.Cobalt.CreateTag(), 300f, true),
                        new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 1f)
                    };
                    ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("Atmo_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                    };
                    AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array7, array8), array7, array8)
                    {
                        time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                        description = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.RECIPE_DESC,
                        nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                        fabricators = new List<Tag> { "SuitFabricator" },
                        requiredTech = Db.Get().TechItems.atmoSuit.parentTechId,
                        sortOrder = 1
                    };
                    AtmoSuitConfig.recipe.RequiresAllIngredientsDiscovered = true;
                }
                ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Worn_Atmo_Suit".ToTag(), 1f, true),
                    new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 1f)
                };
                ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Atmo_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array9, array10), array9, array10)
                {
                    time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                    description = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.REPAIR_WORN_DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Custom,
                    fabricators = new List<Tag> { "SuitFabricator" },
                    requiredTech = Db.Get().TechItems.atmoSuit.parentTechId,
                    sortOrder = 2
                };
                AtmoSuitConfig.recipe.customName = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.REPAIR_WORN_RECIPE_NAME;
                AtmoSuitConfig.recipe.ProductHasFacade = true;
                ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Steel.ToString(), 200f),
                    new ComplexRecipe.RecipeElement(SimHashes.Petroleum.ToString(), 25f)
                };
                ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Jet_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                JetSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array11, array12), array11, array12)
                {
                    time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                    description = global::STRINGS.EQUIPMENT.PREFABS.JET_SUIT.RECIPE_DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                    fabricators = new List<Tag> { "SuitFabricator" },
                    requiredTech = Db.Get().TechItems.jetSuit.parentTechId,
                    sortOrder = 3
                };
                ComplexRecipe.RecipeElement[] array13 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Worn_Jet_Suit".ToTag(), 1f),
                    new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 1f)
                };
                ComplexRecipe.RecipeElement[] array14 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Jet_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                JetSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array13, array14), array13, array14)
                {
                    time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                    description = global::STRINGS.EQUIPMENT.PREFABS.JET_SUIT.RECIPE_DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                    fabricators = new List<Tag> { "SuitFabricator" },
                    requiredTech = Db.Get().TechItems.jetSuit.parentTechId,
                    sortOrder = 4
                };
                if (DlcManager.FeatureRadiationEnabled())
                {
                    ComplexRecipe.RecipeElement[] array15 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.Lead.ToString(), 200f),
                        new ComplexRecipe.RecipeElement(SimHashes.Glass.ToString(), 10f)
                    };
                    ComplexRecipe.RecipeElement[] array16 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("Lead_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                    };
                    LeadSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array15, array16), array15, array16)
                    {
                        time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                        description = global::STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.RECIPE_DESC,
                        nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                        fabricators = new List<Tag> { "SuitFabricator" },
                        requiredTech = Db.Get().TechItems.leadSuit.parentTechId,
                        sortOrder = 5
                    };
                }
                if (DlcManager.FeatureRadiationEnabled())
                {
                    ComplexRecipe.RecipeElement[] array17 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("Worn_Lead_Suit".ToTag(), 1f),
                        new ComplexRecipe.RecipeElement(SimHashes.Glass.ToString(), 5f)
                    };
                    ComplexRecipe.RecipeElement[] array18 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("Lead_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                    };
                    LeadSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array17, array18), array17, array18)
                    {
                        time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
                        description = global::STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.RECIPE_DESC,
                        nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                        fabricators = new List<Tag> { "SuitFabricator" },
                        requiredTech = Db.Get().TechItems.leadSuit.parentTechId,
                        sortOrder = 6
                    };
                }
            }
        }

        [HarmonyPatch(typeof(ClothingFabricatorConfig), "ConfigureRecipes")]// 纺织机
        public static class Patch_ClothingFabricatorConfig
        {
            public static bool Prefix(ClothingFabricatorConfig __instance)
            {
                // 完全替换原有方法的逻辑
                CustomConfigureRecipes(__instance);
                return false; // 阻止原始方法执行
            }
            public static void CustomConfigureRecipes(ClothingFabricatorConfig __instance)
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 2f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Warm_Vest".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                WarmVestConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("ClothingFabricator", array, array2), array, array2)
                {
                    time = global::TUNING.EQUIPMENT.VESTS.WARM_VEST_FABTIME,
                    description = global::STRINGS.EQUIPMENT.PREFABS.WARM_VEST.RECIPE_DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "ClothingFabricator" },
                    sortOrder = 1
                };
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 2f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Funky_Vest".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                FunkyVestConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("ClothingFabricator", array3, array4), array3, array4)
                {
                    time = global::TUNING.EQUIPMENT.VESTS.FUNKY_VEST_FABTIME,
                    description = global::STRINGS.EQUIPMENT.PREFABS.FUNKY_VEST.RECIPE_DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "ClothingFabricator" },
                    sortOrder = 1
                };
            }
        }

        [HarmonyPatch(typeof(ClothingAlterationStationConfig), "ConfigureRecipes")]// 时装翻新器
        public static class Patch_ClothingAlterationStationConfig
        {
            public static bool Prefix(ClothingAlterationStationConfig __instance)
            {
                // 完全替换原有方法的逻辑
                CustomConfigureRecipes(__instance);
                return false; // 阻止原始方法执行
            }
            public static void CustomConfigureRecipes(ClothingAlterationStationConfig __instance)
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Funky_Vest".ToTag(), 1f, false),
                    new ComplexRecipe.RecipeElement("BasicFabric".ToTag(), 2f)
                };
                foreach (EquippableFacadeResource equippableFacadeResource in Db.GetEquippableFacades().resources.FindAll((EquippableFacadeResource match) => match.DefID == "CustomClothing"))
                {
                    ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("CustomClothing".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, equippableFacadeResource.Id, false)
                    };
                    ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("ClothingAlterationStation", array, array2, equippableFacadeResource.Id), array, array2);
                    complexRecipe.time = global::TUNING.EQUIPMENT.VESTS.CUSTOM_CLOTHING_FABTIME;
                    complexRecipe.description = global::STRINGS.EQUIPMENT.PREFABS.CUSTOMCLOTHING.RECIPE_DESC;
                    complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                    complexRecipe.fabricators = new List<Tag> { "ClothingAlterationStation" };
                    complexRecipe.sortOrder = 1;
                }
            }
        }
    }
}

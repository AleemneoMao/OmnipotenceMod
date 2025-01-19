using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace OmnipotenceMod
{
    public class FoodBudling_o
    {
        [HarmonyPatch(typeof(CookingStationConfig), "CreateBuildingDef")]//修改电动烤炉
        public class Patch_CookingStationConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(CookingStationConfig), "ConfigureRecipes")]// 电动烤炉
        public static class Patch_CookingStation
        {
            public static bool Prefix(CookingStationConfig __instance)
            {
                // 完全替换原有方法的逻辑
                CustomConfigureRecipes(__instance);
                return false; // 阻止原始方法执行
            }
            public static void CustomConfigureRecipes(CookingStationConfig __instance)
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BasicPlantFood", 2f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("PickledMeal", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                PickledMealConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array, array2), array, array2)
                {
                    time = 20f,
                    description = global::STRINGS.ITEMS.FOOD.PICKLEDMEAL.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 21
                };
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("MushBar", 1f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("FriedMushBar".ToTag(), 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                FriedMushBarConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array3, array4), array3, array4)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.FRIEDMUSHBAR.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 1
                };
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(MushroomConfig.ID, 1f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("FriedMushroom", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                FriedMushroomConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array5, array6), array5, array6)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.FRIEDMUSHROOM.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 20
                };
                ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("RawEgg", 1f),
                    new ComplexRecipe.RecipeElement("ColdWheatSeed", 2f)
                };
                ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Pancakes", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                CookedEggConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array7, array8), array7, array8)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.PANCAKES.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 20
                };
                ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Meat", 1f)
                };
                ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CookedMeat", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                CookedMeatConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array9, array10), array9, array10)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.COOKEDMEAT.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 21
                };
                ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("FishMeat", 1f)
                };
                ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CookedFish", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                CookedMeatConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array11, array12), array11, array12)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.COOKEDMEAT.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 22
                };
                ComplexRecipe.RecipeElement[] array13 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("ShellfishMeat", 1f)
                };
                ComplexRecipe.RecipeElement[] array14 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CookedFish", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                CookedMeatConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array13, array14), array13, array14)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.COOKEDMEAT.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 22
                };
                ComplexRecipe.RecipeElement[] array15 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(PrickleFruitConfig.ID, 1f)
                };
                ComplexRecipe.RecipeElement[] array16 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("GrilledPrickleFruit", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                GrilledPrickleFruitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array15, array16), array15, array16)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.GRILLEDPRICKLEFRUIT.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 20
                };
                if (DlcManager.IsExpansion1Active())
                {
                    ComplexRecipe.RecipeElement[] array17 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SwampFruitConfig.ID, 1f)
                    };
                    ComplexRecipe.RecipeElement[] array18 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("SwampDelights", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                    };
                    CookedEggConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array17, array18), array17, array18)
                    {
                        time = 30f,
                        description = global::STRINGS.ITEMS.FOOD.SWAMPDELIGHTS.RECIPEDESC,
                        nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                        fabricators = new List<Tag> { "CookingStation" },
                        sortOrder = 20
                    };
                }
                ComplexRecipe.RecipeElement[] array19 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("ColdWheatSeed", 1f)
                };
                ComplexRecipe.RecipeElement[] array20 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("ColdWheatBread", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                ColdWheatBreadConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array19, array20), array19, array20)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.COLDWHEATBREAD.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 50
                };
                ComplexRecipe.RecipeElement[] array21 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("RawEgg", 1f)
                };
                ComplexRecipe.RecipeElement[] array22 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CookedEgg", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                CookedEggConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array21, array22), array21, array22)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.COOKEDEGG.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 1
                };
                if (DlcManager.IsExpansion1Active())
                {
                    ComplexRecipe.RecipeElement[] array23 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("WormBasicFruit", 1f)
                    };
                    ComplexRecipe.RecipeElement[] array24 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("WormBasicFood", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                    };
                    WormBasicFoodConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array23, array24), array23, array24)
                    {
                        time = 30f,
                        description = global::STRINGS.ITEMS.FOOD.WORMBASICFOOD.RECIPEDESC,
                        nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                        fabricators = new List<Tag> { "CookingStation" },
                        sortOrder = 20
                    };
                }
                if (DlcManager.IsExpansion1Active())
                {
                    ComplexRecipe.RecipeElement[] array25 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("WormSuperFruit", 4f),
                        new ComplexRecipe.RecipeElement("Sucrose".ToTag(), 2f)
                    };
                    ComplexRecipe.RecipeElement[] array26 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("WormSuperFood", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                    };
                    WormSuperFoodConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array25, array26), array25, array26)
                    {
                        time = 30f,
                        description = global::STRINGS.ITEMS.FOOD.WORMSUPERFOOD.RECIPEDESC,
                        nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                        fabricators = new List<Tag> { "CookingStation" },
                        sortOrder = 20
                    };
                }
                ComplexRecipe.RecipeElement[] array27 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("HardSkinBerry", 1f)
                };
                ComplexRecipe.RecipeElement[] array28 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CookedPikeapple", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                CookedPikeappleConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array27, array28), array27, array28, DlcManager.AVAILABLE_DLC_2)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.COOKEDPIKEAPPLE.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 18
                };
            }
        }

        [HarmonyPatch(typeof(GourmetCookingStationConfig), "CreateBuildingDef")]//修改燃气灶
        public class Patch_GourmetCookingStationConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 120f;
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(GourmetCookingStationConfig), "ConfigureBuildingTemplate")]//修改燃气灶
        public class Patch_GourmetCookingStationBuildingTemplate
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement("Methane", 0.05f, true)
                };
                go.AddOrGet<ElementConverter>().outputElements = new ElementConverter.OutputElement[]
                {
                    new ElementConverter.OutputElement(0.01f, SimHashes.CarbonDioxide, 318.15f, false, false, 0f, 2f, 1f, byte.MaxValue, 0, true)
                };
            }
        }

        [HarmonyPatch(typeof(GourmetCookingStationConfig), "ConfigureRecipes")]// 燃气灶
        public static class Patch_GourmetCookingStation
        {
            public static bool Prefix(GourmetCookingStationConfig __instance)
            {
                // 完全替换原有方法的逻辑
                CustomConfigureRecipes(__instance);
                return false; // 阻止原始方法执行
            }
            public static void CustomConfigureRecipes(GourmetCookingStationConfig __instance)
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("GrilledPrickleFruit", 2f),
                    new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 2f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Salsa", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                SalsaConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array, array2), array, array2)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.SALSA.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "GourmetCookingStation" },
                    sortOrder = 300
                };
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("FriedMushroom", 1f),
                    new ComplexRecipe.RecipeElement("Lettuce", 4f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("MushroomWrap", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                MushroomWrapConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array3, array4), array3, array4)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.MUSHROOMWRAP.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "GourmetCookingStation" },
                    sortOrder = 400
                };
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CookedMeat", 1f),
                    new ComplexRecipe.RecipeElement("CookedFish", 1f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("SurfAndTurf", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                SurfAndTurfConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array5, array6), array5, array6)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.SURFANDTURF.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "GourmetCookingStation" },
                    sortOrder = 500
                };
                ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("ColdWheatSeed", 10f),
                    new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 1f)
                };
                ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("SpiceBread", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                SpiceBreadConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array7, array8), array7, array8)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.SPICEBREAD.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "GourmetCookingStation" },
                    sortOrder = 600
                };
                ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Tofu", 1f),
                    new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 1f)
                };
                ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("SpicyTofu", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                SpicyTofuConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array9, array10), array9, array10)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.SPICYTOFU.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "GourmetCookingStation" },
                    sortOrder = 800
                };
                ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(GingerConfig.ID, 4f),
                    new ComplexRecipe.RecipeElement("BeanPlantSeed", 4f)
                };
                ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Curry", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                SpicyTofuConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array11, array12), array11, array12)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.CURRY.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "GourmetCookingStation" },
                    sortOrder = 800
                };
                ComplexRecipe.RecipeElement[] array13 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CookedEgg", 1f),
                    new ComplexRecipe.RecipeElement("Lettuce", 1f),
                    new ComplexRecipe.RecipeElement("FriedMushroom", 1f)
                };
                ComplexRecipe.RecipeElement[] array14 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Quiche", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                QuicheConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array13, array14), array13, array14)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.QUICHE.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "GourmetCookingStation" },
                    sortOrder = 800
                };
                ComplexRecipe.RecipeElement[] array15 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("ColdWheatBread", 1f),
                    new ComplexRecipe.RecipeElement("Lettuce", 1f),
                    new ComplexRecipe.RecipeElement("CookedMeat", 1f)
                };
                ComplexRecipe.RecipeElement[] array16 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Burger", 3f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                BurgerConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array15, array16), array15, array16)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.BURGER.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "GourmetCookingStation" },
                    sortOrder = 900
                };
                if (DlcManager.IsExpansion1Active())
                {
                    ComplexRecipe.RecipeElement[] array17 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("ColdWheatSeed", 3f),
                        new ComplexRecipe.RecipeElement("WormSuperFruit", 4f),
                        new ComplexRecipe.RecipeElement("GrilledPrickleFruit", 1f)
                    };
                    ComplexRecipe.RecipeElement[] array18 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("BerryPie", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                    };
                    BerryPieConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array17, array18), array17, array18)
                    {
                        time = 30f,
                        description = global::STRINGS.ITEMS.FOOD.BERRYPIE.RECIPEDESC,
                        nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                        fabricators = new List<Tag> { "GourmetCookingStation" },
                        sortOrder = 900
                    };
                }
            }
        }

        [HarmonyPatch(typeof(MicrobeMusherConfig), "CreateBuildingDef")]//修改食物压制器
        public class Patch_MicrobeMusherConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 120f;
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(MicrobeMusherConfig), "ConfigureRecipes")]// 食物压制器
        public static class Patch_MicrobeMusher
        {
            public static bool Prefix(MicrobeMusherConfig __instance)
            {
                // 完全替换原有方法的逻辑
                CustomConfigureRecipes(__instance);
                return false; // 阻止原始方法执行
            }
            public static void CustomConfigureRecipes(MicrobeMusherConfig __instance)
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Dirt".ToTag(), 75f),
                    new ComplexRecipe.RecipeElement("Water".ToTag(), 75f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("MushBar".ToTag(), 2f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                MushBarConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array, array2), array, array2)
                {
                    time = 20f,
                    description = global::STRINGS.ITEMS.FOOD.MUSHBAR.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "MicrobeMusher" },
                    sortOrder = 1
                };
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BasicPlantFood", 2f),
                    new ComplexRecipe.RecipeElement("Water".ToTag(), 50f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BasicPlantBar".ToTag(), 2f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                BasicPlantBarConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array3, array4), array3, array4)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.BASICPLANTBAR.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "MicrobeMusher" },
                    sortOrder = 2
                };
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BeanPlantSeed", 6f),
                    new ComplexRecipe.RecipeElement("Water".ToTag(), 50f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Tofu".ToTag(), 2f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                TofuConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array5, array6), array5, array6)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.TOFU.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "MicrobeMusher" },
                    sortOrder = 3
                };
                ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("ColdWheatSeed", 5f),
                    new ComplexRecipe.RecipeElement(PrickleFruitConfig.ID, 1f)
                };
                ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("FruitCake".ToTag(), 2f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                FruitCakeConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array7, array8), array7, array8)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.FRUITCAKE.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "MicrobeMusher" },
                    sortOrder = 3
                };
                ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Meat", 1f),
                    new ComplexRecipe.RecipeElement("Tallow", 1f)
                };
                ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("Pemmican".ToTag(), 2f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                PemmicanConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array9, array10), array9, array10, DlcManager.AVAILABLE_DLC_2)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.PEMMICAN.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "MicrobeMusher" },
                    sortOrder = 4
                };
            }
        }

        [HarmonyPatch(typeof(DeepfryerConfig), "CreateBuildingDef")]//修改油炸锅
        public class Patch_DeepfryerConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 240f;
                __result.ExhaustKilowattsWhenActive = 1f;
                __result.SelfHeatKilowattsWhenActive = 1f;
            }
        }

        [HarmonyPatch(typeof(DeepfryerConfig), "ConfigureRecipes")]// 油炸锅
        public static class Patch_Deepfryer
        {
            public static bool Prefix(DeepfryerConfig __instance)
            {
                // 完全替换原有方法的逻辑
                CustomConfigureRecipes(__instance);
                return false; // 阻止原始方法执行
            }
            public static void CustomConfigureRecipes(DeepfryerConfig __instance)
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(CarrotConfig.ID, 1f),
                    new ComplexRecipe.RecipeElement("Tallow", 1f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("FriesCarrot", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                FriesCarrotConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Deepfryer", array, array2), array, array2, DlcManager.DLC2)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.FRIESCARROT.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "Deepfryer" },
                    sortOrder = 100
                };
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BeanPlantSeed", 6f),
                    new ComplexRecipe.RecipeElement("Tallow", 1f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("DeepFriedNosh", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                FriesCarrotConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Deepfryer", array3, array4), array3, array4, DlcManager.DLC2)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.DEEPFRIEDNOSH.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "Deepfryer" },
                    sortOrder = 200
                };
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("FishMeat", 1f),
                    new ComplexRecipe.RecipeElement("Tallow", 2.4f),
                    new ComplexRecipe.RecipeElement("ColdWheatSeed", 2f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("DeepFriedFish", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                DeepFriedFishConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Deepfryer", array5, array6), array5, array6, DlcManager.DLC2)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.DEEPFRIEDFISH.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "Deepfryer" },
                    sortOrder = 300
                };
                ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("ShellfishMeat", 1f),
                    new ComplexRecipe.RecipeElement("Tallow", 2.4f),
                    new ComplexRecipe.RecipeElement("ColdWheatSeed", 2f)
                };
                ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("DeepFriedShellfish", 2f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                DeepFriedShellfishConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Deepfryer", array7, array8), array7, array8, DlcManager.DLC2)
                {
                    time = 30f,
                    description = global::STRINGS.ITEMS.FOOD.DEEPFRIEDSHELLFISH.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "Deepfryer" },
                    sortOrder = 300
                };
            }
        }
    }
}

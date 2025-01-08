using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using STRINGS;
using UnityEngine;
using static STRINGS.ELEMENTS;

namespace OmnipotenceMod
{
    public class RefineBudling_o
    {
        [HarmonyPatch(typeof(OxyliteRefineryConfig), "CreateBuildingDef")]//修改氧石精炼炉
        public class Patch_OxyliteRefineryConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 240f;
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(OxyliteRefineryConfig), "ConfigureBuildingTemplate")]//氧石精炼炉
        public class Patch_OxyliteRefinery
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                Tag tag = SimHashes.Oxygen.CreateTag();
                Tag tag2 = SimHashes.Gold.CreateTag();
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
                OxyliteRefinery oxyliteRefinery = go.AddOrGet<OxyliteRefinery>();
                oxyliteRefinery.emitTag = SimHashes.OxyRock.CreateTag();
                oxyliteRefinery.emitMass = 10f;
                oxyliteRefinery.dropOffset = new Vector3(0f, 1f);
                ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
                conduitConsumer.conduitType = ConduitType.Gas;
                conduitConsumer.consumptionRate = 1.2f;
                conduitConsumer.capacityTag = tag;
                conduitConsumer.capacityKG = 6f;
                conduitConsumer.forceAlwaysSatisfied = true;
                conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
                Storage storage = go.AddOrGet<Storage>();
                storage.capacityKg = 23.2f;
                storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
                storage.showInUI = true;
                ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
                manualDeliveryKG.SetStorage(storage);
                manualDeliveryKG.RequestedItemTag = tag2;
                manualDeliveryKG.refillMass = 1.8000001f;
                manualDeliveryKG.capacity = 7.2000003f;
                manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
                ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
                elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(tag, 0.2f, true),
                    new ElementConverter.ConsumedElement(tag2, 0.001f, true)
                };
                elementConverter.outputElements = new ElementConverter.OutputElement[]
                {
                    new ElementConverter.OutputElement(1.0f, SimHashes.OxyRock, 303.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
                };
                Prioritizable.AddRef(go);
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(SupermaterialRefineryConfig), "CreateBuildingDef")]//修改分子熔炉
        public class Patch_SupermaterialRefineryConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 320f;
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(SupermaterialRefineryConfig), "ConfigureBuildingTemplate")]//分子熔炉
        public class Patch_SupermaterialRefinery
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<DropAllWorkable>();
                go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
                ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
                complexFabricator.heatedTemperature = 313.15f;
                complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
                complexFabricator.duplicantOperated = true;
                go.AddOrGet<FabricatorIngredientStatusManager>();
                go.AddOrGet<CopyBuildingSettings>();
                go.AddOrGet<ComplexFabricatorWorkable>();
                BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
                Prioritizable.AddRef(go);
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Fullerene.CreateTag(), 0.1f),
                    new ComplexRecipe.RecipeElement(SimHashes.Gold.CreateTag(), 5f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.SuperCoolant.CreateTag(), 1000f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array, array2), array, array2);
                complexRecipe.time = 40f;
                complexRecipe.description = global::STRINGS.BUILDINGS.PREFABS.SUPERMATERIALREFINERY.SUPERCOOLANT_RECIPE_DESCRIPTION;
                complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                complexRecipe.fabricators = new List<Tag> { TagManager.Create("SupermaterialRefinery") };
                if (DlcManager.IsExpansion1Active())
                {
                    ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.Graphite.CreateTag(), 9f),
                        new ComplexRecipe.RecipeElement(SimHashes.Aluminum.CreateTag(), 0.5f)
                    };
                    ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.Fullerene.CreateTag(), 1000f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                    };
                    ComplexRecipe complexRecipe2 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array3, array4), array3, array4);
                    complexRecipe2.time = 40f;
                    complexRecipe2.description = global::STRINGS.BUILDINGS.PREFABS.SUPERMATERIALREFINERY.FULLERENE_RECIPE_DESCRIPTION;
                    complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                    complexRecipe2.fabricators = new List<Tag> { TagManager.Create("SupermaterialRefinery") };
                }
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.TempConductorSolid.CreateTag(), 1.5f),
                    new ComplexRecipe.RecipeElement(SimHashes.Polypropylene.CreateTag(), 7f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.HardPolypropylene.CreateTag(), 1000f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                ComplexRecipe complexRecipe3 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array5, array6), array5, array6);
                complexRecipe3.time = 40f;
                complexRecipe3.description = global::STRINGS.BUILDINGS.PREFABS.SUPERMATERIALREFINERY.HARDPLASTIC_RECIPE_DESCRIPTION;
                complexRecipe3.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                complexRecipe3.fabricators = new List<Tag> { TagManager.Create("SupermaterialRefinery") };
                ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Isoresin.CreateTag(), 1.5f),
                    new ComplexRecipe.RecipeElement(SimHashes.Katairite.CreateTag(), 8f)
                };
                ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.SuperInsulator.CreateTag(), 1000f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                ComplexRecipe complexRecipe4 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array7, array8), array7, array8);
                complexRecipe4.time = 40f;
                complexRecipe4.description = global::STRINGS.BUILDINGS.PREFABS.SUPERMATERIALREFINERY.SUPERINSULATOR_RECIPE_DESCRIPTION;
                complexRecipe4.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                complexRecipe4.fabricators = new List<Tag> { TagManager.Create("SupermaterialRefinery") };
                ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Niobium.CreateTag(), 0.5f),
                    new ComplexRecipe.RecipeElement(SimHashes.Tungsten.CreateTag(), 9.5f)
                };
                ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.TempConductorSolid.CreateTag(), 1000f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                ComplexRecipe complexRecipe5 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array9, array10), array9, array10);
                complexRecipe5.time = 40f;
                complexRecipe5.description = global::STRINGS.BUILDINGS.PREFABS.SUPERMATERIALREFINERY.TEMPCONDUCTORSOLID_RECIPE_DESCRIPTION;
                complexRecipe5.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                complexRecipe5.fabricators = new List<Tag> { TagManager.Create("SupermaterialRefinery") };
                ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Isoresin.CreateTag(), 3.5f),
                    new ComplexRecipe.RecipeElement(SimHashes.Petroleum.CreateTag(), 6.5f)
                };
                ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.ViscoGel.CreateTag(), 1000f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
                };
                ComplexRecipe complexRecipe6 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array11, array12), array11, array12);
                complexRecipe6.time = 40f;
                complexRecipe6.description = global::STRINGS.BUILDINGS.PREFABS.SUPERMATERIALREFINERY.VISCOGEL_RECIPE_DESCRIPTION;
                complexRecipe6.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                complexRecipe6.fabricators = new List<Tag> { TagManager.Create("SupermaterialRefinery") };
                if (DlcManager.IsAllContentSubscribed(new string[] { "DLC3_ID", "EXPANSION1_ID" }))
                {
                    ComplexRecipe.RecipeElement[] array13 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.EnrichedUranium.CreateTag(), 10f)
                    };
                    ComplexRecipe.RecipeElement[] array14 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement("SelfChargingElectrobank", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                    };
                    ComplexRecipe complexRecipe7 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array13, array14), array13, array14, new string[] { "DLC3_ID" });
                    complexRecipe7.time = 40f;
                    complexRecipe7.description = global::STRINGS.BUILDINGS.PREFABS.SUPERMATERIALREFINERY.SELF_CHARGING_POWERBANK_RECIPE_DESCRIPTION;
                    complexRecipe7.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                    complexRecipe7.fabricators = new List<Tag> { TagManager.Create("SupermaterialRefinery") };
                    complexRecipe7.requiredTech = Db.Get().TechItems.selfChargingElectrobank.parentTechId;
                }
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(GlassForgeConfig), "CreateBuildingDef")]//修改玻璃熔炉
        public class Patch_GlassForgeConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 240f;
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(WaterPurifierConfig), "CreateBuildingDef")]//修改净水器
        public class Patch_WaterPurifierConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(WaterPurifierConfig), "ConfigureBuildingTemplate")]//净水器
        public class Patch_WaterPurifier
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
                Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
                storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
                go.AddOrGet<WaterPurifier>();
                Prioritizable.AddRef(go);
                ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
                elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(new Tag("DirtyWater"), 2f, true)
                };
                elementConverter.outputElements = new ElementConverter.OutputElement[]
                {
                    new ElementConverter.OutputElement(8f, SimHashes.Water, 0f, false, true, 0f, 0.5f, 0.75f, byte.MaxValue, 0, true)
                };
                ElementDropper elementDropper = go.AddComponent<ElementDropper>();
                elementDropper.emitMass = 10f;
                elementDropper.emitTag = new Tag("ToxicSand");
                elementDropper.emitOffset = new Vector3(0f, 1f, 0f);
                ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
                manualDeliveryKG.SetStorage(storage);
                manualDeliveryKG.RequestedItemTag = new Tag("Filter");
                manualDeliveryKG.capacity = 1200f;
                manualDeliveryKG.refillMass = 300f;
                manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
                ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
                conduitConsumer.conduitType = ConduitType.Liquid;
                conduitConsumer.consumptionRate = 10f;
                conduitConsumer.capacityKG = 20f;
                conduitConsumer.capacityTag = GameTags.AnyWater;
                conduitConsumer.forceAlwaysSatisfied = true;
                conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
                ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
                conduitDispenser.conduitType = ConduitType.Liquid;
                conduitDispenser.invertElementFilter = true;
                conduitDispenser.elementFilter = new SimHashes[] { SimHashes.DirtyWater };
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(MetalRefineryConfig), "CreateBuildingDef")]//修改金属精炼器
        public class Patch_MetalRefineryConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 240f;
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(MetalRefineryConfig), "ConfigureBuildingTemplate")]//金属精炼器
        public class Patch_MetalRefinery
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
                go.AddOrGet<DropAllWorkable>();
                go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
                LiquidCooledRefinery liquidCooledRefinery = go.AddOrGet<LiquidCooledRefinery>();
                liquidCooledRefinery.duplicantOperated = true;
                liquidCooledRefinery.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
                liquidCooledRefinery.keepExcessLiquids = true;
                go.AddOrGet<FabricatorIngredientStatusManager>();
                go.AddOrGet<CopyBuildingSettings>();
                Workable workable = go.AddOrGet<ComplexFabricatorWorkable>();
                BuildingTemplates.CreateComplexFabricatorStorage(go, liquidCooledRefinery);
                liquidCooledRefinery.coolantTag = GameTags.Liquid;
                liquidCooledRefinery.minCoolantMass = 400f;
                liquidCooledRefinery.outStorage.capacityKg = 2000f;
                liquidCooledRefinery.thermalFudge = 0.3f;
                liquidCooledRefinery.inStorage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
                    {
                        Storage.StoredItemModifier.Hide,
                        Storage.StoredItemModifier.Preserve,
                        Storage.StoredItemModifier.Insulate,
                        Storage.StoredItemModifier.Seal
                    });
                liquidCooledRefinery.buildStorage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
                    {
                        Storage.StoredItemModifier.Hide,
                        Storage.StoredItemModifier.Preserve,
                        Storage.StoredItemModifier.Insulate,
                        Storage.StoredItemModifier.Seal
                    });
                liquidCooledRefinery.outStorage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
                    {
                        Storage.StoredItemModifier.Hide,
                        Storage.StoredItemModifier.Preserve,
                        Storage.StoredItemModifier.Insulate,
                        Storage.StoredItemModifier.Seal
                    });
                liquidCooledRefinery.outputOffset = new Vector3(1f, 0.5f);
                workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_metalrefinery_kanim") };
                go.AddOrGet<RequireOutputs>().ignoreFullPipe = true;
                ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
                conduitConsumer.capacityTag = GameTags.Liquid;
                conduitConsumer.capacityKG = 800f;
                conduitConsumer.storage = liquidCooledRefinery.inStorage;
                conduitConsumer.alwaysConsume = true;
                conduitConsumer.forceAlwaysSatisfied = true;
                ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
                conduitDispenser.storage = liquidCooledRefinery.outStorage;
                conduitDispenser.conduitType = ConduitType.Liquid;
                conduitDispenser.elementFilter = null;
                conduitDispenser.alwaysDispense = true;
                foreach (Element element in ElementLoader.elements.FindAll((Element e) => e.IsSolid && e.HasTag(GameTags.Metal)))
                {
                    if (!element.HasTag(GameTags.Noncrushable))
                    {
                        Element lowTempTransition = element.highTempTransition.lowTempTransition;
                        if (lowTempTransition != element)
                        {
                            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                            {
                                new ComplexRecipe.RecipeElement(element.tag, 100f)
                            };
                            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                            {
                                new ComplexRecipe.RecipeElement(lowTempTransition.tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                            };
                            string text = ComplexRecipeManager.MakeObsoleteRecipeID("MetalRefinery", element.tag);
                            string text2 = ComplexRecipeManager.MakeRecipeID("MetalRefinery", array, array2);
                            ComplexRecipe complexRecipe = new ComplexRecipe(text2, array, array2);
                            complexRecipe.time = 40f;
                            complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.METALREFINERY.RECIPE_DESCRIPTION, lowTempTransition.name, element.name);
                            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                            complexRecipe.fabricators = new List<Tag> { TagManager.Create("MetalRefinery") };
                            ComplexRecipeManager.Get().AddObsoleteIDMapping(text, text2);
                        }
                    }
                }
                Element element2 = ElementLoader.FindElementByHash(SimHashes.Steel);
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Iron).tag, 70f),
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.RefinedCarbon).tag, 20f),
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Lime).tag, 10f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Steel).tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                string text3 = ComplexRecipeManager.MakeObsoleteRecipeID("MetalRefinery", element2.tag);
                string text4 = ComplexRecipeManager.MakeRecipeID("MetalRefinery", array3, array4);
                ComplexRecipe complexRecipe2 = new ComplexRecipe(text4, array3, array4);
                complexRecipe2.time = 40f;
                complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe2.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.METALREFINERY.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.Steel).name, ElementLoader.FindElementByHash(SimHashes.Iron).name);
                complexRecipe2.fabricators = new List<Tag> { TagManager.Create("MetalRefinery") };
                ComplexRecipeManager.Get().AddObsoleteIDMapping(text3, text4);
                Prioritizable.AddRef(go);
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(OilRefineryConfig), "CreateBuildingDef")]//修改原油精炼器
        public class Patch_OilRefineryConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(OilRefineryConfig), "ConfigureBuildingTemplate")]//原油精炼器
        public class Patch_OilRefinery
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
                go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
                OilRefinery oilRefinery = go.AddOrGet<OilRefinery>();
                oilRefinery.overpressureWarningMass = 4.5f;
                oilRefinery.overpressureMass = 5f;
                ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
                conduitConsumer.conduitType = ConduitType.Liquid;
                conduitConsumer.consumptionRate = 10f;
                conduitConsumer.capacityTag = SimHashes.CrudeOil.CreateTag();
                conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
                conduitConsumer.capacityKG = 100f;
                conduitConsumer.forceAlwaysSatisfied = true;
                ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
                conduitDispenser.conduitType = ConduitType.Liquid;
                conduitDispenser.invertElementFilter = true;
                conduitDispenser.elementFilter = new SimHashes[] { SimHashes.CrudeOil };
                go.AddOrGet<Storage>().showInUI = true;
                ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
                elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(SimHashes.CrudeOil.CreateTag(), 10f, true)
                };
                elementConverter.outputElements = new ElementConverter.OutputElement[]
                {
                    new ElementConverter.OutputElement(8f, SimHashes.Petroleum, 313.15f, false, true, 0f, 1f, 1f, byte.MaxValue, 0, true)
                };
                Prioritizable.AddRef(go);
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(PolymerizerConfig), "CreateBuildingDef")]//修改聚合物压塑器
        public class Patch_PolymerizerConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(PolymerizerConfig), "ConfigureBuildingTemplate")]//聚合物压塑器
        public class Patch_Polymerizer
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
                Polymerizer polymerizer = go.AddOrGet<Polymerizer>();
                polymerizer.emitMass = 30f;
                polymerizer.emitTag = GameTagExtensions.Create(SimHashes.Polypropylene);
                polymerizer.emitOffset = new Vector3(-1.45f, 1f, 0f);
                polymerizer.exhaustElement = SimHashes.Steam;
                ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
                conduitConsumer.conduitType = ConduitType.Liquid;
                conduitConsumer.consumptionRate = 1.6666666f;
                conduitConsumer.capacityTag = PolymerizerConfig.INPUT_ELEMENT_TAG;
                conduitConsumer.capacityKG = 1.6666666f;
                conduitConsumer.forceAlwaysSatisfied = true;
                conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
                ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
                conduitDispenser.conduitType = ConduitType.Gas;
                conduitDispenser.invertElementFilter = false;
                conduitDispenser.elementFilter = new SimHashes[] { SimHashes.CarbonDioxide };
                ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
                elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(PolymerizerConfig.INPUT_ELEMENT_TAG, 0.2f, true)
                };
                elementConverter.outputElements = new ElementConverter.OutputElement[]
                {
                    new ElementConverter.OutputElement(2f, SimHashes.Polypropylene, 303.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true),
                    new ElementConverter.OutputElement(0.004f, SimHashes.CarbonDioxide, 303.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
                };
                go.AddOrGet<DropAllWorkable>();
                Prioritizable.AddRef(go);
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(MilkPressConfig), "CreateBuildingDef")]//修改植物粉碎机
        public class Patch_MilkPressConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(MilkPressConfig), "AddRecipes")]//植物粉碎机
        public class Patch_MilkPress
        {
            public static bool Prefix(ref object __instance, GameObject go)
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("ColdWheatSeed", 10f),
                    new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 15f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Milk.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MilkPress", array, array2), array, array2, 0, 0);
                complexRecipe.time = 20f;
                complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.MILKPRESS.WHEAT_MILK_RECIPE_DESCRIPTION, global::STRINGS.ITEMS.FOOD.COLDWHEATSEED.NAME, SimHashes.Milk.CreateTag().ProperName());
                complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe.fabricators = new List<Tag> { TagManager.Create("MilkPress") };
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 3f),
                    new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 17f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Milk.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe2 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MilkPress", array3, array4), array3, array4, 0, 0);
                complexRecipe2.time = 20f;
                complexRecipe2.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.MILKPRESS.NUT_MILK_RECIPE_DESCRIPTION, global::STRINGS.ITEMS.FOOD.SPICENUT.NAME, SimHashes.Milk.CreateTag().ProperName());
                complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe2.fabricators = new List<Tag> { TagManager.Create("MilkPress") };
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BeanPlantSeed", 2f),
                    new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 18f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Milk.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe3 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MilkPress", array5, array6), array5, array6, 0, 0);
                complexRecipe3.time = 20f;
                complexRecipe3.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.MILKPRESS.NUT_MILK_RECIPE_DESCRIPTION, global::STRINGS.ITEMS.FOOD.BEANPLANTSEED.NAME, SimHashes.Milk.CreateTag().ProperName());
                complexRecipe3.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe3.fabricators = new List<Tag> { TagManager.Create("MilkPress") };
                if (DlcManager.IsContentSubscribed("DLC3_ID"))
                {
                    ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.SlimeMold.CreateTag(), 50f)
                    };
                    ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                        new ComplexRecipe.RecipeElement(SimHashes.SlimeMold.CreateTag(), 250f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                        new ComplexRecipe.RecipeElement(SimHashes.Phosphorite.CreateTag(), 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                        new ComplexRecipe.RecipeElement(SimHashes.Fertilizer.CreateTag(), 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                        new ComplexRecipe.RecipeElement(SimHashes.Sulfur.CreateTag(), 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                        new ComplexRecipe.RecipeElement(SimHashes.BleachStone.CreateTag(), 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                        new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                    };
                    ComplexRecipe complexRecipe4 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MilkPress", array7, array8), array7, array8, 0, 0, DlcManager.DLC3);
                    complexRecipe4.time = 20f;
                    complexRecipe4.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.MILKPRESS.PHYTO_OIL_RECIPE_DESCRIPTION, ELEMENTS.SLIMEMOLD.NAME, SimHashes.PhytoOil.CreateTag().ProperName(), SimHashes.Dirt.CreateTag().ProperName());
                    complexRecipe4.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                    complexRecipe4.fabricators = new List<Tag> { TagManager.Create("MilkPress") };
                }
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(KilnConfig), "CreateBuildingDef")]//修改窑炉
        public class Patch_KilnConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(KilnConfig), "ConfigureBuildingTemplate")]//窑炉
        public class Patch_Kiln
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
                go.AddOrGet<DropAllWorkable>();
                go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
                ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
                complexFabricator.heatedTemperature = 303.15f;
                complexFabricator.duplicantOperated = false;
                complexFabricator.showProgressBar = true;
                complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
                go.AddOrGet<FabricatorIngredientStatusManager>();
                go.AddOrGet<CopyBuildingSettings>();
                BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
                Tag tag = SimHashes.Ceramic.CreateTag();
                Tag tag2 = SimHashes.Clay.CreateTag();
                Tag tag3 = SimHashes.Carbon.CreateTag();
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(tag2, 50f),
                    new ComplexRecipe.RecipeElement(tag3, 50f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(tag2, 50f),
                    new ComplexRecipe.RecipeElement(tag3, 50f),
                    new ComplexRecipe.RecipeElement(tag, 400f)
                };
                string text = ComplexRecipeManager.MakeObsoleteRecipeID("Kiln", tag);
                string text2 = ComplexRecipeManager.MakeRecipeID("Kiln", array, array2);
                ComplexRecipe complexRecipe = new ComplexRecipe(text2, array, array2);
                complexRecipe.time = 20f;
                complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.Clay).name, ElementLoader.FindElementByHash(SimHashes.Ceramic).name);
                complexRecipe.fabricators = new List<Tag> { TagManager.Create("Kiln") };
                complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                complexRecipe.sortOrder = 100;
                ComplexRecipeManager.Get().AddObsoleteIDMapping(text, text2);
                Tag tag4 = SimHashes.RefinedCarbon.CreateTag();
                ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(tag3, 50f)
                };
                ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(tag4, 100f)
                };
                string text3 = ComplexRecipeManager.MakeObsoleteRecipeID("Kiln", tag4);
                string text4 = ComplexRecipeManager.MakeRecipeID("Kiln", array3, array4);
                ComplexRecipe complexRecipe2 = new ComplexRecipe(text4, array3, array4);
                complexRecipe2.time = 20f;
                complexRecipe2.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.Carbon).name, ElementLoader.FindElementByHash(SimHashes.RefinedCarbon).name);
                complexRecipe2.fabricators = new List<Tag> { TagManager.Create("Kiln") };
                complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe2.sortOrder = 200;
                ComplexRecipeManager.Get().AddObsoleteIDMapping(text3, text4);
                Tag tag5 = SimHashes.RefinedCarbon.CreateTag();
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.WoodLog.CreateTag(), 50f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(tag5, 50f)
                };
                string text5 = ComplexRecipeManager.MakeObsoleteRecipeID("Kiln", tag5);
                string text6 = ComplexRecipeManager.MakeRecipeID("Kiln", array5, array6);
                ComplexRecipe complexRecipe3 = new ComplexRecipe(text6, array5, array6);
                complexRecipe3.time = 20f;
                complexRecipe3.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.WoodLog).name, ElementLoader.FindElementByHash(SimHashes.RefinedCarbon).name);
                complexRecipe3.fabricators = new List<Tag> { TagManager.Create("Kiln") };
                complexRecipe3.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe3.sortOrder = 300;
                ComplexRecipeManager.Get().AddObsoleteIDMapping(text5, text6);
                Prioritizable.AddRef(go);
                return false;// 返回false以阻止原方法执行
            }
        }

        [HarmonyPatch(typeof(RockCrusherConfig), "CreateBuildingDef")]//修改碎石机
        public class Patch_RockCrusherConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(RockCrusherConfig), "ConfigureBuildingTemplate")]//碎石机
        public class Patch_RockCrusher
        {
            public static bool Prefix(ref object __instance, GameObject go, Tag prefab_tag)
            {
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
                go.AddOrGet<DropAllWorkable>();
                go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
                ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
                complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
                complexFabricator.duplicantOperated = true;
                go.AddOrGet<FabricatorIngredientStatusManager>();
                go.AddOrGet<CopyBuildingSettings>();
                ComplexFabricatorWorkable complexFabricatorWorkable = go.AddOrGet<ComplexFabricatorWorkable>();
                BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
                complexFabricatorWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_rockrefinery_kanim") };
                complexFabricatorWorkable.workingPstComplete = new HashedString[] { "working_pst_complete" };
                Tag tag = SimHashes.Sand.CreateTag();
                foreach (Element element in ElementLoader.elements.FindAll((Element e) => e.HasTag(GameTags.Crushable)))
                {
                    ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(element.tag, 50f)
                    };
                    ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(tag, 50f),
                    };
                    string text = ComplexRecipeManager.MakeObsoleteRecipeID("RockCrusher", element.tag);
                    string text2 = ComplexRecipeManager.MakeRecipeID("RockCrusher", array, array2);
                    ComplexRecipe complexRecipe = new ComplexRecipe(text2, array, array2);
                    complexRecipe.time = 20f;
                    complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, element.name, tag.ProperName());
                    complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                    complexRecipe.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                    ComplexRecipeManager.Get().AddObsoleteIDMapping(text, text2);
                }
                foreach (Element element2 in ElementLoader.elements.FindAll((Element e) => e.IsSolid && e.HasTag(GameTags.Metal)))
                {
                    if (!element2.HasTag(GameTags.Noncrushable))
                    {
                        Element lowTempTransition = element2.highTempTransition.lowTempTransition;
                        if (lowTempTransition != element2)
                        {
                            ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
                            {
                                new ComplexRecipe.RecipeElement(element2.tag, 50f)
                            };
                            ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
                            {
                                new ComplexRecipe.RecipeElement(lowTempTransition.tag, 400f),
                                new ComplexRecipe.RecipeElement(element2.tag, 100f)
                            };
                            string text3 = ComplexRecipeManager.MakeObsoleteRecipeID("RockCrusher", lowTempTransition.tag);
                            string text4 = ComplexRecipeManager.MakeRecipeID("RockCrusher", array3, array4);
                            ComplexRecipe complexRecipe2 = new ComplexRecipe(text4, array3, array4);
                            complexRecipe2.time = 20f;
                            complexRecipe2.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.METAL_RECIPE_DESCRIPTION, lowTempTransition.name, element2.name);
                            complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                            complexRecipe2.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                            ComplexRecipeManager.Get().AddObsoleteIDMapping(text3, text4);
                        }
                    }
                }
                Element element3 = ElementLoader.FindElementByHash(SimHashes.Lime);
                ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("EggShell", 5f)
                };
                ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Lime).tag, 50f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                string text5 = ComplexRecipeManager.MakeObsoleteRecipeID("RockCrusher", element3.tag);
                string text6 = ComplexRecipeManager.MakeRecipeID("RockCrusher", array5, array6);
                ComplexRecipe complexRecipe3 = new ComplexRecipe(text6, array5, array6);
                complexRecipe3.time = 20f;
                complexRecipe3.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), MISC.TAGS.EGGSHELL);
                complexRecipe3.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe3.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                ComplexRecipeManager.Get().AddObsoleteIDMapping(text5, text6);
                Element element4 = ElementLoader.FindElementByHash(SimHashes.Lime);
                ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BabyCrabShell", 1f)
                };
                ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(element4.tag, 50f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe4 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array7, array8), array7, array8);
                complexRecipe4.time = 20f;
                complexRecipe4.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME);
                complexRecipe4.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe4.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                Element element5 = ElementLoader.FindElementByHash(SimHashes.Lime);
                ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CrabShell", 1f)
                };
                ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(element5.tag, 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe5 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array9, array10), array9, array10);
                complexRecipe5.time = 20f;
                complexRecipe5.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME);
                complexRecipe5.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe5.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("BabyCrabWoodShell", 1f)
                };
                ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("WoodLog", 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe6 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array11, array12), array11, array12);
                complexRecipe6.time = 20f;
                complexRecipe6.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, WoodLogConfig.TAG.ProperName(), global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.BABY_CRAB_SHELL.VARIANT_WOOD.NAME);
                complexRecipe6.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe6.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                ComplexRecipe.RecipeElement[] array13 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("CrabWoodShell", 5f)
                };
                ComplexRecipe.RecipeElement[] array14 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("WoodLog", 5000f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe7 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array13, array14), array13, array14);
                complexRecipe7.time = 20f;
                complexRecipe7.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, WoodLogConfig.TAG.ProperName(), global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.VARIANT_WOOD.NAME);
                complexRecipe7.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe7.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                ComplexRecipe.RecipeElement[] array15 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Fossil).tag, 50f)
                };
                ComplexRecipe.RecipeElement[] array16 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Lime).tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Fossil).tag, 75f),
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.SedimentaryRock).tag, 25f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe8 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array15, array16), array15, array16);
                complexRecipe8.time = 20f;
                complexRecipe8.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_FROM_LIMESTONE_RECIPE_DESCRIPTION, SimHashes.Fossil.CreateTag().ProperName(), SimHashes.SedimentaryRock.CreateTag().ProperName(), SimHashes.Lime.CreateTag().ProperName());
                complexRecipe8.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe8.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                ComplexRecipe.RecipeElement[] array17 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Katairite).tag, 50f)
                };
                ComplexRecipe.RecipeElement[] array18 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Katairite).tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Steel).tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Diamond).tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe9 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array17, array18), array17, array18, DlcManager.DLC3);
                complexRecipe9.time = 20f;
                complexRecipe9.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_GARBAGE.NAME, SimHashes.Katairite.CreateTag().ProperName());
                complexRecipe9.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
                complexRecipe9.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                ComplexRecipe.RecipeElement[] array19 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Salt.CreateTag(), 100f)
                };
                ComplexRecipe.RecipeElement[] array20 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(TableSaltConfig.ID.ToTag(), 90f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                    new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe10 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array19, array20), array19, array20);
                complexRecipe10.time = 20f;
                complexRecipe10.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, SimHashes.Salt.CreateTag().ProperName(), global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.TABLE_SALT.NAME);
                complexRecipe10.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                complexRecipe10.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                if (ElementLoader.FindElementByHash(SimHashes.Graphite) != null)
                {
                    ComplexRecipe.RecipeElement[] array21 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.Fullerene.CreateTag(), 50f)
                    };
                    ComplexRecipe.RecipeElement[] array22 = new ComplexRecipe.RecipeElement[]
                    {
                        new ComplexRecipe.RecipeElement(SimHashes.Graphite.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                        new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 20f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                    };
                    ComplexRecipe complexRecipe11 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array21, array22), array21, array22, DlcManager.AVAILABLE_EXPANSION1_ONLY);
                    complexRecipe11.time = 20f;
                    complexRecipe11.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, SimHashes.Fullerene.CreateTag().ProperName(), SimHashes.Graphite.CreateTag().ProperName());
                    complexRecipe11.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                    complexRecipe11.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                }
                ComplexRecipe.RecipeElement[] array23 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement("IceBellyPoop", 100f)
                };
                ComplexRecipe.RecipeElement[] array24 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Phosphorite.CreateTag(), 120f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                    new ComplexRecipe.RecipeElement(SimHashes.Clay.CreateTag(), 120f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe12 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array23, array24), array23, array24, DlcManager.AVAILABLE_DLC_2);
                complexRecipe12.time = 20f;
                complexRecipe12.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION_TWO_OUTPUT, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ICE_BELLY_POOP.NAME, SimHashes.Phosphorite.CreateTag().ProperName(), SimHashes.Clay.CreateTag().ProperName());
                complexRecipe12.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
                complexRecipe12.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                ComplexRecipe.RecipeElement[] array25 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Glass).tag, 10f)
                };
                ComplexRecipe.RecipeElement[] array26 = new ComplexRecipe.RecipeElement[]
                {
                   new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Fullerene).tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                   new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Isoresin).tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                   new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Niobium).tag, 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                ComplexRecipe complexRecipe13 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array25, array26), array25, array26, DlcManager.AVAILABLE_DLC_2);
                complexRecipe13.time = 20f;
                complexRecipe13.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.GOLD_BELLY_CROWN.NAME, SimHashes.GoldAmalgam.CreateTag().ProperName());
                complexRecipe13.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
                complexRecipe13.fabricators = new List<Tag> { TagManager.Create("RockCrusher") };
                Prioritizable.AddRef(go);
                return false;// 返回false以阻止原方法执行
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using static STRINGS.BUILDINGS.PREFABS;

namespace OmnipotenceMod
{
    public class OtherBudling_o
    {
        [HarmonyPatch(typeof(ElectrolyzerConfig), "CreateBuildingDef")]//修改电解器发热
        public class Patch_ElectrolyzerConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 0.25f;
            }
        }

        [HarmonyPatch(typeof(ElectrolyzerConfig), "ConfigureBuildingTemplate")]//修改电解器产量
        public class Patch_Electrolyzer
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                CellOffset cellOffset = new CellOffset(0, 1);
                go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(new Tag("Water"), 0.5f, true)
                };
                go.AddOrGet<ElementConverter>().outputElements = new ElementConverter.OutputElement[]
                {
                    new ElementConverter.OutputElement(1.2f, SimHashes.Oxygen, 303.15f, false, false, (float)cellOffset.x, (float)cellOffset.y, 1f, byte.MaxValue, 0, true),
                    new ElementConverter.OutputElement(0.4f, SimHashes.Hydrogen, 303.15f, false, false, (float)cellOffset.x, (float)cellOffset.y, 1f, byte.MaxValue, 0, true)
                };
            }
        }

        [HarmonyPatch(typeof(AirFilterConfig), "CreateBuildingDef")]//修改空气过滤器
        public class Patch_AirFilterConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.RequiresPowerInput = false;
                __result.EnergyConsumptionWhenActive = 0f;
            }
        }

        [HarmonyPatch(typeof(AirFilterConfig), "ConfigureBuildingTemplate")]//修改空气过滤器产出
        public class Patch_AirFilter
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<ElementConsumer>().consumptionRadius = 4;
                go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(new Tag("Filter"), 0.1f, true),
                    new ElementConverter.ConsumedElement(new Tag("ContaminatedOxygen"), 0.1f, true)
                };
                go.AddOrGet<ElementConverter>().outputElements = new ElementConverter.OutputElement[]
                {
                    new ElementConverter.OutputElement(0.1f, SimHashes.Clay, 0f, false, true, 0f, 0.5f, 0.25f, byte.MaxValue, 0, true),
                    new ElementConverter.OutputElement(0.1f, SimHashes.Oxygen, 0f, false, false, 0f, 0f, 0.75f, byte.MaxValue, 0, true)
                };
            }
        }

        [HarmonyPatch(typeof(CeilingLightConfig), "CreateBuildingDef")]//修改吸顶灯
        public class Patch_CeilingLightConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.SelfHeatKilowattsWhenActive = 0.125f;
                __result.ExhaustKilowattsWhenActive = 0.125f;

            }
        }

        [HarmonyPatch(typeof(CeilingLightConfig), "DoPostConfigureComplete")]//修改吸顶灯
        public class Patch_CeilingLight
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<Light2D>().Lux = 5000;
            }
        }

        [HarmonyPatch(typeof(SunLampConfig), "CreateBuildingDef")]//修改日光灯
        public class Patch_SunLampConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 480f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
                __result.ExhaustKilowattsWhenActive = 0.5f;

            }
        }

        [HarmonyPatch(typeof(SunLampConfig), "DoPostConfigureComplete")]//修改日光灯
        public class Patch_SunLamp
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<Light2D>().Lux = 20000;
            }
        }

        [HarmonyPatch(typeof(FloorLampConfig), "DoPostConfigureComplete")]//修改电灯
        public class Patch_FloorLamp
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<Light2D>().Lux = 10000;
            }
        }

        [HarmonyPatch(typeof(SolidTransferArmConfig), "CreateBuildingDef")]//修改清扫器
        public class Patch_SolidTransferArmConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.SelfHeatKilowattsWhenActive = 1f;
            }
        }

        [HarmonyPatch(typeof(SolidTransferArmConfig), "DoPostConfigureComplete")]//修改清扫器
        public class Patch_SolidTransferArm
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<SolidTransferArm>().pickupRange = 8;
            }
        }

        [HarmonyPatch(typeof(SolidTransferArmConfig), "AddVisualizer")]//修改清扫器
        public class Patch_AddVisualizer
        {
            public static void Postfix(GameObject prefab, bool movable)
            {
                prefab.AddOrGet<RangeVisualizer>().RangeMin.x = -8;
                prefab.AddOrGet<RangeVisualizer>().RangeMin.y = -8;
                prefab.AddOrGet<RangeVisualizer>().RangeMax.x = 8;
                prefab.AddOrGet<RangeVisualizer>().RangeMax.y = 8;
            }
        }

        [HarmonyPatch(typeof(CO2ScrubberConfig), "ConfigureBuildingTemplate")]//修改碳素脱离器
        public class Patch_CO2ScrubberConfig
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
                {
                    new ElementConverter.ConsumedElement(GameTagExtensions.Create(SimHashes.Water), 1f, true),
                    new ElementConverter.ConsumedElement(GameTagExtensions.Create(SimHashes.CarbonDioxide), 0.3f, true)
                };
                go.AddOrGet<ElementConverter>().outputElements = new ElementConverter.OutputElement[]
                {
                    new ElementConverter.OutputElement(2f, SimHashes.DirtyWater, 0f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
                };
            }
        }

        [HarmonyPatch(typeof(LiquidConditionerConfig), "CreateBuildingDef")]//修改液体降温器
        public class Patch_LiquidConditionerConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 800f;
                __result.OverheatTemperature = 473.15f;
            }
        }

        [HarmonyPatch(typeof(LiquidPumpConfig), "CreateBuildingDef")]//修改液泵
        public class Patch_LiquidPumpConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 120f;
                __result.OverheatTemperature = 473.15f;
            }
        }

        [HarmonyPatch(typeof(GasPumpConfig), "CreateBuildingDef")]//修改气泵
        public class Patch_GasPumpConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 120f;
                __result.OverheatTemperature = 473.15f;
            }
        }

        [HarmonyPatch(typeof(OilWellCapConfig), "CreateBuildingDef")]//修改油井
        public class Patch_OilWellCapConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 120f;
                __result.SelfHeatKilowattsWhenActive = 1f;
            }
        }

        [HarmonyPatch(typeof(OilWellCapConfig), "ConfigureBuildingTemplate")]//修改油井
        public class Patch_OilWellCapBuildingTemplate
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<OilWellCap>().addGasRate = 0f;
                go.AddOrGet<OilWellCap>().maxGasPressure = 80.00001f;
                go.AddOrGet<OilWellCap>().releaseGasRate = 0f;
            }
        }

        [HarmonyPatch(typeof(LogicDuplicantSensorConfig), "DoPostConfigureComplete")]//修改复制人传感器
        public class Patch_LogicDuplicantSensor
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<LogicDuplicantSensor>().pickupRange = 6;
            }
        }

        [HarmonyPatch(typeof(LogicDuplicantSensorConfig), "AddVisualizer")]//修改复制人传感器
        public class Patch_LogicDuplicantSensorConfig
        {
            public static void Postfix(GameObject prefab, bool movable)
            {
                prefab.AddOrGet<RangeVisualizer>().RangeMin.x = -3;
                prefab.AddOrGet<RangeVisualizer>().RangeMin.y = -0;
                prefab.AddOrGet<RangeVisualizer>().RangeMax.x = 3;
                prefab.AddOrGet<RangeVisualizer>().RangeMax.y = 6;
            }
        }
    }
}

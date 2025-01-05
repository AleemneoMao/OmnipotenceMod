using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TUNING;
using UnityEngine;

namespace OmnipotenceMod
{
    public class PowerBudling_o
    {
        [HarmonyPatch(typeof(GeneratorConfig), "CreateBuildingDef")]//修改煤炭发电机
        public class Patch_GeneratorConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.GeneratorWattageRating = 30000f;// 修改发电
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(HydrogenGeneratorConfig), "CreateBuildingDef")]//修改氢气发电机
        public class Patch_HydrogenGeneratorConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.GeneratorWattageRating = 80000f;// 修改发电
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(ManualGeneratorConfig), "CreateBuildingDef")]//修改人力发电机
        public class Patch_ManualGeneratorConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.GeneratorWattageRating = 20000f;// 修改发电
            }
        }

        [HarmonyPatch(typeof(PetroleumGeneratorConfig), "CreateBuildingDef")]//修改石油发电机
        public class Patch_PetroleumGeneratorConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.GeneratorWattageRating = 100000f;// 修改发电
                __result.ExhaustKilowattsWhenActive = 1f;
                __result.SelfHeatKilowattsWhenActive = 1f;
            }
        }


        [HarmonyPatch(typeof(MethaneGeneratorConfig), "CreateBuildingDef")]//修改石油发电机
        public class Patch_MethaneGeneratorConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.GeneratorWattageRating = 40000f;// 修改发电
                __result.ExhaustKilowattsWhenActive = 0.5f;
                __result.SelfHeatKilowattsWhenActive = 0.5f;
            }
        }

        [HarmonyPatch(typeof(BatteryConfig), "DoPostConfigureComplete")]// 修改智能电池
        public class Patch_BatteryConfig
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<Battery>().capacity = 2000000f;// 修改储电量
                go.AddOrGet<Battery>().joulesLostPerSecond = 0f;
            }
        }

        [HarmonyPatch(typeof(BatterySmartConfig), "DoPostConfigureComplete")]// 修改智能电池
        public class Patch_BatterySmartConfig
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<BatterySmart>().capacity = 2000000f;// 修改储电量
                go.AddOrGet<BatterySmart>().joulesLostPerSecond = 0f;
            }
        }

        [HarmonyPatch(typeof(BatteryMediumConfig), "DoPostConfigureComplete")]// 修改巨型电池
        public class Patch_BatteryMediumConfig
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<Battery>().capacity = 10000000f;// 修改储电量
                go.AddOrGet<Battery>().joulesLostPerSecond = 0.25f;
            }
        }

        [HarmonyPatch(typeof(PowerTransformerSmallConfig), "CreateBuildingDef")]//修改变压器
        public class Patch_PowerTransformerSmallConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0f;
                __result.SelfHeatKilowattsWhenActive = 0f;
                __result.GeneratorWattageRating = 100000f;
                __result.GeneratorBaseCapacity = 100000f;
            }
        }

        [HarmonyPatch(typeof(PowerTransformerConfig), "CreateBuildingDef")]//修改大型变压器
        public class Patch_PowerTransformerConfig
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = 0f;
                __result.SelfHeatKilowattsWhenActive = 0f;
                __result.GeneratorWattageRating = 1000000f;
                __result.GeneratorBaseCapacity = 1000000f;
            }
        }
    }
}

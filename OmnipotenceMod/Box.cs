using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using KMod;
using TUNING;
using UnityEngine;

namespace OmnipotenceMod
{
    public class Box_o
    {
        [HarmonyPatch(typeof(StorageLockerConfig), "ConfigureBuildingTemplate")]// 存储箱容量
        public class Patch_StorageLockerConfig
        {
            public static void Postfix(GameObject go)
            {
                Storage storage = go.AddOrGet<Storage>();
                storage.capacityKg = 100000f;
            }
        }

        [HarmonyPatch(typeof(LiquidReservoirConfig), "ConfigureBuildingTemplate")]// 储液库容量
        public class Patch_LiquidReservoirConfig
        {
            public static void Postfix(GameObject go)
            {
                Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
                storage.capacityKg = 50000f;
            }
        }

        [HarmonyPatch(typeof(GasReservoirConfig), "ConfigureBuildingTemplate")]// 储气库容量
        public class Patch_GasReservoirConfig
        {
            public static void Postfix(GameObject go)
            {
                Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
                storage.capacityKg = 10000f;
            }
        }

        [HarmonyPatch(typeof(RationBoxConfig), "ConfigureBuildingTemplate")]// 口粮箱容量
        public class Patch_RationBoxConfig
        {
            public static void Postfix(GameObject go)
            {
                Storage storage = go.AddOrGet<Storage>();
                storage.capacityKg = 3000f;
            }
        }

        [HarmonyPatch(typeof(RefrigeratorConfig), "DoPostConfigureComplete")]// 冰箱容量
        public class Patch_RefrigeratorConfig
        {
            public static void Postfix(GameObject go)
            {
                Storage storage = go.AddOrGet<Storage>();
                storage.capacityKg = 10000f;
                go.AddOrGetDef<RefrigeratorController.Def>().coolingHeatKW = 0.1f;
                EntityTemplateExtensions.AddOrGetDef<RefrigeratorController.Def>(go).simulatedInternalTemperature = 243.15f;// 设置冰箱制冷温度
            }
        }
    }
}

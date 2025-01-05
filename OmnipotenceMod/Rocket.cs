using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUNING;
using HarmonyLib;
using UnityEngine;

namespace OmnipotenceMod
{
    public class Rocket_o
    {
        [HarmonyPatch(typeof(HydrogenEngineClusterConfig), "DoPostConfigureComplete")]// 液氢引擎
        public class Patch_HydrogenEngine
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<RocketEngineCluster>().efficiency = 100f;
                BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR_PLUS, 100f, 0.05f);
            }
        }
        [HarmonyPatch(typeof(HEPEngineConfig), "DoPostConfigureComplete")]// 辐射粒子引擎
        public class Patch_HEPEngine
        {
            public static void Postfix(GameObject go)
            {
                BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE_PLUS, 75f, 0.15f);
            }
        }
        [HarmonyPatch(typeof(KeroseneEngineClusterConfig), "DoPostConfigureComplete")]// 石油引擎
        [HarmonyPatch()]
        public class Patch_KeroseneEngineCluster
        {
            public static void Postfix(GameObject go)
            {
                BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR, 75f, 0.1f);
            }
        }
    }
}

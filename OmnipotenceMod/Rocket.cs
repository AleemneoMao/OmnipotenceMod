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
        [HarmonyPatch(typeof(HydrogenEngineClusterConfig))]// 液氢引擎
        [HarmonyPatch("DoPostConfigureComplete")]
        public class Patch_HydrogenEngine
        {
            public static void Postfix(GameObject go)
            {
                BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR_PLUS, (float)110, 0.01f);
            }
        }
        [HarmonyPatch(typeof(HEPEngineConfig))]// 辐射粒子引擎
        [HarmonyPatch("DoPostConfigureComplete")]
        public class Patch_HEPEngine
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<RocketEngineCluster>().maxHeight = 25;
                BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE_PLUS, (float)70, 0.025f);
            }
        }
        [HarmonyPatch(typeof(KeroseneEngineClusterConfig))]// 石油引擎
        [HarmonyPatch("DoPostConfigureComplete")]
        public class Patch_KeroseneEngineCluster
        {
            public static void Postfix(GameObject go)
            {
                BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR, (float)100, 0.015f);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace OmnipotenceMod
{
    public class Evaporate_o
    {
        [HarmonyPatch(typeof(DirtyWaterConfig))]// 污染水
        [HarmonyPatch("CreatePrefab")]
        public class Patch_DirtyWater
        {
            public static void Postfix(GameObject __result, DirtyWaterConfig __instance)
            {
                Sublimates sublimates = __result.GetComponent<Sublimates>();
                sublimates.info = new Sublimates.Info(
                    0f,// 修改污染水排放
                    0f,
                    0f,
                    0.5f,
                    SimHashes.Oxygen,
                    byte.MaxValue,
                    0
                );
            }
        }

        [HarmonyPatch(typeof(ToxicSandConfig))]// 污染土
        [HarmonyPatch("CreatePrefab")]
        public class Patch_ToxicSand
        {
            public static void Postfix(GameObject __result, ToxicSandConfig __instance)
            {
                if (__result != null)
                {
                    Sublimates sublimates = __result.GetComponent<Sublimates>();
                    if (sublimates != null)
                    {
                        sublimates.info = new Sublimates.Info(
                            0f,// 修改污染土排放
                            0f,
                            0f,
                            0.5f,
                            SimHashes.Oxygen,
                            byte.MaxValue,
                            0
                        );
                    }
                }
            }
        }

        [HarmonyPatch(typeof(SlimeMoldConfig))]// 菌泥
        [HarmonyPatch("CreatePrefab")]
        public class Patch_SlimeMold
        {
            public static void Postfix(GameObject __result, SlimeMoldConfig __instance)
            {
                if (__result != null)
                {
                    Sublimates sublimates = __result.GetComponent<Sublimates>();
                    if (sublimates != null)
                    {
                        sublimates.info = new Sublimates.Info(
                            0f,// 修改菌泥排放
                            0f,
                            0f,
                            0.5f,
                            SimHashes.Oxygen,
                            byte.MaxValue,
                            0
                        );
                    }
                }
            }
        }

        [HarmonyPatch(typeof(BleachStoneConfig))]// 漂白石
        [HarmonyPatch("CreatePrefab")]
        public class Patch_BleachStone
        {
            public static void Postfix(GameObject __result, BleachStoneConfig __instance)
            {
                if (__result != null)
                {
                    Sublimates sublimates = __result.GetComponent<Sublimates>();
                    if (sublimates != null)
                    {
                        sublimates.info = new Sublimates.Info(
                            0f,// 修改漂白石排放
                            0f,
                            0f,
                            0.5f,
                            __instance.SublimeElementID,
                            byte.MaxValue,
                            0
                        );
                    }
                }
            }
        }
    }
}

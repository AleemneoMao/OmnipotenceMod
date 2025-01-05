using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using static STRINGS.BUILDINGS.PREFABS;
using static STRINGS.DUPLICANTS.CHORES;

namespace OmnipotenceMod
{
    public class Transport_o
    {
        [HarmonyPatch(typeof(SolidConduitDispenser), "ConduitUpdate")]// 修改运输轨道载货量
        public class Patch_GenerateAptitudes
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
            {
                List<CodeInstruction> code = instr.ToList();
                code[37].operand = 100f;
                code[40].operand = 100f;
                return code.AsEnumerable();
            }
        }

        [HarmonyPatch(typeof(LadderFastConfig), "ConfigureBuildingTemplate")]//修改塑料梯移速
        public class Patch_LadderFastConfig
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<Ladder>().upwardsMovementSpeedMultiplier = 1.5f;
                go.AddOrGet<Ladder>().downwardsMovementSpeedMultiplier = 1.5f;
            }
        }

        [HarmonyPatch(typeof(LadderConfig), "ConfigureBuildingTemplate")]//修改普通梯移速
        public class Patch_LadderConfig
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<Ladder>().upwardsMovementSpeedMultiplier = 1.2f;
                go.AddOrGet<Ladder>().downwardsMovementSpeedMultiplier = 1.2f;
            }
        }

        [HarmonyPatch(typeof(FirePoleConfig), "ConfigureBuildingTemplate")]//修改普通梯移速
        public class Patch_FirePoleConfig
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<Ladder>().upwardsMovementSpeedMultiplier = 1f;
                go.AddOrGet<Ladder>().downwardsMovementSpeedMultiplier = 4f;
            }
        }

        [HarmonyPatch(typeof(TravelTubeEntranceConfig), "ConfigureBuildingTemplate")]//修改普通梯移速
        public class Patch_TravelTubeEntranceConfig
        {
            public static void Postfix(GameObject go, Tag prefab_tag)
            {
                go.AddOrGet<TravelTubeEntrance>().joulesPerLaunch = 5000f;
                go.AddOrGet<TravelTubeEntrance>().jouleCapacity = 80000f;
            }
        }
    }
}

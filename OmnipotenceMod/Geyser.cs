using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Klei;
using TUNING;
using static GeyserConfigurator;
using static Klei.SimUtil;

namespace OmnipotenceMod
{
    [HarmonyPatch(typeof(GeyserGenericConfig), "GenerateConfigs")]
    public class Patch_GenerateConfigs
    {
        private static void Postfix(ref List<GeyserGenericConfig.GeyserPrefabParams> __result)
        {
            __result[0].geyserType.temperature = 303.15f;
            __result[1].geyserType.temperature = 373.15f;
            __result[2].geyserType.temperature = 303.15f;
            __result[4].geyserType.diseaseInfo.count = 0;
            __result[6].geyserType.temperature = 303.15f;
            __result[11].geyserType.temperature = 303.15f;
            __result[13].geyserType.diseaseInfo.count = 0;
            __result[14].geyserType.temperature = 303.15f;
            __result[15].geyserType.temperature = 313.15f;
            __result[23].geyserType.temperature = 353.15f;
            __result[23].geyserType.minRatePerCycle = 200f;
            __result[23].geyserType.maxRatePerCycle = 400f;
        }
    }
    /*
    [HarmonyPatch(typeof(GeyserType), "AddDisease")]
    public class Patch_AddDisease
    {
        private static void Postfix(ref GeyserType __result)
        {
            SimUtil.DiseaseInfo diseasenull;
            diseasenull.idx = 0;
            diseasenull.count = 0;
            __result.diseaseInfo = diseasenull;
        }
    }
    */
}

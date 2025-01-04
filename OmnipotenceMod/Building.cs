using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TUNING;

namespace OmnipotenceMod
{
    public class Patche_buildings//修改煤炭发电机
    {
        [HarmonyPatch(typeof(GeneratorConfig), "CreateBuildingDef")]
        public class Patch_Generator
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.GeneratorWattageRating = 30000f;// 修改发电
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Klei.AI;

namespace OmnipotenceMod
{
    [HarmonyPatch(typeof(SlimeGerms), MethodType.Constructor, typeof(bool))]
    public class Patch_SlimeGerms
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            List<CodeInstruction> code = instr.ToList();
            code[3].operand = 103.15f;
            code[4].operand = 103.15f;
            code[5].operand = 103.15f;
            code[6].operand = 103.15f;
            code[9].operand = 600f;
            code[10].operand = 600f;
            return code.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(FoodGerms), MethodType.Constructor, typeof(bool))]
    public class Patch_FoodGerms
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            List<CodeInstruction> code = instr.ToList();
            code[3].operand = 103.15f;
            code[4].operand = 103.15f;
            code[5].operand = 103.15f;
            code[6].operand = 103.15f;
            code[9].operand = 600f;
            code[10].operand = 600f;
            return code.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(ZombieSpores), MethodType.Constructor, typeof(bool))]
    public class Patch_ZombieSpores
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            List<CodeInstruction> code = instr.ToList();
            code[3].operand = 103.15f;
            code[4].operand = 103.15f;
            code[5].operand = 103.15f;
            code[6].operand = 103.15f;
            code[9].operand = 600f;
            code[10].operand = 600f;
            return code.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(RadiationPoisoning), "PopulateElemGrowthInfo")]
    public class Patch_RadiationPoisoning
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            List<CodeInstruction> code = instr.ToList();
            code[16].operand = 300f;
            return code.AsEnumerable();
        }
    }
}

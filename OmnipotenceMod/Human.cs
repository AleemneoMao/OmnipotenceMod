using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Klei.AI;
using Database;
using KMod;
using TUNING;
using System.Reflection.Emit;

namespace OmnipotenceMod
{
    public class Mod : UserMod2// 官方接口，游戏运行时加载harmony补丁，修改复制人默认参数
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            Traverse.Create(typeof(DUPLICANTSTATS)).Field<int>("APTITUDE_BONUS").Value = 15;// 学习兴趣技能士气+15
            Traverse.Create(typeof(DUPLICANTSTATS)).Field<int>("MAX_TRAITS").Value = 2;// 最大特质为2
            Traverse.Create(typeof(DUPLICANTSTATS)).Field<int[]>("APTITUDE_ATTRIBUTE_BONUSES").Value = new int[]
            {
                40,
                40,
                40,
                40
            };//技能点数
            Traverse.Create(typeof(DUPLICANTSTATS.ATTRIBUTE_LEVELING)).Field<int>("MAX_GAINED_ATTRIBUTE_LEVEL").Value = 80;// 最大等级80级
            Traverse.Create(typeof(DUPLICANTSTATS.ATTRIBUTE_LEVELING)).Field<int>("TARGET_MAX_LEVEL_CYCLE").Value = 200;// 最大等级周期
            Traverse.Create(typeof(DUPLICANTSTATS.ATTRIBUTE_LEVELING)).Field<float>("EXPERIENCE_LEVEL_POWER").Value = 1f;// 经验升级幂数
            Traverse.Create(typeof(DUPLICANTSTATS.ATTRIBUTE_LEVELING)).Field<float>("FULL_EXPERIENCE").Value = 10f;// 经验获取倍率10倍
            Traverse.Create(typeof(ROLES)).Field<float>("BASIC_ROLE_MASTERY_EXPERIENCE_REQUIRED").Value = 200f;// 获取技能点的经验，越小越快
            Traverse.Create(typeof(SKILLS)).Field<float>("PASSIVE_EXPERIENCE_PORTION").Value = 1f;// 被动经验获倍率，越大越快
            Traverse.Create(typeof(SKILLS)).Field<float>("FULL_EXPERIENCE").Value = 10f;// 被动经验获倍率，越大越快
            Traverse.Create(typeof(EQUIPMENT.SUITS)).Field<float>("ATMOSUIT_DECAY").Value = -0.01f;// 气压服磨损
            Traverse.Create(typeof(EQUIPMENT.SUITS)).Field<float>("ATMOSUIT_THERMAL_CONDUCTIVITY_BARRIER").Value = 0.4f;// 气压服隔热厚度
            Traverse.Create(typeof(EQUIPMENT.SUITS)).Field<int>("ATMOSUIT_INSULATION").Value = 100;// 气压服隔热
            Traverse.Create(typeof(EQUIPMENT.SUITS)).Field<int>("ATMOSUIT_ATHLETICS").Value = -2;// 气压服运动加成
            Traverse.Create(typeof(EQUIPMENT.SUITS)).Field<int>("ATMOSUIT_DIGGING").Value = 20;// 气压服挖掘加成
            Traverse.Create(typeof(EQUIPMENT.SUITS)).Field<int>("ATMOSUIT_CONSTRUCTION").Value = 20;// 气压服建造加成
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(MinionStartingStats), "GenerateAptitudes")]//修改复制人兴趣为固定4个
    public class Patch_GenerateAptitudes
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            List<CodeInstruction> code = instr.ToList();
            code[7].opcode = OpCodes.Ldc_I4_4;
            code[8].opcode = OpCodes.Ldc_I4_5;
            return code.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(WorldDamage), "OnDigComplete")]//修改挖掘收益为3倍质量
    public class Patch_OnDigComplete
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            List<CodeInstruction> code = instr.ToList();
            code[18].operand = 3.0f;
            return code.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(Database.Attributes), MethodType.Constructor, typeof(ResourceSet))]//修改复制人基础属性
    public class Patch_Attributes
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instr)
        {
            List<CodeInstruction> code = instr.ToList();
            code[10].operand = 40f;
            code[30].operand = 40f;
            code[50].operand = 40f;
            code[70].operand = 40f;
            code[90].operand = 40f;
            code[110].operand = 40f;
            code[130].operand = 40f;
            code[150].operand = 40f;
            code[170].operand = 40f;
            code[190].operand = 40f;
            code[210].operand = 40f;
            code[272].operand = 40f;
            code[293].operand = 40f;
            code[311].operand = 40f;
            code[520].operand = 2f;
            code[538].operand = 40f;
            code[690].operand = 1f;
            code[724].operand = 2f;
            code[744].operand = 2f;
            code[820].operand = 40f;
            return code.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(Game), "OnPrefabInit")]//复制人无负面特质
    internal class Patch_GetTraitVal
    {
        private static void Postfix()
        {
            DUPLICANTSTATS.BADTRAITS.Clear();
            foreach (DUPLICANTSTATS.TraitVal traitVal in DUPLICANTSTATS.GOODTRAITS)
            {
                DUPLICANTSTATS.BADTRAITS.Add(traitVal);
            }
        }
    }

}

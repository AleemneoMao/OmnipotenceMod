using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace OmnipotenceMod
{
    [HarmonyPatch(typeof(Wire), "GetMaxWattageAsFloat")]// 电线负载
    public static class Patch_Wire
    {
        public static bool Prefix(ref float __result, Wire.WattageRating rating)
        {
            switch (rating)
            {
                case Wire.WattageRating.Max500:
                    __result = 25000f;
                    break;
                case Wire.WattageRating.Max1000:
                    __result = 50000f;
                    break;
                case Wire.WattageRating.Max2000:
                    __result = 200000f;
                    break;
                case Wire.WattageRating.Max20000:
                    __result = 1000000f;
                    break;
                case Wire.WattageRating.Max50000:
                    __result = 25000000f;
                    break;
                default:
                    __result = 0f;
                    break;
            }
            return false; // 返回false以阻止原方法执行
        }
    }
}

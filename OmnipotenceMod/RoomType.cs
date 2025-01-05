using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using HarmonyLib;

namespace OmnipotenceMod
{
    public class RoomType_o
    {
        [HarmonyPatch(typeof(RoomType), MethodType.Constructor, new Type[]
        {
            typeof(string),
            typeof(string),
            typeof(string),
            typeof(string),
            typeof(string),
            typeof(RoomTypeCategory),
            typeof(RoomConstraints.Constraint),
            typeof(RoomConstraints.Constraint[]),
            typeof(RoomDetails.Detail[]),
            typeof(int),
            typeof(RoomType[]),
            typeof(bool),
            typeof(bool),
            typeof(string[]),
            typeof(int)
        })]

        public static class Patch_RoomType
        {
            public static void Postfix(RoomType __instance)
            {
                bool flag_PrivateBedroom1 = __instance.Id == "Private Bedroom";//私人卧室最小房间大小
                if (flag_PrivateBedroom1)
                {
                    for (int num_PrivateBedroom = 0; num_PrivateBedroom < __instance.additional_constraints.Length; num_PrivateBedroom++)
                    {
                        bool flag_PrivateBedroom2 = __instance.additional_constraints[num_PrivateBedroom] == RoomConstraints.MINIMUM_SIZE_24;
                        if (flag_PrivateBedroom2)
                        {
                            __instance.additional_constraints[num_PrivateBedroom] = RoomConstraints.MINIMUM_SIZE_12;
                        }
                    }
                }
            }
        }
    }
}

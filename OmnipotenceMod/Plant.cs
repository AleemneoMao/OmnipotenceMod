using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace OmnipotenceMod
{
    public class Plant_o
    {
        [HarmonyPatch(typeof(ColdWheatConfig), "CreatePrefab")]// 冰霜小麦
        public static class Patch_BeanPlant
        {
            public static void Postfix(GameObject __result)
            {
                EntityTemplates.ExtendEntityToBasicPlant(__result, 118.149994f, 218.15f, 278.15f, 358.15f, new SimHashes[]
                {
                    SimHashes.Oxygen,
                    SimHashes.ContaminatedOxygen,
                    SimHashes.CarbonDioxide
                }, true, 0f, 0.15f, "ColdWheatSeed", true, true, true, true, 5f, 0f, 12200f, "ColdWheatOriginal", global::STRINGS.CREATURES.SPECIES.COLDWHEAT.NAME);
                EntityTemplates.ExtendPlantToFertilizable(__result, new PlantElementAbsorber.ConsumeInfo[]
                {
                    new PlantElementAbsorber.ConsumeInfo
                    {
                        tag = GameTags.Dirt,
                        massConsumptionRate = 0.008333334f
                    }
                });
                EntityTemplates.ExtendPlantToIrrigated(__result, new PlantElementAbsorber.ConsumeInfo[]
                {
                    new PlantElementAbsorber.ConsumeInfo
                    {
                        tag = GameTags.Water,
                        massConsumptionRate = 0.033333335f
                    }
                });
            }
        }

        [HarmonyPatch(typeof(PrickleFlowerConfig), "CreatePrefab")]// 毛刺花
        public static class Patch_PrickleFlower
        {
            public static void Postfix(GameObject __result)
            {
                EntityTemplates.ExtendEntityToBasicPlant(__result, 218.15f, 278.15f, 303.15f, 398.15f, new SimHashes[]
                {
                    SimHashes.Oxygen,
                    SimHashes.ContaminatedOxygen,
                    SimHashes.CarbonDioxide
                }, true, 0f, 0.15f, PrickleFruitConfig.ID, true, true, true, true, 5f, 0f, 4600f, "PrickleFlowerOriginal", global::STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.NAME);
                EntityTemplates.ExtendPlantToIrrigated(__result, new PlantElementAbsorber.ConsumeInfo[]
                {
                    new PlantElementAbsorber.ConsumeInfo
                    {
                        tag = GameTags.Water,
                        massConsumptionRate = 0.033333335f
                    }
                });
            }
        }

        [HarmonyPatch(typeof(SeaLettuceConfig), "CreatePrefab")]// 水草
        public static class Patch_SeaLettuce
        {
            public static void Postfix(GameObject __result)
            {
                EntityTemplates.ExtendEntityToBasicPlant(__result, 248.15f, 295.15f, 338.15f, 398.15f, new SimHashes[]
                {
                    SimHashes.Water,
                    SimHashes.SaltWater,
                    SimHashes.Brine
                }, false, 0f, 0.15f, "Lettuce", true, true, true, true, 5f, 0f, 7400f, SeaLettuceConfig.ID + "Original", global::STRINGS.CREATURES.SPECIES.SEALETTUCE.NAME);
                EntityTemplates.ExtendPlantToIrrigated(__result, new PlantElementAbsorber.ConsumeInfo[]
                {
                    new PlantElementAbsorber.ConsumeInfo
                    {
                        tag = SimHashes.SaltWater.CreateTag(),
                        massConsumptionRate = 0.008333334f
                    }
                });
                EntityTemplates.ExtendPlantToFertilizable(__result, new PlantElementAbsorber.ConsumeInfo[]
                {
                    new PlantElementAbsorber.ConsumeInfo
                    {
                        tag = SimHashes.BleachStone.CreateTag(),
                        massConsumptionRate = 0.00083333335f
                    }
                });
            }
        }
    }
}

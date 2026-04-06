using HarmonyLib;
using RimWorld.Planet;
using System;
using System.Reflection;
using Verse;

namespace Closer_VE_Quests.Patches
{
    [HarmonyPatch]
    public static class QuestNode_Root_AncientSilo_TileValidator_Patch
    {
        public static bool Prepare()
        {
            return SupportedModsCache.DeadlifeLoaded
                && AccessTools.TypeByName("VanillaQuestsExpandedDeadlife.QuestNode_Root_AncientSilo") != null;
        }

        public static MethodBase TargetMethod()
        {
            Type type = AccessTools.TypeByName("VanillaQuestsExpandedDeadlife.QuestNode_Root_AncientSilo");
            return AccessTools.PropertyGetter(type, "TileValidator");
        }

        public  static bool Prefix(ref Predicate<Map, PlanetTile> __result)
        {
            __result = (Map map, PlanetTile tile) =>
            {
                if (map == null)
                {
                    return true;
                }

                float distance = Find.WorldGrid.ApproxDistanceInTiles(tile, map.Tile);
                return distance >= ModSettings.ancientSiloMinDistance && distance <= ModSettings.ancientSiloMaxDistance;
            };

            return false;
        }
    }
}

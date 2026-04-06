using HarmonyLib;
using RimWorld.Planet;
using System;
using System.Reflection;
using Verse;

namespace Closer_VE_Quests.Patches
{
    [HarmonyPatch]
    public static class QuestNode_Root_AncientICBMLaunchSite_TileValidator_Patch
    {
        public static bool Prepare()
        {
            return SupportedModsCache.DeadlifeLoaded
                && AccessTools.TypeByName("VanillaQuestsExpandedDeadlife.QuestNode_Root_AncientICBMLaunchSite") != null;
        }

        public static MethodBase TargetMethod()
        {
            Type type = AccessTools.TypeByName("VanillaQuestsExpandedDeadlife.QuestNode_Root_AncientICBMLaunchSite");
            return AccessTools.PropertyGetter(type, "TileValidator");
        }

        public static bool Prefix(ref Predicate<Map, PlanetTile> __result)
        {
            __result = (Map map, PlanetTile tile) =>
            {
                PlanetTile originTile = map.Tile;
                float distance = Find.WorldGrid.ApproxDistanceInTiles(tile, originTile);

                return distance >= ModSettings.ancientIcbmLaunchSiteMinDistance
                    && distance <= ModSettings.ancientIcbmLaunchSiteMaxDistance
                    && (int)Find.WorldGrid[tile].hilliness < 3;
            };

            return false;
        }
    }
}

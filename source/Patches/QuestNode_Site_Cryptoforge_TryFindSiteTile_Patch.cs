using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;
using RimWorld.QuestGen;
using UnityEngine;

namespace Closer_VE_Quests.Patches
{
    [HarmonyPatch]
    public static class QuestNode_Site_Cryptoforge_TryFindSiteTile_Patch
    {
        private static readonly Type QuestNodeSiteType = AccessTools.TypeByName("VanillaQuestsExpandedCryptoforge.QuestNode_Site");
        private static readonly MethodInfo IsValidTileMethod = AccessTools.Method(QuestNodeSiteType, "IsValidTile", new[] { typeof(int), typeof(List<BiomeDef>) });

        public static bool Prepare()
        {
            return SupportedModsCache.CryptoforgeLoaded
                && QuestNodeSiteType != null
                && IsValidTileMethod != null;
        }

        public static MethodBase TargetMethod()
        {
            return AccessTools.Method(QuestNodeSiteType, "TryFindSiteTile");
        }

        public static bool Prefix(ref int tile, Predicate<int> extraValidator, List<BiomeDef> allowedBiomes, ref bool __result)
        {
            if (allowedBiomes != null && !Find.WorldGrid.Tiles.Any(surfaceTile => allowedBiomes.Contains(surfaceTile.PrimaryBiome)))
            {
                allowedBiomes = null;
            }

            Map map = QuestGen_Get.GetMap();
            int minDistance = Mathf.Clamp(ModSettings.cryptoforgeSiteMinDistance, ModSettings.AbsoluteMinDistance, ModSettings.AbsoluteMaxDistance);
            int maxDistance = Mathf.Clamp(ModSettings.cryptoforgeSiteMaxDistance, minDistance, ModSettings.AbsoluteMaxDistance);

            IEnumerable<int> candidateTiles = Find.World.tilesInRandomOrder.Tiles.Where(candidateTile =>
                (extraValidator == null || extraValidator(candidateTile))
                && IsValidTile(candidateTile, allowedBiomes)
                && IsWithinDistance(candidateTile, map, minDistance, maxDistance));

            if (candidateTiles.TryRandomElement(out tile))
            {
                __result = true;
                return false;
            }

            tile = -1;
            __result = false;
            return false;
        }

        private static bool IsValidTile(int tile, List<BiomeDef> allowedBiomes)
        {
            return (bool)IsValidTileMethod.Invoke(null, new object[] { tile, allowedBiomes });
        }

        private static bool IsWithinDistance(int tile, Map map, int minDistance, int maxDistance)
        {
            float distance = Find.WorldGrid.ApproxDistanceInTiles(tile, map.Tile);
            return distance >= minDistance && distance <= maxDistance;
        }
    }
}

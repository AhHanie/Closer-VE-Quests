using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;
using UnityEngine;
using RimWorld.QuestGen;

namespace Closer_VE_Quests.Patches
{
    [HarmonyPatch]
    public static class QuestNode_Site_TheGenerator_TryFindSiteTile_Patch
    {
        private const string InventorShelterQuestNodeTypeName = "VanillaQuestsExpandedTheGenerator.QuestNode_Root_InventorShelter";

        private static readonly Type QuestNodeSiteType = AccessTools.TypeByName("VanillaQuestsExpandedTheGenerator.QuestNode_Site");
        private static readonly MethodInfo IsValidTileMethod = AccessTools.Method(QuestNodeSiteType, "IsValidTile");

        public static bool Prepare()
        {
            return SupportedModsCache.GeneratorLoaded
                && QuestNodeSiteType != null
                && IsValidTileMethod != null;
        }

        public static MethodBase TargetMethod()
        {
            return AccessTools.Method(QuestNodeSiteType, "TryFindSiteTile");
        }

        public static bool Prefix(ref int tile, Predicate<int> extraValidator, ref bool __result)
        {
            Map map = QuestGen_Get.GetMap();
            int minDistance = Mathf.Clamp(ModSettings.generatorSiteMinDistance, ModSettings.AbsoluteMinDistance, ModSettings.AbsoluteMaxDistance);
            int maxDistance = Mathf.Clamp(ModSettings.generatorSiteMaxDistance, minDistance, ModSettings.AbsoluteMaxDistance);
            Predicate<int> effectiveExtraValidator = ShouldIgnoreExtraValidator(extraValidator) ? null : extraValidator;

            IEnumerable<int> candidateTiles = Find.World.tilesInRandomOrder.Tiles.Where(candidateTile =>
                (effectiveExtraValidator == null || effectiveExtraValidator(candidateTile))
                && IsValidTile(candidateTile)
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

        private static bool IsValidTile(int tile)
        {
            return (bool)IsValidTileMethod.Invoke(null, new object[] { tile });
        }

        private static bool IsWithinDistance(int tile, Map map, int minDistance, int maxDistance)
        {
            float distance = Find.WorldGrid.ApproxDistanceInTiles(tile, map.Tile);
            return distance >= minDistance && distance <= maxDistance;
        }

        private static bool ShouldIgnoreExtraValidator(Predicate<int> extraValidator)
        {
            return ModSettings.generatorInventorShelterAnywhere
                && extraValidator != null
                && (IsInventorShelterType(QuestGen.Root?.root?.GetType())
                    || IsInventorShelterType(extraValidator.Method?.DeclaringType)
                    || IsInventorShelterType(extraValidator.Target?.GetType()));
        }

        private static bool IsInventorShelterType(Type type)
        {
            for (Type current = type; current != null; current = current.DeclaringType)
            {
                if (current.FullName == InventorShelterQuestNodeTypeName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace Closer_VE_Quests.Patches
{
    [HarmonyPatch]
    public static class QuestNode_Site_TheGenerator_TryFindSiteTile_Patch
    {
        private static MethodBase _target;

        public static bool Prepare()
        {
            if (!SupportedModsCache.GeneratorLoaded)
            {
                return false;
            }

            _target = PatchTargets.FindSiteTileMethod("VanillaQuestsExpandedTheGenerator.QuestNode_Site");
            return _target != null;
        }

        public static MethodBase TargetMethod()
        {
            return _target;
        }

        [HarmonyInfix(typeof(WorldTilesInRandomOrder), nameof(WorldTilesInRandomOrder.Tiles), InnerTargetKind.Getter)]
        public static List<int> Postfix(List<int> __result)
        {
            return TileDistance.FilterTiles(__result, ModSettings.generatorSiteMinDistance, ModSettings.generatorSiteMaxDistance);
        }
    }
}

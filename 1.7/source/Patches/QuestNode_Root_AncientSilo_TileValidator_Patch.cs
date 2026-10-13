using HarmonyLib;
using RimWorld.Planet;
using System.Reflection;

namespace Closer_VE_Quests.Patches
{
    [HarmonyPatch]
    public static class QuestNode_Root_AncientSilo_TileValidator_Patch
    {
        private static MethodBase _target;

        public static bool Prepare()
        {
            if (!SupportedModsCache.DeadlifeLoaded)
            {
                return false;
            }

            _target = PatchTargets.FindTileValidatorLambda("VanillaQuestsExpandedDeadlife.QuestNode_Root_AncientSilo");
            return _target != null;
        }

        public static MethodBase TargetMethod()
        {
            return _target;
        }

        [HarmonyInfix(typeof(WorldGrid), nameof(WorldGrid.ApproxDistanceInTiles), typeof(PlanetTile), typeof(PlanetTile))]
        public static float Postfix(float __result)
        {
            return TileDistance.Gate(__result, ModSettings.ancientSiloMinDistance, ModSettings.ancientSiloMaxDistance);
        }
    }
}

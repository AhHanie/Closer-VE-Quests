using HarmonyLib;
using RimWorld.Planet;
using System.Linq;
using System.Reflection;
using Verse;

namespace Closer_VE_Quests
{
    public static class PatchTargets
    {
        private static readonly MethodInfo TilesGetter = AccessTools.PropertyGetter(typeof(WorldTilesInRandomOrder), nameof(WorldTilesInRandomOrder.Tiles));
        private static readonly MethodInfo ApproxDistanceInTiles = AccessTools.Method(typeof(WorldGrid), nameof(WorldGrid.ApproxDistanceInTiles), new[] { typeof(PlanetTile), typeof(PlanetTile) });

        public static MethodBase FindSiteTileMethod(string questNodeTypeName)
        {
            MethodBase method = AccessTools.Method(AccessTools.TypeByName(questNodeTypeName), "TryFindSiteTile");
            if (method == null)
            {
                Logger.Warning($"{questNodeTypeName}.TryFindSiteTile was not found; its quest distance patch is skipped.");
                return null;
            }

            if (!Calls(method, TilesGetter))
            {
                Logger.Warning($"{questNodeTypeName}.TryFindSiteTile no longer reads WorldTilesInRandomOrder.Tiles; its quest distance patch is skipped.");
                return null;
            }

            return method;
        }

        public static MethodBase FindTileValidatorLambda(string questNodeTypeName)
        {
            MethodInfo getter = AccessTools.PropertyGetter(AccessTools.TypeByName(questNodeTypeName), "TileValidator");
            MethodInfo[] lambdas = getter == null ? new MethodInfo[0] : AccessTools.Lambdas(getter).ToArray();
            if (lambdas.Length != 1)
            {
                Logger.Warning($"{questNodeTypeName}.TileValidator no longer has exactly one lambda; its quest distance patch is skipped.");
                return null;
            }

            if (!Calls(lambdas[0], ApproxDistanceInTiles))
            {
                Logger.Warning($"{questNodeTypeName}.TileValidator no longer measures WorldGrid.ApproxDistanceInTiles; its quest distance patch is skipped.");
                return null;
            }

            return lambdas[0];
        }

        private static bool Calls(MethodBase caller, MethodBase callee)
        {
            return PatchProcessor.ReadMethodBody(caller).Any(instruction =>
                instruction.Value is MethodBase called
                && called.Module == callee.Module
                && called.MetadataToken == callee.MetadataToken);
        }
    }
}

using RimWorld.Planet;
using RimWorld.QuestGen;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Closer_VE_Quests
{
    public static class TileDistance
    {
        // The Deadlife validators compare the measured distance with a hard-coded cap, so an in-range distance becomes 0 and anything else becomes infinity.
        public static float Gate(float distance, int minDistance, int maxDistance)
        {
            ClampRange(ref minDistance, ref maxDistance);
            return distance >= minDistance && distance <= maxDistance ? 0f : float.PositiveInfinity;
        }

        public static List<int> FilterTiles(List<int> tiles, int minDistance, int maxDistance)
        {
            Map map = QuestGen_Get.GetMap();
            if (map == null)
            {
                return tiles;
            }

            ClampRange(ref minDistance, ref maxDistance);
            WorldGrid worldGrid = Find.WorldGrid;
            PlanetTile origin = map.Tile;
            List<int> result = new List<int>();
            foreach (int tile in tiles)
            {
                float distance = worldGrid.ApproxDistanceInTiles(tile, origin);
                if (distance >= minDistance && distance <= maxDistance)
                {
                    result.Add(tile);
                }
            }

            return result;
        }

        private static void ClampRange(ref int minDistance, ref int maxDistance)
        {
            minDistance = Mathf.Clamp(minDistance, ModSettings.AbsoluteMinDistance, ModSettings.AbsoluteMaxDistance);
            maxDistance = Mathf.Clamp(maxDistance, minDistance, ModSettings.AbsoluteMaxDistance);
        }
    }
}

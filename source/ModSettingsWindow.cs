using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Closer_VE_Quests
{
    public static class ModSettingsWindow
    {
        private static Vector2 scrollPosition;
        private static float contentHeight = 900f;

        public static void Draw(Rect parent)
        {
            ModSettings.Validate();

            bool deadlifeLoaded = SupportedModsCache.DeadlifeLoaded;
            bool generatorLoaded = SupportedModsCache.GeneratorLoaded;
            bool cryptoforgeLoaded = SupportedModsCache.CryptoforgeLoaded;
            bool anySupportedModsLoaded = SupportedModsCache.AnySupportedModsLoaded;

            float viewWidth = parent.width - 16f;
            Rect viewRect = new Rect(0f, 0f, viewWidth, contentHeight);

            Widgets.BeginScrollView(parent, ref scrollPosition, viewRect);

            Listing_Standard listing = new Listing_Standard();
            listing.Begin(new Rect(0f, 0f, viewWidth - 10f, viewRect.height));

            if (!anySupportedModsLoaded)
            {
                listing.Label("No VE Quests supported mods loaded");
            }

            if (deadlifeLoaded)
            {
                DrawSectionHeader(listing, "Vanilla Quests Expanded - Deadlife");

                DrawDistanceSetting(
                    listing,
                    "CloserVEQuests.AncientSiloDistanceDescription".Translate(),
                    "CloserVEQuests.AncientSiloMinDistanceLabel",
                    "CloserVEQuests.AncientSiloMaxDistanceLabel",
                    ref ModSettings.ancientSiloMinDistance,
                    ref ModSettings.ancientSiloMaxDistance);

                DrawDistanceSetting(
                    listing,
                    "CloserVEQuests.AncientIcbmLaunchSiteDistanceDescription".Translate(),
                    "CloserVEQuests.AncientIcbmLaunchSiteMinDistanceLabel",
                    "CloserVEQuests.AncientIcbmLaunchSiteMaxDistanceLabel",
                    ref ModSettings.ancientIcbmLaunchSiteMinDistance,
                    ref ModSettings.ancientIcbmLaunchSiteMaxDistance);
            }

            if (generatorLoaded)
            {
                DrawSectionHeader(listing, "Vanilla Quests Expanded - The Generator");
                listing.Label("CloserVEQuests.GeneratorSiteDistanceNote".Translate());
                listing.Gap(10f);
                DrawMinMaxSliders(
                    listing,
                    "CloserVEQuests.GeneratorSiteMinDistanceLabel",
                    "CloserVEQuests.GeneratorSiteMaxDistanceLabel",
                    ref ModSettings.generatorSiteMinDistance,
                    ref ModSettings.generatorSiteMaxDistance);
            }

            if (cryptoforgeLoaded)
            {
                DrawSectionHeader(listing, "Vanilla Quests Expanded - Cryptoforge");
                listing.Label("CloserVEQuests.CryptoforgeSiteDistanceNote".Translate());
                listing.Gap(10f);
                DrawMinMaxSliders(
                    listing,
                    "CloserVEQuests.CryptoforgeSiteMinDistanceLabel",
                    "CloserVEQuests.CryptoforgeSiteMaxDistanceLabel",
                    ref ModSettings.cryptoforgeSiteMinDistance,
                    ref ModSettings.cryptoforgeSiteMaxDistance);
            }

            listing.End();
            Widgets.EndScrollView();

            contentHeight = Mathf.Max(parent.height + 1f, listing.CurHeight + 18f);
            Text.Font = GameFont.Small;
        }

        private static void DrawSectionHeader(Listing_Standard listing, string label)
        {
            listing.GapLine();
            Text.Font = GameFont.Medium;
            listing.Label(label);
            Text.Font = GameFont.Small;
            listing.Gap(10f);
        }

        private static void DrawDistanceSetting(Listing_Standard listing, string description, string minLabelKey, string maxLabelKey, ref int minValue, ref int maxValue)
        {
            listing.Label(description);
            listing.Gap(10f);
            DrawMinMaxSliders(listing, minLabelKey, maxLabelKey, ref minValue, ref maxValue);
        }

        private static void DrawMinMaxSliders(Listing_Standard listing, string minLabelKey, string maxLabelKey, ref int minValue, ref int maxValue)
        {
            listing.Label(minLabelKey.Translate(minValue));
            int newMinValue = Mathf.RoundToInt(listing.Slider(minValue, ModSettings.AbsoluteMinDistance, ModSettings.AbsoluteMaxDistance));
            if (newMinValue != minValue)
            {
                minValue = newMinValue;
                ModSettings.Validate();
            }

            listing.Gap(10f);

            listing.Label(maxLabelKey.Translate(maxValue));
            int newMaxValue = Mathf.RoundToInt(listing.Slider(maxValue, ModSettings.AbsoluteMinDistance, ModSettings.AbsoluteMaxDistance));
            if (newMaxValue != maxValue)
            {
                maxValue = newMaxValue;
                ModSettings.Validate();
            }

            listing.Gap(18f);
        }
    }
}

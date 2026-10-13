using HarmonyLib;
using UnityEngine;
using Verse;

namespace Closer_VE_Quests
{
    public class Mod : Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
            SupportedModsCache.Refresh();
            GetSettings<ModSettings>();
            new Harmony("sk.closervequests").PatchAll();
        }

        public override string SettingsCategory()
        {
            return "CloserVEQuests.SettingsTitle".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            ModSettingsWindow.Draw(inRect);
            base.DoSettingsWindowContents(inRect);
        }
    }
}

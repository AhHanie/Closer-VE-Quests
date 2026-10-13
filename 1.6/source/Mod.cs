using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Closer_VE_Quests
{
    public class Mod : Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
            LongEventHandler.QueueLongEvent(Init, "CloserVEQuests.LoadingLabel", doAsynchronously: true, null);
        }

        public void Init()
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

using HarmonyLib;
using RimWorld.QuestGen;
using System;
using System.Reflection;

namespace Closer_VE_Quests.Patches
{
    [HarmonyPatch]
    public static class QuestNode_Site_TheGenerator_InventorShelter_Patch
    {
        private const string InventorShelterQuestNodeTypeName = "VanillaQuestsExpandedTheGenerator.QuestNode_Root_InventorShelter";

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

        public static void Prefix(ref Predicate<int> extraValidator)
        {
            if (ShouldIgnoreExtraValidator(extraValidator))
            {
                extraValidator = null;
            }
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

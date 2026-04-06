using Verse;

namespace Closer_VE_Quests
{
    public static class SupportedModsCache
    {
        private const string DeadlifePackageId = "vanillaquestsexpanded.deadlife";
        private const string GeneratorPackageId = "vanillaquestsexpanded.generator";
        private const string CryptoforgePackageId = "vanillaquestsexpanded.cryptoforge";

        public static bool DeadlifeLoaded { get; private set; }
        public static bool GeneratorLoaded { get; private set; }
        public static bool CryptoforgeLoaded { get; private set; }

        public static bool AnySupportedModsLoaded => DeadlifeLoaded || GeneratorLoaded || CryptoforgeLoaded;

        public static void Refresh()
        {
            DeadlifeLoaded = ModsConfig.IsActive(DeadlifePackageId);
            GeneratorLoaded = ModsConfig.IsActive(GeneratorPackageId);
            CryptoforgeLoaded = ModsConfig.IsActive(CryptoforgePackageId);
        }
    }
}

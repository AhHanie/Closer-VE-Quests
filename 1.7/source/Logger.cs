using Verse;

namespace Closer_VE_Quests
{
    public static class Logger
    {
        private const string Prefix = "[Closer VE Quests] ";

        public static void Warning(string message)
        {
            Log.Warning(Prefix + message);
        }
    }
}

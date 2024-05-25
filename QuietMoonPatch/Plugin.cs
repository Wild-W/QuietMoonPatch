using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace QuietMoonPatch
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ModBase : BaseUnityPlugin
    {
        private const string modGUID = "BomenorenMoonModWildW";
        private const string modName = "Bomenoren";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static ModBase instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Bomenoren mod has loaded");

            harmony.PatchAll(typeof(ModBase));
        }
    }
}

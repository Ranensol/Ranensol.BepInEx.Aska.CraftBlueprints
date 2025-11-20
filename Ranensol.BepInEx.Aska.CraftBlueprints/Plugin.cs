using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Ranensol.BepInEx.Aska.CraftBlueprints.Blueprints;
using Ranensol.BepInEx.Aska.CraftBlueprints.FileIO;
using Ranensol.BepInEx.Aska.CraftBlueprints.GameData;
using Ranensol.BepInEx.Aska.CraftBlueprints.Recipes;
using SSSGame;

namespace Ranensol.BepInEx.Aska.CraftBlueprints
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        internal static new ManualLogSource Log;
        internal static List<CraftBlueprintInfo> CustomBlueprints;

        private static string _pluginFolder;

        public override void Load()
        {
            InitializePlugin();
            InitializeConfig();
            DumpGameReferences();
            ApplyHarmonyPatches();
            LoadAndBuildBlueprints();
        }

        private void InitializePlugin()
        {
            Log = base.Log;
            Log.LogInfo($"[Plugin] {MyPluginInfo.PLUGIN_GUID} is loading");

            _pluginFolder = Path.Combine(Paths.PluginPath, MyPluginInfo.PLUGIN_NAME);
            Directory.CreateDirectory(_pluginFolder);

            ResourceInfoHelper.CreateResourceInfos();
        }

        private void InitializeConfig()
        {
            PluginConfig.Initialize(Config);

            Log.LogInfo($"[Plugin] Config loaded - CreateExamples: {PluginConfig.ConfigCreateExamples.Value}, CreateCustom: {PluginConfig.ConfigCreateCustom.Value}, DumpReferences: {PluginConfig.ConfigDumpReferences.Value}");
        }

        private static void DumpGameReferences()
        {
            if (PluginConfig.ConfigDumpReferences.Value)
            {
                ReferenceDumper.DumpAllReferences(_pluginFolder);
            }
            else
            {
                Log.LogInfo($"[Plugin] Reference file dumping disabled in config");
            }
        }

        private static void ApplyHarmonyPatches()
        {
            Log.LogInfo($"[Plugin] Applying Harmony patches");
            Harmony harmony = new(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }

        private static void LoadAndBuildBlueprints()
        {
            // Load recipes from JSON files
            var specs = RecipeLoader.LoadAll(_pluginFolder);

            if (specs.Count == 0)
            {
                CustomBlueprints = [];
                Log.LogInfo($"[Plugin] No recipes to build. Mod loaded successfully but no blueprints added");
                return;
            }

            // Build Unity blueprint objects
            CustomBlueprints = BlueprintBuilder.BuildAll(specs);

            if (CustomBlueprints.Count == 0)
            {
                Log.LogWarning($"[Plugin] All blueprints failed to build. No blueprints added to game.");
                return;
            }

            // Add blueprints to game menus
            var totalAdded = BlueprintMenuIntegration.AddToMenus(
                BlueprintBuilder.AllSpecs,
                CustomBlueprints);

            Log.LogInfo($"[Plugin] Successfully loaded {CustomBlueprints.Count} blueprints with {totalAdded} menu entries");
        }
    }
}
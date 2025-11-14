using SSSGame;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Blueprints
{
    internal static class BlueprintBuilder
    {
        public static List<BlueprintSpec> AllSpecs { get; private set; }

        public static List<CraftBlueprintInfo> BuildAll(List<BlueprintSpec> specs)
        {
            AllSpecs = specs;
            Plugin.Log.LogInfo($"[BlueprintBuilder] Building {specs.Count} blueprints...");

            List<CraftBlueprintInfo> result = [];
            var successCount = 0;
            var failCount = 0;

            foreach (var spec in specs)
            {
                try
                {
                    var blueprint = BlueprintFactory.Create(spec);
                    result.Add(blueprint);
                    successCount++;
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[BlueprintBuilder] Failed to build '{spec.Name}': {ex.Message}");
                    failCount++;
                }
            }

            Plugin.Log.LogInfo($"[BlueprintBuilder] Complete: {successCount} succeeded, {failCount} failed");
            return result;
        }
    }
}
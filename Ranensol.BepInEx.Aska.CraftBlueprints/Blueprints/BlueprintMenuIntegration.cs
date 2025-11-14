using Ranensol.BepInEx.Aska.CraftBlueprints.GameData;
using SSSGame;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Blueprints
{
    internal static class BlueprintMenuIntegration
    {
        public static int AddToMenus(List<BlueprintSpec> specs, List<CraftBlueprintInfo> blueprints)
        {
            var totalAdded = 0;
            var count = Math.Min(specs.Count, blueprints.Count);

            for (var i = 0; i < count; i++)
            {
                totalAdded += AddToMenus(specs[i], blueprints[i]);
            }

            Plugin.Log.LogInfo($"[BlueprintMenuIntegration] Total menu entries added: {totalAdded}");
            return totalAdded;
        }

        private static int AddToMenus(BlueprintSpec spec, CraftBlueprintInfo blueprint)
        {
            var count = 0;

            foreach (var menuName in spec.MenuLists)
            {
                if (!ResourceInfoHelper.AllItemInfoLists.TryGetValue(menuName, out var menuList))
                {
                    Plugin.Log.LogWarning($"[BlueprintMenuIntegration] Menu '{menuName}' not found in game");
                    continue;
                }

                menuList.itemInfoList.Add(blueprint);
                count++;
                Plugin.Log.LogInfo($"[BlueprintMenuIntegration] Added '{blueprint.Name}' to {menuName}");
            }

            return count;
        }
    }
}
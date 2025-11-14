using HarmonyLib;
using SandSailorStudio.Inventory;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Patches
{
    [HarmonyPatch(typeof(ItemInfoDatabase), "Initialize")]
    internal class ItemInfoDatabase_Initialize_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(ItemInfoDatabase __instance)
        {
            Plugin.Log.LogInfo("[ItemInfoDatabase_Initialize_Patch] - Injecting custom blueprints");

            try
            {
                if (__instance?._itemsMap == null || __instance._itemsMap is not Il2CppSystem.Collections.Generic.Dictionary<int, ItemInfo> itemsMap)
                {
                    Plugin.Log.LogError("[ItemInfoDatabase_Initialize_Patch] _itemsMap is null!");
                    return;
                }

                foreach (var bp in Plugin.CustomBlueprints)
                {
                    // Use the ID already set in CreateBlueprint
                    itemsMap[bp.id] = bp;
                    Plugin.Log.LogInfo($"[ItemInfoDatabase_Initialize_Patch] Added blueprint to ItemInfoDatabase: {bp.Name}, ID: {bp.id}");
                }

            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[ItemInfoDatabase_Initialize_Patch] Error in ItemInfoDatabase patch: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
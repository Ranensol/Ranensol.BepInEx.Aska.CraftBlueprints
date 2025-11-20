using Ranensol.BepInEx.Aska.CraftBlueprints.GameData;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.FileIO
{
    internal static class ReferenceDumper
    {
        private static string _referenceFolder;

        public static void DumpAllReferences(string pluginFolder)
        {
            _referenceFolder = Path.Combine(pluginFolder, Constants.REFERENCE_FOLDER_NAME);
            Directory.CreateDirectory(_referenceFolder);

            Plugin.Log.LogInfo($"[ReferenceDumper] Dumping reference files to: {_referenceFolder}");

            DumpItems();
            DumpMenuLists();
            DumpRules();
            DumpCategories();
            DumpInteractions();

            Plugin.Log.LogInfo($"[ReferenceDumper] Reference dump complete!");
        }

        private static void DumpItems()
        {
            var path = Path.Combine(_referenceFolder, "Items.txt");
            var allItems = ResourceInfoHelper.AllItemInfos.Keys.OrderBy(k => k).ToList();
            var filteredItems = ReferenceFilterUtility.FilterItems(allItems);
            File.WriteAllLines(path, filteredItems);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {filteredItems.Count} items (filtered from {allItems.Count})");
        }

        private static void DumpMenuLists()
        {
            var path = Path.Combine(_referenceFolder, "MenuLists.txt");
            var allLists = ResourceInfoHelper.AllItemInfoLists.Keys.OrderBy(k => k).ToList();
            var filteredLists = ReferenceFilterUtility.FilterMenuLists(allLists);
            File.WriteAllLines(path, filteredLists);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {filteredLists.Count} menu lists (filtered from {allLists.Count})");
        }

        private static void DumpRules()
        {
            var path = Path.Combine(_referenceFolder, "Rules.txt");
            var allRules = ResourceInfoHelper.AllBlueprintConditionRules.Keys.OrderBy(k => k).ToList();
            var filteredRules = ReferenceFilterUtility.FilterRules(allRules);
            File.WriteAllLines(path, filteredRules);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {filteredRules.Count} rules (filtered from {allRules.Count})");
        }

        private static void DumpCategories()
        {
            var path = Path.Combine(_referenceFolder, "Categories.txt");
            var allCategories = ResourceInfoHelper.AllItemCategoryInfos.Keys.OrderBy(k => k).ToList();
            var filteredCategories = ReferenceFilterUtility.FilterCategories(allCategories);
            File.WriteAllLines(path, filteredCategories);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {filteredCategories.Count} categories (filtered from {allCategories.Count})");
        }

        private static void DumpInteractions()
        {
            var path = Path.Combine(_referenceFolder, "Interactions.txt");
            var allInteractions = ResourceInfoHelper.AllCraftInteractions.Keys.OrderBy(k => k).ToList();
            var filteredInteractions = ReferenceFilterUtility.FilterInteractions(allInteractions);
            File.WriteAllLines(path, filteredInteractions);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {filteredInteractions.Count} interactions (filtered from {allInteractions.Count})");
        }
    }
}
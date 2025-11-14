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
            DumpStations();
            DumpStorageClasses();

            Plugin.Log.LogInfo($"[ReferenceDumper] Reference dump complete!");
        }

        private static void DumpItems()
        {
            var path = Path.Combine(_referenceFolder, "Items.txt");
            var items = ResourceInfoHelper.AllItemInfos.Keys.OrderBy(k => k).ToList();
            File.WriteAllLines(path, items);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {items.Count} items");
        }

        private static void DumpMenuLists()
        {
            var path = Path.Combine(_referenceFolder, "MenuLists.txt");
            var lists = ResourceInfoHelper.AllItemInfoLists.Keys.OrderBy(k => k).ToList();
            File.WriteAllLines(path, lists);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {lists.Count} menu lists");
        }

        private static void DumpRules()
        {
            var path = Path.Combine(_referenceFolder, "Rules.txt");
            var rules = ResourceInfoHelper.AllBlueprintConditionRules.Keys.OrderBy(k => k).ToList();
            File.WriteAllLines(path, rules);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {rules.Count} rules");
        }

        private static void DumpCategories()
        {
            var path = Path.Combine(_referenceFolder, "Categories.txt");
            var categories = ResourceInfoHelper.AllItemCategoryInfos.Keys.OrderBy(k => k).ToList();
            File.WriteAllLines(path, categories);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {categories.Count} categories");
        }

        private static void DumpStations()
        {
            var path = Path.Combine(_referenceFolder, "Stations.txt");
            var stations = ResourceInfoHelper.AllCraftInteractions.Keys.OrderBy(k => k).ToList();
            File.WriteAllLines(path, stations);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {stations.Count} stations");
        }

        private static void DumpStorageClasses()
        {
            var path = Path.Combine(_referenceFolder, "StorageClasses.txt");
            var classes = ResourceInfoHelper.AllItemStorageClasses.Keys.OrderBy(k => k).ToList();
            File.WriteAllLines(path, classes);
            Plugin.Log.LogInfo($"[ReferenceDumper] Dumped {classes.Count} storage classes");
        }
    }
}
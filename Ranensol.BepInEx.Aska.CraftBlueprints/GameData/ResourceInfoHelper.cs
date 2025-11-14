using SandSailorStudio.Inventory;
using SSSGame;
using UnityEngine;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.GameData
{
    internal static class ResourceInfoHelper
    {
        // Private backing fields - prevents external modification
        private static Dictionary<string, ItemInfo> _allItemInfos = [];
        private static Dictionary<string, ItemInfoList> _allItemInfoLists = [];
        private static Dictionary<string, BlueprintConditionsRule> _allBlueprintConditionRules = [];
        private static Dictionary<string, ItemCategoryInfo> _allItemCategoryInfos = [];
        private static Dictionary<string, CraftInteraction> _allCraftInteractions = [];
        private static Dictionary<string, ItemStorageClass> _allItemStorageClasses = [];

        // Public read-only accessors
        public static IReadOnlyDictionary<string, ItemInfo> AllItemInfos => _allItemInfos;
        public static IReadOnlyDictionary<string, ItemInfoList> AllItemInfoLists => _allItemInfoLists;
        public static IReadOnlyDictionary<string, BlueprintConditionsRule> AllBlueprintConditionRules => _allBlueprintConditionRules;
        public static IReadOnlyDictionary<string, ItemCategoryInfo> AllItemCategoryInfos => _allItemCategoryInfos;
        public static IReadOnlyDictionary<string, CraftInteraction> AllCraftInteractions => _allCraftInteractions;
        public static IReadOnlyDictionary<string, ItemStorageClass> AllItemStorageClasses => _allItemStorageClasses;

        /// <summary>
        /// Loads all game data, skipping duplicates
        /// </summary>
        public static void CreateResourceInfos()
        {
            Plugin.Log.LogInfo($"[ResourceInfoHelper] Creating resource info");

            // Preload all resources, lists will be empty otherwise
            PreloadUnityResources();

            LoadItemInfos();
            LoadBlueprintConditionRules();
            LoadItemCategoryInfos();
            LoadCraftInteractions();
            LoadItemInfoLists();
            LoadItemStorageClasses();
        }

        private static void PreloadUnityResources()
        {
            _ = Resources.LoadAll("", Il2CppSystem.Type.GetType("SSSGame.ResourceInfo, Assembly-CSharp"));
        }

        private static void LoadItemInfos()
        {
            _allItemInfos = [];

            foreach (var item in Resources.FindObjectsOfTypeAll<ItemInfo>())
            {
                if (!_allItemInfos.ContainsKey(item.name))
                {
                    _allItemInfos.Add(item.name, item);
                }
            }

            Plugin.Log.LogInfo($"[ResourceInfoHelper] Loaded {_allItemInfos.Count} ItemInfos");
        }

        private static void LoadBlueprintConditionRules()
        {
            _allBlueprintConditionRules = [];

            foreach (var rule in Resources.FindObjectsOfTypeAll<BlueprintConditionsRule>())
            {
                if (!_allBlueprintConditionRules.ContainsKey(rule.name))
                {
                    _allBlueprintConditionRules.Add(rule.name, rule);
                }
            }

            Plugin.Log.LogInfo($"[ResourceInfoHelper] Loaded {_allBlueprintConditionRules.Count} BlueprintConditionsRules");
        }

        private static void LoadItemCategoryInfos()
        {
            _allItemCategoryInfos = [];

            foreach (var category in Resources.FindObjectsOfTypeAll<ItemCategoryInfo>())
            {
                if (!_allItemCategoryInfos.ContainsKey(category.name))
                {
                    _allItemCategoryInfos.Add(category.name, category);
                }
            }

            Plugin.Log.LogInfo($"[ResourceInfoHelper] Loaded {_allItemCategoryInfos.Count} ItemCategoryInfos");
        }

        private static void LoadCraftInteractions()
        {
            _allCraftInteractions = [];

            foreach (var interaction in Resources.FindObjectsOfTypeAll<CraftInteraction>())
            {
                if (!_allCraftInteractions.ContainsKey(interaction.name))
                {
                    _allCraftInteractions.Add(interaction.name, interaction);
                }
            }

            Plugin.Log.LogInfo($"[ResourceInfoHelper] Loaded {_allCraftInteractions.Count} CraftInteractions");
        }

        private static void LoadItemInfoLists()
        {
            _allItemInfoLists = [];

            foreach (var infoList in Resources.FindObjectsOfTypeAll<ItemInfoList>())
            {
                if (!_allItemInfoLists.ContainsKey(infoList.name))
                {
                    _allItemInfoLists.Add(infoList.name, infoList);
                }
            }

            Plugin.Log.LogInfo($"[ResourceInfoHelper] Loaded {_allItemInfoLists.Count} ItemInfoLists");
        }

        private static void LoadItemStorageClasses()
        {
            _allItemStorageClasses = [];

            foreach (var storage in Resources.FindObjectsOfTypeAll<ItemStorageClass>())
            {
                if (!_allItemStorageClasses.ContainsKey(storage.name))
                {
                    _allItemStorageClasses.Add(storage.name, storage);
                }
            }

            Plugin.Log.LogInfo($"[ResourceInfoHelper] Loaded {_allItemStorageClasses.Count} ItemStorageClasses");
        }

        // Helper methods for common operations

        public static bool ItemExists(string name)
        {
            return _allItemInfos.ContainsKey(name);
        }

        public static bool MenuListExists(string name)
        {
            return _allItemInfoLists.ContainsKey(name);
        }

        public static bool RuleExists(string name)
        {
            return _allBlueprintConditionRules.ContainsKey(name);
        }

        public static bool CategoryExists(string name)
        {
            return _allItemCategoryInfos.ContainsKey(name);
        }

        public static bool InteractionExists(string name)
        {
            return _allCraftInteractions.ContainsKey(name);
        }
    }
}
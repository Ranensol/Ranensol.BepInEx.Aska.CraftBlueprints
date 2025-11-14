using Ranensol.BepInEx.Aska.CraftBlueprints.GameData;
using SandSailorStudio.Attributes;
using SandSailorStudio.Inventory;
using SSSGame;
using UnityEngine;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Blueprints
{
    internal static class BlueprintFactory
    {
        private const float DEFAULT_CRAFT_VOLUME = 1f;
        private const int DEFAULT_STACK_SIZE = 1;
        private const int DEFAULT_SPAWN_HEIGHT = 1;
        private const string VIRTUAL_ITEM_STORAGE_CLASS = "VirtualItem";
        private const string BLUEPRINT_NAME_PREFIX = "BPNAME_Ranensol_";

        public static CraftBlueprintInfo Create(BlueprintSpec spec)
        {
            var resultItem = GetResultItem(spec.ResultItem);
            var ingredients = BuildIngredients(spec.Ingredients);
            var blueprint = CreateBlueprintObject(spec, resultItem, ingredients);

            Plugin.Log.LogInfo($"[BlueprintFactory] Created '{blueprint.Name}' (ID: {blueprint.id})");

            return blueprint;
        }

        private static ItemInfo GetResultItem(string itemName)
        {
            return !ResourceInfoHelper.AllItemInfos.TryGetValue(itemName, out var resultItem)
                ? throw new ArgumentException($"Result item '{itemName}' not found in game")
                : resultItem;
        }

        private static List<ItemInfoQuantity> BuildIngredients((string, int)[] ingredientSpecs)
        {
            List<ItemInfoQuantity> ingredients = [];

            foreach ((var itemName, var quantity) in ingredientSpecs)
            {
                if (!ResourceInfoHelper.AllItemInfos.TryGetValue(itemName, out var ingredientItem))
                {
                    throw new ArgumentException($"Ingredient '{itemName}' not found in game");
                }

                ingredients.Add(new ItemInfoQuantity
                {
                    quantity = quantity,
                    itemInfo = ingredientItem
                });
            }

            return ingredients;
        }

        private static CraftBlueprintInfo CreateBlueprintObject(
            BlueprintSpec spec,
            ItemInfo resultItem,
            List<ItemInfoQuantity> ingredients)
        {
            var blueprint = ScriptableObject.CreateInstance<CraftBlueprintInfo>();

            SetBasicProperties(blueprint, spec);
            SetRecipeData(blueprint, spec, resultItem, ingredients);
            SetUnlockRules(blueprint, spec);
            SetCategoryAndInteraction(blueprint, spec);
            SetLocalization(blueprint, spec, resultItem);
            SetVisualProperties(blueprint, resultItem);
            SetStorageProperties(blueprint);
            InitializeEmptyCollections(blueprint);

            return blueprint;
        }

        private static void SetBasicProperties(CraftBlueprintInfo blueprint, BlueprintSpec spec)
        {
            blueprint.name = $"{BLUEPRINT_NAME_PREFIX}{spec.Quantity}{spec.Name.Replace(" ", "")}";
            blueprint.unique = true;
            blueprint.id = spec.Id;
            blueprint.availableInTrialVersion = true;
            blueprint.showTier = false;
        }

        private static void SetRecipeData(
            CraftBlueprintInfo blueprint,
            BlueprintSpec spec,
            ItemInfo resultItem,
            List<ItemInfoQuantity> ingredients)
        {
            blueprint.parts = ingredients.ToArray();
            blueprint.result = resultItem;
            blueprint.quantity = spec.Quantity;
            blueprint.cost = new ItemInfoQuantity { itemInfo = null, quantity = 0 };
        }

        private static void SetUnlockRules(CraftBlueprintInfo blueprint, BlueprintSpec spec)
        {
            Il2CppSystem.Collections.Generic.List<BlueprintConditionsRule> rulesList = new();

            foreach (var ruleName in spec.Rules)
            {
                if (ResourceInfoHelper.AllBlueprintConditionRules.TryGetValue(ruleName, out var ruleObj))
                {
                    rulesList.Add(ruleObj);
                }
                else
                {
                    Plugin.Log.LogWarning($"[BlueprintFactory] Rule '{ruleName}' not found in game");
                }
            }

            blueprint.blueprintConditionsRules = rulesList;
        }

        private static void SetCategoryAndInteraction(CraftBlueprintInfo blueprint, BlueprintSpec spec)
        {
            var category = GetCategory(spec.Category);
            var interaction = GetInteraction(spec.Interaction);

            blueprint.category = category;
            blueprint.interaction = interaction;
            blueprint.craftVolume = DEFAULT_CRAFT_VOLUME;
        }

        private static ItemCategoryInfo GetCategory(string categoryName)
        {
            return !ResourceInfoHelper.AllItemCategoryInfos.TryGetValue(categoryName, out var categoryObj)
                ? throw new ArgumentException($"Category '{categoryName}' not found in game")
                : categoryObj;
        }

        private static CraftInteraction GetInteraction(string interactionName)
        {
            return !ResourceInfoHelper.AllCraftInteractions.TryGetValue(interactionName, out var interactionObj)
                ? throw new ArgumentException($"Interaction '{interactionName}' not found in game")
                : interactionObj;
        }

        private static void SetLocalization(CraftBlueprintInfo blueprint, BlueprintSpec spec, ItemInfo resultItem)
        {
            blueprint.Localized = false;
            blueprint._localized = true;
            blueprint.localizedName = spec.Name;

            blueprint.localizedDescription = !string.IsNullOrWhiteSpace(spec.Description)
                ? spec.Description
                : resultItem.localizedDescription ?? $"Craft {spec.Name.ToLower()}";

            blueprint.localizedLore = !string.IsNullOrWhiteSpace(spec.Lore)
                ? spec.Lore
                : resultItem.localizedLore ?? string.Empty;
        }

        private static void SetVisualProperties(CraftBlueprintInfo blueprint, ItemInfo resultItem)
        {
            blueprint.icon = resultItem.icon;
            blueprint.previewImage = resultItem.previewImage;
            blueprint.spawnObject = null;
        }

        private static void SetStorageProperties(CraftBlueprintInfo blueprint)
        {
            blueprint.storageClass = ResourceInfoHelper.AllItemStorageClasses[VIRTUAL_ITEM_STORAGE_CLASS];
            blueprint.stackSize = DEFAULT_STACK_SIZE;
            blueprint.spawnHeight = DEFAULT_SPAWN_HEIGHT;
        }

        private static void InitializeEmptyCollections(CraftBlueprintInfo blueprint)
        {
            blueprint._cachedComponents = new Il2CppSystem.Collections.Generic.List<ItemInfo>();
            blueprint._cachedComponentsTable = new Il2CppSystem.Collections.Generic.Dictionary<ItemInfo, Il2CppSystem.ValueTuple<int, int>>();
            blueprint.components = Array.Empty<ItemInfoChance>();
            blueprint.attributes = Array.Empty<AttributeData>();
            blueprint.networkedInventoryAttributes = Array.Empty<AttributeConfig>();
            blueprint.processes = Array.Empty<ItemProcess>();
        }
    }
}
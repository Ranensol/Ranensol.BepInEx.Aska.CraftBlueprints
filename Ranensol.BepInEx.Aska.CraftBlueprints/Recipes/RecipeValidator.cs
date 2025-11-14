using Ranensol.BepInEx.Aska.CraftBlueprints.GameData;
using Ranensol.BepInEx.Aska.CraftBlueprints.Recipes.Models;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Recipes
{
    internal static class RecipeValidator
    {
        private const int RECOMMENDED_ID_START = 900000001;

        private static readonly HashSet<int> _usedIds = [];

        public static void ResetIdTracking()
        {
            _usedIds.Clear();
        }

        public static void ValidateRecipe(JsonRecipeSpec json)
        {
            ValidateId(json.Id);
            ValidateResultItem(json.ResultItem);
            ValidateName(json.Name);
            ValidateIngredients(json.Ingredients);
            ValidateQuantity(json.Quantity);
            ValidateMenuLists(json.MenuLists);
            ValidateRules(json.Rules);
            ValidateInteraction(json.Interaction);
            ValidateCategory(json.Category);
        }

        private static void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"id is required and must be > 0. Recommended range: {RECOMMENDED_ID_START}+");

            if (_usedIds.Contains(id))
                throw new ArgumentException($"id {id} is already used by another recipe. Each recipe must have a unique ID");

            _usedIds.Add(id);
        }

        private static void ValidateResultItem(string resultItem)
        {
            if (string.IsNullOrWhiteSpace(resultItem))
                throw new ArgumentException("resultItem is required");

            if (!ResourceInfoHelper.ItemExists(resultItem))
                throw new ArgumentException($"resultItem '{resultItem}' not found. Check Reference/Items.txt");
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name is required");
        }

        private static void ValidateIngredients(List<JsonIngredient> ingredients)
        {
            if (ingredients == null || ingredients.Count == 0)
                throw new ArgumentException("at least one ingredient is required");

            foreach (var ingredient in ingredients)
            {
                ValidateIngredient(ingredient);
            }
        }

        private static void ValidateIngredient(JsonIngredient ingredient)
        {
            if (string.IsNullOrWhiteSpace(ingredient.Item))
                throw new ArgumentException("ingredient item name is required");

            if (!ResourceInfoHelper.ItemExists(ingredient.Item))
                throw new ArgumentException($"ingredient '{ingredient.Item}' not found. Check Reference/Items.txt");

            if (ingredient.Quantity <= 0)
                throw new ArgumentException($"ingredient '{ingredient.Item}' quantity must be > 0");
        }

        private static void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("quantity must be > 0");
        }

        private static void ValidateMenuLists(List<string> menuLists)
        {
            if (menuLists == null || menuLists.Count == 0)
                throw new ArgumentException("at least one menuList is required");

            foreach (var menu in menuLists)
            {
                if (!ResourceInfoHelper.MenuListExists(menu))
                    throw new ArgumentException($"menuList '{menu}' not found. Check Reference/MenuLists.txt");
            }
        }

        private static void ValidateRules(List<string> rules)
        {
            if (rules == null)
                return;

            foreach (var rule in rules)
            {
                if (!ResourceInfoHelper.RuleExists(rule))
                    throw new ArgumentException($"rule '{rule}' not found. Check Reference/Rules.txt");
            }
        }

        private static void ValidateInteraction(string interaction)
        {
            if (string.IsNullOrWhiteSpace(interaction))
                throw new ArgumentException("interaction is required");

            if (!ResourceInfoHelper.InteractionExists(interaction))
                throw new ArgumentException($"interaction '{interaction}' not found. Check Reference/Stations.txt");
        }

        private static void ValidateCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("category is required");

            if (!ResourceInfoHelper.CategoryExists(category))
                throw new ArgumentException($"category '{category}' not found. Check Reference/Categories.txt");
        }
    }
}
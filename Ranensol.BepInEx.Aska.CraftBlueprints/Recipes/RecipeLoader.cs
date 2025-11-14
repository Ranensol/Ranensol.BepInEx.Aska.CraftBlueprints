using Ranensol.BepInEx.Aska.CraftBlueprints.Blueprints;
using Ranensol.BepInEx.Aska.CraftBlueprints.FileIO;
using Ranensol.BepInEx.Aska.CraftBlueprints.Recipes.Models;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Recipes
{
    internal static class RecipeLoader
    {
        public static List<BlueprintSpec> LoadAll(string pluginFolder)
        {
            RecipeValidator.ResetIdTracking();

            EnsureRecipesFolderExists(pluginFolder);
            EnsureRecipeFilesExist(pluginFolder);

            var recipeFiles = RecipeFileReader.ReadAllRecipeFiles(pluginFolder);
            var allSpecs = ProcessAllRecipeFiles(recipeFiles);

            if (allSpecs.Count == 0)
            {
                Plugin.Log.LogInfo($"[RecipeLoader] No recipes found.");
            }
            else
            {
                Plugin.Log.LogInfo($"[RecipeLoader] Loaded {allSpecs.Count} recipe(s) from JSON files");
            }

            return allSpecs;
        }

        private static void EnsureRecipesFolderExists(string pluginFolder)
        {
            Plugin.Log.LogInfo($"[RecipeLoader] Checking if recipe folder exists in '{pluginFolder}' and creating if necessary");
            var recipesFolder = RecipeFileReader.GetRecipesFolderPath(pluginFolder);
            Directory.CreateDirectory(recipesFolder);
        }

        private static void EnsureRecipeFilesExist(string pluginFolder)
        {
            var recipesFolder = RecipeFileReader.GetRecipesFolderPath(pluginFolder);

            // Create Examples.json if config allows and file doesn't exist
            if (Plugin.ConfigCreateExamples.Value)
            {
                var examplesPath = Path.Combine(recipesFolder, Constants.EXAMPLES_FILE_NAME);
                if (!File.Exists(examplesPath))
                {
                    var examples = ExampleRecipeDefinitions.GetExampleRecipes();
                    RecipeFileWriter.Write(examples, examplesPath);
                    Plugin.Log.LogInfo($"[RecipeLoader] Created {Constants.EXAMPLES_FILE_NAME}");
                }
            }

            // Create CustomRecipes.json if config allows and file doesn't exist
            if (Plugin.ConfigCreateCustom.Value)
            {
                var customPath = Path.Combine(recipesFolder, Constants.CUSTOM_RECIPES_FILE_NAME);
                if (!File.Exists(customPath))
                {
                    var emptyCollection = new RecipeCollection { Recipes = [] };
                    RecipeFileWriter.Write(emptyCollection, customPath);
                    Plugin.Log.LogInfo($"[RecipeLoader] Created {Constants.CUSTOM_RECIPES_FILE_NAME}");
                }
            }
        }

        private static List<BlueprintSpec> ProcessAllRecipeFiles(
            List<(string fileName, RecipeCollection collection)> recipeFiles)
        {
            List<BlueprintSpec> allSpecs = [];

            foreach (var (fileName, collection) in recipeFiles)
            {
                ProcessRecipeFile(fileName, collection, allSpecs);
            }

            return allSpecs;
        }

        private static void ProcessRecipeFile(
            string fileName,
            RecipeCollection collection,
            List<BlueprintSpec> specs)
        {
            if (collection?.Recipes == null || collection.Recipes.Count == 0)
            {
                Plugin.Log.LogInfo($"[RecipeLoader] '{fileName}' contains no recipes");
                return;
            }

            var successCount = 0;
            var failCount = 0;

            foreach (var jsonSpec in collection.Recipes)
            {
                try
                {
                    RecipeValidator.ValidateRecipe(jsonSpec);
                    var spec = ConvertToBlueprintSpec(jsonSpec);
                    specs.Add(spec);
                    successCount++;
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[RecipeLoader] '{fileName}' - Recipe '{jsonSpec?.Name ?? "Unknown"}' failed: {ex.Message}");
                    failCount++;
                }
            }

            Plugin.Log.LogInfo($"[RecipeLoader] '{fileName}': {successCount} succeeded, {failCount} failed");
        }

        private static BlueprintSpec ConvertToBlueprintSpec(JsonRecipeSpec json)
        {
            var ingredients = json.Ingredients
                .Select(i => (i.Item, i.Quantity))
                .ToArray();

            return new BlueprintSpec
            {
                Id = json.Id,
                ResultItem = json.ResultItem,
                Name = json.Name,
                Ingredients = ingredients,
                Quantity = json.Quantity,
                MenuLists = [.. json.MenuLists],
                Rules = [.. json.Rules],
                Interaction = json.Interaction,
                Category = json.Category,
                Description = json.Description,
                Lore = json.Lore
            };
        }
    }
}
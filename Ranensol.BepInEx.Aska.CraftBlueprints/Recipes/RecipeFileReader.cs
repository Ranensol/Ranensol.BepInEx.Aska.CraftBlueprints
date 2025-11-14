using Ranensol.BepInEx.Aska.CraftBlueprints.Recipes.Models;
using System.Text.Json;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Recipes
{
    internal static class RecipeFileReader
    {
        public static List<(string fileName, RecipeCollection collection)> ReadAllRecipeFiles(string pluginFolder)
        {
            var recipesFolder = GetRecipesFolderPath(pluginFolder);
            var jsonFiles = Directory.GetFiles(recipesFolder, Constants.JSON_FILE_PATTERN);

            Plugin.Log.LogInfo($"[RecipeFileReader] Found {jsonFiles.Length} recipe file(s)");

            List<(string, RecipeCollection)> results = [];

            foreach (var filePath in jsonFiles)
            {
                try
                {
                    var fileName = Path.GetFileName(filePath);
                    var collection = ReadRecipeFile(filePath);
                    results.Add((fileName, collection));
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[RecipeFileReader] Failed to read '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            return results;
        }

        public static string GetRecipesFolderPath(string pluginFolder)
        {
            return Path.Combine(pluginFolder, Constants.RECIPES_FOLDER_NAME);
        }

        private static RecipeCollection ReadRecipeFile(string filePath)
        {
            var jsonContent = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<RecipeCollection>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            }) ?? new RecipeCollection();
        }
    }
}
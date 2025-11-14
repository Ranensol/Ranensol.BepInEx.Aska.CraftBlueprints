using Ranensol.BepInEx.Aska.CraftBlueprints.Recipes.Models;
using System.Text.Json;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.FileIO
{
    internal static class RecipeFileWriter
    {
        public static void Write(RecipeCollection collection, string filePath)
        {
            var json = JsonSerializer.Serialize(collection, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            File.WriteAllText(filePath, json);
        }
    }
}
using System.Text.Json.Serialization;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Recipes.Models
{
    internal class RecipeCollection
    {
        [JsonPropertyName("recipes")]
        public List<JsonRecipeSpec> Recipes { get; set; } = [];
    }
}
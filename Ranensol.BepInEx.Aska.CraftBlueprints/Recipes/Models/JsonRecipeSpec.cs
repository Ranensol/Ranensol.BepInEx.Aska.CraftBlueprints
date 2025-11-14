using System.Text.Json.Serialization;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Recipes.Models
{
    internal class JsonRecipeSpec
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("resultItem")]
        public string ResultItem { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ingredients")]
        public List<JsonIngredient> Ingredients { get; set; } = [];

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("menuLists")]
        public List<string> MenuLists { get; set; } = [];

        [JsonPropertyName("rules")]
        public List<string> Rules { get; set; } = [];

        [JsonPropertyName("interaction")]
        public string Interaction { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("lore")]
        public string Lore { get; set; }
    }
}
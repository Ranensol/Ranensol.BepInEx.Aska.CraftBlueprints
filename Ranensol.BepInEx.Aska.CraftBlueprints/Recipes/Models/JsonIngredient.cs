using System.Text.Json.Serialization;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Recipes.Models
{
    internal class JsonIngredient
    {
        [JsonPropertyName("item")]
        public string Item { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
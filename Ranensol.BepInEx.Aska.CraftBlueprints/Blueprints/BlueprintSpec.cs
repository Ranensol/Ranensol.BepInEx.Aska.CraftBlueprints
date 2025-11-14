namespace Ranensol.BepInEx.Aska.CraftBlueprints.Blueprints
{
    internal struct BlueprintSpec
    {
        public int Id { get; set; }
        public string ResultItem { get; set; }
        public string Name { get; set; }
        public (string, int)[] Ingredients { get; set; }
        public int Quantity { get; set; }
        public string[] MenuLists { get; set; }
        public string[] Rules { get; set; }
        public string Interaction { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Lore { get; set; }
    }
}
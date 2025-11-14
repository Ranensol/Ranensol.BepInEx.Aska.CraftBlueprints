namespace Ranensol.BepInEx.Aska.CraftBlueprints
{
    internal static class Constants
    {
        // Folder names
        public const string RECIPES_FOLDER_NAME = "Recipes";
        public const string REFERENCE_FOLDER_NAME = "Reference";

        // File names
        public const string EXAMPLES_FILE_NAME = "Examples.json";
        public const string CUSTOM_RECIPES_FILE_NAME = "CustomRecipes.json";

        // File patterns
        public const string JSON_FILE_PATTERN = "*.json";

        // Config section names
        public const string CONFIG_SECTION_FILES = "Files";

        // Config keys
        public const string CONFIG_KEY_CREATE_EXAMPLES = "CreateExampleFile";
        public const string CONFIG_KEY_CREATE_CUSTOM = "CreateCustomRecipeFile";
        public const string CONFIG_KEY_DUMP_REFERENCES = "DumpReferenceFiles";

        // Config descriptions
        public const string CONFIG_DESC_CREATE_EXAMPLES = "Whether to create Examples.json on launch if it doesn't exist. Set to false to prevent recreation.";
        public const string CONFIG_DESC_CREATE_CUSTOM = "Whether to create CustomRecipes.json on launch if it doesn't exist. Set to false to prevent recreation.";
        public const string CONFIG_DESC_DUMP_REFERENCES = "Whether to dump game reference files (Items.txt, MenuLists.txt, etc.) on launch. Useful for recipe creation.";
    }
}
using BepInEx.Configuration;

namespace Ranensol.BepInEx.Aska.CraftBlueprints
{
    public static class PluginConfig
    {
        // Existing config entries
        public static ConfigEntry<bool> ConfigCreateExamples { get; private set; }
        public static ConfigEntry<bool> ConfigCreateCustom { get; private set; }
        public static ConfigEntry<bool> ConfigDumpReferences { get; private set; }

        // Filter config entries
        public static ConfigEntry<string> ItemsIncludePrefixes { get; private set; }
        public static ConfigEntry<string> ItemsExcludePrefixes { get; private set; }
        public static ConfigEntry<string> CategoriesIncludePrefixes { get; private set; }
        public static ConfigEntry<string> CategoriesExcludePrefixes { get; private set; }
        public static ConfigEntry<string> MenuListsIncludePrefixes { get; private set; }
        public static ConfigEntry<string> MenuListsExcludePrefixes { get; private set; }
        public static ConfigEntry<string> InteractionsIncludePrefixes { get; private set; }
        public static ConfigEntry<string> InteractionsExcludePrefixes { get; private set; }
        public static ConfigEntry<string> RulesIncludePrefixes { get; private set; }
        public static ConfigEntry<string> RulesExcludePrefixes { get; private set; }

        public static void Initialize(ConfigFile config)
        {
            // Existing config bindings
            ConfigCreateExamples = config.Bind(
                Constants.CONFIG_SECTION_FILES,
                Constants.CONFIG_KEY_CREATE_EXAMPLES,
                true,
                Constants.CONFIG_DESC_CREATE_EXAMPLES
            );

            ConfigCreateCustom = config.Bind(
                Constants.CONFIG_SECTION_FILES,
                Constants.CONFIG_KEY_CREATE_CUSTOM,
                true,
                Constants.CONFIG_DESC_CREATE_CUSTOM
            );

            ConfigDumpReferences = config.Bind(
                Constants.CONFIG_SECTION_FILES,
                Constants.CONFIG_KEY_DUMP_REFERENCES,
                true,
                Constants.CONFIG_DESC_DUMP_REFERENCES
            );

            // Filter config bindings
            ItemsIncludePrefixes = config.Bind(
                "Filters - Items",
                "IncludePrefixes",
                "Item_Arrows_,Item_Axes_,Item_Bait_,Item_Boots_,Item_Bows_,Item_Cape_,Item_Chest_,Item_DrawKnives_,Item_Dyes_,Item_FishingRods_,Item_Gloves_,Item_Hammers_,Item_Hammers_,Item_Hoes_,Item_Iron_IronBloom,Item_Iron_Ore,Item_Junk_,Item_Knives_,Item_Magic_,Item_Materials_,Item_Misc_BoneFragments,Item_Misc_CrawlerSack,Item_Misc_Feathers,Item_Misc_SmolkrHornRaw,Item_OneHanded_,Item_PaintBrushes_ThatchBrush,Item_Pants_,Item_Pickaxes_,Item_Rakes_,Item_Seeds_,Item_Shields_,Item_Shoulders_,Item_Shovels_,Item_Special,Item_Stone_Raw,Item_Stone_SmallRaw,Item_Torches_SimpleTorch,Item_TwoHanded_,Item_WaterSkins,Item_Wood_Bark,Item_Wood_BarkFibres,Item_Wood_Beam,Item_Wood_Coal,Item_Wood_Firewood,Item_Wood_HardWoodLog,Item_Wood_HardWoodLongStick,Item_Wood_Plank,Item_Wood_Post,Item_Wood_RawLog,Item_Wood_RawLongStick,Item_Wood_Resin,Item_Wood_Shaft,Item_Wood_Sticks,Item_Wood_Thatch,Item_YuleTree_",
                "Comma-separated list of prefixes. If set, only items starting with these will appear in reference files. Leave empty to include all."
            );

            ItemsExcludePrefixes = config.Bind(
                "Filters - Items",
                "ExcludePrefixes",
                "Item_Arrows_DraugarArrowRef,Item_Bait_MaggotBP,Item_Boots_BaseBoots,Item_Bows_DraugarBowRef,Item_Chest_BaseChest,Item_Gloves_BaseGloves,Item_Helmets_BaseHelmet,Item_Magic_BrokenCore,Item_Magic_ExplodingSackBP,Item_Magic_Jotun,Item_Magic_RavenMagicBP,Item_OneHanded_1HDraugarBattleAxeRef,Item_OneHanded_1HDraugarBattleHammerRef,Item_OneHanded_1HDraugarSwordRef,Item_Pants_BasePants,Item_Shields_ShieldDraugarRef,Item_Shoulders_BaseShoulders,Item_TwoHanded_2HDraugarBattleAxeRef,Item_TwoHanded_2HDraugarBattleHammerRef,Item_TwoHanded_DraugarGreatswordRef,Item_SpecialBags_,Item_FishingRods_2HWoodFishingRod",
                "Comma-separated list of prefixes. Items starting with these will be excluded from reference files."
            );

            CategoriesIncludePrefixes = config.Bind(
                "Filters - Categories",
                "IncludePrefixes",
                "Categ_Blueprints_",
                "Comma-separated list of prefixes. If set, only categories starting with these will appear in reference files."
            );

            CategoriesExcludePrefixes = config.Bind(
                "Filters - Categories",
                "ExcludePrefixes",
                "",
                "Comma-separated list of prefixes. Categories starting with these will be excluded from reference files."
            );

            MenuListsIncludePrefixes = config.Bind(
                "Filters - MenuLists",
                "IncludePrefixes",
                "ArmorsmithBlueprints,DyerBlueprints,LeatherworkerBlueprints,RavenTotemBlueprints,StartBlueprints,WeaverBlueprints,WorkshopBlueprints",
                "Comma-separated list of prefixes. If set, only menu lists starting with these will appear in reference files."
            );

            MenuListsExcludePrefixes = config.Bind(
                "Filters - MenuLists",
                "ExcludePrefixes",
                "",
                "Comma-separated list of prefixes. Menu lists starting with these will be excluded from reference files."
            );

            InteractionsIncludePrefixes = config.Bind(
                "Filters - Interactions",
                "IncludePrefixes",
                "",
                "Comma-separated list of prefixes. If set, only interactions starting with these will appear in reference files."
            );

            InteractionsExcludePrefixes = config.Bind(
                "Filters - Interactions",
                "ExcludePrefixes",
                "Anvil,Carpent,CraftingTable_,craftInteraction",
                "Comma-separated list of prefixes. Interactions starting with these will be excluded from reference files."
            );

            RulesIncludePrefixes = config.Bind(
                "Filters - Rules",
                "IncludePrefixes",
                "",
                "Comma-separated list of prefixes. If set, only rules starting with these will appear in reference files."
            );

            RulesExcludePrefixes = config.Bind(
                "Filters - Rules",
                "ExcludePrefixes",
                "",
                "Comma-separated list of prefixes. Rules starting with these will be excluded from reference files."
            );
        }
    }
}
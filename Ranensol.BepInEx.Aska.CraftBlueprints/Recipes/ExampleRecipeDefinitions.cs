using Ranensol.BepInEx.Aska.CraftBlueprints.Recipes.Models;

namespace Ranensol.BepInEx.Aska.CraftBlueprints.Recipes
{
    internal static class ExampleRecipeDefinitions
    {
        public static RecipeCollection GetExampleRecipes()
        {
            return new RecipeCollection
            {
                Recipes =
                [
                    // Weaver
                    new JsonRecipeSpec
                    {
                        Id = 900000001,
                        ResultItem = "Item_Materials_Linen",
                        Name = "Linen Cloth",
                        Ingredients =
                        [
                            new JsonIngredient { Item = "Item_Wood_BarkFibres", Quantity = 20 }
                        ],
                        Quantity = 5,
                        MenuLists = ["WeaverBlueprints_T1"],
                        Rules = ["Weaver_Rule"],
                        Interaction = "WeaverInteraction",
                        Category = "Categ_Blueprints_Materials",
                        Description = "Craft 5 Linen cloth",
                        Lore = "Use the loom to make flax fibers bidimensional",
                    },
                    new JsonRecipeSpec
                    {
                        Id = 900000002,
                        ResultItem = "Item_Materials_LinenThread",
                        Name = "Linen Thread",
                        Ingredients =
                        [
                            new JsonIngredient { Item = "Item_Wood_BarkFibres", Quantity = 10 }
                        ],
                        Quantity = 5,
                        MenuLists = ["WeaverBlueprints_T1"],
                        Rules = ["Weaver_Rule"],
                        Interaction = "WeaverInteraction",
                        Category = "Categ_Blueprints_Materials",
                        Description = "Craft 5 Linen thread",
                        Lore = "Use the loom to make flax fibers bidimensional",
                    },
                    // Leatherworker
                    new JsonRecipeSpec
                    {
                        Id = 900000003,
                        ResultItem = "Item_Materials_LeatherCuredHide",
                        Name = "Cured Leather Hide",
                        Ingredients =
                        [
                            new JsonIngredient { Item = "Item_Materials_LeatherScraps", Quantity = 10 },
                            new JsonIngredient { Item = "Item_Materials_LinenThread", Quantity = 2 }
                        ],
                        Quantity = 1,
                        MenuLists = ["LeatherworkerBlueprints_T1"],
                        Rules = ["LeatherWorkerL1_Rule"],
                        Interaction = "LeatherworkerTableInteraction",
                        Category = "Categ_Blueprints_Materials",
                        Description = "Craft a Cured Leather Hide",
                        Lore = "Stitch scraps together at the Leatherworker"
                    },
                    // Workshop
                    new JsonRecipeSpec
                    {
                        Id = 900000004,
                        ResultItem = "Item_Torches_SimpleTorch",
                        Name = "Simple Torch - Fish Oil",
                        Ingredients =
                        [
                            new JsonIngredient { Item = "Item_Wood_Sticks", Quantity = 1 },
                            new JsonIngredient { Item = "Item_Materials_BasicRope", Quantity = 4 },
                            new JsonIngredient { Item = "Item_Food_FishOil", Quantity = 1 }
                        ],
                        Quantity = 1,
                        MenuLists = ["WorkshopBlueprints_T0", "WorkshopBlueprints_T1", "WorkshopBlueprints_T2"],
                        Rules = ["WorkshopL0_Rule", "WorkshopL1_Rule", "WorkshopL2_Rule"],
                        Interaction = "WorkstationInteraction",
                        Category = "Categ_Blueprints_Tools",
                        Description = "Craft a Simple Torch using Fish Oil",
                        Lore = "A portable source of light that smells faintly of fish."
                    },
                    // Player Inventory
                    new JsonRecipeSpec
                    {
                        Id = 900000005,
                        ResultItem = "Item_Bows_SimpleBow",
                        Name = "Simple Bow",
                        Ingredients =
                        [
                            new JsonIngredient { Item = "Item_Wood_Sticks", Quantity = 5 },
                            new JsonIngredient { Item = "Item_Materials_BasicRope", Quantity = 3 },
                            new JsonIngredient { Item = "Item_Misc_BoneFragments", Quantity = 5 }
                        ],
                        Quantity = 1,
                        MenuLists = ["StartBlueprints"],
                        Rules = [],
                        Interaction = "VirtualCraftingStation",
                        Category = "Categ_Blueprints_Weaponry",
                        Description = "Ranged Weapon",
                        Lore = "A simple and reliable bow"
                    },
                ]
            };
        }
    }
}
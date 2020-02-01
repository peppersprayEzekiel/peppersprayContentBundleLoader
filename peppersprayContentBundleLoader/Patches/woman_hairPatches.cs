using HarmonyLib;
using peppersprayContentBundleLoaderPlugin.Bundle;

namespace peppersprayContentBundleLoaderPlugin.Patches
{
    [HarmonyPatch(typeof(woman_hair), "Start")]
    public static class woman_hairPatches
    {
        public static void Prefix(woman_hair __instance)
        {
            // @TODO: make IL patch so that boxes will be added at a correct place
            if (gameMode.mode == "character")
            {
                var sex = __instance.isMale ? ItemSex.Male : ItemSex.Female;
                foreach (var item in ContentServer.Instance.ListItems<HairItem>())
                {
                    if (item.Sex == sex || item.Sex == ItemSex.Both)
                    {
                        __instance.addHairBox(item.NameIndex);
                    }

                    switch (item.Sex)
                    {
                        case ItemSex.Female:
                            woman_hair.femaleColorRules[item.NameIndex] = "3shader";
                            break;
                        case ItemSex.Male:
                            woman_hair.maleColorRules[item.NameIndex] = "3shader";
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
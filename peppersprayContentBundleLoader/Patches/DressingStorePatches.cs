using HarmonyLib;
using peppersprayContentBundleLoaderPlugin.Bundle;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Patches
{
    [HarmonyPatch(typeof(DressingStore), "Start")]
    public class DressingStoreStartPatch
    {
        public static void Prefix(DressingStore __instance)
        {
            // @TODO: make IL patch so that boxes will be added at a correct place
            var sex = GameObject.Find("woman") ? ItemSex.Female : ItemSex.Male;
            foreach (var item in ContentServer.Instance.ListItems<ClothingItem>())
            {
                if (item.Sex == sex || item.Sex == ItemSex.Both)
                {
                    __instance.addStoreBox(item.Category, item.Name, (int) item.Material, item.ColorVar);
                }
            }
        }
    }
}
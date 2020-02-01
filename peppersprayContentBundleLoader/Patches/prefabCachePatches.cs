using System.Collections.Generic;
using HarmonyLib;
using peppersprayContentBundleLoaderPlugin.Bundle;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Patches
{
    [HarmonyPatch(typeof(prefabCache), "Add")]
    public static class prefabCacheAddPatch
    {
        public static bool Prefix(string path, GameObject go)
        {
            if (ContentServer.Instance.ForceReload && ContentServer.Instance.HasItemAt(path))
            {
                prefabCache.state.Remove(path);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
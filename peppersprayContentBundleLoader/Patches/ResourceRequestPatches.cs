using System;
using System.Reflection;
using HarmonyLib;
using peppersprayContentBundleLoaderPlugin.Bundle;
using peppersprayContentBundleLoaderPlugin.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace peppersprayContentBundleLoaderPlugin.Patches
{
    [HarmonyPatch(typeof(ResourceRequest), "get_asset")]
    public static class ResourceRequestPatch
    {
        public static string GetPath(this ResourceRequest self)
        {
            // @TODO: heavy duty, improve performance by somehow avoiding reflection
            return self
                .GetType()
                .GetField("m_Path", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(self) 
                as string;
        }
        
        public static bool Prefix(ResourceRequest __instance, ref Object __result)
        {
            var path = __instance.GetPath();
            if (ContentServer.Instance.HasItemAt(path))
            {
                __result = ContentServer.Instance.RequestItem<Object>(path);
                if (__result == null)
                {
                    Log.Instance.Error("Failed to load asset at {0} - RequestItem returned null!", path);
                }
                return false;
            }
            
            return true;
        }
    }

    [HarmonyPatch(typeof(Resources), "Load", typeof(string))]
    [HarmonyPatch(typeof(Resources), "Load", typeof(string), typeof(Type))]
    public static class ResourcesLoadPatch
    {
        public static bool Prefix(string path, ref Object __result)
        {
            if (ContentServer.Instance.HasItemAt(path))
            {
                __result = ContentServer.Instance.RequestItem<Object>(path);
                if (__result == null)
                {
                    Log.Instance.Error("Failed to load asset at {0} - RequestItem returned null!", path);
                }
                return false;
            }

            return true;
        }
    }
}
using System;
using System.Reflection;
using BepInEx;
using BepInEx.Harmony;
using UnityEngine;
using HarmonyLib;
using peppersprayContentBundleLoaderPlugin.Bundle;
using peppersprayContentBundleLoaderPlugin.Utils;

namespace peppersprayContentBundleLoaderPlugin
{
    [BepInPlugin("net.eze.peppersprayContentBundleLoader", "ClothingBundleLoaderPlugin", "1.0")]
    public class BundleLoaderPlugin: BaseUnityPlugin
    {
        public static string Version = "1.0-alpha1";
        
        private void Awake()
        {
            Logger.LogInfo("Plugin starting");
            Log.Instance = new Log(Logger);
            ContentServer.Instance = new ContentServer();

            Log.Instance.Info("Loading configuration and indexes");
            ContentServer.Instance.LoadConfiguration();
            ContentServer.Instance.LoadBundleIndexes();

            Log.Instance.Info("peppersprayContentBundleLoader items:");
            foreach (var item in ContentServer.Instance.ListItems<ContentItem>())
            {
                Log.Instance.Info(item.Mountpoint);
            }
            
            Log.Instance.Info("Patching game");
            var harmony = HarmonyWrapper.PatchAll();
            harmony.PatchAll();
        }
    }
}
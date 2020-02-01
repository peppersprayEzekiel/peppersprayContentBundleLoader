using HarmonyLib;
using peppersprayContentBundleLoaderPlugin.Utils;
using UnityEngine;
using UnityEngine.UI;
 
namespace peppersprayContentBundleLoaderPlugin.Patches
{
    [HarmonyPatch(typeof(loginGUI), "Awake")]
    public static class loginGUIAwakePatches
    {
        private static GameObject _hyperlink;
        private static GameObject _label;
         
        public static void Prefix(loginGUI __instance)
        {
            var canvas = GameObject.FindObjectOfType<Canvas>();
            _hyperlink = new GameObject("pluginHyperlink");
            _hyperlink.transform.SetParent(canvas.transform);
            
            var hyperlink = _hyperlink.AddComponent<Text>();
            var button = _hyperlink.AddComponent<Button>();
            button.onClick.AddListener(Click);

            hyperlink.text = $"peppersprayContentBundleLoader";
            hyperlink.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            hyperlink.color = Color.cyan;
            hyperlink.rectTransform.anchoredPosition = new Vector2(7, -28);
            hyperlink.rectTransform.sizeDelta = new Vector2(600, 20);
            hyperlink.rectTransform.pivot = new Vector2(0, 1);
            hyperlink.rectTransform.anchorMin = new Vector2(0, 1);
            hyperlink.rectTransform.anchorMax = new Vector2(0, 1);
            
            _label = new GameObject("pluginLabel");
            _label.transform.SetParent(canvas.transform);
            var text = _label.AddComponent<Text>();
            text.text = $"{BundleLoaderPlugin.Version} by Ezekiel_2517";
            text.font = Font.CreateDynamicFontFromOSFont("Arial", 8);
            text.fontSize = 11;
            text.rectTransform.anchoredPosition = new Vector2(228, -30);
            text.rectTransform.sizeDelta = new Vector2(600, 20);
            text.rectTransform.pivot = new Vector2(0, 1);
            text.rectTransform.anchorMin = new Vector2(0, 1);
            text.rectTransform.anchorMax = new Vector2(0, 1);
        }

        public static void Click()
        {
            Application.OpenURL("http://github.com/peppersprayEzekiel/peppersprayContentBundleLoader");
        }
    }
}
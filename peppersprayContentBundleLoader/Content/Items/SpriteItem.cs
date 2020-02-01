using System.Xml;
using peppersprayContentBundleLoaderPlugin.Utils;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Bundle
{
    public class SpriteItem: ContentItem
    {
        public static string[] IncludedTags = new string[]
        {
            "icon",
        };

        public SpriteItem(ContentBundleIndex index, string category, string mountpoint, string assetName) : base(index, category, mountpoint, assetName)
        {
        }
        
        public static SpriteItem Parse(ContentBundleIndex index, XmlElement xmlItem)
        {
            return new SpriteItem(
                index,
                xmlItem.Name,
                xmlItem.GetAttribute("mountpoint"),
                xmlItem.GetAttribute("asset")
            );
        }

        public override T Load<T>()
        {
            return Index.Bundle.LoadAsset<Sprite>(AssetName) as T;
        }
    }
}
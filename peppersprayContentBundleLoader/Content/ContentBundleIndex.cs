using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using peppersprayContentBundleLoaderPlugin.Utils;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Bundle
{

    
    public class ContentBundleIndex
    {
        public readonly string Identifier;
        public readonly List<ContentItem> Items;

        public AssetBundle Bundle
        {
            get
            {
                if (_bundle == null)
                {
                    _bundle = AssetBundle.LoadFromFile(_bundlePath);
                }
                
                return _bundle;
            }
        }

        private readonly string _bundlePath;
        private AssetBundle _bundle;

        public ContentBundleIndex(string identifier, string bundlePath)
        {
            Identifier = identifier;
            Items = new List<ContentItem>();
            
            _bundlePath = bundlePath;
        }

        public void ClearCache()
        {
            _bundle?.Unload(false);
            _bundle = null;
        }

        public static ContentBundleIndex Parse(string identifier, string xmlPath, string bundlePath)
        {
            var document = new XmlDocument();
            document.Load(xmlPath);
            
            var index = new ContentBundleIndex(identifier, bundlePath);
            var xmlIndex = document.ChildNodes[0];
            
            Debug.Assert(xmlIndex != null);
            Debug.Assert(xmlIndex.Name == "index");
            
            foreach (var xmlItemNode in xmlIndex.ChildNodes)
            {
                var xmlItem = xmlItemNode as XmlElement;

                ContentItem item = null;
                // parse clothing items
                if (ClothingItem.IncludedTags.Contains(xmlItem.Name))
                {
                    item = ClothingItem.Parse(index, xmlItem);
                }
                else if (SpriteItem.IncludedTags.Contains(xmlItem.Name))
                {
                    item = SpriteItem.Parse(index, xmlItem);
                }
                else if (HairItem.IncludedTags.Contains(xmlItem.Name))
                {
                    item = HairItem.Parse(index, xmlItem);
                }
                
                // add item to the list
                if (item != null)
                {
                    index.Items.Add(item);
                }
                else
                {
                    Log.Instance.Error("Failed to parse content item with tag {0}", xmlItem.Name);
                }
            }

            return index;
        }
    }
}
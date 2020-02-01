using System;
using System.IO;
using System.Linq;
using System.Xml;
using peppersprayContentBundleLoaderPlugin.Utils;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Bundle
{
    public class HairItem: EquipItem
    {
        public int NameIndex => Int32.Parse(Mountpoint.Split('_').LastOrDefault());
        
        public static string[] IncludedTags = new string[]
        {
            "hair",
        };

        public HairItem(ContentBundleIndex index, string category, string mountpoint, string assetName, ItemSex sex, string[] overrideShaders)
            : base(index, category, mountpoint, assetName, sex, overrideShaders)
        {
        }
        
        public static HairItem Parse(ContentBundleIndex index, XmlElement xmlItem)
        {
            // @TODO: code duplication with ClothingItem
            ItemSex sex;
            switch (xmlItem.GetAttribute("sex"))
            {
                case "f":
                    sex = ItemSex.Female;
                    break;
                case "m":
                    sex = ItemSex.Male;
                    break;
                case "both":
                    sex = ItemSex.Both;
                    break;
                default:
                    Log.Instance.Error("Invalid \"sex\" attribute value");
                    return null;
            }
            
            string[] overrideShaders = new string[0];
            var shader = xmlItem.HasAttribute("overrideShader") ? xmlItem.GetAttribute("overrideShader") : null;
            if (shader != null)
            {
                overrideShaders = shader.Split(';');
            }
            
            return new HairItem(
                index,
                xmlItem.GetAttribute("category"),
                xmlItem.GetAttribute("mountpoint"),
                xmlItem.GetAttribute("asset"),
                sex,
                overrideShaders
            );
        }
    }
}
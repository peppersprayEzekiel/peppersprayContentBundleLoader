using System;
using System.IO;
using System.Linq;
using System.Xml;
using peppersprayContentBundleLoaderPlugin.Utils;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Bundle
{
    public class ClothingItem : EquipItem
    {
        public readonly string ColorVar;
        public readonly uint Material;

        public ClothingItem(ContentBundleIndex index, string category, string mountpoint, string assetName, ItemSex sex, string[] overrideShaders, string colorVar, uint material)
            : base(index, category, mountpoint, assetName, sex, overrideShaders)
        {
            ColorVar = colorVar;
            Material = material;
        }
        
        public static string[] IncludedTags = new string[]
        {
            "clothing",
        };

        public static ClothingItem Parse(ContentBundleIndex index, XmlElement xmlItem)
        {
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

            var material = xmlItem.HasAttribute("material") ? UInt32.Parse(xmlItem.GetAttribute("material")) : 0;
            string[] overrideShaders = new string[0];
            var shader = xmlItem.HasAttribute("overrideShader") ? xmlItem.GetAttribute("overrideShader") : null;
            if (shader != null)
            {
                overrideShaders = shader.Split(';');
            }
            
            return new ClothingItem(
                index,
                xmlItem.GetAttribute("category"),
                xmlItem.GetAttribute("mountpoint"),
                xmlItem.GetAttribute("asset"),
                sex,
                overrideShaders,
                xmlItem.GetAttribute("colorVar"),
                material
            );
        }
    }
}
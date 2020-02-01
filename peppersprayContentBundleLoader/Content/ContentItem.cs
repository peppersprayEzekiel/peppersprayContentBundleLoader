using System.IO;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Bundle
{
    public enum ItemSex
    {
        Male,
        Female,
        Both,
    }
        
    public abstract class ContentItem
    {
        public readonly ContentBundleIndex Index;
        public readonly string Category;
        public readonly string Mountpoint;
        public readonly string AssetName;

        public string Name => Path.GetFileName(Mountpoint);

        public ContentItem(ContentBundleIndex index, string category, string mountpoint, string assetName)
        {
            Index = index;
            Category = category;
            Mountpoint = mountpoint;
            AssetName = assetName;
            
            Debug.Assert(Category != null);
            Debug.Assert(Mountpoint != null);
            Debug.Assert(AssetName != null);
        }

        public abstract T Load<T>() where T: Object;
    }
}
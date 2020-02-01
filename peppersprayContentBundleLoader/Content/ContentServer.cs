using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using peppersprayContentBundleLoaderPlugin.Utils;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Bundle
{
    public class ContentServer
    {
        public static ContentServer Instance;
        public bool ForceReload = true;
        
        private string _basePath = @"peppersprayContentBundles\";
        private string _configFile = @"0configuration.xml";
        private string _indexFileSuffix = @".index.xml";
        private Dictionary<string, ContentItem> _itemMapping = new Dictionary<string, ContentItem>();
        private List<ContentBundleIndex> _bundleIndexes = new List<ContentBundleIndex>();

        public ContentServer()
        {
        }

        public bool HasItemAt(string path)
        {
            // game will occasionally request asset at path Null
            if (path == null)
            {
                return false;
            }
            
            return _itemMapping.ContainsKey(path);
        }

        public T RequestItem<T>(string path) where T: Object
        {
            var item = _itemMapping[path];
            if (item == null)
            {
                Log.Instance.Error("RequestItem {0} failed - bundle index not found", path);
                return null;
            }

            if (ForceReload)
            {
                item.Index.ClearCache();
            }
            
            return item.Load<T>();
        }

        public IEnumerable<T> ListItems<T>() where T : ContentItem
        {
            foreach (var index in _bundleIndexes)
            {
                foreach (var item in index.Items)
                {
                    if (item is T)
                    {
                        yield return item as T;
                    }
                }
            }
        }

        public void LoadBundleIndexes()
        {
            foreach (var indexPath in Directory.GetFiles(_basePath, "*" + _indexFileSuffix))
            {
                // get identifier (bundle name) from path
                var identifier = indexPath.Substring(
                    _basePath.Length,
                    indexPath.Length - _indexFileSuffix.Length - _basePath.Length
                );

                // get asset bundle path
                var bundlePath = Path.Combine(_basePath, identifier);

                // parse bundle index
                var index = ContentBundleIndex.Parse(identifier, indexPath, bundlePath);
                _bundleIndexes.Add(index);
                
                // add items to the mapping dictionary
                foreach (var item in index.Items)
                {
                    if (_itemMapping.ContainsKey(item.Mountpoint))
                    {
                        Log.Instance.Warning("Found duplicate resource at path {0} loading bundle {1}, previous loaded from {2}",
                            item.Mountpoint,
                            identifier,
                            _itemMapping[item.Mountpoint].Index.Identifier
                        );
                    }
                    
                    _itemMapping[item.Mountpoint] = item;
                }
            }
        }

        public void LoadConfiguration()
        {
            var doc = new XmlDocument();
            doc.Load(Path.Combine(_basePath, _configFile));

            var forceReloadXml = doc.GetElementsByTagName("force-reload")[0] as XmlElement;
            ForceReload = forceReloadXml.GetAttribute("enabled") == "true";
        }
    }
}
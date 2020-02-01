using System;
using System.IO;
using System.Linq;
using peppersprayContentBundleLoaderPlugin.Utils;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Bundle
{
    public class EquipItem: ContentItem
    {
        public readonly ItemSex Sex;
        public readonly string[] OverrideShaders;
        
        public EquipItem(ContentBundleIndex index, string category, string mountpoint, string assetName, ItemSex sex, string[] overrideShaders) 
            : base(index, category, mountpoint, assetName)
        {
            Sex = sex;
            OverrideShaders = overrideShaders;
        }

        public override T Load<T>()
        {
            var gameObject = Index.Bundle.LoadAsset<GameObject>(AssetName);
            gameObject = _fix(gameObject, Name);

            return gameObject as T;
        }
        
        private GameObject _fix(GameObject obj, string name)
        {
            var lodHits = 0;
            
            foreach (var renderer in obj.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                // fix bones
                _fixBoneNames(renderer);

                // fix LOD name
                lodHits |= _fixLODName(renderer, name);;
                
                // apply shaders (if needed)
                if (OverrideShaders != null)
                {
                    _applyShaders(renderer);
                }
            }

            if (lodHits != 7)
            {
                Log.Instance.Warning("Asset {0} don't have all of the required LODs, will only display from certain distances", name);
            }

            return obj;
        }

        private void _fixBoneNames(SkinnedMeshRenderer renderer)
        {
            // remove root bone - game requirement
            renderer.rootBone = null;

            // fix bone names after blender export
            foreach (var bone in renderer.bones)
            {
                bone.name = bone.name.Replace('_', ' ');
            }
        }

        private int _fixLODName(SkinnedMeshRenderer renderer, string name)
        {
            if (!renderer.name.Contains("_LOD"))
            {
                // first, closest renderer
                renderer.name = name;
                return 1;
            }
            else
            {
                // subsequent _LOD renderers
                var components = renderer.name.Split('_');
                renderer.name = name + "_" + components.Last();
                
                if (!Int32.TryParse(renderer.name.Last().ToString(), out var index))
                {
                    return 0;
                }
                else
                {
                    return 1 << index;
                }
            }
        }

        private void _applyShaders(SkinnedMeshRenderer renderer)
        {
            for (int i = 0; i < OverrideShaders.Length; i++)
            {
                if (renderer.materials.Length <= i)
                {
                    Log.Instance.Warning(
                        "Failed to apply overriden shader #{0} - renderer {1} only has {2} materials!",
                        i,
                        renderer.name,
                        renderer.materials.Length
                    );
                    break;
                }

                if (OverrideShaders[i] != "skip")
                {
                    renderer.materials[i].shader = Shader.Find(OverrideShaders[i]);
                }
            }
        }
    }
}
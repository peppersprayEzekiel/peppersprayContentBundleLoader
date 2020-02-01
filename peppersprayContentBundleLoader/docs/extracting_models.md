## Prerequisites
* **Blender**
* Latest-ish version of **Unity Editor** (one which plays nicely with broken prefabs)
* **FBX Exporter** Unity Package (go to `Window -> Package Manager`, tick `Show preview packages` in `Advanced` and install `FBX Exporter`)

## Ripping the assets
First step is to extract `.asset` from the game resources. Use [uTinyRipper](https://github.com/mafaca/UtinyRipper),
do to that. Drag and drop `3DXChat_Data/resources.assets` and extract it. You will end up with following folder structure:
* `Assets`
    * `Material` - materials
    * `Mesh` - meshes
    * `PrefabInstances` - prefabs (armatures stored with prefabs)
    * `Texture2D` - textures
    
## Converting assets to `.fbx`
### 1. Import required assets for the item you want to convert
You have to figure out the names. Clothing items go by their respective names from `DressingStore`, for example.
Most of the items have similar names across categories (i.e. mesh is `skirt_3`, textures are `skirt_3_x` and prefab is `skirt_3` as well).

1. Import, in following order: **mesh, textures, materials, prefabs**
1. Material shader will be missing, select another shader and fix textures if they're missing

#### Troubleshooting
> Can't find materials/meshes/textures

The files may be under different names, therefore you need to locate them by their metadata identifiers.
In order to:
* Find texture - use *Material* file (`.mat`)
* Find material - use open *Prefab* file (`.prefab`)

Loader release comes with *guidAssetNameLookup* tool, which can be used to find the exact files. You use it from a command 
line like this: `guidAssetNameLookup PrefabInstance/dress_14.prefab`, it will scan the file for linked file references,
and then find paths to that references in the current directory.

> Unity Editor crashes once I open prefab

Try to import more of the assets that you suspect may be included (or even introspect the prefab looking for those). 
Some prefabs may still not load even after that, so either you're up to manual `.prefab` structure editing or simply out of luck.

### 2. Export `.fbx` from Unity Editor
1. Open prefab, right-click on the root object and select `Export to FBX`
1. Change `Export Format` to `Binary` (Blender doesn't work with `ASCII`) and extract it
1. Open *Blender*, remove default objects from the scene, and import `.fbx`

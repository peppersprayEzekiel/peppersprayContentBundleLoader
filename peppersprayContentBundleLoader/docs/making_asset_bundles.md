## Prerequisites
* **Unity Editor v. 5.5.5** (exact same version that the game uses, download from [Unity Download Archives](https://unity3d.com/get-unity/download/archive))
* **CompileBundles** editor script from this repository (create folder `Editor` and import script there)

## 1. Create Unity project and import your `.fbx`
Using **Unity Editor v. 5.5.5** create an empty 3D project, and then drag-and-drop your `.fbx` file there.

## 2. Install and configure CompileBundles script
In order to install the script create `Editor` folder under the `Assets`, and import the `CompileBundle.cs` there.

It's a good idea to edit `Compilebundles.cs` script a little bit so it would output the bundles in the scanned folder 
from the get-go, reducing deploy time. If your plugin is configured with caching disabled that will mean that after 
compilation you will only need to switch to the game and click on the item in the editor to see the results.
You can do that by pointing `assetBundleDirectory` on the **9** line to your game's `peppersprayContentBundles` folder.

## 3. Fix your prefab
Usually there will be something wrong with prefab after import - missing textures or misconfigured materials. 
Fix those (export textures from Blender and import them back, updating materials). Your model should sit upwards straight 
on the bones and morph when those bones are moved in the editor.

Unity Editor 5 have rather limited abilities when it comes to the prefab editing, sometimes you will have to make a 
copy in order to update prefab object structure. Don't worry about having multiple prefabs with the same name, since
you can only mark one of them to go into the asset bundle.

### Clothing/hair item requirements
* Asset must be a *Prefab*
* Prefab must contain imported armature with names like `woman_bone_xxxx` or `woman_bip_xxxx`
    * these names must match with data exported from the original game files
    * armature may or may not start at the root of the prefab, it shouldn't matter
* Prefab must contain three `SkinnedMeshRenderers`, named `name`, `name_LOD1` and `name_LOD2`
    * the important bit is `_LODX`. `name` doesn't matter
    * **Bounds** for the renderers should go as follows: *Center*: `0, 0, 0`, *Extents*: `1, 1, 1`
        
### Icon item requirements
* Asset must be a Texture with type `Sprite (2D and UI)`
* Asset must have `Sprite Mode` set to `Single`

### Using shaders
You can use shaders which you provide in the prefab, or ones from Unity standard set. In order to use one from the game
itself rather than ripping it and re-packaging it in the prefab you can use `overrideShader` in the *index* file of the
content bundle to swap those at runtime (will be covered in the next section of the guide).

## 4. Assign item to the asset bundle
Select your item (in case of *prefab* you only need to select prefab itself, dependencies will be automatically included),
move to the right onto the preview area, locate the *AssetBundle* label to the bottom-left of the area and click on `None`,
then on `New...`, enter the name of your bundle (this will be the name of the file) and hit Enter.

Asset bundles can and usually should contain a more than one item (for example in order to add clothing item you should,
at the very minimum, have two - item prefab itself and its icon for the editor). You will use separate file to describe
which individual asset goes where.

## 5. Compile asset bundles
After everything has been done compile the bundles using `Assets -> Build Asset Bundles`. Verify that bundle file 
(`NAME`) is in the target directory. Other files (`.manifest` and `peppersprayContentBundles.*`) don't matter and can be omitted.

Upon recompilation if progress bar appears only for a fraction of a second that usually means that Unity didn't detect 
changes in the included assets. Verify that you did apply your changes to the prefab.

# Overview

*Content bundle* is a file that contains content (clothing, hair, icons, etc) for the game. 
You compile/make the required files, place them at the correct folder and 
upon next relaunch they will appear in the game.

## File structure
Each *Content bundle* consist of:
* Unity's `Asset Bundle`
* Index file named `NAME.index.xml`

Bundle files are located at `.peppersprayContentBundles\`.

## General workflow
1. Create `fbx` rigged models for the items
1. Create unity project and import models
1. Include desired prefabs into *Asset bundle*
1. Compile asset bundle and place it in the plugin directory
1. Describe each item properties by writing `NAME.index.xml`
1. Load the game and verify results

### 1. Blender Models
In order to start working on a model a good idea would be to start on the existing one 
(at the very least inherit its armature). 

[Extracting models](https://github.com/peppersprayEzekiel/peppersprayContentBundleLoader/blob/master/peppersprayContentBundleLoader/docs/extracting_models.md) 
covers how to import existing game model into the Blender.

### 2. Unity Asset Bundle
Asset bundles are compiled using respective Unity Editor version (`5.5.5` in our case), by 
importing assets from blender, fixing any problems with them, assigning results to the
*Asset bundle* and compiling those.

[Making Asset Bundles](https://github.com/peppersprayEzekiel/peppersprayContentBundleLoader/blob/master/peppersprayContentBundleLoader/docs/making_asset_bundles.md) 
covers how to make Asset bundle.

### 3. Content Bundle Index
The last thing is to cover how to use those assets in the game. `NAME.index.xml` describes which
asset are assigned to which category, individual asset properties and so on.

[Writing Content Bundle Index](https://github.com/peppersprayEzekiel/peppersprayContentBundleLoader/blob/master/peppersprayContentBundleLoader/docs/writing_content_bundle_index.md)
covers how to write Bundle index.

### 4. Viewing added content in game
Plugin will load the assets automatically, although it may require some configuration files
tweaking to troubleshoot it.

[Plugin Configuration](https://github.com/peppersprayEzekiel/peppersprayContentBundleLoader/blob/master/peppersprayContentBundleLoader/docs/plugin_configuration.md) 
covers how to enable debug output and other plugin options. 

## Example project
Download [Unity project](https://drive.google.com/file/d/14ugQMBX_GjGL2mboTD67tCPRBoN59F3u/view?usp=sharing)
which compiles content bundle with various clothing examples from the game.

# 1. How to compile plugin
In order to compile plugin you will need to add following `dll`s as references:
* BepInEx dlls:
    * **0Harmony.dll**
    * **BepInEx.dll**
    * **BepInEx.Harmony.dll**
* Unity dlls:
    * **UnityEngine.dll** (from version 5.5; best to take from game distribution itself)
    * **UnityEngine.UI.dll** (from version 5.5; best to take from game distribution itself)
* Game dlls:
    * **Assembly-UnityScript.dll** (from game distribution)
    
After that you should be able to build the library. You put it in the `.\BepInEx\plugins\` 
folder in order for it to be injected on the startup.
Dont forget that you will also need `BepInEx` installed on top of the game (it comes with
plugin release distribution, but still).

# 2. How to compile guidAssetNameLookup tool
Get the `YamlDotNet` package from the *nuget* and you should be ready to go.

# 2. Supporting documentation
In order to understand the code you can refer to 
[supporting UMLs](https://github.com/peppersprayEzekiel/peppersprayContentBundleLoader/tree/master/peppersprayContentBundleLoader/docs/uml)

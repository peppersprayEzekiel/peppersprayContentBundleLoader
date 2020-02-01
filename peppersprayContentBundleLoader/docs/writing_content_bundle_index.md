# Overview 
Each *content bundle* should go with `NAME.index.xml` file, which would specify each individual items that 
are included in the asset bundle.

## 1. File name location
File should be located in the same directory as the asset bundles, specifically in `.\peppersprayContentBundles\`. Its 
`NAME` portion should exactly match the asset bundle name it describes.

For example, if you previously compiled bundle called `clothingbundle1`, index file should be named `clothingbundle1.index.xml`, 
and should sit right beside asset bundle.

## 2. File structure
XML should contain a single tag at the top, named `index`. `<index>` will contain records for each individual items.

### General attributes for items
Each item, regardless of its type will have two required attributes - `mountpoint` and `asset`.
* `mountpoint` is internal game path to the resource that is being added. It should be unique and should not overlap
with existing assets in the game. Each item type will have specific root folder, taken from the game. Game will construct 
those paths based on its own internal logic, meaning that there are strict requirements to what paths should look like.
* `asset` is a name of the asset in *asset bundle* that you've compiled. It's not required for it to share 
a name with `mountpoint`. It's there so that the loader will know which specific asset to pull from the asset bundle.
Assets don't have the extension, in fact multiple assets can have the same name. Based on the item type an asset with
correct type will be automatically loaded from the bundle.

### Icon items
Icon items should go as `<icon>` tag. They don't have any specific attributes, except for the standard ones:
* **required** `mountpoint` should look like: `Icons/CATEGORY/NAME`, `NAME` is your filename from `mountpoint` and `CATEGORY`
will differ based on the item you want to represent with this icon (check with game code). See *General attributes for items**.
* **required** `asset` - see *General attributes for items**

### Clothing items
Clothing item should go as `<clothing>` tag. Attributes go as follows:
* **required** `mountpoint` should look like: `SEX/cloth/NAME`, where `SEX` is either `Man` or `Woman` and `NAME` is an internal name 
of the item (usually its something like `skirt_7` or `dress_13`). See *General attributes for items**.
* **required** `asset` - see *General attributes for items**
* **required** `category` - internal game identifier of category. Consult with code from `DressingStore` to figure out the names.
* **required** `colorVar` - set of the default colors. Consult with code from `DressingStore` to figure out the names.
* **required** `sex` - either `f` or `m`
* **optional** `overrideShader` name of the shaders to be used on the items. See section *overrideShader attributes*.

### Hair items
Hair items use `<hair>` tag. Attributes are:
* **required** `mountpoint` should look like: `SEX/hair/NAME`, where `SEX` is either `Man` or `Woman` and `NAME` is an 
internal name of the item (usually its something like `skirt_7` or `dress_13`). See *General attributes for items**.
* **required** `asset` - see *General attributes for items**
* **required** `sex` - either `f` or `m`
* **optional** `overrideShader` name of the shaders to be used on the items. See section *overrideShader attributes*.

### `overrideShader` attribute
Use this to use one of the game shaders instead of what you have on the prefab. 
There could be multiple shaders - separate those with a colon (`;`) - they will be applied 
to respective materials (by index). If you only want to override certain shader in sequence use keyword `skip` 
to skip indexes until you get to desired one.

## 3. Example
```
<index>
    <clothing 
            mountpoint="Woman/cloth/skirt_7"
            asset="skirt_7"
            category="botwear"
            colorVar="leather"
            sex="f"
            overrideShader="3DX_Shaders/CLOTHES/1Color Metalness Emission" />

    <icon mountpoint="Icons/woman_cloth/skirt_7" asset="skirt_7" />

    <clothing mountpoint="Woman/cloth/dress_13"
            asset="dress_13"
            category="dress"
            colorVar="wool"
            sex="f"
            overrideShader="3DX_Shaders/CLOTHES/1Color Metalness Emission" />
            
    <icon mountpoint="Icons/woman_cloth/dress_13" asset="dress_13" />
</index>
```
## 1. Injector configuration
In order to see plugin debug messages you need to enable console in the `BepInEx` 
configuration file. Open `.\BepInEx\config\BepInEx.cfg` with a text editor and
set property in `Logging.Console` category named `Enabled` to `true`.

Now, after you launch the game an additional window will appear which will contain both
plugin and Unity output. Look out for red and yellow messages from the plugin.

## 2. Plugin configuration
`.\peppersprayContentBundles\0configuration.xml` will contain plugin settings.
For now there's only one settin - `force-reload`. If you set it to `true` both game
and plugin will not cache your items, meaning that in order to reload those you will not
need to restart the game - simply clicking on the item in the editor will reload item from
the disk, meaning that you can compile the asset, go straight back to the game and after 
clicking on the item the changes will be immediately reflected.
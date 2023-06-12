# TOTKActorRepacker
A tool for quickly batch editing values in TotK actorpack bymls

![](https://i.imgur.com/FRwtWrn.gif)

# What's This For?
Zelda: Tears of the Kingdom for Switch uses a YAML format compiled to binary called ``.byml``.  
Many of them can be found in the game's ``Pack/Actor`` directory and hold values for a lot of important game object variables, so editing them is pretty useful for modding.  
There's already tools that can edit them such as [Switch Toolbox](https://github.com/KillzXGaming/Switch-Toolbox/releases) and [NX-Editor](https://github.com/NX-Editor/NX-Editor). But it can still cost you a lot of time to tweak values, especially over multiple files.  
An example of this is my [Better Sages mod](https://gamebanana.com/mods/443839), where each sage (there's 4 of them, technically 5) has duplicate files split over 3 different ``.pack``s.  
These ``.pack`` files (also called ``SARC``) are compressed with ZSTD compression that requires [a special tool](https://github.com/TotkMods/Totk.ZStdTool/releases) to un/recompress. 
That's where this tool comes in-- it's meant to take all the manual fuss out of the process of changing known values in known ``.pack.zs`` files.  
It also auto-patches the ResourceTable with newly padded sizes to prevent game crashes, another thing that usually requires [a specialized tool](https://gamebanana.com/tools/13409).  
You can repurpose this tool for quickly testing out different sets of values for your own mods, or create ``.json``s to make your mods customizable, or even make your mods compatible with other mods through dependencies.

# Usage 
## Applying Changes From Json
1. When the program boots, it looks for a default ``.json`` to load that has a list of all the changes to apply. By default, it comes with my [Better Sages Mod](https://gamebanana.com/mods/443839) tweaks.  
You can change the path to point to a different ``.json`` in the settings (or edit formsettings.json).  
2. Next, the user can toggle on or off individual changes. You can mouse over a change to see its description. You can also change its value. When it's disabled, it shows the default value.
3. Make sure the game version in the Settings matches your installed game version. You also need to have the ResourceTable file matching that version in ``./OG/System/Resource``. The ones for 1.0.0 through 1.1.2 are already included.  
4. Press the ``Generate Mod`` button, and your changes should be applied. New files will appear in the ``./Output`` folder.
5. If you experience any crashes with the game, try increasing the padding value in Settings. Or keep reading below.

## Comparing Mods and Merging Changes
1. If you're using a mod that already patches the ResourceTable, replace the ResourceTable file in ``./OG/System/Resource/`` with your prepatched one.  
2. If you want to merge mods that edit the same files you're editing, make sure the original, clean files from the game dump are in your ``./OG`` folder.  
3. Click on ``File > Compare Mod Files...`` and choose the root directory of the mod (usually the romfs folder that contains Pack, System, Model, Sound etc. folders).  
4. A folder should be generated next to the program named ``Compare_NameOfTheModFolderYouCompared``. Name it whatever you want and move it into the ``./Dependencies`` folder.
5. When you click the ``Generate Mod`` button, the contents of these folders will automatically be added to your newly generated ``.pack`` files.
6. To change the order that they're applied, rename the files (they go in numeric and alphabetical order like how they show up in Windows Explorer).
7. If you need to merge multiple mods that change the same ``.bgyml`` files within the ``.pack``, read on below.

## Creating Your Own Json
1. Use Switch Toolbox or a similar tool (or view the mod comparison output) to make note of the name of the ``.pack``, the path to the ``.bgyml`` file within it that you want to edit, and the name of the field you're editing.
2. See how I set up my ``bettersagesmod.json`` for reference. Make a new ``.json`` following the same conventions, set it as the default in Settings and reload the program.   

### Some Things to Note
- The "OGValue" field is the default value applied when an option is disabled, and "Value" is the default value when it's enabled (overridden by user entry in the form).
- Paths to ``.pack`` are treated as relative to the game's ``Actor/Path`` folder. Paths to ``.bgyml`` are expected to end in ``.yml`` instead.
- Field names ending with ``:`` will replace the next value following the colon with the entered value while preserving the rest of the YML line.
- Field names not ending with ``:`` will replace the whole line with simply ``FieldName: Value``. When in doubt, use the former.

## Limitations
- Unfortunately, YML fields with the same name all get replaced with the same value. This can be a problem for dictionaries if you want each set of values to be different. In the future I may address this.  
- For numerical values, it's wise to end the value with a decimal point and a zero (i.e. ``1.0`` instead of ``1``) so that when converted back to ``.byml``, the data is formatted as a number rather than a string. I may also eventually handle this automatically as well.

# License
This project is licensed under the [AGPL v3 License](https://github.com/ShrineFox/TOTKActorRepacker/blob/main/LICENSE).

# Credits
- ZSTD Compression/Decompression code from [TotkMods/Totk.ZStdTool](https://github.com/TotkMods/Totk.ZStdTool)
- Cead Library from [TotkMods/Cead](https://github.com/TotkMods/Cead) (SARC/BYML editing)
- [ArchLeaders](https://github.com/ArchLeaders) for creating the above tools and generally being very helpful
- [moonling](https://gamebanana.com/members/1698465), creator of [Yet Another Better Sages Mod](https://gamebanana.com/mods/445290) for included sample dependency

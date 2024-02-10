# MarrowUtils
Adds various helpful utilities.

## Utilities
* Emergency Relocation - Teleports you back to world spawn
* Level Reloader - Reloads the level when you hit Control + R
* Random Stuff - Spawns random NPCs, guns, melees, or takes you to a random level.
* The Janitor - Garry's Mod cleanup menu basically.
* Warehouse Scanner - Point it at something and it tells you the barcode of it.
* Unredacted - Unhides any redacted spawnables/avatars/levels.

## Contribution
* Utility creation is very simple.
* Make your class derive from Utility, and that's it.
* You get a couple base methods that get called.
* Feel free to add more hooks for base methods as needed. Originally I didn't have OnWarehouseInit until I needed it for Unredacted.
### Base Methods
> **Start()** - Equivalent to a Monobehaviour's Start. Gets called once the Utility is loaded, which is shortly after the mod itself gets loaded.

> **Update()** - Equivalent to a Monobehaviour's Update. Called every frame.

> **FixedUpdate()** - Same as a Monobehaviour's FixedUpdate.

> **LateUpdate()** - Same as a Monobehaviour's LateUpdate.

> **CreateMenu()** - Also like Start. Used for Bonemenu setup.

> **OnLevelLoad(LevelInfo levelInfo)** - Called when a level is loaded. Provides Bonelib's LevelInfo class as a variable so you can access information about the loaded level easily.

> **OnLevelUnload()** - Called when a level is unloaded.

> **OnWarehouseInit(List<Pallet> loadedPalletList)** - Called when Warehouse is finished loading. Provides a list of every pallet the user has installed, this includes the basegame pallets.

### Example Script
```cs
using Utils = UnityEngine.Diagnostics.Utils;

namespace MarrowUtils.Utilities.ExampleClass;

internal class ExampleClass : Utility
{
    // Called shortly after OnInitializeMelon
    protected override void Start()
    {
        ModConsole.Msg("Loading ExampleClass...", 1);
    }

    // Called once Warehouse is ready
    protected override void OnWarehouseInit(PalletList loadedPalletList)
    {
        foreach (var pallet in loadedPalletList)
        {
            if (pallet.Barcode == "NotEnoughPhotons.Paranoia")
            {
                // stuff happens if person has Paranoia's pallet
            }
        }
    }

    // Called when a level is loaded
    protected override void OnLevelLoad(LevelInfo levelInfo)
    {
        ModConsole.Msg($"Level loaded: {levelInfo.title});
    }

    // Called when a level is unloaded
    protected override void OnLevelUnload()
    {
        ModConsole.Msg("Level unloaded");
    }

    // Called at the same time as Start, seperated for cleanliness.
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreateSubPanel("Example Class", Color.magenta);
        // dont actually do this lol
        menu.CreateBoolPreference("Crash Game", Color.white, CrashGame);
    }

    // Example Class' own original method, not provided by Utility
    private static void CrashGame()
    {
        Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
    }
}```

using LabFusion.Network;
using static UnityEngine.Random;

namespace MarrowUtils.Utilities.WeaponRandomizer;

internal class WeaponRandomizer : Utility
{
    private static bool _fusionInstalled;

    private static readonly List<Crate> Rifles = new();
    private static readonly List<Crate> Pistols = new();
    private static readonly List<Crate> BluntMelee = new();
    private static readonly List<Crate> BladeMelee = new();
    private static readonly List<Crate> Smgs = new();
    private static readonly List<Crate> Shotguns = new();
    
    protected override void Start()
    {
        ModConsole.Msg("Loading WeaponRandomizer...", 1);
        _fusionInstalled = HelperMethods.CheckIfAssemblyLoaded("labfusion");
    }

    protected override void OnWarehouseInit(CrateList crates)
    {
        foreach (var crate in crates)
        {
            if (!crate.Tags.Contains("Weapon")) return;
            if (crate.Tags.Contains("Rifle"))
            {
                Rifles.Add(crate);
            }
            else if (crate.Tags.Contains("Pistol"))
            {
                Pistols.Add(crate);
            }
            else if (crate.Tags.Contains("Blunt"))
            {
                BluntMelee.Add(crate);
            }
            else if (crate.Tags.Contains("Blade"))
            {
                BladeMelee.Add(crate);
            }
            else if (crate.Tags.Contains("SMG"))
            {
                Smgs.Add(crate);
            }
            else if (crate.Tags.Contains("Shotgun"))
            {
                Shotguns.Add(crate);
            }
        }
    }

    private static Barcode GetRandomWeapon(TagList tags)
    {
        if (tags.Contains("Rifle"))
        {
            return Rifles[Range(0, Rifles.Count)].Barcode;
        }
        if (tags.Contains("Pistol"))
        {
            return Pistols[Range(0, Pistols.Count)].Barcode;
        }
        if (tags.Contains("Blunt"))
        {
            return BluntMelee[Range(0, BluntMelee.Count)].Barcode;
        }
        if (tags.Contains("Blade"))
        {
            return BladeMelee[Range(0, BladeMelee.Count)].Barcode;
        }
        if (tags.Contains("SMG"))
        {
            return Smgs[Range(0, Smgs.Count)].Barcode;
        }
        if (tags.Contains("Shotgun"))
        {
            return Shotguns[Range(0, Shotguns.Count)].Barcode;
        }
        return null;
    }
    
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreateSubPanel("Weapon Randomizer", Color.green);
        menu.CreateBoolPreference("Enabled", Color.white, Preferences.RandomizeWeapons, Preferences.RandomizerCategory);
    }

    private static bool InFusionLobby()
    {
        if (!_fusionInstalled) return false;
        return NetworkInfo.IsClient || NetworkInfo.IsServer;
    }

    protected override void OnSpawnablePlaced(AssetPoolee poolee)
    {
        if (CantRun()) return;
        
        var crate = GetRandomWeapon(poolee.spawnableCrate.Tags);
        if (crate == null) return;
        
        var transform = poolee.transform;
        var position = transform.position;
        var rotation = transform.rotation;
        
        poolee.Despawn();
        HelperMethods.SpawnCrate(crate, position, rotation, Vector3.one, false, _ => {});
        return;

        bool CantRun()
        {
            if (!Preferences.RandomizeWeapons.Value) return true;
            if (InFusionLobby()) return true;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!poolee.spawnableCrate.Tags.Contains("Weapon")) return true;
            return false;
        }
    }
}
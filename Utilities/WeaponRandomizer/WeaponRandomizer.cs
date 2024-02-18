using HarmonyLib;
using LabFusion.Network;

namespace MarrowUtils.Utilities.WeaponRandomizer;

internal class WeaponRandomizer : Utility
{
    private static bool _fusionInstalled;

    private static readonly List<Crate> _rifles = new();
    private static readonly List<Crate> _pistols = new();
    private static readonly List<Crate> _bluntMelee = new();
    private static readonly List<Crate> _bladeMelee = new();
    private static readonly List<Crate> _smgs = new();
    private static readonly List<Crate> _shotguns = new();
    
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
                _rifles.Add(crate);
            }
            else if (crate.Tags.Contains("Pistol"))
            {
                _pistols.Add(crate);
            }
            else if (crate.Tags.Contains("Blunt"))
            {
                _bluntMelee.Add(crate);
            }
            else if (crate.Tags.Contains("Blade"))
            {
                _bladeMelee.Add(crate);
            }
            else if (crate.Tags.Contains("SMG"))
            {
                _smgs.Add(crate);
            }
            else if (crate.Tags.Contains("Shotgun"))
            {
                _shotguns.Add(crate);
            }
        }
    }

    private static Crate GetRandomWeapon(Il2CppSystem.Collections.Generic.List<string> tags)
    {
        if (tags.Contains("Rifle"))
        {
            return _rifles[UnityEngine.Random.Range(0, _rifles.Count)];
        }
        if (tags.Contains("Pistol"))
        {
            return _pistols[UnityEngine.Random.Range(0, _pistols.Count)];
        }
        if (tags.Contains("Blunt"))
        {
            return _bluntMelee[UnityEngine.Random.Range(0, _bluntMelee.Count)];
        }
        if (tags.Contains("Blade"))
        {
            return _bladeMelee[UnityEngine.Random.Range(0, _bladeMelee.Count)];
        }
        if (tags.Contains("SMG"))
        {
            return _smgs[UnityEngine.Random.Range(0, _smgs.Count)];
        }
        if (tags.Contains("Shotgun"))
        {
            return _shotguns[UnityEngine.Random.Range(0, _shotguns.Count)];
        }
        return null;
    }

    private static bool InFusionLobby()
    {
        if (!_fusionInstalled) return false;
        return NetworkInfo.IsClient || NetworkInfo.IsServer;
    }
    
    [HarmonyPatch(typeof(SpawnableCratePlacer), "Awake")]
    private static class CratePlacerPatch
    {
        // ReSharper disable once InconsistentNaming, harmony will fuck up if its not specifcally __instance
        [HarmonyPrefix]
        private static void Prefix(SpawnableCratePlacer __instance)
        {
            if (InFusionLobby()) return;
            if (!Preferences.RandomizeWeapons.Value) return;
            if (!__instance.spawnableCrateReference.Crate.Tags.Contains("Weapon")) return;
            var crate = GetRandomWeapon(__instance.spawnableCrateReference.Crate.Tags);
            if (crate == null) return;
            var crateRef = new SpawnableCrateReference(crate.Barcode);
            __instance.spawnableCrateReference = crateRef;
        }
    }
}
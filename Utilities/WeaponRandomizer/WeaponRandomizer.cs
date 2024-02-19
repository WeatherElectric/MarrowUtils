﻿using LabFusion.Network;
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

    private static Crate GetRandomWeapon(Il2CppSystem.Collections.Generic.List<string> tags)
    {
        if (tags.Contains("Rifle"))
        {
            return Rifles[Range(0, Rifles.Count)];
        }
        if (tags.Contains("Pistol"))
        {
            return Pistols[Range(0, Pistols.Count)];
        }
        if (tags.Contains("Blunt"))
        {
            return BluntMelee[Range(0, BluntMelee.Count)];
        }
        if (tags.Contains("Blade"))
        {
            return BladeMelee[Range(0, BladeMelee.Count)];
        }
        if (tags.Contains("SMG"))
        {
            return Smgs[Range(0, Smgs.Count)];
        }
        if (tags.Contains("Shotgun"))
        {
            return Shotguns[Range(0, Shotguns.Count)];
        }
        return null;
    }
    
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreateSubPanel("Weapon Randomizer", Color.green);
        menu.CreateBoolPreference("Enable Randomization", Color.white, Preferences.RandomizeWeapons, Preferences.RandomizerCategory);
    }

    private static bool InFusionLobby()
    {
        if (!_fusionInstalled) return false;
        return NetworkInfo.IsClient || NetworkInfo.IsServer;
    }
    
    [HarmonyPatch(typeof(SpawnableCratePlacer))]
    private static class CrateStart
    {
        // ReSharper disable once InconsistentNaming, harmony will fuck up if its not specifcally __instance
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SpawnableCratePlacer.PlaceSpawnable))]
        private static void Prefix(SpawnableCratePlacer __instance)
        {
            if (InFusionLobby()) return;
            if (!Preferences.RandomizeWeapons.Value) return;
            if (!__instance.spawnableCrateReference.Crate.Tags.Contains("Weapon")) return;
            var crate = GetRandomWeapon(__instance.spawnableCrateReference.Crate.Tags);
            if (crate == null) return;
            var crateRef = new SpawnableCrateReference(crate.Barcode);
            __instance.spawnable.crateRef = crateRef;
        }
    }
    
    [HarmonyPatch(typeof(SpawnableCratePlacer))]
    private static class CratePlaced
    {
        // ReSharper disable once InconsistentNaming, harmony will fuck up if its not specifcally __instance
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SpawnableCratePlacer.RePlaceSpawnable))]
        private static void Prefix(SpawnableCratePlacer __instance)
        {
            if (InFusionLobby()) return;
            if (!Preferences.RandomizeWeapons.Value) return;
            if (!__instance.spawnableCrateReference.Crate.Tags.Contains("Weapon")) return;
            var crate = GetRandomWeapon(__instance.spawnableCrateReference.Crate.Tags);
            if (crate == null) return;
            var crateRef = new SpawnableCrateReference(crate.Barcode);
            __instance.spawnable.crateRef = crateRef;
        }
    }
}
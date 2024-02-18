// ReSharper disable MemberCanBePrivate.Global, these categories may be used outside of this namespace to create bonemenu options.

namespace MarrowUtils.Melon;

internal static class Preferences
{
    public static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");
    public static readonly MelonPreferences_Category JanitorCategory = MelonPreferences.CreateCategory("TheJanitor");
    public static readonly MelonPreferences_Category ReloaderCategory = MelonPreferences.CreateCategory("LevelReloader");
    public static readonly MelonPreferences_Category UnredactedCategory = MelonPreferences.CreateCategory("Unredacted");
    public static readonly MelonPreferences_Category RandomizerCategory = MelonPreferences.CreateCategory("WeaponRandomizer");

    public static MelonPreferences_Entry<int> LoggingMode { get; set; }
    public static MelonPreferences_Entry<bool> OverrideFusionCheck { get; set; }
    public static MelonPreferences_Entry<bool> EnableReloader { get; set; }
    public static MelonPreferences_Entry<bool> UnredactCrates { get; set; }
    public static MelonPreferences_Entry<bool> RandomizeWeapons { get; set; }

    public static void Setup()
    {
        LoggingMode = GlobalCategory.GetEntry<int>("LoggingMode") ?? GlobalCategory.CreateEntry("LoggingMode", 0, "Logging Mode", "The level of logging to use. 0 = Important Only, 1 = All");
        GlobalCategory.SetFilePath(MelonUtils.UserDataDirectory + "/WeatherElectric.cfg");
        GlobalCategory.SaveToFile(false);
        OverrideFusionCheck = JanitorCategory.GetEntry<bool>("OverrideFusionCheck") ?? JanitorCategory.CreateEntry("OverrideFusionCheck", false, "Override Fusion Check", "Whether or not to override the Fusion check. This is not recommended, as it causes desync.");
        JanitorCategory.SetFilePath(MelonUtils.UserDataDirectory + "/WeatherElectric.cfg");
        JanitorCategory.SaveToFile(false);
        EnableReloader = ReloaderCategory.GetEntry<bool>("EnabledReloader") ?? ReloaderCategory.CreateEntry("EnabledReloader", true, "Enabled", "Whether or not to check for Control + R."); 
        UnredactCrates = UnredactedCategory.GetEntry<bool>("UnredactCrates") ?? UnredactedCategory.CreateEntry("UnredactCrates", true, "Unredact Crates", "Whether or not to unredact crates.");
        UnredactedCategory.SetFilePath(MelonUtils.UserDataDirectory + "/WeatherElectric.cfg");
        UnredactedCategory.SaveToFile(false);
        RandomizeWeapons = RandomizerCategory.GetEntry<bool>("RandomizeWeapons") ?? RandomizerCategory.CreateEntry("RandomizeWeapons", false, "Randomize Weapons", "Whether or not to randomize weapons.");
        RandomizerCategory.SetFilePath(MelonUtils.UserDataDirectory + "/WeatherElectric.cfg");
        RandomizerCategory.SaveToFile(false);
        ModConsole.Msg("Finished preferences setup for MarrowUtils", 1);
    }
}
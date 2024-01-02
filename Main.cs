using MarrowUtils.Utilities.LevelReloader;
using MarrowUtils.Utilities.TheJanitor;
using MarrowUtils.Utilities.WarehouseScanner;

namespace MarrowUtils;

public class Main : MelonMod
{
    internal const string Name = "MarrowUtils";
    internal const string Description = "Template Mod";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "0.0.1";
    internal const string DownloadLink = null;

    internal static MenuCategory MenuCat { get; private set; }
    internal static Assembly CurrAsm { get; } = Assembly.GetExecutingAssembly();

    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        RegisterUtilities();
        var mainCat = MenuManager.CreateCategory("Weather Electric", "#6FBDFF");
        MenuCat = mainCat.CreateCategory("Marrow Utils", Color.gray);
        Utility.StartAll();
        Utility.CreateMenus();
        Hooking.OnLevelInitialized += OnLevelLoad;
        Hooking.OnLevelUnloaded += OnLevelUnload;
    }

    private static void RegisterUtilities()
    {
        ModConsole.Msg("Registering utilities...", 1);
        _ = new LevelReloader();
        _ = new Janitor();
        _ = new WarehouseScanner();
    }

    public override void OnUpdate()
    {
        Utility.OnUpdate();
    }

    private static void OnLevelLoad(LevelInfo levelInfo)
    {
        Utility.LevelLoad(levelInfo);
    }

    private static void OnLevelUnload()
    {
        Utility.LevelUnload();
    }
}
namespace MarrowUtils;

public class Main : MelonMod
{
    internal const string Name = "MarrowUtils";
    internal const string Description = "A collection of utilities for BONELAB.";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "1.0.0";
    internal const string DownloadLink = "https://bonelab.thunderstore.io/package/SoulWithMae/MarrowUtils/";

    internal static MenuCategory MenuCat { get; private set; }
    internal static Assembly CurrAsm { get; } = Assembly.GetExecutingAssembly();

    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        var mainCat = MenuManager.CreateCategory("Weather Electric", "#6FBDFF");
        MenuCat = mainCat.CreateCategory("Marrow Utils", "#009dff");
        Utility.Initialize();
        Hooking.OnLevelInitialized += OnLevelLoad;
        Hooking.OnLevelUnloaded += OnLevelUnload;
#if DEBUG
        ModConsole.Warning("This is a debug build! Expect unstable behavior!");
#endif
    }

    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        if (sceneName.ToUpper().Contains("BOOTSTRAP"))
        {
            AssetWarehouse.OnReady(new Action(WarehouseLoaded));
        }
    }

    private static void WarehouseLoaded()
    {
        var pallets = AssetWarehouse.Instance.GetPallets();
        var crates = AssetWarehouse.Instance.GetCrates();
        Utility.WarehouseLoaded(pallets, crates);
    }

    public override void OnUpdate()
    {
        Utility.OnUpdate();
    }

    public override void OnLateUpdate()
    {
        Utility.OnLateUpdate();
    }
    
    public override void OnFixedUpdate()
    {
        Utility.OnFixedUpdate();
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
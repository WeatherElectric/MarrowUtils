namespace MarrowUtils;

public class Main : MelonMod
{
    internal const string Name = "MarrowUtils";
    internal const string Description = "A collection of utilities for BONELAB.";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "1.0.0";
    internal const string DownloadLink = null;

    internal static MenuCategory MenuCat { get; private set; }
    internal static Assembly CurrAsm { get; } = Assembly.GetExecutingAssembly();

    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        var mainCat = MenuManager.CreateCategory("Weather Electric", "#6FBDFF");
        MenuCat = mainCat.CreateCategory("Marrow Utils", Color.gray);
        Utility.Initialize();
        Hooking.OnLevelInitialized += OnLevelLoad;
        Hooking.OnLevelUnloaded += OnLevelUnload;
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
        Utility.WarehouseLoaded(pallets);
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
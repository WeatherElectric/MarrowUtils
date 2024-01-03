namespace MarrowUtils.Utilities.WarehouseScanner;

internal class WarehouseScanner : Utility
{
    private static bool _isSpawned;
    
    protected override void Start()
    {
        ModConsole.Msg("Loading Warehouse Scanner...", 1);
        Assets.Load();
    }

    protected override void CreateMenu()
    {
        var scannerCat = Main.MenuCat.CreateSubPanel("Warehouse Scanner", Color.magenta);
        scannerCat.CreateFunctionElement("Spawn", Color.green, Spawn);
        scannerCat.CreateFunctionElement("Despawn", Color.red, Despawn);
    }
    
    private static void Spawn()
    {
        if (_isSpawned) return;
        var location = Player.playerHead.position + Player.playerHead.forward * 2f;
        Object.Instantiate(Assets.Scanner, location, Quaternion.identity);
        _isSpawned = true;
    }
    
    private static void Despawn()
    {
        if (!_isSpawned) return;
        // IM SO SMART I KNEW THE INSTANCE WOULD BE USEFUL
        Object.Destroy(Scanner.Object);
        _isSpawned = false;
    }
    
    protected override void OnLevelUnload()
    {
        _isSpawned = false;
    }
}

internal static class Assets
{
    private static AssetBundle _bundle;
    public static GameObject Scanner { get; private set; }

    public static void Load()
    {
        _bundle = HelperMethods.LoadEmbeddedAssetBundle(Main.CurrAsm, HelperMethods.IsAndroid() ? "MarrowUtils.Utilities.WarehouseScanner.Resources.Android.bundle" : "MarrowUtils.Utilities.WarehouseScanner.Resources.Windows.bundle");
        // i didn't feel like naming the prefab something normal
        // god i love rebuilding the bundle every time i make a little mistake
        Scanner = _bundle.LoadPersistentAsset<GameObject>("Assets/Dickweed.prefab");
    }
}
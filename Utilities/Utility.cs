namespace MarrowUtils.Utilities;

// my monobehaviour. MINE.
// i know i can do this all without this class, but this is convienent
/// <summary>
/// Utility class used for managing various mini-mods. Somewhat similar to Monobehaviours, with a Start() and Update().
/// </summary>
public abstract class Utility
{
    private static readonly List<Utility> Instances = new();

    protected internal Utility()
    {
        Instances.Add(this);
    }

    ~Utility()
    {
        Instances.Remove(this);
    }

    /// <summary>
    /// Called when a level is loaded
    /// </summary>
    /// <param name="levelInfo">Information about the level</param>
    protected virtual void OnLevelLoad(LevelInfo levelInfo){}
    /// <summary>
    /// Called when a level is unloaded
    /// </summary>
    protected virtual void OnLevelUnload(){}
    /// <summary>
    /// Called every frame
    /// </summary>
    protected virtual void Update(){}
    /// <summary>
    /// Called when the mod is initialized to create the menu
    /// </summary>
    protected virtual void CreateMenu(){}
    /// <summary>
    /// Called when the mod is initialized
    /// </summary>
    protected virtual void Start(){}
    /// <summary>
    /// Called when AssetWarehouse is initialized
    /// </summary>
    protected virtual void OnWarehouseInit(){}
    protected virtual void OnWarehouseInit(PalletList loadedPalletList){}
    protected virtual void OnWarehouseInit(PalletList loadedPalletList, CrateList crateList){}
    protected virtual void OnWarehouseInit(CrateList crateList){}
    
    /// <summary>
    /// Called every frame after all Update() calls
    /// </summary>
    protected virtual void LateUpdate(){}
    /// <summary>
    /// Called every frame, and instructing Unity to do physics simulations
    /// </summary>
    protected virtual void FixedUpdate(){}
    
    public static void Initialize()
    {
        // automatically create instances of all utilities
        foreach (var type in Main.CurrAsm.GetTypes())
        {
            if (type.IsAbstract || !type.IsSubclassOf(typeof(Utility))) continue;
            Activator.CreateInstance(type);
        }
        
        foreach (var instance in Instances)
        {
            instance.Start();
            instance.CreateMenu();
        }
    }
    
    public static void LevelLoad(LevelInfo levelInfo)
    {
        foreach (var instance in Instances)
        {
            instance.OnLevelLoad(levelInfo);
        }
    }
    
    public static void LevelUnload()
    {
        foreach (var instance in Instances)
        {
            instance.OnLevelUnload();
        }
    }

    public static void OnUpdate()
    {
        foreach (var instance in Instances)
        {
            instance.Update();
        }
    }

    public static void OnLateUpdate()
    {
        foreach (var instance in Instances)
        {
            instance.LateUpdate();
        }
    }

    public static void OnFixedUpdate()
    {
        foreach (var instance in Instances)
        {
            instance.FixedUpdate();
        }
    }

    public static void WarehouseLoaded(PalletList loadedPalletList, CrateList crateList)
    {
        foreach (var instance in Instances)
        {
            instance.OnWarehouseInit();
            instance.OnWarehouseInit(loadedPalletList);
            instance.OnWarehouseInit(crateList);
            instance.OnWarehouseInit(loadedPalletList, crateList);
        }
    }
}
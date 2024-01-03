namespace MarrowUtils.Utilities;

// my monobehaviour. MINE.
// i know i can do this all without this class, but this is convienent
/// <summary>
/// Utility class used for managing various mini-mods. Somewhat similar to Monobehaviours, with a Start() and Update().
/// </summary>
internal abstract class Utility
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
}
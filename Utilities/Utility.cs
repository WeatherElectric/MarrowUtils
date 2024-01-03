﻿namespace MarrowUtils.Utilities;
using System;
using System.Linq;

// my monobehaviour. MINE.
internal abstract class Utility
{
    private static readonly List<Utility> Instances = new();

    protected Utility()
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
        foreach (var instance in Instances)
        {
            Activator.CreateInstance(instance.GetType());
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
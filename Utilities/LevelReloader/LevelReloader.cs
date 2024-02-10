namespace MarrowUtils.Utilities.LevelReloader;

internal class LevelReloader : Utility
{
    protected override void Start()
    {
        ModConsole.Msg("Loading LevelReloader...", 1);
    }
    
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreateSubPanel("Level Reloader", Color.cyan);
        menu.CreateBoolPreference("Enabled", Color.white, Preferences.EnableReloader, Preferences.ReloaderCategory);
    }
    protected override void Update()
    {
        if (!Preferences.EnableReloader.Value || HelperMethods.IsAndroid()) return;
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.R))
        {
            SceneStreamer.Reload();
        }
    }
}
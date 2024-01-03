namespace MarrowUtils.Utilities.EmergencyRelocation;

internal class Teleporter : Utility
{
    protected override void Start()
    {
        ModConsole.Msg("Loading EmergencyRelocation...", 1);
    }
    
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreateSubPanel("Emergency Relocation", Color.green);
        menu.CreateFunctionElement("Relocate", Color.white, Relocate);
    }

    private static Vector3 _levelStart;

    protected override void OnLevelLoad(LevelInfo levelInfo)
    {
        // base game maps don't have a player marker, check for the rigmanager root location for base game maps
        if (CommonBarcodes.Maps.All.Contains(levelInfo.barcode))
        {
            // while I could get the RM from Player.rigManager, that one moves. the root does not.
            var rigmanager = GameObject.Find("[RigManager (Blank)]");
            if (rigmanager == null) return;
            _levelStart = rigmanager.transform.position;
        }
        else
        {
            // since the base game map check failed, it's very likely a custom map, so just grab the player marker
            var playermarker = Object.FindObjectOfType<PlayerMarker>();
            if (playermarker == null) return;
            _levelStart = playermarker.transform.position;
        }
    }
    
    private static void Relocate()
    {
        // fun fact: Teleport() on the rigmanager is just a base game method, i dont even have to do anything to teleport the player, the game just has this
        Player.rigManager.Teleport(_levelStart);
    }
}
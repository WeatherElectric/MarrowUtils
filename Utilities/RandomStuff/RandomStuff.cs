namespace MarrowUtils.Utilities.Random;

internal class RandomStuff : Utility
{
    protected override void Start()
    {
        ModConsole.Msg("Loading RandomStuff...", 1);
    }
    protected override void CreateMenu()
    {
        var randomCat = Main.MenuCat.CreateSubPanel("Random Stuff", Color.yellow);
        randomCat.CreateFunctionElement("Random Gun", Color.white, RandomGun);
        randomCat.CreateFunctionElement("Random Melee", Color.white, RandomMelee);
        randomCat.CreateFunctionElement("Random NPC", Color.white, RandomNpc);
        randomCat.CreateFunctionElement("Random Level", Color.white, RandomLevel);
    }

    private static void RandomGun()
    {
        var position = Player.playerHead.position + Player.playerHead.forward * 2f;
        var gun = CommonBarcodes.Guns.All[UnityEngine.Random.Range(0, CommonBarcodes.Guns.All.Count)];
        HelperMethods.SpawnCrate(gun, position, Quaternion.identity, Vector3.one, false, _ => {});
    }

    private static void RandomMelee()
    {
        var position = Player.playerHead.position + Player.playerHead.forward * 2f;
        var melee = CommonBarcodes.Melee.All[UnityEngine.Random.Range(0, CommonBarcodes.Melee.All.Count)];
        HelperMethods.SpawnCrate(melee, position, Quaternion.identity, Vector3.one, false, _ => {});
    }

    private static void RandomNpc()
    {
        var position = Player.playerHead.position + Player.playerHead.forward * 2f;
        var npc = CommonBarcodes.NPCs.All[UnityEngine.Random.Range(0, CommonBarcodes.NPCs.All.Count)];
        HelperMethods.SpawnCrate(npc, position, Quaternion.identity, Vector3.one, false, _ => {});
    }

    private static void RandomLevel()
    {
        var level = CommonBarcodes.Maps.All[UnityEngine.Random.Range(0, CommonBarcodes.Maps.All.Count)];
        SceneStreamer.Load(level, CommonBarcodes.Maps.LoadDefault);
    }
}
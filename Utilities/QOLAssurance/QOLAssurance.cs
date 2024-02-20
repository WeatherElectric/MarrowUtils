using SLZ.Interaction;
using SLZ.Props.Weapons;
using SLZ.UI;
// ReSharper disable InconsistentNaming

namespace MarrowUtils.Utilities.QolAssurance;

internal class QolAssurance : Utility
{
    protected override void Start()
    {
        ModConsole.Msg("Loading QualityOfLifeAssurance...", 1);
        Assets.Load();
    }
    
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreateSubPanel("Quality of Life Assurance", Color.green);
        menu.CreateBoolPreference("Enabled", Color.white, Preferences.EnableItemPatches, Preferences.QualityOfLifeAssuranceCategory);
    }
    
    #region Spawnables

    protected override void OnSpawnablePlaced(GameObject obj, AssetPoolee poolee)
    {
        if (!Preferences.EnableItemPatches.Value) return;
        if (poolee.spawnableCrate.Barcode == "SLZ.BONELAB.Content.Spawnable.AlarmClock") FixAlarmClock(obj);
        // if (__instance.spawnableCrate.Barcode == "c1534c5a-e55f-4b7b-bdeb-929548656164") FixHeadset(__instance.gameObject);
        // if (__instance.spawnableCrate.Barcode == "c1534c5a-f6e1-4a7b-ab7f-3f5848656164") FixHeadset(__instance.gameObject);
    }

    private static void FixAlarmClock(GameObject obj)
    {
        var tmp = obj.GetComponentInChildren<TextMeshPro>();
        var uiClock = obj.AddComponent<UIClock>();
        var alarmClock = obj.AddComponent<AlarmClock>();
        if (tmp == null || uiClock == null) return;
        uiClock.txt_clock = tmp;
        uiClock.OnEnable();
        var audioSource = obj.AddComponent<AudioSource>();
        SetupAudioSource();
        alarmClock.audioSource = audioSource;
        return;

        void SetupAudioSource()
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.spatialBlend = 1f;
            audioSource.maxDistance = 10f;
            audioSource.minDistance = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.volume = 0.5f;
            audioSource.clip = Assets.AlarmSound;
        }
    }

    // private static void FixHeadset(GameObject obj)
    // {
    //     var weaponSlot = obj.GetComponentInChildren<WeaponSlot>();
    //     WeaponSlot.SlotType currentValue = weaponSlot.slotType;
    //     currentValue &= ~WeaponSlot.SlotType.SIDEARM;
    //     currentValue &= ~WeaponSlot.SlotType.PRIMARY;
    //     currentValue &= ~WeaponSlot.SlotType.SECONDARY;
    //     currentValue &= ~WeaponSlot.SlotType.HEAD;
    //     weaponSlot.slotType = currentValue;
    // }
    
    #endregion
    
    #region Player Rig
    
    [HarmonyPatch(typeof(RigManager))]
    private static class RigmanagerStart
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(RigManager.Start))]
        private static void Postfix(RigManager __instance)
        {
            if (!Preferences.EnableItemPatches.Value) return;
            var HeadSlot = __instance.physicsRig.m_head.GetComponentInChildren<InventorySlotReceiver>();
            if (HeadSlot == null) return;
            HeadSlot.slotType = WeaponSlot.SlotType.HEAD;
        }
    }
    
    #endregion

    private static class Assets
    {
        private static AssetBundle _bundle;
        public static AudioClip AlarmSound { get; private set; }

        public static void Load()
        {
            _bundle = HelperMethods.LoadEmbeddedAssetBundle(Main.CurrAsm, HelperMethods.IsAndroid() ? "MarrowUtils.Utilities.QolAssurance.Resources.Android.bundle" : "MarrowUtils.Utilities.QolAssurance.Resources.Windows.bundle");
            AlarmSound = _bundle.LoadPersistentAsset<AudioClip>("Assets/MarrowUtils/QOLAssurance/BoneTheme.wav");
        }
    }
}
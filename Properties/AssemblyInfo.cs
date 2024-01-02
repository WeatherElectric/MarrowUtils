[assembly: AssemblyTitle(MarrowUtils.Main.Description)]
[assembly: AssemblyDescription(MarrowUtils.Main.Description)]
[assembly: AssemblyCompany(MarrowUtils.Main.Company)]
[assembly: AssemblyProduct(MarrowUtils.Main.Name)]
[assembly: AssemblyCopyright("Developed by " + MarrowUtils.Main.Author)]
[assembly: AssemblyTrademark(MarrowUtils.Main.Company)]
[assembly: AssemblyVersion(MarrowUtils.Main.Version)]
[assembly: AssemblyFileVersion(MarrowUtils.Main.Version)]
[assembly:
    MelonInfo(typeof(MarrowUtils.Main), MarrowUtils.Main.Name, MarrowUtils.Main.Version,
        MarrowUtils.Main.Author, MarrowUtils.Main.DownloadLink)]
[assembly: MelonColor(ConsoleColor.White)]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonOptionalDependencies("LabFusion")]
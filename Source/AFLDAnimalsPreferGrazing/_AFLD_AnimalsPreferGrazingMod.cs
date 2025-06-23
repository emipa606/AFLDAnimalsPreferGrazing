using Mlie;
using UnityEngine;
using Verse;

namespace _AFLD_AnimalsPreferGrazing;

[StaticConstructorOnStartup]
internal class _AFLD_AnimalsPreferGrazingMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static _AFLD_AnimalsPreferGrazingMod Instance;

    private static string currentVersion;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public _AFLD_AnimalsPreferGrazingMod(ModContentPack content) : base(content)
    {
        Instance = this;
        Settings = GetSettings<_AFLD_AnimalsPreferGrazingSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal _AFLD_AnimalsPreferGrazingSettings Settings { get; }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "[AFLD]Animals Prefer Grazing";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        listingStandard.CheckboxLabeled("APG.alsoPerferCorpses".Translate(), ref Settings.AlsoPreferCorpses);
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("APG.modVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}
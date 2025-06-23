using Verse;

namespace _AFLD_AnimalsPreferGrazing;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class _AFLD_AnimalsPreferGrazingSettings : ModSettings
{
    public bool AlsoPreferCorpses = true;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref AlsoPreferCorpses, "alsoPerferCorpses", true);
    }
}
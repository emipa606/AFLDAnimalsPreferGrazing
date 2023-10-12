using HarmonyLib;
using Verse;

namespace _AFLD_AnimalsPreferGrazing;

[StaticConstructorOnStartup]
internal static class Main
{
    static Main()
    {
        new Harmony("com.afld.animalsprefergrazing").PatchAll();
    }
}
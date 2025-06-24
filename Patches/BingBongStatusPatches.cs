using HarmonyLib;

namespace BingBongMod.Patches;

public class BingBongStatusPatches
{
    [HarmonyPatch(typeof(BingBongStatus), "Update")]
    class SetTextsPatch
    {
        static bool Postfix(BingBongPowers __instance)
        {
            return true;
        }
    }
}
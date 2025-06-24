using HarmonyLib;
using Photon.Pun;

namespace BingBongMod.Patches;

public class BingBongForceAbilitiesPatches
{
    [HarmonyPatch(typeof(BingBongForceAbilities), "Start")]
    class ForceStartPatch
    {
        static void Prefix(BingBongForceAbilities __instance)
        {
            __instance.RPCA_BingBongInitObj(__instance.GetComponent<PhotonView>().ViewID);
        }
    }


    [HarmonyPatch(typeof(BingBongForceAbilities), "Update")]
    class ForceUpdatePatch
    {
        static bool Prefix()
        {
            return false;
        }
    }
}
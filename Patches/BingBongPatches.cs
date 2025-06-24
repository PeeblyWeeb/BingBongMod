using BingBongMod.Behaviors;
using HarmonyLib;

namespace BingBongMod.Patches;

public class BingBongPatches
{
    [HarmonyPatch(typeof(BingBong), "Start")]
    class BingBongStartPatch
    {
        static void Prefix(BingBong __instance)
        {
            __instance.gameObject.AddComponent<BingBongDestroyWatcher>();
            __instance.gameObject.AddComponent<BingBongRPC>();
            
            Item item = __instance.GetComponent<Item>();
            if (item.holderCharacter == null)
            {
                BingBongPowersPatches.HideBingBongUI();

                return;
            };
            
            __instance.gameObject.AddComponent<BingBongPowers>();
            
            BingBongTimeControl time = __instance.gameObject.AddComponent<BingBongTimeControl>();
            BingBongPhysics physics = __instance.gameObject.AddComponent<BingBongPhysics>();
            BingBongStatus status = __instance.gameObject.AddComponent<BingBongStatus>();
            BingBongForceAbilities force = __instance.gameObject.AddComponent<BingBongForceAbilities>();
            time.enabled = false;
            physics.enabled = false;
            status.enabled = false;
            force.enabled = false;
        }
    }
}
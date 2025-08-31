using System;
using BingBongMod.Behaviors;
using HarmonyLib;
using UnityEngine;

namespace BingBongMod.Patches;

public class ItemPatches
{
    // they done nuked my boy so, guess this is the new "Start" patch for BingBong, that's cool.
    [HarmonyPatch(typeof(Item), "Start")]
    class BingBongStartPatch
    {
        static void Postfix(Item __instance) {
            BingBong bingBong = __instance.gameObject.GetComponent<BingBong>();
            if (bingBong == null) {
                return;
            }
            
            Plugin.Logger.LogInfo("Found BingBong, attaching components.");
            
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
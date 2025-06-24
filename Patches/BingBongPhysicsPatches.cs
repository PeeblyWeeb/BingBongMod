using System.Reflection;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace BingBongMod.Patches;

public class BingBongPhysicsPatches
{
    [HarmonyPatch(typeof(BingBongPhysics), "DoEffect")]
    class BingBongPhysicsEffectsPatch
    {
        static bool Prefix()
        {
            return false;
        }
    }
    
    [HarmonyPatch(typeof(BingBongPhysics), "SetState")]
    class SetStatePatch
    {
        static bool Prefix(BingBongPhysics __instance, BingBongPhysics.PhysicsType setType)
        {
            __instance.physicsType = setType;
            
            BingBongUtils.UpdateTip(__instance.gameObject);
            return false;
        }
    }
    
    [HarmonyPatch(typeof(BingBongPhysics), "CheckInuput")] // evil retard
    class InputPatch
    {
        static void Postfix(BingBongPhysics __instance)
        {
            BingBongForceAbilities force = __instance.GetComponent<BingBongForceAbilities>();

            float oldForce = force.force;
            float multiplier = 5;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                multiplier = 10f;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                multiplier = 50f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                force.force += multiplier;
            } else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                force.force -= multiplier;
            }

            if (force.force != oldForce)
            {
                BingBongUtils.UpdateTip(__instance.gameObject);
                
                PhotonView view = __instance.GetComponent<PhotonView>();
                view.RPC("BingBongMod__SetForce", RpcTarget.All, view.ViewID, force.force);
            }
        }
    }
    
    [HarmonyPatch(typeof(BingBongPhysics), "SetState")]
    class BingBongPhysicsSetStatePatch
    {
        static void Postfix(BingBongPhysics __instance)
        {
            __instance.GetComponent<BingBongForceAbilities>().physicsType = __instance.physicsType;
            BingBongUtils.UpdateTip(__instance.gameObject);
            
            PhotonView view = __instance.GetComponent<PhotonView>();
            view.RPC("BingBongMod__SetState", RpcTarget.All, view.ViewID, __instance.physicsType);
            
            // __instance
        }
    }
}
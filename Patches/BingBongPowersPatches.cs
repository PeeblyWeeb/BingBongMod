
using System.Reflection;
using HarmonyLib;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BingBongMod.Patches;

public class BingBongPowersPatches
{
    public static void HideBingBongUI()
    {
        if (
            PowersStartPatch.Title
            && PowersStartPatch.SubTitle
            && PowersStartPatch.Tip)
        {
            PowersStartPatch.Title.SetActive(false);
            PowersStartPatch.SubTitle.SetActive(false);
            PowersStartPatch.Tip.SetActive(false);
        }
    }

    public static void UpdateBingBongUI()
    {
        
    } 
    
    [HarmonyPatch(typeof(BingBongPowers), "Start")]
    public class PowersStartPatch
    {
        public static GameObject Title;
        public static GameObject SubTitle;
        public static GameObject Tip;
        
        static bool Prefix(BingBongPowers __instance)
        {
            // steal some ui elements to use for bingbong
            GameObject prompts = GameObject.Find("GAME/GUIManager/Canvas_HUD/Prompts");
            if (Title == null)
            {
                Title = GameObject.Instantiate(GameObject.Find("GAME/GUIManager/Canvas_HUD/Prompts/InteractName"), prompts.transform);
                Title.transform.localPosition = new Vector3(553, -300, 0);
            }
            if (SubTitle == null)
            {
                SubTitle = GameObject.Instantiate(GameObject.Find("GAME/GUIManager/Canvas_HUD/Prompts/InteractName"), prompts.transform);
                SubTitle.transform.localPosition = new Vector3(0, -350, 0);
                SubTitle.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            if (Tip == null)
            {
                Tip = GameObject.Instantiate(GameObject.Find("GAME/GUIManager/Canvas_HUD/Prompts/InteractName"), prompts.transform);
                Tip.transform.localPosition = new Vector3(0, 250, 0);
                Tip.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }

            Item item = __instance.GetComponent<Item>();
            if (item.holderCharacter == Character.localCharacter)
            {
                Title.SetActive(true);
                SubTitle.SetActive(true);
                Tip.SetActive(true);
                
                BingBongUtils.UpdateTip(__instance.gameObject);
            }
            
            __instance.titleText = Title.GetComponentInChildren<TextMeshProUGUI>();
            __instance.descriptionText = SubTitle.GetComponentInChildren<TextMeshProUGUI>();
            
            __instance.SetTexts("Dormant", "");
            __instance.SetTip("", 0);
            
            return false;
        }
    }
    
    [HarmonyPatch(typeof(BingBongPowers), nameof(BingBongPowers.SetTexts))]
    class SetTextsPatch
    {
        static bool Prefix(BingBongPowers __instance)
        {
            if (__instance.titleText == null || __instance.descriptionText == null)
            {
                return false;
            }
            
            return true;
        }
    }
    
    [HarmonyPatch(typeof(BingBongPowers), nameof(BingBongPowers.SetTip))]
    class SetTipPatch
    {
        static bool Prefix(BingBongPowers __instance,string tip)
        {
            if (PowersStartPatch.Tip == null)
            {
                return false;
            }
            
            BingBongUtils.UpdateTip(__instance.gameObject);
            return false;
        }
    }
    
    
 
    
    [HarmonyPatch(typeof(BingBongPowers), "ToggleID")]
    class ToggleIDPatch
    {
        static bool Prefix()
        {
            return false;
        }
    }
    
    [HarmonyPatch(typeof(BingBongPowers), "LateUpdate")]
    class LateUpdatePatch
    {
        static bool Prefix(BingBongPowers __instance)
        {
            if (!Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3)) return false;
            __instance.GetComponent<BingBongStatus>().enabled = false;
            __instance.GetComponent<BingBongPhysics>().enabled = false;
            __instance.GetComponent<BingBongTimeControl>().enabled = false;
            __instance.GetComponent<BingBongForceAbilities>().enabled = false;
            
            PhotonView view = __instance.GetComponent<PhotonView>();
            
            view.RPC("BingBongMod__SetForceEnabled", RpcTarget.All, view.ViewID, false);
            
            if (Input.GetKeyDown(KeyCode.F1))
            {
                __instance.GetComponent<BingBongStatus>().enabled = true;
                BingBongUtils.UpdateTip(__instance.gameObject);
                return false;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                __instance.GetComponent<BingBongTimeControl>().enabled = true;
                BingBongUtils.UpdateTip(__instance.gameObject);
                return false;
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                __instance.GetComponent<BingBongPhysics>().enabled = true;
                __instance.GetComponent<BingBongForceAbilities>().enabled = true;
                
                BingBongUtils.UpdateTip(__instance.gameObject);
                
                view.RPC("BingBongMod__SetForceEnabled", RpcTarget.All, view.ViewID, true);
                return false;
            }

            return false;
        }
    }
}
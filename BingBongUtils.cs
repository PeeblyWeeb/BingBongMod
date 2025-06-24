using System;
using System.Reflection;
using BingBongMod.Patches;
using TMPro;
using UnityEngine;
using PowersStartPatch = BingBongMod.Patches.BingBongPowersPatches.PowersStartPatch;

namespace BingBongMod;

public static class BingBongUtils
{
    public static void UpdateTip(GameObject gameObject) {
        BingBongPowers powers = gameObject.GetComponent<BingBongPowers>();
        if (PowersStartPatch.Tip == null)
            return;
        
        TextMeshProUGUI tipText = PowersStartPatch.Tip.GetComponentInChildren<TextMeshProUGUI>();

        var forceAbilities = powers.GetComponent<BingBongForceAbilities>();
        var timeControl = powers.GetComponent<BingBongTimeControl>();
        var status = powers.GetComponent<BingBongStatus>();

        if (status.enabled) {
            string[] names = Enum.GetNames(typeof(CharacterAfflictions.STATUSTYPE));
            FieldInfo currentStatusInfo = status.GetType()
                .GetField("currentStatusTarget", BindingFlags.Instance | BindingFlags.NonPublic);
            CharacterAfflictions.STATUSTYPE statusType = (CharacterAfflictions.STATUSTYPE)currentStatusInfo.GetValue(status);
            tipText.text = names[(byte)statusType];
            return;
        }
        
        if (forceAbilities.enabled) {
            tipText.text = $"{forceAbilities.physicsType} ({forceAbilities.force})";
            return;
        }

        if (timeControl.enabled) {
            tipText.text = timeControl.currentTimeScale.ToString();
            return;
        }

        tipText.text = "";
    }
}
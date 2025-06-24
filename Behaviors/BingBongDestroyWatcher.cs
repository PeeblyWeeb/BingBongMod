using BingBongMod.Patches;
using UnityEngine;

namespace BingBongMod.Behaviors;

public class BingBongDestroyWatcher : MonoBehaviour
{
    void OnDestroy()
    {
        BingBongPowersPatches.HideBingBongUI();
    }
}
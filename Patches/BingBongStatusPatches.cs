using System.Reflection;
using HarmonyLib;
using Photon.Pun;

namespace BingBongMod.Patches;

public class BingBongStatusPatches
{
    
    // the developers used "this.currentStatusTarget" in this function
    // instead of the passed in "statusID" causing it to not be synced.
    // this patch fixes that
    [HarmonyPatch(typeof(BingBongStatus), nameof(BingBongStatus.RPCA_AddStatusBingBing))]
    class AddStatusPatch
    {
        static bool Prefix(int target, int statusID, int mult)
        {
            Character component = PhotonView.Find(target).GetComponent<Character>();
            CharacterAfflictions.STATUSTYPE status = (CharacterAfflictions.STATUSTYPE)statusID;
            if (component.IsLocal)
            {
                if (mult > 0)
                {
                    if (statusID == -1)
                    {
                        component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Injury, 1f, false);
                        component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Hunger, 1f, false);
                        component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Cold, 1f, false);
                        component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Poison, 1f, false);
                        component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Curse, 1f, false);
                        component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Drowsy, 1f, false);
                        component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Weight, 1f, false);
                        component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Hot, 1f, false);
                        return false;
                    }
                    component.refs.afflictions.AddStatus(status, 0.2f, false);
                    return false;
                }
                else
                {
                    if (statusID == -1)
                    {
                        component.refs.afflictions.SubtractStatus(CharacterAfflictions.STATUSTYPE.Injury, 1f, false);
                        component.refs.afflictions.SubtractStatus(CharacterAfflictions.STATUSTYPE.Hunger, 1f, false);
                        component.refs.afflictions.SubtractStatus(CharacterAfflictions.STATUSTYPE.Cold, 1f, false);
                        component.refs.afflictions.SubtractStatus(CharacterAfflictions.STATUSTYPE.Poison, 1f, false);
                        component.refs.afflictions.SubtractStatus(CharacterAfflictions.STATUSTYPE.Curse, 1f, false);
                        component.refs.afflictions.SubtractStatus(CharacterAfflictions.STATUSTYPE.Drowsy, 1f, false);
                        component.refs.afflictions.SubtractStatus(CharacterAfflictions.STATUSTYPE.Weight, 1f, false);
                        component.refs.afflictions.SubtractStatus(CharacterAfflictions.STATUSTYPE.Hot, 1f, false);
                        return false;
                    }
                    component.refs.afflictions.SubtractStatus(status, 0.2f, false);
                }
            }

            return false;
        }
    }
}
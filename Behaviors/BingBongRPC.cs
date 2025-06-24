using Photon.Pun;
using UnityEngine;

namespace BingBongMod.Behaviors;

public class BingBongRPC : MonoBehaviour
{
    [PunRPC]
    public void BingBongMod__SetState(int viewID, BingBongPhysics.PhysicsType newPhysicsType)
    {
        BingBongForceAbilities force = PhotonView.Find(viewID).gameObject.GetComponent<BingBongForceAbilities>();
        
        force.physicsType = newPhysicsType;
    }
    
    [PunRPC]
    public void BingBongMod__SetForceEnabled(int viewID, bool newState)
    {
        BingBongForceAbilities force = PhotonView.Find(viewID).gameObject.GetComponent<BingBongForceAbilities>();
        force.enabled = newState;
    }
    
    [PunRPC]
    public void BingBongMod__SetForce(int viewID, float newforceValue)
    {
        BingBongForceAbilities force = PhotonView.Find(viewID).gameObject.GetComponent<BingBongForceAbilities>();
        force.force = newforceValue;
    }
}
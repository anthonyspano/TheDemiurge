using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SetPitExit : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D col)
        {
            // PlayerManager.Instance.PitSpawnPoint = col.ClosestPoint(PlayerManager.Instance.transform.position);
            // Debug.Log("outer point: " + PlayerManager.Instance.PitSpawnPoint);
        
        }
    }
}
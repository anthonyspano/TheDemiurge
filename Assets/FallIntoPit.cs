using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class FallIntoPit : MonoBehaviour
    {

        void Start()
        {


        }




        // find the center of the collider to determine which edge player entered
        // center = position of object
        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.CompareTag("PlayerAttack"))
            {
                // Player respawns out of pit 
                //PlayerManager.Instance.transform.position = col.GetContacts(0)
                // get the side which the player entered
                
                var difference = col.ClosestPoint(PlayerManager.Instance.transform.position) - (Vector2)transform.position;
                PlayerManager.Instance.transform.position = col.ClosestPoint(PlayerManager.Instance.transform.position) + difference * 1.5f;




                // player takes damage



            }


        }

    }
}
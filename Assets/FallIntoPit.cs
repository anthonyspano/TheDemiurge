using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class FallIntoPit : MonoBehaviour
    {

        public int fallDamage;
        private Collider2D col;



        void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.CompareTag("PlayerAttack"))
            {
                //Debug.Log("falling");
                // player falling
                //PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Falling;

                // add command into player input buffer
                PlayerInputBuffer.Instance.Add(new InputBufferMemory(Time.frameCount, PlayerController.PlayerStatus.Falling));

                //var spawnCollider = GameObject.Find("PitExit");
                
                // get the difference between the player and center of the pit
                var difference = PlayerManager.Instance.transform.position - transform.position;
                Debug.Log("magnitude: " + difference.magnitude);
                
                // if difference < x, make 5x
                if(difference.magnitude < 1) difference *= 7;

                // spawn player in opposite direction away from center
                PlayerManager.Instance.PitSpawnPoint = PlayerManager.Instance.transform.position + difference;


            }


        }



    }


}
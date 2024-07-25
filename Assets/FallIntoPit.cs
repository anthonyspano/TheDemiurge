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

                col = collider;

                StartCoroutine("FallingSequence");


            }


        }

        IEnumerator FallingSequence()
        {

            yield return null;

            yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);

            var difference = col.ClosestPoint(PlayerManager.Instance.transform.position) - (Vector2)transform.position;
            PlayerManager.Instance.transform.position = col.ClosestPoint(PlayerManager.Instance.transform.position) + difference * 0.9f;

            // player takes damage
            PlayerManager.Instance.pHealth.Damage(fallDamage);

        }



    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // makes player dash one time according to last directional input
    public class Dash : MonoBehaviour
    {

        public IEnumerator ExecuteDash()
        {
            Debug.Log("dashing");
            // start cooldown of jump
            PlayerManager.Instance.StartJumpCD();
            
            // perform jump
            PlayerManager.Instance.CanMove = false;
            var direction = PlayerManager.Instance.LastMove;
            Debug.Log(PlayerManager.Instance.transform.position);
            
            // while player isn't at intended final space after jump
            Vector2 finalSpace = PlayerManager.Instance.transform.position + direction * PlayerManager.Instance.JumpDistance;
            Debug.Log("final space" + finalSpace);
            PlayerManager.Instance.anim.Play("Player_Jump", 0);
            while(Mathf.Abs((PlayerManager.Instance.transform.XandY() - finalSpace).magnitude) > 0.5f)
            {
                // raycast into wall, if going to hit wall, then stop at wall
                RaycastHit2D hit = Physics2D.Raycast(PlayerManager.Instance.transform.position, direction, 0.5f);
                if(hit)
                {
                    if(hit.collider.CompareTag("Wall"))
                    {
                        Debug.Log("stopping");
                        PlayerManager.Instance.anim.Play("Player_Idle");
                        break;
                    }
                }

                PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position,
                                                                                PlayerManager.Instance.transform.position + direction * PlayerManager.Instance.JumpDistance,
                                                                                PlayerManager.Instance.MDD);


            yield return null;
            
            }
        }

    }
}
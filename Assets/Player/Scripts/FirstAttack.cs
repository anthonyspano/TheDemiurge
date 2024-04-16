using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // set attacking trigger back to bool
    // animation event check after each "phase" of animation - if continuechain = false then play 'idle'
    public class FirstAttack : State
    {
        private bool continueChain = false;
        private PlayerBattleSystem pbs;

        public FirstAttack(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
        }
        public override IEnumerator Start() 
        {
            PlayerManager.Instance.CanMove = false;
            PlayerManager.Instance.GetComponent<Animator>().SetBool("isAttacking", true);
            yield return null;

            PlayerManager.Instance.GetComponent<Animator>().SetBool("isBusy", true);
            // scoot towards last move
            var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            //Debug.Log(PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance);
            PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);

            while(PlayerManager.Instance.GetComponent<Animator>().GetBool("isAttacking") == true)
            {
                if(PlayerInput.Slash())
                { 
                    PlayerManager.Instance.continueChain = true;
                    Debug.Log("next attack!");
                }
                yield return null;
            }

            PlayerManager.Instance.isBusy = false;
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;

            PlayerBattleSystem.SetState(new Begin(pbs));

                


        }
    }
}
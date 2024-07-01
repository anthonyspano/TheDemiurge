using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // set attacking trigger back to bool
    // animation event check after each "phase" of animation - if continuechain = false then play 'idle'
    public class PlayerAttack : State
    {
        private bool continueChain = false;
        private PlayerBattleSystem pbs;
        private Animator playerAnim;

        public PlayerAttack(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
            playerAnim = PlayerManager.Instance.GetComponent<Animator>();
        }
        public override IEnumerator Start() 
        {
            
            playerAnim.Play(new AnimatorHashRef().GetFirstAttackState());
            yield return null;
            
            //yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);
            
            yield return new WaitUntil(() => PlayerManager.Instance.nextMoveReady);

            PlayerManager.Instance.nextMoveReady = false;
            

            if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Attack)
            {
                Debug.Log("here");
                playerAnim.Play(new AnimatorHashRef().GetNextState(playerAnim.GetCurrentAnimatorStateInfo(0).shortNameHash));
                yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);
            }

            playerAnim.Play("Player Idle");
            PlayerManager.Instance.StartAttackCD();


            PlayerBattleSystem.SetState(new Begin(pbs));

                


        }


    }
}
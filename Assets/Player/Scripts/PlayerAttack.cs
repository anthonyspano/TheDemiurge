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
        private AudioSource playerAudio;

        public PlayerAttack(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
            playerAnim = PlayerManager.Instance.GetComponent<Animator>();
            playerAudio = PlayerManager.Instance.GetComponent<AudioSource>();
        }
        public override IEnumerator Start() 
        {
            Debug.Log("attacking");
            playerAnim.Play(new AnimatorHashRef().GetFirstAttackState());
            playerAudio.Play();

            yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);
           
            // yield return new WaitUntil(() => PlayerManager.Instance.nextMoveReady);

            // if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Attack)
            // {
              
            //     playerAnim.Play(new AnimatorHashRef().GetNextState(playerAnim.GetCurrentAnimatorStateInfo(0).shortNameHash));
            //     yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);
            // }
           
            // PlayerManager.Instance.nextMoveReady = false;

            //PlayerManager.Instance.StartAttackCD();


            PlayerBattleSystem.SetState(new Begin(pbs));

                


        }


    }
}
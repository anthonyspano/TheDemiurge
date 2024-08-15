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
        private PlayerStateMachine psm;
        private Animator playerAnim;
        private AudioSource playerAudio;

        public PlayerAttack(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            psm = playerStateMachine;
            playerAnim = PlayerManager.Instance.GetComponent<Animator>();
            playerAudio = PlayerManager.Instance.GetComponent<AudioSource>();
        }
        public override IEnumerator Start() 
        {
            string attackAnimation = new AnimatorHashRef().GetFirstAttackState();
            playerAnim.Play(attackAnimation);
            playerAudio.Play();

            //yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);
            // check to see if player animator is currently playing the chosen attack animation
            yield return new WaitUntil(() => !PlayerManager.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation));

            yield return null;
            yield return null;
            yield return null;
            


            _playerStateMachine.SetState(new Begin(psm));

                


        }


    }
}
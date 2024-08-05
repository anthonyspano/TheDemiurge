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
            
            playerAnim.Play(new AnimatorHashRef().GetFirstAttackState());
            playerAudio.Play();

            yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);


            _playerStateMachine.SetState(new Begin(psm));

                


        }


    }
}
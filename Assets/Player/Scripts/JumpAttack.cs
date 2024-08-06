using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{

    public class JumpAttack : State
    {
        PlayerStateMachine psm;
        Animator playerAnim;
        AudioSource playerAudio;

        public JumpAttack(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            psm = playerStateMachine;
            playerAnim = psm.GetComponent<Animator>();
            playerAudio = psm.GetComponent<AudioSource>();
        }

        public override IEnumerator Start()
        {
            playerAnim.Play(new AnimatorHashRef().GetFirstAttackState());
            playerAudio.Play();

            playerAnim.SetBool("inAir", true);
            
            //yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.InAir);
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.JoystickButton2) || PlayerController.Instance.jumpTime >= PlayerManager.Instance.MaxJumpTime);

            //Debug.Log(PlayerController.Instance.jumpTime);
            //yield return new WaitForSeconds(PlayerController.Instance.jumpTime);
            playerAnim.SetBool("inAir", false);
            


            yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);

            _playerStateMachine.SetState(new Begin(psm));

        }




    }
}
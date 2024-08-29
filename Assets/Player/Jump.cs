using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class Jump : State
    {
        private float coolDownTimer = 0;
        private Animator anim;
        private Rigidbody2D rb;
        private BoxCollider2D bc;

        private PlayerStateMachine psm;
        private bool once;

        public Jump(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            psm = playerStateMachine;
            anim = psm.GetComponent<Animator>();
            rb = psm.GetComponent<Rigidbody2D>();
            bc = psm.GetComponent<BoxCollider2D>();
        }

        public override IEnumerator Start()
        {
            PlayerManager.Instance.moveSpeed = PlayerManager.Instance.dashSpeed;
            bc.enabled = false;

            yield return new WaitForSeconds(PlayerManager.Instance.dashLength);

            PlayerManager.Instance.moveSpeed = PlayerManager.Instance.runSpeed;
            bc.enabled = true;

            // end jump endeavors
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            PlayerManager.Instance.isBusy = false;

            _playerStateMachine.SetState(new Begin(psm));

        }

    }

}

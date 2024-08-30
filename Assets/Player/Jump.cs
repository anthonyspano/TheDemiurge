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
        private SpriteRenderer sr;

        private PlayerStateMachine psm;
        private bool once;

        public Jump(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            psm = playerStateMachine;
            anim = psm.GetComponent<Animator>();
            rb = psm.GetComponent<Rigidbody2D>();
            bc = psm.GetComponent<BoxCollider2D>();
            sr = psm.GetComponent<SpriteRenderer>();
        }

        public override IEnumerator Start()
        {
            PlayerManager.Instance.moveSpeed = PlayerManager.Instance.dashSpeed;
            bc.enabled = false;
            sr.color = new Color(255f, 255f, 255f, 0.25f);

            yield return new WaitForSeconds(PlayerManager.Instance.dashLength);

            PlayerManager.Instance.moveSpeed = PlayerManager.Instance.runSpeed;
            bc.enabled = true;
            sr.color = new Color(255f, 255f, 255f, 1f);

            // end jump endeavors
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            PlayerManager.Instance.isBusy = false;

            _playerStateMachine.SetState(new Begin(psm));

        }

    }

}

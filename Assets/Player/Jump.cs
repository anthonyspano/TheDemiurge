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
        private Renderer _renderer;

        private PlayerStateMachine psm;
        private bool once;

        public Jump(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            psm = playerStateMachine;
            anim = psm.GetComponent<Animator>();
            rb = psm.GetComponent<Rigidbody2D>();
            bc = psm.GetComponent<BoxCollider2D>();
            sr = psm.GetComponent<SpriteRenderer>();
            _renderer = psm.GetComponent<Renderer>();
        }

        public override IEnumerator Start()
        {
            // change material of player to ChangeAlpha
            PlayerManager.Instance._renderer.material = Resources.Load<Material>("ChangeAlpha");
            Debug.Log("AlphaMaterial");

            // set alpha to 0.25f
            PlayerManager.Instance._renderer.material.SetFloat("_Alpha", 0.25f);

            PlayerManager.Instance.moveSpeed = PlayerManager.Instance.dashSpeed;
            PlayerManager.Instance.transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log(PlayerManager.Instance.transform.GetChild(3));
            sr.color = new Color(255f, 255f, 255f, 0.25f);

            yield return new WaitForSeconds(PlayerManager.Instance.dashLength);

            PlayerManager.Instance.moveSpeed = PlayerManager.Instance.runSpeed;
            sr.color = new Color(255f, 255f, 255f, 1f);

            // set alpha to normal
            PlayerManager.Instance._renderer.material.SetFloat("_Alpha", 1f);

            // change mat back to SpriteOutline 
            PlayerManager.Instance._renderer.material = Resources.Load<Material>("GlowingOutline");
            PlayerManager.Instance._renderer.material.SetFloat("_OutlineThickness", PlayerManager.Instance.outlineThickness); 

            //yield return new WaitForSeconds(1f);
            PlayerManager.Instance.transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = true;

            // end jump endeavors
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            PlayerManager.Instance.isBusy = false;

            _playerStateMachine.SetState(new Begin(psm));

        }

    }

}

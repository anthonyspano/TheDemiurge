using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{

    public class Falling : State
    {
        PlayerStateMachine psm;
        Animator anim;
        Rigidbody2D rb;
        BoxCollider2D bc;

        public Falling(PlayerStateMachine playerStateMachine) : base(playerStateMachine) 
        {
            psm = playerStateMachine; 
            anim = psm.GetComponent<Animator>();
            rb = psm.GetComponent<Rigidbody2D>();  
            bc = psm.GetComponent<BoxCollider2D>();

        }

        public override IEnumerator Start()
        {
            
            anim.Play("Falling");
            PlayerManager.Instance.GetComponent<AudioSource>().PlayOneShot(PlayerManager.Instance.falling1, 0.7f);

            bc.enabled = false;
            yield return null;

            yield return new WaitUntil(() => !anim.GetCurrentAnimatorStateInfo(0).IsName("Falling"));
            psm.transform.position = PlayerManager.Instance.PitSpawnPoint;
            Debug.Log("actual: " + psm.transform.position);
            
            //yield return new WaitUntil(() => PlayerController.Instance.playerInputActions.Player.Movement.ReadValue<Vector2>().magnitude != 0);
            //yield return null;
            PlayerManager.Instance.pHealth.Damage(20);
            
            yield return new WaitForSeconds(0.6f);
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            bc.enabled = true;

            _playerStateMachine.SetState(new Begin(psm));
        }



    }




}
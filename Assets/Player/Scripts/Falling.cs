using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{

    public class Falling : State
    {
        PlayerBattleSystem pbs;
        Animator anim;
        Rigidbody2D rb;
        BoxCollider2D bc;

        public Falling(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem) 
        {
            pbs = playerBattleSystem; 
            anim = pbs.GetComponent<Animator>();
            rb = pbs.GetComponent<Rigidbody2D>();  
            bc = pbs.GetComponent<BoxCollider2D>();

        }

        public override IEnumerator Start()
        {
            
            anim.Play("Falling");

            bc.enabled = false;
            yield return null;

            yield return new WaitUntil(() => !anim.GetCurrentAnimatorStateInfo(0).IsName("Falling"));
            pbs.transform.position = PlayerManager.Instance.PitSpawnPoint;
            Debug.Log("actual: " + pbs.transform.position);
            
            //yield return new WaitUntil(() => PlayerController.Instance.playerInputActions.Player.Movement.ReadValue<Vector2>().magnitude != 0);
            //yield return null;
            PlayerManager.Instance.pHealth.Damage(20);
            
            yield return new WaitForSeconds(0.6f);
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            bc.enabled = true;

            PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
        }



    }




}
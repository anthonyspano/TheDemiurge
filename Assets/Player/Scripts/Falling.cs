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


            // simulate falling (probably better through animation and keeping player still)
            rb.gravityScale = 4;
            bc.enabled = false;


            yield return new WaitForSeconds(1f);
            rb.gravityScale = 0;
            bc.enabled = true;

            anim.Play("Player Idle");
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;

            PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
        }


    }




}
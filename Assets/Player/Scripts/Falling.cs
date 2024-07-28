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

            bc.enabled = true;
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;

            PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
        }


    }




}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // root of decision tree for bosses
    public class BossEngage : State
    {
        BossBattleSystem bbs;
        Animator anim;
        AudioSource audio;

        public BossEngage(BossBattleSystem _bossBattleSystem) : base(_bossBattleSystem)
        {
            bbs = _bossBattleSystem;
            anim = bbs.GetComponent<Animator>();
            bbs.Player = PlayerManager.Instance.transform;
            audio = bbs.GetComponent<AudioSource>();


        }

        public override IEnumerator Start()
        {
            if(bbs.em.timeToReact && !bbs.em.ReactOnce)
            {
                bbs.em.ReactOnce = true;
                bbs.em.timeToReact = false;
                BossBattleSystem.SetState(new ReactionAttack(BossBattleSystem));
            }
            else
            {
                yield return new WaitUntil(() => bbs.em.PlayerIsInRange(bbs.em.pursuitRange));
                while(bbs.em.PlayerIsInRange(bbs.em.pursuitRange) && !bbs.em.PlayerIsInRange(bbs.em.attackRange) && bbs.CanMove)
                {
                    anim.SetBool("Running", true);
                    anim.SetFloat("MoveX", bbs.em.PlayerFacingVector().x);
                    anim.SetFloat("MoveY", bbs.em.PlayerFacingVector().y);
                    bbs.transform.position = Vector3.MoveTowards(bbs.transform.position, bbs.Player.position, 5f * Time.deltaTime);
                    yield return null;
                }

                //Debug.Log(bbs.em.pursuitRange);
                // set bs target to player's current position
                //bbs.em.PlayerPosition = PlayerManager.player.transform.position;
                anim.SetBool("Running", false);
                bbs.CanMove = false;
                // checking if player is in range
                //yield return new WaitUntil(() => bbs.em.PlayerIsInRange(bbs.em.attackRange));
                
                //yield return new WaitForSeconds(1f); // the wait before attacking
                BossBattleSystem.SetState(new BossAttackPlayer(BossBattleSystem));
                yield return null;
            }
        }




        

    }
}
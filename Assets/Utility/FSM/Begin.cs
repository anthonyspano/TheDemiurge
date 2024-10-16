using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.ultimate2d.combat
{
    public class Begin : State
    {
        PlayerBattleSystem pbs;
        AudioSource audio;
        EnemyManager em;

        public Begin(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
            
        }


        public override IEnumerator Start()
        {
            // wait until Input Buffer contains player input
            yield return new WaitUntil(() => PlayerInputBuffer.Instance.GetCommand() != PlayerController.PlayerStatus.Neutral); 
            
            switch(PlayerInputBuffer.Instance.GetCommand())
            {       
                case PlayerController.PlayerStatus.Attack:
                    if(PlayerManager.Instance.attackCooldown <= 0)
                    {
                        // start cooldown timer and attack
                        //PlayerManager.Instance.CanMove = false;
                        PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Attack;
                        Debug.Log("starting");
                        PlayerBattleSystem.SetState(new PlayerAttack(PlayerBattleSystem));
                    }
                    else 
                    {
                        //Debug.Log("cooldown");
                        PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                    }

                    break;

                case PlayerController.PlayerStatus.Ultimate:
                    if(PlayerManager.Instance.ultReady)
                    {
                        PlayerManager.Instance.isBusy = true;
                        // use ult - function in playermanager 
                        Debug.Log("here");
                        PlayerManager.Instance.FireUltimate();
                        yield return new WaitUntil(() => !PlayerManager.Instance.isBusy);
                        PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                        
                    }
                    else
                    {   
                        PlayerManager.Instance.isBusy = false;
                        PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                    }

                    break;

                default:
                    PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
                    PlayerManager.Instance.isBusy = false;
                    PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                    break;

            }

            // replace current command with neutral after execution
            PlayerInputBuffer.Instance.SetCurrentFrame(PlayerController.PlayerStatus.Neutral);


            
        }
 
        
    }
}
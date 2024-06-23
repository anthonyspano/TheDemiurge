using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            yield return new WaitUntil(() => PlayerInputBuffer.Instance.GetCommand(PlayerInputBuffer.Instance.InputBufferWindow) != PlayerController.PlayerStatus.Neutral); // 300 frames looking backwards
            Debug.Log("button hit");
            switch(PlayerInputBuffer.Instance.GetCommand(PlayerInputBuffer.Instance.InputBufferWindow))
            {       
                case PlayerController.PlayerStatus.Attack:
                    if(PlayerManager.Instance.attackCooldown <= 0)
                    {
                        // start cooldown timer and attack
                        //PlayerManager.Instance.CanMove = false;
                        PlayerManager.Instance.StartAttackCD();
                        PlayerBattleSystem.SetState(new PlayerAttack(PlayerBattleSystem));
                    }
                    else 
                    {
                        PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                    }

                    break;

                case PlayerController.PlayerStatus.Ultimate:
                    if(PlayerManager.Instance.ultReady)
                    {
                        // use ult - function in playermanager 
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


            
        }
 
        
    }
}
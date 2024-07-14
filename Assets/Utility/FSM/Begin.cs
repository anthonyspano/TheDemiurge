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
            
            if(PlayerInputBuffer.Instance.GetCommand() == PlayerController.PlayerStatus.LightAttack ||
               PlayerInputBuffer.Instance.GetCommand() == PlayerController.PlayerStatus.JumpAttack)
            {       
                PlayerController.Instance.playerStatus = PlayerInputBuffer.Instance.GetCommand();
                PlayerBattleSystem.SetState(new PlayerAttack(PlayerBattleSystem));                

            }
            if(PlayerInputBuffer.Instance.GetCommand() == PlayerController.PlayerStatus.Ultimate)
            {
                if(PlayerManager.Instance.ultReady)
                {
                    PlayerManager.Instance.isBusy = true;
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

            }
            else
            {
                PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
                PlayerManager.Instance.isBusy = false;
                PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
            

            }

            // replace current command with neutral after execution
            PlayerInputBuffer.Instance.SetCurrentFrame(PlayerController.PlayerStatus.Neutral);


            
        }
 
        
    }
}
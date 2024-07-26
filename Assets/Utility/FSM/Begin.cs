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
            //yield return new WaitUntil(() => PlayerController.Instance.playerStatus.CommandReady());
            yield return new WaitUntil(() => PlayerInputBuffer.Instance.GetCommand() != PlayerController.PlayerStatus.Neutral); 

            PlayerController.Instance.playerStatus = PlayerInputBuffer.Instance.GetCommand();
            PlayerInputBuffer.Instance.SetCurrentFrame(PlayerController.PlayerStatus.Neutral);
            if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.LightAttack) 
            {       
                PlayerBattleSystem.SetState(new PlayerAttack(PlayerBattleSystem));                

            }
            if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.JumpAttack)
            {
                PlayerBattleSystem.SetState(new JumpAttack(PlayerBattleSystem));
            }
            if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Ultimate)
            {
                if(PlayerManager.Instance.ultReady)
                {
                    PlayerManager.Instance.isBusy = true;
                    
                    // use ult - function in playermanager 
                   // Debug.Log("telling child to fire");
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
            if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Falling)
            {
                PlayerBattleSystem.SetState(new Falling(PlayerBattleSystem));
            }
            else
            {
                PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
                PlayerManager.Instance.isBusy = false;
                PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
            

            }

            // replace current command with neutral after execution
            //PlayerInputBuffer.Instance.SetCurrentFrame(PlayerController.PlayerStatus.Neutral);


            
        }
 
        
    }
}
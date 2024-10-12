using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.ultimate2d.combat
{
    public class Begin : State
    {
        PlayerStateMachine psm;
        AudioSource audio;
        EnemyManager em;

        public Begin(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            psm = playerStateMachine;
            
        }


        public override IEnumerator Start()
        {
            // wait until Input Buffer contains player input
            //yield return new WaitForSeconds(PlayerManager.Instance.attackCooldownRate);
            yield return new WaitUntil(() => PlayerInputBuffer.Instance.GetCommand() != PlayerController.PlayerStatus.Neutral); 

            PlayerController.Instance.playerStatus = PlayerInputBuffer.Instance.GetCommand();
            PlayerInputBuffer.Instance.SetCurrentFrame(PlayerController.PlayerStatus.Neutral);
            if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.LightAttack) 
            {       
                _playerStateMachine.SetState(new PlayerAttack(psm));                

            }
            // else if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.JumpAttack)
            // {
            //     _playerStateMachine.SetState(new JumpAttack(psm));
            // }
            else if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Dash)
            {
                Debug.Log("dash");
                _playerStateMachine.SetState(new Jump(psm));
            }
            else if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Ultimate)
            {
                if(PlayerManager.Instance.ultReady)
                {
                    PlayerManager.Instance.isBusy = true;
                    PlayerManager.Instance.FireUltimate();
                    yield return new WaitUntil(() => !PlayerManager.Instance.isBusy);
                    _playerStateMachine.SetState(new Begin(psm));
                    
                }
                else
                {   
                    PlayerManager.Instance.isBusy = false;
                    _playerStateMachine.SetState(new Begin(psm));
                }

            }
            else if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Falling)
            {
                _playerStateMachine.SetState(new Falling(psm));
            }
            else
            {
                Debug.Log("resetting");
                PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
                PlayerManager.Instance.isBusy = false;
                _playerStateMachine.SetState(new Begin(psm));
            

            }

            
        }
 
        
    }
}
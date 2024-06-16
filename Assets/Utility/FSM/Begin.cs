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

        

        // close the loop according to notepad on desk
        public override IEnumerator Start()
        {
            Debug.Log("waiting");
            // TODO: wait until Input Buffer does not have a neutral action
            yield return new WaitUntil(() => PlayerInputBuffer.Instance.GetCommand() != PlayerController.PlayerStatus.Neutral);
            Debug.Log("passed");
            // grab key pressed from gamemanager
            yield return null;
            PlayerManager.Instance.GetComponent<Animator>().SetBool("isBusy", false);
            switch(PlayerInputBuffer.Instance.GetCommand())
            {       
                case PlayerController.PlayerStatus.Attack:
                    if(PlayerManager.Instance.attackCooldown <= 0)
                    {
                        // start cooldown timer
                        PlayerManager.Instance.attackCooldown = PlayerManager.Instance.attackCooldownRate;
                        PlayerManager.Instance.StartAttackCD();
                        Debug.Log("Attacking");
                        PlayerBattleSystem.SetState(new FirstAttack(PlayerBattleSystem));
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
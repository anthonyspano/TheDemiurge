using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.ultimate2d.combat
{
    
    public class AnimatorHashRef
    {

        // int - short name hash of prior animation state
        // string - string of the following animation state (return null if no following state)
        Dictionary<int, string> nextAnimDict;

        public AnimatorHashRef()
        {
            nextAnimDict = new Dictionary<int, string>();

            nextAnimDict.Add(1072997824, "v-attack-dr-2"); // hash for atk1, string for atk 2
            nextAnimDict.Add(692857786, "v-attack-dl-2"); 
            nextAnimDict.Add(-1041687744, "v-attack-ul-2"); 
            nextAnimDict.Add(-682519238, "v-attack-ur-2"); 

        }

        public string GetNextState(int hash)
        {
            try
            {
                return nextAnimDict[hash];
            }
            catch(Exception e)
            {
                return "";
            }


        }

        public string GetFirstAttackState()
        {
            // helper method to identify correct attack anim to play from idle
            switch(PlayerController.Instance.playerStatus)
            {
                case PlayerController.PlayerStatus.LightAttack:
                    switch(PlayerManager.Instance.pFacingDir)
                    {
                        case PlayerManager.Direction.DownRight:
                            return "v-attack-dr-1";
                        case PlayerManager.Direction.DownLeft:
                            return "v-attack-dl-1";
                        case PlayerManager.Direction.UpRight:
                            return "v-attack-ur-1";
                        case PlayerManager.Direction.UpLeft:
                            return "v-attack-ul-1";

                        default:
                            Debug.Log("Player direction null");
                            return null;
                            
                    }
                case PlayerController.PlayerStatus.Sweep:
                    switch(PlayerManager.Instance.pFacingDir)
                    {
                        case PlayerManager.Direction.DownRight:
                            return "";
                        case PlayerManager.Direction.DownLeft:
                            return "v-attack-dl-1";
                        case PlayerManager.Direction.UpRight:
                            return "v-attack-ur-1";
                        case PlayerManager.Direction.UpLeft:
                            return "v-attack-ul-1";

                        default:
                            Debug.Log("Player direction null");
                            return null;
                            break;
                    }
                case PlayerController.PlayerStatus.JumpAttack:
                    switch(PlayerManager.Instance.pFacingDir)
                    {
                        case PlayerManager.Direction.DownRight:
                            return "v-jumpattack-dr";
                        case PlayerManager.Direction.DownLeft:
                            return "v-jumpattack-dl";
                        case PlayerManager.Direction.UpRight:
                            return "v-attack-ur-1";
                        case PlayerManager.Direction.UpLeft:
                            return "v-attack-ul-1";

                        default:
                            Debug.Log("Player direction null");
                            return null;
                            break;
                    }
                default:
                    return null;
                    break;

            }


            


        }




    }
}
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
            switch(PlayerManager.Instance.pFacingDir)
            {
                case PlayerManager.Direction.right:
                    return "v-attack-dr-1";
                case PlayerManager.Direction.left:
                    return "v-attack-dl-1";
                case PlayerManager.Direction.up:
                    return "v-attack-dl-1";
                case PlayerManager.Direction.down:
                    return "v-attack-dr-1";

                default:
                    Debug.Log("Player direction null");
                    return null;
                    break;
            }

            


        }




    }
}
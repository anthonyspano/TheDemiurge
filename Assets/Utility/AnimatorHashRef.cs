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
        Dictionary<int, string> dict;

        public AnimatorHashRef()
        {
            dict = new Dictionary<int, string>();

            dict.Add(1072997824, "v-attack-dr-2"); // hash for atk1, string for atk 2

        }

        public string GetNextState(int hash)
        {
            try
            {
                return dict[hash];
            }
            catch(Exception e)
            {
                return "";
            }


        }




    }
}
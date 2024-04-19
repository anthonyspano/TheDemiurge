using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyStart : State
    {
        private SkellyBattleSystem sbs;
        
        public SkellyStart(SkellyBattleSystem skellyBattleSystem) : base(skellyBattleSystem)
        {
            sbs = skellyBattleSystem;
        }

        public override IEnumerator Start()
        {
            // wait until player is in range
            yield return new WaitUntil(() => sbs.em.PlayerIsInRange(sbs.em.pursuitRange));

            // throw boomerang
            Debug.Log("Throwing you a bone!");

            // laugh
            Debug.Log("Hahahaha!");

            
        }

    }
}
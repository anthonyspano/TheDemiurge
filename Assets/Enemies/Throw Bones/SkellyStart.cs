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
            // throw boomerang
            SkellyBattleSystem.SetState(new SkellyMove(SkellyBattleSystem));

            // laugh
            //Debug.Log("Hahahaha!");
            yield return null;

            
        }

    }
}
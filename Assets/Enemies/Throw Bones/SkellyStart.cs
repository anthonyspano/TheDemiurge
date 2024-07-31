using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyStart : State
    {
        private EnemyStateMachine esm;
        
        public SkellyStart(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
        }

        public override IEnumerator Start()
        {
            // throw boomerang
            _enemyStateMachine.SetState(new SkellyMove(esm));

            // laugh
            //Debug.Log("Hahahaha!");
            yield return null;

            
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ChargerStart : State
    {
        private EnemyStateMachine esm;
        
        public ChargerStart(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
        }

        public override IEnumerator Start()
        {
            yield return new WaitUntil(() => esm.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"));

            yield return new WaitForSeconds(1.3f);
            _enemyStateMachine.SetState(new MeleeEnemyAttackLeap(esm));


            
        }

    }
}
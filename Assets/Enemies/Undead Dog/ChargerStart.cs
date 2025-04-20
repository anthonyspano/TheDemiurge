using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ChargerStart : State
    {
        private EnemyStateMachine esm;
        private Animator anim;
        
        public ChargerStart(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
            anim = esm.GetComponent<Animator>();
        }

        public override IEnumerator Start()
        {
            //yield return new WaitUntil(() => esm.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"));
            anim.Play("Idle");

            yield return new WaitForSeconds(1f);
            _enemyStateMachine.SetState(new ChargerRun(esm));


            
        }

    }
}
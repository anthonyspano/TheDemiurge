using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ChargerMove : State
    {
        private EnemyStateMachine esm;
        private EnemyManager em;
        private Animator anim;
        
        public ChargerMove(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
            em = esm.GetComponent<EnemyManager>();
            anim = esm.GetComponent<Animator>();
        }

        public override IEnumerator Start()
        {

            // spawn point for enemy to move to and attack at
            // make spawn point a little bit closer to the enemy from the player
            var attackPoint = GameObject.Instantiate(new GameObject("attackPoint"), PlayerManager.Instance.transform.position, Quaternion.identity);

            if(Vector2.Distance(attackPoint.transform.position, esm.transform.position) > 0.4f)
                anim.SetBool("isRunning", true);

            while(Vector2.Distance(attackPoint.transform.position, esm.transform.position) > 0.4f)
            {

                esm.transform.position = Vector2.MoveTowards(esm.transform.position, attackPoint.transform.position, em.moveSpeed * Time.deltaTime);
                yield return null;
            }

            Object.Destroy(attackPoint);

            // attack
            anim.SetBool("isAttacking", true);
           



            _enemyStateMachine.SetState(new ChargerStart(esm));
            
            yield return null;
        }

    }
}
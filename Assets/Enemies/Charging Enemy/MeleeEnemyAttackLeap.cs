using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class MeleeEnemyAttackLeap : State
    {
        private EnemyStateMachine esm;
        private EnemyManager em;
        private Animator anim;
        
        public MeleeEnemyAttackLeap(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
            em = esm.GetComponent<EnemyManager>();
            anim = esm.GetComponent<Animator>();
        }

        public override IEnumerator Start()
        {
            // spawns object at player position
            // TBI: varying distance behind or in front of player
            var attackPoint = GameObject.Instantiate(new GameObject("attackPoint"), PlayerManager.Instance.transform.position, Quaternion.identity);
            // Debug.Log(attackPoint.transform.position);
            // Debug.Log(PlayerManager.Instance.transform.position);
            // moves to withing "leaping distance" object
            if(Vector2.Distance(attackPoint.transform.position, esm.transform.position) > em.leapingDistance)
                anim.SetBool("isRunning", true);

            while(Vector2.Distance(attackPoint.transform.position, esm.transform.position) > em.leapingDistance)
            {

                esm.transform.position = Vector2.MoveTowards(esm.transform.position, attackPoint.transform.position, em.moveSpeed * Time.deltaTime);
                yield return null;
            }

            // plays leaping animation
            anim.SetBool("isLeaping", true);

            // moves slower? leaping speed
            while(Vector2.Distance(attackPoint.transform.position, esm.transform.position) > 1.5f)
            {

                esm.transform.position = Vector2.MoveTowards(esm.transform.position, attackPoint.transform.position, em.leapingSpeed * Time.deltaTime);
                yield return null;
            }

            // when within x distance (1f), play rest of attack
            anim.SetBool("finishingStrike", true);

            while(Vector2.Distance(attackPoint.transform.position, esm.transform.position) > 0.5f)
            {

                esm.transform.position = Vector2.MoveTowards(esm.transform.position, attackPoint.transform.position, em.leapingSpeed * Time.deltaTime);
                yield return null;
            }
            
            anim.SetBool("isLeaping", false);
            anim.SetBool("isRunning", false);
            Object.Destroy(attackPoint);
            yield return new WaitForSeconds(2);


            _enemyStateMachine.SetState(new ChargerStart(esm));
            
            yield return null;
        }

    }
}
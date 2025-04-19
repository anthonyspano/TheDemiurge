using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ChargerRun : State
    {
        private EnemyStateMachine esm;
        private SpriteRenderer sr;
        private EnemyManager em;
        private PolygonCollider2D hurtBox;
        private Animator anim;
        
        
        public ChargerRun(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
            sr = esm.GetComponent<SpriteRenderer>();
            em = esm.GetComponent<EnemyManager>();
            hurtBox = esm.GetComponent<PolygonCollider2D>();
            anim = esm.GetComponent<Animator>();
        }

        public override IEnumerator Start()
        {
            esm.attackPoint = GameObject.Instantiate(new GameObject("attackPoint"), PlayerManager.Instance.transform.position, Quaternion.identity);

            if(Vector2.Distance(esm.attackPoint.transform.position, esm.transform.position) > em.leapingDistance)
                anim.Play("running");

            while(Vector2.Distance(esm.attackPoint.transform.position, esm.transform.position) > em.leapingDistance)
            {

                esm.transform.position = Vector2.MoveTowards(esm.transform.position, esm.attackPoint.transform.position, em.moveSpeed * Time.deltaTime);
                yield return null;
            }


            anim.Play("Idle");
            yield return new WaitForSeconds(1f);

            _enemyStateMachine.SetState(new ChargerAttack(esm));


            yield return null;
            
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ChargerAttack : State
    {
        private EnemyStateMachine esm;
        private SpriteRenderer sr;
        private Animator anim;
        private EnemyManager em;
        private PolygonCollider2D hurtBox;
        
        public ChargerAttack(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
            sr = esm.GetComponent<SpriteRenderer>();
            anim = esm.GetComponent<Animator>();
            em = esm.GetComponent<EnemyManager>();
            hurtBox = esm.GetComponent<PolygonCollider2D>();
        }

        public override IEnumerator Start()
        {
            // plays leaping animation
            anim.Play("charge");
            yield return null;
            hurtBox.enabled = true;

            // moves slower? leaping speed
            float timer = 1f;
            while(Vector2.Distance(esm.attackPoint.transform.position, esm.transform.position) > 0.1f && timer > 0)
            {
                Debug.Log(timer);
                esm.transform.position = Vector2.MoveTowards(esm.transform.position, esm.attackPoint.transform.position, em.leapingSpeed * Time.deltaTime);
                timer -= Time.deltaTime;
                yield return null;
            }

            hurtBox.enabled = false;

            anim.Play("Idle");
            _enemyStateMachine.SetState(new ChargerStart(esm));


            
            
        }

    }
}
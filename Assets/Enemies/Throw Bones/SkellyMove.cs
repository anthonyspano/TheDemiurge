using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyMove : State
    {
        private EnemyStateMachine esm;
        private EnemyManager em;
        private Vector2 targetPos;
        private float distanceToTravel;
        
        public SkellyMove(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
            em = esm.GetComponent<EnemyManager>();
        }

        public override IEnumerator Start()
        {        
                // raycast a direction, if there is a wall pick new location
                Vector2 desiredJumpDirection = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
                RaycastHit2D hit = Physics2D.Raycast(esm.transform.position, desiredJumpDirection, em.leapingDistance, 1 << 12);
                Debug.Log(desiredJumpDirection * em.leapingDistance);
                Debug.Log(hit.collider);

                // if  wall, reroll
                if(hit)
                {
                    while(hit.collider.CompareTag("Wall"))
                    {
                        Debug.Log("rerolling");
                        desiredJumpDirection = new Vector2(Random.Range(-1,1), Random.Range(-1,1));
                        hit = Physics2D.Raycast(esm.transform.position, desiredJumpDirection, em.leapingDistance, 1 << 12);
                        yield return null;

                    }
                }

                

                // jump to location
                float timer = 1.5f;
                float step = em.moveSpeed * Time.deltaTime;
                step = 0.013f;
                //Debug.Log("starting spot: " + esm.transform.position);
                //Debug.Log("ending spot: " + (esm.transform.XandY() + desiredJumpDirection * em.leapingDistance));
                while(Vector2.Distance(esm.transform.position, em.leapingDistance * desiredJumpDirection + esm.transform.XandY()) > 0.15f && timer > 0)
                {
                    
                    esm.transform.position = Vector2.MoveTowards(esm.transform.position, em.leapingDistance * desiredJumpDirection + esm.transform.XandY(), step);
                    Debug.Log(esm.transform.position);
                    timer -= Time.deltaTime;
                    Debug.Log(timer);
                    yield return null;
                }



            _enemyStateMachine.SetState(new ThrowBone(esm));

        }

    }
}

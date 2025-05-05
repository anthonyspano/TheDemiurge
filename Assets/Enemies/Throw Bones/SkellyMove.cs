using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyMove : State
    {
        private EnemyStateMachine esm;
        private EnemyManager em;
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
                //Debug.Log(desiredJumpDirection * em.leapingDistance);
                //Debug.Log(hit.collider);

                // if  wall, reroll
                while(hit && hit.collider.CompareTag("Wall"))
                {
                    desiredJumpDirection = new Vector2(Random.Range(-1,1), Random.Range(-1,1));
                    hit = Physics2D.Raycast(esm.transform.position, desiredJumpDirection, em.leapingDistance, 1 << 12);
                    yield return null;

                }
                

                

                // jump to location
                float timer = 0;
                Vector3 startPosition = esm.transform.position;
                Vector3 targetPosition = em.leapingDistance * new Vector3(desiredJumpDirection.x, desiredJumpDirection.y, 0) + esm.transform.position;
                float jumpDuration = 0.5f;
                while(Vector2.Distance(esm.transform.position, (Vector2)targetPosition) > 0.15f && timer < jumpDuration)
                {
                    
                    esm.transform.position = Vector3.Lerp(startPosition, targetPosition, timer / jumpDuration); // timer / jumpDuration
                    //Debug.Log("Start Position: " + startPosition + " current position: " + esm.transform.position);
                    timer += Time.deltaTime;
                    yield return null;
                }

                esm.transform.position = targetPosition;



            _enemyStateMachine.SetState(new ThrowBone(esm));

        }

    }
}

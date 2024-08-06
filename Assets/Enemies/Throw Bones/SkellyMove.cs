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
            float distanceToPlayer = Vector2.Distance(esm.transform.position, PlayerManager.Instance.transform.position);
            // if too close to player, move away
            if(distanceToPlayer < 5)
            {
                Vector2 directionToPlayer = (PlayerManager.Instance.transform.position - esm.transform.position).normalized;
                distanceToTravel = em.RetreatRange;
                // if there is a wall, then cut target position short (raycast)
                RaycastHit2D hit = Physics2D.Raycast(esm.transform.position, directionToPlayer * new Vector2(-1,-1), em.RetreatRange, 1 << 12);
                if(hit)
                {
                    if(hit.collider.CompareTag("Wall"))
                    {
                        distanceToTravel = hit.distance;
                    }
                }

                // get target pos
                targetPos =  (Vector2)esm.transform.position + distanceToTravel * directionToPlayer * new Vector2(-1,-1);

                // while not at target position 
                while(Vector2.Distance(esm.transform.position, targetPos) > 1f)
                {
                    // Debug.Log("target: " + targetPos);
                    // Debug.Log("current: " + esm.transform.position);
                    // move to target
                    esm.transform.position = Vector2.MoveTowards(esm.transform.position, targetPos, em.moveSpeed * Time.deltaTime);
                    yield return null;
                }

            }
            // else if too far, move closer
            else if(distanceToPlayer > 10)
            {

                // while not in range of player 
                while(distanceToPlayer > 10)
                {
                    esm.transform.position = Vector2.MoveTowards(esm.transform.position, PlayerManager.Instance.transform.position, 0.1f);
                    distanceToPlayer = Vector2.Distance(esm.transform.position, PlayerManager.Instance.transform.position);
                    yield return null;
                }

            }


            _enemyStateMachine.SetState(new ThrowBone(esm));

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // bad code: tell skelly to move for 2 seconds max
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
            while(distanceToPlayer < 4)
            {
                yield return null;
                
                // move away from player
                Vector2 directionToPlayer = (PlayerManager.Instance.transform.position - esm.transform.position).normalized;
                distanceToTravel = 7;
                
                // if there is a collider, go to collider and stop before (raycast)
                RaycastHit2D hit = Physics2D.Raycast(esm.transform.position, directionToPlayer * new Vector2(-1,-1), 2f, 1 << 12);
                if(hit)
                {
                    if(hit.collider.CompareTag("Wall"))
                    {
                        if(hit.distance < 1)
                            distanceToTravel = hit.distance;
                        Debug.Log(hit.distance);
                    }
                }

                // get target pos
                //targetPos =  (Vector2)esm.transform.position + distanceToTravel * directionToPlayer * new Vector2(-1,-1);
                targetPos = (Vector2)PlayerManager.Instance.transform.position + directionToPlayer * distanceToTravel * new Vector2(-1,-1);

                // while not at target position 
                while(Vector2.Distance(esm.transform.position, targetPos) > 1f)
                {
                    // Debug.Log("target: " + targetPos);
                    // Debug.Log("current: " + esm.transform.position);
                    // move to target
                    //Debug.Log(targetPos);
                    Debug.Log("distance from player: " + Vector2.Distance(esm.transform.position, PlayerManager.Instance.transform.position));
                    esm.transform.position = Vector2.MoveTowards(esm.transform.position, targetPos, em.moveSpeed * Time.deltaTime);
                    yield return null;
                }

                distanceToPlayer = Vector2.Distance(esm.transform.position, PlayerManager.Instance.transform.position);

            }

            // while not in range of player 
            while(distanceToPlayer > 10)
            {
                Debug.Log("TOO FAR");
                esm.transform.position = Vector2.MoveTowards(esm.transform.position, PlayerManager.Instance.transform.position, 0.1f);
                distanceToPlayer = Vector2.Distance(esm.transform.position, PlayerManager.Instance.transform.position);
                yield return null;
            }

            


            _enemyStateMachine.SetState(new ThrowBone(esm));

        }

    }
}

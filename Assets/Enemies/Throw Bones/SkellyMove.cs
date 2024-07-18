using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyMove : State
    {
        private SkellyBattleSystem sbs;
        private EnemyManager em;
        private Vector2 targetPos;
        private float distanceToTravel;
        
        public SkellyMove(SkellyBattleSystem skellyBattleSystem) : base(skellyBattleSystem)
        {
            sbs = skellyBattleSystem;
            em = sbs.GetComponent<EnemyManager>();
        }

        public override IEnumerator Start()
        {        
            float distanceToPlayer = Vector2.Distance(sbs.transform.position, PlayerManager.Instance.transform.position);
            // if too close to player, move away
            if(distanceToPlayer < 5)
            {
                Vector2 directionToPlayer = (PlayerManager.Instance.transform.position - sbs.transform.position).normalized;
                distanceToTravel = em.RetreatRange;
                // if there is a wall, then cut target position short (raycast)
                RaycastHit2D hit = Physics2D.Raycast(sbs.transform.position, directionToPlayer * new Vector2(-1,-1), em.RetreatRange);
                if(hit)
                {
                    if(hit.collider.CompareTag("Wall"))
                    {
                        distanceToTravel = hit.distance;
                    }
                }

                // get target pos
                targetPos =  (Vector2)sbs.transform.position + distanceToTravel * directionToPlayer * new Vector2(-1,-1);

                // while not at target position 
                while(Vector2.Distance(sbs.transform.position, targetPos) > 0.1f)
                {
                    // move to target
                    sbs.transform.position = Vector2.MoveTowards(sbs.transform.position, targetPos, 0.1f);
                    yield return null;
                }

            }
            // else if too far, move closer
            else if(distanceToPlayer > 10)
            {

                // while not in range of player 
                while(distanceToPlayer > 10)
                {
                    sbs.transform.position = Vector2.MoveTowards(sbs.transform.position, PlayerManager.Instance.transform.position, 0.1f);

                    yield return null;
                }

            }

            SkellyBattleSystem.SetState(new ThrowBone(SkellyBattleSystem));

        }

    }
}

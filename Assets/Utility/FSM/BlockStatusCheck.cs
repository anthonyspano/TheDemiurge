using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class BlockStatusCheck : State
    {
        PlayerStateMachine psm;
        EnemyManager em;
        int myHealth;

        public BlockStatusCheck(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            psm = playerStateMachine;
            em = psm.GetComponent<EnemyManager>();
            myHealth = psm.GetComponent<EnemyTakeDamage>().healthSystem.GetHealth();
            
        }

        

        public override IEnumerator Start()
        {
            // 80% of max health
            // make this an enum status so it can be turned off
            if(myHealth < psm.GetComponent<EnemyTakeDamage>().maxHealth * .8f)
            {
                // do spin attack
            }

            
            yield return null;
            
        }
 
        
    }
}

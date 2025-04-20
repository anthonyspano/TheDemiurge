using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class EnemyStateMachine : StateMachine
    {
        [HideInInspector]
        public EnemyManager em;
        [HideInInspector]
        public GameObject attackPoint;
        
        void Start()
        {
            em = GetComponent<EnemyManager>();
            if(em.gameObject.CompareTag("Enemy"))
                SetState(new SkellyStart(this));  
            else if(em.gameObject.CompareTag("Charger"))
                SetState(new ChargerStart(this));

        }


    }

}
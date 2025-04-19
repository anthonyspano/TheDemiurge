using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyBattleSystem : StateMachine
    {
        [HideInInspector]
        public EnemyManager em;
        [HideInInspector]
        public GameObject attackPoint;

        void Start()
        {
            em = GetComponent<EnemyManager>();
            SetState(new SkellyStart(this));  
        }


    }

}
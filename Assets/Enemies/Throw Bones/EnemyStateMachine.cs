using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class EnemyStateMachine : StateMachine
    {
        [HideInInspector]
        public EnemyManager em;
        void Start()
        {
            em = GetComponent<EnemyManager>();
            SetState(new SkellyStart(this));  
        }


    }

}
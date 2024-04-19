using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyBattleSystem : StateMachine
    {
        public EnemyManager em;
        void Start()
        {
            em = GetComponent<EnemyManager>();
            SetState(new SkellyStart(this));  
        }


    }

}
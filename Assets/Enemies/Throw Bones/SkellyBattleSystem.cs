using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyBattleSystem : StateMachine
    {
        [HideInInspector]
        public EnemyManager em;
<<<<<<< Updated upstream:Assets/Enemies/Throw Bones/SkellyBattleSystem.cs
=======
        [HideInInspector]
        public GameObject attackPoint;

>>>>>>> Stashed changes:Assets/Enemies/Throw Bones/EnemyStateMachine.cs
        void Start()
        {
            em = GetComponent<EnemyManager>();
            SetState(new SkellyStart(this));  
        }


    }

}
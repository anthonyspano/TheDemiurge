using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // cleanup: shared variables for class
    public class BossBattleSystem : StateMachine
    {
        [HideInInspector]
        public EnemyManager em;
        private HealthBar healthBar;
        private Transform player;
        public Transform Player
        {
            get { return player; }
            set { player = value; }
        }

        private bool canMove = true;
        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }
        
        private bool dead = false;
        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        private void Start()
        {
            em = GetComponent<EnemyManager>();
            healthBar = GameObject.Find("BossHealthBar").GetComponent<HealthBar>();
            healthBar.Setup(transform.GetComponentInChildren<BossTakeDamage>().healthSystem);
            SetState(new BossEngage(this));
        }










    }

}
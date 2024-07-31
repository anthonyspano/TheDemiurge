using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add constructor here for each new implementation
namespace com.ultimate2d.combat
{
    public abstract class State
    {
        protected BlockBattleSystem BlockBattleSystem;
        protected PlayerStateMachine _playerStateMachine;
        protected BossBattleSystem BossBattleSystem;
        protected EnemyStateMachine _enemyStateMachine;

        public State(BlockBattleSystem blockBattleSystem)
        {
            BlockBattleSystem = blockBattleSystem;

        }

        public State(PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine  = playerStateMachine;
        }

        public State(BossBattleSystem bossBattleSystem)
        {
            BossBattleSystem = bossBattleSystem;
        }

        public State(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }
        
        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Attack()
        {
            yield break;
        }

        public virtual IEnumerator Heal()
        {
            yield break;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // set attacking trigger back to bool
    // animation event check after each "phase" of animation - if continuechain = false then play 'idle'
    public class PlayerAttack : State
    {
        private bool continueChain = false;
        private PlayerBattleSystem pbs;

        public PlayerAttack(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
        }
        public override IEnumerator Start() 
        {
            PlayerManager.Instance.CanMove = false;

            PlayerManager.Instance.GetComponent<Animator>().Play(new AnimatorHashRef().GetFirstAttackState());
            yield return null;

            yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);
            PlayerBattleSystem.SetState(new Begin(pbs));

                


        }
    }
}
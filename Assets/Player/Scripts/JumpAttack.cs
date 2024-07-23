using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{

    public class JumpAttack : State
    {
        PlayerBattleSystem pbs;
        Animator playerAnim;
        AudioSource playerAudio;

        public JumpAttack(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
            playerAnim = pbs.GetComponent<Animator>();
            playerAudio = pbs.GetComponent<AudioSource>();
        }

        public override IEnumerator Start()
        {

            playerAnim.Play(new AnimatorHashRef().GetFirstAttackState());
            playerAudio.Play();

            playerAnim.SetBool("inAir", true);
            
            yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.InAir);
            //Debug.Log(PlayerController.Instance.jumpTime);
            if(PlayerController.Instance.jumpTime > 0.1f)
                yield return new WaitForSeconds(0.5f);
            //Debug.Log(PlayerController.Instance.jumpTime);
            //yield return new WaitUntil(() => (!PlayerController.Instance.inAir));
            yield return new WaitForSeconds(PlayerController.Instance.jumpTime);
            playerAnim.SetBool("inAir", false);
            


            yield return new WaitUntil(() => PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Idle);

        }




    }
}
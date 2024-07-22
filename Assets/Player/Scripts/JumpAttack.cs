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
            Debug.Log(Time.time);
            playerAudio.Play();

            playerAnim.SetBool("inAir", true);
            yield return new WaitUntil(() => playerAnim.GetCurrentAnimatorStateInfo(0).IsName("v-jumpattack-dr-hold"));
            Debug.Log(Time.time);
            //yield return new WaitUntil(() => (!PlayerController.Instance.inAir));
            yield return new WaitForSeconds(PlayerController.Instance.jumpTime);
            playerAnim.SetBool("inAir", false);


            yield return null;
        }




    }
}
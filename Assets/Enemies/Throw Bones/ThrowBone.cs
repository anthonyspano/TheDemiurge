using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ThrowBone : State
    {
        private SkellyBattleSystem sbs;
        public ThrowBone(SkellyBattleSystem skellyBattleSystem) : base(skellyBattleSystem)
        {
            sbs = skellyBattleSystem;
        }

        public override IEnumerator Start()
        {
            
            // anim
            sbs.GetComponent<Animator>().Play("Attack");

            yield return null;
            yield return new WaitUntil(() => !sbs.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"));

            // instantiate bone object
            Transform bonerangPrefab = Resources.Load<Transform>("Bonerang");
            Transform bonerang = GameObject.Instantiate(bonerangPrefab, sbs.transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(bonerang.GetComponent<BoxCollider2D>(), sbs.transform.Find("Hitbox").GetComponent<BoxCollider2D>());

            // launch bone directly behind player
            bonerang.GetComponent<Bonerang>().Target = PlayerManager.Instance.transform.position;
            yield return new WaitUntil(() => bonerang.position == bonerang.GetComponent<Bonerang>().Target);

            // bonerang hovers
            yield return new WaitForSeconds(1);

            // have bone return to skelly
            bonerang.GetComponent<Bonerang>().Target = sbs.transform.position;
            Physics2D.IgnoreCollision(bonerang.GetComponent<BoxCollider2D>(), sbs.transform.Find("Hitbox").GetComponent<BoxCollider2D>(), false);

            yield return new WaitUntil(() => Vector3.Distance(bonerang.position, bonerang.GetComponent<Bonerang>().Target) < 1f);

            
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 1.25f));
            

            SkellyBattleSystem.SetState(new SkellyStart(SkellyBattleSystem));
        }


    }

}
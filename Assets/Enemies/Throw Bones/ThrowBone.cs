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
            Physics2D.IgnoreLayerCollision(sbs.gameObject.layer, sbs.gameObject.layer, true);
            sbs.GetComponent<AudioSource>().PlayOneShot(sbs.GetComponent<EnemyManager>().attackSound, 0.8f);

            // launch bone directly behind player
            var myBonerang = bonerang.GetComponent<Bonerang>();
            myBonerang.owner = sbs.transform;
            myBonerang.Target = PlayerManager.Instance.transform.position;

            yield return new WaitUntil(() => bonerang.position == myBonerang.Target);

            // bonerang hovers
            yield return new WaitForSeconds(1);

            // have bone return to skelly
            Physics2D.IgnoreCollision(bonerang.GetComponent<BoxCollider2D>(), sbs.transform.GetComponent<BoxCollider2D>(), false);
            myBonerang.isReturning = true;
            
            

            

            yield return new WaitUntil(() => Vector3.Distance(bonerang.position, myBonerang.Target) < 1f);

            
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 1.25f));
            

            SkellyBattleSystem.SetState(new SkellyStart(SkellyBattleSystem));
        }


    }

}
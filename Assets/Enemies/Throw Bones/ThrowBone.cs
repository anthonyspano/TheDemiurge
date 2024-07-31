using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ThrowBone : State
    {
        private EnemyStateMachine esm;
        public ThrowBone(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
        }

        public override IEnumerator Start()
        {
            
            // anim
            esm.GetComponent<Animator>().Play("Attack");

            yield return null;
            yield return new WaitUntil(() => !esm.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"));

            // instantiate bone object
            Transform bonerangPrefab = Resources.Load<Transform>("Bonerang");
            Transform bonerang = GameObject.Instantiate(bonerangPrefab, esm.transform.position, Quaternion.identity);
            Physics2D.IgnoreLayerCollision(esm.gameObject.layer, esm.gameObject.layer, true);
            esm.GetComponent<AudioSource>().PlayOneShot(esm.GetComponent<EnemyManager>().attackSound, 0.8f);

            // launch bone directly behind player
            var myBonerang = bonerang.GetComponent<Bonerang>();
            myBonerang.owner = esm.transform;
            myBonerang.Target = PlayerManager.Instance.transform.position;

            yield return new WaitUntil(() => bonerang.position == myBonerang.Target);

            // bonerang hovers
            yield return new WaitForSeconds(1);

            // have bone return to skelly
            Physics2D.IgnoreCollision(bonerang.GetComponent<BoxCollider2D>(), esm.transform.GetComponent<BoxCollider2D>(), false);
            myBonerang.isReturning = true;
            
            

            

            yield return new WaitUntil(() => Vector3.Distance(bonerang.position, myBonerang.Target) < 1f);

            
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 1.25f));
            

            _enemyStateMachine.SetState(new SkellyStart(esm));
        }


    }

}
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ThrowBone : State
    {
        private EnemyStateMachine esm;
        private AudioSource e_audioManager;
        public ThrowBone(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            esm = enemyStateMachine;
            e_audioManager = esm.transform.GetComponent<AudioSource>();
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

            // add force in the direction of the player
            var direction = (PlayerManager.Instance.transform.position - esm.transform.position).normalized;
            bonerang.GetComponent<Rigidbody2D>().AddForce(direction * esm.GetComponent<EnemyManager>().throwSpeed, ForceMode2D.Impulse);
            // have enemyaudiomanager play all enemy sounds to prevent clipping

            e_audioManager.GetComponent<AudioSource>().PlayOneShot(esm.GetComponent<EnemyManager>().attackSound, 0.7f);

            // launch bone directly behind player
            // var myBonerang = bonerang.GetComponent<Bonerang>();
            // myBonerang.owner = esm.transform;
            // myBonerang.Target = PlayerManager.Instance.transform.position;

            //yield return new WaitUntil(() => bonerang.position == myBonerang.Target);

            // have bone return to skelly
            // Physics2D.IgnoreCollision(bonerang.GetComponent<BoxCollider2D>(), esm.transform.GetComponent<BoxCollider2D>(), false);
            // myBonerang.isReturning = true;
            
            

            

            //yield return new WaitUntil(() => Vector3.Distance(bonerang.position, myBonerang.Target) < 1f);

            
            //yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 1.25f));
            yield return new WaitForSeconds(UnityEngine.Random.Range(1.25f, 2.25f));
            

            _enemyStateMachine.SetState(new SkellyStart(esm));
        }


    }

}
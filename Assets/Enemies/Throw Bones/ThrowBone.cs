using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("Throwing you a bone!");

            // instantiate bone object
            Transform bonerangPrefab = Resources.Load<Transform>("Bonerang");
            Transform bonerang = GameObject.Instantiate(bonerangPrefab, sbs.transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(bonerang.GetComponent<BoxCollider2D>(), sbs.transform.Find("Hitbox").GetComponent<BoxCollider2D>());

            // launch bone directly behind player
            Vector2 targetPos = PlayerManager.Instance.transform.position;
            while(bonerang.transform.XandY() != targetPos)
            {
                bonerang.transform.position = Vector2.MoveTowards(bonerang.transform.position, targetPos, 0.1f);
                yield return null;

            }

            // bonerang hovers
            yield return new WaitForSeconds(1);

            // have bone return to skelly
            targetPos = sbs.transform.position;
            Physics2D.IgnoreCollision(bonerang.GetComponent<BoxCollider2D>(), sbs.transform.Find("Hitbox").GetComponent<BoxCollider2D>(), false);
            while(bonerang.transform.XandY() != targetPos)
            {
                bonerang.transform.position = Vector2.MoveTowards(bonerang.transform.position, targetPos, 0.1f);
                yield return null;
            }



            yield break;
        }


    }

}
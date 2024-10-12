using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SpriteFlipToPlayer : MonoBehaviour
    {

        void Update()
        {
            GetComponent<SpriteRenderer>().flipX = PlayerManager.Instance.transform.position.x > transform.position.x;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat{


    public class PlayerHurt : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D col)
        {
            if(col.transform.CompareTag("Charger") || col.transform.CompareTag("Projectile"))
            {
                PlayerManager.Instance.pHealth.Damage(70);
                //audioSource.PlayOneShot(hurt1, 0.7f);

            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat{


    public class PlayerHurt : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip hurt1;
        private Animator animator;

        void Start()
        {
            audioSource = transform.parent.GetComponent<AudioSource>();
            animator = transform.parent.GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if(col.transform.CompareTag("Charger") || col.transform.CompareTag("Projectile"))
            {
                PlayerManager.Instance.pHealth.Damage(70);


            }
        }
    }

}
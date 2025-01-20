using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dedicated script for the explosion animation
public class BlowUp : MonoBehaviour
{
    Animator anim;
    public AudioClip explosionSound;
    void Start()
    {
        anim = GetComponent<Animator>();

        // play explosion animation
        anim.Play("Explosive Death", 0);
    }

    public void PlayExplosionSound()
    {
        // play explosion sound
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(explosionSound);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

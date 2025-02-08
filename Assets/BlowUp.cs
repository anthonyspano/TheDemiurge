using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dedicated script for the explosion animation
public class BlowUp : MonoBehaviour
{
    Animator anim;
    public AudioClip explosionSound;
    private AudioSource soundChannel;

    void Start()
    {
        anim = GetComponent<Animator>();

        soundChannel = GameObject.Find("SoundChannel1").GetComponent<AudioSource>();

        // play explosion animation
        anim.Play("Explosive Death", 0);
    }

    public void PlayExplosionSound()
    {
        // play explosion sound
        if(!soundChannel.isPlaying)
            soundChannel.PlayOneShot(explosionSound);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

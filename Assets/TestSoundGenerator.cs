using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundGenerator : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    private EnemySoundQueue esq;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        esq = GetComponent<EnemySoundQueue>();

        StartCoroutine(RunTest());
    }

    void Update()
    {
        
    }

    IEnumerator RunTest() {
        // play six times
        audioSource.PlayOneShot(audioClip, 0.75f);
        audioSource.PlayOneShot(audioClip, 0.75f);
        audioSource.PlayOneShot(audioClip, 0.75f);
        audioSource.PlayOneShot(audioClip, 0.75f);
        audioSource.PlayOneShot(audioClip, 0.75f);
        audioSource.PlayOneShot(audioClip, 0.75f);


        yield return new WaitForSeconds(2f);
        
        // send 6 requests
        esq.AddRequest(audioClip, 0.75f);
        esq.AddRequest(audioClip, 0.75f);
        esq.AddRequest(audioClip, 0.75f);
        esq.AddRequest(audioClip, 0.75f);
        esq.AddRequest(audioClip, 0.75f);
        esq.AddRequest(audioClip, 0.75f);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class prevents stacking of sounds
// if two sounds request in the same frame 
public class EnemySoundQueue : MonoBehaviour
{
    private AudioSource audioSource;

    private List<SoundRequest> soundRequestQueue;

    private static EnemySoundQueue _instance;

    public static EnemySoundQueue Instance 
    {
        get { return _instance; }
    }

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        soundRequestQueue = new List<SoundRequest>();
    }

    
    void Update()
    {
        // clear the request queue
        if(soundRequestQueue.Count > 0)
            PlayRequests();

        // clear request queue
        soundRequestQueue.Clear();

    }

    void PlayRequests() {
        // find the queued sound that is closest to the player
        SoundRequest chosenRequest = soundRequestQueue[0];
        
        foreach(SoundRequest sr in soundRequestQueue) {
            if(sr.volume > chosenRequest.volume) {
                chosenRequest = sr;
            }
        }

        if(chosenRequest != null) {
            audioSource.PlayOneShot(chosenRequest.audioClip, chosenRequest.volume);
        }
    

    }

    public void AddRequest(AudioClip sound, float volume) {
        soundRequestQueue.Add(new SoundRequest(sound, volume));
    }
}

public class SoundRequest {
    private AudioClip _audioClip;
    private float _volume;

    public SoundRequest (AudioClip audioClip, float volume) {
        _audioClip = audioClip;
        _volume = volume;
    }

    public AudioClip audioClip {
        get { return _audioClip; }
    }

    public float volume {
        get { return _volume; }
    }
}
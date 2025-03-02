using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.ultimate2d.combat;

public class Gashadokuro : MonoBehaviour
{
    LineRenderer lineRenderer;
    LineRenderer rightEyeLineRenderer;

    bool isEnabled;

    public int damage;

    Vector3 desiredPosition;

    private Transform leftEye;
    private Transform rightEye;

    private AudioSource audioSource;
    public AudioClip laserSounds;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();  
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("LoadLasers");

        leftEye = transform.GetChild(0).transform;
        rightEye = transform.GetChild(1).transform;
        
        rightEyeLineRenderer = rightEye.gameObject.GetComponent<LineRenderer>();
    }

    void Update() 
    {
        transform.position = PlayerManager.Instance.transform.position + new Vector3(10f, 0, 0f);
        if(isEnabled) 
        {
            FireLasers();
        }

    }

    IEnumerator LoadLasers()
    {
        yield return new WaitForSeconds(1.2f);
        isEnabled = true;

        Debug.Log("sound");
        audioSource.PlayOneShot(laserSounds, 0.7f);

        // yield end of sound
        yield return new WaitForSeconds(laserSounds.length);

        // stop playing
        audioSource.Stop();
    }

    void FireLasers() 
    {
        // line properties
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;  
        lineRenderer.endWidth = 0.05f;  
        lineRenderer.sortingLayerName = "GUI";

        rightEyeLineRenderer.positionCount = 2;
        rightEyeLineRenderer.startWidth = 0.05f;  
        rightEyeLineRenderer.endWidth = 0.05f;  
        rightEyeLineRenderer.sortingLayerName = "GUI";

        lineRenderer.SetPosition(0, new Vector3(leftEye.position.x, leftEye.position.y, -5)); // lefteye - localpos
        lineRenderer.SetPosition(1, new Vector3(PlayerManager.Instance.transform.position.x, PlayerManager.Instance.transform.position.y, -5));

        rightEyeLineRenderer.SetPosition(0, new Vector3(rightEye.position.x, rightEye.position.y, -5)); // lefteye - localpos
        rightEyeLineRenderer.SetPosition(1, new Vector3(PlayerManager.Instance.transform.position.x, PlayerManager.Instance.transform.position.y, -5));

        PlayerManager.Instance.pHealth.Damage(damage);
    }

}

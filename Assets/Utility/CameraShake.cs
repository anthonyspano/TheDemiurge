using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{

    // Camera transform
    private Transform transform;

    private float shakeDuration = 0f;

    private float shakeMagnitude = 0.3f;
    
    // how fast the shake goes away
    private float dampingSpeed = 2.0f;

    private Vector3 initialPosition;

    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
        
        // initialPosition = main camera's position?
        //initialPosition = initialPosition = transform.position;
    }

    private void OnEnable()
    {
        // set to main camera's position
        //initialPosition = initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            var newPosition = (Vector3)(com.ultimate2d.combat.PlayerManager.Instance.transform.localPosition + Random.insideUnitSphere * shakeMagnitude);
            transform.localPosition = new Vector3(newPosition.x, newPosition.y, -6f);
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = new Vector3(com.ultimate2d.combat.PlayerManager.Instance.transform.localPosition.x,
                                                  com.ultimate2d.combat.PlayerManager.Instance.transform.localPosition.y, -6f);
        }
    }

    public void TriggerShake(float d)
    {
        // set damping speed on call
        shakeMagnitude = d;
        
        shakeDuration = 0.6f;
    }
    
}

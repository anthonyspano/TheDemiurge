using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.ultimate2d.combat;

public class Gashadokuro : MonoBehaviour
{
    LineRenderer lineRenderer;

    bool isEnabled;

    public int damage;

    Vector3 desiredPosition;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();  
        
        StartCoroutine("LoadLasers");

        
    }

    void Update() 
    {
        transform.position = PlayerManager.Instance.transform.position + new Vector3(10f, 0, -5f);
        if(isEnabled) 
        {
            FireLasers();
        }

    }

    IEnumerator LoadLasers()
    {
        yield return new WaitForSeconds(1.2f);
        isEnabled = true;
    }

    void FireLasers() 
    {
        // line properties
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;  
        lineRenderer.endWidth = 0.05f;  
        lineRenderer.sortingLayerName = "GUI";
        //lineRenderer.material.color = Color.yellow;
        //lineRenderer.endColor = Color.yellow;

        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -5)); // lefteye - localpos
        lineRenderer.SetPosition(1, new Vector3(PlayerManager.Instance.transform.position.x, PlayerManager.Instance.transform.position.y, -5));

        PlayerManager.Instance.pHealth.Damage(damage);
    }

}

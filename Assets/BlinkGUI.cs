using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script just blinks the prompt
public class BlinkGUI : MonoBehaviour
{
    SpriteRenderer sr;
    private bool isCovered;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while(true)
        {

            yield return new WaitForSeconds(0.75f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.75f);
            sr.enabled = true;
            
        }
    }
}

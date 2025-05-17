using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPulsate : MonoBehaviour
{
    public float pulsateSpeed;
    float maxAlpha;
    public Image affectedImage;

    void Start()
    {
        maxAlpha = .45f;
        affectedImage = GetComponent<Image>();
        StartCoroutine(PulsateToTime());    
    }

    IEnumerator PulsateToTime()
    {
        bool increasing = false;
        while(true)
        {
            if(increasing && affectedImage.color.a <= maxAlpha)
            {
                affectedImage.color = new Color(affectedImage.color.r, 
                                                affectedImage.color.g, 
                                                affectedImage.color.b, 
                                                affectedImage.color.a + pulsateSpeed * Time.deltaTime);
                
            }
            else
            {
                increasing = false;
            }

            yield return null;
            
            if(!increasing && affectedImage.color.a > 0f)
            {
                affectedImage.color = new Color(affectedImage.color.r, 
                                                affectedImage.color.g, 
                                                affectedImage.color.b, 
                                                affectedImage.color.a - pulsateSpeed * Time.deltaTime);
                
            }
            else
            {
                increasing = true;
            }

            if(affectedImage.color.a < 0f)
            {
                yield return new WaitForSeconds(0.3f);
            }
            

            


        }
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSwitch : MonoBehaviour
{
    private bool interacted;
    public GameObject trapObject;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(!interacted)
        {
            interacted = true;

            // spawn trap
            var trap = Instantiate(trapObject, transform.position, Quaternion.identity);

            
        }
    }
}

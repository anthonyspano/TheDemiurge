using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Object icon;
    private Object prompt;
    protected bool interacted;

    private void Start()
    {
        prompt = GameObject.Instantiate(icon, transform.position + new Vector3(0,-0.75f,50), Quaternion.identity);


    }

    protected void OnTriggerStay2D(Collider2D col)
    {

        if(PlayerInput.Interact() && !interacted)
        {
            interacted = true;
            
            // remove prompt
            Object.Destroy(prompt);
           
            Trigger();
        }

    }


    protected virtual void Trigger(){}
}

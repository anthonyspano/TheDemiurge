using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLevel : Interactable
{

    protected override void Trigger()
    {
        // tell game manager to start the level
        Debug.Log("starting level");
        GameManager.Instance.BeginLevel();
        

        
        
    }
}

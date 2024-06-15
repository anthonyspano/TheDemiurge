using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ActivateLevel : Interactable
    {

        protected override void Trigger()
        {
            // tell game manager to start the level
            Debug.Log("starting level");
            GameManager.Instance.BeginLevel();        
            
        }

    }
}
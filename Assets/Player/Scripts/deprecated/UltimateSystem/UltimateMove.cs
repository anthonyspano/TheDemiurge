using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class UltimateMove : MonoBehaviour
    {
        private UltimateBar ultBar;
        private bool ultReady;
        public event EventHandler FireUltAnim;

        private void Start()
        {
            ultBar = PlayerManager.Instance.GetComponent<UltimateBar>();
            ultBar.OnUltReady += ReadyUlt;


        }

        private void ReadyUlt(object sender, EventArgs e)
        {
            // see PowerManager.cs

            //ultReady = true;

            // light up icon

        }
        
        private void Update()
        {
            // Debug Only
            if (Input.GetKeyDown(KeyCode.F))
            {
                ultBar.AddUlt(100);
            }

            if (ultReady && PlayerInput.Ultimate())
            {
                Debug.Log("Ultimate!");
                ultReady = false;
                UseUlt();
            }
        }
        
        private void UseUlt()
        {

            // play ult anim
            if(FireUltAnim != null) FireUltAnim(this, EventArgs.Empty);
            
            // empty ult bar
            ultBar.SetUlt(0);

            // disable icon


        }


    }
}
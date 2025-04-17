using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class RotateAroundPlayer : MonoBehaviour
    {

        private float x;
        private float y;

        private Vector2 lastMove;
        public int Z_offset;
        public float cursorDistanceFromPlayer;
        
        private void Update()
        {
            x = Input.GetAxis(PlayerInput.x);
            y = Input.GetAxis(PlayerInput.y);
            Vector3 moveDirection = new Vector3(x, y);

            // if if not firing ult
            // set position

            // if firing ult   
            if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.y) > 0.1f)
            {
                moveDirection.Normalize();
                
                // pos
                var pos = PlayerManager.Instance.transform.XandY();
                pos += (Vector2)moveDirection * cursorDistanceFromPlayer; // 1 is good for final
                transform.position = pos;

                // rotate
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) *
                            Mathf.Rad2Deg;     // return angle in radians tan(y/x)
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward - new Vector3(0,0, Z_offset)); // needs to be flipped
                
            }
        }

    } 
}
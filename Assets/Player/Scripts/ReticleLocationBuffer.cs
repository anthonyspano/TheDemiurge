using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// buffer that saves direction inputs and changes position of reticle if x frames are in buffer
// minInputsForAction - this is the buffer system window, set accordingly
namespace com.ultimate2d.combat
{
    public class ReticleLocationBuffer : MonoBehaviour
    {
        private float x;
        private float y;

        private Vector2 lastMove;
        public int Z_offset;
        public float cursorDistanceFromPlayer;

        private List<Vector3> playerMovementInputs;

        public int maxBufferSize; // how many frames back do you want to read?
        public int minInputsForAction; // how close to zero inputs do you want to stop reading?

        void Start()
        {
            playerMovementInputs = new List<Vector3>();
        }
        
        private void Update()
        {
            x = Input.GetAxis(PlayerInput.x);
            y = Input.GetAxis(PlayerInput.y);
            Vector3 moveDirection = new Vector3(x, y);
            moveDirection.Normalize();
            // create a buffer that saves direction inputs and changes position of reticle if x frames are in buffer
            if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.y) > 0.1f)
            {
                // save to buffer
                playerMovementInputs.Add(moveDirection);
                
            }

            // maintain size each frame
            if(playerMovementInputs.Count > maxBufferSize)
            {
                // remove oldest item
                playerMovementInputs.RemoveAt(0); 
            }

            // only execute movement if buffer list is full enough
            if(playerMovementInputs.Count > minInputsForAction)
            {
                // move direction will be replaced by buffer input of oldest frame
                // pos
                var pos = PlayerManager.Instance.transform.XandY();
                pos += (Vector2)playerMovementInputs[0] * cursorDistanceFromPlayer; // 1 is good for final
                transform.position = pos;

                // rotate
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) *
                            Mathf.Rad2Deg;     // return angle in radians tan(y/x)
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward - new Vector3(0,0, Z_offset)); // needs to be flipped

                // remove currently read element of list
                playerMovementInputs.RemoveAt(0);
            }

        }
    }
}
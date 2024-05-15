using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static com.ultimate2d.combat.PlayerController;

// test class that uses a list for the buffer
namespace com.ultimate2d.combat
{
    public class InputBufferMemory
    {
        public int frame;
        public PlayerController.PlayerStatus action;

        public InputBufferMemory(int i, PlayerController.PlayerStatus a)
        {
            frame = i;
            action = a;
        }
    }

    // record player inputs into a buffer and execute the nearest one available
    public class PlayerInputBuffer : MonoBehaviour
    {
        List<InputBufferMemory> InputBuffer;
        public int bufferSize; // size of the ring buffer
        public float bufferExpiration; // amount of frames before an input is allowed to stay in the buffer
        public float bufferCleanupTime; // amount of time (seconds) before the buffer gets checked again

        void Start()
        {
            InputBuffer = new List<InputBufferMemory>();
            StartCoroutine("Cleanup");
        }

        void Update()
        {
            Debug.Log(PlayerManager.Instance.isBusy);
            // execute input if not currently executing
            if(!PlayerManager.Instance.isBusy)
            {

                if(InputBuffer.Count > 0)
                {
                    switch(InputBuffer[0].action)
                    {
                        case PlayerController.PlayerStatus.Attack:
                            PlayerController.Instance.playerStatus = PlayerStatus.Attack;
                            // Play attack animation once based on player's current direction
                            PlayerManager.Instance.anim.SetBool("isAttacking", true);
                            break;

                        case PlayerController.PlayerStatus.Ultimate:
                            if(PlayerManager.Instance.ultReady)
                            { 
                                PlayerController.Instance.playerStatus = PlayerStatus.Ultimate;
                                PlayerManager.Instance.FireUltimate();
                            }

                            break;

                        default:
                            PlayerManager.Instance.isBusy = false;
                            break;
                    }

                    InputBuffer.RemoveAt(0);
                }
            }

        }

        public void Add(InputBufferMemory ibm)
        {
            // if size > x, removeat(x)
            if(InputBuffer.Count > bufferSize)
            {
                InputBuffer.RemoveAt(0);
            }
            else
                InputBuffer.Add(ibm);

        }

        public IEnumerator Cleanup()
        {
            // remove stale cache every second - bufferExpiration
            foreach (InputBufferMemory buffer in InputBuffer)
            {
                if(Time.frameCount - buffer.frame > bufferExpiration)
                    InputBuffer.Remove(buffer);
            }

            yield return new WaitForSeconds(bufferCleanupTime);

            StartCoroutine("Cleanup");
        }







    }
}
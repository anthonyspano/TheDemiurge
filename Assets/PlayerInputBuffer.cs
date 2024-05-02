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

        void Start()
        {
            InputBuffer = new List<InputBufferMemory>();
        }

        void Update()
        {
            // execute input if not currently executing
            if(!PlayerManager.Instance.isBusy)
            {
                PlayerManager.Instance.isBusy = true;
                if(InputBuffer.Count > 0)
                {
                    switch(InputBuffer[0].action)
                    {
                        case PlayerController.PlayerStatus.Attack:
                            PlayerController.Instance.playerStatus = PlayerStatus.Attack;
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
            InputBuffer.Add(ibm);

        }

        public void Cleanup()
        {
            // remove cache every few seconds
        }







    }
}
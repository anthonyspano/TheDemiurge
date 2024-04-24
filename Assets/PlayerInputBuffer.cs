using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // record player inputs into a buffer and execute the nearest one available
    public class PlayerInputBuffer : MonoBehaviour
    {
        List<PlayerController.PlayerStatus> InputBuffer;

        void Start()
        {
            InputBuffer = new List<PlayerController.PlayerStatus>();
        }

        void Update()
        {
            // execute input if not currently executing
            if(!PlayerManager.Instance.isBusy)
            {
                if(InputBuffer.Count > 0)
                {
                    switch(InputBuffer[0])
                    {
                        case PlayerController.PlayerStatus.Attack:
                            //Debug.Log("attacking now");
                            PlayerManager.Instance.isBusy = true;
                            PlayerManager.Instance.TestAttack();
                            break;

                        case PlayerController.PlayerStatus.Ultimate:
                            //perform ultimate
                            break;

                        default:
                            break;
                    }

                    Debug.Log(InputBuffer.Count);
                    InputBuffer.RemoveAt(0);
                }
            }

        }

        public void Add(PlayerController.PlayerStatus s)
        {
            InputBuffer.Add(s);

        }







    }
}
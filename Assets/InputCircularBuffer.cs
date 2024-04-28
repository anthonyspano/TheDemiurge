using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{


    public class InputCircularBuffer : MonoBehaviour
    {
        Dictionary<int, PlayerController.PlayerStatus> InputBuffer;

        void Start()
        {
            InputBuffer = new Dictionary<int, PlayerController.PlayerStatus>();
        }

        void Update()
        {

            if(!PlayerManager.Instance.isBusy)
            {
                // key - frame, value - status
                // get first key of dictionary
                // switch(InputBuffer[key])
            
                //int frame;
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
                //InputBuffer.RemoveAt(0);
            
            }



        }


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// input buffer using a dictionary
namespace com.ultimate2d.combat
{


    public class InputCircularBuffer : MonoBehaviour
    {
        // <K: frame, V: PlayerStatus>
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
                // check dictionary for most recent frame
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
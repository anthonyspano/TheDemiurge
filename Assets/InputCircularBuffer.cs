using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualStudio.Utilities;

public class InputCircularBuffer : MonoBehaviour
{
    CircularBuffer<Dictionary> InputBuffer;

    void Start()
    {
        InputBuffer = new CircularBuffer<Dictionary>();
    }

    void Update()
    {

        if(!PlayerManager.Instance.isBusy)
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
            //InputBuffer.RemoveAt(0);
        
        }



    }


}

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
        private static PlayerInputBuffer _instance;
        public static PlayerInputBuffer Instance
        {
            get { return _instance; }
        }
        
        public List<InputBufferMemory> InputBuffer;
        public int bufferSize; // size of the ring buffer

        public int index = 0; 

        void Awake()
        {
            // singleton
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        void Start()
        {
            InputBuffer = new List<InputBufferMemory>();

            for(int i = bufferSize; i > 0; i--)
            {
                InputBuffer.Add(new InputBufferMemory(0, PlayerController.PlayerStatus.Neutral));
            }
        }

        void Update()
        {
            // execute input if not currently executing
            // every frame either 
            // a) execute an action, 
            // b) 

            // if(!PlayerManager.Instance.isBusy)
            // {
            //     switch(InputBuffer[index % bufferSize].action)
            //     {
            //         case PlayerController.PlayerStatus.Attack:
            //             if(PlayerManager.Instance.attackCooldownEnabled) 
            //                 break;
            //             PlayerManager.Instance.isBusy = true;
            //             PlayerController.Instance.playerStatus = PlayerStatus.Attack;
            //             // Play attack animation once based on player's current direction
            //             switch(PlayerManager.Instance.pFacingDir)
            //             {
            //                 case PlayerManager.Direction.right:
            //                     PlayerManager.Instance.anim.Play("v-attack-dr-1");
            //                     break;
            //                 case PlayerManager.Direction.left:
            //                     PlayerManager.Instance.anim.Play("Player_Atk_Left");
            //                     break;
            //                 case PlayerManager.Direction.down:
            //                     PlayerManager.Instance.anim.Play("v-attack-dr-1");
            //                     break;
            //                 case PlayerManager.Direction.up:
            //                     PlayerManager.Instance.anim.Play("Player_Atk_Up");
            //                     break;                                                                        
            //                 default:
            //                     break;
            //             }
            //             break;

            //         case PlayerController.PlayerStatus.Ultimate:
            //             if(PlayerManager.Instance.ultReady)
            //             { 
            //                 PlayerController.Instance.playerStatus = PlayerStatus.Ultimate;
            //                 PlayerManager.Instance.isBusy = true;
            //                 PlayerManager.Instance.FireUltimate();
            //             }
            //             break;

            //         default:
            //             PlayerManager.Instance.isBusy = false;
            //             break;
            //     }
                
            Add(new InputBufferMemory(Time.frameCount, PlayerController.PlayerStatus.Neutral));
                
            // }
            // else if(InputBuffer[index % bufferSize].action == PlayerController.PlayerStatus.Attack)
            // {
            //     PlayerManager.Instance.continueChain = true;
            //     index++;
            // }





        }

        public void Add(InputBufferMemory ibm)
        {
            index++;
            index = index % bufferSize;
            InputBuffer[index] = ibm;

        }


    }
}
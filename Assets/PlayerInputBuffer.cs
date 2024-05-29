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
        public float bufferExpiration; // amount of frames before an input is allowed to stay in the buffer
        public float bufferCleanupTime; // amount of time (seconds) before the buffer gets checked again

        private int index = 0; 

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
            if(!PlayerManager.Instance.isBusy)
            {

                if(InputBuffer.Count > 0)
                {
                    switch(InputBuffer[0].action)
                    {
                        case PlayerController.PlayerStatus.Attack:
                            PlayerManager.Instance.isBusy = true;
                            PlayerController.Instance.playerStatus = PlayerStatus.Attack;
                            // Play attack animation once based on player's current direction
                            switch(PlayerManager.Instance.pFacingDir)
                            {
                                case PlayerManager.Direction.right:
                                    PlayerManager.Instance.anim.Play("Player_Atk_Right");
                                    break;
                                case PlayerManager.Direction.left:
                                    PlayerManager.Instance.anim.Play("Player_Atk_Left");
                                    break;
                                case PlayerManager.Direction.down:
                                    PlayerManager.Instance.anim.Play("Player_Atk_Down");
                                    break;
                                case PlayerManager.Direction.up:
                                    PlayerManager.Instance.anim.Play("Player_Atk_Up");
                                    break;                                                                        
                                default:
                                    break;
                            }
                            break;

                        case PlayerController.PlayerStatus.Ultimate:
                            if(PlayerManager.Instance.ultReady)
                            { 
                                PlayerController.Instance.playerStatus = PlayerStatus.Ultimate;
                                PlayerManager.Instance.isBusy = true;
                                PlayerManager.Instance.FireUltimate();
                            }

                            break;

                        default:
                            PlayerManager.Instance.isBusy = false;
                            break;
                    }

                    Add(new InputBufferMemory(Time.frameCount, PlayerController.PlayerStatus.Neutral));
                }
            }
            // else if(attack anim is playing), set continue chain to true
            // set isAttacking to true at beginning of attack, then false when it is exiting
            else if(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Attack)
            {
                PlayerManager.Instance.continueChain = true;
            }


        }

        public void Add(InputBufferMemory ibm)
        {
            index++;
            index = index % bufferSize;
            InputBuffer[index] = ibm;

        }


    }
}
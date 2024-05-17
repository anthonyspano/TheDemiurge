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
            StartCoroutine("Cleanup");
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
            for (int i = 0; i < InputBuffer.Count; i++)
            {
                if(Time.frameCount - InputBuffer[i].frame > bufferExpiration)
                {
                    InputBuffer.RemoveAt(i);
                    i--;
                }
            }


            yield return new WaitForSeconds(bufferCleanupTime);

            StartCoroutine("Cleanup");
        }







    }
}
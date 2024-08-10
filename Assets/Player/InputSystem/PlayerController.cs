using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.ultimate2d.combat
{
    // player controller using custom input system
    public class PlayerController : MonoBehaviour
    {
        // singleton
        private static PlayerController _instance;
        public static PlayerController Instance
        {
            get { return _instance; }
        }


        private Animator anim;
        private Vector2 inputVector; 
        private Vector2 currentInputVector = Vector2.zero; 
        private Vector2 smoothInputVelocity; 
        public float acceleration;
        public float DeadZone;

        // input buffer
        PlayerInputBuffer _playerInputBuffer;

        public float movementSpeed;

        public enum PlayerStatus {Idle, Move, LightAttack, Ultimate, Sweep, JumpAttack, Neutral, InAir, Falling};
        public PlayerStatus playerStatus;

        // jump attack
        public float jumpTime;
        private float startTime;


        private void Awake()
        {
            // singleton
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            
            anim = GetComponent<Animator>();

            playerStatus = PlayerStatus.Idle;

            _playerInputBuffer = PlayerManager.Instance.GetComponent<PlayerInputBuffer>();
        }

        void Update()
        {
            if(PlayerInput.Ultimate())
            {
                _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.Ultimate));
            }
            if(PlayerInput.JumpAttackDown())
            {
                startTime = Time.time;
                _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.JumpAttack)); 
            }

            if(PlayerInput.JumpAttackHold())
            {                
                jumpTime = Time.time - startTime;
            }

            if(playerStatus == PlayerStatus.Idle || playerStatus == PlayerStatus.InAir)
            {
                Vector2 currentInputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                if(currentInputVector.magnitude > DeadZone)
                {
                    // movement dampering
                    //currentInputVector = Vector2.SmoothDamp(currentInputVector, inputVector, ref smoothInputVelocity, acceleration);
                    
                    PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, 
                                                                                    (Vector2)PlayerManager.Instance.transform.position + currentInputVector, 
                                                                                    PlayerManager.Instance.moveSpeed * Time.deltaTime);
                    PlayerManager.Instance.anim.SetBool("isMoving", true);

                }
                else
                    PlayerManager.Instance.anim.SetBool("isMoving", false);

                



            }

        }

        // public void Sweep(InputAction.CallbackContext context)
        // {
        //     // take frame/timestamp that the button was pressed
        //    _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.Sweep)); 
        //    Debug.Log("sweep!");

        // }

        public void SetPlayerInAir()
        {
            playerStatus = PlayerStatus.InAir;
        }

        public void SetPlayerNeutral()
		{
			playerStatus = PlayerStatus.Neutral;
            PlayerManager.Instance.anim.SetBool("isMoving", false);
            PlayerManager.Instance.anim.Play("Player Idle");
		}

        public bool CommandReady()
        {
            if(playerStatus != PlayerStatus.Idle ||
               playerStatus != PlayerStatus.Neutral)
            {
                return true;
            }


            return false;
        }


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.ultimate2d.combat
{
    public class PlayerController : MonoBehaviour
    {
        // singleton
        private static PlayerController _instance;
        public static PlayerController Instance
        {
            get { return _instance; }
        }


        private Animator anim;
        private PlayerInputActions playerInputActions;
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

            playerInputActions = new PlayerInputActions();
            // playerInputActions.Keyboard.Enable();
            playerInputActions.Player.Enable();
            playerInputActions.Player.JumpAttack.performed += JumpAttack;
            playerInputActions.Player.Sweep.performed += Sweep;
            playerInputActions.Player.Attack.performed += Attack;
            playerInputActions.Player.Ultimate.performed += Ultimate;
            playerInputActions.Player.Movement.performed += Movement;
            //playerInputActions.Player.JumpAttack.duration

            
            //playerInputActions.Player.Movement.performed += c => Debug.Log(c.ReadValue<Vector2>());

            _playerInputBuffer = PlayerManager.Instance.GetComponent<PlayerInputBuffer>();
        }

        private void FixedUpdate()
        {
            // move

            if(playerStatus == PlayerStatus.Idle || playerStatus == PlayerStatus.InAir)
            {
                inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
                // if(inputVector.magnitude > DeadZone)
                // {
                //     // create  move direction
                //     var direction = new Vector3(inputVector.x, inputVector.y * PlayerManager.Instance.verticalRunMod, 0) + PlayerManager.Instance.transform.position;
                //     // multiply move vector by speed 
                //     PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, direction, PlayerManager.Instance.moveSpeed * Time.deltaTime);
                //     PlayerManager.Instance.anim.SetBool("isMoving", true);
                // }
                // else
                // {
                //     PlayerManager.Instance.anim.SetBool("isMoving", false);
                // }

                currentInputVector = Vector2.SmoothDamp(currentInputVector, inputVector, ref smoothInputVelocity, acceleration);
                
                if(currentInputVector.magnitude > DeadZone)
                {
                    PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, (Vector2)PlayerManager.Instance.transform.position + currentInputVector, PlayerManager.Instance.moveSpeed * Time.deltaTime);
                    PlayerManager.Instance.anim.SetBool("isMoving", true);
                }
                else
                    PlayerManager.Instance.anim.SetBool("isMoving", false);

            }

            // jump attack air time
            if(Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                startTime = Time.time;

            }

            if(Input.GetKey(KeyCode.JoystickButton2))
            {
                jumpTime = Time.time - startTime;
                if(jumpTime > 0.4f)
                    jumpTime = 0.4f;
                

            }


        }

        public void Ultimate(InputAction.CallbackContext context)
        {
            
            _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.Ultimate));

            // for(int i = 0; i<_playerInputBuffer.InputBuffer.Count; i++)
            // {
            //     if(_playerInputBuffer.InputBuffer[i].action == PlayerStatus.Ultimate)
            //         Debug.Log("ULTIMATE REGISTERED");
            // }
        }


        public void Attack(InputAction.CallbackContext context)
        {
            // take frame/timestamp that the button was pressed
           _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.LightAttack)); 
           

        }

        public void Sweep(InputAction.CallbackContext context)
        {
            // take frame/timestamp that the button was pressed
           _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.Sweep)); 
           Debug.Log("sweep!");

        }

        public void JumpAttack(InputAction.CallbackContext context)
        {
            _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.JumpAttack)); 
            
        }

        public void Movement(InputAction.CallbackContext context)
        {
            // see fixed update
        }

        public void SetPlayerInAir()
        {
            playerStatus = PlayerStatus.InAir;
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
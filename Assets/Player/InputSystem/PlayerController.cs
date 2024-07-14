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
        public float DeadZone;

        // input buffer
        PlayerInputBuffer _playerInputBuffer;

        public float movementSpeed;

        public enum PlayerStatus {Idle, Move, LightAttack, Ultimate, Sweep, JumpAttack, Neutral};
        public PlayerStatus playerStatus;

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

            
            //playerInputActions.Player.Movement.performed += c => Debug.Log(c.ReadValue<Vector2>());

            _playerInputBuffer = PlayerManager.Instance.GetComponent<PlayerInputBuffer>();
        }

        private void FixedUpdate()
        {
            // move
            inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
            if(playerStatus == PlayerStatus.Idle)
            {
                if(inputVector.magnitude > DeadZone)
                {
                    // create  move direction
                    var direction = new Vector3(inputVector.x, inputVector.y * PlayerManager.Instance.verticalRunMod, 0) + PlayerManager.Instance.transform.position;
                    // multiply move vector by speed 
                    PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, direction, PlayerManager.Instance.moveSpeed * Time.deltaTime);
                    PlayerManager.Instance.anim.SetBool("isMoving", true);
                }
                else
                {
                    PlayerManager.Instance.anim.SetBool("isMoving", false);
                }

            }

            

        }

        public void Ultimate(InputAction.CallbackContext context)
        {
            _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.Ultimate));
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
            Debug.Log("jump!");
        }

        public void Movement(InputAction.CallbackContext context)
        {
            // see fixed update
        }



    }

}
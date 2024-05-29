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

        // input buffer
        PlayerInputBuffer _playerInputBuffer;

        public float movementSpeed;

        public enum PlayerStatus {Idle, Move, Dodge, Attack, Ultimate, Neutral};
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
            //playerInputActions.Player.Dodge.performed += Dodge;
            playerInputActions.Player.Attack.performed += Attack;
            playerInputActions.Player.Ultimate.performed += Ultimate;
            //playerInputActions.Keyboard.Movement.performed += Movement;
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
                if(inputVector != new Vector2(0, 0))
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
            // var value1 = playerInputActions.Player.Movement.ReadValue<Vector2>(); // 2DVector
            // if(value1 != null)
            //     Debug.Log(value1);
            

        }

        public void Ultimate(InputAction.CallbackContext context)
        {
            _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.Ultimate));
        }

        // public void Dodge(InputAction.CallbackContext context)
        // {
        //     // started, performed, canceled
        //     // if(context.performed)
        //     //     Debug.Log("dodge " + context.phase);

        //     // start dodge cooldown  
        //     if(!PlayerManager.Instance.isBusy)
        //     {
        //         // PlayerManager.Instance.isBusy = true;
        //         // StartCoroutine("DodgeRoll");

        //     }
        // }

        // private IEnumerator DodgeRoll()
        // {
        //     var direction = PlayerManager.Instance.LastMove;
        //     // while animator is playing clip, add force
        //     //yield return null;

        //     // while player isn't at intended final space after jump
        //     Vector2 finalSpace = PlayerManager.Instance.transform.position + direction * PlayerManager.Instance.JumpDistance;
        //     anim.Play("Player_Jump", 0);
        //     while(Mathf.Abs((PlayerManager.Instance.transform.XandY() - finalSpace).magnitude) > 0.1f)
        //     {
        //         // raycast into wall, if going to hit wall, then stop at wall
        //         RaycastHit2D hit = Physics2D.Raycast(PlayerManager.Instance.transform.position, direction, 0.5f);
        //         if(hit)
        //         {
        //             if(hit.collider.CompareTag("Wall"))
        //             {
        //                 Debug.Log("stopping");
        //                 anim.Play("Player_Idle");
        //                 break;
        //             }
        //         }

        //         yield return null;
        //     }
        // }

        public void Movement(InputAction.CallbackContext context)
        {
            //Debug.Log(context.ReadValue<Vector2>());
            //var input = context.ReadValue<Vector2>();
            // if(input.x > 0.1f || input.y > 0.1f)
            // {
            //     if(playerStatus == PlayerStatus.Idle || playerStatus == PlayerStatus.Move)
            //     {
            //         Debug.Log("moving");
            //         // create  move direction
            //         var direction = new Vector3(input.x, input.y * PlayerManager.Instance.verticalRunMod, 0) + PlayerManager.Instance.transform.position;
            //         // multiply move vector by speed 
            //         PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, direction, PlayerManager.Instance.moveSpeed * Time.deltaTime);
            //         PlayerManager.Instance.anim.SetBool("isMoving", true);

            //     }
            
            // }
        }

        public void Attack(InputAction.CallbackContext context)
        {
            // take frame/timestamp that the button was pressed

           _playerInputBuffer.Add(new InputBufferMemory(Time.frameCount, PlayerStatus.Attack)); 
           

        }


    }

}
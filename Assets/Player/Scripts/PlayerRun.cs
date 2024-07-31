using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class PlayerRun : State
    {
        PlayerStateMachine psm;
        public PlayerRun(PlayerStateMachine playerStateMachine) : base(playerStateMachine) 
        {
            psm = playerStateMachine;
        }

        public override IEnumerator Start()
        {
            while(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Move && PlayerManager.Instance.CanMove)
            {
                // create  move direction
                var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") * PlayerManager.Instance.verticalRunMod, 0) + PlayerManager.Instance.transform.position;
                // multiply move vector by speed 
                PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, direction, PlayerManager.Instance.moveSpeed * Time.deltaTime);
                PlayerManager.Instance.anim.SetBool("isMoving", true);
                yield return null;
            }

            PlayerManager.Instance.anim.SetBool("isMoving", false);

            //PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            //PlayerManager.Instance.isBusy = false;

            _playerStateMachine.SetState(new Begin(psm));

        }
    }

}
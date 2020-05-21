using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.PlayerState.States
{
    public class FallingLeftState : PlayerState
    {
        public FallingLeftState(PlayerCore playerCore, Animator animator, PlayerController controller)
        {
            base.playerCore = playerCore;
            base.animator = animator;
            base.controller = controller;
        }

        // Update is called once per frame
        public override void EnterState()
        {
            Debug.Log("Falling Left");
        }

        public override void Update()
        {
            if (controller.IsGrounded)
            {
                playerCore.TransitionToState(playerCore.IdleLeftState);
            }
            else if (controller.MovingRight())
            {
                playerCore.TransitionToState(playerCore.FallingRightState);
            }

            controller.Move();
        }

        public override void ExitState()
        {

        }

        public override void OnCollisionEnter2D(Collision2D other)
        {

        }
    }
}
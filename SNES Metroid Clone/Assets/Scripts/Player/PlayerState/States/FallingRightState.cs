using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.PlayerState.States
{
    public class FallingRightState : PlayerState
    {
        public FallingRightState(PlayerCore playerCore, Animator animator, PlayerController controller)
        {
            base.playerCore = playerCore;
            base.animator = animator;
            base.controller = controller;
        }

        // Update is called once per frame
        public override void EnterState()
        {
            Debug.Log("FallingRight");
        }

        public override void Update()
        {
            if (controller.IsGrounded)
            {
                playerCore.TransitionToState(playerCore.IdleRightState);
            }
            else if (controller.MovingLeft())
            {
                playerCore.TransitionToState(playerCore.FallingLeftState);
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


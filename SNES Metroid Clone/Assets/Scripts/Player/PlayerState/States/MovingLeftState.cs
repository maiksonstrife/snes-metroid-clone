using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.PlayerState.States
{
    public class MovingLeftState : PlayerState
    {
    
        public MovingLeftState(PlayerCore playerCore, Animator animator, PlayerController controller)
        {
            base.playerCore = playerCore;
            base.animator = animator;
            base.controller = controller;
        }
    
        public override void EnterState()
        {
            
            Debug.Log("Moving Left");
        }

        public override void Update()
        {
            
            if (!controller.MovingLeft() && !controller.MovingRight())
            {
                playerCore.TransitionToState(playerCore.IdleLeftState);
            }
            else if (controller.MovingRight())
            {
                controller.FaceRight();
                playerCore.TransitionToState(playerCore.MovingRightState);
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


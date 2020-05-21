using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.PlayerState.States
{
    public class IdleCenterState : PlayerState
    {

        public IdleCenterState(PlayerCore playerCore, Animator animator, PlayerController controller)
        {
            base.playerCore = playerCore;
            base.animator = animator;
            base.controller = controller;
        }
        
        // Update is called once per frame
        public override void EnterState()
        {
            controller.FaceCenter();
        }

        public override void Update()
        {
            if (controller.MovingLeft())
            {
                controller.FaceLeft();
                playerCore.TransitionToState(playerCore.MovingLeftState);
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


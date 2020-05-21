using UnityEngine;

namespace Player.PlayerState.States
{
    public class MovingRightState : PlayerState
    {
        
        public MovingRightState(PlayerCore playerCore, Animator animator, PlayerController controller)
        {
            base.playerCore = playerCore;
            base.animator = animator;
            base.controller = controller;
        }
        
        
        public override void EnterState()
        {
            
        }

        public override void Update()
        {
            if (controller.IsGrounded)
            {
                if (!controller.MovingLeft() && !controller.MovingRight())
                {
                    playerCore.TransitionToState(playerCore.IdleRightState);
                }
                else if (controller.MovingLeft())
                {
                    controller.FaceLeft();
                    playerCore.TransitionToState(playerCore.MovingLeftState);
                }
            }
            else
            {
                playerCore.TransitionToState(playerCore.FallingRightState);
            }
            controller.Move();
        }

        public void Jump()
        {
            
        }
        public override void ExitState()
        {
            
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
           
        }
    }
}

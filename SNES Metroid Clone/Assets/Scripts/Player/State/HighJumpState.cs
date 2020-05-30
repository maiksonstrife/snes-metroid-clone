using UnityEngine;

namespace Player.State
{
    public class HighJumpState : PlayerState
    {
        public HighJumpState(PlayerController playerController)
        {
            base.player = playerController;
        }
        
        public override void EnterState()
        {
            player.Animator.HighJump();
            Debug.Log("Entered High Jump State");
        }

        public override void Update(ControllerInput input)
        {
            Debug.Log("Update in HighJumpState");
            //Update facing
            if (input.Right && !input.Left)
            {
                player.isFacingRight = true;
            }
            else if (input.Left && !input.Right)
            {
                player.isFacingRight = false;
            }

            player.Animator.JoystickUpdate(input);
            
            player.moveDirection.x = input.HorizInput * player.speed;
            player.moveDirection.y -= player.gravity * Time.deltaTime;
            
            player.CC2D.Move(player.moveDirection * Time.deltaTime);
            player.CollisionState = player.CC2D.collisionState;

            player.isGrounded = player.CollisionState.Below;

            if (player.isGrounded)
            {
                Debug.Log("Grounded in High Jump State");
                //Transition to StandingState
                player.TransitionToState(player.standingState);
                return;
            }
            
            if (player.CollisionState.Above)
            {
                player.moveDirection.y -= player.gravity * Time.deltaTime;
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exited High Jump State");
 
        }

        public override string ToString()
        {
            return "HighJump State";
        }
    }
}
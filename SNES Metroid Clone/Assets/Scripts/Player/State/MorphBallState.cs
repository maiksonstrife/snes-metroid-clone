using UnityEngine;

namespace Player.State
{
    public class MorphBallState : PlayerState
    {
        public MorphBallState(PlayerController playerController)
        {
            base.player = playerController;
        }
        
        public override void EnterState()
        {
            
        }

        public override void Update(ControllerInput input)
        {
            if (input.Right && !input.Left)
            {
                player.isFacingRight = true;
            }
            else if (input.Left && !input.Right)
            {
                player.isFacingRight = false;
            }
            
            player.moveDirection.x = input.HorizInput * player.speed;
            player.moveDirection.y = 0.0f;
            
            player.moveDirection.y -= player.gravity * Time.deltaTime;
            
            player.CC2D.Move(player.moveDirection * Time.deltaTime);
            player.CollisionState = player.CC2D.collisionState;

            player.isGrounded = player.CollisionState.Below;
            if (input.Up && (input.VertInput > 0.5f))
            {
                //Transition to StandingState
                player.TransitionToState(player.standingState);
            }
        }

        public override void ExitState()
        {
            
        }

        public override string ToString()
        {
            return "Morph Ball State";
        }
    }
}
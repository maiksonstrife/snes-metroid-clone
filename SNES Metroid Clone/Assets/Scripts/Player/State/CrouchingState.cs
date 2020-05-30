using Equipment;
using UnityEngine;

namespace Player.State
{
    public class CrouchingState : PlayerState
    {
        public CrouchingState(PlayerController playerController)
        {
            base.player = playerController;
        }
        public override void EnterState()
        {
            player.isCrouched = true;
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

            player.moveDirection = Vector3.zero;
            
            player.moveDirection.y -= player.gravity * Time.deltaTime;
            
            player.CC2D.Move(player.moveDirection);
            player.CollisionState = player.CC2D.collisionState;

            player.isGrounded = player.CollisionState.Below;

            if (input.Up && (input.VertInput > 0.5f))
            {
                //Transition to StandingState
                player.TransitionToState(player.standingState);
            }

            /*if (input.TappedDownThisFrame && player.powerSuit.IsEnabled(PowerSuit.Upgrade.MorphBall))
            {
                //Transition to Morphball
                player.TransitionToState(player.morphBallState);
            }*/
            
        }

        public override void ExitState()
        {
            player.isCrouched = false;
        }

        public override string ToString()
        {
            return "CrouchingState";
        }
    }
}
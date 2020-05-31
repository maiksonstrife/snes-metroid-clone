using Equipment;
using UnityEngine;

namespace Player.State
{
    public class CrouchingState : PlayerState
    {
        private float crouchDelay = 0.25f;
        private float timeCrouched;
        
        public CrouchingState(PlayerController playerController)
        {
            base.player = playerController;
        }
        public override void EnterState()
        {
            timeCrouched = 0.0f;
            player.Animator.Crouch();
            Debug.Log("Entered Crouched State");
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

            player.Animator.JoystickUpdate(input);

            player.moveDirection = Vector3.zero;
            
            player.moveDirection.y -= player.gravity * Time.deltaTime;
            
            player.CC2D.Move(player.moveDirection);
            player.CollisionState = player.CC2D.collisionState;

            player.isGrounded = player.CollisionState.Below;

            if (input.Up && (input.VertInput > 0.7f) && (timeCrouched > crouchDelay))
            {
                //Transition to StandingState
                player.TransitionToState(player.standingState);
            }
            Debug.Log(input.VertInput);
            if (input.TappedDownThisFrame && (input.VertInput < -0.6f) && player.powerSuit.IsEnabled(PowerSuit.Upgrade.MorphBall) && (timeCrouched > crouchDelay))
            {
                //Transition to Morphball
                player.TransitionToState(player.morphBallState);
            }
            timeCrouched += Time.deltaTime;
        }

        public override void ExitState()
        {

            Debug.Log("Exited Crouch State");
        }

        public override string ToString()
        {
            return "CrouchingState";
        }
    }
}
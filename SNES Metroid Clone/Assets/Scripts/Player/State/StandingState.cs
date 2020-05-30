using UnityEngine;
using UnityEngine.EventSystems;

namespace Player.State
{
    public class StandingState : PlayerState
    {
        public StandingState(PlayerController playerController)
        {
            base.player = playerController;
        }
        public override void EnterState()
        {
            player.Animator.Stand();
            Debug.Log("Entered Standing State");
        }

        public override void Update(ControllerInput input)
        {
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
            player.moveDirection.y = 0.0f;

            if (input.Jump)
            {
                if (Mathf.Abs(input.HorizInput) < 0.15)
                {
                    //Transition to High Jump
                    player.isGrounded = false;
                    player.moveDirection.y += player.jumpSpeed;
                    player.TransitionToState(player.highJumpState);
                    return;
                }
                else
                {
                    //Transition to Somersault
                    player.moveDirection.y += player.jumpSpeed;
                    player.TransitionToState(player.somersaultState);
                    return;
                }
            }

            player.moveDirection.y -= player.gravity * Time.deltaTime;
            
            player.CC2D.Move(player.moveDirection * Time.deltaTime);
            player.CollisionState = player.CC2D.collisionState;

            player.isGrounded = player.CollisionState.Below;

            if (input.Down && (Mathf.Abs(input.HorizInput) < 0.15))
            {
                //Transition to CrouchingState
                player.TransitionToState(player.crouchingState);
                return;
            }

            if (!player.isGrounded)
            {
                Debug.Log("Ungrounded from Standing State");
                //Transition to falling State
                player.TransitionToState(player.fallingState);
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exited Standing State");
        }

        public override string ToString()
        {
            return "Standing State";
        }
    }
}
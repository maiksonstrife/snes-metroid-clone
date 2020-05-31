using System.Net.NetworkInformation;
using UnityEngine;

namespace Player.State
{
    public class FallingState : PlayerState
    {
        public FallingState(PlayerController playerController)
        {
            base.player = playerController;
        }
        public override void EnterState()
        {
            Debug.Log("Entered Falling State");
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

            player.moveDirection.x = input.HorizInput * player.speed;

            player.moveDirection.y -= player.gravity * Time.deltaTime;
            
            player.CC2D.Move(player.moveDirection * Time.deltaTime);
            player.CollisionState = player.CC2D.collisionState;

            player.isGrounded = player.CollisionState.Below;

            if (player.isGrounded)
            {
                //Transition to Standing State
                player.TransitionToState(player.standingState);
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exited Falling State");
        }

        public override string ToString()
        {
            return "Falling State";
        }
    }
}
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
            Debug.Log("Entered Morphball State");
            player.Animator.EnterMorphBall();
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
            //player.moveDirection.y = 0.0f;
            
            player.moveDirection.y -= player.gravity * Time.deltaTime;
            
            player.CC2D.Move(player.moveDirection * Time.deltaTime);
            player.CollisionState = player.CC2D.collisionState;

            player.isGrounded = player.CollisionState.Below;
            if (input.Up && (input.VertInput > 0.5f) && CanCrouch())
            {
                //Transition to Crouching
                player.TransitionToState(player.crouchingState);
            }
        }

        public override void ExitState()
        {
            
        }

        public override string ToString()
        {
            return "Morph Ball State";
        }

        private bool CanCrouch()
        {
            Vector3 upperL = new Vector3(player.transform.position.x - player._boxCollider2D.size.x / 2, 
                player.transform.position.y + player._boxCollider2D.size.y, 0.0f);
            Vector3 upperR = new Vector3(player.transform.position.x + player._boxCollider2D.size.x / 2, 
                player.transform.position.y + player._boxCollider2D.size.y, 0.0f);

            RaycastHit2D hitR = Physics2D.Raycast(upperR, Vector2.up, 0.3f);
            RaycastHit2D hitL = Physics2D.Raycast(upperL, Vector2.up, 0.3f);

            if ((!hitR.collider) && (!hitL.collider))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
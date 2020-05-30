using System.Collections;
using Control;
using UnityEngine;

namespace Player.State
{
    public class SomersaultState : PlayerState
    {
        private bool _wallJumpAble;
        private bool _wallJumpAbleLeft;
        private bool _wallJumpAbleRight;

        private bool _hasWallJumped;

        private float _wallJumpMultiplier = 1.5f;
        
        public SomersaultState(PlayerController playerController)
        {
            base.player = playerController;
        }
        public override void EnterState()
        {
            Debug.Log("Entered Somersault State");
            player.isJumping = true;
            _wallJumpAble = false;
            _wallJumpAbleLeft = false;
            _wallJumpAbleRight = false;
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
            
            if(Input.GetButtonUp("Jump"))
            {
                if (player.moveDirection.y > 0)
                {
                    player.moveDirection.y = player.moveDirection.y * 0.5f;
                }
            }
            
            if(!_hasWallJumped)
                player.moveDirection.x = input.HorizInput * player.speed;
            player.moveDirection.y -= player.gravity * Time.deltaTime;
            
            player.CC2D.Move(player.moveDirection * Time.deltaTime);
            player.CollisionState = player.CC2D.collisionState;

            if (player.CollisionState.Above)
            {
                player.moveDirection.y -= player.gravity * Time.deltaTime;
            }

            player.isGrounded = player.CollisionState.Below;
            if (player.isGrounded)
            {
                player.isJumping = false;
                //Transition to Standing State
                player.TransitionToState(player.standingState);
                return;
            }

            if (!_wallJumpAble)
            {
                _wallJumpAble = CanWallJump(player.CollisionState);
            }

            if (_wallJumpAble)
            {
                if (input.Jump && _wallJumpAbleLeft && input.Left)
                {
                    WallJump();
                }

                if (input.Jump && _wallJumpAbleRight && input.Right)
                {
                    WallJump();
                }
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exited Somersault State");
        }

        private bool CanWallJump(CharacterController2D.CharacterCollisionState2D coll2D)
        {
            if (coll2D.Right || coll2D.Left)                            //Wall to the left or right 
            {
                if (coll2D.Above || coll2D.Below)                       //In a corner, no wall jump
                {
                    _wallJumpAble = false;
                    return false;
                }
                else
                {
                    _wallJumpAble = true;
                    if (coll2D.Left)
                        _wallJumpAbleRight = true;
                    if (coll2D.Right)
                        _wallJumpAbleLeft = true;
                    player.StartCoroutine(WallJumpWindow());
                    return true;
                }
            }
            else
            {
                _wallJumpAble = false;
                return false;
            }
        }

        private void WallJump()
        {
            if (player.isFacingRight)
            {
                player.moveDirection.x = player.jumpSpeed * _wallJumpMultiplier;
                player.moveDirection.y = player.jumpSpeed * _wallJumpMultiplier;
            }
            else
            {
                player.moveDirection.x = -player.jumpSpeed * _wallJumpMultiplier;
                player.moveDirection.y = player.jumpSpeed * _wallJumpMultiplier;
            }
        }

        public override string ToString()
        {
            return "Somersault State";
        }

        private IEnumerator WallJumpWindow()
        {
            yield return new WaitForSeconds(0.3f);
            _wallJumpAble = false;
            _wallJumpAbleLeft = false;
            _wallJumpAbleRight = false;
        }
        
    }
}
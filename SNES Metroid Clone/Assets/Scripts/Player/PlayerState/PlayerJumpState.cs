using System.Collections;
using System.Collections.Generic;
using Player.PlayerState;
using UnityEngine;


namespace Player.PlayerState
{
    public class PlayerJumpState : PlayerState
    {
        private bool _facingRight;
        private bool _facingLeft;

        private float _horizInput;
        public PlayerJumpState(PlayerCore player, Animator animator)
        {
            playerCore = player;
            base.animator = animator;
        }
        
        public override void EnterState()
        {
            Debug.Log("PlayerJumpState");
        }

        // Update is called once per frame
        public override void Update()
        {
            _horizInput = Input.GetAxis("Horizontal");
            
            if ((_horizInput > 0) && !_facingRight)
            {
                //Debug.Log("FacingRight");
                _facingRight = true;
                _facingLeft = false;
               
            }
            else if ((_horizInput < 0) && !_facingLeft)
            {
                //Debug.Log("FacingLeft");
                _facingRight = false;
                _facingLeft = true;
            }

            if (playerCore.Controller.isGrounded)
            {
                animator.ResetTrigger("jump");
                playerCore.TransitionToState(playerCore.PlayerIdleState);
            }
            AnimatorUpdate();
        }

        // ExitState is called upon exiting the state
        public override void ExitState()
        {
            
        }
        

        // State Collision Handling
        public override void OnCollisionEnter2D(Collision2D other)
        {
            playerCore.TransitionToState(playerCore.PlayerIdleState);
        }
        
        void AnimatorUpdate()
        {
            animator.SetBool("grounded", playerCore.Controller.isGrounded);
            animator.SetFloat("horizontalInput", _horizInput);
            animator.SetBool("facingRight", _facingRight);
            animator.SetBool("facingLeft", _facingLeft);
        }
    }
}


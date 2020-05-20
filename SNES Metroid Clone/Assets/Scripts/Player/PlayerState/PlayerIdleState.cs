﻿using UnityEngine;


namespace Player.PlayerState
{
    public class PlayerIdleState : PlayerState
    {
        private bool _facingRight;
        private bool _facingLeft;

        private float _horizInput;
        
        public PlayerIdleState(PlayerCore player, Animator animator)
        {
            playerCore = player;
            base.animator = animator;
        }
        
        // Update is called once per frame
        public override void EnterState()
        {
            Debug.Log("PlayerIdleState");
            _facingRight = animator.GetBool("facingRight");
            _facingLeft = animator.GetBool("facingLeft");
            
        }

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
            else if ((_horizInput > 0.01f && _facingRight))
            {
                playerCore.TransitionToState(playerCore.PlayerMovingState);
            }
            else if ((_horizInput < -0.01f) && _facingLeft)
            {
                playerCore.TransitionToState(playerCore.PlayerMovingState);
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                playerCore.Jump();
                playerCore.TransitionToState(playerCore.PlayerJumpState);
            }
            
            AnimatorUpdate();
        }

        public override void ExitState()
        {
            
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            
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


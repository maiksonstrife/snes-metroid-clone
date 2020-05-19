using System;
using UnityEngine;


namespace Player.PlayerState
{
    public class PlayerMovingState : PlayerState
    {

        private bool _facingRight;
        private bool _facingLeft;
        private float _horizInput;
        
        public PlayerMovingState(PlayerCore player, Animator animator)
        {
            playerCore = player;
            base.animator = animator;
        }

        // Update is called once per frame
        public override void EnterState()
        {
            _horizInput = Input.GetAxis("Horizontal");
            _facingRight = animator.GetBool("facingRight");
            _facingLeft = animator.GetBool("facingLeft");
            AnimatorUpdate();
        }

        public override void Update()
        {
            float lastHorizInput = _horizInput;
            _horizInput = Input.GetAxis("Horizontal");

            if ((Math.Abs(_horizInput) < 0.01) || (lastHorizInput < 0 && _horizInput > 0) || (lastHorizInput > 0 && _horizInput < 0))
            {
                playerCore.TransitionToState(playerCore.PlayerIdleState);
            }
            else
            {
                float dist = _horizInput * playerCore.Speed * Time.deltaTime;
                playerCore.transform.position += new Vector3(dist, 0.0f, 0.0f);
                
            }
            AnimatorUpdate();
        }

        public override void ExitState()
        {
            
        }
        
        void AnimatorUpdate()
        {
            animator.SetFloat("horizontalInput", _horizInput);
            animator.SetBool("facingRight", _facingRight);
            animator.SetBool("facingLeft", _facingLeft);
        }
    }
}


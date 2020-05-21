using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
 
        private bool _facingRight = false;
        private bool _facingLeft = false;
        private bool _crouched = false;
        
        [Range(1.0f, 10.0f)] [SerializeField] private float speed = 2.0f;
        [Range(0.5f, 10.0f)] [SerializeField] private float gravity = 1.05f;
        [Range(2.0f, 30.0f)] [SerializeField] private float jumpVelocity = 15.0f;
        [Range(1.0f, 2.5f)] [SerializeField] private float fallMultiplier = 1.0f;
        [Range(1.0f, 2.5f)] [SerializeField] private float lowJumpMultiplier = 1.0f;

        private float _horizontalInput;
        private float _verticalInput;
        private Vector3 _moveDirection = Vector3.zero;

        private CharacterController _controller;
        private Animator _animator;

        public bool IsGrounded => _controller.isGrounded;

        public float JumpVelocity => jumpVelocity;
  
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            if(_controller == null) Debug.LogError("No Character Controller on Player");
            _animator = GetComponent<Animator>();
            if(_animator == null) Debug.LogError("No Animator on Player");
        }

        // Update is called once per frame
        void Update()
        {
            _moveDirection = Vector3.zero;
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            
            _moveDirection += Vector3.right * (_horizontalInput * speed);

           
            _moveDirection += Vector3.down * (gravity);

            if (Input.GetButtonDown("Fire1"))
            {
               //Jump
            }
    
            UpdateAnimator();
        }

        void UpdateAnimator()
        {
            Debug.Log(_facingLeft);
            _animator.SetBool("grounded", _controller.isGrounded);
            _animator.SetFloat("horizontalInput", _horizontalInput);
            _animator.SetBool("facingRight", _facingRight);
            _animator.SetBool("facingLeft", _facingLeft);
        }
        public void Move()
        {
            //Debug.Log(_moveDirection);
            _controller.Move(_moveDirection * Time.deltaTime);
        }
        
        public void FaceCenter()
        {
            _facingLeft = false;
            _facingRight = false;
        }

        public void FaceRight()
        {
            _facingLeft = false;
            _facingRight = true;
        }

        public void FaceLeft()
        {
            _facingLeft = true;
            _facingRight = false;
        }
        
        public bool MovingRight()
        {
            if (_horizontalInput > 0)
            {
                return true;
            }
            return false;
        }

        public bool MovingLeft()
        {
            if (_horizontalInput < 0)
            {
                return true;
            }

            return false;
        }
        
    }
}


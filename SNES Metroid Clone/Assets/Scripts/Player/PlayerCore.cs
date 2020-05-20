using System;
using System.Collections;
using System.Collections.Generic;
using Player.PlayerState;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerCore : MonoBehaviour
    {
        #region Private Members

        private PlayerState.PlayerState _playerState;
        private PlayerIdleState _playerIdleState;
        private PlayerMovingState _playerMovingState;
        private PlayerJumpState _playerJumpState;

        private Animator _animator;

        private CharacterController _controller;
        private Vector3 _moveDirection = Vector3.zero;
        private Vector3 _gravity = Vector3.zero;
        private Vector3 _jumpVelocity = Vector3.zero;

        [Range(1.0f, 10.0f)] [SerializeField] private float speed = 2.0f;
        [Range(0.5f, 10.0f)] [SerializeField] private float gravity = 1.05f;
        [Range(2.0f, 30.0f)] [SerializeField] private float jumpVelocity = 15.0f;
        [Range(1.0, 2.5f)] [SerializeField] private float fallMultiplier = 1.0f;
        [Range(1.0f, 2.5f)] [SerializeField] private float lowJumpMultiplier = 1.0f;
        
        #endregion
        
        #region Properties
        public float Speed => speed;
        public float JumpVelocity => jumpVelocity;
        public CharacterController Controller => _controller;
        public PlayerIdleState PlayerIdleState => _playerIdleState;
        public PlayerMovingState PlayerMovingState => _playerMovingState;
        public PlayerJumpState PlayerJumpState => _playerJumpState;
        
        
        #endregion
        

        #region CoreFunctionality

        // Start is called before the first frame update
        void Start()
        {
            
            _animator = this.GetComponent<Animator>();
            if(_animator == null) Debug.LogError("Player has no Animation Controller Component");

            _controller = this.GetComponent<CharacterController>();
            if(_controller == null) Debug.LogError("Player has no CharacterController Component");
            
            _gravity = new Vector3(0.0f, -gravity, 0.0f);
            _jumpVelocity = new Vector3(0.0f, jumpVelocity, 0.0f);
            
            //Initialize States
            _playerIdleState = new PlayerIdleState(this, _animator);
            _playerMovingState = new PlayerMovingState(this, _animator);
            _playerJumpState = new PlayerJumpState(this, _animator);
            
            _playerState = _playerIdleState;
            
        }

        // Update is called once per frame
        void Update()
        {
            _moveDirection = Vector3.zero;
            _playerState.Update();
            MovementUpdate();
            
        }

        public void TransitionToState(PlayerState.PlayerState state)
        {
            _playerState.ExitState();
            _playerState = state;
            _playerState.EnterState();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Collision");
            _playerState.OnCollisionEnter2D(other);
        }

        void MovementUpdate()
        {
            _moveDirection += _gravity * Time.deltaTime;
            _controller.Move(_moveDirection);
        }

        public void Move(Vector3 input)
        {
            _moveDirection += input;
        }

        public void Jump()
        {
            _animator.SetTrigger("jump");
            _moveDirection += _jumpVelocity;
        }
        
    
        #endregion
    
    }
}


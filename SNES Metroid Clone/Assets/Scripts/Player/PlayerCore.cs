using System;
using System.Collections;
using System.Collections.Generic;
using Player.PlayerState;
using Player.PlayerState.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerCore : MonoBehaviour
    {
        #region Private Members

        private PlayerState.PlayerState _playerState;
        private IdleCenterState _idleCenterState;
        private IdleRightState _idleRightState;
        private IdleLeftState _idleLeftState;
        private MovingRightState _movingRightState;
        private MovingLeftState _movingLeftState;
        private FallingLeftState _fallingLeftState;
        private FallingRightState _fallingRightState;

        private Animator _animator;

        private CharacterController _controller;
        private PlayerController _playerController;
        private Vector3 _moveDirection = Vector3.zero;
        private Vector3 _gravity = Vector3.zero;
        private Vector3 _jumpVelocity = Vector3.zero;

    
        
        #endregion
        
        #region Properties

        public CharacterController Controller => _controller;
        public IdleCenterState IdleCenterState => _idleCenterState;
        public IdleRightState IdleRightState => _idleRightState;
        public IdleLeftState IdleLeftState => _idleLeftState;
        public MovingRightState MovingRightState => _movingRightState;
        public MovingLeftState MovingLeftState => _movingLeftState;
        public FallingLeftState FallingLeftState => _fallingLeftState;
        public FallingRightState FallingRightState => _fallingRightState;
        
        #endregion
        

        #region CoreFunctionality

        // Start is called before the first frame update
        void Start()
        {
            
            _animator = this.GetComponent<Animator>();
            if(_animator == null) Debug.LogError("Player has no Animation Controller Component");

            _controller = this.GetComponent<CharacterController>();
            if(_controller == null) Debug.LogError("Player has no CharacterController Component");

            _playerController = this.GetComponent<PlayerController>();
            
 
            
            //Initialize States
            _idleCenterState = new IdleCenterState(this, _animator, _playerController);
            _idleRightState = new IdleRightState(this, _animator, _playerController);
            _idleLeftState = new IdleLeftState(this, _animator, _playerController);
            _movingRightState = new MovingRightState(this, _animator, _playerController);
            _movingLeftState = new MovingLeftState(this, _animator, _playerController);
            _fallingLeftState = new FallingLeftState(this, _animator, _playerController);
            _fallingRightState = new FallingRightState(this, _animator, _playerController);

            _playerState = _idleCenterState;

        }

        // Update is called once per frame
        void Update()
        {
            _playerState.Update();

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
        

        public void Jump()
        {
            _animator.SetTrigger("jump");
            _moveDirection += _jumpVelocity;
        }
        
    
        #endregion
    
    }
}


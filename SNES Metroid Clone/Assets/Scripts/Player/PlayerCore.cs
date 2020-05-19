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
        
        public PlayerIdleState PlayerIdleState => _playerIdleState;

        public PlayerMovingState PlayerMovingState => _playerMovingState;

        private Animator _animator;

        [Range(1.0f, 10.0f)] [SerializeField] private float speed = 2.0f;
        public float Speed => speed;

        #endregion
        

        #region CoreFunctionality

        // Start is called before the first frame update
        void Start()
        {
            //Initialize and null check Animator Controller
            _animator = this.GetComponent<Animator>();
            if(_animator == null) Debug.LogError("Player has no Animation Controller Component");
            
            //Initialize States
            _playerIdleState = new PlayerIdleState(this, _animator);
            _playerState = _playerIdleState;
            
            _playerMovingState = new PlayerMovingState(this, _animator);

           
        }

        // Update is called once per frame
        void Update()
        {
            _playerState.Update();
            MovementUpdate();
        }

        public void TransitionToState(PlayerState.PlayerState state)
        {
            _playerState.ExitState();
            _playerState = state;
            _playerState.EnterState();
        }

        void MovementUpdate()
        {
            
        }
        
    
        #endregion
    
    }
}


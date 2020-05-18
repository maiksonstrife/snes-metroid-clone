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

        private Animator _animator;
        private bool _facingRight = false;
        private bool _facingLeft = false;

        [Range(1.0f, 10.0f)] [SerializeField] private float speed;
    
        #endregion
        

        #region CoreFunctionality

        // Start is called before the first frame update
        void Start()
        {
            //Initialize State
            _playerIdleState = new PlayerIdleState(this);
            _playerState = _playerIdleState;

            //Initialize and null check Animator Controller
            _animator = this.GetComponent<Animator>();
            if(_animator == null) Debug.LogError("Player has no Animation Controller Component");
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

        public void UpdateAnimatorTrigger(string trigger)
        {
            //TODO
        }

        public void UpdateAnimatorBool(string var, bool value)
        {
            
        }
        
    
        #endregion
    
    }
}


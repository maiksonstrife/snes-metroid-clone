using UnityEngine;

namespace Player
{
    public class ControllerInput
    {
        #region Private Fields
        
        private float _vertInput;
        private float _horizInput;

        private bool _up;
        private bool _down;
        private bool _left;
        private bool _right;

        private bool _jump;
        private bool _shoot;

        private float _lastVertInput;
        private float _lastHorizInput;

        private bool _tappedDown;
        
        #endregion

        #region Properties

        public float VertInput => _vertInput;
        public float HorizInput => _horizInput;
        public bool Up => _up;
        public bool Down => _down;
        public bool Left => _left;
        public bool Right => _right;
        public bool Jump => _jump;
        public bool Shoot => _shoot;

        public bool TappedDownThisFrame => _tappedDown;

        #endregion
        
        
        
        
        public ControllerInput()
        {
            _vertInput = 0.0f;
            _horizInput = 0.0f;

            _up = false;
            _down = false;
            _left = false;
            _right = false;

            _jump = false;
            _shoot = true;

            _lastHorizInput = 0.0f;
            _lastVertInput = 0.0f;

            _tappedDown = false;
        }

        // Update is called once per frame
        public void Update()
        {
            _lastHorizInput = _horizInput;
            _lastVertInput = _vertInput;

            _vertInput = Input.GetAxis("Vertical");
            _horizInput = Input.GetAxis("Horizontal");

            _up = (_vertInput > 0) ? true : false;
            _down = (_vertInput < 0) ? true : false;
            _right = (_horizInput > 0) ? true : false;
            _left = (_horizInput < 0) ? true : false;

            _tappedDown = (_down && (_lastVertInput > -0.15f)) ? true : false;

            _shoot = Input.GetButtonDown("Fire1");
            _jump = Input.GetButtonDown("Jump");
        }

        public void ResetDownTap()
        {
            _tappedDown = false;
        }
    }
}

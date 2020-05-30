using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAnimator : MonoBehaviour
    {
        private PlayerController _playerController;
        private Animator _animator;

        private int _base, _facingRight, _facingLeft;
        private int _currentLayer;
        
        // Start is called before the first frame update
        void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _animator = GetComponentInChildren<Animator>();
            PlayerController.OnWallJump += WallJump;

            _base = _animator.GetLayerIndex("Start Layer");
            _currentLayer = _base;

            _facingRight = _animator.GetLayerIndex("Facing Right");
            _facingLeft = _animator.GetLayerIndex("Facing Left");
            
            //Debug.Log("Base: " + _base + "  Right: " + _facingRight + "  Left: " + _facingLeft);
        }

        // Update is called once per frame
        void Update()
        {
            float horizInput = Input.GetAxis("Horizontal");
            _animator.SetFloat("Horizontal Input", Mathf.Abs(horizInput));
            
            _animator.SetBool("Is Grounded", _playerController.isGrounded);
            if(_playerController.isGrounded) _animator.ResetTrigger("Wall Jump");        // Need to reset if slid to ground
            _animator.SetBool("Is Jumping", _playerController.isJumping);
            _animator.SetBool("Wall Jump Able", _playerController.wallJumpAble);
            if(!_playerController.wallJumpAble) _animator.ResetTrigger("Wall Jump");    //Need to reset if slid to point where not on the wall
            _animator.SetBool("Is Crouching", _playerController.isCrouched);
            _animator.SetBool("Is Morphball", _playerController.isMorphBall);
            
            if (_playerController.isFacingRight && _currentLayer != _facingRight)
            {
                _animator.SetLayerWeight(_facingRight, 1.0f);
                _currentLayer = _facingRight;
                ZeroOtherWeights();
            }
            else if (!_playerController.isFacingRight && _currentLayer != _facingLeft)
            {
                _animator.SetLayerWeight(_facingLeft, 1.0f);
                _currentLayer = _facingLeft;
                ZeroOtherWeights();
            }
        }

        private void ZeroOtherWeights()
        {
            for (int i = 0; i < _animator.layerCount; i++)
            {
                if (i != _currentLayer)
                {
                    _animator.SetLayerWeight(i, 0f);
                }
            }
        }

        public void FaceRight()
        {
            _animator.SetLayerWeight(_facingRight, 1.0f);
            _currentLayer = _facingRight;
            ZeroOtherWeights();
        }

        public void FaceLeft()
        {
            _animator.SetLayerWeight(_facingLeft, 1.0f);
            _currentLayer = _facingLeft;
            ZeroOtherWeights();
        }

        public void JoystickUpdate(ControllerInput input)
        {
            _animator.SetFloat("Horizontal Input", Mathf.Abs(input.HorizInput));
        }
        public void Crouch()
        {
            _animator.SetBool("Is Crouching", true);
        }

        public void Stand()
        {
            _animator.SetBool("Is Crouching", false);
        }

        public void WallJump()
        {
            _animator.SetTrigger("Wall Jump");
        }
        
    }
}

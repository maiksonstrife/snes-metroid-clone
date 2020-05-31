using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private PlayerController _playerController;
        private Animator _animator;

        private int _base, _facingRight, _facingLeft;
        private int _currentLayer;
        
        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _base = _animator.GetLayerIndex("Start Layer");
            _currentLayer = _base;

            _facingRight = _animator.GetLayerIndex("Facing Right");
            _facingLeft = _animator.GetLayerIndex("Facing Left");
            
            //Debug.Log("Base: " + _base + "  Right: " + _facingRight + "  Left: " + _facingLeft);
        }

        // Update is called once per frame
        void Update()
        {
            
            
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

        private void FaceRight()
        {
            _animator.SetLayerWeight(_facingRight, 1.0f);
            _currentLayer = _facingRight;
            ZeroOtherWeights();
        }

        private void FaceLeft()
        {
            _animator.SetLayerWeight(_facingLeft, 1.0f);
            _currentLayer = _facingLeft;
            ZeroOtherWeights();
        }

        public void JoystickUpdate(ControllerInput input)
        {
            //Debug.Log("JoystickUpdate" + input.HorizInput);
            _animator.SetFloat("Horizontal Input", Mathf.Abs(input.HorizInput));
            if(input.Left)
                FaceLeft();
            if(input.Right)
                FaceRight();
        }

        public void PhysicsUpdate(Vector3 velocity)
        {
            
        }
        public void Crouch()
        {
            _animator.SetBool("Is Crouching", true);
            _animator.SetBool("Is Grounded", true);
            _animator.ResetTrigger("Wall Jump");
            _animator.SetBool("Is Jumping", false);
            _animator.SetBool("Is Morphball", false);
        }

        public void Stand()
        {
            _animator.SetBool("Is Crouching", false);
            _animator.SetBool("Is Grounded", true);
            _animator.ResetTrigger("Wall Jump");
            _animator.SetBool("Is Jumping", false);
        }

        public void WallJump()
        {
            _animator.SetBool("Wall Jump Able", true);
            _animator.SetTrigger("Wall Jump");
        }

        public void HighJump()
        {
            _animator.SetBool("Is Grounded", false);
            _animator.SetBool("Is Jumping", true);
        }

        public void Somersault()
        {
            _animator.SetBool("Is Grounded", false);
            _animator.SetBool("Is Jumping", true);
        }

        public void EnterMorphBall()
        {
            _animator.SetBool("Is Morphball", true);
            //_animator.SetBool("Is Crouching", false);
        }
        
    }
}

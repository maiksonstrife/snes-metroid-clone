using System;
using System.Collections;
using Control;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Private Fields

        private CharacterController2D _cc2D;
        private BoxCollider2D _boxCollider2D;
        private CharacterController2D.CharacterCollisionState2D _collisionState;
        private Sprite _sprite;
        private GameObject _powerSuit;

        private Vector2 _originalColliderSize;

        #endregion

        #region State Booleans

        public bool isGrounded;
        public bool isJumping;
        public bool isHighJumping;
        public bool isFacingRight;
        public bool hasWallJumped;
        public bool isCrouched;
        public bool wallJumpAble;
        public bool isFalling;
        
        #endregion
        

        #region Tunable Fields
        
        [SerializeField] private float speed = 6.0f;
        [SerializeField] private float jumpSpeed = 8.0f;
        [SerializeField] private float gravity = 20.0f;
        [SerializeField] private float wallJumpMultiplier = 1.5f;
        
        #endregion

        #region Movement

        private Vector3 _moveDirection = Vector3.zero;
        private bool _lastWallJumpLeft;
        #endregion

        #region Delegates

        //Hack to Animator for wall jump able to wall jump transition
        public delegate void WallJumpTrigger();
        public static WallJumpTrigger OnWallJump;

        #endregion
        
        #region Monobehaviour
        
        public void Start()
        {
            _cc2D = GetComponent<CharacterController2D>();    //No null check, required component above.
            _boxCollider2D = GetComponent<BoxCollider2D>();   //No null check, required component with CC2D.
            _originalColliderSize = _boxCollider2D.size;
        }

        public void Update()
        {
            _powerSuit = transform.GetChild(0).gameObject;
            _sprite = GetComponentInChildren<SpriteRenderer>().sprite;
            _boxCollider2D.size = new Vector2(_originalColliderSize.x, _sprite.bounds.size.y);
            //_boxCollider2D.offset = new Vector2(0.0f, _sprite.bounds.size.y);                                                        //Fixes issue where crouch is floating momentarily... but problems with every other sprite...
            //_powerSuit.transform.position = new Vector3(transform.position.x, transform.position.y + _sprite.bounds.size.y, 0.0f);
            _cc2D.RecalculateDistanceBetweenRays();
            
            float horizInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");
            
            if(hasWallJumped == false)
                _moveDirection.x = horizInput * speed;
            
            if (isGrounded)
            {
                _moveDirection.y = 0.0f;
                isJumping = false;
                isFalling = false;

                if (_moveDirection.x < 0)
                {
                    isFacingRight = false;
                }
                else if (_moveDirection.x > 0)
                {
                    isFacingRight = true;
                }

                if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")) && (Mathf.Abs(horizInput) < 0.15f))
                {
                    _moveDirection.y = jumpSpeed;
                    isJumping = true;
                    isHighJumping = true;
                }
                else if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")) && (Mathf.Abs(horizInput) > 0.15f))
                {
                    _moveDirection.y = jumpSpeed;
                    isJumping = true;
                    isHighJumping = false;
                }
            }
            else     //Player in the air/jumping
            {
                if (!isJumping)
                {
                    isFalling = true;
                }
                if (horizInput > 0)
                {
                    isFacingRight = true;
                }
                else if (horizInput < 0)
                {
                    isFacingRight = false;
                }
                if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
                {
                    if (_moveDirection.y > 0)
                    {
                        _moveDirection.y = _moveDirection.y * 0.5f;
                    }
                    
                }
            }

            _moveDirection.y -= gravity * Time.deltaTime;
            
            //If crouching, we're not moving
            if (isCrouched)
            {
                _moveDirection.x = 0.0f;    //However can still change facing from movement inputs above...
            }
                
            
            _cc2D.Move(_moveDirection * Time.deltaTime);
            _collisionState = _cc2D.collisionState;

            isGrounded = _collisionState.Below;
            
            //Crouching
            if (vertInput < 0f && Math.Abs(horizInput) < 0.01f && !isCrouched)
            {
               // _boxCollider2D.size = new Vector2(_originalColliderSize.x, _originalColliderSize.y * 0.5f);
               // transform.position = new Vector3(transform.position.x, transform.position.y - (_originalColliderSize.y * 0.25f), transform.position.z);
               // _cc2D.RecalculateDistanceBetweenRays();
                isCrouched = true;
               
            }

            if (vertInput > 0.35f && isCrouched)               //Can return without vertical check because no moving when crouching
            {                                               //Will need to do vertical check when returning from morph ball state (unimplemented so far)
               // _boxCollider2D.size = _originalColliderSize;
               // transform.position = new Vector3(transform.position.x, transform.position.y + (_originalColliderSize.y * 0.25f), transform.position.z);
                isCrouched = false;
            }
            
            // //Morph Ball Unimplemented (Keep for later)
            // RaycastHit2D hitCeiling = Physics2D.Raycast(upper right corner, Vector2.up, 2.0f, default);
            // RaycastHit2D hitCeiling = Physics2D.Raycast(upper left corner, Vector2.up, 2.0f, default);
            // if (vertInput < 0f && isCrouched)
            // {
            //     //Enter morph ball state
            // }
            //
            // if (vertInput > 0.0f && isMorphBallMode)
            // {
            //     //Check overhead if able to expand collider again
            //     if (!hitCeiling.collider)
            //     {
            //         //Exit morph ball state to crouched state
            //         isMorphBallMode = false;
            //         isCrouched = true;
            //     }
            //     
            // }
            

            if (_collisionState.Above)
            {
                _moveDirection.y -= gravity * Time.deltaTime;
            }
            
            //Wall jumping
            if ((_collisionState.Left || _collisionState.Right) && isJumping && !_collisionState.Below && !isHighJumping && !isFalling && !isGrounded && !_collisionState.Above)     //In a position to wall jump
            {
                wallJumpAble = true;
                if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")) && hasWallJumped == false && isGrounded == false)
                {
                    OnWallJump();
                    if (_moveDirection.x < 0)
                    {
                        _moveDirection.x = jumpSpeed * wallJumpMultiplier;
                        _moveDirection.y = jumpSpeed * wallJumpMultiplier;
                        isFacingRight = true;
                        _lastWallJumpLeft = false;
                    }
                    else if (_moveDirection.x > 0)
                    {
                        _moveDirection.x = -jumpSpeed * wallJumpMultiplier;
                        _moveDirection.y = jumpSpeed * wallJumpMultiplier;
                        isFacingRight = false;
                        _lastWallJumpLeft = true;
                    }

                    StartCoroutine(WallJumpCooldown());
                }
            }
            else
            {
                wallJumpAble = false;
            }
        }

        public void LateUpdate()
        {
            
        }

        #endregion

        #region Coroutines

        private IEnumerator WallJumpCooldown()
        {
            hasWallJumped = true;
            yield return new WaitForSeconds(0.5f);
            hasWallJumped = false;
        }
        

        #endregion
    }
}


using System;
using System.Collections;
using Control;
using UnityEngine;

using Equipment;
using UnityEngine.PlayerLoop;
using Weapons;

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
        private Transform _powerSuitTransform;
        private PowerSuit _powerSuit;

        private Vector2 _originalColliderSize;

        #endregion

        #region State Fields

        public bool isGrounded;
        public bool isJumping;
        public bool isHighJumping;
        public bool isFacingRight;
        public bool hasWallJumped;
        public bool isCrouched;
        public bool wallJumpAble;
        public bool isFalling;
        public bool isShooting;
        private bool _canShoot = true;
        
        public enum Facing
        {
            Center,
            Right,
            Left,
            AimUpLeft,
            AimUpRight,
            AimDownLeft,
            AimDownRight
        };

        public Facing facing = Facing.Center;
        
        #endregion
        

        #region Tunable Fields
        
        [SerializeField] private float speed = 6.0f;
        [SerializeField] private float jumpSpeed = 8.0f;
        [SerializeField] private float ballJumpSpeed = 6.0f;
        [SerializeField] private float gravity = 20.0f;
        [SerializeField] private float wallJumpMultiplier = 1.5f;
        [SerializeField] private float wallJumpControlDelay = 0.5f;
        [SerializeField] private float weaponCooldown = 0.25f;
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
            _powerSuit = GetComponentInChildren<PowerSuit>();
            if(_powerSuit == null) Debug.LogError("No power suit attached to player.");
            
            _originalColliderSize = _boxCollider2D.size;
        }

        public void Update()
        {
            //Ensure collider reflects current sprite
            ColliderUpdate();
            
            //Get controller input
            float horizInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");
            
            //Update facing based on input
            FacingUpdate(horizInput, vertInput);
            
            //If walljumped do not update horizontal direction
            //TODO: walljump coroutine so that movement can apply after certain time into a wall jump
            if(hasWallJumped == false)
                _moveDirection.x = horizInput * speed;
            
            if (isGrounded)     //Player on the ground...
            {
                _moveDirection.y = 0.0f;              //Keeps animation from bouncing
                isJumping = false;
                isFalling = false;
                

                if (Input.GetButtonDown("Jump"))
                {
                    Jump(horizInput);
                }

            }
            else     //Player in the air/jumping
            {
                if (!isJumping && !isHighJumping)
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
                if (Input.GetButtonUp("Jump"))
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
                isCrouched = true;
               
            }

            if (vertInput > 0.35f && isCrouched)               //Can return without vertical check because no moving when crouching
            {                                               //Will need to do vertical check when returning from morph ball state (unimplemented so far)
               isCrouched = false;
            }
            
            // //Morph Ball Unimplemented (Keep for later)
            // RaycastHit2D hitCeiling = Physics2D.Raycast(upper right corner, Vector2.up, 2.0f, default);
            // RaycastHit2D hitCeiling = Physics2D.Raycast(upper left corner, Vector2.up, 2.0f, default);
            if ((vertInput < 0f) && isCrouched && _powerSuit.IsEnabled(PowerSuit.Upgrade.MorphBall))
            {
                //Enter morph ball state
            }
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
            if (CanWallJump())     
            {
                if (Input.GetButtonDown("Jump"))
                {
                    WallJump();
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                FireWeapon();
            }
 
        }

        


        public void LateUpdate()
        {
            
        }

        #endregion
        
        #region Internal Functions
        
        private void ColliderUpdate()
        {
            _powerSuitTransform = transform.GetChild(0);
            _sprite = _powerSuitTransform.GetComponent<SpriteRenderer>().sprite;
            _boxCollider2D.size = new Vector2(_originalColliderSize.x, _sprite.bounds.size.y); 
            _boxCollider2D.offset = new Vector2(0.0f,_sprite.bounds.size.y / 2); 
            _powerSuitTransform.position = new Vector3(transform.position.x, 
                                                    transform.position.y + _sprite.bounds.size.y / 2,
                                                    0.0f);
            _cc2D.RecalculateDistanceBetweenRays();
        }
        
        private void FacingUpdate(float horizInput, float vertInput)
        {
            if (horizInput > 0)
            {
                isFacingRight = true;
            }
            else if (horizInput < 0)
            {
                isFacingRight = false;
            }
        }
        
        private void Jump(float horizInput)
        {
            if (Mathf.Abs(horizInput) > 0.15)     //Somersault
            {
                _moveDirection.y = jumpSpeed;
                isJumping = true;
                isHighJumping = false;
            }
            else                                  //HighJump
            {
                _moveDirection.y = jumpSpeed;
                isJumping = true;
                isHighJumping = true;
            }
           
        }

        private bool CanWallJump()
        {
            if (isHighJumping || isGrounded || isFalling || hasWallJumped)         //High jumping or on ground or falling, no wall jump
            {
                wallJumpAble = false;
                return false;
            }
            if (_collisionState.Left || _collisionState.Right)    //Wall to the left or right
            {
                if (_collisionState.Above || _collisionState.Below)       //In a corner... cannot wall jump
                {
                    wallJumpAble = false;
                    return false;
                }
                else                                                      //Not in a corner... wall jump!
                {
                    wallJumpAble = true;
                    return true;
                }
            }
            else
            {
                wallJumpAble = false;
                return false;
            }
        }
        
        private void WallJump()
        {
            isJumping = true;
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
        
        private void FireWeapon()
        {
            isShooting = true;
            StartCoroutine(IsShooting());
            if (_canShoot)
            {
                GameObject projectile = _powerSuit.GetSelectedWeapon();

                if (isFacingRight)
                {
                    Vector3 position = new Vector3(transform.position.x + 0.25f, transform.position.y + 1.6f, 0.0f);
                    GameObject instantiatedProjectile = Instantiate(projectile, position, Quaternion.identity);
                    IWeapon weapon = instantiatedProjectile.GetComponent<PowerBeam>();
                    if (weapon == null)
                    {
                        Debug.LogError("Couldn't find IWeapon implementation on beam weapon.");
                    }
                    else
                    {
                        weapon.SetDirection(Vector3.right);
                    }

                }
                else
                {
                    Vector3 position = new Vector3(transform.position.x - 0.25f, transform.position.y + 1.6f, 0.0f);
                    GameObject instantiatedProjectile = Instantiate(projectile, position, Quaternion.identity);
                    IWeapon weapon = instantiatedProjectile.GetComponent<PowerBeam>();
                    if (weapon == null)
                    {
                        Debug.LogError("Couldn't find IWeapon implementation on beam weapon.");
                    }
                    else
                    {
                        weapon.SetDirection(Vector3.left);
                    }

                }

                _canShoot = false;
                StartCoroutine(ShootCooldown());
            }
        }
        
        #endregion

        #region Coroutines

        private IEnumerator WallJumpCooldown()
        {
            hasWallJumped = true;
            yield return new WaitForSeconds(wallJumpControlDelay);
            hasWallJumped = false;
        }

        private IEnumerator IsShooting()
        {
            yield return new WaitForSeconds(1.0f);
            isShooting = false;
        }

        private IEnumerator ShootCooldown()
        {
            yield return new WaitForSeconds(weaponCooldown);
            _canShoot = true;
        }
        

        #endregion
    }
}


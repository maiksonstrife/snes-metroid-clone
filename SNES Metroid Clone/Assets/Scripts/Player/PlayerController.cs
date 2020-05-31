using System;
using System.Collections;
using Control;
using UnityEngine;

using Equipment;
using Player.State;
using Unity.Mathematics;
using UnityEngine.Serialization;
using Weapons;

namespace Player
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Private Fields

        private CharacterController2D _cc2D;
        public CharacterController2D CC2D => _cc2D;
        public BoxCollider2D _boxCollider2D;
        private CharacterController2D.CharacterCollisionState2D _collisionState;

        public PlayerAnimator Animator;

        public CharacterController2D.CharacterCollisionState2D CollisionState
        {
            get => _collisionState;
            set => _collisionState = value;
        }

        private Sprite _sprite;
        private Transform _powerSuitTransform;
        public PowerSuit powerSuit;

        private Vector2 _originalColliderSize;

        private ControllerInput _controllerInput;

        #endregion

        #region State

        private PlayerState _currentState;
        [NonSerialized] public StandingState standingState;
        [NonSerialized] public CrouchingState crouchingState;
        [NonSerialized] public HighJumpState highJumpState;
        [NonSerialized] public SomersaultState somersaultState;
        [NonSerialized] public MorphBallState morphBallState;
        [NonSerialized] public FallingState fallingState;
        
        public bool isGrounded;
        public bool isFacingRight;

        public bool isShooting;
        private bool _canShoot = true;

        #endregion
        

        #region Tunable Fields
        
        [SerializeField] public float speed = 6.0f;
        [SerializeField] public float jumpSpeed = 8.0f;
        [SerializeField] public float ballJumpSpeed = 6.0f;
        [SerializeField] public float gravity = 20.0f;
        [SerializeField] public float wallJumpMultiplier = 1.5f;
        [SerializeField] public float wallJumpControlDelay = 0.5f;
        [SerializeField] public float weaponCooldown = 0.25f;
        
        #endregion

        #region Movement

        public Vector3 moveDirection = Vector3.zero;

        #endregion

 
        #region Monobehaviour
        
        public void Start()
        {
            _cc2D = GetComponent<CharacterController2D>();    //No null check, required component above.
            _boxCollider2D = GetComponent<BoxCollider2D>();   //No null check, required component with CC2D.
            powerSuit = GetComponentInChildren<PowerSuit>();
            if(powerSuit == null) Debug.LogError("No power suit attached to player.");

            Animator = GetComponent<PlayerAnimator>();
            
            _controllerInput = new ControllerInput();
            _originalColliderSize = _boxCollider2D.size;
            
            standingState = new StandingState(this);
            crouchingState = new CrouchingState(this);
            highJumpState = new HighJumpState(this);
            somersaultState = new SomersaultState(this);
            morphBallState = new MorphBallState(this);
            fallingState = new FallingState(this);
            
            TransitionToState(standingState);
        }

        public void Update()
        {
            //Ensure collider reflects current sprite
            ColliderUpdate();
            
            //Collect Input -> updates _controllerInput
            _controllerInput.Update();
            
            //Update Current State
            _currentState.Update(_controllerInput);

            //TODO work shooting into state management
            if (_controllerInput.Shoot)
            {
                FireWeapon();
            }
 
        }

        


        public void LateUpdate()
        {
            
        }

        #endregion

        #region State Management

        public void TransitionToState(PlayerState state)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            _currentState = state;
            _currentState.EnterState();
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
        
        
        private void FireWeapon()
        {
            isShooting = true;
            StartCoroutine(IsShooting());
            if (_canShoot)
            {
                GameObject projectile = powerSuit.GetSelectedWeapon();
                Vector3 position = Vector3.zero;
                Quaternion rotation = Quaternion.identity;
                
                if (isFacingRight)
                {
                    position = new Vector3(transform.position.x + 0.25f, transform.position.y + 1.6f, 0.0f);
                    rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

                }
                else
                {
                    position = new Vector3(transform.position.x - 0.25f, transform.position.y + 1.6f, 0.0f);
                }
                GameObject instantiatedProjectile = Instantiate(projectile, position, rotation);
                IWeapon weapon = instantiatedProjectile.GetComponent<IWeapon>();
                if (weapon == null)
                {
                    Debug.LogError("Couldn't find IWeapon implementation on beam weapon.");
                }
                else
                {
                    weapon.SetDirection(Vector3.left);
                }
                _canShoot = false;
                StartCoroutine(ShootCooldown());
            }
        }


        #endregion

        #region Coroutines

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


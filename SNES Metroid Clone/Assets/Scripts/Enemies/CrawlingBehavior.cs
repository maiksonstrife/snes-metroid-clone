using System;
using Control;
using Enemies.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    [RequireComponent(typeof(CharacterController2D))] [RequireComponent(typeof(BoxCollider2D))]
    public class CrawlingBehavior : MonoBehaviour
    {
        private EnemyState _currentState;
        public Vector3 gravityDirection;
        public Vector3 moveDirection;
        
        [SerializeField] public float speed = 3.0f;
        [SerializeField] public float gravity = 20.0f;
        
        public FallingState FallingState;
        public CrawlingState CrawlingState;
        public NegotiateCornerState NegotiateCorner;
        public NegotiateLostGroundState NegotiateLostGround;
        
        private CharacterController2D _controller;
        public CharacterController2D.CharacterCollisionState2D collisionState;
        private BoxCollider2D _collider;
        
        public void Start()
        {
            gravityDirection = Vector3.down;
            moveDirection = Vector3.left;
            _controller = GetComponent<CharacterController2D>();
            _collider = GetComponent<BoxCollider2D>();
            FallingState = new FallingState(this);
            CrawlingState = new CrawlingState(this);
            NegotiateCorner = new NegotiateCornerState(this);
            NegotiateLostGround = new NegotiateLostGroundState(this);

            _currentState = FallingState;
        }

        public void Update()
        {
            _currentState.Update();
        }

        public bool IsGrounded()
        {
            Vector2 pt1 = new Vector2(transform.position.x, transform.position.y);
            Vector2 pt2 = new Vector2(transform.position.x, transform.position.y);
            
            RaycastHit2D hit1;
            RaycastHit2D hit2;
            
            if ((gravityDirection == Vector3.down))
            {
                pt1 = pt1 + new Vector2(-(_collider.size.x / 2), -(_collider.size.y / 2));
                pt2 = pt2 + new Vector2(_collider.size.x / 2, -(_collider.size.y / 2));

                hit1 = Physics2D.Raycast(pt1, Vector2.down, 0.1f);
                hit2 = Physics2D.Raycast(pt2, Vector2.down, 0.1f);
                
                return (hit1.collider || hit2.collider);
            }

            if ((gravityDirection == Vector3.up))
            {
                pt1 = pt1 + new Vector2(-(_collider.size.x / 2), (_collider.size.y / 2));
                pt2 = pt2 + new Vector2(_collider.size.x / 2, (_collider.size.y / 2));

                hit1 = Physics2D.Raycast(pt1, Vector2.up, 0.2f);
                hit2 = Physics2D.Raycast(pt2, Vector2.up, 0.2f);
                
                return (hit1.collider || hit2.collider);
            }

            if ((gravityDirection == Vector3.left))
            {
                pt1 = pt1 + new Vector2(-(_collider.size.x / 2), (_collider.size.y / 2));
                pt2 = pt2 + new Vector2(-(_collider.size.x / 2), -(_collider.size.y / 2));

                hit1 = Physics2D.Raycast(pt1, Vector2.left, 0.2f);
                hit2 = Physics2D.Raycast(pt2, Vector2.left, 0.2f);
                
                return (hit1.collider || hit2.collider);
            }

            if ((gravityDirection == Vector3.right))
            {
                pt1 = pt1 + new Vector2((_collider.size.x / 2), (_collider.size.y / 2));
                pt2 = pt2 + new Vector2((_collider.size.x / 2), -(_collider.size.y / 2));

                hit1 = Physics2D.Raycast(pt1, Vector2.right, 0.2f);
                hit2 = Physics2D.Raycast(pt2, Vector2.right, 0.2f);
                
                return (hit1.collider || hit2.collider);
            }

            return false;
        }

        public bool InCorner()
        {
            if ((collisionState.Above || collisionState.Below) && (collisionState.Right || collisionState.Left))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void SetRotation(Vector3 gravityVector)
        {
            if (gravityVector == Vector3.down)
            {
                transform.rotation = Quaternion.identity;
                GetComponent<SpriteRenderer>().flipY = false;
            }
            else if (gravityVector == Vector3.up)
            {
                transform.rotation = Quaternion.identity;
                GetComponent<SpriteRenderer>().flipY = true;
            }
            else if (gravityVector == Vector3.left)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                GetComponent<SpriteRenderer>().flipY = false;
            }
            else if (gravityVector == Vector3.right)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                GetComponent<SpriteRenderer>().flipY = true;
            }
        
        }
        
        public void TransitionToState(EnemyState state)
        {
            _currentState?.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }
    }
    
}
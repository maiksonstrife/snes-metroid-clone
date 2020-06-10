using Control;
using UnityEngine;

namespace Enemies.States
{
    public class NegotiateLostGroundState : EnemyState
    {
        private int movesWithoutGravity = 30;
        private int moves;
        
        public NegotiateLostGroundState(CrawlingBehavior fsm)
        {
            _root = fsm;
            _CC2D = _root.GetComponent<CharacterController2D>();
        }
        public override void EnterState()
        {
            //Debug.Log("Negotiating Lost Ground");
            NegotiateLostGround(_root.moveDirection, _root.gravityDirection);
            moves = 0;
        }

        public override void Update()
        {
            Vector3 totalMove = Vector3.zero;

            totalMove += _root.moveDirection * _root.speed;
            if (moves > movesWithoutGravity)
            {
                totalMove += _root.gravityDirection * (_root.gravity * Time.deltaTime);
            }
            else
            {
                totalMove += -_root.gravityDirection * (_root.gravity * Time.deltaTime);
            }
            
            _CC2D.Move(totalMove * Time.deltaTime);
            _root.collisionState = _CC2D.collisionState;

            if (_root.IsGrounded())
            {
                _root.TransitionToState(_root.CrawlingState);
            }

            moves++;
        }

        public override void ExitState()
        {
            //Debug.Log("Regained Ground");
        }

        private void NegotiateLostGround(Vector3 moveDirection, Vector3 gravityDirection)
        {
            _root.gravityDirection = -moveDirection;
            _root.moveDirection = gravityDirection;

            
        }
        
    }
}
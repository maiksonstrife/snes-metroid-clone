using Control;
using UnityEngine;

namespace Enemies.States
{
    public class NegotiateCornerState : EnemyState
    {

        public NegotiateCornerState(CrawlingBehavior fsm)
        {
            _root = fsm;
            _CC2D = _root.GetComponent<CharacterController2D>();
        }
        public override void EnterState()
        {
            //Debug.Log("Negotiating Corner");
            Vector3 move = _root.moveDirection;
            Vector3 grav = _root.gravityDirection;
            NegotiateCorner(move, grav);
        }

        public override void Update()
        {
            Vector3 totalMove = Vector3.zero;

            totalMove += _root.gravityDirection * (_root.gravity);
            
            _CC2D.Move(totalMove * Time.deltaTime);
            _root.collisionState = _CC2D.collisionState;

            if (_root.IsGrounded())
            {
                _root.TransitionToState(_root.CrawlingState);
            }
        }

        public override void ExitState()
        {
            //Debug.Log("Corner Handled");
        }

        private void NegotiateCorner(Vector3 moveDirection, Vector3 gravityDirection)
        {
            _root.moveDirection = -gravityDirection;
            _root.gravityDirection = moveDirection;
            
        }
    }
}
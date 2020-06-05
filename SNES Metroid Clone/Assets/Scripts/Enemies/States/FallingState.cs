using Control;
using UnityEngine;

namespace Enemies.States
{
    public class FallingState : EnemyState
    {
        
        
        public FallingState(CrawlingBehavior fsm)
        {
            _root = fsm;
            _CC2D = _root.GetComponent<CharacterController2D>();
        }
        public override void EnterState()
        {
            Debug.Log("Entered Falling State");
        }

        public override void Update()
        {
            Vector3 totalMove = Vector3.zero;

            totalMove += _root.gravityDirection * (_root.gravity * Time.deltaTime);
            _CC2D.Move(totalMove);
            _root.collisionState = _CC2D.collisionState;

            if (_root.IsGrounded())
            {
                _root.SetRotation(_root.gravityDirection);
                _root.TransitionToState(_root.CrawlingState);
            }
        }

        public override void ExitState()
        {
            //Debug.Log("Exited Falling State");
        }
    }
}
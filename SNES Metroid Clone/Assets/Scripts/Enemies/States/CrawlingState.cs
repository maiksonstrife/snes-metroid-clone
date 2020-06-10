using Control;
using UnityEngine;

namespace Enemies.States
{
    public class CrawlingState : EnemyState
    {
        public CrawlingState(CrawlingBehavior fsm)
        {
            _root = fsm;
            _CC2D = _root.GetComponent<CharacterController2D>();
        }
        public override void EnterState()
        {
            //Debug.Log("Entered Crawling State");
            _root.SetRotation(_root.gravityDirection);
        }

        public override void Update()
        {
            Vector3 totalMove = Vector3.zero;
            
            totalMove = totalMove + (_root.moveDirection * _root.speed);
            totalMove = totalMove + _root.gravityDirection * (_root.gravity);
            
            _CC2D.Move(totalMove * Time.deltaTime);
            _root.collisionState = _CC2D.collisionState;

            if (_root.InCorner())
            {
                _root.TransitionToState(_root.NegotiateCorner);
            }
            else if (!_root.IsGrounded())
            {
                _root.TransitionToState(_root.NegotiateLostGround);
            }
        }

        public override void ExitState()
        {
            //Debug.Log("Exited Crawling State");
        }
    }
}
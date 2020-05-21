using UnityEngine;

namespace Player.PlayerState
{
    public abstract class PlayerState
    {
        protected PlayerCore playerCore;
        protected Animator animator;
        protected PlayerController controller;
        
        // EnterState is called upon entering the state
        public abstract void EnterState();

        // Update is called once per frame
        public abstract void Update();

        // ExitState is called upon exiting the state
        public abstract void ExitState();
        
        // State Collision Handling
        public abstract void OnCollisionEnter2D(Collision2D other);

    }
}



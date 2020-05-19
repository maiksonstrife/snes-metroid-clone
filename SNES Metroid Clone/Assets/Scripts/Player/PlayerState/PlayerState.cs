using UnityEngine;

namespace Player.PlayerState
{
    public abstract class PlayerState
    {
        protected PlayerCore playerCore;
        protected Animator animator;
        
        // EnterState is called upon entering the state
        public abstract void EnterState();

        // Update is called once per frame
        public abstract void Update();

        // ExitState is called upon exiting the state
        public abstract void ExitState();
        
    }
}



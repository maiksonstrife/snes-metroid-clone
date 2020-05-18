using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.PlayerState
{
    public abstract class PlayerState
    {
        protected PlayerCore _player;
        
        // EnterState is called upon entering the state
        public abstract void EnterState();

        // Update is called once per frame
        public abstract void Update();

        // ExitState is called upon exiting the state
        public abstract void ExitState();
        
    }
}



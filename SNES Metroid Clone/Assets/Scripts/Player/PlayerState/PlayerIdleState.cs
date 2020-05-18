using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player.PlayerState
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(PlayerCore player)
        {
            _player = player;
        }
        
        // Update is called once per frame
        public override void EnterState()
        {
            //TODO: PlayerIdleState::EnterState()
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            //TODO: PlayerIdleState::Update()
            
        }

        public override void ExitState()
        {
            //TODO: PlayerIdleState::ExitState()
            throw new System.NotImplementedException();
        }
    }
}


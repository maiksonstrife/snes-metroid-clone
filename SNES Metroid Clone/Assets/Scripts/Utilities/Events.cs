using System;
using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public static class Events
    {
        [Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }

        [Serializable] public class EventFadeComplete : UnityEvent<bool> { }
        //True for Fadeout
    }
}

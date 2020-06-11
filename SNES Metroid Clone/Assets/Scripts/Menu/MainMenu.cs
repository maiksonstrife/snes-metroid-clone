using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        //Function to receive animation events
        //Functions to play fade in/out animations

        [SerializeField] private Animation _animator;
        [SerializeField] private AnimationClip _fadeOut;
        [SerializeField] private AnimationClip _fadeIn;

        public Events.EventFadeComplete OnMainMenuFadeComplete;
        #region Monobehaviour

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        }

        #endregion

        #region EventHandling

        private void HandleGameStateChanged(GameManager.GameState currentSate, GameManager.GameState previousState)
        {
            if (previousState == GameManager.GameState.PREGAME && currentSate == GameManager.GameState.RUNNING)
            {
                FadeOut();
            }

            if (previousState != GameManager.GameState.PREGAME && currentSate == GameManager.GameState.PREGAME)
            {
                FadeIn();
            }
        }

        #endregion

        #region Animation

        public void OnFadeOutComplete()
        {
            //Debug.Log("FadeOut Complete");
            OnMainMenuFadeComplete.Invoke(true);
        }

        public void OnFadeInComplete()
        {
            //Debug.Log("FadeIn Complete");
            OnMainMenuFadeComplete.Invoke(false);
            UIManager.Instance.SetDummyCameraActive(true);
        }

        public void FadeIn()
        {
            _animator.Stop();
            _animator.clip = _fadeIn;
            _animator.Play();
        }

        public void FadeOut()
        {
            UIManager.Instance.SetDummyCameraActive(false);
            _animator.Stop();
            _animator.clip = _fadeOut;
            _animator.Play();
        }

        #endregion
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Menu;
using UnityEngine;
using Utilities;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private UnityEngine.Camera _dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;
    
    #region Monobehaviour

    private void Start()
    {
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    private void Update()
    {
        //Early Out
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }
    
    private void HandleGameStateChanged(GameManager.GameState currentSate, GameManager.GameState previousState)
    {
        _pauseMenu.gameObject.SetActive(currentSate == GameManager.GameState.PAUSED);
    }

    #endregion
    
}

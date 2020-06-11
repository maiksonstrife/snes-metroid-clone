using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities;

namespace Core
{

    
    
    //pause simulation in pause state
    
    public class GameManager : Singleton<GameManager>
    {
        //keep track of game state
        //generate other persistent systems
        
        public enum GameState { PREGAME, RUNNING, PAUSED }

        public GameObject[] systemPrefabs;
        private List<GameObject> _instancedSystemPrefabs;
        
        private string _currentLevel = string.Empty;
        
        private GameState _currentGameState = GameState.PREGAME;

        public Events.EventGameState OnGameStateChanged;
        public Events.EventFadeComplete OnMainMenuFadeComplete;
        
        public GameState CurrentGameState
        {
            get => _currentGameState;
            private set => _currentGameState = value;
        }

        private List<AsyncOperation> _loadOperations;

        #region Monobehaviour

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            
            _instancedSystemPrefabs = new List<GameObject>();
            _loadOperations = new List<AsyncOperation>();
            
            InstantiateSystemPrefabs();
            
            UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        }

        private void Update()
        {
            //Early out in pregame state
            if (GameManager.Instance.CurrentGameState == GameState.PREGAME) return;
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        protected override void OnDestroy()
        {
            foreach (GameObject instancePrefab in _instancedSystemPrefabs)
            {
                Destroy(instancePrefab);
            }
            _instancedSystemPrefabs.Clear();
            
            base.OnDestroy();
        }

        #endregion

        #region GameManagement

        public void StartGame()
        {
            LoadLevel("Debug");
        }
        
        void InstantiateSystemPrefabs()
        {
            GameObject prefabInstance;
            for (int i = 0; i < systemPrefabs.Length; i++)
            {
                prefabInstance = Instantiate(systemPrefabs[i]);
                _instancedSystemPrefabs.Add(prefabInstance);
            }
        }
        
        void UpdateState(GameState state)
        {
            GameState previousGameState = _currentGameState;
            _currentGameState = state;

            switch (_currentGameState)
            {
                case GameState.PREGAME:
                    Time.timeScale = 1.0f;
                    break;
                case GameState.RUNNING:
                    Time.timeScale = 1.0f;
                    break;
                case GameState.PAUSED:
                    Time.timeScale = 0.0f;
                    break;
                default:
                    break;
            }
            
            OnGameStateChanged.Invoke(_currentGameState, previousGameState);
            //scene transitions
        }

        public void TogglePause()
        {
            UpdateState((_currentGameState == GameState.RUNNING) ? GameState.PAUSED : GameState.RUNNING);
        }

        public void QuitGame()
        {
            //Autosave? Or saving warning? **Features for Quitting**
            Debug.Log("Quitting Application");
            Application.Quit();
        }

        public void RestartGame()
        {
            UpdateState(GameState.PREGAME);
        }

        #endregion
        
        #region LevelManagement
        
        public void LoadLevel(string levelName)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            if (ao == null)
            {
                Debug.LogError("[GameManager]: Unable to load level" + levelName);
                return;
                
            }
            
            ao.completed += OnLevelLoadComplete;
            _loadOperations.Add(ao);
            _currentLevel = levelName;
        }

        private void OnLevelLoadComplete(AsyncOperation ao)
        {
            Debug.Log("Load Complete");
            if (_loadOperations.Contains(ao))
            {
                _loadOperations.Remove(ao);
            }

            if (_loadOperations.Count == 0)
            {
                
                UpdateState(GameState.RUNNING);
            }
                
            
        }

        public void UnloadLevel(string levelName)
        {
            if (levelName == String.Empty) return;
            
            AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
            if (ao == null)
            {
                Debug.LogError("[GameManager]: Unable to unload level" + levelName);
                return;
                
            }
            ao.completed += OnLevelUnloadComplete;
        }

        private void OnLevelUnloadComplete(AsyncOperation obj)
        {
            Debug.Log("Unload Complete");
        }
        
        #endregion
        
        #region Event Handling

        private void HandleMainMenuFadeComplete(bool fadeOut)
        {
            if (!fadeOut)
            {
                UnloadLevel(_currentLevel);
            }
        }
        
        #endregion
        
    }
}

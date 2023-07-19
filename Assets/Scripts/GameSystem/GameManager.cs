using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.GameSystem.States;

namespace Assets.Scripts.GameSystem
{
    [DefaultExecutionOrder(-6000)]
    public class GameManager : MonoBehaviour, IDisposable
    {
        #region Events

        public Action SceneReloadRequested;

        #endregion Events
        
        #region Variables

        private GameStateHandler _gameStateHandler;

        [SerializeField] private List<BaseGameState> _gameStateList;

        #endregion Variables

        #region Properties

        public Action<GameStateType> GameStateChanged { get => _gameStateHandler.GameStateChanged; set => _gameStateHandler.GameStateChanged = value; }

        #endregion Properties

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        private void Initialize()
        {
            _gameStateHandler = new GameStateHandler(_gameStateList);
        }

        public void Dispose()
        {
            _gameStateHandler.Dispose();
        }

        private void ChangeGameState(GameStateType gameStateType)
        {
            _gameStateHandler.ChangeGameState(gameStateType);
        }

        public void OnPlayButtonClicked()
        {
            ChangeGameState(GameStateType.Play);
        }

        public void OnAllCarsCompleted()
        {
            ChangeGameState(GameStateType.Win);
        }

        private void ReloadScene()
        {
            SceneReloadRequested?.Invoke();
        }

        #endregion Functions
    }
}
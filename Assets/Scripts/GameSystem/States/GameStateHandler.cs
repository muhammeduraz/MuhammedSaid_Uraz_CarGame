using System;
using System.Collections.Generic;

namespace Assets.Scripts.GameSystem.States
{
    public class GameStateHandler : IDisposable
    {
        #region Events

        public Action<GameStateType> GameStateChanged;

        #endregion Events

        #region Variables

        private BaseGameState _currentGameState;

        private readonly List<BaseGameState> _gameStateList;

        #endregion Variables

        #region Functions

        public GameStateHandler(List<BaseGameState> gameStateList)
        {
            _gameStateList = gameStateList;
        }

        public void Initialize()
        {
            BaseGameState gameState = null;

            for (int i = 0; i < _gameStateList.Count; i++)
            {
                gameState = _gameStateList[i];

                if (gameState != null)
                {
                    gameState.Initialize();
                }
            }
        }

        public void Dispose()
        {
            _currentGameState = null;
        }

        public void ChangeGameState(GameStateType gameStateType)
        {
            if (_currentGameState && gameStateType == _currentGameState.gameStateType)
                return;

            DeactivateCurrentGameState();

            _currentGameState = GetGameStateByStateType(gameStateType);

            ActivateCurrentGameState();

            GameStateChanged?.Invoke(gameStateType);
        }

        private void ActivateCurrentGameState()
        {
            if (_currentGameState != null)
            {
                _currentGameState.OnStateEnter();
            }
        }

        private void DeactivateCurrentGameState()
        {
            if (_currentGameState != null)
            {
                _currentGameState.OnStateExit();
            }
        }

        private BaseGameState GetGameStateByStateType(GameStateType gameStateType)
        {
            BaseGameState gameState = null;

            for (int i = 0; i < _gameStateList.Count; i++)
            {
                gameState = _gameStateList[i];

                if (gameState != null && gameState.gameStateType == gameStateType)
                {
                    return gameState;
                }
            }

            return null;
        }

        #endregion Functions
    }
}
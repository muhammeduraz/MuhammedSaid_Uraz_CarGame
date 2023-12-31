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

        private IGameState _currentGameState;

        private readonly List<IGameState> _gameStateList;

        #endregion Variables

        #region Functions

        public GameStateHandler(List<IGameState> gameStateList)
        {
            _gameStateList = gameStateList;
        }

        public void Initialize()
        {
            IGameState gameState = null;

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

            IGameState gameState = null;

            for (int i = 0; i < _gameStateList.Count; i++)
            {
                gameState = _gameStateList[i];

                if (gameState != null)
                {
                    gameState.Dispose();
                }
            }
        }

        public void ChangeGameState(GameStateType gameStateType)
        {
            if (_currentGameState != null && gameStateType == _currentGameState.GameStateType)
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

        private IGameState GetGameStateByStateType(GameStateType gameStateType)
        {
            IGameState gameState = null;

            for (int i = 0; i < _gameStateList.Count; i++)
            {
                gameState = _gameStateList[i];

                if (gameState != null && gameState.GameStateType == gameStateType)
                {
                    return gameState;
                }
            }

            return null;
        }

        #endregion Functions
    }
}
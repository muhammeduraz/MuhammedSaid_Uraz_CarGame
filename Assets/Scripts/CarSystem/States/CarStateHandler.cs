using System;
using System.Collections.Generic;

namespace Assets.Scripts.CarSystem.States
{
    public class CarStateHandler : IDisposable
    {
        #region Variables

        private CarHandler _carHandler;
        private ICarState _currentCarState;

        private readonly List<ICarState> _carStateList;

        #endregion Variables

        #region Properties

        public ICarState CurrentCarState => _currentCarState;

        #endregion Properties

        #region Functions

        public CarStateHandler(CarHandler carHandler, List<BaseCarState> gameStateList)
        {
            _carHandler = carHandler;
            _carStateList = new List<ICarState>();

            BaseCarState loopState = null;
            foreach (BaseCarState state in gameStateList)
            {
                loopState = UnityEngine.Object.Instantiate(state);
                _carStateList.Add(state);
            }
        }

        public void Initialize()
        {
            ICarState gameState = null;

            for (int i = 0; i < _carStateList.Count; i++)
            {
                gameState = _carStateList[i];

                if (gameState != null)
                {
                    gameState.Initialize(_carHandler, this);
                }
            }
        }

        public void Dispose()
        {
            _currentCarState = null;

            ICarState gameState = null;

            for (int i = 0; i < _carStateList.Count; i++)
            {
                gameState = _carStateList[i];

                if (gameState != null)
                {
                    gameState.Dispose();
                }
            }
        }

        public void ChangeCarStateToMovement()
        {
            if (!_carHandler.IsPathCompleted)
            {
                ChangeCarState(typeof(ControlCarState));
            }
            else
            {
                ChangeCarState(typeof(AutomaticCarState));
            }
        }

        public void ChangeCarState(Type type)
        {
            if (_currentCarState != null && _currentCarState.GetType() == type)
                return;

            DeactivateCurrentCarState();

            _currentCarState = GetCarStateByStateType(type);

            ActivateCurrentCarState();
        }

        private void ActivateCurrentCarState()
        {
            if (_currentCarState != null)
            {
                _currentCarState.OnStateEnter();
            }
        }

        private void DeactivateCurrentCarState()
        {
            if (_currentCarState != null)
            {
                _currentCarState.OnStateExit();
            }
        }

        private ICarState GetCarStateByStateType(Type type)
        {
            ICarState gameState = null;

            for (int i = 0; i < _carStateList.Count; i++)
            {
                gameState = _carStateList[i];

                if (gameState != null && gameState.GetType() == type)
                {
                    return gameState;
                }
            }

            return null;
        }

        #endregion Functions
    }
}
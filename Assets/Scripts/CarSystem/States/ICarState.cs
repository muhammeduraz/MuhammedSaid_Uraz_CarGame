using System;

namespace Assets.Scripts.CarSystem.States
{
    public interface ICarState : IDisposable
    {
        #region Functions

        public void Initialize(CarHandler carHandler, CarStateHandler stateHandler);

        public void OnStateEnter();

        public void OnStateUpdate();

        public void OnStateExit();

        #endregion Functions
    }
}
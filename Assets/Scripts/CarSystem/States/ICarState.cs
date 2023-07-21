using System;

namespace Assets.Scripts.CarSystem.States
{
    public interface ICarState : IDisposable
    {
        #region Functions

        public void Initialize(CarHandler carHandler);

        public void OnStateEnter();

        public void OnStateUpdate();

        public void OnStateExit();

        #endregion Functions
    }
}
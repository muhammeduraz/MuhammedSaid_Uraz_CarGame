using UnityEngine;

namespace Assets.Scripts.CarSystem.States
{
    public abstract class BaseCarState : ScriptableObject, ICarState
    {
        #region Variables

        protected CarHandler carHandler;
        protected CarStateHandler stateHandler;

        #endregion Variables

        #region Functions

        public virtual void Initialize(CarHandler carHandler, CarStateHandler stateHandler)
        {
            this.carHandler = carHandler;
            this.stateHandler = stateHandler;
        }

        public virtual void Dispose()
        {
            carHandler = null;
        }

        public virtual void OnStateEnter()
        {
            
        }

        public virtual void OnStateUpdate()
        {
            
        }

        public virtual void OnStateExit()
        {

        }

        #endregion Functions
    }
}
using UnityEngine;

namespace Assets.Scripts.CarSystem.States
{
    public abstract class BaseCarState : ScriptableObject, ICarState
    {
        #region Variables

        protected CarHandler carHandler;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public virtual void Initialize(CarHandler carHandler)
        {
            this.carHandler = carHandler;
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
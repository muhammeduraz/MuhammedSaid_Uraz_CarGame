using UnityEngine;

namespace Assets.Scripts.CarSystem.States
{
    [CreateAssetMenu (fileName = "AutomaticCarState", menuName = "Scriptable Objects/CarSystem/States/AutomaticCarState")]
    public class AutomaticCarState : BaseCarState
    {
        #region Variables



        #endregion Variables

        #region Functions

        public override void Initialize(CarHandler carHandler, CarStateHandler stateHandler)
        {
            base.Initialize(carHandler, stateHandler);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}
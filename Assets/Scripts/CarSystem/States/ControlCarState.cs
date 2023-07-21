using UnityEngine;

namespace Assets.Scripts.CarSystem.States
{
    [CreateAssetMenu (fileName = "ControlCarState", menuName = "Scriptable Objects/CarSystem/States/ControlCarState")]
    public class ControlCarState : BaseCarState
    {
        #region Variables



        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public override void Initialize(CarHandler carHandler)
        {
            base.Initialize(carHandler);
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

            carHandler.Move();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}
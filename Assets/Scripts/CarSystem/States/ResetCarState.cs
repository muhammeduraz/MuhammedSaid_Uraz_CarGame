using UnityEngine;

namespace Assets.Scripts.CarSystem.States
{
    [CreateAssetMenu (fileName = "ResetCarState", menuName = "Scriptable Objects/CarSystem/States/ResetCarState")]
    public class ResetCarState : BaseCarState
    {
        #region Functions

        public override void Initialize(CarHandler carHandler, CarStateHandler stateHandler)
        {
            base.Initialize(carHandler, stateHandler);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            StopMovement();
            stateHandler.ChangeCarState(typeof(IdleCarState));
        }

        private void StopMovement()
        {
            carHandler.IsMovementActive = false;

            carHandler.Rigidbody.velocity = Vector2.zero;
            carHandler.Rigidbody.angularVelocity = 0f;
        }

        #endregion Functions
    }
}
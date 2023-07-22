using UnityEngine;

namespace Assets.Scripts.CarSystem.States
{
    [CreateAssetMenu (fileName = "IdleCarState", menuName = "Scriptable Objects/CarSystem/States/IdleCarState")]
    public class IdleCarState : BaseCarState
    {
        #region Functions

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            StopMovement();
        }

        private void StopMovement()
        {
            //carHandler.IsMovementActive = false;

            carHandler.Rigidbody.velocity = Vector2.zero;
            carHandler.Rigidbody.angularVelocity = 0f;
        }

        #endregion Functions
    }
}
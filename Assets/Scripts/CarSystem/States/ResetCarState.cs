using UnityEngine;

namespace Assets.Scripts.CarSystem.States
{
    [CreateAssetMenu (fileName = "ResetCarState", menuName = "Scriptable Objects/CarSystem/States/ResetCarState")]
    public class ResetCarState : BaseCarState
    {
        #region Functions

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            SetPositionToStartPosition();
            stateHandler.ChangeCarState(typeof(IdleCarState));
        }

        public void SetPositionToStartPosition()
        {
            carHandler.transform.position = carHandler.PathHandler.PathData.StartPosition;

            carHandler.CurrentRotation = carHandler.PathHandler.PathData.StartRotation;
            carHandler.Rigidbody.rotation = carHandler.CurrentRotation;
        }

        #endregion Functions
    }
}
using UnityEngine;
using Assets.Scripts.InputSystem;

namespace Assets.Scripts.CarSystem.States
{
    [CreateAssetMenu (fileName = "ControlCarState", menuName = "Scriptable Objects/CarSystem/States/ControlCarState")]
    public class ControlCarState : BaseCarState
    {
        #region Variables

        private float _currentInputValue;

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

            SubscribeToInput(true);
            StartMovement();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            _currentInputValue = carHandler.GetInputValue();
            carHandler.PathHandler.LogPathData(_currentInputValue);

            Move();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            SubscribeToInput(false);
        }

        public void StartMovement()
        {
            carHandler.IsMovementActive = true;
            carHandler.Rigidbody.simulated = true;
        }

        private void SubscribeToInput(bool subscribe)
        {
            InputHandler inputHandler = FindObjectOfType<InputHandler>();

            if (subscribe)
            {
                carHandler.GetInputValue += inputHandler.GetInputValue;
            }
            else
            {
                carHandler.GetInputValue -= inputHandler.GetInputValue;
            }
        }

        private void Move()
        {
            carHandler.CurrentRotation += _currentInputValue * carHandler.Settings.RotationSpeed * -10f * Time.deltaTime;

            carHandler.Rigidbody.velocity = carHandler.transform.up * carHandler.Settings.MovementSpeed;
            carHandler.Rigidbody.rotation = Mathf.Lerp(carHandler.Rigidbody.rotation, carHandler.CurrentRotation, carHandler.Settings.RotationLerpSpeed * Time.deltaTime);
        }

        #endregion Functions
    }
}
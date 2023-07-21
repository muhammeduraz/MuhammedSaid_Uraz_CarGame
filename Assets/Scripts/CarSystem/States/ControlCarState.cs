using UnityEngine;
using Assets.Scripts.InputSystem;

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

            SubscribeToInput(true);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            Move();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            SubscribeToInput(false);
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
            carHandler.CurrentRotation += carHandler.GetInputValue() * carHandler.Settings.RotationSpeed * -10f * Time.deltaTime;

            carHandler.Rigidbody.velocity = carHandler.transform.up * carHandler.Settings.MovementSpeed;
            carHandler.Rigidbody.rotation = Mathf.Lerp(carHandler.Rigidbody.rotation, carHandler.CurrentRotation, carHandler.Settings.RotationLerpSpeed * Time.deltaTime);
        }

        #endregion Functions
    }
}
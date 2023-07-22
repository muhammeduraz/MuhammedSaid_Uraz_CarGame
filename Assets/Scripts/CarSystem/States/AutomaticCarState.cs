using UnityEngine;

namespace Assets.Scripts.CarSystem.States
{
    [CreateAssetMenu (fileName = "AutomaticCarState", menuName = "Scriptable Objects/CarSystem/States/AutomaticCarState")]
    public class AutomaticCarState : BaseCarState
    {
        #region Variables

        private int _currentIVDDataIndex;

        private IVDData _currentIVDData;

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

            _currentIVDData = carHandler.PathHandler.GetIVDDataByIndex(_currentIVDDataIndex);
            
            StartMovement();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            MoveThroughPath();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            _currentIVDDataIndex = 0;
        }

        public void StartMovement()
        {
            carHandler.IsMovementActive = true;
            carHandler.Rigidbody.simulated = true;
        }

        private void MoveThroughPath()
        {
            if (_currentIVDData == null) return;

            _currentIVDData.duration -= Time.deltaTime;

            carHandler.CurrentRotation += _currentIVDData.value * carHandler.Settings.RotationSpeed * -10f * Time.deltaTime;

            carHandler.Rigidbody.velocity = carHandler.transform.up * carHandler.Settings.MovementSpeed;
            carHandler.Rigidbody.rotation = Mathf.Lerp(carHandler.Rigidbody.rotation, carHandler.CurrentRotation, carHandler.Settings.RotationLerpSpeed * Time.deltaTime);

            CheckIfCurrentIVDDataDurationConsumed();
        }

        private void OnCurrentIVDDataDurationConsumed()
        {
            _currentIVDDataIndex++;
            _currentIVDData = carHandler.PathHandler.GetIVDDataByIndex(_currentIVDDataIndex);

            if (_currentIVDData == null)
                stateHandler.ChangeCarState(typeof(IdleCarState));
        }

        private void CheckIfCurrentIVDDataDurationConsumed()
        {
            if (_currentIVDData == null || (_currentIVDData != null && _currentIVDData.duration < 0))
            {
                OnCurrentIVDDataDurationConsumed();
            }
        }

        #endregion Functions
    }
}
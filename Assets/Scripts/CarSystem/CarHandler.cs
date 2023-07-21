using System;
using UnityEngine;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using Assets.Scripts.CarSystem.Data;
using Assets.Scripts.CarSystem.States;

namespace Assets.Scripts.CarSystem
{
    public class CarHandler : MonoBehaviour, IInitializable, IDisposable
    {
        #region Events

        public Action CarHitAnObstacle;
        public Action CarCompletedPath;

        public delegate float InputRequest();
        public InputRequest GetInputValue;

        #endregion Events

        #region Variables

        private bool _isMovementActive;

        private float _currentRotation;

        private CarPathData _pathData;
        private CarStateHandler _stateHandler;

        [SerializeField] private CarSettings _settings;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private List<BaseCarState> _carStateList;

        #endregion Variables

        #region Properties

        public bool IsMovementActive { get => _isMovementActive; set { _isMovementActive = value; enabled = value; } }
        public float CurrentRotation { get => _currentRotation; set => _currentRotation = value; }
        
        public CarPathData PathData { get => _pathData; }
        public CarSettings Settings { get => _settings; }
        public Rigidbody2D Rigidbody { get => _rigidbody; }

        #endregion Properties

        #region Unity Functions

        private void Update()
        {
            _stateHandler.CurrentCarState.OnStateUpdate();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        public void Initialize()
        {
            _stateHandler = new CarStateHandler(this, _carStateList);
            _stateHandler.Initialize();
            _stateHandler.ChangeCarState(typeof(ControlCarState));

            SetPositionToStartPosition();
            StopMovement();
        }

        public void Dispose()
        {
            _settings = null;
            _pathData = null;
            _rigidbody = null;

            _stateHandler.Dispose();
            _stateHandler = null;
        }

        public void OnPathCompleted()
        {
            _stateHandler.ChangeCarState(typeof(AutomaticCarState));
        }

        public void SetPathData(CarPathData pathData)
        {
            _pathData = pathData;
        }

        public void StartMovement()
        {
            IsMovementActive = true;
            _rigidbody.simulated = true;
        }

        public void StopMovement()
        {
            IsMovementActive = false;

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
        }

        public void SetPositionToStartPosition()
        {
            transform.position = _pathData.StartPosition;

            _currentRotation = _pathData.StartRotation;
            _rigidbody.rotation = _currentRotation;
        }

        #endregion Functions

        #region Trigger Functions

        private void OnTriggerEnter2D(Collider2D collider)
        {
            collider.TryGetComponent(out ITriggerable triggerable);

            if (triggerable == null) return;

            triggerable.OnTriggered(this);
        }

        #endregion Trigger Functions
    }
}
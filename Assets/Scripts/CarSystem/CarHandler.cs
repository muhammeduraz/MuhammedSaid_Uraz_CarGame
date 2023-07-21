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

        private bool _isPathCompleted;
        private bool _isMovementActive;

        private float _currentRotation;

        private CarPathData _pathData;
        private CarStateHandler _stateHandler;

        [SerializeField] private CarSettings _settings;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private List<BaseCarState> _carStateList;

        #endregion Variables

        #region Properties

        public bool IsPathCompleted { get => _isPathCompleted; }
        public bool IsMovementActive { get => _isMovementActive; set { _isMovementActive = value; enabled = value; } }
        public float CurrentRotation { get => _currentRotation; set => _currentRotation = value; }
        
        public CarPathData PathData { get => _pathData; }
        public CarSettings Settings { get => _settings; }
        public Rigidbody2D Rigidbody { get => _rigidbody; }
        public CarStateHandler StateHandler { get => _stateHandler; }

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
            _stateHandler.ChangeCarState(typeof(IdleCarState));

            SetPositionToStartPosition();
        }

        public void Dispose()
        {
            _settings = null;
            _pathData = null;
            _rigidbody = null;

            _stateHandler.Dispose();
            _stateHandler = null;
        }

        public void SetPathData(CarPathData pathData)
        {
            _pathData = pathData;
        }

        public void OnPathCompleted()
        {
            _stateHandler.ChangeCarState(typeof(ResetCarState));
            _isPathCompleted = true;
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
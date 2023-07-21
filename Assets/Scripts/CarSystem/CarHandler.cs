using System;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.InputSystem;
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

        private const float DegreeToRadian = Mathf.PI / 180f;

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
        public CarPathData PathData { get => _pathData; }

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
            //transform.rotation = new Quaternion(0f, 0f, Mathf.Sin(-_pathData.StartRotation * DegreeToRadian), Mathf.Cos(-_pathData.StartRotation * DegreeToRadian));

            _currentRotation = _pathData.StartRotation;
            _rigidbody.rotation = _currentRotation;
        }

        public void Move()
        {
            _currentRotation += GetInputValue() * _settings.RotationSpeed * -10f * Time.deltaTime;

            _rigidbody.velocity = transform.up * _settings.MovementSpeed;
            _rigidbody.rotation = Mathf.Lerp(_rigidbody.rotation, _currentRotation, _settings.RotationLerpSpeed * Time.deltaTime);
        }

        public void SubscribeToInput(bool subscribe)
        {
            InputHandler inputHandler = FindObjectOfType<InputHandler>();

            if (subscribe)
            {
                GetInputValue += inputHandler.GetInputValue;
            }
            else
            {
                GetInputValue -= inputHandler.GetInputValue;
            }
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
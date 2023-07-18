using System;
using UnityEngine;
using Assets.Scripts.CarSystem.Data;

namespace Assets.Scripts.CarSystem
{
    public class CarHandler : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private bool _isMovementActive;

        private float _currentRotation;

        [SerializeField] private CarSettings _settings;

        [SerializeField] private Rigidbody2D _rigidbody;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _isMovementActive = !_isMovementActive;
                _rigidbody.simulated = _isMovementActive;
            }

            if (!_isMovementActive) return;

            _currentRotation += Input.GetAxis("Horizontal") * _settings.RotationSpeed * -10f * Time.deltaTime;

            _rigidbody.velocity = transform.up * _settings.MovementSpeed;
            _rigidbody.rotation = Mathf.Lerp(_rigidbody.rotation, _currentRotation, _settings.RotationLerpSpeed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        public void Initialize()
        {
            _currentRotation = _rigidbody.rotation;
        }

        public void Dispose()
        {
            _settings = null;
            _rigidbody = null;
        }

        #endregion Functions
    }
}
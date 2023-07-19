using System;
using UnityEngine;
using Assets.Scripts.CarSystem.Data;
using Assets.Scripts.ObstacleSystem;

namespace Assets.Scripts.CarSystem
{
    public class CarHandler : MonoBehaviour, IInitializable, IDisposable
    {
        #region Events

        public Action CarHitAnObstacle;
        public Action CarCompletedPath;

        #endregion Events

        #region 

        private const float DegreeToRadian = Mathf.PI / 180f;

        private bool _isMovementActive;

        private float _currentRotation;

        [SerializeField] private CarSettings _settings;
        [SerializeField] private CarPathData _pathData;

        [SerializeField] private Rigidbody2D _rigidbody;

        #endregion Variables

        #region Properties

        public bool IsMovementActive { get => _isMovementActive; set { _isMovementActive = value; enabled = value; } }

        public CarPathData PathData { get => _pathData; }

        #endregion Properties

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            Move();
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

            SetPositionToStartPosition();
            StopMovement();
        }

        public void Dispose()
        {
            _settings = null;
            _pathData = null;
            _rigidbody = null;
        }

        public void StartMovement()
        {
            IsMovementActive = true;
            _rigidbody.simulated = true;
        }

        public void StopMovement()
        {
            IsMovementActive = false;

            _rigidbody.simulated = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
        }

        public void SetPositionToStartPosition()
        {
            transform.position = _pathData.StartPosition;
            transform.rotation = new Quaternion(0f, 0f, Mathf.Sin(_pathData.StartRotation * DegreeToRadian), Mathf.Cos(_pathData.StartRotation * DegreeToRadian));
        }

        private void Move()
        {
            _currentRotation += Input.GetAxis("Horizontal") * _settings.RotationSpeed * -10f * Time.deltaTime;

            _rigidbody.velocity = transform.up * _settings.MovementSpeed;
            _rigidbody.rotation = Mathf.Lerp(_rigidbody.rotation, _currentRotation, _settings.RotationLerpSpeed * Time.deltaTime);
        }

        private void OnHitAnObstacle()
        {
            CarHitAnObstacle?.Invoke();
        }

        #endregion Functions

        #region Trigger Functions

        private void OnTriggerEnter2D(Collider2D collider)
        {
            collider.TryGetComponent(out IObstacle obstacle);

            if (obstacle == null) return;

            obstacle.OnObstacleHit();
            OnHitAnObstacle();
        }

        #endregion Trigger Functions
    }
}
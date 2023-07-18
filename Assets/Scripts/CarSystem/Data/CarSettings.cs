using UnityEngine;

namespace Assets.Scripts.CarSystem.Data
{
    [CreateAssetMenu (fileName = "CarSettings", menuName = "Scriptable Objects/CarSystem/Data/CarSettings")]
    public class CarSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _rotationLerpSpeed;

        #endregion Variables

        #region Properties

        public float MovementSpeed { get => _movementSpeed; }
        public float RotationSpeed { get => _rotationSpeed; }
        public float RotationLerpSpeed { get => _rotationLerpSpeed; }

        #endregion Properties
    }
}
using UnityEngine;

namespace Assets.Scripts.CarSystem.Data
{
    [CreateAssetMenu (fileName = "CarPathData", menuName = "Scriptable Objects/CarSystem/Data/CarPathData")]
    public class CarPathData : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _startRotation;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _finishPosition;

        #endregion Variables

        #region Properties

        public float StartRotation { get => _startRotation; }
        public Vector2 StartPosition { get => _startPosition; }
        public Vector2 FinishPosition { get => _finishPosition; }

        #endregion Properties

        #region Editor Functions

#if UNITY_EDITOR

        public void SetData(float startRotation, Vector2 startPosition, Vector2 finishPosition)
        {
            _startRotation = startRotation;
            _startPosition = startPosition;
            _finishPosition = finishPosition;
        }

#endif

#endregion Editor Functions
    }
}
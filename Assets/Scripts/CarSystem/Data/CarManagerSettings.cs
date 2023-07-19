using UnityEngine;

namespace Assets.Scripts.CarSystem.Data
{
    [CreateAssetMenu (fileName = "CarManagerSettings", menuName = "Scriptable Objects/CarSystem/Data/CarManagerSettings")]
    public class CarManagerSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private int _carCount;

        #endregion Variables

        #region Properties

        public int CarCount { get => _carCount; }

        #endregion Properties
    }
}
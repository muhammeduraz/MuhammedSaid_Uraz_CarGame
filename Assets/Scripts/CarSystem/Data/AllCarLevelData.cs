using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.CarSystem
{
    [CreateAssetMenu (fileName = "AllCarLevelData", menuName = "Scriptable Objects/CarSystem/Data/AllCarLevelData")]
    public class AllCarLevelData : ScriptableObject
    {
        #region Variables

        [SerializeField] private List<CarLevelData> _carLevelDataList;

        #endregion Variables

        #region Functions

        public CarLevelData GetCarLevelDataByIndex(int index)
        {
            return _carLevelDataList[index % _carLevelDataList.Count];
        }

        #endregion Functions

        #region Editor Functions

#if UNITY_EDITOR

        public void AddCarLevelData(CarLevelData carLevelData)
        {
            _carLevelDataList.Add(carLevelData);
        }

#endif

#endregion Editor Functions
    }
}
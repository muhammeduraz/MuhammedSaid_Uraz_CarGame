using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.CarSystem.Data;

namespace Assets.Scripts.CarSystem
{
    [CreateAssetMenu (fileName = "CarLevelData", menuName = "Scriptable Objects/CarSystem/Data/CarLevelData")]
    public class CarLevelData : ScriptableObject
    {
        #region Variables

        [SerializeField] private List<CarPathData> _carPathDataList;

        #endregion Variables

        #region Functions

        public CarPathData GetCarPathDataByIndex(int index)
        {
            return _carPathDataList[index % _carPathDataList.Count];
        }

        #endregion Functions
    }
}
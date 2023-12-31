using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.CarSystem.Data;

namespace Assets.Scripts.CarSystem
{
    [CreateAssetMenu (fileName = "CarLevelData", menuName = "Scriptable Objects/CarSystem/Data/CarLevelData")]
    public class CarLevelData : ScriptableObject
    {
        #region Variables

        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private List<CarPathData> _carPathDataList;

        #endregion Variables

        #region Properties

        public GameObject ObstaclePrefab { get => _obstaclePrefab; }

        #endregion Properties

        #region Functions

        public CarPathData GetCarPathDataByIndex(int index)
        {
            return _carPathDataList[index % _carPathDataList.Count];
        }

        #endregion Functions

        #region Editor Functions

#if UNITY_EDITOR

        public void Initialize()
        {
            _carPathDataList = new List<CarPathData>();
        }

        public void SetCarPathDataList(List<CarPathData> carPathDataList)
        {
            _carPathDataList = new List<CarPathData>();

            foreach (CarPathData data in carPathDataList)
            {
                _carPathDataList.Add(data);
            }
        }

        public void AddCarPathDataToList(CarPathData carPathData)
        {
            _carPathDataList.Add(carPathData);
        }

        public void SetObstaclePrefabData(GameObject prefab)
        {
            _obstaclePrefab = prefab;
        }

#endif

        #endregion Editor Functions
    }
}
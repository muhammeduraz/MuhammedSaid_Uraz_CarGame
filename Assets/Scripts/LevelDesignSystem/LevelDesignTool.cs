#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Assets.Scripts.CarSystem;
using System.Collections.Generic;
using Assets.Scripts.CarSystem.Data;
using Assets.Scripts.ObstacleSystem.Finish;
using Assets.Scripts.ObstacleSystem.Obstacles;

namespace Assets.Scripts.LevelDesignSystem
{
    public class LevelDesignTool : MonoBehaviour
    {
        #region Variables

        [ReadOnly] public FinishLine latestFinishLine;
        [ReadOnly] public CarHandler latestCarHandler;

        [BoxGroup("Parents")] public Transform obstacleParent;

        [BoxGroup("Prefabs")] public FinishLine finishLine;
        [BoxGroup("Prefabs")] public CarHandler carHandler;
        [BoxGroup("Prefabs")] public BaseObstacle obstacle;

        [BoxGroup("Save Settings")] public string levelId;
        [BoxGroup("Save Settings")] public string carPathDataPath;
        [BoxGroup("Save Settings")] public string carLevelDataPath;
        [BoxGroup("Save Settings")] public AllCarLevelData _allCarLevelData;

        [BoxGroup("Data")] public DesignPathData _currentDesignPathData;
        [BoxGroup("Data")] public List<DesignPathData> _designPathDataList = new List<DesignPathData>();

        #endregion Variables

        #region Functions

        [Button(50)]
        [BoxGroup("Spawn Buttons")]
        [GUIColor(0f, 0.5f, 1f)]
        private void SpawnNewObstacle()
        {
            BaseObstacle instantiatedObstacle = Instantiate(obstacle, obstacleParent, true);
            instantiatedObstacle.name += $"_{FindObjectsOfType<BaseObstacle>().Length - 1}";
            instantiatedObstacle.transform.position = Vector3.zero;

            Selection.activeGameObject = instantiatedObstacle.gameObject;
        }

        [Button(50)]
        [BoxGroup("Spawn Buttons")]
        [GUIColor(0f, 1f, 0f)]
        private void SpawnFinishLine()
        {
            latestFinishLine = Instantiate(finishLine, transform);
            latestFinishLine.transform.position = Vector3.zero;

            Selection.activeGameObject = latestFinishLine.gameObject;
        }

        [Button(50)]
        [BoxGroup("Spawn Buttons")]
        [GUIColor(0.75f, 0.5f, 0.5f)]
        private void SpawnNewCar()
        {
            latestCarHandler = Instantiate(carHandler, transform, true);
            latestCarHandler.transform.position = Vector3.zero;

            Selection.activeGameObject = latestCarHandler.gameObject;
        }

        [Button(50)]
        [BoxGroup("Set Button")]
        [GUIColor(1f, 0.5f, 0.5f)]
        private void SetPathDataValues()
        {
            if (latestCarHandler != null)
            {
                _currentDesignPathData.startRotation = latestCarHandler.Rigidbody.rotation;
                _currentDesignPathData.startPosition = latestCarHandler.transform.position;
            }
            else
            {
                Debug.LogError("Latest car is null!");
            }

            if (latestFinishLine != null)
            {
                _currentDesignPathData.finishPosition = latestFinishLine.transform.position;
            }
            else
            {
                Debug.LogError("Latest finish line is null!");
            }
        }

        [Button(50)]
        [BoxGroup("Save Buttons")]
        [GUIColor(1f, 0f, 0.5f)]
        private void SaveAndCreateNewPathData()
        {
            _designPathDataList.Add(_currentDesignPathData);
            _currentDesignPathData = new DesignPathData();

            latestFinishLine = null;
            latestCarHandler = null;
        }

        [Button(50)]
        [BoxGroup("Save Buttons")]
        [GUIColor(1f, 0.5f, 0f)]
        private void CreateCarPathData()
        {
            if (_designPathDataList.Count < 1) return;

            DesignPathData data = null;
            CarPathData carPathData = null;

            CreateFolder(levelId);
            ClearNullObjectsFromTheDataList();

            CarLevelData carLevelData = ScriptableObject.CreateInstance<CarLevelData>();
            carLevelData.Initialize();

            for (int i = 0; i < _designPathDataList.Count; i++)
            {
                data = _designPathDataList[i];

                if (data != null)
                {
                    carPathData = ScriptableObject.CreateInstance<CarPathData>();
                    carPathData.SetData(data.startRotation, data.startPosition, data.finishPosition);

                    carLevelData.AddCarPathDataToList(carPathData);
                    AssetDatabase.CreateAsset(carPathData, Path.Combine(carPathDataPath, levelId, $"CarPathData_{i}_{levelId}.asset"));
                }
            }

            AssetDatabase.CreateAsset(carLevelData, Path.Combine(carPathDataPath, $"CarLevelData_{levelId}.asset"));
            _allCarLevelData.AddCarLevelData(carLevelData);

            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();

            _currentDesignPathData = new DesignPathData();
            _designPathDataList.Clear();
        }

        [Button(50)]
        [FoldoutGroup("Delete Buttons")]
        [GUIColor(0.5f, 0.5f, 0.5f)]
        private void DeleteAllObstacles()
        {
            FindObjectsOfType<BaseObstacle>().ToList().ForEach(obj => DestroyImmediate(obj.gameObject));
        }

        [Button(50)]
        [FoldoutGroup("Delete Buttons")]
        [GUIColor(0.5f, 0.4f, 0.5f)]
        private void DeleteAllCars()
        {
            FindObjectsOfType<CarHandler>().ToList().ForEach(obj => DestroyImmediate(obj.gameObject));
        }

        [Button(50)]
        [FoldoutGroup("Delete Buttons")]
        [GUIColor(0.4f, 0.5f, 0.4f)]
        private void DeleteAllFinishLines()
        {
            FindObjectsOfType<FinishLine>().ToList().ForEach(obj => DestroyImmediate(obj.gameObject));
        }

        private void CreateFolder(string folderName)
        {
            if (!Directory.Exists(carPathDataPath))
            {
                Debug.LogError($"{carPathDataPath}: does not exist!");
                return;
            }

            if (string.IsNullOrEmpty(folderName))
            {
                Debug.LogError($"folderName is null or empty!");
                return;
            }

            if (Directory.Exists(Path.Combine(carPathDataPath, $"{folderName}")))
            {
                Debug.LogError($"{Path.Combine(carPathDataPath, $"{folderName}")}: does exist!");
                return;
            }

            Directory.CreateDirectory(Path.Combine(carPathDataPath, $"{folderName}"));
        }

        private void ClearNullObjectsFromTheDataList()
        {
            DesignPathData data = null;

            for (int i = _designPathDataList.Count - 1; i >= 0; i--)
            {
                data = _designPathDataList[i];

                if (data == null)
                {
                    _designPathDataList.RemoveAt(i);
                }
            }
        }

        #endregion Functions

        #region Inner Classes

        [Serializable]
        public class DesignPathData
        {
            public float startRotation;
            public Vector2 startPosition;
            public Vector2 finishPosition;
        }

        #endregion Inner Classes
    }
}

#endif
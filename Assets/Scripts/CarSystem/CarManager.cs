using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.LevelSystem;
using Assets.Scripts.CarSystem.Data;
using Assets.Scripts.CarSystem.States;

namespace Assets.Scripts.CarSystem
{
    public class CarManager : MonoBehaviour, IInitializable, IDisposable
    {
        #region Events

        public Action AllCarsCompleted; 
        public Action<Vector2> FinishPositionUpdated;
        public Action TapToPlayButtonAppearRequested; 
        
        #endregion Events
        
        #region Variables

        private int _currentCarIndex;

        private CarHandler _cachedCarHandler;
        private CarHandler _currentCarHandler;
        private List<CarHandler> _playedCarList;

        private CarLevelData _carLevelData;

        [SerializeField] private CarManagerSettings _settings;
        [SerializeField] private CarHandler _carHandlerPrefab;

        #endregion Variables

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        public void Initialize()
        {
            _playedCarList = new List<CarHandler>();

            FetchCarLevelData();
            DecideCarSpawnOrAllCarsCompleted();
        }

        public void Dispose()
        {
            _settings = null;
        }

        private void FetchCarLevelData()
        {
            LevelService levelService = FindObjectOfType<LevelService>();

            if (levelService == null) return;

            _carLevelData = levelService.GetCurrentCarLevelData();
        }

        private bool IsOutOfCars()
        {
            return _currentCarIndex >= _settings.CarCount;
        }

        private void DecideCarSpawnOrAllCarsCompleted()
        {
            if (!IsOutOfCars())
            {
                SpawnCar();
                ResetCars();

                TapToPlayButtonAppearRequested?.Invoke();
            }
            else
            {
                OnAllCarsCompleted();
            }
        }

        private void OnAllCarsCompleted()
        {
            AllCarsCompleted?.Invoke();
        }

        public void OnCarCompletedPath()
        {
            _currentCarIndex++;
            _currentCarHandler.OnPathCompleted();

            _playedCarList.Add(_currentCarHandler);
            StopCarMovements();
            SubscribeToCar(false);

            DecideCarSpawnOrAllCarsCompleted();
        }

        public void OnCarHitAnObstacle()
        {
            StopCarMovements();
            ResetCars();

            TapToPlayButtonAppearRequested?.Invoke();
        }

        private void SpawnCar()
        {
            _currentCarHandler = Instantiate(_carHandlerPrefab, transform);
            _currentCarHandler.SetPathData(_carLevelData.GetCarPathDataByIndex(_currentCarIndex));
            _currentCarHandler.Initialize();

            FinishPositionUpdated?.Invoke(_currentCarHandler.PathHandler.PathData.FinishPosition);

            SubscribeToCar(true);
        }

        private void SubscribeToCar(bool subscribe)
        {
            if (subscribe)
            {
                _currentCarHandler.CarCompletedPath += OnCarCompletedPath;
                _currentCarHandler.CarHitAnObstacle += OnCarHitAnObstacle;
            }
            else
            {
                _currentCarHandler.CarCompletedPath -= OnCarCompletedPath;
                _currentCarHandler.CarHitAnObstacle -= OnCarHitAnObstacle;
            }
        }

        public void OnTapToPlayButtonClicked()
        {
            StartCarMovements();
        }

        public void StartCarMovements()
        {
            for (int i = 0; i < _playedCarList.Count; i++)
            {
                _cachedCarHandler = _playedCarList[i];

                if (_cachedCarHandler != null)
                {
                    _cachedCarHandler.StateHandler.ChangeCarStateToMovement();
                }
            }

            _currentCarHandler.StateHandler.ChangeCarStateToMovement();
        }

        public void StopCarMovements()
        {
            for (int i = 0; i < _playedCarList.Count; i++)
            {
                _cachedCarHandler = _playedCarList[i];

                if (_cachedCarHandler != null)
                {
                    _cachedCarHandler.StateHandler.ChangeCarState(typeof(IdleCarState));
                }
            }

            _currentCarHandler.StateHandler.ChangeCarState(typeof(IdleCarState));
        }

        public void ResetCars()
        {
            for (int i = 0; i < _playedCarList.Count; i++)
            {
                _cachedCarHandler = _playedCarList[i];

                if (_cachedCarHandler != null)
                {
                    _cachedCarHandler.StateHandler.ChangeCarState(typeof(ResetCarState));
                }
            }

            _currentCarHandler.StateHandler.ChangeCarState(typeof(ResetCarState));
        }

        #endregion Functions
    }
}
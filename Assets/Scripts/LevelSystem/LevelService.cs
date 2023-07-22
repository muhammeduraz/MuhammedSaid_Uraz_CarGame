using System;
using UnityEngine;
using Assets.Scripts.CarSystem;
using Assets.Scripts.SaveSystem;
using UnityEngine.SceneManagement;
using Assets.Scripts.LevelSystem.Data;
using Assets.Scripts.GameSystem.States;

namespace Assets.Scripts.LevelSystem
{
    [DefaultExecutionOrder(100)]
    public class LevelService : MonoBehaviour, IDisposable
    {
        #region Events

        public Action<LevelData, LoadSceneMode> LevelLoadRequested;
        public Action<LevelData, UnloadSceneOptions> LevelUnloadRequested;
        public Action<LevelData, UnloadSceneOptions, LevelData, LoadSceneMode> LevelUnloadAndLoadRequested;

        #endregion Events

        #region Variables

        private const string LevelIndexSaveKey = "LevelIndexSaveKey";

        [SerializeField] private LevelData _mainScene;
        [SerializeField] private LevelData _gameScene;

        [SerializeField] private AllCarLevelData _allCarLevelData;

        #region Properties

        public int LevelIndex
        { 
            get
            {
                return SaveService.Load(LevelIndexSaveKey, 0);
            }
            set 
            {
                SaveService.Save(LevelIndexSaveKey, value);
            } 
        }

        #endregion Properties
        
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
            LoadGameScene();
        }

        public void Dispose()
        {
            _mainScene = null;
            _gameScene = null;
        }

        private void LoadLevel(LevelData levelData, LoadSceneMode loadSceneMode)
        {
            LevelLoadRequested?.Invoke(levelData, loadSceneMode);
        }

        public void OnLevelCompleted()
        {
            LevelIndex++;
        }

        public CarLevelData GetCurrentCarLevelData()
        {
            return _allCarLevelData.GetCarLevelDataByIndex(LevelIndex);
        }

        public void LoadGameScene()
        {
            LoadLevel(_gameScene, LoadSceneMode.Additive);
        }

        public void OnGameStateChangedToWin(GameStateType gameStateType)
        {
            if (gameStateType == GameStateType.Win)
            {
                OnLevelCompleted();
            }
        }

        #endregion Functions
    }
}
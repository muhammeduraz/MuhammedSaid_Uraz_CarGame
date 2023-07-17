using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.LevelSystem.Data;

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

        [SerializeField] private LevelData _mainScene;
        [SerializeField] private LevelData _menuScene;
        [SerializeField] private LevelData _gameScene;

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
            LoadLevel(_menuScene, LoadSceneMode.Additive);
        }

        public void Dispose()
        {
            _mainScene = null;
            _menuScene = null;
            _gameScene = null;
        }

        private void LoadLevel(LevelData levelData, LoadSceneMode loadSceneMode)
        {
            LevelLoadRequested?.Invoke(levelData, loadSceneMode);
        }

        public void LoadGameSceneFromMenu()
        {
            LevelUnloadAndLoadRequested?.Invoke(_menuScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects, _gameScene, LoadSceneMode.Additive);
        }

        #endregion Functions
    }
}
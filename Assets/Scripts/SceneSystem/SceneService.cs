using System;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using Assets.Scripts.LevelSystem.Data;

namespace Assets.Scripts.SceneSystem
{
    public class SceneService : MonoBehaviour, IInitializable, IDisposable
    {
        #region Events

        public delegate bool IsLoadingPanelVisible();
        public IsLoadingPanelVisible isLoadingPanelVisible;

        public delegate float LoadingPanelAppearDuration();
        public LoadingPanelAppearDuration loadingPanelAppearDuration;

        public Action LoadingPanelAppearRequested;
        public Action LoadingPanelDisappearRequested;
        public Action<float> LoadingPanelProgressBarUpdateRequested;
        public Action<float, float> LoadingPanelProgressBarUpdateWithDurationRequested;

        #endregion Events
        
        #region Variables

        private string _cachedString;

        [BoxGroup("Settings")][SerializeField] private float _extraLoadingDuration;

        private WaitForSeconds _waitForLoadingPanelAppear;
        private WaitForSeconds _waitForExtraLoadingDuration;

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
            _waitForLoadingPanelAppear = new WaitForSeconds(loadingPanelAppearDuration());
            _waitForExtraLoadingDuration = new WaitForSeconds(_extraLoadingDuration);
        }

        public void Dispose()
        {
            
        }

        private IEnumerator AppearLoadingPanel()
        {
            if (!isLoadingPanelVisible())
            {
                LoadingPanelAppearRequested?.Invoke();
                yield return _waitForLoadingPanelAppear;
            }
        }

        private IEnumerator DisappearLoadingPanel()
        {
            if (isLoadingPanelVisible())
            {
                LoadingPanelDisappearRequested?.Invoke();
                yield return _waitForLoadingPanelAppear;
            }
        }

        public void ReloadCurrentLevel()
        {
            _cachedString = SceneManager.GetActiveScene().name;
            StartCoroutine(LoadSceneAsync(_cachedString, LoadSceneMode.Additive));
        }

        private IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
        {
            yield return AppearLoadingPanel();

            Scene activeScene = SceneManager.GetActiveScene();
            if (loadSceneMode == LoadSceneMode.Additive && activeScene.name.Equals(sceneName))
            {
                Coroutine unloadCoroutine = StartCoroutine(UnloadSceneAsync(sceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects));
                yield return unloadCoroutine;
            }

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            while (isLoadingPanelVisible() && !loadOperation.isDone)
            {
                LoadingPanelProgressBarUpdateRequested?.Invoke(0.33f + loadOperation.progress / 3f);
                yield return null;
            }

            LoadingPanelProgressBarUpdateWithDurationRequested?.Invoke(1f, _extraLoadingDuration);

            Scene scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);

            yield return _waitForExtraLoadingDuration;

            LoadingPanelDisappearRequested?.Invoke();
        }

        private IEnumerator UnloadSceneAsync(string sceneName, UnloadSceneOptions unloadSceneOptions)
        {
            if (!isLoadingPanelVisible())
            {
                LoadingPanelAppearRequested?.Invoke();
                yield return _waitForLoadingPanelAppear;
            }

            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneName, unloadSceneOptions);

            while (!unloadOperation.isDone)
            {
                LoadingPanelProgressBarUpdateRequested?.Invoke(unloadOperation.progress / 3f);
                yield return null;
            }
        }

        private IEnumerator UnloadAndLoadScene(string unloadSceneName, UnloadSceneOptions unloadSceneOptions, string loadSceneName, LoadSceneMode loadSceneMode)
        {
            yield return StartCoroutine(AppearLoadingPanel());
            yield return StartCoroutine(UnloadSceneAsync(unloadSceneName, unloadSceneOptions));
            yield return StartCoroutine(LoadSceneAsync(loadSceneName, loadSceneMode));
            yield return StartCoroutine(DisappearLoadingPanel());
        }

        public void OnLevelLoadRequested(LevelData levelData, LoadSceneMode loadSceneMode)
        {
            StartCoroutine(LoadSceneAsync(levelData.SceneReference.SceneName, loadSceneMode));
        }

        public void OnLevelUnloadRequested(LevelData levelData, UnloadSceneOptions unloadSceneOptions)
        {
            StartCoroutine(UnloadSceneAsync(levelData.SceneReference.SceneName, unloadSceneOptions));
        }

        public void OnLevelUnloadAndLoadRequested(LevelData unloadLevelData, UnloadSceneOptions unloadSceneOptions, LevelData loadLevelData, LoadSceneMode loadSceneMode)
        {
            StartCoroutine(UnloadAndLoadScene(unloadLevelData.SceneReference.SceneName, unloadSceneOptions, loadLevelData.SceneReference.SceneName, loadSceneMode));
        }

        #endregion Functions
    }
}
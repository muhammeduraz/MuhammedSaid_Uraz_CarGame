using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Assets.Scripts.CanvasSystem.ProgressBar;
using Assets.Scripts.CanvasSystem.Loading.Data;

namespace Assets.Scripts.CanvasSystem.Loading
{
    public class LoadingPanel : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private bool _isVisible;

        [BoxGroup("Settings")][SerializeField] private LoadingPanelSettings _settings;

        [BoxGroup("Components")][SerializeField] private Image _backgroundImage;
        [BoxGroup("Components")][SerializeField] private CanvasGroup _canvasGroup;
        [BoxGroup("Components")][SerializeField] private DefaultProgressBar _progressBar;

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

        }

        public void Dispose()
        {

        }

        public bool IsVisible() => _isVisible;
        
        public float AppearDuration() => _settings.AppearDuration;

        public void Appear()
        {
            if (_isVisible) return;
            _isVisible = true;

            gameObject.SetActive(true);
            _progressBar.Appear(true);
            _canvasGroup.DOFade(1f, _settings.AppearDuration);
        }

        public void Disappear()
        {
            if (!_isVisible) return;
            _isVisible = false;

            _canvasGroup.DOFade(0f, _settings.DisappearDuration)
                .OnComplete(() => 
                {
                    gameObject.SetActive(false);
                });
        }

        private void Reset()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            _progressBar.SetValue(0f, 0f);
            _isVisible = false;
        }

        public void UpdateProgress(float progress)
        {
            _progressBar.SetValue(progress);
        }

        public void UpdateProgress(float progress, float duration = 0.25f)
        {
            _progressBar.SetValue(progress, duration);
        }

        #endregion Functions
    }
}
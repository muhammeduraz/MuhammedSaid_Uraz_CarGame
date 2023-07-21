using System;
using Assets.Scripts.GameSystem.States;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CanvasSystem.Menus
{
    public class WinMenu : MonoBehaviour, IDisposable
    {
        #region Events

        public Action NextLevelButtonClicked;
        
        #endregion Events
        
        #region Variables

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _levelCompletedText;

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

        private void Initialize()
        {
            _canvasGroup.alpha = 0f;
        }

        public void Dispose()
        {
            _canvasGroup = null;
            _nextLevelButton = null;
            _levelCompletedText = null;
        }

        private void OnNextLevelButtonClicked()
        {
            NextLevelButtonClicked?.Invoke();
            _nextLevelButton.onClick.RemoveAllListeners();
        }

        private void SetupNextLevelButton()
        {
            _nextLevelButton.onClick.RemoveAllListeners();
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
        }

        public void Appear()
        {
            SetupNextLevelButton();

            StartAppearAnimation();
        }

        private void StartAppearAnimation()
        {
            _nextLevelButton.transform.localScale = Vector3.zero;
            _levelCompletedText.transform.localScale = Vector3.zero;

            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(_canvasGroup.DOFade(1f, 0.25f).SetEase(Ease.Linear))
                .Append(_levelCompletedText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack))
                .AppendInterval(1f)
                .Append(_nextLevelButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack));
        }

        public void OnGameStateChangedToWin(GameStateType gameStateType)
        {
            if (gameStateType == GameStateType.Win)
            {
                Appear();
            }
        }

        #endregion Functions
    }
}
using TMPro;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Assets.Scripts.CanvasSystem
{
    public class PlayButtonHandler : MonoBehaviour, IInitializable, IDisposable
    {
        #region Events

        public Action PlayButtonClicked;

        #endregion Events
        
        #region Variables

        private Sequence _textSequence;

        [BoxGroup("Components")][SerializeField] private Button _playButton;
        [BoxGroup("Components")][SerializeField] private TextMeshProUGUI _playText;

        #endregion Variables

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Terminate();
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        public void Initialize()
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(OnPlayButtonClicked);

            ActivatePlayTextAnimation();
            gameObject.SetActive(true);
        }

        private void Terminate()
        {
            _playButton.onClick.RemoveAllListeners();
            DeactivatePlayTextAnimation();

            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton = null;

            _textSequence?.Kill();
            _textSequence = null;

            _playText = null;
        }

        private void OnPlayButtonClicked()
        {
            Terminate();
            PlayButtonClicked?.Invoke();
        }

        private void ActivatePlayTextAnimation()
        {
            _textSequence?.Kill();
            _textSequence = DOTween.Sequence();
            _textSequence
                .Join(_playText.transform.DOScale(1.25f, 0.5f))
                .Join(_playText.DOFade(0.75f, 0.5f))
                .Append(_playText.transform.DOScale(1f, 0.5f))
                .Join(_playText.DOFade(1f, 0.5f));

            _textSequence.SetLoops(-1, LoopType.Restart);
        }

        private void DeactivatePlayTextAnimation()
        {
            _textSequence?.Kill();
        }

        #endregion Functions
    }
}
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.InputSystem
{
    [DefaultExecutionOrder(-1000)]
    public class InputHandler : MonoBehaviour, IDisposable
    {
        #region Variables

        private InputButton _currentInputButton;

        [SerializeField] private List<InputButton> _buttonList;

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
            Input.multiTouchEnabled = false;
        }

        public void Dispose()
        {
            _buttonList = null;
            _currentInputButton = null;
        }

        public void SubscribeToButtons()
        {
            _buttonList.ForEach(button => button.PointerDown += OnPointerDown);
            _buttonList.ForEach(button => button.PointerUp += OnPointerUp);
        }

        public void UnsubscribeToButtons()
        {
            _buttonList.ForEach(button => button.PointerDown -= OnPointerDown);
            _buttonList.ForEach(button => button.PointerUp -= OnPointerUp);
        }

        private void OnPointerDown(InputButton inputButton)
        {
            _currentInputButton = inputButton;
        }

        private void OnPointerUp(InputButton inputButton)
        {
            _currentInputButton = null;
        }

        public float GetInputValue()
        {
            if (_currentInputButton != null)
                return _currentInputButton.Value;

            return 0f;
        }

        #endregion Functions
    }
}
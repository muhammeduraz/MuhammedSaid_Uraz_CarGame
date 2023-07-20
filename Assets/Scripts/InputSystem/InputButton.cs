using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Scripts.InputSystem
{
    public class InputButton : MonoBehaviour, IDisposable, IPointerDownHandler, IPointerUpHandler
    {
        #region Events

        public Action<InputButton> PointerDown;
        public Action<InputButton> PointerUp;
        
        #endregion Events
        
        #region Variables

        [SerializeField] private float _value;

        #endregion Variables

        #region Properties

        public float Value { get => _value; }

        #endregion Properties

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
        }

        public void Dispose()
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDown?.Invoke(this);
        }

        #endregion Functions
    }
}
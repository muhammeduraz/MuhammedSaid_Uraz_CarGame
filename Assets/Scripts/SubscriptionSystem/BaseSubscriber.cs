using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.SubscribeSystem
{
    [DefaultExecutionOrder(-5000)]
    public abstract class BaseSubscriber : MonoBehaviour
    {
        #region Unity Functions

        private void Awake()
        {
            Initialize();
            SafeSubscribeEvents();
        }

        private void OnDestroy()
        {
            SafeUnSubscribeEvents();
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        protected abstract void Initialize();
        protected abstract void Dispose();

        protected abstract void SubscribeEvents();
        protected abstract void UnSubscribeEvents();

        private void SafeSubscribeEvents()
        {
            try
            {
                SubscribeEvents();
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"{this}: {exception.Message}: {exception.HelpLink}");
            }        
        }
        private void SafeUnSubscribeEvents()
        {
            try
            {
                UnSubscribeEvents();
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"{this}: {exception.Message}: {exception.HelpLink}");
            }
        }

        #endregion Functions
    }
}
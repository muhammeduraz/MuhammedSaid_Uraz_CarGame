using System;
using UnityEngine;

namespace Assets.Scripts.CanvasSystem.Canvases
{
    public abstract class BaseCanvas : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        [SerializeField] protected CanvasGroup canvasGroup;

        #endregion Variables

        #region Properties



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

        public virtual void Initialize()
        {

        }

        public virtual void Dispose()
        {

        }

        #endregion Functions
    }
}
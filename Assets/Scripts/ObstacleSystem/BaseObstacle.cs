using System;
using UnityEngine;

namespace Assets.Scripts.ObstacleSystem
{
    public abstract class BaseObstacle : MonoBehaviour, IObstacle, IInitializable, IDisposable
    {
        #region Variables



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

        public virtual void OnObstacleHit()
        {

        }

        #endregion Functions
    }
}
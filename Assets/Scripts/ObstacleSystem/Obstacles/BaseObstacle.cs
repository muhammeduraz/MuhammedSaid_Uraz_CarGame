using System;
using UnityEngine;
using Assets.Scripts.CarSystem;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.ObstacleSystem.Obstacles
{
    public abstract class BaseObstacle : MonoBehaviour, ITriggerable, IInitializable
    {
        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        #endregion Unity Functions

        #region Functions

        public virtual void Initialize()
        {

        }

        public virtual void OnTriggered(CarHandler carHandler)
        {
            carHandler.CarHitAnObstacle?.Invoke();
        }

        #endregion Functions
    }
}
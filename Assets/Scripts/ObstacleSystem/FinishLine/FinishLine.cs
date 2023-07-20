using UnityEngine;
using Assets.Scripts.CarSystem;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.ObstacleSystem.Finish
{
    public class FinishLine : MonoBehaviour, ITriggerable
    {
        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        #endregion Unity Functions

        #region Functions

        private void Initialize()
        {

        }

        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        public void OnTriggered(CarHandler carHandler)
        {
            carHandler.CarCompletedPath?.Invoke();
        }

        #endregion Functions
    }
}
using UnityEngine;
using Assets.Scripts.CarSystem;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.ObstacleSystem.Finish
{
    public class FinishLine : MonoBehaviour, ITriggerable
    {
        #region Functions

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
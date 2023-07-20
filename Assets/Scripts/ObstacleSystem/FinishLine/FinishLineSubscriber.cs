using Assets.Scripts.CarSystem;
using Assets.Scripts.SubscribeSystem;
using Assets.Scripts.ObstacleSystem.Finish;

namespace Assets.Scripts.ObstacleSystem
{
    public class FinishLineSubscriber : BaseSubscriber 
    {
        #region Variables

        private FinishLine _finishLine;

        private CarManager _carManager;

        #endregion Variables

        #region Functions

        protected override void Initialize()
        {
            _finishLine = FindObjectOfType<FinishLine>();

            _carManager = FindObjectOfType<CarManager>();
        }

        protected override void Dispose()
        {
            _finishLine = null;

            _carManager = null;
        }

        protected override void SubscribeEvents()
        {
            _carManager.FinishPositionUpdated += _finishLine.UpdatePosition;
        }

        protected override void UnSubscribeEvents()
        {
            _carManager.FinishPositionUpdated -= _finishLine.UpdatePosition;
        }

        #endregion Functions
    }
}
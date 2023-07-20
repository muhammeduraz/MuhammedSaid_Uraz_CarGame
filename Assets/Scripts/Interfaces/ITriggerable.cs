using Assets.Scripts.CarSystem;

namespace Assets.Scripts.Interfaces
{
    public interface ITriggerable
    {
        #region Functions

        public void OnTriggered(CarHandler carHandler);

        #endregion Functions
    }
}
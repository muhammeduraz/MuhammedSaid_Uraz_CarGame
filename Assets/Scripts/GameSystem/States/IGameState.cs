using System;

namespace Assets.Scripts.GameSystem.States
{
    public interface IGameState : IInitializable, IDisposable
    {
        #region Properties
        
        public GameStateType GameStateType { get; }
        
        #endregion Properties
        
        #region Functions

        public void OnStateEnter();

        public void OnStateExit();

        #endregion Functions
    }
}
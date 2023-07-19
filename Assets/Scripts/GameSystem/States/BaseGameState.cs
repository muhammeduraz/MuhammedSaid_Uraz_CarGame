using UnityEngine;

namespace Assets.Scripts.GameSystem.States
{
    public class BaseGameState : ScriptableObject, IGameState
    {
        #region Variables

        public GameStateType gameStateType;

        #endregion Variables

        #region Functions

        public virtual void Initialize()
        {
            
        }

        public virtual void OnStateEnter()
        {

        }

        public virtual void OnStateExit()
        {

        }

        #endregion Functions
    }
}
namespace Assets.Scripts.GameSystem.States
{
    public interface IGameState
    {
        #region Properties
        
        public GameStateType GameStateType { get; }
        
        #endregion Properties
        
        #region Functions

        public void Initialize();

        public void OnStateEnter();

        public void OnStateExit();

        #endregion Functions
    }
}
using Assets.Scripts.GameSystem;
using Assets.Scripts.LevelSystem;
using Assets.Scripts.SubscribeSystem;

namespace Assets.Scripts.CanvasSystem.Menus
{
    public class WinMenuSubscriber : BaseSubscriber 
    {
        #region Variables

        private WinMenu _winMenu;

        private GameManager _gameManager;
        private LevelService _levelService;

        #endregion Variables

        #region Functions

        protected override void Initialize()
        {
            _winMenu = FindObjectOfType<WinMenu>();

            _gameManager = FindObjectOfType<GameManager>();
            _levelService = FindObjectOfType<LevelService>();
        }

        protected override void Dispose()
        {
            _winMenu = null;

            _gameManager = null;
            _levelService = null;
        }

        protected override void SubscribeEvents()
        {
            _winMenu.NextLevelButtonClicked += _levelService.LoadGameScene;

            _gameManager.GameStateChanged += _winMenu.OnGameStateChangedToWin;
        }

        protected override void UnSubscribeEvents()
        {
            _winMenu.NextLevelButtonClicked -= _levelService.LoadGameScene;

            _gameManager.GameStateChanged -= _winMenu.OnGameStateChangedToWin;
        }

        #endregion Functions
    }
}
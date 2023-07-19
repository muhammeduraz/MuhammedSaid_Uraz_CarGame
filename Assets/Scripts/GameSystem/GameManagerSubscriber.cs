using Assets.Scripts.LevelSystem;
using Assets.Scripts.SceneSystem;
using Assets.Scripts.CanvasSystem;
using Assets.Scripts.SubscribeSystem;

namespace Assets.Scripts.GameSystem
{
    public class GameManagerSubscriber : BaseSubscriber 
    {
        #region Variables

        private GameManager _gameManager;

        private SceneService _sceneService;
        private LevelService _levelService;
        private PlayButtonHandler _playButtonHandler;

        #endregion Variables

        #region Functions

        protected override void Initialize()
        {
            _gameManager = FindObjectOfType<GameManager>();

            _sceneService = FindObjectOfType<SceneService>();
            _levelService = FindObjectOfType<LevelService>();
            _playButtonHandler = FindObjectOfType<PlayButtonHandler>();
        }

        protected override void Dispose()
        {
            _gameManager = null;

            _sceneService = null;
            _levelService = null;
            _playButtonHandler = null;
        }

        protected override void SubscribeEvents()
        {
            _gameManager.SceneReloadRequested += _sceneService.ReloadCurrentLevel;

            _playButtonHandler.PlayButtonClicked += _gameManager.OnPlayButtonClicked;
        }

        protected override void UnSubscribeEvents()
        {
            _gameManager.SceneReloadRequested -= _sceneService.ReloadCurrentLevel;

            _playButtonHandler.PlayButtonClicked -= _gameManager.OnPlayButtonClicked;
        }

        #endregion Functions
    }
}
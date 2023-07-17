using Assets.Scripts.LevelSystem;
using Assets.Scripts.SubscribeSystem;

namespace Assets.Scripts.CanvasSystem.Buttons
{
    public class PlayButtonSubscriber : BaseSubscriber 
    {
        #region Variables

        private PlayButton _playButton;

        private LevelService _levelService;

        #endregion Variables

        #region Functions

        protected override void Initialize()
        {
            _playButton = FindObjectOfType<PlayButton>();

            _levelService = FindObjectOfType<LevelService>();
        }

        protected override void Dispose()
        {
            _playButton = null;

            _levelService = null;
        }

        protected override void SubscribeEvents()
        {
            if (_playButton == null || _levelService == null) return;

            _playButton.PlayButtonClicked += _levelService.LoadGameSceneFromMenu;
        }

        protected override void UnSubscribeEvents()
        {
            if (_playButton == null || _levelService == null) return;

            _playButton.PlayButtonClicked -= _levelService.LoadGameSceneFromMenu;
        }

        #endregion Functions
    }
}
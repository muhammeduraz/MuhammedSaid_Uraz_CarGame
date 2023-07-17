using System;
using Assets.Scripts.SceneSystem;
using Assets.Scripts.SubscribeSystem;

namespace Assets.Scripts.LevelSystem
{
    public class LevelServiceSubscriber : BaseSubscriber 
    {
        #region Variables

        private LevelService _levelService;

        private SceneService _sceneService;

        #endregion Variables

        #region Functions

        protected override void Initialize()
        {
            _levelService = FindObjectOfType<LevelService>();

            _sceneService = FindObjectOfType<SceneService>();
        }

        protected override void Dispose()
        {
            _levelService = null;

            _sceneService = null;
        }

        protected override void SubscribeEvents()
        {
            if (_levelService == null) return;

            _levelService.LevelLoadRequested += _sceneService.OnLevelLoadRequested;
            _levelService.LevelUnloadRequested += _sceneService.OnLevelUnloadRequested;
            _levelService.LevelUnloadAndLoadRequested += _sceneService.OnLevelUnloadAndLoadRequested;
        }

        protected override void UnSubscribeEvents()
        {
            if (_levelService == null) return;

            _levelService.LevelLoadRequested -= _sceneService.OnLevelLoadRequested;
            _levelService.LevelUnloadRequested -= _sceneService.OnLevelUnloadRequested;
            _levelService.LevelUnloadAndLoadRequested -= _sceneService.OnLevelUnloadAndLoadRequested;
        }

        #endregion Functions
    }
}
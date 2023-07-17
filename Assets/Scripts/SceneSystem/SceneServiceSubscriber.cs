using Assets.Scripts.SubscribeSystem;
using Assets.Scripts.CanvasSystem.Loading;

namespace Assets.Scripts.SceneSystem
{
    public class SceneServiceSubscriber : BaseSubscriber 
    {
        #region Variables

        private SceneService _sceneService;

        private LoadingPanel _loadingPanel;

        #endregion Variables

        #region Functions

        protected override void Initialize()
        {
            _sceneService = FindObjectOfType<SceneService>();

            _loadingPanel = FindObjectOfType<LoadingPanel>();
        }

        protected override void Dispose()
        {
            _sceneService = null;

            _loadingPanel = null;
        }

        protected override void SubscribeEvents()
        {
            if (_sceneService == null) return;

            _sceneService.isLoadingPanelVisible += _loadingPanel.IsVisible;
            _sceneService.loadingPanelAppearDuration += _loadingPanel.AppearDuration;

            _sceneService.LoadingPanelAppearRequested += _loadingPanel.Appear;
            _sceneService.LoadingPanelDisappearRequested += _loadingPanel.Disappear;

            _sceneService.LoadingPanelProgressBarUpdateRequested += _loadingPanel.UpdateProgress;
            _sceneService.LoadingPanelProgressBarUpdateWithDurationRequested += _loadingPanel.UpdateProgress;
        }

        protected override void UnSubscribeEvents()
        {
            if (_sceneService == null) return;

            _sceneService.isLoadingPanelVisible -= _loadingPanel.IsVisible;
            _sceneService.loadingPanelAppearDuration -= _loadingPanel.AppearDuration;

            _sceneService.LoadingPanelAppearRequested -= _loadingPanel.Appear;
            _sceneService.LoadingPanelDisappearRequested -= _loadingPanel.Disappear;
            
            _sceneService.LoadingPanelProgressBarUpdateRequested -= _loadingPanel.UpdateProgress;
            _sceneService.LoadingPanelProgressBarUpdateWithDurationRequested -= _loadingPanel.UpdateProgress;
        }

        #endregion Functions
    }
}
using Assets.Scripts.GameSystem;
using Assets.Scripts.CanvasSystem;
using Assets.Scripts.SubscribeSystem;

namespace Assets.Scripts.CarSystem
{
    public class CarManagerSubscriber : BaseSubscriber 
    {
        #region Variables

        private CarManager _carManager;
        private GameManager _gameManager;

        private PlayButtonHandler _playButtonHandler;

        #endregion Variables

        #region Functions

        protected override void Initialize()
        {
            _carManager = FindObjectOfType<CarManager>();
            _gameManager = FindObjectOfType<GameManager>();

            _playButtonHandler = FindObjectOfType<PlayButtonHandler>(true);
        }

        protected override void Dispose()
        {
            _carManager = null;

            _gameManager = null;
            _playButtonHandler = null;
        }

        protected override void SubscribeEvents()
        {
            _playButtonHandler.PlayButtonClicked += _carManager.OnTapToPlayButtonClicked;

            _carManager.AllCarsCompleted += _gameManager.OnAllCarsCompleted;
            _carManager.TapToPlayButtonAppearRequested += _playButtonHandler.Initialize;
        }

        protected override void UnSubscribeEvents()
        {
            _playButtonHandler.PlayButtonClicked -= _carManager.OnTapToPlayButtonClicked;

            _carManager.AllCarsCompleted -= _gameManager.OnAllCarsCompleted;
            _carManager.TapToPlayButtonAppearRequested -= _playButtonHandler.Initialize;
        }

        #endregion Functions
    }
}
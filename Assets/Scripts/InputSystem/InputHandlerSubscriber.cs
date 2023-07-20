using Assets.Scripts.CanvasSystem;
using Assets.Scripts.SubscribeSystem;

namespace Assets.Scripts.InputSystem
{
    public class InputHandlerSubscriber : BaseSubscriber
    {
        #region Variables

        private InputHandler _inputHandler;

        private PlayButtonHandler _playerButtonHandler;

        #endregion Variables

        #region Functions

        protected override void Initialize()
        {
            _inputHandler = FindObjectOfType<InputHandler>();

            _playerButtonHandler = FindObjectOfType<PlayButtonHandler>();
        }

        protected override void Dispose()
        {
            _inputHandler = null;

            _playerButtonHandler = null;
        }

        protected override void SubscribeEvents()
        {
            _playerButtonHandler.PlayButtonClicked += _inputHandler.SubscribeToButtons;
        }

        protected override void UnSubscribeEvents()
        {
            _playerButtonHandler.PlayButtonClicked -= _inputHandler.SubscribeToButtons;
        }

        #endregion Functions
    }
}
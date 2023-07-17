using System;

namespace Assets.Scripts.CanvasSystem.Canvases
{
    public class MainCanvasHandler : BaseCanvas, IInitializable, IDisposable
    {
        #region Variables



        #endregion Variables

        #region Properties



        #endregion Properties

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion Unity Functions

        #region Functions

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion Functions
    }
}
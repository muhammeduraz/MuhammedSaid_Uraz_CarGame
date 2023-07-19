using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.CanvasSystem.Editor
{
    public class EventSystemChecker : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventSystem _eventSystemPrefab;

        #endregion Variables

        #region Unity Functions

        private void Awake()
        {
            Initialize();
        }

        #endregion Unity Functions

        #region Functions

        private void Initialize()
        {
            CheckEventSystem();
        }

        #endregion Functions

        #region Functions

        private void CheckEventSystem()
        {
            if (EventSystem.current == null)
            {
                Instantiate(_eventSystemPrefab, transform);
            }
        }

        #endregion Functions
    }
}
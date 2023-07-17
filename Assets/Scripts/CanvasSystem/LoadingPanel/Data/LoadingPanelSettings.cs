using UnityEngine;

namespace Assets.Scripts.CanvasSystem.Loading.Data
{
    [CreateAssetMenu (fileName = "LoadingPanelSettings", menuName = "Scriptable Objects/Canvas/LoadingPanel/Data/LoadingPanelSettings")]
    public class LoadingPanelSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _appearDuration;
        [SerializeField] private float _disappearDuration;

        #endregion Variables

        #region Properties

        public float AppearDuration { get => _appearDuration; }
        public float DisappearDuration { get => _disappearDuration; }

        #endregion Properties
    }
}
using UnityEngine;
using Sirenix.OdinInspector;
using Assets.Scripts.SceneSystem;

namespace Assets.Scripts.LevelSystem.Data
{
    [CreateAssetMenu (fileName = "LevelData", menuName = "Scriptable Objects/Data/Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        #region Variables

        [BoxGroup("Scene")][SerializeField] private SceneReference _sceneReference;

        #endregion Variables

        #region Properties

        public SceneReference SceneReference { get => _sceneReference; }

        #endregion Properties

        #region Functions

        #endregion Functions
    }
}
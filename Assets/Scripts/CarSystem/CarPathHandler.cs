using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.CarSystem.Data;

namespace Assets.Scripts.CarSystem
{
    public class CarPathHandler
    {
        #region Variables

        private float _currentInputValue;

        private CarPathData _pathData;

        private IVDData _currentIVDData;
        private List<IVDData> _iVDDataList;

        #endregion Variables

        #region Properties

        public CarPathData PathData { get => _pathData; }

        #endregion Properties

        #region Functions

        public CarPathHandler(CarPathData pathData)
        {
            _currentInputValue = -1f;

            _pathData = pathData;
            _iVDDataList = new List<IVDData>();
        }

        ~CarPathHandler()
        {
            _pathData = null;
            _iVDDataList = null;
            _currentIVDData = null;
        }

        public void LogPathData(float value)
        {
            if (_currentInputValue != value)
            {
                OnInputValueChanged(value);
            }

            _currentIVDData.duration += Time.deltaTime;
        }

        private void OnInputValueChanged(float value)
        {
            _currentInputValue = value;

            _currentIVDData = new IVDData(value);
            _iVDDataList.Add(_currentIVDData);
            Debug.LogError("  :::  " + _iVDDataList.Count);
        }

        public IVDData GetIVDDataByIndex(int index)
        {
                Debug.LogError(index + "  :::  " + _iVDDataList.Count);
            if (index < _iVDDataList.Count)
            {
                return _iVDDataList[index];
            }

            return null;
        }

        #endregion Functions
    }

    /// <summary>
    /// Input Value Duration Data
    /// </summary>
    public class IVDData
    {
        public float value;
        public float duration;

        public IVDData(float value, float duration = 0f)
        {
            this.value = value;
            this.duration = duration;
        }
    }
}
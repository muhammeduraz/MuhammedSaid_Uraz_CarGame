using System;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class SaveService
    {
        #region Variables

        private string _cachedString = string.Empty;
        private const string DebugPrefix = "SaveService";

        #endregion Variables

        #region Functions

        public void Save<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                SendLog($"Save failed: Key is null or empty.");
                return;
            }

            try
            {
                _cachedString = JsonUtility.ToJson(value);
                PlayerPrefs.SetString(key, _cachedString);
            }
            catch (Exception e)
            {
                SendLog($"Save failed: Serialization error. {e}");
            }

            _cachedString = string.Empty;
        }

        public T Load<T>(string key, T defaultValue = default)
        {
            if (string.IsNullOrEmpty(key))
            {
                SendLog($"Load failed: Key is null or empty, returning default value.");
                return defaultValue;
            }

            try
            {
                _cachedString = PlayerPrefs.GetString(key, string.Empty);

                if (string.IsNullOrEmpty(_cachedString))
                {
                    Save(key, defaultValue);
                    SendLog($"Load failed: Saved json value is null or empty, laoding default value.");
                    return defaultValue;
                }

                T savedObject = JsonUtility.FromJson<T>(_cachedString);
                return savedObject;
            }
            catch (Exception e)
            {
                SendLog($"Get failed: Deserialization error, returning default value. {e}");
                return defaultValue;
            }
        }

        public bool HasSave<T>(string key)
        {
            return !string.IsNullOrEmpty(key) && PlayerPrefs.HasKey(key);
        }

        public void Delete<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                SendLog($"Key is not or empty.");
                return;
            }

            if (!PlayerPrefs.HasKey(key))
            {
                SendLog($"Key does not exist: {key}");
                return;
            }

            PlayerPrefs.DeleteKey(key);
            SendLog($"Key deleted successfully: {key}");
        }

        public void DeleteSave()
        {
            bool isDeleteSuccessful = true;

            try
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                isDeleteSuccessful = false;
                SendLog($"Deleting error: {e.Message}");
            }
            finally
            {
                if (isDeleteSuccessful)
                {
                    SendLog($"Save deleted successfully.");
                }
            }
        }

        private void SendLog(string message)
        {
            Debug.Log($"{DebugPrefix}:{message}");
        }

        #endregion Functions
    }
}
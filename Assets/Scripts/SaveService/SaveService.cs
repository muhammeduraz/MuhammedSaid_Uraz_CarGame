using System;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public static class SaveService
    {
        #region Variables

        private const string DebugPrefix = "SaveService";

        #endregion Variables

        #region Functions

        public static void Save(string key, int value)
        {
            if (string.IsNullOrEmpty(key))
            {
                SendLog($"Save failed: Key is null or empty.");
                return;
            }

            try
            {
                PlayerPrefs.SetInt(key, value);
            }
            catch (Exception e)
            {
                SendLog($"Save failed: Serialization error. {e}");
            }
        }

        public static int Load(string key, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(key))
            {
                SendLog($"Load failed: Key is null or empty, returning default value.");
                return defaultValue;
            }

            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    Save(key, defaultValue);
                    SendLog($"Load failed: Saved json value is null or empty, laoding default value.");
                    return defaultValue;
                }

                int result = PlayerPrefs.GetInt(key, defaultValue);
                return result;
            }
            catch (Exception e)
            {
                SendLog($"Get failed: Deserialization error, returning default value. {e}");
                return defaultValue;
            }
        }

        public static bool HasSave(string key)
        {
            return !string.IsNullOrEmpty(key) && PlayerPrefs.HasKey(key);
        }

        public static void DeleteSave()
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

        private static void SendLog(string message)
        {
            Debug.Log($"{DebugPrefix}:{message}");
        }

        #endregion Functions
    }
}
using System;
using System.IO;
using UnityEngine;

namespace Saver
{
    public class ObjectSaver
    {
        public enum SaveType { JSON, PlayerPrefab }

        #region Inits Objects
        public static void InitObject<T>(T thisObject, SaveType saveType = SaveType.PlayerPrefab, string instanceID = "")
        {
            if (ObjectExist<T>(saveType, instanceID))
                LoadObject(ref thisObject, saveType, instanceID);
            else
                SaveObject(thisObject, saveType, instanceID);
        }
        #endregion

        #region Load Save Delete
        public static bool LoadObject<TObject>(ref TObject objectSaving, SaveType saveType = SaveType.PlayerPrefab, string savingId = "")
        {
            string contents = (saveType == SaveType.JSON) ? File.ReadAllText(GetSavingPathFile<TObject>(savingId)) : PlayerPrefs.GetString(typeof(TObject).ToString() + savingId);

            try
            {
                JsonUtility.FromJsonOverwrite(contents, objectSaving);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }

            return false;
        }
        public static bool SaveObject<TObject>(TObject currentObject, SaveType saveType = SaveType.PlayerPrefab, string savingId = "")
        {
            try
            {
                string contents = JsonUtility.ToJson(currentObject);
                if (saveType == SaveType.JSON)
                {
                    File.WriteAllText(GetSavingPathFile<TObject>(savingId), contents);
                    return true;
                }
                else
                {
                    PlayerPrefs.SetString(typeof(TObject).ToString() + savingId, contents);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }

            return false;
        }
        public static bool DeleteObject<TObject>(SaveType saveType = SaveType.PlayerPrefab, string savingId = "")
        {
            if (!ObjectExist<TObject>(saveType, savingId))
                return false;

            try
            {
                if (saveType == SaveType.JSON)
                {
                    File.Delete(GetSavingPathFile<TObject>(savingId));
                    return true;
                }
                else
                {
                    PlayerPrefs.DeleteKey(typeof(TObject).ToString() + savingId);
                    return true;
                }

            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }

            return false;
        }
        #endregion

        #region Get Paths and Check exist
        public static bool ObjectExist<TObject>(SaveType saveType = SaveType.PlayerPrefab, string savingId = "")
        {
            if (saveType == SaveType.JSON)
            {
                if (File.Exists(GetSavingPathFile<TObject>(savingId)))
                {
                    return true;
                }
            }
            else
            {
                if (PlayerPrefs.HasKey(typeof(TObject).ToString() + savingId))
                {
                    return true;
                }
            }

            return false;
        }
        public static string GetSavingPathFile<TObject>(string savingId = "")
        {
            if (!Directory.Exists(Application.persistentDataPath + "/data/"))
                Directory.CreateDirectory(Application.persistentDataPath + "/data/");

            return Application.persistentDataPath + "/data/" + typeof(TObject).ToString() + savingId + ".json";
        }
        public static string GetSavingPathDirectory()
        {
            return Application.persistentDataPath + "/data/";
        }
        #endregion

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Edit/Clear All Json Files", false, 266)]
        public static void ClearAllFiles()
        {
            string directoryPath = GetSavingPathDirectory();
            if (Directory.Exists(directoryPath))
            {
                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    File.Delete(file);
                }
            }

            Debug.Log("All Files Deleted path is: " + directoryPath);
        }
#endif
    }
}

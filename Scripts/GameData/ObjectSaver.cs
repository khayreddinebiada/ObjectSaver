using System;
using System.IO;
using UnityEngine;

namespace Saver
{
    public class ObjectSaver
    {
        public enum SaveType
        {
            JSON, PlayerPrefab
        }

        public static bool LoadObject<TObject>(ref TObject objectSaving, SaveType saveType = SaveType.JSON)
        {
            string contents = (saveType == SaveType.JSON) ? File.ReadAllText(GetSavingPathFile<TObject>()) : PlayerPrefs.GetString(typeof(TObject).ToString());

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

        public static bool SaveObject<TObject>(TObject currentObject, SaveType saveType = SaveType.JSON)
        {
            try
            {
                string contents = JsonUtility.ToJson(currentObject);
                if (saveType == SaveType.JSON)
                {
                    File.WriteAllText(GetSavingPathFile<TObject>(), contents);
                    return true;
                }
                else
                {
                    PlayerPrefs.SetString(typeof(TObject).ToString(), contents);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }

            return false;
        }

        public static bool DeleteObject<TObject>(SaveType saveType = SaveType.JSON)
        {
            if (!ObjectExist<TObject>(saveType))
                return false;

            try
            {
                if (saveType == SaveType.JSON)
                {
                    File.Delete(GetSavingPathFile<TObject>());
                    return true;
                }
                else
                {
                    PlayerPrefs.DeleteKey(typeof(TObject).ToString());
                    return true;
                }

            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }

            return false;
        }

        public static bool ObjectExist<TObject>(SaveType saveType = SaveType.JSON)
        {
            if (saveType == SaveType.JSON)
            {
                if (File.Exists(GetSavingPathFile<TObject>()))
                {
                    return true;
                }
            }
            else
            {
                if (PlayerPrefs.HasKey(typeof(TObject).ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetSavingPathFile<TObject>()
        {
            return Application.persistentDataPath + "/" + typeof(TObject).ToString() + ".json";
        }

        public static string GetSavingPathDirectory()
        {
            return Application.persistentDataPath + "/";
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Edit/Clear Json Files", false, 266)]
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

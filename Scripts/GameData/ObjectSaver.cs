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

        public static T LoadObject<T>(SaveType saveType = SaveType.JSON)
        {
            if (!CheckObjectExist<T>(saveType))
                return default(T);

            string contents = (saveType == SaveType.JSON) ? File.ReadAllText(GetSavingPath<T>()) : PlayerPrefs.GetString(typeof(T).ToString());

            try
            {
                return JsonUtility.FromJson<T>(contents);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }

            return default(T);
        }

        /*
        public static void ModifyObject<T>(T currentObject, SaveType saveType = SaveType.JSON)
        {
            string filePath = GetSavingPath<T>();

            try
            {
                string contents = JsonUtility.ToJson(currentObject);
                if (saveType == SaveType.JSON)
                    File.WriteAllText(filePath, contents);
                else
                    PlayerPrefs.SetString(typeof(T).ToString(), contents);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }
        }
        */

        public static void SaveObject<T>(T currentObject, SaveType saveType = SaveType.JSON)
        {

            try
            {
                string contents = JsonUtility.ToJson(currentObject);
                if (saveType == SaveType.JSON)
                {
                    File.WriteAllText(GetSavingPath<T>(), contents);
                }
                else
                    PlayerPrefs.SetString(typeof(T).ToString(), contents);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }
        }

        public static bool DeleteObject<T>(SaveType saveType = SaveType.JSON)
        {
            if (!CheckObjectExist<T>(saveType))
                return false;

            try
            {
                if (saveType == SaveType.JSON)
                {
                    File.Delete(GetSavingPath<T>());
                    return true;
                }
                else
                {
                    PlayerPrefs.DeleteKey(typeof(T).ToString());
                    return true;
                }

            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }

            return false;
        }

        public static bool CheckObjectExist<T>(SaveType saveType = SaveType.JSON)
        {
            if (saveType == SaveType.JSON)
            {
                string filePath = GetSavingPath<T>();

                if (File.Exists(GetSavingPath<T>()))
                {
                    return true;
                }
            }
            else
            {
                if (PlayerPrefs.HasKey(typeof(T).ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetSavingPath<T>()
        {
            return Application.persistentDataPath + typeof(T).ToString() + ".json";
        }
    }
}

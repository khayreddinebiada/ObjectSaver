using System;
using System.IO;

namespace UnityEngine
{
    public class ScriptableSaver
    {
        public static void ModifyObject<T>(T currentObject)
        {
            string filePath = Application.persistentDataPath + typeof(T).ToString() + ".json";

            SaveObjectIfNotExits<T>(filePath, currentObject);

            try
            {
                string contents = JsonUtility.ToJson(currentObject);
                File.WriteAllText(filePath, contents);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }
        }

        public static T LoadObject<T>(T currentObject)
        {
            string filePath = Application.persistentDataPath + typeof(T).ToString() + ".json";

            SaveObjectIfNotExits<T>(filePath, currentObject);

            string contents = File.ReadAllText(filePath);

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

        private static void SaveObjectIfNotExits<T>(string filePath, T currentObject)
        {
            if (File.Exists(filePath))
            {
                return;
            }

            try
            {
                string contents = JsonUtility.ToJson(currentObject);
                File.WriteAllText(filePath, contents);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }
        }

        public static bool DeleteObject(Type typeOfName)
        {
            string filePath = Application.persistentDataPath + typeOfName.ToString() + ".json";

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Error: " + e.Message);
                }
            }

            return false;
        }
    }
}

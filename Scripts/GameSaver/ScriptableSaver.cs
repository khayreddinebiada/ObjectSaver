using UnityEngine;

namespace Saver
{
    public class ScriptableSaver : ScriptableObject
    {
        #region Unique
        protected void InitObjectUnique<T>(T thisObject, ObjectSaver.SaveType saveType = ObjectSaver.SaveType.JSON)
        {
            if (ObjectSaver.ObjectExist<T>("", saveType))
                ObjectSaver.LoadObject(ref thisObject, "", saveType);
            else
                ObjectSaver.SaveObject(thisObject, "", saveType);
        }

        protected void SaveObjectUnique<T>(T thisObject, ObjectSaver.SaveType saveType = ObjectSaver.SaveType.JSON)
        {
            ObjectSaver.SaveObject(thisObject, "", saveType);
        }

        protected void DeleteObjectUnique<T>(ObjectSaver.SaveType saveType = ObjectSaver.SaveType.JSON)
        {
            ObjectSaver.DeleteObject<T>("", saveType);
        }
        #endregion

        #region Multi
        protected void InitObject<T>(T thisObject, ObjectSaver.SaveType saveType = ObjectSaver.SaveType.JSON)
        {
            if (ObjectSaver.ObjectExist<T>(GetInstanceID().ToString(), saveType))
                ObjectSaver.LoadObject(ref thisObject, GetInstanceID().ToString(), saveType);
            else
                ObjectSaver.SaveObject(thisObject, GetInstanceID().ToString(), saveType);
        }

        protected void SaveObject<T>(T thisObject, ObjectSaver.SaveType saveType = ObjectSaver.SaveType.JSON)
        {
            ObjectSaver.SaveObject(thisObject, GetInstanceID().ToString(), saveType);
        }

        protected void DeleteObject<T>(ObjectSaver.SaveType saveType = ObjectSaver.SaveType.JSON)
        {
            ObjectSaver.DeleteObject<T>(GetInstanceID().ToString(), saveType);
        }
        #endregion
    }
}

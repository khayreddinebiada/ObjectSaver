using UnityEngine;

namespace Saver
{
    public class MonoSaver : MonoBehaviour
    {
        protected void InitObject<T>(T thisObject)
        {
            if (ObjectSaver.ObjectExist<T>())
                ObjectSaver.LoadObject(ref thisObject);
            else
                ObjectSaver.SaveObject(thisObject);
        }

        protected void SaveObject<T>(T thisObject)
        {
            ObjectSaver.SaveObject(thisObject);
        }

        protected void DeleteObject<T>()
        {
            ObjectSaver.DeleteObject<T>();
        }
    }
}

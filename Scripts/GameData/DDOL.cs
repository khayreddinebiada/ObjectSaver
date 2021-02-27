using UnityEngine;

namespace GlobalData
{
    public class DDOL : MonoBehaviour
    {
        private static DDOL _instance;
        public static DDOL instance
        {
            get { return _instance; }
        }

        // Start is called before the first frame update
        private void Awake()
        {
            // Don't Destroy On Loading
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
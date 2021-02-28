using Saver;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo : MonoBehaviour
{

    [SerializeField]
    private Product product;

    private void Start()
    {
        print(Application.persistentDataPath);
        ObjectSaver.LoadObject(ref product);
    }

    public void GoScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}

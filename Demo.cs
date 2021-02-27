using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo : MonoBehaviour
{
    private void Start()
    {

    }

    public void GoScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}

using Saver;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo : MonoBehaviour
{
    private void Start()
    {
        Product pro = new Product();
        ObjectSaver.SaveObject(pro);
        pro.age = 0;
        Debug.Log("Before: " + pro.age);
        pro = ObjectSaver.LoadObject<Product>();
        Debug.Log("After Load: " + pro.age);
        ObjectSaver.DeleteObject<Product>();
        Debug.Log("CheckObject: " + ObjectSaver.CheckObjectExist<Product>());

    }

    public void GoScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}

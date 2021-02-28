using Saver;
using UnityEngine;

public class Product : MonoSaver
{
    public int age;
    public string namePlayer;
    public double[] tall;

    public void Awake()
    {
        InitObject(this);
    }

    public void SaveMe()
    {
        SaveObject(this);
    }

    public void DeleteMe()
    {
        DeleteObject<Product>();
    }
}

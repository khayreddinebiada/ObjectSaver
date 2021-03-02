# Scriptable-Saver
 
For save data it's very easy you need just call InitObject on create this class like example bellow:

    public void Awake()
    {
        ObjectSaver.InitObject(this, ObjectSaver.SaveType.PlayerPrefab);
    }
    
    // Example of constructor.
    public Product()
    {
        ObjectSaver.InitObject(this, ObjectSaver.SaveType.PlayerPrefab);
    }
    
InitObject will automatic check the file if exist or no if exist will load on the object class if not exist it will create new file and save the object data there.

When you want save or delete you can use this: 

        ObjectSaver.SaveObject(this, ObjectSaver.SaveType.PlayerPrefab); // Save file
        
        ObjectSaver.DeleteObject<Product>(ObjectSaver.SaveType.PlayerPrefab); // Delete file.
        
You can save as PlayerPrefab or Json format. And you can save as unique instance and multi instance.

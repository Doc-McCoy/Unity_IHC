using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Persistence
{
    public static void Save<T>(T data, string filename)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + filename, json);
    }

    public static T Load<T>(string filename)
    {
        T data = default(T);
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + filename;
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<T>(json);
        }

        return data;
    }
}

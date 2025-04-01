using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public class DataBase : MonoBehaviour
{
    [SerializeField] internal string fileName; 
    internal static DataBase instance;
    internal SavableInventory Load()
    {
        SavableInventory output = new();
        try
        {
            Debug.Log("Loading init.");
#if UNITY_EDITOR
            string path = Path.Combine(Application.persistentDataPath,fileName+".json");
            if (File.Exists(path))
            {
                output = JsonConvert.DeserializeObject<SavableInventory>(File.ReadAllText(path));
            }
#endif
#if UNITY_WEBGL
            if (PlayerPrefs.HasKey(fileName))
            {
                output = JsonConvert.DeserializeObject<SavableInventory>(PlayerPrefs.GetString(fileName));
            }
#endif
            Debug.Log("Loaded!");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Failed to load.\nMessage:{e.Message}\nStack:{e.StackTrace}");
        }
        return output;
    }
    internal void Save(object toSafe)
    {
        try
        {
            Debug.Log("Saving init.");
            string toWrite = JsonConvert.SerializeObject(toSafe);
            #if UNITY_EDITOR
            string path = Path.Combine(Application.persistentDataPath,fileName+".json");
            if (File.Exists(path)) File.Delete(path);
            using StreamWriter writer = new(path,true);
            writer.Write(toWrite);
            #endif
            #if UNITY_WEBGL
            PlayerPrefs.SetString(fileName,toWrite);
            PlayerPrefs.Save();         
            #endif
            Debug.Log("Saved!");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Failed to load.\nMessage:{e.Message}\nStack:{e.StackTrace}");
        }

    }
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

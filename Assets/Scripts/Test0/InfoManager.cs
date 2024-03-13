using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class InfoManager
{
    public static readonly InfoManager Instance = new InfoManager();

    private InfoManager() { }

    public ZooInfo ZooInfo
    {
        get; set;
    }
    public void SaveLocal()
    {
        Debug.Log("<color=yellow>SaveLocal</color>");
        var json = JsonConvert.SerializeObject(ZooInfo);
        string path = Path.Combine(Application.persistentDataPath, "zoo_info.json");
        Debug.Log(path);
        File.WriteAllText(path, json);
        Debug.Log("save complete!");
    }
}

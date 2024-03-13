using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainTest0 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ZooInfo zooInfo = null;

        string path = Path.Combine(Application.persistentDataPath, "zoo_info.json");

        //if (File.Exists(path))
        //{
        Debug.Log("<color=yellow>기존유저</color>");
        string json = File.ReadAllText(path);
        Debug.Log(json);
        zooInfo = JsonConvert.DeserializeObject<ZooInfo>(json);
        InfoManager.Instance.ZooInfo = zooInfo;
        //}
        //else
        //{
        //    Debug.Log("<color=yellow>신규유저</color>");
        //    zooInfo = new ZooInfo();
        //    zooInfo.Init();
        //    InfoManager.Instance.ZooInfo = zooInfo;
        //    InfoManager.Instance.SaveLocal();
        //}
        List<AnimalInfo> animalInfos = InfoManager.Instance.ZooInfo.animalInfos;

        foreach (AnimalInfo info in animalInfos)
        {
            Debug.Log(info);
            if (info.type == 0)
            {
                HerbivoreInfo herbivoreInfo = info as HerbivoreInfo;
                Debug.Log(herbivoreInfo);
            }
            //else if(info.type == 1) {
            //    CarnivoreInfo carnivoreInfo = info as CarnivoreInfo;
            //    Debug.Log(carnivoreInfo);
            //}
        }

        AnimalInfo lion = new CarnivoreInfo("Lion", 1, 2, 3);
        var lionC = lion as CarnivoreInfo;
        Debug.Log(lion);
        Debug.LogFormat("{0}, {1}, {2}, {3}", lion.name, lion.type, lion.moveSpeed, lionC.damage);
    }
}

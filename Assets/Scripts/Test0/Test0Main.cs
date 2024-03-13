using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test0Main : MonoBehaviour
{
    void Start()
    {
        // 해당 Start구문에서 함수들은 테스트를 하기 위해서는 반복 실행하기에 따로 빼주었다.
        // 핵심 및 추가 정보는 DeserializeInfo() 메소드 참조

        // DeserializeObject 실행하고 해당 값을 return해 주었다.
        ZooInfo zooInfo = DeserializeInfo();

        // return된 값, 즉 zoo_info.json파일의 정보를 다른곳에서 가져다 쓰기 위하여 값을 싱글톤 클래스에 할당해주었다.
        InfoManager.Instance.zooInfo = zooInfo;
        
        // 할당된 ZooInfo중 AnimalInfo들의 정보인 List를 사용하기 위해 가져왔다.
        List<AnimalInfo> animalInfos = InfoManager.Instance.zooInfo.animalInfos;

        // animalInfos의 각 객체별로 Debug.Log() 하였다.
        Debugging(animalInfos);

        // 테스트를 위한 AnimalInfo를 CarnivoreInfo를 통해 객체 생성
        AnimalInfo lion = new CarnivoreInfo("Lion", 1, 2, 3);

        // 업캐스팅후 다운캐스팅을 진행하면 자식객체의 정보가 없어지는지 확인해보았다.
        CarnivoreInfo lionC = lion as CarnivoreInfo;    // == (CarnivoreInfo)lion

        // 출력 정보 -> lion : CarnivoreInfo, lionC : CarnivoreInfo
        // 출력 정보 -> Lion, 1, 2, 3
        Debug.LogFormat("lion : {0}, lionC : {1}", lion, lionC);
        Debug.LogFormat("{0}, {1}, {2}, {3}", lionC.name, lionC.type, lionC.moveSpeed, lionC.damage);

        // 기존 AnimalInfo List에 새로운 AnimalInfo lion을 Add()로 추가해 주었다.
        animalInfos.Add(lion);
        
        // 기존과 다른 변경된 정보를 가진 animalInfos를 new ZooInfo를 통해 ZooInfo를 최신화 하였다.  
        InfoManager.Instance.zooInfo = new ZooInfo(animalInfos);
        
        // 최신화 된 ZooInfo를 zoo_info.json으로 파일을 저장해주었다.
        InfoManager.Instance.SaveLocal();

        // 최신화 후 저장된 ZooInfo가 원하는 결과값이 나오는지 즉시 확인해보기 위해
        // 저장된 zoo_info.json파일을 다시 불러왔다.
        InfoManager.Instance.zooInfo = DeserializeInfo();

        animalInfos = InfoManager.Instance.zooInfo.animalInfos;
        Debugging (animalInfos);
    }

    ZooInfo DeserializeInfo()
    {
        // json파일을 읽기 또는 쓰기를 위해 사용하는 JsonSerializerSettings 클래스를 인스턴스하여 객체를 가져온다.
        JsonSerializerSettings settings = new JsonSerializerSettings();

        // 자신이 만든 Converter인 AnimalInfoConverter 객체를 인스턴스화 하여 정보를 가져오고
        // settings에서 Converter의 IList인 Converters에 Add()로 추가해주었다.
        settings.Converters.Add(new AnimalInfoConverter());

        // Application 저장소 C:\Users\Computer\AppData\LocalLow\DefaultCompany\"[UNITY-PROJECTNAME]"\
        // 위 저장소에 존재하는 zoo_info.json을 가져온다.
        // 해당 info.json 파일은 테스트를 위해 만들어 넣어 두었기 때문에 Null체크는 따로 하지 않았다.
        string path = Path.Combine(Application.persistentDataPath, "zoo_info.json");
        string json = File.ReadAllText(path);

        // 가져온 json파일의 문자열(string)을 Newtonsoft의 JsonConvert를 통하여 역직렬화 하였다.
        // 이때 위에서 자신의 Converter가 포함되있는 Converters의 객체 settings를 넣어준다.  
        ZooInfo info = JsonConvert.DeserializeObject<ZooInfo>(json, settings);
        return info;
    }

    void Debugging(List<AnimalInfo> animalInfos)
    {
        foreach (AnimalInfo info in animalInfos)
        {
            if (info.type == 0)
            {
                HerbivoreInfo herbivoreInfo = info as HerbivoreInfo;
                Debug.Log(herbivoreInfo);
            }
            else if (info.type == 1)
            {
                CarnivoreInfo carnivoreInfo = info as CarnivoreInfo;
                Debug.Log(carnivoreInfo);
            }
        }
    }
}

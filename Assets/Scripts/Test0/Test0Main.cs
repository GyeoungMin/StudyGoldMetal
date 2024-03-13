using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test0Main : MonoBehaviour
{
    void Start()
    {
        // �ش� Start�������� �Լ����� �׽�Ʈ�� �ϱ� ���ؼ��� �ݺ� �����ϱ⿡ ���� ���־���.
        // �ٽ� �� �߰� ������ DeserializeInfo() �޼ҵ� ����

        // DeserializeObject �����ϰ� �ش� ���� return�� �־���.
        ZooInfo zooInfo = DeserializeInfo();

        // return�� ��, �� zoo_info.json������ ������ �ٸ������� ������ ���� ���Ͽ� ���� �̱��� Ŭ������ �Ҵ����־���.
        InfoManager.Instance.zooInfo = zooInfo;
        
        // �Ҵ�� ZooInfo�� AnimalInfo���� ������ List�� ����ϱ� ���� �����Դ�.
        List<AnimalInfo> animalInfos = InfoManager.Instance.zooInfo.animalInfos;

        // animalInfos�� �� ��ü���� Debug.Log() �Ͽ���.
        Debugging(animalInfos);

        // �׽�Ʈ�� ���� AnimalInfo�� CarnivoreInfo�� ���� ��ü ����
        AnimalInfo lion = new CarnivoreInfo("Lion", 1, 2, 3);

        // ��ĳ������ �ٿ�ĳ������ �����ϸ� �ڽİ�ü�� ������ ���������� Ȯ���غ��Ҵ�.
        CarnivoreInfo lionC = lion as CarnivoreInfo;    // == (CarnivoreInfo)lion

        // ��� ���� -> lion : CarnivoreInfo, lionC : CarnivoreInfo
        // ��� ���� -> Lion, 1, 2, 3
        Debug.LogFormat("lion : {0}, lionC : {1}", lion, lionC);
        Debug.LogFormat("{0}, {1}, {2}, {3}", lionC.name, lionC.type, lionC.moveSpeed, lionC.damage);

        // ���� AnimalInfo List�� ���ο� AnimalInfo lion�� Add()�� �߰��� �־���.
        animalInfos.Add(lion);
        
        // ������ �ٸ� ����� ������ ���� animalInfos�� new ZooInfo�� ���� ZooInfo�� �ֽ�ȭ �Ͽ���.  
        InfoManager.Instance.zooInfo = new ZooInfo(animalInfos);
        
        // �ֽ�ȭ �� ZooInfo�� zoo_info.json���� ������ �������־���.
        InfoManager.Instance.SaveLocal();

        // �ֽ�ȭ �� ����� ZooInfo�� ���ϴ� ������� �������� ��� Ȯ���غ��� ����
        // ����� zoo_info.json������ �ٽ� �ҷ��Դ�.
        InfoManager.Instance.zooInfo = DeserializeInfo();

        animalInfos = InfoManager.Instance.zooInfo.animalInfos;
        Debugging (animalInfos);
    }

    ZooInfo DeserializeInfo()
    {
        // json������ �б� �Ǵ� ���⸦ ���� ����ϴ� JsonSerializerSettings Ŭ������ �ν��Ͻ��Ͽ� ��ü�� �����´�.
        JsonSerializerSettings settings = new JsonSerializerSettings();

        // �ڽ��� ���� Converter�� AnimalInfoConverter ��ü�� �ν��Ͻ�ȭ �Ͽ� ������ ��������
        // settings���� Converter�� IList�� Converters�� Add()�� �߰����־���.
        settings.Converters.Add(new AnimalInfoConverter());

        // Application ����� C:\Users\Computer\AppData\LocalLow\DefaultCompany\"[UNITY-PROJECTNAME]"\
        // �� ����ҿ� �����ϴ� zoo_info.json�� �����´�.
        // �ش� info.json ������ �׽�Ʈ�� ���� ����� �־� �ξ��� ������ Nullüũ�� ���� ���� �ʾҴ�.
        string path = Path.Combine(Application.persistentDataPath, "zoo_info.json");
        string json = File.ReadAllText(path);

        // ������ json������ ���ڿ�(string)�� Newtonsoft�� JsonConvert�� ���Ͽ� ������ȭ �Ͽ���.
        // �̶� ������ �ڽ��� Converter�� ���Ե��ִ� Converters�� ��ü settings�� �־��ش�.  
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

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> playerBulletPrefabs;
    [SerializeField] private List<GameObject> enemyBulletPrefabs;
    [SerializeField] private List<GameObject> itemPrefabs;
    [SerializeField] private GameObject followerPrefab;
    [SerializeField] private GameObject followerBulletPrefab;

    //List<IObjectPool<GameObject>> pools;

    private Dictionary<string, IObjectPool<GameObject>> objectDictionary = new Dictionary<string, IObjectPool<GameObject>>();

    private void Start()
    {
        //CreateEmptyObjects(enemyPrefabs, "Enemys");
        CreateEmptyObjects(playerBulletPrefabs, "Player Bullets");
        CreateEmptyObjects(itemPrefabs, "Items");
        //CreateEmptyObject(followerPrefab);
    }

    private void CreateEmptyObjects(List<GameObject> prefabs, string groupName)
    {
        GameObject parentObject = new GameObject(groupName);
        foreach (GameObject prefab in prefabs)
        {
            //CreateEmptyObject(prefab, groupName);
            GameObject emptyObject = CreateEmptyObject(prefab);
            emptyObject.transform.SetParent(parentObject.transform, false);
        }
    }

    private GameObject CreateEmptyObject(GameObject prefab)
    {
        GameObject emptyObject = new GameObject(prefab.name);
        IObjectPool<GameObject> pool = ObjectPoolManager2.Instance.CreateObjectPool<IPoolable>(emptyObject, prefab, 10);
        objectDictionary.Add(prefab.name, pool);

        return emptyObject;
    }

    private void CreatePoolObjects(List<GameObject> prefabs, string groupName)
    {

    }

    private void CreatePoolObject(GameObject prefab)
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            objectDictionary[playerBulletPrefabs[0].name].Get();
        }
    }
}

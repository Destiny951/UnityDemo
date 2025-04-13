using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static EnemyPoolManager;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    [System.Serializable]
    public class ObjectPoolConfig
    {
        public string objectTag;                    // �����ڽű��е���
        public GameObject prefab;                       // Ԥ����
        public int initialSize = 1;                // ��ʼ��С
        public int maxSize = 5;                   // �������
    }

    [Header("Object config")]
    public List<ObjectPoolConfig> poolConfigs;

    private Dictionary<string, ObjectPool<GameObject>> poolDict;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }
    private void Start()
    {
        poolDict = new Dictionary<string, ObjectPool<GameObject>>();
        InitAllPools();
    }

    private void InitAllPools()
    {
        foreach (var config in poolConfigs)
        {
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => {
                    var objectItem = Instantiate(config.prefab);
                    objectItem.gameObject.SetActive(false);
                    return objectItem;
                },
                actionOnGet: objectItem => {
                    objectItem.gameObject.SetActive(true);
                },
                actionOnRelease: objectItem => {
                    objectItem.gameObject.SetActive(false);
                },
                actionOnDestroy: objectItem => {
                    Destroy(objectItem.gameObject);
                },
                collectionCheck: true,
                defaultCapacity: config.initialSize,
                maxSize: config.maxSize
            );

            poolDict.Add(config.objectTag, pool);
        }
    }

    public GameObject GetObjectItem(string tag)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogError($"��������δע�᣺{tag}");
            return null;
        }
        return poolDict[tag].Get();
    }

    public void ReturnObjectItem(string tag, GameObject objectItem)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning($"����ʧ�ܣ���������δע�᣺{tag}");
            return;
        }

        poolDict[tag].Release(objectItem);
    }
}

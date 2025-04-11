using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager instance;

    [System.Serializable]
    public class EnemyPoolConfig
    {
        public string enemyTag;                    // �����ڽű��е���
        public Enemy prefab;                       // Ԥ����
        public int initialSize = 5;                // ��ʼ��С
        public int maxSize = 20;                   // �������
    }

    [System.Serializable]
    public class EnemySpawnConfig  //�������õ�������
    {
        public string enemyTag;
        public List<Transform> spawnPoints = new List<Transform>();
    }

    [Header("Enemy config")]
    [SerializeField] private List<EnemyPoolConfig> poolConfigs;
    [SerializeField] private List<EnemySpawnConfig> spawnConfigs;

    private Dictionary<string, ObjectPool<Enemy>> poolDict;

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
        poolDict = new Dictionary<string, ObjectPool<Enemy>>();
        InitAllPools();
        SpawnAllConfiguredEnemies();
    }

    public void SpawnAllConfiguredEnemies()
    {
        foreach (var config in spawnConfigs)
        {
            foreach (var point in config.spawnPoints)
            {
                var enemy = GetEnemy(config.enemyTag);
                if (enemy == null)
                {
                    Debug.LogError($"GetEnemy ���� null��Tag: {config.enemyTag}");
                    continue;
                }
                enemy.transform.position = point.position;
            }
        }
    }

    private void InitAllPools()
    {
        foreach (var config in poolConfigs)
        {
            ObjectPool<Enemy> pool = new ObjectPool<Enemy>(
                createFunc: () => {
                    var enemy = Instantiate(config.prefab);
                    enemy.gameObject.SetActive(false);
                    return enemy;
                },
                actionOnGet: enemy => {
                    enemy.gameObject.SetActive(true);
                },
                actionOnRelease: enemy => {
                    enemy.gameObject.SetActive(false);
                },
                actionOnDestroy: enemy => {
                    Destroy(enemy.gameObject);
                },
                collectionCheck: true,
                defaultCapacity: config.initialSize,
                maxSize: config.maxSize
            );

            poolDict.Add(config.enemyTag, pool);
        }
    }

    public Enemy GetEnemy(string tag)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogError($"��������δע�᣺{tag}");
            return null;
        }
        return poolDict[tag].Get();
    }

    public void ReturnEnemy(string tag, Enemy enemy)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning($"����ʧ�ܣ���������δע�᣺{tag}");
            return;
        }

        poolDict[tag].Release(enemy);
    }

}

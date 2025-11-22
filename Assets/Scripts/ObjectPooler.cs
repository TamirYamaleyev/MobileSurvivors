using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, Transform> poolParents; // Store parent containers

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        poolParents = new Dictionary<string, Transform>();

        foreach (Pool pool in pools)
        {
            // Create a parent GameObject for this pool
            GameObject parent = new GameObject(pool.tag + "_Pool");
            parent.transform.parent = this.transform; // optional: keep under ObjectPooler
            poolParents.Add(pool.tag, parent.transform);

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                // Parent it under the pool container
                obj.transform.parent = parent.transform;

                // Make sure bullets/enemies know their pool tag
                PooledObject pooledObj = obj.GetComponent<PooledObject>();
                if (pooledObj == null)
                    obj.AddComponent<PooledObject>().poolTag = pool.tag;
                else
                    pooledObj.poolTag = pool.tag;

                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn’t exist.");
            return null;
        }

        // Find an inactive object in the pool
        GameObject objToSpawn = null;
        foreach (var item in poolDictionary[tag])
        {
            if (!item.activeInHierarchy)
            {
                objToSpawn = item;
                break;
            }
        }

        if (objToSpawn == null)
        {
            Debug.LogWarning("No inactive objects left in pool: " + tag);
            return null;
        }

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        return objToSpawn;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);

        // Optional: reparent to pool container in case it was moved in the scene
        PooledObject pooledObj = obj.GetComponent<PooledObject>();
        if (pooledObj != null && poolParents.ContainsKey(pooledObj.poolTag))
        {
            obj.transform.parent = poolParents[pooledObj.poolTag];
        }
    }
}

// Simple helper component to store pool tag
public class PooledObject : MonoBehaviour
{
    public string poolTag;
}

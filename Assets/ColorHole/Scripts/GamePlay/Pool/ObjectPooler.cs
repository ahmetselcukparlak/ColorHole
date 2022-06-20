using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPooler : Singleton<ObjectPooler>
{
    [Serializable]
    public class PoolInfo
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<PoolInfo> pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            var objPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);

                objPool.Enqueue(obj);
            }
            _poolDictionary.Add(pool.tag, objPool);
        }
    }

    public GameObject SpawnFromPool(string poolTag, Vector3 position, Quaternion? rotation = null, Transform parent = null)
    {
        if (!_poolDictionary.ContainsKey(poolTag)) return null;

        var objectToSpawn = _poolDictionary[poolTag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetParent(parent ? parent : transform);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation ?? objectToSpawn.transform.rotation;

        _poolDictionary[poolTag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    public T SpawnFromPool<T>(string poolTag, Vector3 position, Quaternion? rotation = null, Transform parent = null)
    {
        return SpawnFromPool(poolTag, position, rotation).GetComponent<T>();
    }
    public void DestroyFromPool(GameObject poolObject)
    {
        poolObject.SetActive(false);
        poolObject.transform.SetParent(transform);
    }
    public void DestroyFromPool(GameObject poolObject, float t)
    {
        StartCoroutine(DestroyFromPoolCoroutine(poolObject, t));
    }
    private IEnumerator DestroyFromPoolCoroutine(GameObject poolObject, float t)
    {
        yield return new WaitForSeconds(t);
        DestroyFromPool(poolObject);
    }
}
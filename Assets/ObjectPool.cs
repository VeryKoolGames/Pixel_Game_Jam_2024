using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform, true);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.GetComponent<PoolableObject>().OnObjectSpawn();
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            obj.GetComponent<PoolableObject>().OnObjectSpawn();
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.GetComponent<PoolableObject>().OnObjectDeactivate();
        pool.Enqueue(obj);
    }
}
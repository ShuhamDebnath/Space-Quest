using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public GameObject preFab;
    public float poolSize;
    public List<GameObject> pool;

    void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }

    }

    private GameObject CreateNewObject()
    {

        GameObject obj = Instantiate(preFab, transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public GameObject GetPoolObject()
    {
        foreach (GameObject gameObject in pool)
        {
            if (!gameObject.activeSelf)
            {
                return gameObject;
            }

        }
        return CreateNewObject();
    }
}

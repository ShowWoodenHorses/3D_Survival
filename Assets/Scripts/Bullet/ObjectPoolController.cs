using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{

    public GameObject prefab;
    [SerializeField] private protected int sizePool;
    public List<GameObject> objectsPool;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        objectsPool = new List<GameObject>();
        for (int i = 0; i < sizePool; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectsPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < objectsPool.Count; i++)
        {
            if (!objectsPool[i].activeInHierarchy)
            {
                return objectsPool[i];
            }
        }

        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        objectsPool.Add(newObj);

        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }
        obj.SetActive(false);
        Rigidbody objRB = obj.GetComponent<Rigidbody>();
        if (objRB != null)
        {
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }
}

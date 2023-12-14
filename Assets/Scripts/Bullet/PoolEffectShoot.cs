using System.Collections.Generic;
using UnityEngine;

public class PoolEffectShoot : ObjectPoolController
{
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
}

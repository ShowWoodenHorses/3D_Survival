using System;
using System.Collections;
using UnityEngine;

public class EffectShoot : MonoBehaviour
{
    private PoolEffectShoot _poolEffectShoot;


    public void Initialize(PoolEffectShoot poolEffectShoot)
    {
        _poolEffectShoot = poolEffectShoot;
    }

    private void Update()
    {
        StartCoroutine(EffectDestroy());
    }

    private IEnumerator EffectDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        _poolEffectShoot.ReturnObjectToPool(this.gameObject);
    }
}

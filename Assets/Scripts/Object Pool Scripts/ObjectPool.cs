using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _poolSize = 5;
    [SerializeField] private float _spawnRate = 1f;

    private GameObject[] _pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        _pool = new GameObject[_poolSize];

        for (int i = 0; i < _pool.Length; i++)
        {
            _pool[i] = Instantiate(_enemyPrefab, transform);
            _pool[i].SetActive(false);
        }
    }

    private void EnableObjectInPool()
    {
        foreach (GameObject pooledObject in _pool)
        {
            if (!pooledObject.activeInHierarchy)
            {
                pooledObject.SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnPrefab()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(_spawnRate);
        }
    }
}

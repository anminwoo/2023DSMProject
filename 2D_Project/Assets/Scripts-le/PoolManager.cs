using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private int maxPoolSize;
    private IObjectPool<Enemy> enemyPool;

    private void Awake()
    {
        
    }
}

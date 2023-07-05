using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] private int maxPoolSize;
    private IObjectPool<Enemy> enemyPool;

    private void Awake()
    {
        instance = this;
        
        enemyPool = new ObjectPool<Enemy>(
            SpawnEnemy,
            onGet,
            onRelease,
            onDestroy,
            false,
            maxSize:maxPoolSize);
    }

    private Enemy SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab, transform);
        enemy.SetPool(enemyPool);
        return enemy;
    }
    public void onGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    public void onRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public void onDestroy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            enemyPool.Get();
        }
    }
}

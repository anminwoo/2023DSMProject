using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] private int defaultCap;
    [SerializeField] private int maxPoolSize;
    private IObjectPool<Enemy> enemyPool;

    private void Awake()
    {
        instance = this;
        
        enemyPool = new ObjectPool<Enemy>(
            SpawnEnemy,
            OnGet,
            OnRelease,
            OnDestroy,
            false,
            defaultCap,
            maxPoolSize);
    }

    private Enemy SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.SetPool(enemyPool);
        return enemy;
    }

    public void OnGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    public void OnRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public void OnDestroy(Enemy enemy)
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

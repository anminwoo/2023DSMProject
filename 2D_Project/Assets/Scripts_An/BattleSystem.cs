using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private static BattleSystem _singleton;

    public static BattleSystem Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton	!= value)
            {   
                Debug.Log("경고 : 이미 배틀시스템이 존재합니다!");
                Destroy(value.gameObject);
            }
        }
    }

    [SerializeField] public bool isBattle;

    public BoxCollider2D[] lockers;

    public Wave[] waves;

    public int currentWave;

    private void Awake()
    {
        Singleton = this;
    }

    public void BattleStart()
    {
        isBattle = true;
        for (int i = lockers.Length - 1; i >= 0; i--)
        {
            lockers[i].isTrigger = false;
        }

        for (int i = 0; i < waves[currentWave].enemySpawnCount; i++)
        {
            PoolManager.instance.enemyPool.Get();
        }

        currentWave++;
    }

    public void BattleStop()
    {
        isBattle = false;
        for (int i = lockers.Length - 1; i >= 0; i--)
        {
            lockers[i].isTrigger = true;
        }
    }

    [Serializable]
    public struct Wave
    {
        public int enemySpawnCount;
    }
}
